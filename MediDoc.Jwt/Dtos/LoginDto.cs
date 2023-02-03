using System.ComponentModel.DataAnnotations;

namespace MediDoc.Jwt.Dtos
{
    public class LoginDto
    {
        [Required, MinLength(3)]
        public string Username { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
