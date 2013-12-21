using System;
using System.Collections.Generic;

namespace H5Forms.Entities.Form
{
    public class FormEntry : EntityBase
    {
        public int FormId { get; set; }
        public DateTime EntryDate { get; set; }
        public string Ip { get; set; }
        public Dictionary<string, string> ControlValues { get; set; }  
    }
}
