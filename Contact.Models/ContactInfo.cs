using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.Models
{
    public class ContactInfo
    {

        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }

    }
}
