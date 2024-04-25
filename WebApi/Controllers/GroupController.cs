using Domain.DTOs.CourseDTO;
using Domain.DTOs.GroupDTO;
using Domain.Filters;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]

public class GroupController(IGroupService groupService):ControllerBase
{
     [HttpGet("get-group-students-count")]
    public async Task<List<GetGroupStudent>> GetGroupStudentsCount()
    {
        return await groupService.GetGroupStudentsCount();
    }
    [HttpGet("get-groups")]
    public async Task<Response<List<GetGroupDto>>> GetGroupsAsync([FromQuery]GroupFilter filter)
    {
        return await groupService.GetGroups(filter);
    }

    [HttpGet("{groupId:int}")]
    public async Task<Response<GetGroupDto>> GetGroupByIdAsync(int groupId)
    {
        return await groupService.GetGroupById(groupId);
    }

    [HttpPost("add-group")]
    public async Task<Response<string>> AddGroupAsync(AddGroupDto groupDto)
    {
        return await groupService.AddGroup(groupDto);
    }

    [HttpPut("update-group")]
    public async Task<Response<string>> UpdateGroupAsync(UpdateGroupDto groupDto)
    {
        return await groupService.UpdateGroup(groupDto);
    }

    [HttpDelete("{groupId:int}")]
    public async Task<Response<bool>> DeleteGroupAsync(int groupId)
    {
        return await groupService.DeleteGroup(groupId);
    }

}