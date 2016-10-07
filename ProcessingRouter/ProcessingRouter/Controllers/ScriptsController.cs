using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessingRouterFacade.Controllers
{
    public class ScriptsController : BaseController
    {
        [ActionName("ProcessingClient.js")]
        public ActionResult InitialScript()
        {
            processingController.GetParameterControllerURL = Url.Action("Index","JSON");
            var model = processingController.Initialize();

            return this.JavaScriptFromView(model:model);
        }

        public ActionResult Index()
        {
            return Content("Scripts folder");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            var res = this.JavaScriptFromView();
            res.ExecuteResult(ControllerContext);
        }

    }
}