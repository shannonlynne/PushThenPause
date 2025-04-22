using PushThenPause.Business.Dtos;
using PushThenPause.Business.Interfaces;
using PushThenPause.Data;

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

            //TODO: get info
            //await _context 
            //get user, get cycles, get those names of the break and user activities
            //display those along with streak count and nemo mode

            return dashboardDto;
        }

        public async Task<List<CycleDto>> AddCycle()
        {
            List<CycleDto> cycleDtos = new List<CycleDto>();

            //Add a cycle
            return cycleDtos;
        }

        //
    }
}
