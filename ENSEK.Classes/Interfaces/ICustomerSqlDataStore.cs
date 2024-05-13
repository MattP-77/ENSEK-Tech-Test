using ENSEK.Classes.CompositeClasses;

namespace ENSEK.Classes.Interfaces
{
    public interface ICustomerSqlDataStore
    {
        bool Update(List<CustomerComposite> instancesToSave);
    }
}
