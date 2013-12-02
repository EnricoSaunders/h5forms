using System.Collections.Generic;

namespace H5Forms.Dtos.Form.Controls
{
    public abstract class OptionsControl : ValueControl
    {
        public IList<Option> Options { get; set; }

        public Option SelectedOption { get; set; }
    }
}
