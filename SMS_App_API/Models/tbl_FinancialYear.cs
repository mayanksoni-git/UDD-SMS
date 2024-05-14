
using System.Collections.Generic;

namespace ePayment_API.Models
{
    public class tbl_FinancialYear
    {
        public int FinancialYear_Id { get; set; }
        public int FinancialYear_ProjectCount { get; set; }
        public string FinancialYear_Name { get; set; }
        public List<tbl_Scheme_Wise_Report> obj_tbl_Scheme_Wise_Report_Li { get; set; }
    }
}
