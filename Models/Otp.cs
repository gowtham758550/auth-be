using System.ComponentModel.DataAnnotations;

namespace auth.Models
{
    public class Otp
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        public int Value {get; set;}
        public DateTime ExpiresAt {get; set;} = DateTime.Now.AddMinutes(1);
        public virtual User? User {get; set;}
    }
}