using System.Web.Mvc;

namespace FirstREST.Areas.AccountsReceivable
{
    public class AccountsReceivableAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AccountsReceivable";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AccountsReceivable_default",
                "AccountsReceivable/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
