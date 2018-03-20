using System.Collections.Generic;
using CompleteMvcCmsProject1.Areas.PhotoSection.Models;

namespace CompleteMvcCmsProject1.Areas.PhotoSection.ViewModel
{
    public class ViewPhotoVM
    {
        public IEnumerable<PhotoUpload> PhotoList { get; set; }
    }
}