using AutoMapper;
using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Utils;

public class AutoMapping: Profile
{
    public AutoMapping()
    {
        CreateMap<UserInsertDTO, UserModel>();
        CreateMap<UserDTO, UserModel>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(mf => mf.UserId.FromHashId()))
           .ReverseMap()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(mf => mf.UserId.ToHashId()));
        CreateMap<AccountDTO, UserModel>();

        CreateMap<TaskDTO, TaskModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(mf => mf.UserId.FromHashId()))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(mf => mf.TaskId.FromHashId()))
            .ReverseMap()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(mf => mf.UserId.ToHashId()))
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(mf => mf.TaskId.ToHashId()));

        CreateMap<TaskInsertDTO, TaskModel>();
    }
}
