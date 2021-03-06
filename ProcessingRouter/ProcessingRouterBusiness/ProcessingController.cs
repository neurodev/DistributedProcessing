﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessingRouterBusiness.Data;
using ProcessingRouterBusiness.Models;

namespace ProcessingRouterBusiness
{
    public class ProcessingController
    {
        private ProcessingRepository _processingRepository = new ProcessingRepository();
        private Interfaces.IProcessingRequestor _requestor;
        public string GetParameterControllerURL { get; set; }
        public string RequestorName { get; set; }
        public ProcessingController(Interfaces.IProcessingRequestor requestor)
        {
            _requestor = requestor;
        }

        public InitializationScriptModel Initialize()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory;
            var WorkerId = Guid.NewGuid();
            var template = new StringBuilder(System.IO.File.ReadAllText(System.IO.Path.Combine(filePath, "bin","ProcessorStartup.js")));
            template.Replace("[replace#workerId]", WorkerId.ToString());
            template.Replace("[replace#parameterURL]", GetParameterControllerURL);
            template.Replace("[replace#requestorName]", _requestor.GetType().ToString());




            var result = new InitializationScriptModel();
            result.ProcessingRouterScript = template.ToString();
            result.ProcessingRequesterScript = _requestor.StartUpScript();
            
            return result;
        }

        public string GetNewParameterSet(Guid WorkerId)
        {
            var ps = _processingRepository.GetParameterset(WorkerId);
            if (ps==null)
            {
                var NewParameterSet = _requestor.CalculationParameters();
                var JSON = NewParameterSet.ToJSON();
                
                var parametersetId = _processingRepository.SaveParameterset(_requestor.GetType().ToString(),NewParameterSet.ParameterSetId, JSON, WorkerId);
                return JSON;
            }

            _processingRepository.SaveParametersSent(ps.ParameterSetID, WorkerId);

            return ps.Parameters;
        }

        public string ResultReceived(Guid workerId, string result, int parametersetId)
        {
            _processingRepository.UpdateResult(workerId, parametersetId, result);
            var results = _processingRepository.GetResultsCount(parametersetId, result);
            if (results >= _requestor.RequiredMatchingResults())
            {
                _processingRepository.DeleteParameterSet(parametersetId);
                _requestor.RegisterResult(parametersetId, result);
            }
            return GetNewParameterSet(workerId);
        }
    }
}
