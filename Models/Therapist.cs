using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wbl.Models;

public class Therapist
{
    [Key]
    public int TherapistId { get; set; }

    [Display(Name = "Specialization")]
    public string Specialization { get; set; }

    [Required]
    [Display(Name = "Experience Level")]
    public string ExperienceLevel { get; set; }

    [Display(Name = "Notes or Comments")]
    public string Notes { get; set; }

    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public ICollection<Availibility> Availabilities { get; set; }
}
