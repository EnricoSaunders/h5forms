using System.Web.Mvc;
using H5Forms.BusinessLogic;
using H5Forms.Dtos.Form;

namespace H5Forms.MvcWebApp.Controllers
{
    public class FormsTestController : Controller
    {
        #region Properties
        private FormAdmin _formAdmin;        
        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _formAdmin = new FormAdmin();            
        }

        public ActionResult Test(string id)
        {
            var form = _formAdmin.GetFormByHash(id);

            return View(form);
        }

        [HttpPost]
        public ActionResult Test(FormEntry formEntry)
        {
            ModelState.Clear();

            var result = _formAdmin.AddEntry(formEntry);
            var form = _formAdmin.GetForm(formEntry.FormId);           
            
            foreach (var message in result)
            {
                ModelState.AddModelError(string.Empty, message);
                form.SetValues(formEntry);
            }

            return View(form);
        }

    }
}
