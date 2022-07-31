using Microsoft.EntityFrameworkCore;

namespace HRMS.Dal
{
    public interface IDbSpecificConfigurationProvider
    {
        void ConfigureDatabaseDependentExtensions(ModelBuilder modelBuilder);
    }
}
