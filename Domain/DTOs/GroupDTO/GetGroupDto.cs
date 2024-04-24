using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.DTOs.GroupDTO;

public class GetGroupDto
{
    public int Id { get; set; }
    public string GroupName { get; set; } = null!;
    public string? Description { get; set; }
    public Status Status { get; set; }
    [ForeignKey("CourseId")]
    public int CourseId { get; set; }
}