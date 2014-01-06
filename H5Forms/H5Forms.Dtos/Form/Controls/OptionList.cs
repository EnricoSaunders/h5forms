using System.Collections.Generic;
using System.Linq;

namespace H5Forms.Dtos.Form.Controls
{
    public class OptionList : OptionsControl
    {
        public OptionLayoutType OptionLayoutType { get; set; }
        public bool AllowOther { get; set; }
        public Option OtherOption { get; set; }
        public string OtherValue { get; set; }

        public override void SetValue(string value)
        {
            var values = value.Split(new[] { FormSettings.SELECTED_VALUES_SEPARATOR });
            Value = values[0];

            if (string.Equals(values[0], "-1"))
            {
                AllowOther = true;
                OtherValue = values[1];
            }
        }

        public override string GetFormattedValue()
        {
            var values = new List<string> {};

            if (!string.IsNullOrEmpty(Value) && !string.Equals(Value, "-1"))
            {
                var value = Options.Where(o => o.Id == int.Parse(Value)).Select(o => o.Value).SingleOrDefault();
                values.Add(value);
            }

            if (AllowOther && !string.IsNullOrEmpty(OtherValue))
                values.Add(OtherValue);

            return string.Join(FormSettings.SELECTED_VALUES_CLIENT_SEPARATOR.ToString(), values);
        }
    }
}
