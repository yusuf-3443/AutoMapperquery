namespace Domain.Entities;

public class TimeTable : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? FromTime { get; set; }
    public TimeSpan? ToTime { get; set; }
    public DateTimeOffset CreatedDate { get; set; }

    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }
    public List<ProgressBook>? ProgressBooks { get; set; }
}
