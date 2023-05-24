using Microsoft.EntityFrameworkCore;

public class PersonasDbContext : DbContext
{
    public PersonasDbContext(DbContextOptions<PersonasDbContext> options)
        : base(options)
    {
      
    }

    public DbSet<Persona>? Personas { get; set; }
}
