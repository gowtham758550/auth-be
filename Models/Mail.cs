
namespace auth.Models
{
    public class Mail
    {
        public string? FromAddress {get; set;} = "gowtham758550@gmail.com";
        public string? ToAddress {get; set;}
        public string? Subject {get; set;}
        public bool IsBodyHtml {get; set;} = false;
        public string? Body {get; set;}
    }
}