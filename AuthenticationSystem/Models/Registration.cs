using System.ComponentModel.DataAnnotations;

namespace AuthenticationSystem.Models
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
