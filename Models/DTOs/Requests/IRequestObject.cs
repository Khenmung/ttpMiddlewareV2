using Microsoft.AspNetCore.Http;

namespace ttpMiddleware.Models.DTOs
{
    public interface IRequestObject
    {
        IHttpContextAccessor getobject();
    }
}