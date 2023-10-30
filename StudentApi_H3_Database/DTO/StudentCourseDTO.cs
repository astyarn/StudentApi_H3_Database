namespace StudentApi_H3_Database.DTO
{
    public class SaveAndUpdateStudentCourseDTO
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }

    public class StudentCourseDTO : SaveAndUpdateStudentCourseDTO
    {
        public StudentDTOMinusRelation Student {  get; set; }   
        public CourseDTOMinusRelation Course {  get; set; }   
    }

    public class StudentCourseDTOMinusCourse : SaveAndUpdateStudentCourseDTO
    {
        public StudentDTOMinusRelation Student { get; set; }
    }

    public class StudentCourseDTOMinusStudent : SaveAndUpdateStudentCourseDTO
    {
        public CourseDTOMinusRelation Course { get; set; }  
    }
}
