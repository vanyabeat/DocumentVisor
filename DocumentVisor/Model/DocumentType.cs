﻿namespace DocumentVisor.Model
{
    public class DocumentType : IDataField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
    }
}