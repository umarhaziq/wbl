using System.ComponentModel.DataAnnotations;

namespace wbl.Models
{
    public class SessionTime
    {
        [Key]
        public int Sessiontimeid { get; set; }
        public TimeSpan start { get; set; }
        public TimeSpan end { get; set; }
    }
}
