using System.Collections.Generic;

namespace H5Forms.Dtos.Form
{
    public class FormEntries
    {               
        public Dictionary<string, string> Columns { get; set; }

        public IList<Dictionary<string, string>> Entries { get; set; }
    }
}

