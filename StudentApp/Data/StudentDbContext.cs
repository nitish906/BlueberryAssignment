using Microsoft.EntityFrameworkCore;
using StudentApp.Models.Entites;

namespace StudentApp.Data
{
    public class StudentDbContext : DbContext
    {

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : 
            base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
