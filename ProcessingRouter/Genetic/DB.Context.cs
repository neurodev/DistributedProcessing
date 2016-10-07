﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Genetic
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Evolution2Entities : DbContext
    {
        public Evolution2Entities()
            : base("name=Evolution2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<InstructionOrder> InstructionOrder { get; set; }
        public virtual DbSet<ParameterSets> ParameterSets { get; set; }
        public virtual DbSet<Colours> Colours { get; set; }
        public virtual DbSet<Instructions> Instructions { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
    
        public virtual ObjectResult<GetGeneration_Result> GetGeneration(ObjectParameter parameterSetID)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetGeneration_Result>("GetGeneration", parameterSetID);
        }
    
        public virtual int GangBang()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GangBang");
        }
    
        public virtual int RegisterResult(Nullable<int> parameterSetId, Nullable<int> score)
        {
            var parameterSetIdParameter = parameterSetId.HasValue ?
                new ObjectParameter("ParameterSetId", parameterSetId) :
                new ObjectParameter("ParameterSetId", typeof(int));
    
            var scoreParameter = score.HasValue ?
                new ObjectParameter("Score", score) :
                new ObjectParameter("Score", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegisterResult", parameterSetIdParameter, scoreParameter);
        }
    }
}
