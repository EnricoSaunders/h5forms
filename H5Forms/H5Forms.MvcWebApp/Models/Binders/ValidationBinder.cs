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
                case ValidationType.Email:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Email));
                    break;
                case ValidationType.Number:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Number));
                    break;
                case ValidationType.FormattedNumber:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(FormattedNumber));
                    break;   
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }


   
}