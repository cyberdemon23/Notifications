using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Notifications.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
            Session["CurrentUser"] = "TheWorstWay";
            FormsAuthentication.SetAuthCookie(userName, false);
            var url = FormsAuthentication.GetRedirectUrl(userName, false);
            return Redirect(url);
        }
    }
}
