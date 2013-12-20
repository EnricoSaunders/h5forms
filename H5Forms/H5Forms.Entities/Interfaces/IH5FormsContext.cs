using System.Collections.Generic;
using System.Data.Entity;
using H5Forms.Entities.Form;

namespace H5Forms.Entities.Interfaces
{
    public interface IH5FormsContext
    {
        void CreateEntryModel(int formId, IList<string> columns);
        void UpdateEntryModel(int formId, IList<string> columns);
        void AddEntry(FormEntry entry);
        IList<FormEntry> GetEntries(int formId);
        IDbSet<H5Forms.Entities.Form.Form> Forms { get; }        
        IDbSet<H5Forms.Entities.User> Users { get; }
        Database Database { get; }    

        int SaveChanges();       
    }
}
