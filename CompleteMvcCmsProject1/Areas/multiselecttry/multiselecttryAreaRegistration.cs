using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.multiselecttry
{
    public class multiselecttryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "multiselecttry";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "multiselecttry_default",
                "multiselecttry/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}