using oShopSolution.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
	public interface ICommentAPI
	{
		Task<List<CommentView>> GetAll();
		Task<CommentView> GetById(int id);
		Task<bool> Create(CommentRequest request);
		Task<List<CommentView>> GetAllByProductId(GetCommentPageRequest request);
	}
}
