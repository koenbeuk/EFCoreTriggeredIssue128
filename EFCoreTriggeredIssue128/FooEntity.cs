using System;

namespace EFCoreTriggeredIssue128
{
    public class FooEntity
    {
        public int Id { get; set; }

        public DateTime? Trigger1DateTime { get; set; }

        public DateTime? Trigger2DateTime { get; set; }
    }
}
