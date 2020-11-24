using HireRank.Common.Exceptions;
using HireRank.WebApi.Enums;
using HireRank.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using ValidationException = HireRank.Common.Exceptions.ValidationException;

namespace HireRank.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == 403)
                {
                    await ErrorResponseWriter.WriteExceptionResponseAsync(context, "You don't a have permission");
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string exceptionResponse;
            if (ex.GetType() == typeof(EntityNotFoundException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)HttpStatusCode.BadRequest,
                                                                      (int)ErrorCodes.EntityNotFoundError,
                                                                       ex.Message);
            }
            else if (ex.GetType() == typeof(DatabaseException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)HttpStatusCode.BadRequest,
                                                                      (int)ErrorCodes.DatabaseError,
                                                                       ex.Message);
            }
            else if (ex.GetType() == typeof(ValidationException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)HttpStatusCode.BadRequest,
                                                                      (int)ErrorCodes.ValidationError,
                                                                       ex.Message);
            }
            else
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)HttpStatusCode.InternalServerError,
                                                                      (int)ErrorCodes.ServerError,
                                                                        "server error");
            }
            await ErrorResponseWriter.WriteExceptionResponseAsync(context, exceptionResponse);
        }
    }
}
