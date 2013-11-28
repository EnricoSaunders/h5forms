using System;
using System.Collections.Generic;
using System.Linq;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form.Controls;
using H5Forms.Dtos.Form.ValidationRules;
using Newtonsoft.Json;

namespace H5Forms.Dtos.Form
{
    public class Form
    {
        public int Id { get; set; }
        public IList<Control> Controls { get; set; }
        public User User { get; set; }

        #region Controls
        public Control AddControl(ControlType controlType)
        {
            if (Controls == null) Controls = new List<Control>();

            var id = Controls.Count == 0 ? 1 : Controls.Max(c => c.Id) + 1;
            Control control = GetNewControl(controlType);
            control.Id = id;
            Controls.Add(control);

            return control;
        }

        private Control GetNewControl(ControlType controlType)
        {
            var result = default(Control);

            switch (controlType)
            {
                case ControlType.DropDown:
                    result = new DropDown
                    {
                        ControlType = ControlType.DropDown, 
                        Label = "Label", 
                        Options = new List<string>(),
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                IsRequired = false,
                                Message = Resource.RequiredDefaultMessage,
                                ValidationType = ValidationType.Required                                
                            }
                        }

                    };
                    break;
                case ControlType.FreeText:
                    result = new FreeText
                    {
                        ControlType = ControlType.FreeText,
                        Text = "Some text",
                        Properties = new Properties
                        {
                            FontSize = 12,
                            Color = "#000000",
                            Strong = false
                        }
                    };
                    break;
                case ControlType.TextBox:
                    result = new TextBox
                    {
                        ControlType = ControlType.TextBox, 
                        Label = "Label",
                        Size = 200,
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                ValidationType = ValidationType.Required,
                                IsRequired = false,
                                Message = Resource.RequiredDefaultMessage,                                                              
                            },
                            new ValidationRules.Length
                            {
                                ValidationType = ValidationType.Length,
                                Message = Resource.LengthDefaultMessage
                            }
                        }
                    };
                    break;
                default:
                    throw new Exception(Resource.InvalidControlType);
                    break;
            }

            return result;
        }

        public int DeleteControl(int controlId)
        {
            var control = GetControl(controlId);
            Controls.Remove(control);

            return controlId;
        }

        public Control GetControl(int controlId)
        {
            var control = Controls.SingleOrDefault(c => c.Id == controlId);

            if (control == null)
                throw new ValidationException(Resource.InvalidControl);

            return control;
        }

        #endregion

        //#region ValidationRules
        //public ValidationRules.ValidationRule AddValidationRule(int controlId, ValidationRules.ValidationType validationType)
        //{
        //    var result = default(ValidationRule);
        //    var control = (ValueControl)GetControl(controlId);

        //    switch (validationType)
        //    {
        //        case ValidationType.Required:
        //            result = new H5Forms.Dtos.Form.ValidationRules.Required
        //            {
        //                ValidationType = ValidationType.Required,
        //                Message = Resource.RequiredDefaultMessage
        //            };
        //            break;
        //        case ValidationType.Length:
        //            result = new H5Forms.Dtos.Form.ValidationRules.Required
        //            {
        //                ValidationType = ValidationType.Required,
        //                Message = Resource.LengthDefaultMessage
        //            };
        //            break;               
        //        default:
        //            throw new Exception("Validation type doesn't exist");
        //            break;
        //    }


        //    if (control.ValidationRules == null)
        //        control.ValidationRules = new List<ValidationRule>();

        //    control.ValidationRules.Add(result);

        //    return result;
        //}

        //public int DeleteValidationRule(int controlId, ValidationRules.ValidationType validationType)
        //{
        //    var control = (ValueControl)GetControl(controlId);
        //    var validationRule = control.ValidationRules.Single(vr => vr.ValidationType == validationType);

        //    control.ValidationRules.Remove(validationRule);

        //    return controlId;
        //}

        //#endregion
    }
}
