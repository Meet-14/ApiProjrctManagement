namespace WebProjrctManagement.Model
{
    public class StudentTaskModel
    {
        public int? TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? AssignDate { get; set; }
        public int StudentProjectID { get; set; }
        public int? ProjectID { get; set; }
        public string? ProjectDefinition { get; set; }
        public int? StudentID { get; set; }
        public string? StudentName { get; set; }

    }
}
