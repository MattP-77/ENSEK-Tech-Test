using System.Runtime.Serialization;

namespace ENSEK.Classes.EntityClasses
{    
    public class CustomerEntity
    {
        public Guid Id { get; set; }

        public int? AccountId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
} 