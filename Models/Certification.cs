using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbl.Models
{
    public class Certification
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Certification Name")]
        public string CertificationName { get; set; }

        [Display(Name = "Issue Date")]
        public DateTime? IssueDate { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Issuing Authority")]
        public string IssuingAuthority { get; set; }

        [ForeignKey("Therapist")]
        public int TherapistId { get; set; }

        public Therapist Therapist { get; set; }
    }
}
