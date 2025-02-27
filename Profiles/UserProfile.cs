using AutoMapper;
using loja_api.Entities;
using loja_api.Mapper.User;

namespace loja_api.Profiles;

public class UserProfile : Profile
{

    public UserProfile() 
    {
    
        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<User, UserUpdateDTO>().ReverseMap();   

        CreateMap<User, CreateUserDTO>().ReverseMap()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
    }
}
