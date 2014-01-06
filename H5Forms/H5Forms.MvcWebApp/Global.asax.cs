using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using H5Forms.BusinessLogic;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.Dtos.Form.ValidationRules;
using H5Forms.Infrastructure;
using H5Forms.MvcWebApp.Models.Binders;

namespace H5Forms.MvcWebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Custom Model Binders

            ModelBinders.Binders.Add(new KeyValuePair<Type, IModelBinder>(typeof(Control), new ControlBinder()));
            ModelBinders.Binders.Add(new KeyValuePair<Type, IModelBinder>(typeof(ValidationRule), new ValidationBinder()));
            ModelBinders.Binders.Add(new KeyValuePair<Type, IModelBinder>(typeof(FormEntry), new FormEntryBinder()));           

            #endregion

            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonNetValueProviderFactory());

            BootStrapper.BootStrap();           
        }
    }
}