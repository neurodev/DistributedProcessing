using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingRouterBusiness.Interfaces
{
    public interface IProcessingRequestor
    {
        /// <summary>
        /// Javascript code. 
        /// It must contain a function called StartCalculation(parameters), which will be executed when the parameters are downloaded and the calculation is able to start.
        /// When the calculation is completed, it must call the function RegisterResult(parametersetId, result), 
        /// which is part of the framework.
        /// </summary>
        /// <returns></returns>
        string StartUpScript();
        
        /// <summary>
        /// Must return a JSON string. The top level of the JSON string should have a parametersetId property that uniquely identifies this set of parameters.
        /// </summary>
        /// <returns></returns>
        Types.ParameterSet CalculationParameters();
        
        /// <summary>
        /// This will be called when a calculation has been completed and verified 
        /// </summary>
        /// <param name="parametersetId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        void RegisterResult<ResultType>(int parametersetId, ResultType result);

        /// <summary>
        /// This will determine how many results need to be collected with the same answer in order to return an answer with confidence
        /// The more results required, the more confident we can be in the result.
        /// </summary>
        /// <returns></returns>
        int RequiredMatchingResults();
    }
}
