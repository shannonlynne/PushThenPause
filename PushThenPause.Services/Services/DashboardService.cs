using Microsoft.EntityFrameworkCore;
using PushThenPause.Business.Dtos;
using PushThenPause.Business.Interfaces;
using PushThenPause.Data;
using PushThenPause.Data.Models;

namespace PushThenPause.Business.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly PushThenPauseDbContext _context;
        public DashboardService(PushThenPauseDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboard(int userId)
        {
            DashboardDto dashboardDto = new DashboardDto();

            User? user = await _context.Users.FindAsync(userId);
            if (user is not null)
                dashboardDto.DisplayName = user.DisplayName;

            List<Cycle> cycles = new List<Cycle>();
            cycles = await _context.Cycles
                .Where(c => c.UserId == userId)
                .ToListAsync();

            foreach (Cycle cycle in cycles)
            {
                BreakActivity? breakActivity = _context.BreakActivities
                    .FirstOrDefault(c => c.BreakActivityId == cycle.BreakActivityId);
                UserTask? userTask = _context.UserTasks
                    .FirstOrDefault(c => c.UserTaskId == cycle.UserTaskId);

                if (breakActivity is not null && userTask is not null)
                {
                    dashboardDto.CycleDtos.Add(
                        new CycleDto
                        {
                            BreakActivityDuration = breakActivity.DurationMinutes,
                            BreakActivityTitle = breakActivity.Title,
                            UserTaskDuration = userTask.DurationMinutes,
                            UserTaskTitle = userTask.Title
                        });
                }
            }

            StreakTracker? streakCount = await _context.StreakTrackers
                .FirstOrDefaultAsync(s => s.UserId == userId);
            if (streakCount is not null)
                dashboardDto.StreakCount = streakCount.StreakCount;

            NemsModeSettings? nemsModeSettings = await _context.NemsModeSettings
                .FirstOrDefaultAsync(n => n.UserId == userId);
            if (nemsModeSettings is not null)
                dashboardDto.NemsModEnabled = nemsModeSettings.IsEnabled;

            return dashboardDto;
        }
    }
}
