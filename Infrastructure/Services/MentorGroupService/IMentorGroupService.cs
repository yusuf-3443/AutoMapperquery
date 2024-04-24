using Domain.DTOs.MentorGroupDTO;
using Domain.Responses;

namespace Infrastructure.Services.MentorService
{
    public interface IMentorGroupService
    {
        Task<Response<List<GetMentorGroupDto>>> GetMentorGroupsAsync();
        Task<Response<GetMentorGroupDto>> GetMentorGroupByIdAsync(int id);
        Task<Response<string>> CreateMentorGroupAsync(AddMentorGroupDto mentorGroup);
        Task<Response<string>> UpdateMentorGroupAsync(UpdateMentorGroupDto mentorGroup);
        Task<Response<bool>> DeleteMentorGroupAsync(int id);
    }
}
