﻿using System.ComponentModel.DataAnnotations;

namespace ContactMe.Models
{
    public class ContactMeBindingModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [StringLength(1750)]
        public string Body  { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
