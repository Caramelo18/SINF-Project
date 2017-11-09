using System.Web.Mvc;

namespace FirstREST.Areas.SalesDetails
{
    public class SalesDetailsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SalesDetails";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SalesDetails_default",
                "SalesDetails/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
