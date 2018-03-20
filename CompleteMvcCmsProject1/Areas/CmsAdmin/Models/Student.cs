namespace CompleteMvcCmsProject1.Areas.CmsAdmin.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MidName { set; get; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string QuillContent { get; set; }
        public int GenderId { get; set; }
        public int BloodId { get; set; }
    }
}