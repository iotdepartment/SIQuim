using System;
using System.Collections.Generic;

namespace SIQuim.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int MaterialId { get; set; }

    public int UbicacionId { get; set; }

    public double Cantidad { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual Materiale Material { get; set; } = null!;

    public virtual Ubicacione Ubicacion { get; set; } = null!;
}
