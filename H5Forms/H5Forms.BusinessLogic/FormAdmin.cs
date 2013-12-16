using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;
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

        public FormAdmin() : this(new H5FormsContext() )
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

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() {IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted})){
                //((IObjectContextAdapter) _h5FormsContext).ObjectContext.Connection.Open();

                #region Form               

                _h5FormsContext.Forms.Add(form);

                _h5FormsContext.SaveChanges();

                form.Hash = HashHelper.Hash(string.Format("{0}{1}{2}", form.Id, formDto.User.Nick, form.CreateDate)).Replace("/", string.Empty).Replace("+", string.Empty).Replace("=", string.Empty);

                _h5FormsContext.SaveChanges();

                #endregion

                #region FormEntryModel

                var columns = formDto.Controls.Where(c => c is ValueControl).Select(c => c.ColumnName).ToList();

               _h5FormsContext.CreateEntryModel(form.Id,  columns);

                #endregion

                transaction.Complete();
            }                                  
        }

        public void UpdateForm(Form formDto)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formDto.Id);

            form.Title = formDto.Title;
            form.UpdateDate = DateTime.Now;
            form.Enabled = formDto.Enabled;
            form.LabelLayoutType = (int)formDto.LabelLayoutType;
            form.Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls;


            var columns = formDto.Controls.Where(c => c is ValueControl).Select(c => c.ColumnName).ToList();

            _h5FormsContext.UpdateEntryModel(form.Id, columns);

            _h5FormsContext.SaveChanges();
        }

        public IList<string> AddEntry(FormEntry formEntryDto)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formEntryDto.FormId);
            var formDto = Mapper.Map<Entities.Form.Form, Form>(form);
            var result = default(IList<string>);

            formDto.SetValues(formEntryDto);
            result = formDto.Validate();

            if (result.Count > 0)
                return result;

            var formEntry = Mapper.Map<FormEntry, Entities.Form.FormEntry>(formEntryDto);

             _h5FormsContext.AddEntry(formEntry);

            return result;
        }       
    }
}
