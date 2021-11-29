using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTriggeredIssue128
{

    public class ApplicationDbContext : TriggeredDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FooEntity> Foos { get; set; }
    }
}
