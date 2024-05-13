using System.Runtime.Serialization;

namespace ENSEK.Classes.EntityClasses
{
    [Serializable]
    public class MeterReadingEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int? AccountId { get; set; }

        [DataMember]
        public DateTime? MeterReadingDateTime { get; set; }

        [DataMember]
        public int? MeterReadValue { get; set; }
    }
}