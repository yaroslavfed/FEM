using Microsoft.AspNetCore.Mvc;

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


}