namespace StudentApi_H3_Database.DTO
{
    public class SaveStudentDTO
    {
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public int TeamId { get; set; }
    }

    public class UpdateStudentDTO : SaveStudentDTO
    {
        public int StudentId { get; set; }
    }

    public class StudentDTO : UpdateStudentDTO 
    {
        public TeamDTOMinusRelation? Team { get; set; }

        public ICollection<StudentCourseDTOMinusStudent> StudentCourse { get; set; } = new List<StudentCourseDTOMinusStudent>();
    }

    public class StudentDTOMinusRelation : UpdateStudentDTO
    {

    }

    public class StudentDTOToTeam : UpdateStudentDTO
    {
        public TeamDTOMinusRelation? Team { get; set; }

    }
}
