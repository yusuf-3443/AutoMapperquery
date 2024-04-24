using Domain.DTOs.StudentGroupDTO;
using Domain.Responses;
using Infrastructure.Services.StudentGroupService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
[ApiController]
public class StudentGroupController(IStudentGroupService studentGroupService) : ControllerBase
{
    [HttpGet("get-student-groups")]
    public async Task<Response<List<GetStudentGroupDto>>> GetStudentGroupsAsync()
    {
        return await studentGroupService.GetStudentGroupsAsync();
    }

    [HttpGet("{studentgroupId:int}")]
    public async Task<Response<GetStudentGroupDto>> GetStudentGroupByIdAsync(int studentgroupId)
    {
        return await studentGroupService.GetStudentGroupByIdAsync(studentgroupId);
    }

    [HttpPost("create-student-group")]
    public async Task<Response<string>> AddStudentGroupAsync(AddStudentGroupDto studentGroupDto)
    {
        return await studentGroupService.CreateStudentGroupAsync(studentGroupDto);
    }

    [HttpPut("update-student-group")]
    public async Task<Response<string>> UpdateStudentGroupAsync(UpdateStudentGroupDto studentGroupDto)
    {
        return await studentGroupService.UpdateStudentGroupAsync(studentGroupDto);
    }

    [HttpDelete("{studentgroupId:int}")]
    public async Task<Response<bool>> DeleteStudentGroupAsync(int studentgroupId)
    {
        return await studentGroupService.DeleteStudentGroupAsync(studentgroupId);
    }
}
