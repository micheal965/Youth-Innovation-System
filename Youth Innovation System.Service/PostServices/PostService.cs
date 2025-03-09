using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Shared.DTOs.Post;
using Youth_Innovation_System.Shared.Exceptions;

namespace Youth_Innovation_System.Service.PostServices
{
    public class PostService : IPostService
    {
        private readonly ICloudinaryServices _cloudinaryServices;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork,
                          ICloudinaryServices cloudinaryServices,
                          IMapper mapper,
                          UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryServices = cloudinaryServices;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PostResponseDto> CreatePostAsync(string userId, CreatePostDto createPostDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new NotFoundException("User not found");

            var postRepo = _unitOfWork.Repository<Post>();

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var newPost = new Post()
                {
                    UserId = userId,
                    Title = createPostDto.Title,
                    Content = createPostDto.Content,
                };

                await postRepo.AddAsync(newPost);
                await _unitOfWork.CompleteAsync();//Save changes to get postId

                //Upload images if provided
                if (createPostDto.Images != null && createPostDto.Images.Count > 0)
                {
                    var postImageRepo = _unitOfWork.Repository<PostImage>();

                    var uploadResults = await _cloudinaryServices.UploadImagesAsync(createPostDto.Images);
                    var postImages = uploadResults.Select(result => new PostImage
                    {
                        PostId = newPost.Id,
                        imageUrl = result.imageUrl,
                        imagePublicId = result.publicId,
                    }).ToList();
                    await postImageRepo.AddRangeAsync(postImages);
                    await _unitOfWork.CompleteAsync();
                }
                await transaction.CommitAsync();
                return _mapper.Map<PostResponseDto>(newPost);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAllPostsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
