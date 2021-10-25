using Microsoft.EntityFrameworkCore;
using oShopSolution.Data.EF;
using oShopSolution.Data.Entities;
using oShopSolution.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Comment
{
	public class CommentService : ICommentService
	{
		private readonly OShopDbContext _context;
		public CommentService(OShopDbContext context)
		{
			_context = context;
		}

		public async Task<int> Create(CommentRequest request)
		{
			var comment = new ProductComment()
			{
				TextComment = request.TextComment,
				Rating = request.Rating,
				ProductId = request.ProductId
			};
			_context.ProductComments.Add(comment);
			await _context.SaveChangesAsync();
			return comment.Id;
		}
		public async Task<List<CommentView>> GetAll()
		{
			var query = from cm in _context.ProductComments
						select new { cm };
			var comment = await query.Select(x => new CommentView()
			{
				Id = x.cm.Id,
				TextComment = x.cm.TextComment,
				Rating = x.cm.Rating,
				ProductId = x.cm.ProductId
			}).ToListAsync();
			return comment;
		}

		public async Task<List<CommentView>> GetAllByProductId(GetCommentPageRequest request)
		{
			var query = from cm in _context.ProductComments
						join p in _context.Products on cm.ProductId equals p.Id into pcm
						from p in pcm.DefaultIfEmpty()
						select new { p,cm};
			if (request.ProductId.HasValue && request.ProductId.Value > 0)
			{
				query = query.Where(p => p.cm.Id == request.ProductId);
			}
			var data = await query.Select(x => new CommentView()
			{
				Id = x.cm.Id,
				TextComment = x.cm.TextComment,
				Rating = x.cm.Rating,
				ProductId = x.cm.ProductId
			}).ToListAsync();
			return data;
		}

		public async Task<CommentView> GetById(int commentId)
		{
			var comment = await _context.ProductComments.FindAsync(commentId);
			var product = await _context.Products.FirstOrDefaultAsync();
			var commentView = new CommentView()
			{
				Id = comment.Id,
				TextComment = comment.TextComment,
				Rating = comment.Rating,
				ProductId = comment.ProductId
			};
			return commentView;
		}
	}
}
