using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePayment_API.Models
{
    public class SchemeFundMaster
    {
        public string SchemeName { get; set; }
        public decimal AmtInLac { get; set; }
        public string SessionYear { get; set; }
        public string ULBName { get; set; }
        public string DistName { get; set; }
        public string ULBType { get; set; }
        public string ParliamentaryConstName { get; set; }
        public string MPName { get; set; }
        public string AssemblyConstName { get; set; }
        public string MLAName { get; set; }
        public int ID { get; set; }
        public int YearId { get; set; }
        public int Scheme_Id { get; set; }
        public int Circle_Id { get; set; }
        public int ULBTypeId { get; set; }
        public int ULBID { get; set; }
        public int UserLoginID { get; set; }
        public List<FundSanctionedGeoDetails> godetailList { get; set; }

    }
    public class FundSanctionedGeoDetails
    {
        //SM.SchemeID,SM.SchemeName,  FM.ID  ,FMD.GoLink,GONumber,GODate,Tranch
        public int SchemeID { get; set; }
        public int ID { get; set; }
        public string SchemeName { get; set; }
        public string GoLink { get; set; }
        public string GONumber { get; set; }
        public DateTime? GODate { get; set; }
        public int Tranch { get; set; }
    }
}