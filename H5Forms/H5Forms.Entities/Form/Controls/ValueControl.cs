using System.Collections.Generic;
using System.Linq;
using H5Forms.Entities.Form.ValidationRules;

namespace H5Forms.Entities.Form.Controls
{
    public class ValueControl : Control
    {       
        public Label Label { get; set; }
        public string Value { get; set; }
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
