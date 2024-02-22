using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Model;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderId, o.ProductId });

        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(o => o.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Product)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(o => o.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GoodsReceiptDetail>().HasKey(g => new { g.GoodsReceiptId, g.ProductId });

        modelBuilder.Entity<GoodsReceiptDetail>()
            .HasOne(g => g.GoodsReceipt)
            .WithMany(g => g.GoodsReceiptDetails)
            .HasForeignKey(g => g.GoodsReceiptId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<GoodsReceiptDetail>()
            .HasOne(g => g.Product)
            .WithMany(g => g.GoodsReceiptDetails)
            .HasForeignKey(g => g.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
    public DbSet<GoodsReceiptDetail> GoodsReceiptDetails { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
}