using System;
using System.Collections.Generic;
using System.Linq;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form.Controls;
using Newtonsoft.Json;

namespace H5Forms.Dtos.Form
{
    public class Form
    {
        public int Id { get; set; }
        public IList<Control> Controls { get; set; }
        public User User { get; set; }

        public Control AddControl(ControlType controlType)
        {
            if (Controls == null) Controls = new List<Control>();

            var id = Controls.Count == 0 ? 1 : Controls.Max(c => c.Id) + 1;
            Control control = GetNew(controlType);
            control.Id = id;
            Controls.Add(control);

            return control;
        }

        private Control GetNew(ControlType controlType)
        {
            var result = default(Control);

            switch (controlType)
            {
                case ControlType.DropDown:
                    result = new DropDown{ControlType = ControlType.DropDown, Label = "Label", Options = new List<string>()};
                    break;
                case ControlType.Label:
                    result = new Label { ControlType = ControlType.Label, Text = "Label"};
                    break;
                case ControlType.TextBox:
                    result = new TextBox { ControlType = ControlType.TextBox, Label = "Label" };
                    break;
                default:
                    throw new Exception("Control type doesn't exists");
                    break;
            }

            return result;
        }

        public int DeleteControl(int controlId)
        {
            var control = Controls.SingleOrDefault(c => c.Id == controlId);

            if (control == null)
                throw new ValidationException(Resource.InvalidControl);

            Controls.Remove(control);

            return controlId;
        }
    }
}
