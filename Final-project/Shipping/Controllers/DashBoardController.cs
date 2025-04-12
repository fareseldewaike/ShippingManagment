using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.services;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashBoardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("GetMerchantDashBorad")]
        public async Task<IActionResult> GetMerchantDashboard(string merchantId)
        {
            var dashboard = await _dashboardService.GetMerchantDashboardAsync(merchantId);
            if (dashboard == null)
            {
                return NotFound("Dashboard not found.");
            }
            return Ok(dashboard);
        }
        [HttpGet("GetRepresentativeDashBorad")]
        public async Task<IActionResult> GetRepresentativeDashboard(string representativeId)
        {
            var dashboard = await _dashboardService.GetRepresentativeDashboardAsync(representativeId);
            if (dashboard == null)
            {
                return NotFound("Dashboard not found.");
            }
            return Ok(dashboard);
        }
        [HttpGet("GetEmployeeDashBorad")]
        public async Task<IActionResult> GetEmployeeDashboard()
        {
            var dashboard = await _dashboardService.GetEmployeeDashboardAsync();
            if (dashboard == null)
            {
                return NotFound("Dashboard not found.");
            }
            return Ok(dashboard);
        }
    }
}
