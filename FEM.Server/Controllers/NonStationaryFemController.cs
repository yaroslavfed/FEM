using Microsoft.AspNetCore.Mvc;
using NonStationary.DTO.Configurations;
using NonStationary.DTO.OutputModels;

namespace FEM.Server.Controllers;

/// <summary>
/// Контроллер для решения нестационарных уравнений
/// </summary>
[ApiController]
[Route("api/fem/non-stationary")]
public class NonStationaryFemController : ControllerBase
{
    public NonStationaryFemController()
    {
    }

    /// <summary>
    /// Решает нестационарное уравнение с помощью векторного МКЭ
    /// </summary>
    /// <param name="testConfiguration"><see cref="NonStationaryTestConfiguration">Конфигурация нестационарной задачи</see></param>
    /// <response code="200">Возвращает id результата, невязку и количество итераций</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Решение нестационарного уравнения</returns>
    [HttpPost]
    [ProducesResponseType(typeof(NonStationaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCalculation([FromBody] NonStationaryTestConfiguration testConfiguration)
    {
        return Ok();
    }
}