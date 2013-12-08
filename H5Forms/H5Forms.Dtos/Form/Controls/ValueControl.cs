using System.Collections.Generic;
using System.Linq;
using H5Forms.Dtos.Converters;
using H5Forms.Dtos.Form.ValidationRules;
using Newtonsoft.Json;

namespace H5Forms.Dtos.Form.Controls
{
    public class ValueControl : Control
    {       
        public string Label { get; set; }
        public string Value { get; set; }
        //[JsonConverter(typeof(ValidationConverter))]
        public IList<ValidationRule> ValidationRules { get; set; }
        public bool IsValid()
        {
            return !BrokenRules.Any();
        }
        public IList<string> BrokenRules
        {
            get
            {
                var result = new List<string>();

                if (ValidationRules == null || ValidationRules.Count == 0) return result;

                foreach (var vr in ValidationRules)
                {
                    if (!vr.IsValid(Value)) result.Add(vr.Message);
                }

                return result;
            }
        }
    }
}
