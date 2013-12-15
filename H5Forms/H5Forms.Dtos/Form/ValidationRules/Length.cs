using H5Forms.Dtos.Form.Controls;

namespace H5Forms.Dtos.Form.ValidationRules
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
        public override string Message(ValueControl control)
        {
            var result = string.Empty;

            if (Min.HasValue && Max.HasValue)
                result = string.Format(Resource.LengthBetweenMessage, control.Label, Min.Value, Max.Value );
            else if (Min.HasValue && !Max.HasValue)
                result = string.Format(Resource.LengthFromMessage, control.Label, Min.Value);
            else if (!Min.HasValue && Max.HasValue)
                result = string.Format(Resource.LengthToMessage, control.Label, Max.Value);

            return result;
        }
    }
}
