using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CompleteMvcCmsProject1.Areas.NewsPortal.Models;

namespace CompleteMvcCmsProject1.Areas.NewsPortal.Controllers
{
    public class PracticeController : Controller
    {
        // GET: NewsPortal/Practice
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult multiselect()
        {
            return View();
        }

        public ActionResult multiselectVue()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MultiSelectModel Posted)
        {
            MultiSelectModel VM = new MultiSelectModel();
            VM.Id = Posted.Id;
            return View();
        }

        public ActionResult VueDatatable()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ExcelTry()
        {
            TypeAheadTable source = new TypeAheadTable();
            source.states = "<ul><li>line 1</li><li>line 2</li></ul>";
            source.states = Regex.Replace(source.states, "<ul>", string.Empty);
            source.states = Regex.Replace(source.states, "</ul>", string.Empty);
            source.states = Regex.Replace(source.states, "<li>", string.Empty);
            source.states = Regex.Replace(source.states, "</li>", "\n");
            
            return View(source);
        }
        [HttpGet]
        public ActionResult download()
        {
            string source="<ul><li>line 1</li><li>line 2</li></ul>";           
            

            
            source = Regex.Replace(source, "<ul>", string.Empty);
            source = Regex.Replace(source, "</ul>", string.Empty);
            source = Regex.Replace(source, "<li>", string.Empty);
            source = Regex.Replace(source, "</li>", "CHAR(10)");
            return View();
           
            
        }

    }
}