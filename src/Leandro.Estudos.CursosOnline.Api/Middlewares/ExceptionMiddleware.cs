using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Leandro.Estudos.CursosOnline.Api.Middlewares
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        HandleExceptionAsync(context, ex);
      }
    }

    private void HandleExceptionAsync(HttpContext context, Exception ex)
    {
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
  }

  public static class ExceptionMiddlewareHandler
  {
    public static IApplicationBuilder UseExcptionMiddleware(this IApplicationBuilder app)
    {
      app.UseMiddleware<ExceptionMiddleware>();
      return app;
    }
  }
}