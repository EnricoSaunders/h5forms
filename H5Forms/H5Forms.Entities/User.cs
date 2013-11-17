using System.Collections.Generic;

namespace H5Forms.Entities
{
    public class User : EntityBase
    {
        public IList<Form.Form> Forms { get; set; }
    }
}
