using System.Web.Mvc;
using H5Forms.Dtos.Form.ValidationRules;

namespace H5Forms.MvcWebApp.Models.Binders
{

    public class ValidationBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result;
            result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".ValidationType");

            var controlType = (ValidationType)result.ConvertTo(typeof(ValidationType));

            switch (controlType)
            {
                case ValidationType.Length:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Length));
                    break;
                case ValidationType.Required:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Required));
                    break;               
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }


   
}