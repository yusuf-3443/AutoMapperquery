using System.Net;
using AutoMapper;
using Domain.DTOs.TimeTableDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TimeTableService;

public class TimeTableService(DataContext context, IMapper mapper) : ITimeTableService
{
    public async Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTables(TimeTableFilter filter)
    {
        try
        {
        var timetables = context.TimeTable.AsQueryable();
        if(filter.DayOfWeek>0)
        timetables = timetables.Where(x=>x.DayOfWeek==filter.DayOfWeek);

         var response = await timetables.Skip((filter.PageNumber-1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
         var totalRecord = timetables.Count();

         var mapped = mapper.Map<List<GetTimeTableDto>>(response);
                return new PagedResponse<List<GetTimeTableDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
    
            
        }
         catch (Exception ex)
            {
                return new PagedResponse<List<GetTimeTableDto>>(HttpStatusCode.InternalServerError, ex.Message);
            }
    }
   public async Task<Response<GetTimeTableDto>> GetTimeTableById(int id)
{
    try
    {
        var timeTable = await context.TimeTable.FirstOrDefaultAsync(x => x.Id == id);
        if (timeTable == null)
            return new Response<GetTimeTableDto>(HttpStatusCode.BadRequest, "TimeTable not found");
        var mapped = mapper.Map<GetTimeTableDto>(timeTable);
        return new Response<GetTimeTableDto>(mapped);
    }
    catch (Exception e)
    {
        return new Response<GetTimeTableDto>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<string>> AddTimeTable(AddTimeTableDto timeTable)
{
    try
    {
        var mapped = mapper.Map<TimeTable>(timeTable);

        await context.TimeTable.AddAsync(mapped);
        await context.SaveChangesAsync();

        return new Response<string>("Successfully created a new time table");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto timeTable)
{
    try
    {
        var mappedTimeTable = mapper.Map<TimeTable>(timeTable);
        context.TimeTable.Update(mappedTimeTable);
        var update = await context.SaveChangesAsync();
        if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "TimeTable not found");
        return new Response<string>("TimeTable updated successfully");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<bool>> DeleteTimeTable(int id)
{
    try
    {
        var timeTable = await context.TimeTable.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (timeTable == 0)
            return new Response<bool>(HttpStatusCode.BadRequest, "TimeTable not found");

        return new Response<bool>(true);
    }
    catch (Exception e)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
    }
}
}