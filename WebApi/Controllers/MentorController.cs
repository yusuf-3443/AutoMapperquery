using Domain.DTOs.MentorDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.MentorService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService mentorService;

        public MentorController(IMentorService mentorService)
        {
            this.mentorService = mentorService;
        }

        [HttpGet("get-mentors")]
        public async Task<Response<List<GetMentorDto>>> GetMentorsAsync(MentorFilter filter)
        {
            return await mentorService.GetMentorsAsync(filter);
        }

        [HttpGet("{mentorId:int}")]
        public async Task<Response<GetMentorDto>> GetMentorByIdAsync(int mentorId)
        {
            return await mentorService.GetMentorByIdAsync(mentorId);
        }

        [HttpPost("create-mentor")]
        public async Task<Response<string>> AddMentorAsync(AddMentorDto mentorDto)
        {
            return await mentorService.CreateMentorAsync(mentorDto);
        }

        [HttpPut("update-mentor")]
        public async Task<Response<string>> UpdateMentorAsync(UpdateMentorDto mentorDto)
        {
            return await mentorService.UpdateMentorAsync(mentorDto);
        }

        [HttpDelete("{mentorId:int}")]
        public async Task<Response<bool>> DeleteMentorAsync(int mentorId)
        {
            return await mentorService.DeleteMentorAsync(mentorId);
        }
    }
}
