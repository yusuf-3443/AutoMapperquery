using Domain.DTOs.ProgressBookDTO;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.ProgressBookService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class ProgressBookController(IProgressBookService progressBookService):ControllerBase
{
    [HttpGet("get-progress-books")]
    public async Task<Response<List<GetProgressBookDto>>> GetProgressBooksAsync(ProgressBookFilter filter)
    {
        return await progressBookService.GetProgressBooks(filter);
    }

    [HttpGet("progressBookId:int")]
    public async Task<Response<GetProgressBookDto>> GetProgressBookByIdAsync(int progressBookId)
    {
        return await progressBookService.GetProgressBookById(progressBookId);
    }

    [HttpPost("add-progress-book")]
    public async Task<Response<string>> AddProgressBookAsync(AddProgressBookDto progressBookDto)
    {
        return await progressBookService.AddProgressBook(progressBookDto);
    }

    [HttpPut("update-progress-book")]
    public async Task<Response<string>> UpdateProgressBookAsync(UpdateProgressBookDto progressBookDto)
    {
        return await progressBookService.UpdateProgressBook(progressBookDto);
    }

    [HttpDelete("progressBookId:int")]
    public async Task<Response<bool>> DeleteProgressBookAsync(int progressBookId)
    {
        return await progressBookService.DeleteProgressBook(progressBookId);
    }
}
