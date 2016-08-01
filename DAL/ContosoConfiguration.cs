#region using

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using Data.Framework;
using Data.Framework.Interceptor;
#endregion

namespace Data
{
    public class ContosoConfiguration : DbConfiguration
    {
        public ContosoConfiguration()
        {
            // Enable handling of transaction commit failures: https://msdn.microsoft.com/en-us/data/dn630221
            SetTransactionHandler("System.Data.SqlClient", () => new CommitFailureHandler());

            // Enable connection resiliency/retry logic (5 times in 30s by default): https://msdn.microsoft.com/en-us/data/dn456835
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(5, TimeSpan.FromSeconds(30)));
            SetExecutionStrategy("System.Data.SqlClient", () => new CustomExecutionStrategy(5, TimeSpan.FromSeconds(30)));           
            // Add Entity Framework Interceptors
            DbInterception.Add(new TransientErrorInterceptor());
            DbInterception.Add(new LoggingInterceptor());
        }
    }
}
