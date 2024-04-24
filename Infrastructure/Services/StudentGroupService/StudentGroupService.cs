using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.StudentGroupDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StudentGroupService;

public class StudentGroupService(DataContext context, IMapper mapper) : IStudentGroupService
{
    #region GetStudentGroupsAsync

    public async Task<Response<List<GetStudentGroupDto>>> GetStudentGroupsAsync()
    {
        try
        {
            var studentGroups = await context.StudentGroups.ToListAsync();
            var mapped = mapper.Map<List<GetStudentGroupDto>>(studentGroups);
            return new Response<List<GetStudentGroupDto>>(mapped);
        }
        catch (DbException dbEx)
        {
            return new Response<List<GetStudentGroupDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new Response<List<GetStudentGroupDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    #endregion

    #region GetStudentGroupByIdAsync

    public async Task<Response<GetStudentGroupDto>> GetStudentGroupByIdAsync(int id)
    {
        try
        {
            var studentGroup = await context.StudentGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (studentGroup == null)
                return new Response<GetStudentGroupDto>(HttpStatusCode.BadRequest, "Student group not found");
            var mapped = mapper.Map<GetStudentGroupDto>(studentGroup);
            return new Response<GetStudentGroupDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStudentGroupDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region CreateStudentGroupAsync

    public async Task<Response<string>> CreateStudentGroupAsync(AddStudentGroupDto studentGroup)
    {
        try
        {
            var mapped = mapper.Map<StudentGroup>(studentGroup);

            await context.StudentGroups.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new student group");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region UpdateStudentGroupAsync

    public async Task<Response<string>> UpdateStudentGroupAsync(UpdateStudentGroupDto studentGroup)
    {
        try
        {
            var mappedStudentGroup = mapper.Map<StudentGroup>(studentGroup);
            context.StudentGroups.Update(mappedStudentGroup);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Student group not found");
            return new Response<string>("Student group updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    public async Task<Response<bool>> DeleteStudentGroupAsync(int id)
    {
        try
        {
            var studentGroup = await context.StudentGroups.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (studentGroup == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Student group not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
