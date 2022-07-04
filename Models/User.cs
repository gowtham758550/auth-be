using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        User = 0
    }

    public class User
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        [Required]
        public string? Username {get; set;}
        [Required]
        public string? EmailId {get; set;}
        [Required]
        public byte[]? PasswordSalt {get; set;} 
        public byte[]? PasswordHash {get; set;} 
        public DateTime DOB {get; set;}
        public Roles Role {get; set;}
        public DateTime CreataedAt = DateTime.Now;
        public DateTime ModifiedAt = DateTime.Now;
        public string? RefreshToken {get; set;}
        public DateTime TokenCreated {get; set;}
        public DateTime TokenExpires {get; set;}
        public bool IsVerified {get; set;} = false;
        public ICollection<Otp>? Otps {get; set;}
    }
}