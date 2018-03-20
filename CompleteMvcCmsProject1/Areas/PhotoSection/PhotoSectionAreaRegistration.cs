using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.PhotoSection
{
    public class PhotoSectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PhotoSection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PhotoSection_default",
                "PhotoSection/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}