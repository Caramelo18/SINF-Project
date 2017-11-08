using System.Web.Mvc;

namespace FirstREST.Areas.SalesClient
{
    public class SalesClientAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SalesClient";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SalesClient_default",
                "SalesClient/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
