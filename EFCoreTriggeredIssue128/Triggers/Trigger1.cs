using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreTriggeredIssue128.Triggers
{
    public class Trigger1 : IBeforeSaveTrigger<FooEntity>
    {
        private readonly ApplicationDbContext applicationDbContext;

        public Trigger1(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task BeforeSave(ITriggerContext<FooEntity> context, CancellationToken cancellationToken)
        {
            context.Entity.Trigger1DateTime = DateTime.UtcNow;

            await applicationDbContext.Foos.ToListAsync(); //Invoke something on the dbContext
        }
    }
}
