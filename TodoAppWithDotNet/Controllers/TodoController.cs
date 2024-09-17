using Microsoft.AspNetCore.Mvc;

namespace TodoAppWithDotNet.Controllers
{
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
