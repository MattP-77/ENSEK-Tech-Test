using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Classes.ResponseClasses
{
    public class ResultReponseEntity
    {
        public int SuccessfulReadCount { get; set; }

        public int FailedReadCount { get; set; }

        public int TotalReadCount { get; set; }

        public string MessageText { get; set; }
    }
}
