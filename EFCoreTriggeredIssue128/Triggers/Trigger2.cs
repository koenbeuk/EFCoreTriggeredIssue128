using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreTriggeredIssue128.Triggers
{
    public class Trigger2 : IBeforeSaveTrigger<FooEntity>
    {
        private readonly ApplicationDbContext applicationDbContext;

        public Trigger2(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task BeforeSave(ITriggerContext<FooEntity> context, CancellationToken cancellationToken)
        {
            context.Entity.Trigger2DateTime = DateTime.UtcNow;

            await applicationDbContext.Foos.ToListAsync(); //Invoke something on the dbContext
        }
    }
}
