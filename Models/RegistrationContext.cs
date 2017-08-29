using Microsoft.EntityFrameworkCore;
 
namespace Registration.Models
{
    public class RegistrationContext : DbContext //QuotesContext should be named for something referring to the name of the whole project.

    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RegistrationContext(DbContextOptions<RegistrationContext> options) : base(options) { }  //sets up DB connection with models and refers to name of the context file

        public DbSet<User> Users { get; set; } // <User> refers to the model, Users refers to table
    }
}
	//Add a Db Set line for each Table name

