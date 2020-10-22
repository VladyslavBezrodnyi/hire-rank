namespace HireRank.WebApi.Responses
{
    public class BadResponse : BaseResponse
    {
        public string Details { get; set; }

        public int? ErrorCode { get; set; }

        public BadResponse(int statusCode, int? errorCode, string details = "")
            : base(statusCode)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}
