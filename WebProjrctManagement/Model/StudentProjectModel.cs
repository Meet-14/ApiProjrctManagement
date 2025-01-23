namespace WebProjrctManagement.Model
{
    public class StudentProjectModel
    {
        public int? StudentProjectID { get; set; }
        public int ProjectID { get; set; }
        public string? ProjectDefinition { get; set; }
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public int FacultyID { get; set; }
        public string? FacultyName { get; set; }
        public string AcademicYear { get; set; }
        public DateTime StartingDate { get; set; }
        public int? MeetingsConducted { get; set; }
    }
}
