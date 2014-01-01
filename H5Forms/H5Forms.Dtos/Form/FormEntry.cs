using System;
using System.Collections.Generic;

namespace H5Forms.Dtos.Form
{
    public class FormEntry
    {        
        public int Id { get; set; }
        public int FormId { get; set; }
        public DateTime EntryDate { get; set; }
        public string Ip { get; set; }

        public Dictionary<string, string> ControlValues { get; set; }  
    }
}

