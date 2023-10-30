namespace StudentApi_H3_Database.DTO
{
    public class SaveTeamDTO
    {
        public string TeamName { get; set; }
        public string TeamDescribtion { get; set; }
    }

    public class UpdateTeamDTO : SaveTeamDTO
    {
        public int TeamId { get; set; }
    }

    public class TeamDTO : UpdateTeamDTO
    {
        public ICollection<StudentDTOMinusRelation> Student { get; set; }  
    }

    public class TeamDTOMinusRelation : UpdateTeamDTO
    {

    }
}
