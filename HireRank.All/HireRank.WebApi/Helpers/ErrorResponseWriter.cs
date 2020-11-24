using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace HireRank.WebApi.Helpers
{
    public class ErrorResponseWriter
    {
        public static async Task WriteExceptionResponseAsync(HttpContext context, string response)
        {
            var originalBodyStream = context.Response.Body;
            try
            {
                using var streamWriter = new StreamWriter(originalBodyStream);
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await streamWriter.WriteAsync(response);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}
