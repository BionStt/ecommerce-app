namespace Ecommerce.Server.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Bleu of Chanel (Parfum)",
                Description = "Good smelling parfume for men.",
                ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcQPAExovK60WIUHAo73G4vCbiD7YZD3IayaadFzKqQ1cG3NtScEyEXOMZb38vtbqWiFbaeZoSIp0ac&usqp=CAc",
                Price = 9.99m
            },
            new Product
            {
                Id = 2,
                Title = "No Country for Old Men (Film)",
                Description = "No Country for Old Men is a 2007 American neo-Western crime thriller film written and directed by Joel and Ethan Coen, based on Cormac McCarthy's 2005 novel of the same name.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/8/8b/No_Country_for_Old_Men_poster.jpg",
                Price = 4.99m
            },
            new Product
            {
                Id = 3,
                Title = "1984 (Book)",
                Description = "Nineteen Eighty-Four (also stylised as 1984) is a dystopian social science fiction novel and cautionary tale written by the English writer George Orwell",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/1984first.jpg/220px-1984first.jpg",
                Price = 19.99m
            }
	    );
	}

	public DbSet<Product> Products { get; set; }
}
