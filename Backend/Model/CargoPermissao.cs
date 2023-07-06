using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class CargoPermissao
{
    public int Id { get; set; }

    public int Cargo { get; set; }

    public int Permissao { get; set; }

    public virtual Cargo CargoNavigation { get; set; } = null!;

    public virtual Permissao PermissaoNavigation { get; set; } = null!;
}
