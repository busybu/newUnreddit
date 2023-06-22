using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class Permissao
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<CargoPermissao> CargoPermissaos { get; set; } = new List<CargoPermissao>();
}
