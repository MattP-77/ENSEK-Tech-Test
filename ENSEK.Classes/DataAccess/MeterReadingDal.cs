using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.DataStores.Sql;
using ENSEK.Classes.Interfaces;

namespace ENSEK.Classes.DataAccess
{
    public class MeterReadingDal
    {
        #region Fields

        /// <summary>
        /// The data store.
        /// </summary>
        private readonly IMeterReadingSqlDataStore dataStore;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingDal"/> class.
        /// </summary>
        public MeterReadingDal()
        {
            this.dataStore = new MeterReadingSqlDataStore();
        }

        #endregion

        #region Methods

        public bool Update(List<MeterReadingComposite> instancesToSave)
        {
            return this.dataStore.Update(instancesToSave);
        }

        #endregion
    }
}
