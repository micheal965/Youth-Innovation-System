using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.Entities.Identity;
using Youth_Innovation_System.Core.IRepositories;
using Youth_Innovation_System.Core.IServices;
using Youth_Innovation_System.Core.Specifications.CommentSpecifications;
using Youth_Innovation_System.Core.Specifications.PostSpecifications;
using Youth_Innovation_System.Shared.DTOs.Comment;
using Youth_Innovation_System.Shared.DTOs.Post;
using Youth_Innovation_System.Shared.Exceptions;
using Youth_Innovation_System.Shared.Pagination;

namespace Youth_Innovation_System.Service.PostServices
{
	public class CommentService : ICommentService
	{
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
        public CommentService(IMapper mapper,
							  IUnitOfWork unitOfWork,
							  UserManager<ApplicationUser> userManager)
        {
			_mapper = mapper;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}

        public async Task<CommentResponseDto> CreateCommentAsync(string userId, int postId, CreateCommentDto createCommentDto)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) throw new Exception("user not found");
			var post = await _unitOfWork.Repository<Post>().GetAsync(postId);
			if (post == null) throw new Exception("post not found");
			var commentRepo = _unitOfWork.Repository<Comment>();
			var comment = new Comment()
			{
				UserId = userId,
				postId = postId,
				createOn = DateTime.Now,
				Content = createCommentDto.Content
			};
			try
			{
				await commentRepo.AddAsync(comment);
				await _unitOfWork.CompleteAsync();
				return _mapper.Map<CommentResponseDto>(comment);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to create comment: " + ex.Message);
			}
		}

		public async Task<bool> DeleteCommentAsync(int commentId, string userId)
		{
			var commentRepo = _unitOfWork.Repository<Comment>();
			UpdateOrDeleteCommentSpec spec = new UpdateOrDeleteCommentSpec(commentId, userId);
			var comment = await commentRepo.GetWithSpecAsync(spec);
			if (comment == null) throw new Exception("comment not found");
			//if (comment.UserId != userId) throw new UnauthorizedAccessException("You are not authorized to delete this comment.");

			try
			{
				commentRepo.Delete(comment);
				return await _unitOfWork.CompleteAsync() > 0;
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to delete comment: " + ex.Message);
			}
		}

		public async Task<List<CommentResponseDto>> GetAllCommentsAsync(int postId)
		{
			var post =await _unitOfWork.Repository<Post>().GetAsync(postId);
			if (post == null) throw new Exception("Post not found");
			GetAllCommentsSepc spec = new GetAllCommentsSepc(postId);
			var comments = await _unitOfWork.Repository<Comment>().GetAllWithSpecAsync(spec);
			if (comments.Count == 0)
				throw new Exception("there is no comments on this post");
			return _mapper.Map<List<CommentResponseDto>>(comments);

		}

		public async Task<CommentResponseDto> GetCommentAsync(int CommentId)
		{
			var commentRepo = _unitOfWork.Repository<Comment>();
			GetCommentSpec spec = new GetCommentSpec(CommentId);
			var comment = await commentRepo.GetWithSpecAsync(spec);
			if (comment == null) throw new Exception("comment not found");
			return _mapper.Map<CommentResponseDto>(comment);
		}

		public async Task UpdateCommentAsync(string userId, UpdateCommentDto updateCommentDto)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) throw new Exception("user not found");
			var commentRepo = _unitOfWork.Repository<Comment>();
			UpdateOrDeleteCommentSpec spec = new UpdateOrDeleteCommentSpec(updateCommentDto.Id,userId);
			var comment =await commentRepo.GetWithSpecAsync(spec);
			if (comment == null) throw new Exception("comment not found");
			comment.Content = updateCommentDto.Content ?? comment.Content;
			try 
			{
				commentRepo.Update(comment);
				await _unitOfWork.CompleteAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
