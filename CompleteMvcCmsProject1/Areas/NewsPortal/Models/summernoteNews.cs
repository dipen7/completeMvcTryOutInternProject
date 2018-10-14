using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompleteMvcCmsProject1.Areas.NewsPortal.Models
{
    public class summernoteNews
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Discussion { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}