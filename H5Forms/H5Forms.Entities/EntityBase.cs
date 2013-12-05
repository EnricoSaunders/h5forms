using System;

namespace H5Forms.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Enabled { get; set; }
    }
}
