using System;
using System.Collections.Generic;

namespace SIQuim.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public int NumeroDeEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public virtual ICollection<Registro> RegistroResponsableEntregaNavigations { get; set; } = new List<Registro>();

    public virtual ICollection<Registro> RegistroResponsableRecibaNavigations { get; set; } = new List<Registro>();
}
