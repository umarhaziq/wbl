using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbl.Models
{
    public class Child
    {
        [Key]
        [Required]
        public int ChildId { get; set; }

        [Display(Name = "IC Number")]
        [Required(ErrorMessage = "IC Number is Required")]
        public string Child_IC { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Child_Name { get; set; }

        [Required]
        [Display(Name = "Birth Place")]
        public string Child_Birthplace { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime Child_DOB { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Gender")]
        public string Child_Gender { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nationality")]
        public string Child_Nationality { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Race")]
        public string Child_Race { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Religion")]
        public string Child_Religion { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public Programs Programs { get; set; }

        [Display(Name = "Address")]
        public string Child_Address { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Application Status")]
        public string? ApplicationStatus { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Payment Status")]
        public string? PaymentStatus { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string? Program_Category { get; set; }

        [ForeignKey("Parent1")]
        public string FatherIc { get; set; }

        public Parent1 Parent { get; set; }

        public int? Id { get; set; }
        [ForeignKey("Id")]

        public Invoices Invoice { get; set; }

        public ICollection<TreatmentHistory> TreatmentHistories { get; set; }
    }
}
