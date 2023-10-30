using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using ttpMiddleware.Models;

namespace ttpMiddleware.RawModels
{
    public interface ISync
    {
        public Task<IActionResult> Sync(JProperty data);
    }
}
