namespace wbl.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public long SessionId { get; set; }
        public Session Session { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public ReportType ReportType { get; set; }

    }

    public enum ReportType
    {
        Screening,
        FullAssessment,
        AfterSession
    }
}
