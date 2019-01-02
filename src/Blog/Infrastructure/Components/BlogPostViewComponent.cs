using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MarkDonile.Blog.Infrastructure.ViewComponents
{
    public class BlogPostViewComponent : ViewComponent
    {
        private IBlogPostRepository _repository;

        public BlogPostViewComponent(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke(int? id)
        {
            BlogPost blogPost = null;

            if (id.HasValue)
            {
                blogPost = _repository.BlogPosts.FirstOrDefault(bp => bp.Id == id);
            }
            else
            {
                blogPost = _repository.BlogPosts.OrderByDescending(bp => bp.ReleaseDate).FirstOrDefault();
            }

            return View(blogPost);
        }
    }
}