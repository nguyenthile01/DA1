using AutoMapper;
using Y.Authorization.Users;

namespace Y.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<CreateOrEditUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
            CreateMap<User, CreateOrEditUserDto>().ForMember(x => x.RoleNames, opt => opt.Ignore());
        }
    }
}
