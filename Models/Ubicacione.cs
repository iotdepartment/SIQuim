using System;
using System.Collections.Generic;

namespace SIQuim.Models;

public partial class Ubicacione
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Tipo { get; set; }

    public bool Activa { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
