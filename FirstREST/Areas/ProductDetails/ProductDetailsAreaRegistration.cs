using System.Web.Mvc;

namespace FirstREST.Areas.ProductDetails
{
    public class ProductDetailsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ProductDetails";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProductDetails_default",
                "ProductDetails/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
