using System.ComponentModel.DataAnnotations;

namespace auth.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string? UserName {get; set;}
        [Required]
        public string? OldPassword {get; set;}
        [Required]
        public string? NewPassword {get; set;}
    }
}