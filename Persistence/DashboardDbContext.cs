using Microsoft.EntityFrameworkCore;
using dashboard.Core.Models;

namespace dashboard.Persistence
{
    public class DashboardDbContext : DbContext
    {
         //Only add models that are to be queried or have no relations
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UpcomingEvent> UpcomingEvents { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamEnvironment> TeamEnvironments { get; set; }
        public DbSet<ClientGroup> ClientGroups { get; set; }
        
        public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
            :base(options)
        {
            
        }

        //Creates a composite primary key for VehicleFeature table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vehiclefeature => new {vehiclefeature.VehicleId, vehiclefeature.FeatureId});
        }

       

    }
}