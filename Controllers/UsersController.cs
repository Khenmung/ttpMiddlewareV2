using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ttpMiddleware.Data.Entities;
using ttpMiddleware.Models;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    [ODataRoutePrefix("[controller]")]
    public class UserCollsController : ProtectedController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ttpauthContext _dbContext;

        public UserCollsController(UserManager<ApplicationUser> userManager,ttpauthContext dbContext)
        {
            this._userManager = userManager;
            this._dbContext = dbContext;
        }
        [HttpGet]
        [EnableQuery]
        public IQueryable<ApplicationUser> GetUser()
        {
            return _userManager.Users.AsQueryable();
        }
    }
}
