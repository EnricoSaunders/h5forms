using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace H5Forms.Dtos.Form.Controls
{
    public class FormattedNumber: ValueControl
    {
        public char Separator { get; set; }
        public IList<Part> Parts
        {
            get
            {
                if (ValidationRules == null || !ValidationRules.Any()) return new List<Part>();

                var validationRule = ValidationRules.OfType<ValidationRules.FormattedNumber>().Single();
                var regex = validationRule.RegEx.Split(new[] {'|'})[0];
                var parts = regex.Split(new[] { Separator }).Select(p => new Part { Length = int.Parse(Regex.Replace(p, "[^0-9]", "")) }).ToList();

                if (string.IsNullOrEmpty(Value)) return parts;

                var values = Value.Split(new[] {Separator});

                for (int i = 0; i < values.Length; i++)
                {
                    parts[i].Value = values[i];
                }

                return parts;
            }
        } 
        public override void SetValue(string value)
        {
            if (value.ToCharArray().All(c => c == FormSettings.SELECTED_VALUES_SEPARATOR))
                Value = string.Empty;
            else
                Value = value.Replace(FormSettings.SELECTED_VALUES_SEPARATOR.ToString(), Separator.ToString());                                
        }

        public override string GetFormattedValue()
        {
            return Value;    
        }
    }

    public class Part
    {
        public int Length { get; set; }
        public string Value { get; set; }
    }
}
