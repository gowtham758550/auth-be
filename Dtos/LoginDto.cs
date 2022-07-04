using System.ComponentModel.DataAnnotations;

namespace auth.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username not in the request body")]
        public string? Username {get; set;}
        [Required(ErrorMessage = "Password not in the request body")]
        public string? Password {get; set;}
    }
}