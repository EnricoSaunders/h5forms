namespace H5Forms.Dtos.Form.Controls
{   
    public abstract class Control 
    {
        public int Id { get; set; }
        public string ColumnName { get { return string.Format("{0}{1}",FormSettings.COLUMN_PREFIX, Id); } }
        public ControlType ControlType { get; set; }        
    }
}
