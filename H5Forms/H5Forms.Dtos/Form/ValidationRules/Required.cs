namespace H5Forms.Dtos.Form.ValidationRules
{
    public class Required: ValidationRule
    {
        public override bool IsValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
