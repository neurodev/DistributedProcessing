using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProcessingRouterBusiness;
using ProcessingRouterBusiness.Interfaces;

namespace ProcessingRouterFacade.Controllers
{
    public class BaseController : Controller
    {

        protected IProcessingRequestor requestor;
        protected ProcessingController processingController;
        public BaseController()
        {
            var objectName = "Genetic.Requestor";
            Type objectType = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                               from type in asm.GetTypes()
                               where type.IsClass
                               //&& type.Name == "Requestor" 
                               && type.FullName == objectName
                               select type).Single();

            var requestor = Activator.CreateInstance(objectType);
            //var requestor = Activator.CreateInstance("Genetic", "Genetic.Requestor");
            //processingController = new ProcessingController((IProcessingRequestor)requestor.Unwrap());
            processingController = new ProcessingController((IProcessingRequestor)requestor);
            processingController.ResultsNeededForVerification = 1;
        }
        
    }
}