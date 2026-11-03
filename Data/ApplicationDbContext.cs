using EnterpriseAiPlayground.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAiPlayground.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances =>
        Set<EmployeeLeaveBalance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeLeaveBalance>(entity =>
        {
            entity.ToTable("employee_leave_balances");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.EmployeeId)
                .HasColumnName("employee_id");

            entity.Property(x => x.AnnualAllowanceDays)
                .HasColumnName("annual_allowance_days");

            entity.Property(x => x.UsedDays)
                .HasColumnName("used_days");

            entity.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");
        });
    }
}