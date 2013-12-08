﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using H5Forms.Dtos.Converters;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.EfRepository;
using H5Forms.Entities.Interfaces;
using System;
using Newtonsoft.Json;

namespace H5Forms.BusinessLogic
{
    public class FormAdmin
    {
        private IH5FormsContext _h5FormsContext;

        #region Contructor

        public FormAdmin() : this(new H5FormsContext())
        {
            
        }
        public FormAdmin(IH5FormsContext h5FormsContext)
        {
            _h5FormsContext = h5FormsContext;
         
        }

        #endregion

        public IEnumerable<string> GetControlTypes()
        {
            return Enum.GetNames(typeof (ControlType));
        }

        public IEnumerable<string> GetLayoutTypes()
        {
            return Enum.GetNames(typeof(LayoutType));
        }      

      
        public IList<BasicForm> GetForms(string user)
        {
            var forms = _h5FormsContext.Forms.Where(f => string.Equals(f.User.Nick, user)).ToList();

            return Mapper.Map<IList<Entities.Form.Form>, IList<BasicForm>>(forms);
        }

        public Form GetForm(int formId)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formId);
            var controls = JsonConvert.DeserializeObject<List<Control>>(form.Controls, new JsonConverter[] { new ControlConverter(), new ValidationConverter()});

            return Mapper.Map<Entities.Form.Form, Form>(form);
        }

        public void CreateForm(Form formDto)
        {
            var form = new Entities.Form.Form
            {
                User = _h5FormsContext.Users.Single(u => string.Equals(u.Nick, formDto.User.Nick)),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Enabled = formDto.Enabled,
                Title = formDto.Title,
                Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls                                
            };

            _h5FormsContext.Forms.Add(form);

            _h5FormsContext.SaveChanges();            
        }

        public void UpdateForm(Form formDto)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formDto.Id);

            form.Title = formDto.Title;
            form.UpdateDate = DateTime.Now;
            form.Enabled = formDto.Enabled;            
            form.Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls;
            _h5FormsContext.SaveChanges();
        }

      
    }
}
