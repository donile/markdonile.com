using AutoMapper;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarkDonile.Blog.Controllers
{
    public class BlogPostController : Controller
    {
        private IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public BlogPostController(
            IBlogPostRepository repository,
            IMapper mapper
        )
        {
            _blogPostRepository = repository;
            _mapper = mapper;
        }

        public ViewResult List()
        {
            return View();
        }

        [HttpGet("blog-posts")]
        public IActionResult GetBlogPosts()
        {
            return Ok(_blogPostRepository.GetAll());
        }

        [HttpGet("blog-posts/{id}", Name = nameof(GetBlogPost))]
        public IActionResult GetBlogPost(int id)
        {
            var blogPost = _blogPostRepository.Get(id);

            if (blogPost is null)
            {
                return NotFound();
            }
            
            return Ok(blogPost);
        }

        [Authorize]
        [HttpPost("blog-posts")]
        public IActionResult CreateBlogPost(CreateBlogPostDto blogPostToCreate)
        {
            if (blogPostToCreate is null)
            {
                return BadRequest();
            }
            
            var blogPost = _mapper.Map<BlogPost>(blogPostToCreate);

            _blogPostRepository.Add(blogPost);
            _blogPostRepository.SaveChanges();

            return CreatedAtRoute(
                nameof(GetBlogPost),
                new { id = blogPost.Id },
                blogPost
            );
        }
    }
}