using System;

namespace H5Forms.Entities.Form.ValidationRules
{
    public class Length: ValidationRule
    {
        public int? Max { get; set; }
        public int? Min { get; set; }
        public override bool IsValid(string value)
        {
            return (!Min.HasValue || (!string.IsNullOrEmpty(value) && value.Length >= Min.Value)) &&
                   (!Max.HasValue || (!string.IsNullOrEmpty(value) && value.Length <= Max.Value));
        }
    }
}
