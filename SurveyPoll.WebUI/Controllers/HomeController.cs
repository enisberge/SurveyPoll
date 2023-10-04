using Microsoft.AspNetCore.Mvc;

namespace SurveyPoll.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult QuestionPoll()
        {
            return View();
        }
        public IActionResult FullError()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}
