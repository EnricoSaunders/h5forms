using H5Forms.Dtos.Converters;
using Newtonsoft.Json;

namespace H5Forms.Dtos.Form.Controls
{   
    public  class Control 
    {
        public int Id { get; set; }
        public ControlType ControlType { get; set; }        
    }
}
