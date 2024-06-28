using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace wbl.Models
{
    public class TreatmentHistory
    {
        [Key]
        public int historyId { get; set; }

        [Required]
        public int ChildId { get; set; }

        [ForeignKey("ChildId")]
        public Child Child { get; set; }

        public string? pediatricians { get; set; }

        [Required]
        public string? recHospClinic { get; set; }

        [Required]
        public string? diagnoseDesc { get; set; }


        [Required]
        public string? deadlineDignose { get; set; }
    }
}
