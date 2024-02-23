using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Model;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
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

        var RoleOfAdmin = new IdentityRole(UserRole.Admin);
        RoleOfAdmin.NormalizedName = UserRole.Admin.ToUpper();
        var RoleOfUser = new IdentityRole(UserRole.User);
        RoleOfUser.NormalizedName = UserRole.User.ToUpper();
        var RoleOfStaff = new IdentityRole(UserRole.Staff);
        RoleOfStaff.NormalizedName = UserRole.Staff.ToUpper();
        modelBuilder.Entity<IdentityRole>().HasData(
            RoleOfAdmin,
            RoleOfUser,
            RoleOfStaff
            );

        var hasher = new PasswordHasher<IdentityUser>();
        var PrincipalAdmin = new User
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            PasswordHash = hasher.HashPassword(null, "Admin123#"),
            Address = "",
            Name = "PrincipalAdmin",
            IsActive = true
        };
        modelBuilder.Entity<User>().HasData(PrincipalAdmin);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = RoleOfAdmin.Id,
                UserId = PrincipalAdmin.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = RoleOfStaff.Id,
                UserId = PrincipalAdmin.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = RoleOfUser.Id,
                UserId = PrincipalAdmin.Id
            }
            );
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Business", Description = "" },
            new Category { CategoryId = 2, CategoryName = "Personal Development", Description = "" },
            new Category { CategoryId = 3, CategoryName = "Manga-Comic", Description = "" },
            new Category { CategoryId = 4, CategoryName = "Psychology", Description = "" },
            new Category { CategoryId = 5, CategoryName = "Self-help", Description = "" }
            );
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
    public DbSet<GoodsReceiptDetail> GoodsReceiptDetails { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
}