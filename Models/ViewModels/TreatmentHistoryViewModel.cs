using System.ComponentModel.DataAnnotations;

namespace wbl.Models.ViewModels
{
    public class TreatmentHistoryViewModel
    {
        public int ChildId { get; set; }

        public int historyId { get; set; }
        [Required]
        public string? Pediatricians { get; set; }

        [Required]
        public string? RecHospClinic { get; set; }

        [Required]
        public string? DiagnoseDesc { get; set; }

        [Required]
        public string? DeadlineDiagnose { get; set; }
    }
}
