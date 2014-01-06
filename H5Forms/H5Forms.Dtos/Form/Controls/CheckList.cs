using System.Collections.Generic;
using System.Linq;

namespace H5Forms.Dtos.Form.Controls
{
    public class CheckList : OptionsControl
    {
        public CheckList()
        {
            SelectedValues = new List<int>();
        }

        public IList<int> SelectedValues { get; set; }
        public OptionLayoutType OptionLayoutType { get; set; }

        public override void SetValue(string value)
        {
            Value = value;
            SelectedValues = new List<int>(value.Split(new[] {FormSettings.SELECTED_VALUES_SEPARATOR}).Select(v => int.Parse(v)).ToArray());
        }

        public override string GetFormattedValue()
        {
           var selectedValues = Options.Where(o => SelectedValues.Any(s => s == o.Id)).Select(o => o.Value);
            
           return  string.Join(FormSettings.SELECTED_VALUES_CLIENT_SEPARATOR.ToString(), selectedValues);
        }
    }
}
