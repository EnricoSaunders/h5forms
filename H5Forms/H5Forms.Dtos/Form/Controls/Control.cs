namespace H5Forms.Dtos.Form.Controls
{   
    public  class Control 
    {
        public int Id { get; set; }
        public string ColumnName { get { return string.Format(FormSettings.COLUMN_PREFIX, Id); } }
        public ControlType ControlType { get; set; }        
    }
}
