﻿using System;
using System.Collections.Generic;
using H5Forms.BusinessLogic;
using System.Web.Mvc;
using H5Forms.Dtos;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.Dtos.Form.Controls.Factories;
using H5Forms.Infrastructure;
using H5Forms.MvcWebApp.Models;
using Microsoft.AspNet.Identity;

namespace H5Forms.MvcWebApp.Controllers
{
    [Authorize]
    public class FormsAdminController : Controller
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

        #region Forms

        public ActionResult GetForms()
        {           
            var response = new Response<IList<BasicForm>> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _formAdmin.GetForms(User.Identity.GetUserName());
            }
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult GetForm(int formId)
        {
            var response = new Response<Form> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _formAdmin.GetForm(formId);
            }
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult CreateForm(Form form)
        {
            var response = new Response<int> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                form.User = new User { UserName = User.Identity.GetUserName() };
                _formAdmin.CreateForm(form);
            }
            catch (ValidationException exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(exception.Message);
            }
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

        [HttpPost]
        public ActionResult UpdateForm(Form form)
        {
            var response = new Response<int> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {                
                 _formAdmin.UpdateForm(form);
            }
            catch (ValidationException exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(exception.Message);
            }
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

        #endregion

        #region Controls
        public ActionResult GetTypes()
        {
            return this.JsonNet(
                        new
                        {
                            ControlTypes = _formAdmin.GetControlTypes(),
                            OptionLayoutTypes = _formAdmin.GetOptionLayoutTypes(),
                            LabelLayoutTypes = _formAdmin.GetLabelLayoutTypes()
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

        #region FormEntries
        
        [HttpPost]
        public ActionResult GetFormEntries(int formId)
        {
            var response = new Response<FormEntries> { Result = new Result() { HasErrors = false, Messages = new List<string>() } };

            try
            {
                response.Data = _formAdmin.GetFormEntries(formId);
            }
            catch (Exception)
            {
                response.Result.HasErrors = true;
                response.Result.Messages.Add(Resource.GeneralError);
            }

            return this.JsonNet(response);
        }

         [HttpPost]
        public ActionResult EntriesExcel(int formId)
        {
            var report = _formAdmin.EntriesExcel(formId);

            return new ExcelResult
            {
                ExcelPackage = report.ExcelPackage,
                FileName = string.Format(Resource.EntriesExcelFileName, report.FormTitle)
            };
        }        


        #endregion
    }
}
