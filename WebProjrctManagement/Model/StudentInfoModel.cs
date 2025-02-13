namespace WebProjrctManagement.Model
{
    public class StudentInfoModel
    {
        public int ProjectID { get; set; }
        public string? ProjectDefinition { get; set; }
        public int StudentID { get; set; }
        public string? Email { get; set; }
        public string? StudentName { get; set; }    
        public string? ImagePath { get; set; }
        public int FacultyID { get; set; }
        public string? FacultyName { get; set; }
    }
}
