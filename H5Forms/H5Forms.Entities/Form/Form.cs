using System.Collections.Generic;
using H5Forms.Entities.Form.Controls;

namespace H5Forms.Entities.Form
{
    public class Form: EntityBase
    {        
        public virtual IList<Control> Controls { get; set; }
        public string JsonValue { get; set; }
        public virtual  User User { get; set; }
    }
}
