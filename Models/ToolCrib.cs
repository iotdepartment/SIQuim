using System;
using System.Collections.Generic;

namespace SIQuim.Models;

public partial class ToolCrib
{
    public int Id { get; set; }

    public string? Requisitor { get; set; }

    public string? Contenedor { get; set; }

    public string? Material { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Contenido { get; set; }

    public TimeOnly? Hora { get; set; }

    public string? Unidad { get; set; }

    public string? Categoria { get; set; }

    public string? Area { get; set; }

    public double? Costo { get; set; }
}
