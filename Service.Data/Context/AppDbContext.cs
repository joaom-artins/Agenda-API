using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Domain.Models.v1;
using System.Reflection.Emit;

namespace Service.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<AvailabilitySlotModel> AvailabilitySlots { get; set; } = default!;
    public DbSet<AppointmentModel> Appointments { get; set; } = default!;
    public override DbSet<UserModel> Users { get; set; } = default!;
    public DbSet<UserRefreshTokenModel> UserRefreshTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserModel>().ToTable("Users");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RolesClaim");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UsersRoles");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UsersLogins");   
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UsersClaims");

        builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });

        builder.Entity<AppointmentModel>()
            .HasOne(a => a.Client)  
            .WithMany()           
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}
