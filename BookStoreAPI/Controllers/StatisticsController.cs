using BookStoreAPI.Services;
using BusinessObject.DTO;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRole.Admin)]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _service;

        public StatisticsController(StatisticsService service)
        {
            _service = service;
        }
        [HttpGet("total-revenue-profit-sale")]
        public async Task<IActionResult> GetTotalRevenueAndProfitAndSaleByDateRangeAsync(
            [FromQuery(Name = "start-date")] DateTime startDate,
        [FromQuery(Name = "end-date")] DateTime endDate
            )
        {
            if (startDate <= endDate)
            {
                var result = await _service.GetTotalRevenueProfitSaleByDateRange(startDate, endDate);
                return Ok(new ResponseDTO<TotalRevenueProfitSaleByDateRangeDTO>() { Payload = result });
            }
            else
            {
                return BadRequest(new ResponseDTO<object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 400, Message = "Start date must not be greater than End Date!!!" }

                });
            }
        }

        [HttpGet("revenue-overview")]
        public async Task<IActionResult> GetOverviewRevenueAsync(
            [FromQuery(Name = "year")] int year
            )
        {

            if (year <= DateTime.Today.Year)
            {
                var result = await _service.GetRevenueOverviewByYear(year);
                return Ok(new ResponseDTO<List<decimal>>() { Payload = result });
            }
            else
            {
                return BadRequest(new ResponseDTO<object>()
                {
                    Success = false,
                    Payload = null,
                    Error = new ErrorDetails() { Code = 400, Message = "Don't have result for year in the future" }

                });
            }
        }

        [HttpGet("recent-sales")]
        public async Task<IActionResult> GetRecentSalesAsync(
            [FromQuery] RequestDTO input
            )
        {
            var result = await _service.GetRecentSales(input);
            return Ok(new ResponseDTO<List<RecentSales>>() { Payload = result });
        }
    }
}
