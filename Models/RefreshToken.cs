
using System.ComponentModel.DataAnnotations;

namespace auth.Models
{
    public class RefreshToken
    {
        [Required]
        public string? Token {get; set;}
        public DateTime Created {get; set;} = DateTime.Now;
        public DateTime Expires {get; set;} = DateTime.Now.AddDays(7);
    }
}