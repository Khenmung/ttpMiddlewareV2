using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttpMiddleware.Models.DTOs
{
    public class RequestObject : IRequestObject
    {
        private readonly IHttpContextAccessor _context;

        public RequestObject(IHttpContextAccessor context)
        {
            this._context = context;
        }
        public IHttpContextAccessor getobject()
        {
            return this._context;
        }
    }
}
