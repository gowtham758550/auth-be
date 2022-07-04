using auth.Models;
using System.ComponentModel.DataAnnotations;

namespace auth.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username not in the request body")]
        public string? Username {get; set;}
        [Required(ErrorMessage = "Password not in the request body")]
        public string? Password {get; set;}
        [Required(ErrorMessage = "Email id not in the request body")]
        public string? EmailId {get; set;}
        public Roles Role {get; set;}
    }
}