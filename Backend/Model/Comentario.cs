using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class Comentario
{
    public int Id { get; set; }

    public string Conteudo { get; set; } = null!;

    public int Usuario { get; set; }

    public int Post { get; set; }

    public virtual Post PostNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
