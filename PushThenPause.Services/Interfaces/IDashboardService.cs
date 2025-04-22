using PushThenPause.Services.Dtos;

namespace PushThenPause.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboard(int userId);
    }
}
