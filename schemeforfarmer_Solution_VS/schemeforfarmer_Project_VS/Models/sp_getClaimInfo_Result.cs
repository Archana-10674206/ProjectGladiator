//------------------------------------------------------------------------------
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
    
    public partial class sp_getClaimInfo_Result
    {
        public long insuranceApplicationId { get; set; }
        public Nullable<int> farmerid { get; set; }
        public string croptype { get; set; }
        public string cropname { get; set; }
        public Nullable<decimal> suminsured { get; set; }
        public Nullable<System.DateTime> dateofapplication { get; set; }
        public System.DateTime dateofloss { get; set; }
        public double area { get; set; }
        public string causeofloss { get; set; }
    }
}