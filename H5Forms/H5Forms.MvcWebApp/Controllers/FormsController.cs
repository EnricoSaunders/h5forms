using System;
using System.Collections.Generic;
using H5Forms.BusinessLogic;
using System.Web.Mvc;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form.Controls;
using H5Forms.Dtos.Form.Controls.Factories;
using H5Forms.Infrastructure;

namespace H5Forms.MvcWebApp.Controllers
{
    public class FormsController : Controller
    {
        #region Properties
        private FormAdmin _formAdmin;
        private IControlFactory _controlFactory;      
        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _formAdmin = new FormAdmin();
            _controlFactory = new BasicControlFactory();            
        }
        public ActionResult Index()
        {           
            return View();
        }

        #region Controls
        public ActionResult GetTypes()
        {
            return this.JsonNet(
                        new
                        {
                            ControlTypes = _formAdmin.GetControlTypes(),
                            LayoutTypes = _formAdmin.GetLayoutTypes()
                        }
                );
        }

        [HttpPost]
        public ActionResult CreateControl(ControlType controlType)
        {
            var response = new Response<Control> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _controlFactory.CreateControl(controlType);                
            }           
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

      

        #endregion

       
    }
}
