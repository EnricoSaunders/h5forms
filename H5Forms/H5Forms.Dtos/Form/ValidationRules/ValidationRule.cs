using H5Forms.Dtos.Form.Controls;

namespace H5Forms.Dtos.Form.ValidationRules
{
    public abstract class ValidationRule
    {
        public ValidationType ValidationType { get; set; }
        public abstract string Message(ValueControl control);
        public abstract bool IsValid(string value);
    }
}
