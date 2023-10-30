namespace StudentApi_H3_Database.Models
{
    public class Team
    {
        public int TeamId { get; set; } 
        public int TeamName { get; set; } 
        public int TeamDescribtion { get; set; } 
        public ICollection<Student> Student { get; set; } = new List<Student>();
    }
}
