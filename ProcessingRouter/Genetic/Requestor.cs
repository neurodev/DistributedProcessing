using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessingRouterBusiness.Types;
using System.Data.Entity.Core.Objects;

namespace Genetic
{
    public class Requestor : ProcessingRouterBusiness.Interfaces.IProcessingRequestor
    {
        Evolution2Entities DBContext = new Evolution2Entities();

        public ParameterSet CalculationParameters()
        {
           ObjectParameter outputParameter = new ObjectParameter("ParameterSetID", typeof(int));
            var result = new Generation();

            foreach (var step in DBContext.GetGeneration(outputParameter)) {
                result.steps.Add(new Step()
                {
                    colour = step.colour,
                    StartX = step.StartX,
                    StartY = step.StartY,
                    EndX = step.EndX,
                    EndY = step.EndY
                });
            };

            result.ParameterSetId = Convert.ToInt32(outputParameter.Value);
            return result;
        }

        public void RegisterResult<ResultType>(int parametersetId, ResultType result)
        {
            DBContext.RegisterResult(parametersetId, Convert.ToInt32(result));
        }

        public string StartUpScript()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory;
            var template = new StringBuilder(System.IO.File.ReadAllText(System.IO.Path.Combine(filePath, "bin", "CalculationScript.js")));

            template.Replace("[replace#targetImageURL]","/Static/GWB.png" );
            template.Replace("[replace#delay]", "50");


            return template.ToString();
                
        }
    }
}
