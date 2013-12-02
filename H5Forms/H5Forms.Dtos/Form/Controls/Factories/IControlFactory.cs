namespace H5Forms.Dtos.Form.Controls.Factories
{
    public  interface IControlFactory
    {
        Control CreateControl(ControlType controlType);
    }
}
