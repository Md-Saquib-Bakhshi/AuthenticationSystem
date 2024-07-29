using AuthenticationSystem.Models;

namespace AuthenticationSystem.Repository
{
    public interface IRegistrationService
    {
        //Create User
        public Task<bool> AddUser(Registration user);
        //Read User
        public Task<IEnumerable<Registration>> GetAllUser();
        public Task<Registration> GetUser(Guid id);
        //Update User
        public Task<bool> UpdateUser(Guid id, Registration user);
        //Delete User
        public Task<bool> DeleteUser(Guid id);
    }
}
