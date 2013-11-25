using System.Collections.Generic;

namespace H5Forms.Entities
{
    public class User : EntityBase
    {
        public string Nick { get; set; }
        public IList<Form.Form> Forms { get; set; }
    }
}
