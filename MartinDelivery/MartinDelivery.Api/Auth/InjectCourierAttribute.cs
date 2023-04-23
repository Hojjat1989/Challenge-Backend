using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MartinDelivery.Api.Auth;

[AttributeUsage(AttributeTargets.Method)]
public class InjectCourierAttribute : ActionFilterAttribute
{
    private const string AuthTokenHeader = "auth";
    private const string SecretKey = "key_lkajsdfhUAYFIUsadhfAGFjh12000";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var service = context.Controller as IServiceWithCourier;
        if (service == null)
        {
            throw new Exception("controller should inherit from IServiceWithOrganization interface");
        }

        if (!context.HttpContext.Request.Headers.TryGetValue(AuthTokenHeader, out var authHeaders))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authToken = authHeaders.First();
        if (string.IsNullOrWhiteSpace(authToken))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!JwtTokenHandler.ValidateToken(SecretKey, authToken, out var data))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var tokenModel = CourierTokenModel.FromDictionary(data);
        service.CourierId = tokenModel.Id;
    }
}
