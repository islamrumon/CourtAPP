﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourtApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class COURTDATABASEEntities : DbContext
    {
        public COURTDATABASEEntities()
            : base("name=DB_9BC044_CCASEDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AREAINF> AREAINFs { get; set; }
        public virtual DbSet<COURTINF> COURTINFs { get; set; }
        public virtual DbSet<SMINF> SMINFs { get; set; }
        public virtual DbSet<SMINFP> SMINFPs { get; set; }
        public virtual DbSet<WRINF> WRINFs { get; set; }
        public virtual DbSet<WRINFP> WRINFPs { get; set; }
    
        public virtual int UpdateWRP(Nullable<long> wRID, Nullable<int> pSL, string pRSNAME, string pRSADDRESS, Nullable<int> aREAID)
        {
            var wRIDParameter = wRID.HasValue ?
                new ObjectParameter("WRID", wRID) :
                new ObjectParameter("WRID", typeof(long));
    
            var pSLParameter = pSL.HasValue ?
                new ObjectParameter("PSL", pSL) :
                new ObjectParameter("PSL", typeof(int));
    
            var pRSNAMEParameter = pRSNAME != null ?
                new ObjectParameter("PRSNAME", pRSNAME) :
                new ObjectParameter("PRSNAME", typeof(string));
    
            var pRSADDRESSParameter = pRSADDRESS != null ?
                new ObjectParameter("PRSADDRESS", pRSADDRESS) :
                new ObjectParameter("PRSADDRESS", typeof(string));
    
            var aREAIDParameter = aREAID.HasValue ?
                new ObjectParameter("AREAID", aREAID) :
                new ObjectParameter("AREAID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateWRP", wRIDParameter, pSLParameter, pRSNAMEParameter, pRSADDRESSParameter, aREAIDParameter);
        }
    
        public virtual int UpdateSMP(Nullable<long> smId, Nullable<long> psl, string pName, string pAddress, Nullable<int> areaId, string smType)
        {
            var smIdParameter = smId.HasValue ?
                new ObjectParameter("smId", smId) :
                new ObjectParameter("smId", typeof(long));
    
            var pslParameter = psl.HasValue ?
                new ObjectParameter("psl", psl) :
                new ObjectParameter("psl", typeof(long));
    
            var pNameParameter = pName != null ?
                new ObjectParameter("pName", pName) :
                new ObjectParameter("pName", typeof(string));
    
            var pAddressParameter = pAddress != null ?
                new ObjectParameter("pAddress", pAddress) :
                new ObjectParameter("pAddress", typeof(string));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("areaId", areaId) :
                new ObjectParameter("areaId", typeof(int));
    
            var smTypeParameter = smType != null ?
                new ObjectParameter("smType", smType) :
                new ObjectParameter("smType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateSMP", smIdParameter, pslParameter, pNameParameter, pAddressParameter, areaIdParameter, smTypeParameter);
        }
    
        public virtual ObjectResult<SommonList_Result> SommonList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SommonList_Result>("SommonList");
        }
    
        public virtual ObjectResult<warrantL_Result> warrantL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<warrantL_Result>("warrantL");
        }
    
        public virtual ObjectResult<SomonWithDetials_Result> SomonWithDetials()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SomonWithDetials_Result>("SomonWithDetials");
        }
    
        public virtual ObjectResult<WarrantWithDetials_Result> WarrantWithDetials()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WarrantWithDetials_Result>("WarrantWithDetials");
        }
    }
}
