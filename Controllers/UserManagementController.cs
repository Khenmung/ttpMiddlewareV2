using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Data;
using ttpMiddleware.Data.Entities;

using ttpMiddleware.CommonFunctions;namespace ttpMiddleware.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ODataRoutePrefix("[controller]")]
    public class UserManagementController : ProtectedController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagementController(AppDBContext appDBContext,UserManager<ApplicationUser> userManager )
        {
            AppDBContext = appDBContext;
            this.userManager = userManager;
        }

        public AppDBContext AppDBContext { get; }

        [HttpGet]
        [EnableQuery]
        public IQueryable<ApplicationUser> GetUser()
        {
            return AppDBContext.Users.AsQueryable();
        }
        [HttpGet]
        public async Task<IActionResult> GetUserClaims(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                //Redirect to NotFound
                return NotFound();
            }
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var claims = await userManager.GetClaimsAsync(user);
            return (IActionResult)claims;
        }
    }
}
