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
                var control = Controls.Single(c => c.Id == controlId);

                switch (control.ControlType)
                {
                    case ControlType.CheckList:
                        ((CheckList) control).Value = column.Value;
                        ((CheckList) control).SelectedValues = new List<int>(column.Value.Split(new[] {FormSettings.SELECTED_VALUES_SEPARATOR}).Select(v => int.Parse(v)).ToArray());
                        break;
                    case ControlType.OptionList:
                        var values = column.Value.Split(new[] {FormSettings.SELECTED_VALUES_SEPARATOR});
                        ((OptionList) control).Value = values[0];

                        if (string.Equals(values[0], "-1"))
                        {
                            ((OptionList) control).AllowOther = true;
                            ((OptionList) control).OtherValue = values[1];
                        }

                        break;
                    case ControlType.DropDown: case ControlType.TextBox:
                         ((ValueControl) control).Value = column.Value;
                        break;
                }              
            }
        }
        public IList<string> Validate()
        {
            var result = new List<string>();

            foreach (var control in Controls.OfType<ValueControl>().Select(c => (ValueControl)c))
            {
                result.AddRange(control.BrokenRules);
            }

            return result;
        }
    }
}

