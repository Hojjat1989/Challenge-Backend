using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MartinDelivery.Api.Auth;

[AttributeUsage(AttributeTargets.Method)]
public class InjectOrganizationAttribute : ActionFilterAttribute
{
    private const string AuthTokenHeader = "auth";
    private const string SecretKey = "key_asdkjhJiioKHAFT876asdjkhSkjh";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var service = context.Controller as IServiceWithOrganization;
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

        var tokenModel = OrganizationTokenModel.FromDictionary(data);
        service.OrganizationId = tokenModel.Id;
    }
}
