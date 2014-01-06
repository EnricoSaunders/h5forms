namespace H5Forms.Dtos.Form.ValidationRules
{
    public class Email : RegexRule
    {
        public Email()
        {
            RegEx = @"(^$|^.*@.*\..*$)";
        }      
      
        public override string Message(Controls.ValueControl control)
        {
            return string.Format(Resource.EmailMessage, control.Label);
        }       
    }
}
