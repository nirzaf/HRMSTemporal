using HRMS.Dal.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Dal.Migrations.MsSql
{
    internal class MsSqlConfigurationProvider : IDbSpecificConfigurationProvider
    {
        public void ConfigureDatabaseDependentExtensions(ModelBuilder modelBuilder)
        {
            /**********Step 1. Basic Configuration************/
            //modelBuilder.Entity<User>().ToTable("Users", b => b.IsTemporal());

            /**********Step 2. Customizing Configuration************/
            modelBuilder.Entity<User>().ToTable("Users", b => b.IsTemporal(builder =>
            {
                builder.HasPeriodStart("PeriodStart");
                builder.HasPeriodEnd("PeriodEnd");
                builder.UseHistoryTable("UserChanges");
            }));

        }
    }
}
