using AutoMapper;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Shared.DTOs.Identity;

namespace Youth_Innovation_System.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserToReturnDto>();

            CreateMap<UpdateUserDto, ApplicationUser>();
        }
    }
}
