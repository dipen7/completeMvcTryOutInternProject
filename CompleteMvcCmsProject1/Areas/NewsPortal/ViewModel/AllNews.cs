
using CompleteMvcCmsProject1.Areas.NewsPortal.Models;
using System.Collections.Generic;

namespace CompleteMvcCmsProject1.Areas.NewsPortal.ViewModel
{
    public class AllNews
    {
        public IEnumerable<summernoteNews> Newsz { get; set; }
    }
}