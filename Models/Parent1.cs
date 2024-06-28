using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbl.Models
{
    public class Parent1
    {
        [Required]
        [Display(Name = "Name")]
        public string FatherName { get; set; }

        [Display(Name = "Phone Number")]
        public string FatherPhone { get; set; }

        [Key]
        [Display(Name = "IC Number")]
        public string FatherIc { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string FatherAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Race")]
        public string FatherRace { get; set; }

        [StringLength(50)]
        [Display(Name = "Occupation")]
        public string FatherOccupation { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string FatherEmail { get; set; }

        [Display(Name = "Work Address")]
        public string FatherWorkAddress { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Child> Children { get; set; }
    }
}
