using Microsoft.AspNetCore.Mvc;
using SIQuim.Models;
using Microsoft.EntityFrameworkCore;

namespace SIQuim.Controllers
{
    public class InventarioController : Controller
    {
        private readonly AppDbContext _context;

        public InventarioController(AppDbContext context)
        {
            _context = context;
        }

        // Método Index que carga la vista principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para buscar empleado por número
        [HttpGet]
        public IActionResult BuscarEmpleado(int numeroDeEmpleado)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(e => e.NumeroDeEmpleado == numeroDeEmpleado);

            if (empleado == null)
            {
                return Json(new { success = false, message = "Empleado no encontrado" });
            }

            return Json(new
            {
                success = true,
                nombre = empleado.Nombre,
                numero = empleado.NumeroDeEmpleado,
                area = empleado.Area,
                categoria = empleado.Categoria
            });
        }
    }
}