using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using H5Forms.Dtos.Form;

namespace H5Forms.MvcWebApp.Models.Binders
{
    public class FormEntryBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {                                           
            var form = controllerContext.HttpContext.Request.Form;

            var result = new FormEntry
            {
                ControlValues = new Dictionary<string, string>()
            };

            var aux = form.Get(FormSettings.FORM_ID_NAME);
            int formId;
            var validFormId = int.TryParse(aux, out formId);

            if (validFormId)
            {
                result.FormId = formId;

                foreach (var key in form.AllKeys.Where(k => k.StartsWith(FormSettings.COLUMN_PREFIX)))
                {
                    var values = form.GetValues(key).Select(v => v.Replace(FormSettings.SELECTED_VALUES_SEPARATOR.ToString(), string.Empty));
                    var value = string.Join(FormSettings.SELECTED_VALUES_SEPARATOR.ToString(), values) ;

                    result.ControlValues.Add(key, value);
                }
            }

            return result;
        }
    }

  
}