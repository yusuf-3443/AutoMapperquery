using AutoMapper;
using Domain.DTOs.CourseDTO;
using Domain.DTOs.GroupDTO;
using Domain.DTOs.MentorDto;
using Domain.DTOs.MentorGroupDTO;
using Domain.DTOs.ProgressBookDTO;
using Domain.DTOs.StudentDTO;
using Domain.DTOs.StudentGroupDTO;
using Domain.DTOs.TimeTableDTO;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Student, AddStudentDto>().ReverseMap();
        CreateMap<Student, GetStudentDto>().ReverseMap();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();
        CreateMap<Course, AddCourseDto>().ReverseMap();
        CreateMap<Course, GetCourseDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();
        CreateMap<Group, AddGroupDto>().ReverseMap();
        CreateMap<Group, GetGroupDto>().ReverseMap();
        CreateMap<Group, UpdateGroupDto>().ReverseMap();
        CreateMap<Mentor, AddMentorDto>().ReverseMap();
        CreateMap<Mentor, GetMentorDto>().ReverseMap();
        CreateMap<Mentor, UpdateMentorDto>().ReverseMap();
                CreateMap<MentorGroup, AddMentorGroupDto>().ReverseMap();
                CreateMap<MentorGroup, GetMentorGroupDto>().ReverseMap();
                CreateMap<MentorGroup, UpdateMentorGroupDto>().ReverseMap();
                                CreateMap<StudentGroup, AddStudentGroupDto>().ReverseMap();
                                CreateMap<StudentGroup, GetStudentGroupDto>().ReverseMap();
                                CreateMap<StudentGroup, UpdateStudentGroupDto>().ReverseMap();
                                CreateMap<TimeTable, AddTimeTableDto>().ReverseMap();
                                CreateMap<TimeTable, GetTimeTableDto>().ReverseMap();
                                CreateMap<TimeTable, UpdateTimeTableDto>().ReverseMap();
                                CreateMap<ProgressBook, AddProgressBookDto>().ReverseMap();
                                CreateMap<ProgressBook, GetProgressBookDto>().ReverseMap();
                                CreateMap<ProgressBook, UpdateProgressBookDto>().ReverseMap();








        // //ForMembers
        // CreateMap< Student,GetStudentDto>()
        //     .ForMember(sDto => sDto.FulName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
        //     .ForMember(sDto => sDto.EmailAddress, opt => opt.MapFrom(s =>s.Email));
        //
        // //Reverse map
        // CreateMap<BaseStudentDto,Student>().ReverseMap();
        //
        // // ignore
        // CreateMap<Student, AddStudentDto>()
        //     .ForMember(dest => dest.FirstName, opt => opt.Ignore());


    }   
}