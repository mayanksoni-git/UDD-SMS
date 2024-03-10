
using System;

namespace SMS_External_API.Models
{
    public class tbl_ProjectWorkInstallment
    {
        public int ProjectWorkInstallment_Id { get; set; }
        public string ProjectWorkInstallment_GO_Date { get; set; }
        public string ProjectWorkInstallment_GO_Number { get; set; }
        public int ProjectWorkInstallment_Work_Id { get; set; }
        public Decimal ProjectWorkInstallment_TotalRelease { get; set; }
        public Decimal ProjectWorkInstallment_CentralShare { get; set; }
        public Decimal ProjectWorkInstallment_StateShare { get; set; }
        public Decimal ProjectWorkInstallment_ULBShare { get; set; }
        public Decimal ProjectWorkInstallment_Centage { get; set; }
        public string ProjectWorkInstallment_Document_Path { get; set; }
    }
}
