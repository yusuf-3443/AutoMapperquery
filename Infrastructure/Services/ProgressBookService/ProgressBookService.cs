using System.Net;
using AutoMapper;
using Domain.DTOs.ProgressBookDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ProgressBookService;

public class ProgressBookService(DataContext context, IMapper mapper) : IProgressBookService
{
    public async Task<PagedResponse<List<GetProgressBookDto>>> GetProgressBooks(ProgressBookFilter filter)
    {
        try
        {
            var progressBooks = context.ProgressBook.AsQueryable();
            if (filter.Grade > 0)
                progressBooks = progressBooks.Where(x => x.Grade == filter.Grade);

            var response = await progressBooks.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var totalRecord = progressBooks.Count();

            var mapped = mapper.Map<List<GetProgressBookDto>>(response);
            return new PagedResponse<List<GetProgressBookDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetProgressBookDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetProgressBookDto>> GetProgressBookById(int id)
    {
        try
        {
            var progressBook = await context.ProgressBook.FirstOrDefaultAsync(x => x.Id == id);
            if (progressBook == null)
                return new Response<GetProgressBookDto>(HttpStatusCode.BadRequest, "ProgressBook not found");
            var mapped = mapper.Map<GetProgressBookDto>(progressBook);
            return new Response<GetProgressBookDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetProgressBookDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> AddProgressBook(AddProgressBookDto progressBook)
    {
        try
        {
            var mapped = mapper.Map<ProgressBook>(progressBook);

            await context.ProgressBook.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new progress book");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateProgressBook(UpdateProgressBookDto progressBook)
    {
        try
        {
            var mappedProgressBook = mapper.Map<ProgressBook>(progressBook);
            context.ProgressBook.Update(mappedProgressBook);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "ProgressBook not found");
            return new Response<string>("ProgressBook updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteProgressBook(int id)
    {
        try
        {
            var progressBook = await context.ProgressBook.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (progressBook == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "ProgressBook not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public Task<PagedResponse<List<GetProgressBookDto>>> GetProgressBooks()
    {
        throw new NotImplementedException();
    }

}