using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.PhotoSection.Models
{
    public class summernote
    {
        [AllowHtml]
        public string content { get; set; }
    }
}