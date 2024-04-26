using Domain.DTOs.MentorDto;
using Domain.DTOs.MentorDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MentorService;

public interface IMentorService
{
    Task<PagedResponse<List<GetMentorDto>>> GetMentorsAsync(MentorFilter filter);
    Task<Response<GetMentorDto>> GetMentorByIdAsync(int id);
    Task<Response<string>> CreateMentorAsync(AddMentorDto mentor);
    Task<Response<string>> UpdateMentorAsync(UpdateMentorDto mentor);
    Task<Response<bool>> DeleteMentorAsync(int id);
}