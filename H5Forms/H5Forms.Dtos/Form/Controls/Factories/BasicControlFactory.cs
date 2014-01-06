using System;
using System.Collections.Generic;
using H5Forms.Dtos.Form.ValidationRules;

namespace H5Forms.Dtos.Form.Controls.Factories
{
    public  class BasicControlFactory : IControlFactory
    {
        public  Control CreateControl(ControlType controlType)
        {
            var result = default(Control);

            switch (controlType)
            {               
                case ControlType.FreeText:
                    result = new FreeText
                    {
                        ControlType = ControlType.FreeText,
                        Text = Resource.FreeTextDefaultText,                                                
                        FontSize = 12,
                        Color = "#000000",
                        Strong = false                        
                    };
                    break;
                case ControlType.TextBox:
                    result = new TextBox
                    {
                        ControlType = ControlType.TextBox,
                        Label = Resource.TextBoxLabel,
                        Size = 200,
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                ValidationType = ValidationType.Required,
                                IsRequired = false                                                              
                            },
                            new ValidationRules.Length
                            {
                                ValidationType = ValidationType.Length
                            }
                        }
                    };
                    break;
                case ControlType.Number:
                    result = new Number
                    {
                        ControlType = ControlType.Number,
                        Label = Resource.NumberLabel,
                        Size = 200,
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                ValidationType = ValidationType.Required,
                                IsRequired = false                                                              
                            },
                            new ValidationRules.Length
                            {
                                ValidationType = ValidationType.Length
                            },
                            new ValidationRules.Number
                            {
                                ValidationType = ValidationType.Number
                            }
                        }
                    };
                    break;
                case ControlType.Email:
                    result = new Email
                    {
                        ControlType = ControlType.Number,
                        Label = Resource.EmailLabel,
                        Size = 200,
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                ValidationType = ValidationType.Required,
                                IsRequired = false                                                              
                            },
                            new ValidationRules.Length
                            {
                                ValidationType = ValidationType.Length
                            },
                            new ValidationRules.Email
                            {
                                ValidationType = ValidationType.Email
                            }
                        }
                    };
                    break;
                case ControlType.FormattedNumber:
                    result = new FormattedNumber
                    {
                        ControlType = ControlType.FormattedNumber,
                        Label = Resource.FormattedNumberLabel,   
                        Separator = FormSettings.DEFAULT_FORMAT_SEPARATOR,
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                ValidationType = ValidationType.Required,
                                IsRequired = false                                                              
                            },                           
                            new ValidationRules.FormattedNumber
                            {
                                ValidationType = ValidationType.FormattedNumber                                                                
                            }
                        }
                    };
                    break;
                case ControlType.DropDown:
                    result = new DropDown
                    {
                        ControlType = ControlType.DropDown,
                        Label = Resource.DropDownLabel,
                        Value = 0.ToString(),                        
                        EmptyOption = new Option{Id = -1, Value = string.Empty},
                        Options = new List<Option>
                        {                          
                           new Option{Id = 1, Value = Resource.FirstOption},
                           new Option{Id = 2, Value = Resource.SecondOption},
                           new Option{Id = 3, Value = Resource.ThirdOption}
                        },
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                IsRequired = false,                                
                                ValidationType = ValidationType.Required                                
                            }
                        }

                    };
                    break;
                case ControlType.CheckList:
                    result = new CheckList
                    {
                        ControlType = ControlType.CheckList,
                        Label = Resource.CheckListLabel,
                        OptionLayoutType = OptionLayoutType.OneColumn,
                        SelectedValues = new List<int>{},                        
                        Options = new List<Option>
                        {                            
                            new Option{Id = 1, Value = Resource.FirstOption},
                            new Option{Id = 2, Value = Resource.SecondOption},
                            new Option{Id = 3, Value = Resource.ThirdOption}
                        },
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                IsRequired = false,                                
                                ValidationType = ValidationType.Required                                
                            }
                        }

                    };
                    break;
                case ControlType.OptionList:
                    result = new OptionList
                    {
                        ControlType = ControlType.OptionList,
                        Label = Resource.OptionListLabel,
                        OptionLayoutType = OptionLayoutType.OneColumn,
                        AllowOther = false,
                        OtherValue = string.Empty,
                        OtherOption = new Option { Id = -1, Value = Resource.OtherValue },
                        Options = new List<Option>
                        {                            
                            new Option{Id = 1, Value = Resource.FirstOption},
                            new Option{Id = 2, Value = Resource.SecondOption},
                            new Option{Id = 3, Value = Resource.ThirdOption}
                        },
                        ValidationRules = new List<ValidationRule>
                        {
                            new ValidationRules.Required
                            {
                                IsRequired = false,                                
                                ValidationType = ValidationType.Required                                
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
    }
}
