using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Enums
{
    public enum ResponseStatusCode
    {
        // 1xx - Information
        // 2xx - Success
        // 3xx - Redirection
        // 4xx - Cleint Error
        // 5xx - Server Error

        Continue = 100,
        Ok = 200,
        Created = 201,
        Accepted = 202,
        MovedPermanently = 301,
        Found = 302,
        NotModified = 304,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        InternalServerError = 500
    }
}
