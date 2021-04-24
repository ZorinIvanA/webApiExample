using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace FitnessRecords.Presentation.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            builder.Use(async (context, next) =>
            {
                Log.Logger.Information($"Запрос {context.Request.Method} {context.Request.Path} начат");
                await next.Invoke();
                Log.Logger.Information($"Запрос {context.Request.Method} {context.Request.Path} закончен");
            });

            return builder;
        }
    }
}
