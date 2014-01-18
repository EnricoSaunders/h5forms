using System.Collections.Generic;

namespace H5Forms.Entities
{   
    public class User 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IList<Form.Form> Forms { get; set; }
    }
}
