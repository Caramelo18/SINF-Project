using System.Web.Mvc;

namespace FirstREST.Areas.AccountsPayable
{
    public class AccountsPayableAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AccountsPayable";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AccountsPayable_default",
                "AccountsPayable/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
