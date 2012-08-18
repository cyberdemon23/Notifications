using System.Web.Mvc;
using System.Web.Http;

namespace Notifications.Web.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            
        }
    }
}
