using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schemeforfarmer_Project_VS.Models
{
    public class calculateinsurance
    {
        public string InsuranceCompany { get; set; }
        public float SumInsuredPerHectare { get; set; }
        public float SharePremium { get; set; }
        public float PremiumAmount { get; set; }
        public string CropName { get; set; }
        public float Area { get; set; }
        public float SumInsured { get; set; }

    }
}