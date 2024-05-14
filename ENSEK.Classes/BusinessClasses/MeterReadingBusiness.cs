using ENSEK.Classes.CompositeClasses;
using ENSEK.Classes.DataAccess;

namespace ENSEK.Classes.BusinessClasses
{
    public static class MeterReadingBusiness
    {
        #region Fields

        /// <summary>
        /// The dal.
        /// </summary>
        private static MeterReadingDal dal;

        #endregion

        #region Properties

        /// <summary>
        /// Gets Dal.
        /// </summary>
        private static MeterReadingDal Dal
        {
            get
            {
                return dal ?? (dal = new MeterReadingDal());
            }
        }

        #endregion

        #region Public Methods and Operators

        public static bool Update(List<MeterReadingComposite> instancesToSave)
        {
            return Dal.Update(instancesToSave);
        }

        #endregion
    }
}
 