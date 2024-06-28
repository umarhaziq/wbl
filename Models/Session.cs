using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbl.Models
{
    public class Session
    {
        [Key]
        public long SessionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int TherapistId { get; set; }
        public Therapist Therapist { get; set; }

        public int ChildId { get; set; }
        public Child Child { get; set; }

        [ForeignKey("CustomerService")]
        public int CustomerServiceId { get; set; }
        public CustomerService CustomerService { get; set; }

        public int SessionTimeId { get; set; }
        public SessionTime SessionTime { get; set; }
    }
}