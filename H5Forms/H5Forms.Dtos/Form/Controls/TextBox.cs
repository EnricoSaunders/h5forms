namespace H5Forms.Dtos.Form.Controls
{
    public class TextBox: ValueControl
    {
        public int Size { get; set; }

        public override void SetValue(string value)
        {
            Value = value;
        }

        public override string GetFormattedValue()
        {
            return Value;
        }
    }
}
