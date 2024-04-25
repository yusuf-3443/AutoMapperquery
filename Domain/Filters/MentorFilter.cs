using Domain.Enums;

namespace Domain.Filters;

public class MentorFilter : PaginationFilter
{
    public Status Status { get; set; }
}
