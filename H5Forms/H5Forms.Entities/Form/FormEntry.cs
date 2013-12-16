using System.Collections.Generic;

namespace H5Forms.Entities.Form
{
    public class FormEntry : EntityBase
    {
        public int FormId { get; set; }

        public Dictionary<string, string> ControlValues { get; set; }  
    }
}
