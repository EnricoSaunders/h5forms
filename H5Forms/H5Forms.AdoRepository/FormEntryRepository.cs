using System;
using System.Collections.Generic;
using H5Forms.Entities.Interfaces;

namespace H5Forms.AdoRepository
{
    public class FormEntryRepository: IFormEntryRepository
    {
        public void CreateModel(int formId, int userId, IList<string> columns)
        {
            throw new NotImplementedException();
        }

        public void UpdateModel(int formId, int userId, IList<string> columns)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(Entities.Form.FormEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
