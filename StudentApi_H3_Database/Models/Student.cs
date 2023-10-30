namespace StudentApi_H3_Database.Models
{
    public class Student
    {
        public int StudentId { get; set; } 
        public string StudentName { get; set; } 
        public int StudentAge { get; set; } 
        public int TeamId { get; set; } 
        public Team Team { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; } = new List<StudentCourse>();  
        
    }
}
