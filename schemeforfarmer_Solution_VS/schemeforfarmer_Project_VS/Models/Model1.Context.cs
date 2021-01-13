﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace schemeforfarmer_Project_VS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class dbFarmerScheme3Entities : DbContext
    {
        public dbFarmerScheme3Entities()
            : base("name=dbFarmerScheme3Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBidCrop> tblBidCrops { get; set; }
        public virtual DbSet<tblBidder> tblBidders { get; set; }
        public virtual DbSet<tblBid> tblBids { get; set; }
        public virtual DbSet<tblCropDetail> tblCropDetails { get; set; }
        public virtual DbSet<tblCropForSale> tblCropForSales { get; set; }
        public virtual DbSet<tblFarmer> tblFarmers { get; set; }
        public virtual DbSet<tblInsurance> tblInsurances { get; set; }
        public virtual DbSet<tblInsuranceClaim> tblInsuranceClaims { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    
        public virtual int proc_calculateInsurance(string croptype, string cropname, Nullable<double> area, ObjectParameter sumfinal, ObjectParameter fshare, ObjectParameter gvtshare)
        {
            var croptypeParameter = croptype != null ?
                new ObjectParameter("croptype", croptype) :
                new ObjectParameter("croptype", typeof(string));
    
            var cropnameParameter = cropname != null ?
                new ObjectParameter("cropname", cropname) :
                new ObjectParameter("cropname", typeof(string));
    
            var areaParameter = area.HasValue ?
                new ObjectParameter("area", area) :
                new ObjectParameter("area", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_calculateInsurance", croptypeParameter, cropnameParameter, areaParameter, sumfinal, fshare, gvtshare);
        }
    
        public virtual ObjectResult<proc_getdetailsofinsuree_Result> proc_getdetailsofinsuree(Nullable<long> appid)
        {
            var appidParameter = appid.HasValue ?
                new ObjectParameter("appid", appid) :
                new ObjectParameter("appid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_getdetailsofinsuree_Result>("proc_getdetailsofinsuree", appidParameter);
        }
    
        public virtual ObjectResult<sp_GetCurrentBid_Result> sp_GetCurrentBid(Nullable<int> cropid)
        {
            var cropidParameter = cropid.HasValue ?
                new ObjectParameter("cropid", cropid) :
                new ObjectParameter("cropid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetCurrentBid_Result>("sp_GetCurrentBid", cropidParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> sp_PreviousBids(Nullable<int> cropid)
        {
            var cropidParameter = cropid.HasValue ?
                new ObjectParameter("cropid", cropid) :
                new ObjectParameter("cropid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("sp_PreviousBids", cropidParameter);
        }
    
        public virtual ObjectResult<spDisplayPending_Result> spDisplayPending(Nullable<int> farmerid)
        {
            var farmeridParameter = farmerid.HasValue ?
                new ObjectParameter("farmerid", farmerid) :
                new ObjectParameter("farmerid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spDisplayPending_Result>("spDisplayPending", farmeridParameter);
        }
    
        public virtual ObjectResult<spDisplaySoldHistory_Result> spDisplaySoldHistory(Nullable<int> farmerid)
        {
            var farmeridParameter = farmerid.HasValue ?
                new ObjectParameter("farmerid", farmerid) :
                new ObjectParameter("farmerid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spDisplaySoldHistory_Result>("spDisplaySoldHistory", farmeridParameter);
        }
    
        public virtual int sp_Place_Request(Nullable<int> farmerid, string croptype, string cropname, Nullable<double> quantity, string fertilizerType, string soilPhCertificate)
        {
            var farmeridParameter = farmerid.HasValue ?
                new ObjectParameter("farmerid", farmerid) :
                new ObjectParameter("farmerid", typeof(int));
    
            var croptypeParameter = croptype != null ?
                new ObjectParameter("croptype", croptype) :
                new ObjectParameter("croptype", typeof(string));
    
            var cropnameParameter = cropname != null ?
                new ObjectParameter("cropname", cropname) :
                new ObjectParameter("cropname", typeof(string));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("quantity", quantity) :
                new ObjectParameter("quantity", typeof(double));
    
            var fertilizerTypeParameter = fertilizerType != null ?
                new ObjectParameter("FertilizerType", fertilizerType) :
                new ObjectParameter("FertilizerType", typeof(string));
    
            var soilPhCertificateParameter = soilPhCertificate != null ?
                new ObjectParameter("SoilPhCertificate", soilPhCertificate) :
                new ObjectParameter("SoilPhCertificate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Place_Request", farmeridParameter, croptypeParameter, cropnameParameter, quantityParameter, fertilizerTypeParameter, soilPhCertificateParameter);
        }
    
        public virtual int Add_MSPtotblCropforSale(Nullable<int> crpid)
        {
            var crpidParameter = crpid.HasValue ?
                new ObjectParameter("crpid", crpid) :
                new ObjectParameter("crpid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Add_MSPtotblCropforSale", crpidParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> proc_checkexpiredateofclaim(Nullable<int> policy, Nullable<System.DateTime> dateofinsuranceapp, Nullable<System.DateTime> dateofloss, Nullable<int> fid, string croptype)
        {
            var policyParameter = policy.HasValue ?
                new ObjectParameter("policy", policy) :
                new ObjectParameter("policy", typeof(int));
    
            var dateofinsuranceappParameter = dateofinsuranceapp.HasValue ?
                new ObjectParameter("dateofinsuranceapp", dateofinsuranceapp) :
                new ObjectParameter("dateofinsuranceapp", typeof(System.DateTime));
    
            var dateoflossParameter = dateofloss.HasValue ?
                new ObjectParameter("dateofloss", dateofloss) :
                new ObjectParameter("dateofloss", typeof(System.DateTime));
    
            var fidParameter = fid.HasValue ?
                new ObjectParameter("fid", fid) :
                new ObjectParameter("fid", typeof(int));
    
            var croptypeParameter = croptype != null ?
                new ObjectParameter("croptype", croptype) :
                new ObjectParameter("croptype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("proc_checkexpiredateofclaim", policyParameter, dateofinsuranceappParameter, dateoflossParameter, fidParameter, croptypeParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_approveBid(Nullable<int> bid, Nullable<int> cropid)
        {
            var bidParameter = bid.HasValue ?
                new ObjectParameter("bid", bid) :
                new ObjectParameter("bid", typeof(int));
    
            var cropidParameter = cropid.HasValue ?
                new ObjectParameter("cropid", cropid) :
                new ObjectParameter("cropid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_approveBid", bidParameter, cropidParameter);
        }
    
        public virtual int sp_approveBidder(Nullable<int> bid, string admin, Nullable<System.DateTime> adate, string pass, string email)
        {
            var bidParameter = bid.HasValue ?
                new ObjectParameter("bid", bid) :
                new ObjectParameter("bid", typeof(int));
    
            var adminParameter = admin != null ?
                new ObjectParameter("admin", admin) :
                new ObjectParameter("admin", typeof(string));
    
            var adateParameter = adate.HasValue ?
                new ObjectParameter("adate", adate) :
                new ObjectParameter("adate", typeof(System.DateTime));
    
            var passParameter = pass != null ?
                new ObjectParameter("pass", pass) :
                new ObjectParameter("pass", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_approveBidder", bidParameter, adminParameter, adateParameter, passParameter, emailParameter);
        }
    
        public virtual int sp_approveFarmer(Nullable<int> fid, string admin, Nullable<System.DateTime> aDate, string pass, string email)
        {
            var fidParameter = fid.HasValue ?
                new ObjectParameter("fid", fid) :
                new ObjectParameter("fid", typeof(int));
    
            var adminParameter = admin != null ?
                new ObjectParameter("admin", admin) :
                new ObjectParameter("admin", typeof(string));
    
            var aDateParameter = aDate.HasValue ?
                new ObjectParameter("aDate", aDate) :
                new ObjectParameter("aDate", typeof(System.DateTime));
    
            var passParameter = pass != null ?
                new ObjectParameter("pass", pass) :
                new ObjectParameter("pass", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_approveFarmer", fidParameter, adminParameter, aDateParameter, passParameter, emailParameter);
        }
    
        public virtual int sp_approvesale(Nullable<int> cropid, string cropType, string cropname)
        {
            var cropidParameter = cropid.HasValue ?
                new ObjectParameter("cropid", cropid) :
                new ObjectParameter("cropid", typeof(int));
    
            var cropTypeParameter = cropType != null ?
                new ObjectParameter("cropType", cropType) :
                new ObjectParameter("cropType", typeof(string));
    
            var cropnameParameter = cropname != null ?
                new ObjectParameter("cropname", cropname) :
                new ObjectParameter("cropname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_approvesale", cropidParameter, cropTypeParameter, cropnameParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_getCropData_Result> sp_getCropData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getCropData_Result>("sp_getCropData");
        }
    
        public virtual ObjectResult<Nullable<decimal>> sp_GetMAxBidAmount(Nullable<int> cropid, Nullable<int> bidderid)
        {
            var cropidParameter = cropid.HasValue ?
                new ObjectParameter("cropid", cropid) :
                new ObjectParameter("cropid", typeof(int));
    
            var bidderidParameter = bidderid.HasValue ?
                new ObjectParameter("bidderid", bidderid) :
                new ObjectParameter("bidderid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("sp_GetMAxBidAmount", cropidParameter, bidderidParameter);
        }
    
        public virtual ObjectResult<sp_getPendingBidders_Result> sp_getPendingBidders()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getPendingBidders_Result>("sp_getPendingBidders");
        }
    
        public virtual ObjectResult<sp_getpendingbids_Result> sp_getpendingbids()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getpendingbids_Result>("sp_getpendingbids");
        }
    
        public virtual ObjectResult<sp_getPendingFarmers_Result> sp_getPendingFarmers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getPendingFarmers_Result>("sp_getPendingFarmers");
        }
    
        public virtual ObjectResult<sp_getPendingSaleData_Result> sp_getPendingSaleData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getPendingSaleData_Result>("sp_getPendingSaleData");
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_InsertintoBidCrops(Nullable<int> bidId, Nullable<int> farmerId, Nullable<int> cropId, Nullable<int> bidderId)
        {
            var bidIdParameter = bidId.HasValue ?
                new ObjectParameter("bidId", bidId) :
                new ObjectParameter("bidId", typeof(int));
    
            var farmerIdParameter = farmerId.HasValue ?
                new ObjectParameter("FarmerId", farmerId) :
                new ObjectParameter("FarmerId", typeof(int));
    
            var cropIdParameter = cropId.HasValue ?
                new ObjectParameter("CropId", cropId) :
                new ObjectParameter("CropId", typeof(int));
    
            var bidderIdParameter = bidderId.HasValue ?
                new ObjectParameter("BidderId", bidderId) :
                new ObjectParameter("BidderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertintoBidCrops", bidIdParameter, farmerIdParameter, cropIdParameter, bidderIdParameter);
        }
    
        public virtual int sp_newBid(Nullable<int> cropid, Nullable<int> bidderid, Nullable<decimal> bidamt, Nullable<System.DateTime> dateofbid)
        {
            var cropidParameter = cropid.HasValue ?
                new ObjectParameter("cropid", cropid) :
                new ObjectParameter("cropid", typeof(int));
    
            var bidderidParameter = bidderid.HasValue ?
                new ObjectParameter("bidderid", bidderid) :
                new ObjectParameter("bidderid", typeof(int));
    
            var bidamtParameter = bidamt.HasValue ?
                new ObjectParameter("bidamt", bidamt) :
                new ObjectParameter("bidamt", typeof(decimal));
    
            var dateofbidParameter = dateofbid.HasValue ?
                new ObjectParameter("dateofbid", dateofbid) :
                new ObjectParameter("dateofbid", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_newBid", cropidParameter, bidderidParameter, bidamtParameter, dateofbidParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_UpdateSoldPriceintblCropForSale(Nullable<int> crpid)
        {
            var crpidParameter = crpid.HasValue ?
                new ObjectParameter("crpid", crpid) :
                new ObjectParameter("crpid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdateSoldPriceintblCropForSale", crpidParameter);
        }
    
        public virtual int sp_UpdatetblCropforsale(Nullable<int> crpid)
        {
            var crpidParameter = crpid.HasValue ?
                new ObjectParameter("crpid", crpid) :
                new ObjectParameter("crpid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdatetblCropforsale", crpidParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<sp_getClaimData_Result> sp_getClaimData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getClaimData_Result>("sp_getClaimData");
        }
    
        public virtual ObjectResult<marketplace_Result> marketplace(Nullable<int> farmerid)
        {
            var farmeridParameter = farmerid.HasValue ?
                new ObjectParameter("farmerid", farmerid) :
                new ObjectParameter("farmerid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<marketplace_Result>("marketplace", farmeridParameter);
        }
    
        public virtual ObjectResult<sp_lostBids_Result> sp_lostBids(Nullable<int> bidderId)
        {
            var bidderIdParameter = bidderId.HasValue ?
                new ObjectParameter("bidderId", bidderId) :
                new ObjectParameter("bidderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_lostBids_Result>("sp_lostBids", bidderIdParameter);
        }
    
        public virtual int sp_updateClaim(Nullable<long> policy, Nullable<int> flag)
        {
            var policyParameter = policy.HasValue ?
                new ObjectParameter("policy", policy) :
                new ObjectParameter("policy", typeof(long));
    
            var flagParameter = flag.HasValue ?
                new ObjectParameter("flag", flag) :
                new ObjectParameter("flag", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_updateClaim", policyParameter, flagParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_validateClaim(Nullable<System.DateTime> dateofinsuranceapp, Nullable<System.DateTime> dateofloss, Nullable<int> fid, string croptype)
        {
            var dateofinsuranceappParameter = dateofinsuranceapp.HasValue ?
                new ObjectParameter("dateofinsuranceapp", dateofinsuranceapp) :
                new ObjectParameter("dateofinsuranceapp", typeof(System.DateTime));
    
            var dateoflossParameter = dateofloss.HasValue ?
                new ObjectParameter("dateofloss", dateofloss) :
                new ObjectParameter("dateofloss", typeof(System.DateTime));
    
            var fidParameter = fid.HasValue ?
                new ObjectParameter("fid", fid) :
                new ObjectParameter("fid", typeof(int));
    
            var croptypeParameter = croptype != null ?
                new ObjectParameter("croptype", croptype) :
                new ObjectParameter("croptype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_validateClaim", dateofinsuranceappParameter, dateoflossParameter, fidParameter, croptypeParameter);
        }
    
        public virtual ObjectResult<sp_wonBids_Result> sp_wonBids(Nullable<int> bidderid)
        {
            var bidderidParameter = bidderid.HasValue ?
                new ObjectParameter("bidderid", bidderid) :
                new ObjectParameter("bidderid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_wonBids_Result>("sp_wonBids", bidderidParameter);
        }
    
        public virtual ObjectResult<sp_getpendingbids1_Result> sp_getpendingbids1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getpendingbids1_Result>("sp_getpendingbids1");
        }
    
        public virtual ObjectResult<sp_getClaimInfo_Result> sp_getClaimInfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getClaimInfo_Result>("sp_getClaimInfo");
        }
    
        public virtual ObjectResult<string> sp_GetAllCrops()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_GetAllCrops");
        }
    }
}