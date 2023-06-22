using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class UpVote
{
    public int Id { get; set; }

    public int? Post { get; set; }

    public int? Usuario { get; set; }

    public virtual Post? PostNavigation { get; set; }

    public virtual Usuario? UsuarioNavigation { get; set; }
}
