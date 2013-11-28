namespace H5Forms.Dtos.Form.ValidationRules
{
    public abstract class ValidationRule
    {
        public ValidationType ValidationType { get; set; }
        public string Message { get; set; }
        public abstract bool IsValid(string value);
    }
}
