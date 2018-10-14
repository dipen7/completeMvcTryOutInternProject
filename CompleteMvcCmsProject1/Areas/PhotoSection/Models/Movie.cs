using System;
using System.ComponentModel.DataAnnotations;

namespace CompleteMvcCmsProject1.Areas.PhotoSection.Models
{
    public class Movie
    {
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}