﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H5Forms.Dtos.Form.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace H5Forms.Dtos.Converters
{
    public class ControlConverter : JsonCreationConverter<Control>
    {
        protected override Control Create(Type objectType, JObject jObject)
        {
            var controlType = (ControlType)jObject["ControlType"].Value<int>();
           var result = default(Control);

           switch (controlType)
           {
               case ControlType.CheckList:
                   result = new CheckList();
                   break;
               case ControlType.DropDown:
                   result = new DropDown();
                   break;
               case ControlType.FreeText:
                   result = new FreeText();
                   break;
               case ControlType.OptionList:
                   result = new OptionList();
                   break;
               case ControlType.TextBox:
                   result = new TextBox();
                   break;
           }

            return result;
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
   
}