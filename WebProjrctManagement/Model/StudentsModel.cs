namespace WebProjectManagement.Model
{
    public class StudentsModel
    {
        public int? StudentID { get; set; }
        public string StudentName { get; set; }
        public string Enr_No { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string? Password { get; set; }
    }

    public class StudentDropDownModel
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
    }
}
