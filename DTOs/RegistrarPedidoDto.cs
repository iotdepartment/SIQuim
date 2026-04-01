namespace SIQuim.DTOs
{

    public class RegistrarPedidoDto
    {
        public int ResponsableEntrega { get; set; }
        public int ResponsableReciba { get; set; }
        public List<MaterialPedidoDto> Materiales { get; set; } = new();
    }

    public class MaterialPedidoDto
    {
        public int MaterialId { get; set; }
        public int Qty { get; set; }
    }

}
