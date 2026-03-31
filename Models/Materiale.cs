using System;
using System.Collections.Generic;

namespace SIQuim.Models;

public partial class Materiale
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public string Description { get; set; } = null!;

    public string? Kanban { get; set; }

    public string? PartNumber { get; set; }

    public string? Vendor { get; set; }

    public string? Uom { get; set; }

    public int? StdPack { get; set; }

    public double? Cost { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<Registro> Registros { get; set; } = new List<Registro>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
