using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace wbl.Models
{
    public class Availibility
    {
        [Key]
        public int AvailabilityId { get; set; }
        public int TherapistId { get; set; }
        public Therapist Therapist { get; set; }
        public DateTime AvailableDate { get; set; }
    }
}
