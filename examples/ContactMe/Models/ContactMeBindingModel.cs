using System.ComponentModel.DataAnnotations;

namespace ContactMe.Models
{
    public class ContactMeBindingModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(1750)]
        public string Body  { get; set; }
    }
}
