using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentVisor.Model
{
    public class ExecutorRecord
    {
        public string Guid { get; set; }
        public int HasCd { get; set; }

        public string IdentifiersJson { get; set; }
        public string Info { get; set; }
        public int IsEmpty { get; set; }

        public int OutputDivisionId { get; set; }
        public string OutputNumber { get; set; }
        public long OutputNumberDate { get; set; }
        public string BlobData { get; set; }
    }
}
