#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentVisor.Model
{
    public class QueryBlobData
    {
        public int Id { get; set; }
        public uint BytesSize { get; set; }
        public byte[]? Data { get; set; }
    }
}
