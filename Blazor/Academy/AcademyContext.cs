using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AcademyContext : DbContext
{
    public AcademyContext(DbContextOptions<AcademyContext> options) : base(options)
    {
    }
    public DbSet<Academy.Models.Direction> Directions { get; set; } = default!;
    public DbSet<Academy.Models.Group> Groups { get; set; } = default!;
    public DbSet<Academy.Models.Student> Students { get; set; } = default!;
    public DbSet<Academy.Models.Teacher> Teachers { get; set; } = default!;
    public DbSet<Academy.Models.Discipline> Disciplines { get; set; } = default!;
}
