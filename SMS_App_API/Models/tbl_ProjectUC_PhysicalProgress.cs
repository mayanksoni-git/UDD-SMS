
namespace ePayment_API.Models
{
    public class tbl_ProjectUC_PhysicalProgress
    {
        public int PhysicalProgressComponent_Id { get; set; }
        public int ProjectUC_PhysicalProgress_UC_Id { get; set; }
        public string PhysicalProgressComponent_Component { get; set; }
        public string Unit_Name { get; set; }
        public int PhysicalProgress_Total { get; set; }
        public int PhysicalProgress_Current { get; set; }
    }
}
