using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CourseService;

public class CourseService(DataContext context, IMapper mapper) : ICourseService
{
    public async Task<PagedResponse<List<GetCourseDto>>> GetCourses(CourseFilter filter)
    {
        try
        {
            var courses = context.Courses.AsQueryable();

            if(!string.IsNullOrEmpty(filter.CourseName))
            courses = courses.Where(x=>x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();
            var mapped = mapper.Map<List<GetCourseDto>>(response);
            return new PagedResponse<List<GetCourseDto>>(mapped,filter.PageNumber,filter.PageSize,totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        try
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Not found");
            var mapped = mapper.Map<GetCourseDto>(course);
            return new Response<GetCourseDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetCourseDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddCourse(AddCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            await context.Courses.AddAsync(mapped);
            var save = await context.SaveChangesAsync();
            if (save > 0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateCourse(UpdateCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            context.Courses.Update(mapped);
            var save = await context.SaveChangesAsync();
            if (save > 0) return new Response<string>("Successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Failed");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }    
    }

    public async Task<Response<bool>> DeleteCourse(int id)
    {
        try
        {

        var course = await context.Courses.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (course > 0) return new Response<bool>(true);
        return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
        
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
        }

    public async Task<Response<List<GetCourseDto>>> GetCourseWithStudentName()
        {
            try
            {
                var courses = context.Courses
         .Where(c => c.Groups.Any(g => g.StudentGroups.Any(sg => sg.Student.FirstName.StartsWith("A")))).ToListAsync();
                var mapped = mapper.Map<List<GetCourseDto>>(courses);
                return new Response<List<GetCourseDto>>(mapped);
            }
            catch (Exception e)
            {
                return new Response<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
                public async Task<Response<GetCourseDto>> GetCourseWithStudentGender()
        {
            try
            {
            var course = await context.Courses.Where(c => c.Groups
            .Any(g => g.StudentGroups.Any(sg => sg.Student.Gender == Gender.Male)) && 
               c.Groups.Any(g => g.StudentGroups.Any(sg => sg.Student.Gender == Gender.Female))).ToListAsync();
                var mapped = mapper.Map<GetCourseDto>(course);
                return new Response<GetCourseDto>(mapped);
            }
            catch (Exception e)
            {
                return new Response<GetCourseDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

}