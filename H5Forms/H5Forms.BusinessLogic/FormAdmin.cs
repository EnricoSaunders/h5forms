using H5Forms.EfRepository;
using H5Forms.Entities.Interfaces;

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
    }
}
