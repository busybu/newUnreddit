using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class Forum
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataCriado { get; set; }

    public int? Criador { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual Usuario? CriadorNavigation { get; set; }

    public virtual ICollection<ForumUsuario> ForumUsuarios { get; set; } = new List<ForumUsuario>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
