using System;
using H5Forms.Dtos.Form.ValidationRules;
using Newtonsoft.Json.Linq;

namespace H5Forms.Dtos.Converters
{
    public class ValidationConverter : JsonCreationConverter<ValidationRule>
    {
        protected override ValidationRule Create(Type objectType, JObject jObject)
        {
            var validationType = (ValidationType)jObject["ValidationType"].Value<int>();
            var result = default(ValidationRule);

            switch (validationType)
            {
                case ValidationType.Length:
                    result = new Length();
                    break;
                case ValidationType.Required:
                    result = new Required();
                    break;
                case ValidationType.Email:
                    result = new Email();
                    break;
                case ValidationType.Number:
                    result = new Number();
                    break;
                case ValidationType.FormattedNumber:
                    result = new FormattedNumber();
                    break;
            }

            return result;
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
   
}
