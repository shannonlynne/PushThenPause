using Microsoft.EntityFrameworkCore;
using PushThenPause.Data.Models;

namespace PushThenPause.Data;

public class PushThenPauseDbContext : DbContext
{
    public PushThenPauseDbContext(DbContextOptions<PushThenPauseDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserTask> Tasks => Set<UserTask>();
    public DbSet<TaskCategory> TaskCategories => Set<TaskCategory>();
    public DbSet<BreakActivity> BreakActivities => Set<BreakActivity>();
    public DbSet<Cycle> Cycles => Set<Cycle>();
    public DbSet<StreakTracker> StreakTrackers => Set<StreakTracker>();
    public DbSet<NemsModeSettings> NemsModeSettings => Set<NemsModeSettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.StreakTracker)
            .WithOne(s => s.User)
            .HasForeignKey<StreakTracker>(s => s.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.NemsModeSettings)
            .WithOne(n => n.User)
            .HasForeignKey<NemsModeSettings>(n => n.UserId);
    }
}
