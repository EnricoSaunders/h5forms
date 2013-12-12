using System.Web.Mvc;
using H5Forms.BusinessLogic;

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

    }
}
