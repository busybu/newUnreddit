namespace Reddit.DTO;

public class PostDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }
    public string Anexo { get; set; }
    public DateTime DataCriacao { get; set; }
    public string Jwt { get; set; }
    public int ForumID { get; set; }
    public string NomeForum { get; set; }
    public int IdAutor {get; set; }
    public string NomeAutor { get; set; }
    public int Like {get; set;}

}