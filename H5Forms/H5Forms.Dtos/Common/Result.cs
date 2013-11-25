using System.Collections.Generic;

namespace H5Forms.Dtos.Common
{
    public class Result
    {
        public bool HasErrors { get; set; }

        public IList<string> Messages { get; set; }
    }
}
