using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIntegrationTestSample
{
    public class BlogsRepositorio
    {
        private readonly BloggingContext _bloggingContext;

        public BlogsRepositorio(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        public async Task InserirBlog(Blog blog)
        {
            _bloggingContext.Add(blog);
            
            await _bloggingContext.SaveChangesAsync();
        }

        public async Task<Blog> ObterBlog(int blogId)
        {
            return await _bloggingContext.Blogs.FindAsync(blogId);
        }
    }
}