﻿using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using AutoMapper;
using H5Forms.BusinessLogic.Helpers;
using H5Forms.Dtos.Common;
using H5Forms.Dtos.Form;
using H5Forms.Dtos.Form.Controls;
using H5Forms.EfRepository;
using H5Forms.Entities.Interfaces;
using System;
using H5Forms.Infrastructure;
using OfficeOpenXml;

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
            var forms = _h5FormsContext.Forms.Where(f => string.Equals(f.User.UserName, user)).ToList();

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
            Validate(formDto);

            var form = new Entities.Form.Form
            {
                User = _h5FormsContext.Users.Single(u => string.Equals(u.UserName, formDto.User.UserName)),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Enabled = formDto.Enabled,
                Title = formDto.Title,
                LabelLayoutType = (int)formDto.LabelLayoutType,
                Controls = Mapper.Map<Form, Entities.Form.Form>(formDto).Controls
            };

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() {IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted})){                

                #region Form               

                _h5FormsContext.Forms.Add(form);

                _h5FormsContext.SaveChanges();

                form.Hash = HashHelper.Hash(string.Format("{0}{1}{2}", form.Id, formDto.User.UserName, form.CreateDate)).Replace("/", string.Empty).Replace("+", string.Empty).Replace("=", string.Empty);

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
            Validate(formDto);

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
            formEntryDto.EntryDate = DateTime.Now;
            formEntryDto.Ip = HttpContext.Current.Request.GetClientIpAddress();

            var form = _h5FormsContext.Forms.Single(f => f.Id == formEntryDto.FormId);
            var formDto = Mapper.Map<Entities.Form.Form, Form>(form);
            var result = default(IList<string>);

            formDto.SetValues(formEntryDto);

            #region Validation

            result = formDto.BasicValidation();

            if (result.Count > 0)
                return result;           

            result = formDto.UniqueValidation(_h5FormsContext.IsUniqueEntry);      

            if (result.Count > 0)
                return result;

            var formEntry = Mapper.Map<FormEntry, Entities.Form.FormEntry>(formEntryDto);

            #endregion

            _h5FormsContext.AddEntry(formEntry);

            return result;
        }

        public FormEntries GetFormEntries(int formId)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formId);
            var formDto = Mapper.Map<Entities.Form.Form, Form>(form);           

            return new FormEntries
            {
                Columns = new[] { new { ColumnName = "EntryDate", Label = Resource.Date }, new { ColumnName = "Ip", Label = Resource.Ip } }
                                    .Concat(formDto.Controls.OfType<ValueControl>().Select(c => new { c.ColumnName, c.Label }))
                                    .ToDictionary(c => c.ColumnName, c=> c.Label),
                Entries = _h5FormsContext.GetEntries(formId).Select(e => GeEntryFormated(formDto, e)).ToList()
            };    
        }

        private Dictionary<string, string> GeEntryFormated(Form formDto, Entities.Form.FormEntry entry)
        {
            var result = new Dictionary<string, string>();

            result["EntryDate"] = entry.EntryDate.ToString();
            result["Ip"] = entry.Ip;

            var entryDto = Mapper.Map<Entities.Form.FormEntry, FormEntry>(entry);

            formDto.SetValues(entryDto);

            foreach (var kv in entry.ControlValues)
            {
                var controlId = int.Parse(kv.Key.Replace(FormSettings.COLUMN_PREFIX, string.Empty));
                var control = formDto.Controls.Single(c => c.Id == controlId) as ValueControl;

                result[kv.Key] = control.GetFormattedValue();

                //var optionValues = kv.Value;

                //if (control is OptionsControl)
                //{
                //    var optionsControl = (control as OptionsControl);

                //    switch (control.ControlType)
                //    {
                //        case ControlType.OptionList:
                //        {
                //            var values = optionValues.Split(new[] {FormSettings.SELECTED_VALUES_SEPARATOR}).ToArray();

                //            if (!string.IsNullOrEmpty(values[0]))
                //            {
                //                var option = optionsControl.Options.SingleOrDefault(o => o.Id == int.Parse(values[0]));

                //                if (option != null)
                //                    values[0] = option.Value;   
                //            }                            

                //            optionValues = string.Join(FormSettings.SELECTED_VALUES_CLIENT_SEPARATOR.ToString(), values.Where(v => !string.IsNullOrEmpty(v)));
                //        }

                //            break;
                //        default:
                //        {
                //            var optionIds = optionValues.Split(new[] {FormSettings.SELECTED_VALUES_SEPARATOR}).Where(id => !string.IsNullOrEmpty(id)).Select(id => int.Parse(id)).ToArray();
                //            optionValues = string.Join(FormSettings.SELECTED_VALUES_CLIENT_SEPARATOR.ToString(), optionsControl.Options.Where(o => optionIds.Any(oi => oi == o.Id)).Select(o => o.Value).ToArray());

                //        }
                //            break;
                //    }
                //}              
            }

            return result;
        }

        public EntriesExcel EntriesExcel(int formId)
        {
            var form = _h5FormsContext.Forms.Single(f => f.Id == formId);
            var entries = GetFormEntries(formId);

            FileInfo template = new FileInfo(String.Format(@"{0}\ReportTemplates\FormEntriesTemplate.xlsx", AppDomain.CurrentDomain.BaseDirectory));            
            ExcelPackage pck = new ExcelPackage(template, true);
            var ws = pck.Workbook.Worksheets[1];
            var row = 5;
            var col = 1;

            #region Header

            foreach (var column in entries.Columns)
            {
                ws.Cells[row, col].Value = column.Value;
                col++;
            }

            ws.Cells[3, 2].Value = form.Title;

            #endregion

            #region Body

            foreach (var entry in entries.Entries)
            {
                col = 1;
                row++;

                foreach (var column in entries.Columns)
                {                    
                    ws.Cells[row, col].Value = entry[column.Key];
                    col++;
                }
            }

            #endregion

            return new EntriesExcel
            {
                FormTitle = form.Title,
                ExcelPackage = pck
            };
        }

        private void Validate(Form formDto)
        {
            if(formDto.Controls == null || !formDto.Controls.Any(c => c is ValueControl))
                throw new ValidationException(Resource.ControlsEmpty);
        }
    }
}
