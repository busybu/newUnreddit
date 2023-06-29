namespace Reddit.DTO;

public class Jwt
{
    public int UserID { get; set; }
    public bool Authenticated {get; set;} = false;
}