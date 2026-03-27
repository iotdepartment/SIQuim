using System;
using System.Collections.Generic;

namespace SIQuim.Models;

public partial class Registro
{
    public int Id { get; set; }

    public DateTime FechaHora { get; set; }

    public int ResponsableEntrega { get; set; }

    public int ResponsableReciba { get; set; }

    public int MaterialId { get; set; }

    public int Qty { get; set; }

    public virtual Materiale Material { get; set; } = null!;

    public virtual Empleado ResponsableEntregaNavigation { get; set; } = null!;

    public virtual Empleado ResponsableRecibaNavigation { get; set; } = null!;
}
