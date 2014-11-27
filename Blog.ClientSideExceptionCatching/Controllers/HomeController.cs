using System;
using System.Web.Mvc;

namespace Blog.ClientSideExceptionCatching.Controllers
{
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void RaiseServerSideException()
        {
            throw new InvalidOperationException("Exception on purpose");
        }
    }
}