using AutoMapper;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.Entities.PostAggregate;
using Youth_Innovation_System.Shared.DTOs.Comment;
using Youth_Innovation_System.Shared.DTOs.Identity;
using Youth_Innovation_System.Shared.DTOs.Offer;
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

            //Mapping from comment to commentResponseDto then
            //Mapping from comment to commentreplytoreturndto inside the commentResponseDto then
            //Mapping from react to commentreactiontoreturndto inside the commentResponseDto 
            #region The comments before for this mapping
            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.createdOn, src => src.MapFrom(src => src.createOn))
                .ForMember(dest => dest.Replies, src => src.MapFrom(src => src.Replies))
                .ForMember(dest => dest.Reactions, src => src.MapFrom(src => src.Reactions));

            CreateMap<Comment, CommentReplyToReturnDto>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.createdOn, opt => opt.MapFrom(src => src.createOn));
            CreateMap<React, CommentReactionToReturnDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.reactionType, opt => opt.MapFrom(src => src.reactionType));
            #endregion
            CreateMap<Offer, OfferToReturnDto>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        }
    }
}
