using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Models
{
    public class Mydbcontext : DbContext
    {
        public Mydbcontext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Attendance> attendances { get; set; }
        //public DbSet<Studen> StudentRegisters { get; set; }






    }
}
