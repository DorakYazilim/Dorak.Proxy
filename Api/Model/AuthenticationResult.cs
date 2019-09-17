namespace Api.Model
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public long ExpiresIn => 1000 * 60 * 60 * 24 * 10;


        public AuthenticationResult(string token)
        {
            Token = token;
        }
    }
}