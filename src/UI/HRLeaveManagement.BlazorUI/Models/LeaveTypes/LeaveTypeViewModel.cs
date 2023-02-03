using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.BlazorUI.Models.LeaveTypes;

public class LeaveTypeViewModel
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Default Number Of Days")]
    public int DefaultDays { get; set; }
}