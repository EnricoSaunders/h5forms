using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.EfRepository;
using H5Forms.Entities.Interfaces;
using System;

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

        public void UpdateForm(Form formDto)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formDto.Id);                        

            form.Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls;
            _h5FormsContext.SaveChanges();            
        }

        public Form AddForm(string user)
        {
            var form = new Entities.Form.Form
            {                
                User = _h5FormsContext.Users.Single(u => string.Equals(u.Nick, user))
            };

            _h5FormsContext.Forms.Add(form);

            _h5FormsContext.SaveChanges();

            return Mapper.Map<Entities.Form.Form, Form>(form);
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
    }
}
