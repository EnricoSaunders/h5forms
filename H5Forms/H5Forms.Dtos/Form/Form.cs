using System;
using System.Collections.Generic;
using System.Linq;
using H5Forms.Dtos.Form.Controls;

namespace H5Forms.Dtos.Form
{
    public class Form
    {        
        public int Id { get; set; }        
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Enabled { get; set; }
        public string Title { get; set; }
        public LabelLayoutType LabelLayoutType { get; set; }        
        public IList<Control> Controls { get; set; }
        public User User { get; set; }
        public void SetValues(FormEntry formEntry)
        {
            foreach (var column in formEntry.ControlValues)
            {
                var controlId = int.Parse(column.Key.Replace(FormSettings.COLUMN_PREFIX,string.Empty));
                var control = Controls.Single(c => c.Id == controlId) as ValueControl;                

                control.SetValue(column.Value);               
            }
        }
        public IList<string> BasicValidation()
        {
            var result = new List<string>();

            foreach (var control in Controls.OfType<ValueControl>().Select(c => c))
            {
                result.AddRange(control.BrokenRules);
            }            

            return result;
        }

        public IList<string> UniqueValidation(Func<int, string, string, bool> isUnique)
        {
            var result = new List<string>();

            foreach (var control in Controls.OfType<ValueControl>().Where(c => c.IsUnique).Select(c => c))
            {
                if (!isUnique(Id, control.ColumnName, control.Value))
                    result.Add(string.Format(Resource.UniqueMessage, control.Label));
            }

            return result;
        }
    }
}

