using System;
using System.Collections.Generic;
using System.Linq;
using H5Forms.Dtos.Form.ValidationRules;

namespace H5Forms.Dtos.Form.Controls
{
    public abstract class ValueControl : Control
    {       
        public string Label { get; set; }
        public string HoverTitle { get; set; }
        public string Value { get; set; }
        public bool IsUnique { get; set; }
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
                    if (!vr.IsValid(Value)) result.Add(vr.Message(this));
                }

                return result;
            }
        }

        public abstract void SetValue(string value);

        public abstract string GetFormattedValue();

    }
}
