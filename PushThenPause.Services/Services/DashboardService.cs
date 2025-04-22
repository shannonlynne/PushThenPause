using PushThenPause.Data;
using PushThenPause.Services.Dtos;
using PushThenPause.Services.Interfaces;

namespace PushThenPause.Services.Services
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

            //TODO: get info
            //await _context 
            //get user, get cycles, get those names of the break and user activities
            //display those along with streak count and nemo mode

            return dashboardDto;
        }
    }
}
