using AuthenticationSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationSystem.Repository
{
    public class LoginService : ILoginService
    {
        RegistrationDbContext _registrationDbContext;

        public LoginService(RegistrationDbContext registrationDbContext)
        {
            _registrationDbContext = registrationDbContext;
        }

        public async Task<bool> IsExistingUser(string email, string password)
        {
            var checkingUser = await _registrationDbContext.RegistrationSet.FirstOrDefaultAsync( x => x.Email == email);
            if (checkingUser != null)
            {
                if(checkingUser.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
