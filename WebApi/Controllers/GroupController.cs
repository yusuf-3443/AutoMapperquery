using Domain.DTOs.CourseDTO;
using Domain.DTOs.GroupDTO;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]

public class GroupController(IGroupService groupService):ControllerBase
{
    [HttpGet("get-groups")]
    public async Task<Response<List<GetGroupDto>>> GetGroupsAsync()
    {
        return await groupService.GetGroups();
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