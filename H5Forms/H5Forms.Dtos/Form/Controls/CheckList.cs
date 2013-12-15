using System.Collections.Generic;

namespace H5Forms.Dtos.Form.Controls
{
    public class CheckList : OptionsControl
    {
        public IList<int> SelectedValues { get; set; }

        public OptionLayoutType OptionLayoutType { get; set; }
    }
}
