using System.Net;
using AutoMapper;
using Domain.DTOs.GroupDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class GroupService(IMapper mapper, DataContext context) : IGroupService
{
 public async Task<Response<List<GetGroupDto>>> GetGroups()
 {
    try
    {
        var groups = await context.Groups.ToListAsync();
        var mapped = mapper.Map<List<GetGroupDto>>(groups);
        return new Response<List<GetGroupDto>>(mapped);
    }
    catch (Exception e)
    {
        return new Response<List<GetGroupDto>>(HttpStatusCode.InternalServerError,e.Message);
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
}