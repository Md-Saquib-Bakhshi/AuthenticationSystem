using AuthenticationSystem.Data;
using AuthenticationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationSystem.Repository
{
    public class RegistrationService : IRegistrationService
    {
        RegistrationDbContext _registationDbContext;

        public RegistrationService(RegistrationDbContext registationDbContext)
        {
            _registationDbContext = registationDbContext;
        }

        //Create User
        public async Task<bool> AddUser(Registration user)
        {
            if(user != null)
            {
                await _registationDbContext.RegistrationSet.AddAsync(user);
                await _registationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Read User
        public async Task<IEnumerable<Registration>> GetAllUser()
        {
            var allUser = await _registationDbContext.RegistrationSet.ToListAsync();
            return allUser;
        }

        public async Task<Registration> GetUser(Guid id)
        {
            var specificUser = await _registationDbContext.RegistrationSet.FirstOrDefaultAsync(x => x.Id == id);
            if(specificUser != null)
            {
                return specificUser;
            }
            return null;
        }

        //Update User
        public async Task<bool> UpdateUser(Guid id, Registration user)
        {
            var existedUser = await _registationDbContext.RegistrationSet.FirstOrDefaultAsync(x => x.Id==id);
            if(existedUser != null)
            {
                existedUser.FirstName = user.FirstName;
                existedUser.LastName = user.LastName;
                existedUser.Phone = user.Phone; 
                existedUser.Email = user.Email;
                existedUser.Password = user.Password;
                await _registationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Delete User
        public async Task<bool> DeleteUser(Guid id)
        {
            var existedUser = await _registationDbContext.RegistrationSet.FirstOrDefaultAsync(x => x.Id ==id);
            if(existedUser != null)
            {
                _registationDbContext.RegistrationSet.Remove(existedUser);
                await _registationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
