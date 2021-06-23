using System;
using System.Net;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace eMedicalRecords.API.Infrastructures.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next) => 
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            switch (ex)
            {
                case RecordNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ArgumentNullException:
                    break;
            }
            
            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(new
            {
                ex.Message
            });

            await context.Response.WriteAsync(result);
        }
    }
}