using Microsoft.EntityFrameworkCore;
using PushThenPause.Data.Models;

namespace PushThenPause.Data;

public class PushThenPauseDbContext : DbContext
{
    public PushThenPauseDbContext(DbContextOptions<PushThenPauseDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserTask> UserTasks => Set<UserTask>();
    public DbSet<BreakActivity> BreakActivities => Set<BreakActivity>();
    public DbSet<Cycle> Cycles => Set<Cycle>();
    public DbSet<StreakTracker> StreakTrackers => Set<StreakTracker>();
    public DbSet<NemsModeSettings> NemsModeSettings => Set<NemsModeSettings>();
}
