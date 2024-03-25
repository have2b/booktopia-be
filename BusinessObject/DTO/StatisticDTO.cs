namespace BusinessObject.DTO
{

    public class TotalRevenueProfitSaleByDateRangeDTO
    {
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
        public int NumberOfOrders { get; set; }
    }

    public class RecentSales
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public decimal SaleAmount { get; set; }
    }
}
