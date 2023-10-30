namespace StudentApi_H3_Database.DTO
{
    public class SaveCourseDTO
    {
        public string CourseName { get; set; }
    }

    public class UpdateCourseDTO : SaveCourseDTO
    {
        public int CourseId { get; set; }
    }

    public class CourseDTO : UpdateCourseDTO
    {
        public ICollection<StudentCourseDTOMinusCourse> StudentCourse { get; set; }
    }

    public class CourseDTOMinusRelation : UpdateCourseDTO
    {

    }
}
