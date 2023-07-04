namespace Reddit.DTO;

public class ForumDTO
{
    public int Id {get; set;}
    public string Titulo {get; set;}
    public string Descricao {get; set;}
    public int Quantidade {get; set;}
    public bool IsMember { get; set;}
    public DateTime DataCriacao {get; set;}
    public string Jwt {get; set;}
}