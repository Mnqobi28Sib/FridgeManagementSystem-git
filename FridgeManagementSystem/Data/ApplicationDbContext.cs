using FridgeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<MaintenanceVisit> MaintenanceVisits { get; set; }
    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<FaultReport> FaultReports { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define primary keys
        modelBuilder.Entity<FaultReport>().HasKey(fr => fr.ReportId);
        modelBuilder.Entity<Technician>().HasKey(t => t.TechId);
        modelBuilder.Entity<Fridge>().HasKey(f => f.FridgeId);
        modelBuilder.Entity<MaintenanceVisit>().HasKey(mv => mv.Id);

        // Define relationships
        modelBuilder.Entity<Fridge>()
            .HasOne(f => f.Technician)
            .WithMany(t => t.Fridges)
            .HasForeignKey(f => f.TechId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        modelBuilder.Entity<MaintenanceVisit>()
            .HasOne(mv => mv.Fridge) // Assuming MaintenanceVisit has a FridgeId
            .WithMany(f => f.MaintenanceVisits) // Assuming Fridge has a collection of MaintenanceVisits
            .HasForeignKey(mv => mv.FridgeId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        modelBuilder.Entity<MaintenanceVisit>()
            .HasOne(mv => mv.Technician) // Assuming MaintenanceVisit has a TechId
            .WithMany(t => t.MaintenanceVisits) // Assuming Technician has a collection of MaintenanceVisits
            .HasForeignKey(mv => mv.TechId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

       
    }

}