using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using wbl.Models;

public class ParentViewModel
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

}


