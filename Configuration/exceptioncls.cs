using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ttpMiddleware.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace ttpMiddleware.Common
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private static ttpauthContext contextdb;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ttpauthContext dbcontext)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex, dbcontext).ConfigureAwait(false);
            }
        }
        public static Task HandleExceptionMessageAsync(HttpContext context, Exception exception, ttpauthContext dbcontext)
        {
            contextdb = dbcontext;
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.InnerException
            });
            string url = context.Request.Path.ToString();

            //LogError(url, result.ToString());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
        public static void LogError(string moduleName, string detail, string userId = "", int orgId = 0)
        {
            //ttpauthContext context = new ttpauthContext();
            //var parentId = await contextdb.MasterItems
            //    .Where(x => x.OrgId == orgId && x.MasterDataName.ToLower() == "error status")
            //    .Select(s => s.MasterDataId).ToListAsync();

            //var statusId = await contextdb.MasterItems
            //    .Where(x => x.OrgId == orgId &&
            //x.MasterDataName.ToLower() == "pending" && x.ParentId == parentId[0])
            //    .Select(s => s.MasterDataId).ToListAsync();

            ErrorLog log = new ErrorLog()
            {
                Detail = detail,
                CreatedDate = DateTime.Now,
                CreatedBy = userId,
                OrgId = (short)orgId,
                ModuleName = moduleName,
                StatusId = 0
            };
            contextdb.ErrorLogs.Add(log);
            contextdb.SaveChanges();

        }
    }
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

}
