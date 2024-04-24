using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class QueryService(DataContext context)
{
    public List<GroupStudent> GetStudentGroup()
    {
        var sql = (from g in context.Groups
        join sg in context.StudentGroups on g.Id equals sg.GroupId
        join s in context.Students on sg.StudentId equals s.Id
        let count = context.Students.Count(x=>x.Id==sg.StudentId)
        select new GroupStudent
        {
            Group = g,
            StudentCount = count
        }).ToList();
        return sql;
    }
}
