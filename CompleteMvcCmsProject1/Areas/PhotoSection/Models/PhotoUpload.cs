using System;

namespace CompleteMvcCmsProject1.Areas.PhotoSection.Models
{
    public class PhotoUpload
    {
        public int Id { get; set; }
        public string PhotoName { get; set; }
        public string AdminName { set; get; }
        public DateTime ModifiedDate { set; get; }
        public string PhotoDetails { get; set; }
    }
}