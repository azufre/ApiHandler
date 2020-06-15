using System;
using System.Net;

namespace ApiHandler.Handler
{
    public class HttpException :  Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
