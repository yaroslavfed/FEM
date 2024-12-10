using FEM.SharedCore.Services.BoundaryConditionService;
using FEM.SharedCore.Services.InaccuracyService;
using FEM.SharedCore.Services.MatrixPortraitService;
using FEM.SharedCore.Services.SolverService;
using Microsoft.AspNetCore.Mvc;
using Stationary.Core.Services.GlobalMatrixService;
using Stationary.Core.Services.RightPartVectorService;
using Stationary.Core.Services.TestResultService;
using Stationary.Core.Services.VisualizerService;
using Stationary.DTO.Configurations;
using Stationary.DTO.OutputModels;
using IStationaryTestSessionService
    = FEM.SharedCore.Services.TestSessionService.ITestSessionService<Stationary.DTO.TestingContext.StationaryTestSession
        , Stationary.DTO.Configurations.StationaryTestConfiguration>;

namespace FEM.Server.Controllers;

/// <summary>
/// Контроллер для решения стационарного уравнения векторного МКЭ
/// </summary>
[ApiController]
[Route("api/fem/stationary")]
public class StationaryFemController : ControllerBase
{
    private readonly ILogger                           _logger;
    private readonly IStationaryGlobalMatrixServices   _globalMatrixServices;
    private readonly IStationaryTestSessionService     _testSessionService;
    private readonly IMatrixPortraitService            _portraitService;
    private readonly IStationaryRightPartVectorService _rightPartVectorService;
    private readonly IStationaryVisualizerService      _visualizerService;
    private readonly IBoundaryConditionFactory         _boundaryCondition;
    private readonly ISolverService                    _solverService;
    private readonly IStationaryTestResultService      _testResultService;
    private readonly IInaccuracyService                _inaccuracyService;

    /// <inheritdoc />
    public StationaryFemController(
        ILogger<StationaryFemController> logger,
        IStationaryGlobalMatrixServices globalMatrixServices,
        IStationaryTestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IStationaryRightPartVectorService rightPartVectorService,
        IStationaryVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition,
        ISolverService solverService,
        IStationaryTestResultService testResultService,
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
    /// <param name="testConfiguration"><see cref="StationaryTestConfiguration">Входные параметры расчётной сессии</see></param>
    /// <response code="200">Возвращает id результата, невязку и количество итераций</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Решение стационарного уравнения</returns>
    [HttpPost]
    [ProducesResponseType(typeof(StationaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            var femResponse = new StationaryResponse
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
    /// <response code="200">Полную информацию о проведенной сессии</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Полная модель решения уравнения</returns>
    [HttpGet("{id:guid}")]
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