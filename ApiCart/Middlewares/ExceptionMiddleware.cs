using CartProject.Application.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;

namespace CartProject.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) { _next = next; }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {

            var result = JsonConvert.SerializeObject(ResultService.Fail(e.Message), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(result);

        }
    }
}

