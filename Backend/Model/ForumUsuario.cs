using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class ForumUsuario
{
    public int Id { get; set; }

    public int Forum { get; set; }

    public int Usuarios { get; set; }

    public virtual Forum ForumNavigation { get; set; } = null!;

    public virtual Usuario UsuariosNavigation { get; set; } = null!;
}
