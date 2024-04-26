namespace Domain.DTOs.ProgressBookDTO;

public class UpdateProgressBookDto
{
    public int Id { get; set; }
    public int Grade { get; set; }
    public bool IsAttended { get; set; }
    public string? Notes { get; set; }
    public int LateMinutes { get; set; }
    public int TimeTableId { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
}
