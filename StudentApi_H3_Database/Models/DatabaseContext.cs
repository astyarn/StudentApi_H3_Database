using Microsoft.EntityFrameworkCore;

namespace StudentApi_H3_Database.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
