using Domain.DTOs.TimeTableDTO;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.TimeTableService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]

public class TimeTableController(ITimeTableService timeTableService):ControllerBase
{
    [HttpGet("get-time-tables")]
    public async Task<Response<List<GetTimeTableDto>>> GetTimeTablesAsync(TimeTableFilter filter)
    {
        return await timeTableService.GetTimeTables(filter);
    }

    [HttpGet("timeTableId:int")]
    public async Task<Response<GetTimeTableDto>> GetTimeTableByIdAsync(int timeTableId)
    {
        return await timeTableService.GetTimeTableById(timeTableId);
    }

    [HttpPost("add-time-table")]
    public async Task<Response<string>> AddTimeTableAsync(AddTimeTableDto timeTableDto)
    {
        return await timeTableService.AddTimeTable(timeTableDto);
    }

    [HttpPut("update-time-table")]
    public async Task<Response<string>> UpdateTimeTableAsync(UpdateTimeTableDto timeTableDto)
    {
        return await timeTableService.UpdateTimeTable(timeTableDto);
    }

    [HttpDelete("timeTableId:int")]
    public async Task<Response<bool>> DeleteTimeTableAsync(int timeTableId)
    {
        return await timeTableService.DeleteTimeTable(timeTableId);
    }
}
