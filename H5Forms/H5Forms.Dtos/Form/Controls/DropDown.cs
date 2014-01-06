using System.Linq;

namespace H5Forms.Dtos.Form.Controls
{
    public class DropDown : OptionsControl
    {     
        public Option EmptyOption { get; set; }

        public override void SetValue(string value)
        {
            Value = value;
        }

        public override string GetFormattedValue()
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;

            return Options.Where(o => o.Id == int.Parse(Value)).Select(o => o.Value).Single();
        }
    }
}
