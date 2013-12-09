using System;

namespace H5Forms.Dtos.Form
{
    public class BasicForm
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Enabled { get; set; }
        public string Title { get; set; }
    }
}
