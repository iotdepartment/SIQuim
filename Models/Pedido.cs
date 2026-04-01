namespace SIQuim.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }

        // Relación con empleados
        public int ResponsableEntregaId { get; set; }
        public Empleado ResponsableEntrega { get; set; }

        public int ResponsableRecibeId { get; set; }
        public Empleado ResponsableRecibe { get; set; }

        // Relación con registros
        public virtual ICollection<Registro> Registros { get; set; } = new List<Registro>();
    }
}
