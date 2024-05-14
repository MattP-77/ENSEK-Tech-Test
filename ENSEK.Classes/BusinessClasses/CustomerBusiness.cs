using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.DataAccess;

namespace ENSEK.Classes.BusinessClasses
{
    public static class CustomerBusiness
    {
        #region Fields

        /// <summary>
        /// The dal.
        /// </summary>
        private static CustomerDal dal;

        #endregion
         
        #region Properties

        /// <summary>
        /// Gets Dal.
        /// </summary>
        private static CustomerDal Dal
        {
            get
            {
                return dal ?? (dal = new CustomerDal());
            }
        }

        #endregion

        #region Public Methods and Operators

        public static bool Update(List<CustomerComposite> instancesToSave)
        {
            return Dal.Update(instancesToSave);
        }

        /// <summary>
        /// Creates the log entry.
        /// </summary>
        /// <param name="entryLogData">The entry log data.</param>
        //public static void CreateLogEntry(MiCorporateTraceLogEntityComposite entryLogData)
        //{
        //    Dal.CreateLogEntry(entryLogData);
        //}

        #endregion
    }
}
