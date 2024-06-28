using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wbl.Models
{
    public class Programs
    {
        [Key]
        public int ProgramId { get; set; }

        [Required]
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string? Program_Category { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
