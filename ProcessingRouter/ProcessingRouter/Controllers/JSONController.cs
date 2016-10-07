using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessingRouterFacade.Controllers
{
    public class JSONController : BaseController
    {
        // GET: JSON
        [HttpPost]
        public ActionResult GetNewParameters(Guid WorkerID)
        {
            return new ContentResult { Content = processingController.GetNewParameterSet(WorkerID), ContentType = "application/json" };
            //return View();
        }

        [HttpPost]
        public ActionResult RegisterResult(Guid WorkerID, int ParameterSetId, string Result)
        {
            
            return new ContentResult { Content = processingController.ResultReceived(WorkerID, Result, ParameterSetId), ContentType = "application/json" };
        }
    }
}