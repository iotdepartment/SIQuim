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


        [HttpGet]
        public IActionResult BuscarEmpleado(string numeroDeEmpleado)
        {
            try
            {
                if (!int.TryParse(numeroDeEmpleado, out int num))
                    return Json(new { success = false, error = "Número inválido" });

                var empleado = _context.Empleados
                    .FirstOrDefault(e => e.NumeroDeEmpleado == num);

                if (empleado == null)
                    return Json(new { success = false, error = "Empleado no encontrado" });

                return Json(new
                {
                    success = true,
                    numero = empleado.NumeroDeEmpleado,
                    nombre = empleado.Nombre,
                    area = empleado.Area,
                    categoria = empleado.Categoria
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // Vista principal
        public IActionResult Materiales()
        {
            return View();
        }

        // Endpoint que devuelve los registros en JSON
        [HttpGet]
        public IActionResult GetMateriales()
        {
            var materiales = _context.Materiales.ToList();
            return Json(new { data = materiales }); 
        }


        [HttpGet]
        public IActionResult GetMaterialByKanban(string kanban)
        {
            if (string.IsNullOrWhiteSpace(kanban))
                return Json(new { success = false, error = "Kanban inválido" });

            var material = _context.Materiales
                .FirstOrDefault(m => m.Kanban.Trim().ToUpper() == kanban.Trim().ToUpper());

            if (material == null)
                return Json(new { success = false, error = "Material no encontrado" });

            return Json(new
            {
                success = true,
                material = new
                {
                    description = material.Description,
                    kanban = material.Kanban.Trim().ToUpper(),
                    partNumber = material.PartNumber,
                    uom = material.Uom,
                    stdPack = material.StdPack,
                    imageUrl = material.ImageUrl // si tienes este campo en la tabla
                }
            });
        }

    }
}