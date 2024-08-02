using AuthenticationSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthenticationSystem.Repository
{
    public class LoginService : ILoginService
    {
        private readonly RegistrationDbContext _registrationDbContext;

        public LoginService(RegistrationDbContext registrationDbContext)
        {
            _registrationDbContext = registrationDbContext;
        }

        public async Task<bool> IsExistingUser(string email, string password)
        {
            var user = await _registrationDbContext.RegistrationSet
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                return user.Password == password; 
            }

            return false;
        }
    }
}
