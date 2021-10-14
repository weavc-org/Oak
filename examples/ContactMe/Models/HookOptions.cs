using System.Collections.Generic;

namespace Oak.ContactMe.Models
{
    public class HookOptions 
    {
        public List<string> Discord { get; set; } = new List<string>();
        public List<string> Emails { get; set; } = new List<string>();

    }
}