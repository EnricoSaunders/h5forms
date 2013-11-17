namespace H5Forms.Entities.Form.ValidationRules
{
    public abstract class ValidationRule
    {        
        public string Message { get; set; }
        public abstract bool IsValid(string value);
    }
}
