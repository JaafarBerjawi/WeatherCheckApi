namespace Common.Entities
{
    public class APIResponseResult<T>
    {
        public APIResponseStatusCode StatusCode { get; set; }
        public ApiResponseData<T> DataObject { get; set; }
        public ErrorObject ErrorObject { get; set; }
    }

    public enum APIResponseStatusCode
    {
        [APIResponseStatusCode(IsSuccess = true)]
        Success = 200,

        [APIResponseStatusCode(IsSuccess = false)]
        BadRequest = 400,

        [APIResponseStatusCode(IsSuccess = false)]
        Unauthorized = 401,

        [APIResponseStatusCode(IsSuccess = false)]
        InternalServerError = 500
    }

    public class APIResponseStatusCodeAttribute : Attribute
    {
        public bool IsSuccess { get; set; }
    }
}
