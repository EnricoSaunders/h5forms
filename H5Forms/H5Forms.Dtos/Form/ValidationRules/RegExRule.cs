using System.Text.RegularExpressions;

namespace H5Forms.Dtos.Form.ValidationRules
{
    public abstract class RegexRule: ValidationRule
    {        
        public string RegEx { get; set; }

        public override bool IsValid(string value)
        {
            return Regex.IsMatch(value ?? string.Empty, RegEx);
        }
    }
}
