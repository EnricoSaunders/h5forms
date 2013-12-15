namespace H5Forms.Dtos.Form.Controls
{
    public class OptionList : OptionsControl
    {
        public OptionLayoutType OptionLayoutType { get; set; }
        public bool AllowOther { get; set; }
        public Option OtherOption { get; set; }
        public string OtherValue { get; set; }
    }
}
