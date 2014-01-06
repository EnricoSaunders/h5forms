using System.Text.RegularExpressions;

namespace H5Forms.Dtos.Form.ValidationRules
{
    public class Number : RegexRule
    {
        public Number()
        {
            RegEx = @"^\d*\.?\d*$";            
        }
        
        public override string Message(Controls.ValueControl control)
        {
            return string.Format(Resource.NumberMessage, control.Label);
        }       
    }
}
