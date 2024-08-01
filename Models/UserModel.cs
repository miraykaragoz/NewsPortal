using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? RedirectUrl { get; set; }
    }
}