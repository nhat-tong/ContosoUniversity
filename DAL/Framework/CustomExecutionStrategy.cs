using System;
using System.Data.Entity.Infrastructure;

namespace Data.Framework
{
    public class CustomExecutionStrategy : DbExecutionStrategy
    {
        public CustomExecutionStrategy(int maxRetryOn, TimeSpan maxDelay) : base(maxRetryOn, maxDelay)
        {            
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            // Determine retry shoud be run or not
            if (exception is MyException) return true;
            return false;
        }
    }
}
