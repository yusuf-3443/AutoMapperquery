using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.MentorGroupDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MentorService
{
    public class MentorGroupService : IMentorGroupService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MentorGroupService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<GetMentorGroupDto>>> GetMentorGroupsAsync()
        {
            try
            {
                var mentorGroups = await _context.MentorGroups.ToListAsync();
                var mapped = _mapper.Map<List<GetMentorGroupDto>>(mentorGroups);
                return new Response<List<GetMentorGroupDto>>(mapped);
            }
            catch (DbException dbEx)
            {
                return new Response<List<GetMentorGroupDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
            }
            catch (Exception ex)
            {
                return new Response<List<GetMentorGroupDto>>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Response<GetMentorGroupDto>> GetMentorGroupByIdAsync(int id)
        {
            try
            {
                var mentorGroup = await _context.MentorGroups.FirstOrDefaultAsync(x => x.Id == id);
                if (mentorGroup == null)
                    return new Response<GetMentorGroupDto>(HttpStatusCode.BadRequest, "Mentor group not found");
                var mapped = _mapper.Map<GetMentorGroupDto>(mentorGroup);
                return new Response<GetMentorGroupDto>(mapped);
            }
            catch (Exception e)
            {
                return new Response<GetMentorGroupDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<string>> CreateMentorGroupAsync(AddMentorGroupDto mentorGroup)
        {
            try
            {
                var mapped = _mapper.Map<MentorGroup>(mentorGroup);

                await _context.MentorGroups.AddAsync(mapped);
                await _context.SaveChangesAsync();

                return new Response<string>("Successfully created a new mentor group");
            }
            catch (Exception e)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
public async Task<Response<string>> UpdateMentorGroupAsync(UpdateMentorGroupDto mentorGroup)
{
    try
    {
        var mappedMentorGroup = _mapper.Map<MentorGroup>(mentorGroup);
        _context.MentorGroups.Update(mappedMentorGroup);
        var update = await _context.SaveChangesAsync();
        if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Mentor group not found");
        return new Response<string>("Mentor group updated successfully");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<bool>> DeleteMentorGroupAsync(int id)
{
    try
    {
        var mentorGroup = await _context.MentorGroups.FirstOrDefaultAsync(x => x.Id == id);
        if (mentorGroup == null)
            return new Response<bool>(HttpStatusCode.BadRequest, "Mentor group not found");

        _context.MentorGroups.Remove(mentorGroup);
        await _context.SaveChangesAsync();

        return new Response<bool>(true);
    }
    catch (Exception e)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
    }
}
    }
}
