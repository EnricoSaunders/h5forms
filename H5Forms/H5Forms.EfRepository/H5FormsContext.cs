using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using H5Forms.Dtos.Form;
using H5Forms.Entities.Interfaces;
using System.Data.Entity;
using FormEntry = H5Forms.Entities.Form.FormEntry;

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

            query.Append(string.Format("Create table {0}(Id int primary key identity, FormId int, EntryDate datetime, Ip varchar(50) ", tableName));

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

            var tableColumns = Database.SqlQuery<string>(query.ToString()).Where(c => !new[] { "Id", "FormId", "EntryDate", "Ip" }.Any(r => string.Equals(c, r))).ToList();
            var columnsToAdd = columns.Where(c => !tableColumns.Any(t => string.Equals(c, t))).ToList();
            var columnsToDrop = tableColumns.Where(c => !columns.Any(t => string.Equals(c, t))).ToList();

            query.Clear();

            if (columnsToDrop.Any())
            {
                query.Append(string.Format("Alter table {0} drop column ", tableName));

                foreach (var column in columnsToDrop)
                {
                    query.Append(string.Format("{0},", column));
                }

                var cm = query.Remove(query.Length - 1, 1).ToString();
                Database.ExecuteSqlCommand(cm);
            }

            query.Clear();

            if (columnsToAdd.Any())
            {
                query.Append(string.Format("Alter table {0} add ", tableName));

                foreach (var column in columnsToAdd)
                {
                    query.Append(string.Format("{0} varchar(max),", column));
                }

                var cm = query.Remove(query.Length - 1, 1).ToString();
                Database.ExecuteSqlCommand(cm);                
            }           
        }

        public void AddEntry(Entities.Form.FormEntry entry)
        {
            var query = new StringBuilder();
            var tableName = string.Format("{0}{1}", FormSettings.TABLE_PREFIX, entry.FormId);

            query.AppendLine(string.Format("Insert into {0}( FormId, EntryDate, Ip", tableName));

            foreach (var column in entry.ControlValues.Keys)
            {
                query.Append(string.Format(",{0}", column));
            }
           
            query.Append(")");
            query.AppendLine(string.Format("Values({0},'{1}','{2}' ", entry.FormId, entry.EntryDate.ToString("yyyy/MM/dd hh:mm:ss"), entry.Ip));

            foreach (var column in entry.ControlValues.Keys)
            {
                query.Append(string.Format(",'{0}'", entry.ControlValues[column]));
            }
            
            query.Append(")");

            Database.ExecuteSqlCommand(query.ToString());
        }

        public bool IsUniqueEntry(int formId, string column, string value)
        {
            var otherEntry = GetEntryByValue(formId, column, value);

            return otherEntry == null;
        }

        public FormEntry GetEntryByValue(int formId, string column, string value)
        {
            var result = new List<FormEntry>();
            var query = new StringBuilder();
            var tableName = string.Format("{0}{1}", FormSettings.TABLE_PREFIX, formId);

            query.Append(string.Format("select column_name from information_schema.columns where table_name = '{0}'", tableName));
            var tableColumns = Database.SqlQuery<string>(query.ToString()).Where(c => !new[] { "Id", "FormId", "EntryDate", "Ip" }.Any(r => string.Equals(c, r))).ToList();
            query.Clear();
            query.Append(string.Format("Select Id, EntryDate, Ip"));

            foreach (var c in tableColumns)
            {
                query.Append(string.Format(", {0}", c));
            }

            query.Append(string.Format(" from {0} where {1} = '{2}'", tableName, column, value));

            try
            {
                Database.Connection.Open();

                var cm = Database.Connection.CreateCommand();

                cm.CommandType = CommandType.Text;
                cm.CommandText = query.ToString();

                var reader = cm.ExecuteReader();

                while (reader.Read())
                {
                    var entry = new FormEntry
                    {
                        Id = reader.GetInt32(0),
                        FormId = formId,
                        EntryDate = reader.GetDateTime(1),
                        Ip = reader.GetString(2),
                        ControlValues = tableColumns.ToDictionary(c => c, c => reader[c].ToString())
                    };

                    result.Add(entry);
                }

                Database.Connection.Close();
            }
            catch (Exception)
            {
                if (Database.Connection.State == ConnectionState.Open)
                    Database.Connection.Close();

                throw;
            }

            return result.Any() ? result[0] : null;
        }

        public IList<Entities.Form.FormEntry> GetEntries(int formId)
        {
            var result = new List<FormEntry>();
            var query = new StringBuilder();
            var tableName = string.Format("{0}{1}", FormSettings.TABLE_PREFIX, formId);

            query.Append(string.Format("select column_name from information_schema.columns where table_name = '{0}'", tableName));
            var tableColumns = Database.SqlQuery<string>(query.ToString()).Where(c => !new[] { "Id", "FormId", "EntryDate", "Ip" }.Any(r => string.Equals(c, r))).ToList();
            query.Clear();
            query.Append(string.Format("Select Id, EntryDate, Ip"));

            foreach (var column in tableColumns)
            {
                query.Append(string.Format(", {0}", column));
            }

            query.Append(string.Format(" from {0}", tableName));

            try
            {
                Database.Connection.Open();

                var cm = Database.Connection.CreateCommand();

                cm.CommandType = CommandType.Text;
                cm.CommandText = query.ToString();

                var reader = cm.ExecuteReader();

                while (reader.Read())
                {
                    var entry = new FormEntry
                    {
                        Id = reader.GetInt32(0),                        
                        FormId = formId,
                        EntryDate = reader.GetDateTime(1),
                        Ip = reader.GetString(2),
                        ControlValues = tableColumns.ToDictionary(c => c, c => reader[c].ToString())
                    };

                    result.Add(entry);
                }

                Database.Connection.Close();
            }
            catch (Exception)
            {
                if (Database.Connection.State == ConnectionState.Open)
                 Database.Connection.Close();

                throw;
            }                       

            return result;
        }
    }
}