using Microsoft.AspNetCore.Mvc;

namespace MoE.ECE.Web.Controllers
{
    public class TestController : ControllerBase
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}