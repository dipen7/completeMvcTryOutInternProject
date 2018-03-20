using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.CmsAdmin
{
    public class CmsAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CmsAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            
            context.MapRoute(
                "CmsAdmin_default",
                "CmsAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}