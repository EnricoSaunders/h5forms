using System.Collections.Generic;

namespace H5Forms.Entities.Form
{
    public class Form: EntityBase
    {                
        public string Controls { get; set; }
        public virtual  User User { get; set; }
        public virtual IList<FormEntry> FormEntries { get; set; }
    }
}
