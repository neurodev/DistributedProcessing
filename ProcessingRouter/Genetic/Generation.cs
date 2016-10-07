using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace Genetic
{
    public class Generation : ProcessingRouterBusiness.Types.ParameterSet
    {
        public int ScoreToBeat { get; set; }
        public List<Step> steps = new List<Step>();
        public override string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}
