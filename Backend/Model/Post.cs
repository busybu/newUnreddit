using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class Post
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Conteudo { get; set; }

    public byte[]? Anexo { get; set; }

    public int? Autor { get; set; }

    public int? Forum { get; set; }

    public virtual Usuario? AutorNavigation { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Forum? ForumNavigation { get; set; }

    public virtual ICollection<UpVote> UpVotes { get; set; } = new List<UpVote>();
}
