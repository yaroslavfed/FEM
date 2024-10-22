using System.Diagnostics.CodeAnalysis;
using FEM.Common.Core.Services.BoundaryConditionService;
using FEM.Common.Core.Services.GlobalMatrixService;
using FEM.Common.Core.Services.InaccuracyService;
using FEM.Common.Core.Services.MatrixPortraitService;
using FEM.Common.Core.Services.RightPartVectorService;
using FEM.Common.Core.Services.SolverService;
using FEM.Common.Core.Services.TestResultService;
using FEM.Common.Core.Services.TestSessionService;
using FEM.Common.Services.VisualizerService;
using FEM.Server.Data.OutputModels;
using FEM.Stationary.DTO.Configurations;
using FEM.Stationary.DTO.TestingContext;
using Microsoft.AspNetCore.Mvc;

namespace FEM.Server.Controllers;

/// <summary>
/// Контроллер для решения стационарного уравнения векторного МКЭ
/// </summary>
[ApiController]
[Route("api/fem/stationary")]
public class StationaryFemController : ControllerBase
{
    private readonly ILogger                   _logger;
    private readonly IGlobalMatrixServices     _globalMatrixServices;
    private readonly ITestSessionService       _testSessionService;
    private readonly IMatrixPortraitService    _portraitService;
    private readonly IRightPartVectorService   _rightPartVectorService;
    private readonly IVisualizerService        _visualizerService;
    private readonly IBoundaryConditionFactory _boundaryCondition;
    private readonly ISolverService            _solverService;
    private readonly ITestResultService        _testResultService;
    private readonly IInaccuracyService        _inaccuracyService;

    /// <inheritdoc />
    public StationaryFemController(
        ILogger<StationaryFemController> logger,
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition,
        ISolverService solverService,
        ITestResultService testResultService,
        IInaccuracyService inaccuracyService
    )
    {
        _logger = logger;
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
        _boundaryCondition = boundaryCondition;
        _solverService = solverService;
        _testResultService = testResultService;
        _inaccuracyService = inaccuracyService;
    }

    /// <summary>
    /// Решает стационарное уравнение с помощью векторного МКЭ
    /// </summary>
    /// <param name="testConfiguration"><see cref="StationaryTestSession">Входные параметры расчётной сессии</see></param>
    /// /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "meshParameters": {
    ///            "xCenterCoordinate": 0.5,
    ///            "yCenterCoordinate": 0.5,
    ///            "zCenterCoordinate": 0.5,
    ///            "xStepToBounds": 0.5,
    ///            "yStepToBounds": 0.5,
    ///            "zStepToBounds": 0.5
    ///        },
    ///        "splittingParameters": {
    ///            "xSplittingCoefficient": 4,
    ///            "ySplittingCoefficient": 4,
    ///            "zSplittingCoefficient": 4,
    ///            "xMultiplyCoefficient": 1,
    ///            "yMultiplyCoefficient": 1,
    ///            "zMultiplyCoefficient": 1
    ///        },
    ///        "additionParameters": {
    ///            "muCoefficient": 1,
    ///            "gammaCoefficient": 1,
    ///            "boundaryCondition": 0
    ///        }
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Возвращает id результата, невязку и количество итераций</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Решение уравнения</returns>
    [HttpPost(Name = "vector-fem-solver")]
    [ProducesResponseType(typeof(FemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<IActionResult> CreateCalculation([FromBody] StationaryTestConfiguration testConfiguration)
    {
        Console.WriteLine($"[{nameof(StationaryFemController)}] [Info] Started session");
        _logger.LogInformation($"[{nameof(StationaryFemController)}] [Info] Started session");

        try
        {
            _logger.LogInformation(
                $"[{nameof(StationaryFemController)}] {nameof(CreateCalculation)} initialize calculation"
            );
            _logger.LogInformation($"[{nameof(StationaryFemController)}] create test session");

            // Создаем сессию тестирования
            var testSession = await _testSessionService.CreateTestSessionAsync(testConfiguration);

            Console.WriteLine($"[{nameof(StationaryFemController)}] [Info] Test session created");
            _logger.LogInformation($"[{nameof(StationaryFemController)}] [Info] Test session created");
            _logger.LogInformation($"[{nameof(StationaryFemController)}] save plots to images");

            // Отрисовываем графики расчётной области
            await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);

            Console.WriteLine($"[{nameof(StationaryFemController)}] [Info] Mesh`s plots were created");
            _logger.LogInformation($"[{nameof(StationaryFemController)}] [Info] Mesh`s plots were created");
            _logger.LogInformation($"[{nameof(StationaryFemController)}] resolve matrix portrait");

            // Создаем профиль матрицы
            var matrixProfile = await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh);

            // Инициализируем вектора профиля матрицы
            await matrixProfile.InitializeVectorsAsync(
                testSession
                    .Mesh
                    .Elements
                    .SelectMany(element => element.Edges)
                    .DistinctBy(edge => edge.EdgeIndex)
                    .Count()
            );

            _logger.LogInformation($"[{nameof(StationaryFemController)}] calculate global matrix");

            // Строим глобальную матрицу
            await _globalMatrixServices.GetGlobalMatrixAsync(matrixProfile, testSession);

            _logger.LogInformation($"[{nameof(StationaryFemController)}] calculate right part vector");

            // Строим вектор правой части
            await _rightPartVectorService.GetRightPartVectorAsync(matrixProfile, testSession);

            _logger.LogInformation($"[{nameof(StationaryFemController)}] resolve boundary conditions");

            // Получаем краевые условия
            var boundaryConditionService
                = await _boundaryCondition.ResolveBoundaryConditionAsync(testSession.BoundaryCondition);

            _logger.LogInformation($"[{nameof(StationaryFemController)}] set boundary conditions");

            // Задаем краевое условие на расчётной области
            await boundaryConditionService.SetBoundaryConditionsAsync(testSession, matrixProfile);

            _logger.LogInformation($"[{nameof(StationaryFemController)}] save matrix profile to files");

            // Сохраняем данные профиля матрицы в файле
            await _visualizerService.WriteMatrixToFileAsync(matrixProfile);

            Console.WriteLine($"[{nameof(StationaryFemController)}] [Info] Matrix profile was saved from file");
            _logger.LogInformation($"[{nameof(StationaryFemController)}] calculate slae start");

            // Получаем вектора решения
            // Установленная точности = 1e-15
            // Установленное количество итераций = 1000
            var solutionParameters = await _solverService.GetSolutionVectorAsync(matrixProfile, 1000, 1e-15);

            // Рассчитываем точности решения
            await _inaccuracyService.GetSolutionVectorInaccuracy(testSession, solutionParameters);

            _logger.LogInformation($"[{nameof(StationaryFemController)}] saving test result");

            // Сохраняем результат рассчётов в хранилище
            var resultId = await _testResultService.AddTestResultAsync(solutionParameters);

            // Генерируем ответное сообщение
            var femResponse = new FemResponse
            {
                Id = resultId,
                Discrepancy = solutionParameters.SolutionInfo!.Discrepancy,
                IterationsCount = solutionParameters.ItersCount
            };

            return Ok(femResponse);
        } catch (Exception exception)
        {
            return BadRequest(
                $"Something went wrong. Status code: {StatusCodes.Status500InternalServerError}, {exception.Message}"
            );
        } finally
        {
            Console.WriteLine($"[{nameof(StationaryFemController)}] [Info] Started ended");
            _logger.LogInformation($"[{nameof(StationaryFemController)}] [Info] Started ended");
        }
    }

    /// <summary>
    /// Получает результат сессии из хранилища
    /// </summary>
    /// <param name="id">Идентификатор проведенной расчётной сессии</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     Get
    ///     3fa85f64-5717-4562-b3fc-2c963f66afa6
    /// </remarks>
    /// <response code="200">Полную информацию о проведенной сессии</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Полная модель решения уравнения</returns>
    [HttpGet("{id:guid}", Name = "additional-info")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestResult(Guid id)
    {
        try
        {
            // Достаем результат решения из хранилища
            var result = await _testResultService.GetTestResultAsync(id);
            return Ok(result);
        } catch (Exception exception)
        {
            return BadRequest(
                $"Something went wrong. Status code: {StatusCodes.Status500InternalServerError}, {exception.Message}"
            );
        }
    }
}