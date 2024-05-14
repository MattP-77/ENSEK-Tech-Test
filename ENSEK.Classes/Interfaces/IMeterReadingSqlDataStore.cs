using ENSEK.Classes.CompositeClasses;

namespace ENSEK.Classes.Interfaces
{
    public interface IMeterReadingSqlDataStore
    {        
        bool Update(List<MeterReadingComposite> instancesToSave);
    }
} 