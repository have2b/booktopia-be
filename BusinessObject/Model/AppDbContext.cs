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

        var roleOfAdmin = new IdentityRole(UserRole.Admin);
        roleOfAdmin.NormalizedName = UserRole.Admin.ToUpper();
        var roleOfUser = new IdentityRole(UserRole.User);
        roleOfUser.NormalizedName = UserRole.User.ToUpper();
        modelBuilder.Entity<IdentityRole>().HasData(
            roleOfAdmin,
            roleOfUser
            );

        var hasher = new PasswordHasher<IdentityUser>();
        var principalAdmin = new User
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            PasswordHash = hasher.HashPassword(null, "Admin123#"),
            Address = "",
            Name = "PrincipalAdmin",
            IsActive = true
        };
        modelBuilder.Entity<User>().HasData(principalAdmin);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = roleOfAdmin.Id,
                UserId = principalAdmin.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = roleOfUser.Id,
                UserId = principalAdmin.Id
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
    public DbSet<Publisher> Publishers { get; set; }
}