public class LoginDTO
{
    public bool UserExist {get; set;} = false;
    public bool Sucess {get; set;} = false;
    public string Jwt {get; set;} = null;
}