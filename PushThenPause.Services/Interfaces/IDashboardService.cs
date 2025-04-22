using PushThenPause.Business.Dtos;

namespace PushThenPause.Business.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboard(int userId);
    }
}
