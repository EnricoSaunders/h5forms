using System.Data.Entity;

namespace H5Forms.Entities.Interfaces
{
    public interface IH5FormsContext
    {
        IDbSet<H5Forms.Entities.Form.Form> Forms { get; }
        IDbSet<H5Forms.Entities.Form.FormEntry> FormEntries { get; }
        IDbSet<H5Forms.Entities.User> Users { get; }

        int SaveChanges();
    }
}
