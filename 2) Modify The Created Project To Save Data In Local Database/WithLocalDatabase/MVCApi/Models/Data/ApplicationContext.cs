using Microsoft.EntityFrameworkCore;

namespace MVCApi.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
