using Domain.DTOs.StudentGroupDTO;
using Domain.Responses;

namespace Infrastructure.Services.StudentGroupService;

public interface IStudentGroupService
{
    Task<Response<List<GetStudentGroupDto>>> GetStudentGroupsAsync();
    Task<Response<GetStudentGroupDto>> GetStudentGroupByIdAsync(int id);
    Task<Response<string>> CreateStudentGroupAsync(AddStudentGroupDto studentGroup);
    Task<Response<string>> UpdateStudentGroupAsync(UpdateStudentGroupDto studentGroup);
    Task<Response<bool>> DeleteStudentGroupAsync(int id);
}