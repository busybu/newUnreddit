using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class Cargo
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public int Forum { get; set; }

    public virtual ICollection<CargoPermissao> CargoPermissaos { get; set; } = new List<CargoPermissao>();

    public virtual Forum ForumNavigation { get; set; } = null!;
}
