using CompleteMvcCmsProject1.Areas.CmsAdmin.Models;
using System.Collections.Generic;

namespace CompleteMvcCmsProject1.Areas.CmsAdmin.ViewModel
{
    public class StudentPageVM
    {
        public IEnumerable<Student> OurCustomers { get; set; }
    }
}