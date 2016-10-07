using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingRouterBusiness.Types
{
    public abstract class ParameterSet
    {
        public int ParameterSetId { get; set; }
        public abstract string ToJSON();
    }
}
