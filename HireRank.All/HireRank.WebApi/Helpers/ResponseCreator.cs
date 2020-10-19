using HireRank.WebApi.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace HireRank.WebApi.Helpers
{
    public static class ResponseCreator
    {
        public static async Task<string> CreateSuccessResponseAsync(HttpResponse response, int statusCode)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(response.Body).ReadToEndAsync();
            object responseObj = JsonConvert.DeserializeObject<object>(responseText);
            OkResponse okResponse = new OkResponse(statusCode, responseObj);
            response.Body.Seek(0, SeekOrigin.Begin);
            return JsonConvert.SerializeObject(okResponse);
        }

        public static string CreateBadResponse(int statusCode, int errorCode, string details)
        {
            BadResponse badResponse = new BadResponse(statusCode, errorCode, details);
            return JsonConvert.SerializeObject(badResponse);
        }
    }
}
