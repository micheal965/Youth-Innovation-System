using AutoMapper;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Shared.DTOs.Identity;
using Youth_Innovation_System.Shared.DTOs.Post;

namespace Youth_Innovation_System.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserToReturnDto>();

            CreateMap<UpdateUserDto, ApplicationUser>();

            CreateMap<Post, PostResponseDto>()
                .ForMember(dest => dest.imagesUrls,
                        opt => opt.MapFrom(src => src.postImages.Select(pi => pi.imageUrl)));
        }
    }
}
