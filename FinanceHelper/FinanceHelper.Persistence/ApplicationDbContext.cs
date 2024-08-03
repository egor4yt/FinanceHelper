using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<ExpenseItemType> ExpenseItemTypes { get; set; }
    public DbSet<FinancesDistributionItemValueType> FinancesDistributionItemValueTypes { get; set; }
    public DbSet<IncomeSourceType> IncomeSourceTypes { get; set; }
    public DbSet<MetadataLocalization> MetadataLocalizations { get; set; }
    public DbSet<MetadataType> MetadataTypes { get; set; }
    public DbSet<SupportedLanguage> SupportedLanguages { get; set; }
    public DbSet<TagType> TagTypes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<IncomeSource> IncomeSources { get; set; }
    public DbSet<ExpenseItem> ExpenseItems { get; set; }
    public DbSet<FinanceDistributionPlanItem> FinanceDistributionPlanItems { get; set; }
    public DbSet<FinanceDistributionPlan> FinanceDistributionPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}