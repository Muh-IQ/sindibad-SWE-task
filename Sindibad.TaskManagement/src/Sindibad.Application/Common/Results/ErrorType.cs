using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application.Common.Results;

public enum ErrorType
{

    None = 0,

    BadRequest = 400,

    Unauthorized = 401,

    Forbidden = 403,

    NotFound = 404,

    Conflict = 409,

    Timeout = 408,

    Validation = 422,

    InternalServerError = 500,

    ServiceUnavailable = 503
}