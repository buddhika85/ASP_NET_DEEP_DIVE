
using System.Text;

namespace _6_MinimalAPI_DI_CodeOrganization.Results
{
    public class HtmlResult : IResult
    {
        private readonly string html;

        public HtmlResult(string html)
        {
            this.html = html;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.ContentType = "test/html";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(html);
            await context.Response.WriteAsync(html);
        }
    }
}
