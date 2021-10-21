using oShopSolution.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Catalog.Comment
{
	public interface ICommentService
	{
		Task<List<CommentView>> GetAll();
		Task<List<CommentView>> GetAllByProductId(GetCommentPageRequest request);
		Task<int> Create(CommentRequest request);
		Task<CommentView> GetById(int commentId);
	}
}
