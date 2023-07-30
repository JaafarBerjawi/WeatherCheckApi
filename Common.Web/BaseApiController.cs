using Common.Business;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Common.Web
{
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Structures APIResponseResult as API Response
        /// </summary>
        /// <typeparam name="T">Success data type</typeparam>
        /// <param name="responseResult"></param>
        /// <returns>API Response</returns>
        public object GetAPIResponse<T>(APIResponseResult<T> responseResult)
        {
            HttpContext.Response.StatusCode = (int)responseResult.StatusCode;
            
            if(APIResponseHelper.IsAPIResponseStatusCodeSuccessful(responseResult.StatusCode))
            {
                return responseResult.DataObject;
            }
            return responseResult.ErrorObject;
        }
    }
}