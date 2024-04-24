using System.Net;
using AutoMapper;
using Domain.DTOs.CourseDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CourseService;

public class CourseService(DataContext context, IMapper mapper) : ICourseService
{
    public async Task<Response<List<GetCourseDto>>> GetCourses()
    {
        try
        {
            var courses = await context.Courses.ToListAsync();
            var mapped = mapper.Map<List<GetCourseDto>>(courses);
            return new Response<List<GetCourseDto>>(mapped);
        }
        catch (Exception e)
        {
            return new Response<List<GetCourseDto>>(HttpStatusCode.InternalServerError, e.Message);
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
}