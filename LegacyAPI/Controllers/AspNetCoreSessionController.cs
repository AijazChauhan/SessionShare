using Microsoft.AspNetCore.Mvc;

namespace LegacyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AspNetCoreSessionController : Controller
    {
        [HttpGet("Index")]
        public ActionResult Index()
        {
            var model = System.Web.HttpContext.Current?.Session?["Test"] as string;

            return View(model);
        }

        
    }
}
