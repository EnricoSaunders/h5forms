using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using H5Forms.Dtos.Form;
using H5Forms.Entities.Interfaces;
using System.Data.Entity;

namespace H5Forms.EfRepository
{
    public class H5FormsContext : DbContext, IH5FormsContext
    {       
        public H5FormsContext()
            : base(ConfigurationManager.ConnectionStrings["H5Forms"].ConnectionString)
        {
          
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public IDbSet<Entities.Form.Form> Forms{ get; set;}        
        public IDbSet<Entities.User> Users { get; set; }

        public void CreateEntryModel(int formId, IList<string> columns)
        {
            var query = new StringBuilder();
            var tableName = string.Format("{0}{1}", FormSettings.TABLE_PREFIX, formId);

            query.Append(string.Format("Create table {0}(Id int primary key identity, FormId int ", tableName));

            foreach (var column in columns)
            {
                query.Append(string.Format(",{0} varchar(max)", column));
            }

            query.Append(")");

            Database.ExecuteSqlCommand(query.ToString());
        }

        public void UpdateEntryModel(int formId, IList<string> columns)
        {
            var query = new StringBuilder();
            var tableName = string.Format("{0}{1}", FormSettings.TABLE_PREFIX, formId);

            query.Append(string.Format("select column_name from information_schema.columns where table_name = '{0}'", tableName));

            var tableColumns = Database.SqlQuery<string>(query.ToString()).Where(c => !string.Equals(c, "Id") && !string.Equals(c, "FormId")).ToList();
            var columnsToAdd = columns.Where(c => !tableColumns.Any(t => string.Equals(c, t))).ToList();
            var columnsToDrop = tableColumns.Where(c => !columns.Any(t => string.Equals(c, t))).ToList();

            query.Clear();

            if (columnsToDrop.Count() > 0)
            {
                query.Append(string.Format("Alter table {0} drop column ", tableName));

                foreach (var column in columnsToDrop)
                {
                    query.Append(string.Format("{0}, ", column));
                }

                var cm = query.Remove(query.Length - 2, 1).ToString();
                Database.ExecuteSqlCommand(cm);
            }

            if (columnsToAdd.Count() > 0)
            {
                query.Append(string.Format("Alter table {0} add ", tableName));

                foreach (var column in columnsToAdd)
                {
                    query.Append(string.Format("{0} varchar(max), ", column));
                }

                var cm = query.Remove(query.Length - 2, 1).ToString();
                Database.ExecuteSqlCommand(cm);                
            }           
        }

        public void AddEntry(Entities.Form.FormEntry entry)
        {
            var query = new StringBuilder();
            var tableName = string.Format("{0}{1}", FormSettings.TABLE_PREFIX, entry.FormId);

            query.AppendLine(string.Format("Insert into {0}( FormId", tableName));

            foreach (var column in entry.ControlValues.Keys)
            {
                query.Append(string.Format(",{0}", column));
            }
           
            query.Append(")");
            query.AppendLine(string.Format("Values({0}", entry.FormId));

            foreach (var column in entry.ControlValues.Keys)
            {
                query.Append(string.Format(",'{0}'", entry.ControlValues[column]));
            }
            
            query.Append(")");

            Database.ExecuteSqlCommand(query.ToString());
        }
    }
}