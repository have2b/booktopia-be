using BusinessObject.DTO;
using DataAccess.Repository.Interface;
using static BusinessObject.Model.Order;

namespace BookStoreAPI.Services
{
    public class StatisticsService
    {
        private readonly IOrderRepository _repository;

        public StatisticsService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<TotalRevenueProfitSaleByDateRangeDTO> GetTotalRevenueProfitSaleByDateRange(DateTime startDate, DateTime endDate)
        {
            var orders = await _repository.GetOrdersByDateRange(startDate, endDate);
            var shippedOrders = orders.Where(x => x.Status == StatusType.Shipped).ToList();
            var revenue = shippedOrders.Sum(x => x.OrderDetails.Sum(y => y.Quantity * y.Book.SellPrice * (1 - (decimal)y.Discount / 100)));
            var profit = revenue - shippedOrders.Sum(x => x.OrderDetails.Sum(y => y.Quantity * y.Book.CostPrice));
            return new TotalRevenueProfitSaleByDateRangeDTO
            {
                NumberOfOrders = orders.Count,
                Revenue = revenue,
                Profit = profit
            };
        }

        public async Task<List<decimal>> GetRevenueOverviewByYear(int year)
        {
            var revenueOverview = new List<decimal>();
            var orders = await _repository.GetOrdersByYear(year);
            var shippedOrders = orders.Where(x => x.Status == StatusType.Shipped).ToList();
            for (int month = 1; month <= 12; month++)
            {
                var revenueByMonth = shippedOrders.Where(x => x.CreatedAt.Month == month).Sum(x => x.OrderDetails?.Sum(y => y.Quantity * y.Book.SellPrice * (1 - (decimal)y.Discount / 100)));
                revenueOverview.Add(revenueByMonth ?? 0);
            }
            return revenueOverview;
        }
        public async Task<List<RecentSales>> GetRecentSales(RequestDTO input)
        {
            var recentSales = new List<RecentSales>();
            var orders = await _repository.GetRecentOrders(input);
            foreach (var order in orders)
            {
                var revenue = order.OrderDetails?.Sum(y => y.Quantity * y.Book?.SellPrice * (1 - (decimal)y.Discount / 100)) ?? 0;
                recentSales.Add(new RecentSales
                {
                    Name = order.User?.Name,
                    PhoneNumber = order.User?.PhoneNumber,
                    SaleAmount = revenue,
                    Email = order.User?.Email,
                });
            }
            return recentSales;
        }
    }
}
