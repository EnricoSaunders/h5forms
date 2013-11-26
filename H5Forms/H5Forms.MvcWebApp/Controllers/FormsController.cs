using System;
using System.Collections.Generic;
using H5Forms.BusinessLogic;
using System.Web.Mvc;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.Infrastructure;
using H5Forms.MvcWebApp.Models;

namespace H5Forms.MvcWebApp.Controllers
{
    public class FormsController : Controller
    {
        #region Properties
        private FormAdmin _formAdmin;      
        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _formAdmin = new FormAdmin();

            if (H5FormSession.Current.Form == null)
            {
                H5FormSession.Current.Form = _formAdmin.AddForm("Test");
            }
        }
        public ActionResult Index()
        {           
            return View();
        }
       
        [HttpGet]
        public ActionResult GetControlTypes()
        {
            return this.JsonNet(_formAdmin.GetControlTypes());
        }

        [HttpPost]
        public ActionResult AddControl(ControlType controlType)
        {
            var response = new Response<Control> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = H5FormSession.Current.Form.AddControl(controlType);
                _formAdmin.UpdateForm(H5FormSession.Current.Form);
            }           
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult DeleteControl(int controlId)
        {
            var response = new Response<int> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = H5FormSession.Current.Form.DeleteControl(controlId);
                _formAdmin.UpdateForm(H5FormSession.Current.Form);
            }
            catch (ValidationException ex)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(ex.Message);
            }
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }



    }
}
