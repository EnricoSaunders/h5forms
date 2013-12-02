using System.Collections.Generic;
using System.Linq;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form.Controls;
using H5Forms.Dtos.Form.Controls.Factories;

namespace H5Forms.Dtos.Form
{
    public class Form
    {
        //private IControlFactory _controlFactory;

        //#region Constructor
        //public Form() : this(new BasicControlFactory())
        //{
            
        //}

        //public Form(IControlFactory controlFactory)
        //{
        //    _controlFactory = controlFactory;
        //}

        //#endregion

        public int Id { get; set; }
        public IList<Control> Controls { get; set; }
        public User User { get; set; }

        //#region Controls
        //public ControlCreateControl(ControlType controlType)
        //{
        //    if (Controls == null) Controls = new List<Control>();

        //    var id = Controls.Count == 0 ? 1 : Controls.Max(c => c.Id) + 1;
        //    Control control = _controlFactory.CreateControl(controlType);
        //    control.Id = id;
        //    Controls.Add(control);

        //    return control;
        //}       
        //public int DeleteControl(int controlId)
        //{
        //    var control = GetControl(controlId);
        //    Controls.Remove(control);

        //    return controlId;
        //}
        //public Control GetControl(int controlId)
        //{
        //    var control = Controls.SingleOrDefault(c => c.Id == controlId);

        //    if (control == null)
        //        throw new ValidationException(Resource.InvalidControl);

        //    return control;
        //}

        //#endregion

      
    }
}
