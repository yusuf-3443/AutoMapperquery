using Domain.DTOs.CourseDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CourseService;

public interface ICourseService
{
    Task<PagedResponse<List<GetCourseDto>>> GetCourses(CourseFilter filter);
    Task<Response<GetCourseDto>> GetCourseById(int id);
    Task<Response<string>> AddCourse(AddCourseDto course);
    Task<Response<string>> UpdateCourse(UpdateCourseDto course);
    Task<Response<bool>> DeleteCourse(int id);
}