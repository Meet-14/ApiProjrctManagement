namespace WebProjrctManagement.Model
{
    public class MeetingCountByFaculty
    {
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        public int MeetingCount { get; set; }
    }
    public class StatisticsModel
    {
        public List<MeetingModel> MeetingDetails { get; set; }

        public List<MeetingCountByFaculty> MeetingCount { get; set; }
    }

}
