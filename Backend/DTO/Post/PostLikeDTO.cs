namespace Reddit.DTO;

public class PostLikeInteractionDTO
{
    public int IdPost { get; set; }
    public string Jwt { get; set; }
    public int IdUser {get; set;}
    public Boolean HasLike {get; set;}
    public int Quantity { get; set; }
}