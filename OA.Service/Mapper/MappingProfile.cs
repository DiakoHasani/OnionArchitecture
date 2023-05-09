using AutoMapper;
using OA.Common.Helpers;
using OA.Data.Entities;
using OA.Service.DTO.Todo;
using OA.Service.DTO.User;

namespace OA.Service.Mapper;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoEntity, PostTodoModel>();
        CreateMap<PostTodoModel, TodoEntity>();

        CreateMap<RegisterUserModel, ApplicationUser>();
        CreateMap<ApplicationUser, RegisterUserModel>();

        CreateMap<TodoModel, TodoEntity>();
        CreateMap<TodoEntity, TodoModel>().ForMember(a => a.Date, b => b.MapFrom(c => c.Date.GetShamsiDate()));
    }
}