using AuthenticationSystem.Data;

namespace AuthenticationSystem.Repository
{
    public interface ILoginService
    {
        public Task<bool> IsExistingUser(string email, string password);
    }
}
