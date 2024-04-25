using Domain.Entities;

namespace Domain.DTOs.MentorDTO;

public class GetMentorMoreThenOneGroup
{
    public Mentor? Mentor { get; set; }
    public int Students { get; set; }
}
