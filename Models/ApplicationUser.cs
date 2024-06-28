using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wbl.Models;

namespace wbl.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; }

        [StringLength(11, ErrorMessage = "Phone number cannot be longer than 11 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        // Navigation property for Therapist
        public Therapist Therapist
        {
            get; set;
        }

        public CustomerService CustomerService
        {
            get; set;
        }
        // Navigation properties
        public Parent1 Parent1 { get; set; }
        public Parent2 Parent2 { get; set; }
    }
}

