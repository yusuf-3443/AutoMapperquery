using Domain.DTOs.MentorGroupDTO;
using Domain.Responses;
using Infrastructure.Services.MentorService;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MentorGroupController(IMentorGroupService mentorGroupService) : ControllerBase
    {
       

        [HttpGet("get-mentor-groups")]
        public async Task<Response<List<GetMentorGroupDto>>> GetMentorGroupsAsync()
        {
            return await mentorGroupService.GetMentorGroupsAsync();
        }

        [HttpGet("{mentorGroupId:int}")]
        public async Task<Response<GetMentorGroupDto>> GetMentorGroupByIdAsync(int mentorGroupId)
        {
            return await mentorGroupService.GetMentorGroupByIdAsync(mentorGroupId);
        }

        [HttpPost("create-mentor-group")]
        public async Task<Response<string>> AddMentorGroupAsync(AddMentorGroupDto mentorGroupDto)
        {
            return await mentorGroupService.CreateMentorGroupAsync(mentorGroupDto);
        }

        [HttpPut("update-mentor-group")]
        public async Task<Response<string>> UpdateMentorGroupAsync(UpdateMentorGroupDto mentorGroupDto)
        {
            return await mentorGroupService.UpdateMentorGroupAsync(mentorGroupDto);
        }

        [HttpDelete("{mentorGroupId:int}")]
        public async Task<Response<bool>> DeleteMentorGroupAsync(int mentorGroupId)
        {
            return await mentorGroupService.DeleteMentorGroupAsync(mentorGroupId);
        }
    }
}
