using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.MentorDto;
using Domain.DTOs.MentorDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MentorService
{
    public class MentorService : IMentorService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MentorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<List<GetMentorDto>>> GetMentorsAsync(MentorFilter filter)
        {
            try
            {
                var mentors = _context.Mentors.AsQueryable();

                if(filter.Status>0)
                mentors = mentors.Where(x=>x.Status==filter.Status);

                var response = await mentors
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
                var totalRecord = mentors.Count();

                var mapped = _mapper.Map<List<GetMentorDto>>(response);
                return new PagedResponse<List<GetMentorDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<GetMentorDto>>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<GetMentorDto>> GetMentorByIdAsync(int id)
        {
            try
            {
                var mentor = await _context.Mentors.FirstOrDefaultAsync(x => x.Id == id);
                if (mentor == null)
                    return new Response<GetMentorDto>(HttpStatusCode.BadRequest, "Mentor not found");
                var mapped = _mapper.Map<GetMentorDto>(mentor);
                return new Response<GetMentorDto>(mapped);
            }
            catch (Exception e)
            {
                return new Response<GetMentorDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> CreateMentorAsync(AddMentorDto mentor)
        {
            try
            {
                var existingMentor = await _context.Mentors.FirstOrDefaultAsync(x => x.Email == mentor.Email);
                if (existingMentor != null)
                    return new Response<string>(HttpStatusCode.BadRequest, "Mentor already exists");
                var mapped = _mapper.Map<Mentor>(mentor);

                await _context.Mentors.AddAsync(mapped);
                await _context.SaveChangesAsync();

                return new Response<string>("Successfully created a new mentor");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> UpdateMentorAsync(UpdateMentorDto mentor)
        {
            try
            {
                var mappedMentor = _mapper.Map<Mentor>(mentor);
                _context.Mentors.Update(mappedMentor);
                var update = await _context.SaveChangesAsync();
                if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Mentor not found");
                return new Response<string>("Mentor updated successfully");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteMentorAsync(int id)
        {
            try
            {
                var mentor = await _context.Mentors.Where(x => x.Id == id).ExecuteDeleteAsync();
                if (mentor == 0)
                    return new Response<bool>(HttpStatusCode.BadRequest, "Mentor not found");

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

      
    }
}