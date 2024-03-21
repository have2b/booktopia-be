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
        modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderId, o.BookId });
        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(o => o.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(o => o.Book)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(o => o.BookId)
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
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
}