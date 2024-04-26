using Domain.DTOs.ProgressBookDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ProgressBookService;

public interface IProgressBookService
{
    Task<PagedResponse<List<GetProgressBookDto>>> GetProgressBooks(ProgressBookFilter filter);
    Task<Response<GetProgressBookDto>> GetProgressBookById(int id);
    Task<Response<string>> AddProgressBook(AddProgressBookDto progressBook);
    Task<Response<string>> UpdateProgressBook(UpdateProgressBookDto progressBook);
    Task<Response<bool>> DeleteProgressBook(int id);
}
