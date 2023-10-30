namespace StudentApi_H3_Database.DTO
{
    public class SaveTeamDTO
    {
        public string TeamName { get; set; }
        public int TeamDescribtion { get; set; }
    }

    public class UpdateTeamDTO : SaveTeamDTO
    {
        public int TeamId { get; set; }
    }

    public class TeamDTO : UpdateTeamDTO
    {
        public ICollection<StudentDTOMinusRelation> Students { get; set; }  
    }

    public class TeamDTOMinusRelation : UpdateTeamDTO
    {

    }
}
