using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.NewsPortal
{
    public class NewsPortalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NewsPortal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NewsPortal_default",
                "NewsPortal/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}