
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TechChallengeFiap.Application.Helpers;

public class ResultObject : ObjectResult
{
    public ResultObject(HttpStatusCode statusCode, object value) : base(value)
    {
        StatusCode = (int)statusCode;
    }
}