using System;
using System.Collections.Generic;

namespace Reddit.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public DateTime? DataNascimento { get; set; }

    public byte[]? FotoUsuario { get; set; }

    public byte[]? Senha { get; set; }

    public string? Salt { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<ForumUsuario> ForumUsuarios { get; set; } = new List<ForumUsuario>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UpVote> UpVotes { get; set; } = new List<UpVote>();
}
