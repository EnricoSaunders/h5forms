using System.Collections.Generic;

namespace H5Forms.Entities.Form.Controls
{
    public abstract class OptionsControls : ValueControl
    {
        public IList<string> Options { get; set; }
    }
}
