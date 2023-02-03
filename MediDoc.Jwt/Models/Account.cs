using System.ComponentModel.DataAnnotations;

namespace MediDoc.Jwt.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExp { get; set; }
    }
}
