using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbl.Models
{
    public class Parent2
    {

        [Required]
        [Display(Name = "Name")]
        public string MotherName { get; set; }

        [Display(Name = "Phone Number")]
        public string MotherPhone { get; set; }

        [Key]
        [Display(Name = "IC Number")]
        public string MotherIc { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string MotherAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Race")]
        public string MotherRace { get; set; }

        [StringLength(50)]
        [Display(Name = "Occupation")]
        public string MotherOccupation { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string MotherEmail { get; set; }

        [Display(Name = "Work Address")]
        public string? MotherWorkAddress { get; set; }

        [ForeignKey("Parent1")]
        public string FatherIc { get; set; }
    }
}
