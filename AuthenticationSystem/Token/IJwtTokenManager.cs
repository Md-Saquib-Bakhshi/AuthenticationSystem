namespace AuthenticationSystem.Token
{
    public interface IJwtTokenManager
    {
        public string Authenticate(string email);
    }
}