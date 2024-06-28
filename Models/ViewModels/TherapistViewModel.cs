// TherapistViewModel.cs
namespace wbl.Models.ViewModels
{
public class TherapistViewModel
{
    public int TherapistId { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PhoneNumber { get; set; }
    public string Specialization { get; set; }
    public string Availability { get; set; }
    public string ExperienceLevel { get; set; }
    public string Notes { get; set; }
}

}
