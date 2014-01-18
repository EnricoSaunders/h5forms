using System;
using System.Collections.Generic;

namespace H5Forms.Dtos.Form.ValidationRules
{
    public class FormattedNumber: RegexRule
    {
        public FormattedNumber()
        {
            RegEx = @"(^\d{2}-\d{3}-\d{4}$|^$|^---$)";
        }      

        public override string Message(Controls.ValueControl control)
        {
            return string.Format(Resource.NumberMessage, control.Label);
        }
    }
}
