namespace easypay.web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Redirect("Easypay");
        }
    }
}