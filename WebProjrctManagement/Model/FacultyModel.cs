namespace WebProjrctManagement.Model
{
    public class FacultyModel
    {
        public int? FacultyID { get; set; }
        public string FacultyName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string? Password { get; set; }
    }

    public class FacultyDropDownModel
    {
        public int? FacultyID { get; set; }
        public string FacultyName { get; set; }
    }
}
