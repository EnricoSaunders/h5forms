using System.Web.Mvc;
using H5Forms.Dtos.Form.Controls;

namespace H5Forms.MvcWebApp.Models.Binders
{
    public class ControlBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result;
            result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".controlType");            

            var controlType = (ControlType)result.ConvertTo(typeof(ControlType));

            switch (controlType)
            {
                  case ControlType.CheckList:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(CheckList));
                    break;
                  case ControlType.DropDown:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(DropDown));
                    break;
                  case ControlType.FreeText:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(FreeText));
                    break;
                  case ControlType.OptionList:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(OptionList));
                    break;
                  case ControlType.TextBox:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TextBox));
                    break;
                  case ControlType.Email:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Email));
                    break;
                  case ControlType.Number:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Number));
                    break;
                  case ControlType.FormattedNumber:
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(FormattedNumber));
                    break;
            }          

            return base.BindModel(controllerContext, bindingContext);
        }        
    }  
}