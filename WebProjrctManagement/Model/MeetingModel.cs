namespace WebProjrctManagement.Model
{
    public class MeetingModel
    {
        public int? MeetingID { get; set; }
        public int StudentID { get; set; }
        public string? StudentName { get; set; }
        public string? ImagePath { get; set; }
        public int FacultyID { get; set; }
        public string? FacultyName { get; set; }
        public int ProjectID { get; set; }
        public string? ProjectDefinition { get; set; }
        public DateTime? Date { get; set; }
        public string Discussion { get; set; }
        public string? Remark { get; set; }
    }
}
