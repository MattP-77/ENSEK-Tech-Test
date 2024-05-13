using ENSEK.Classes.EntityClasses;

namespace ENSEK.Classes.CompositeClasses
{
    [Serializable]
    public class MeterReadingComposite : MeterReadingEntity
    {
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterReadingComposite"/> class.
        /// </summary>
        public MeterReadingComposite()
        {
        }

        #endregion

        #region Properties

        public bool IsValid { get; set; }

        #endregion
    }
}
