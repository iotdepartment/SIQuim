using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIQuim.DTOs;
using SIQuim.Models;

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
                    id = material.Id,
                    description = material.Description,
                    kanban = material.Kanban.Trim().ToUpper(),
                    partNumber = material.PartNumber,
                    uom = material.Uom,
                    stdPack = material.StdPack,
                    imageUrl = material.ImageUrl
                }
            });
        }
        [HttpPost]
        public IActionResult RegistrarPedido([FromBody] RegistrarPedidoDto dto)
        {
            var entrega = _context.Empleados.FirstOrDefault(e => e.NumeroDeEmpleado == dto.ResponsableEntrega);
            var recibe = _context.Empleados.FirstOrDefault(e => e.NumeroDeEmpleado == dto.ResponsableReciba);

            if (entrega == null || recibe == null)
                return Json(new { success = false, error = "Empleado no encontrado" });

            var pedido = new Pedido
            {
                FechaHora = DateTime.Now,
                ResponsableEntregaId = entrega.Id,
                ResponsableRecibeId = recibe.Id
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach (var item in dto.Materiales)
            {
                var registro = new Registro
                {
                    FechaHora = DateTime.Now,
                    ResponsableEntrega = entrega.Id,
                    ResponsableReciba = recibe.Id,
                    MaterialId = item.MaterialId,
                    Qty = item.Qty,
                    PedidoId = pedido.Id
                };

                _context.Registros.Add(registro);
            }

            _context.SaveChanges();

            return Json(new { success = true, pedidoId = pedido.Id });
        }

        // Vista Razor
        public IActionResult Pedidos()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHistorialMaterial(int idMaterial)
        {
            var historial = await _context.Pedidos
                .Include(p => p.ResponsableEntrega)
                .Include(p => p.ResponsableRecibe)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Material)
                .Where(p => p.Registros.Any(r => r.MaterialId == idMaterial))
                .OrderByDescending(p => p.FechaHora)
                .Select(p => new
                {
                    pedidoId = p.Id,
                    fecha = p.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                    entrega = p.ResponsableEntrega.Nombre,
                    recibe = p.ResponsableRecibe.Nombre,
                    cantidad = p.Registros
                                .Where(r => r.MaterialId == idMaterial)
                                .Select(r => r.Qty)
                                .FirstOrDefault()
                })
                .ToListAsync();

            return Json(new { data = historial });
        }

        // Endpoint JSON para DataTables
        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.ResponsableEntrega)
                .Include(p => p.ResponsableRecibe)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Material)
                .OrderByDescending(p => p.FechaHora)
                .ToListAsync();

            var result = pedidos.SelectMany(p => p.Registros.Select(r => new
            {
                id = p.Id,
                fechaHora = p.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                entrega = p.ResponsableEntrega?.Nombre,
                recibe = p.ResponsableRecibe?.Nombre,

                // Material individual
                nombre = r.Material.Description,
                kanban = r.Material.Kanban,
                cantidad = r.Qty,
                imagen = string.IsNullOrEmpty(r.Material.ImageUrl)
                            ? "/images/materiales/default.png"
                            : r.Material.ImageUrl
            })).ToList();

            return Json(new { data = result });
        }

    }
}