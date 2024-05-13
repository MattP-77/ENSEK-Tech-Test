using System.Runtime.Serialization;

namespace ENSEK.Classes.EntityClasses
{
    [Serializable]
    public class CustomerEntity
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int? AccountId { get; set; }

        [DataMember]
        public string? FirstName { get; set; }

        [DataMember]
        public string? LastName { get; set; }
    }
}