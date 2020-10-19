namespace HireRank.WebApi.Responses
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }

        public BaseResponse(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
