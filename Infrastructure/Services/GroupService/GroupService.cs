using System.Net;
using AutoMapper;
using Domain.DTOs.GroupDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class GroupService(IMapper mapper, DataContext context) : IGroupService
{
    
 public async Task<PagedResponse<List<GetGroupDto>>> GetGroups(GroupFilter filter)
 {
    try
    {
        var groups = context.Groups.AsQueryable();

        if(!string.IsNullOrEmpty(filter.GroupName))
        groups = groups.Where(x=>x.GroupName.ToLower().Contains(filter.GroupName.ToLower()));

        var response = await groups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
        var totalRecord = groups.Count();

        var mapped = mapper.Map<List<GetGroupDto>>(response);
        return new PagedResponse<List<GetGroupDto>>(mapped,filter.PageNumber,filter.PageSize,totalRecord);
    }
    catch (System.Exception)
    {
        
        throw;
    }
 }
    public async Task<Response<GetGroupDto>> GetGroupById(int id)
    {
        try
        {
        var group = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (group == null) return new Response<GetGroupDto>(HttpStatusCode.BadRequest, "Not found");
            var mapped = mapper.Map<GetGroupDto>(group);
            return new Response<GetGroupDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetGroupDto>(HttpStatusCode.InternalServerError,e.Message);
        }
        }
    public async Task<Response<string>> AddGroup(AddGroupDto group)
    {
        try
        {
        var mapped = mapper.Map<Group>(group);
        await context.Groups.AddAsync(mapped);
        var save = await context.SaveChangesAsync();
        if(save > 0) return new Response<string>("Successfully");
        return new Response<string>(HttpStatusCode.BadRequest,"Failed");   
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
    public async Task<Response<string>> UpdateGroup(UpdateGroupDto group)
    {
        try
        {
            var mapped = mapper.Map<Group>(group);
            context.Groups.Update(mapped);
            var save = await context.SaveChangesAsync();
            if(save>0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
    public async Task<Response<bool>> DeleteGroup(int id)
    {
        try
        {
            var group = await context.Groups.Where(x=>x.Id==id).ExecuteDeleteAsync();
            if(group>0) return new Response<bool>(true);
            return new Response<bool>(HttpStatusCode.BadRequest,"Not found");

        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<List<GetGroupStudent>> GetGroupStudentsCount()
    {
        var sql = await (from g in context.Groups
        join sg in context.StudentGroups on g.Id equals sg.GroupId
        join s in context.Students on sg.StudentId equals s.Id
        let count = context.Students.Count(x=>x.Id==sg.StudentId)
        select new GetGroupStudent
        {
            Group = g,
            StudentCount = count
        }).ToListAsync();
        return new List<GetGroupStudent>(sql);
    }
}