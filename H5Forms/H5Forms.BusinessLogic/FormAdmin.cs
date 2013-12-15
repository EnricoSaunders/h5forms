﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.EfRepository;
using H5Forms.Entities.Interfaces;
using System;
using H5Forms.Infrastructure;

namespace H5Forms.BusinessLogic
{
    public class FormAdmin
    {
        private readonly IH5FormsContext _h5FormsContext;

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

        public IEnumerable<string> GetOptionLayoutTypes()
        {
            return Enum.GetNames(typeof(OptionLayoutType));
        }

        public IEnumerable<string> GetLabelLayoutTypes()
        {
            return Enum.GetNames(typeof(LabelLayoutType));
        }
                 

        public IList<BasicForm> GetForms(string user)
        {
            var forms = _h5FormsContext.Forms.Where(f => string.Equals(f.User.Nick, user)).ToList();

            return Mapper.Map<IList<Entities.Form.Form>, IList<BasicForm>>(forms);
        }

        public Form GetForm(int formId)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formId);            

            return Mapper.Map<Entities.Form.Form, Form>(form);
        }

        public Form GetFormByHash(string hash)
        {
            var form = _h5FormsContext.Forms.Single(f => string.Equals(f.Hash, hash));            

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
                LabelLayoutType = (int)formDto.LabelLayoutType,
                Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls                                
            };
            

            _h5FormsContext.Forms.Add(form);

            _h5FormsContext.SaveChanges();

            form.Hash = HashHelper.Hash(string.Format("{0}{1}{2}", form.Id, formDto.User.Nick, form.CreateDate)).Replace("/", string.Empty).Replace("+", string.Empty).Replace("=", string.Empty);

            _h5FormsContext.SaveChanges();
        }

        public void UpdateForm(Form formDto)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formDto.Id);

            form.Title = formDto.Title;
            form.UpdateDate = DateTime.Now;
            form.Enabled = formDto.Enabled;
            form.LabelLayoutType = (int)formDto.LabelLayoutType;
            form.Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls;
            _h5FormsContext.SaveChanges();
        }

        public IList<string> AddEntry(FormEntry formEntry)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formEntry.FormId);
            var formDto = Mapper.Map<Entities.Form.Form, Form>(form);
            var result = default(IList<string>);

            formDto.SetValues(formEntry);
            result = formDto.Validate();

            if (result.Count > 0)
                return result;

            // _formEntryRepository.Add(formEntry);

            return result;
        }       
    }
}
