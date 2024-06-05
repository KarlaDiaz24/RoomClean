using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomClean.Services;

namespace RoomClean.Controllers
{
    [Authorize(Roles = "Empleado")] 
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardEmpleadoController : ControllerBase
    {
        private readonly ITareaService _tareas;

        public DashboardEmpleadoController(ITareaService tareasService)
        {
            _tareas = tareasService;
        }
        [HttpGet("list")]
        public async Task<IActionResult> ObtenerLista()
        {
            var response = await _tareas.ObtenerLista();
            return Ok(response);
        }
    }
}
