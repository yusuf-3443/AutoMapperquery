using AutoMapper;
using Domain.DTOs.GroupDTO;
using Domain.Filters;
using Domain.Responses;

public interface IGroupService
{
    Task<List<GetGroupStudent>> GetGroupStudentsCount();
    Task<PagedResponse<List<GetGroupDto>>> GetGroups(GroupFilter filter);
    Task<Response<GetGroupDto>> GetGroupById(int id);
    Task<Response<string>> AddGroup(AddGroupDto group);
    Task<Response<string>> UpdateGroup(UpdateGroupDto group);
    Task<Response<bool>> DeleteGroup(int id);
}