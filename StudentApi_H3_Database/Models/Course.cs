namespace StudentApi_H3_Database.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ICollection<StudentCourse> StudentCourse { get; set; } = new List<StudentCourse>();
    }
}
