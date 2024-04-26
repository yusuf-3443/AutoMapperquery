using Domain.DTOs.TimeTableDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TimeTableService;

public interface ITimeTableService
{
    Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTables(TimeTableFilter filter);
    Task<Response<GetTimeTableDto>> GetTimeTableById(int id);
    Task<Response<string>> AddTimeTable(AddTimeTableDto timeTable);
    Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto timeTable);
    Task<Response<bool>> DeleteTimeTable(int id);
}
