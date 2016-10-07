using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingRouterBusiness.Data
{
    public class ProcessingRepository
    {
        ProcessingRouterEntities DBContext = new ProcessingRouterEntities();
        public ProcessingRepository()
        {

        }

        
        public void UpdateResult(Guid WorkerID, int ParametersetId, string Result)
        {
            var record = DBContext.ParametersSent.Where(x => x.WorkerID == WorkerID && x.ParameterSetID == ParametersetId).FirstOrDefault();
            if (record != null)
            {

                record.Result = Result;
                DBContext.SaveChanges();
            }
        }

        public int GetResultsCount(int ParametersetId, string Result)
        {
            var records = DBContext.ParametersSent.Where(x => x.ParameterSetID == ParametersetId && x.Result == Result).Count();
            return records;
        }

        public void DeleteParameterSet(int ParameterSetId)
        {
            var paramsSentToDelete = DBContext.ParametersSent.Where(x => x.ParameterSetID == ParameterSetId);
            DBContext.ParametersSent.RemoveRange(paramsSentToDelete);
            var paramsToDelete = DBContext.ParameterSets.Where(x => x.ParameterSetID == ParameterSetId);
            DBContext.ParameterSets.RemoveRange(paramsToDelete);
            DBContext.SaveChanges();
        }

        //public ParameterSet GetParameterset(int ParametersetId)
        //{
        //    return DBContext.ParameterSets.Where(x => x.ParameterSetID == ParametersetId).FirstOrDefault();
        //}

        public void SaveParameterset(int ParametersetId, string Parameters, Guid WorkerID)
        {
            if(DBContext.ParameterSets.Count(x=>x.ParameterSetID==ParametersetId)==0)
                DBContext.ParameterSets.Add(new ParameterSets() { ParameterSetID = ParametersetId, Parameters = Parameters });
            DBContext.ParametersSent.Add(new ParametersSent() { ParameterSetID = ParametersetId, WorkerID = WorkerID });
            DBContext.SaveChanges();
        }

        public void SaveParametersSent(int ParametersetId, Guid WorkerID)
        {
            DBContext.ParametersSent.Add(new ParametersSent() { ParameterSetID = ParametersetId, WorkerID = WorkerID });
            DBContext.SaveChanges();
        }

        public ParameterSets GetParameterset(Guid WorkerId)
        {
            var ps = DBContext.ParameterSets.Where(x => !x.ParametersSent.Where(y => y.WorkerID == WorkerId).Any()).FirstOrDefault();
            return ps;
        }

    }
}
