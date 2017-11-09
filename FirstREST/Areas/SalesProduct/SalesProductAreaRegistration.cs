using System.Web.Mvc;

namespace FirstREST.Areas.SalesProduct
{
    public class SalesProductAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SalesProduct";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SalesProduct_default",
                "SalesProduct/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            );
        }
    }
}
