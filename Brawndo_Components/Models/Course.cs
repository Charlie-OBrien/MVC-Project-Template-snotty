namespace Brawndo_Components.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
    }

    public class OnsiteCourse : Course
    {
        public string Location { get; set; } = string.Empty;
        public string Days { get; set; } = string.Empty;
        public DateTime Time { get; set; }
    }

    public class OnlineCourse : Course
    {
        public string URL { get; set; } = string.Empty;
    }
}
