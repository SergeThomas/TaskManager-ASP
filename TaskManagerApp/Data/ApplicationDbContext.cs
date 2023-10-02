using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Models;

namespace TaskManagerApp.Data
{
    // we need to inherit this class from DBContext that is in EntityFrameWorcCore
    public class ApplicationDbContext : DbContext       // Installed EFC-6 for using statement
    {   
        // we are saying that we will receive options in the constructor class and pass them to base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // create taskMan table with the name taskMans to load all columns from model
        public DbSet<TaskMan> TaskMans { get; set; } 
    }
}
