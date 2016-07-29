using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace Data
{
    public class ContosoConfiguration : DbConfiguration
    {
        public ContosoConfiguration()
        {
            SetTransactionHandler("System.Data.SqlClient", () => new CommitFailureHandler());
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
