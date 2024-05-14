using System.Runtime.Serialization;

namespace ENSEK.Classes.EntityClasses
{
    public class MeterReadingEntity
    {
        public Guid Id { get; set; }

        public int? AccountId { get; set; }

        public DateTime? MeterReadingDateTime { get; set; }

        public int? MeterReadValue { get; set; }

        public string? SubmittedValue { get; set; }

        public bool IsValid { get; set; }
    }
} 