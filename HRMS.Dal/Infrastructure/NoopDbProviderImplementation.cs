using Microsoft.EntityFrameworkCore;

namespace HRMS.Dal
{
    internal class NoopDbProviderImplementation : IDbSpecificConfigurationProvider
    {
        public void ConfigureDatabaseDependentExtensions(ModelBuilder modelBuilder)
        {
            //Do Nothing
        }
    }
}
