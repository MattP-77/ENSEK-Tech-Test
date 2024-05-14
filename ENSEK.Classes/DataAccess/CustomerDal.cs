using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.DataStores.Sql;
using ENSEK.Classes.Interfaces;

namespace ENSEK.Classes.DataAccess
{
    public class CustomerDal
    {
        #region Fields

        /// <summary>
        /// The data store.
        /// </summary>
        private readonly ICustomerSqlDataStore dataStore;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDal"/> class.
        /// </summary>
        public CustomerDal()
        {
            this.dataStore = new CustomerSqlDataStore();
        }

        #endregion
         
        #region Methods

        public bool Update(List<CustomerComposite> instancesToSave)
        {
            return this.dataStore.Update(instancesToSave);
        }

        //public void CreateLogEntry(MiCorporateTraceLogEntityComposite entryLogData)
        //{
        //    this.dataStore.CreateLogEntry(entryLogData);
        //}

        #endregion
    }
}
