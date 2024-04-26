namespace Domain.Entities;

public class ProgressBook : BaseEntity
{
    public int Grade { get; set; }
    public int TimeTableId { get; set; }
    public virtual TimeTable? TimeTable { get; set; }
    public int StudentId { get; set; }
    public virtual Student? Student { get; set; }
    public bool IsAttended { get; set; }
    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }
    public string? Notes { get; set; }
    public int LateMinutes { get; set; }

}
