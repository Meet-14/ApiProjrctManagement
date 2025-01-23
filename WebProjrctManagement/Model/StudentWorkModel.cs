namespace WebProjrctManagement.Model
{
    public class StudentWorkModel
    {
        public int? StudentWorkID { get; set; }
        public int StudentID { get; set; }
        public string? StudentName { get; set; }

        public string FileHeading { get; set; }

        public string FilePath { get; set; }

        public DateTime? SubmittedDate { get; set; }
    }
}
