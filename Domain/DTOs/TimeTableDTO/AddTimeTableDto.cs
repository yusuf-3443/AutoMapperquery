using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.TimeTableDTO;

public class AddTimeTableDto
{
    public DayOfWeek DayOfWeek { get; set; }
    [Required(ErrorMessage = "Please enter the duration of the service")]
    [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$", ErrorMessage = "Use format HH:MM only")]
    public string FromTime { get; set; }
    [Required(ErrorMessage = "Please enter the duration of the service")]
    [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$", ErrorMessage = "Use format HH:MM only")]
    public string ToTime { get; set; }
    public DateTimeOffset? CreatedDate { get; set; }
    public int GroupId { get; set; }
}

