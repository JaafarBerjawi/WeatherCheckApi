using Common.Entities;
using System.Reflection;

namespace Common.Business
{
    public class APIResponseHelper
    {
        public static APIResponseResult<T> GetSuccessfulResponse<T>(T data)
        {
            return new APIResponseResult<T>
            {
                StatusCode = APIResponseStatusCode.Success,
                DataObject = new ApiResponseData<T> { Data = data }
            };
        }

        public static APIResponseResult<T> GetBadRequestResponse<T>(string errorCode, string errorMessage)
        {
            return new APIResponseResult<T>
            {
                StatusCode = APIResponseStatusCode.BadRequest,
                ErrorObject = new ErrorObject
                {
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage
                }
            };
        }

        public static APIResponseResult<T> GetInternalServerErrorResponse<T>()
        {
            return new APIResponseResult<T>
            {
                StatusCode = APIResponseStatusCode.InternalServerError,
                ErrorObject = new ErrorObject
                {
                    ErrorCode = "INRNL-SRVR-ERR",
                    ErrorMessage = "Internal Server Error"
                }
            };
        }

        public static APIResponseResult<T> GetMissingInputResponse<T>(string fieldName)
        {
            return GetBadRequestResponse<T>("MISSING-INPUT", $"{fieldName} is required");
        }

        public static bool IsAPIResponseStatusCodeSuccessful(APIResponseStatusCode statusCode)
        {
            // Get the enum member's field info
            FieldInfo fieldInfo = typeof(APIResponseStatusCode).GetField(statusCode.ToString());

            // Get the APIResponseStatusCodeAttribute attached to the enum member
            APIResponseStatusCodeAttribute attribute = fieldInfo?.GetCustomAttribute<APIResponseStatusCodeAttribute>();

            // Access the attribute value
            return attribute?.IsSuccess ?? false;
        }
    }
}