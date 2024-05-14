
using System;

namespace PMIS_API.Models
{
    public class tbl_ProjectWorkGO
    {
        public int ProjectWorkGO_Id { get; set; }
        public string ProjectWorkGO_GO_Date { get; set; }
        public string ProjectWorkGO_GO_Number { get; set; }
        public int ProjectWorkGO_Work_Id { get; set; }
        public Decimal ProjectWorkGO_TotalRelease { get; set; }
        public Decimal ProjectWorkGO_CentralShare { get; set; }
        public Decimal ProjectWorkGO_StateShare { get; set; }
        public Decimal ProjectWorkGO_ULBShare { get; set; }
        public Decimal ProjectWorkGO_Centage { get; set; }
        public string ProjectWorkGO_IssuingAuthority { get; set; }
        public int ProjectWorkGO_ULB_Id { get; set; }
        public string ProjectWorkGO_DepartmentName { get; set; }
        public string ProjectWorkGO_Document_Path { get; set; }
        public string ULBName { get; set; }
    }
}
