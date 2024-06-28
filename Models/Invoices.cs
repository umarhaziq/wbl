using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbl.Models
{
    public class Invoices
    {
        [Key]
        public int Id { get; set; }

        public string InvoiceName { get; set; }



        public DateTime Date { get; set; }




        public int ChildId { get; set; }

        [ForeignKey("ChildId")]
        [ValidateNever]

        public Child Child { get; set; }



        public byte[]? Attachment { get; set; }

        public string? AttachmentContentType { get; set; } // Add this property




    }
}
