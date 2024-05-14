
namespace ePayment_API.Models
{
    public class tbl_ProjectUC_Deliverables
    {
        public int Deliverables_Id { get; set; }
        public int ProjectUC_Deliverables_UC_Id { get; set; }
        public string Deliverables_Deliverables { get; set; }
        public string Unit_Name { get; set; }
        public int Deliverables_Total { get; set; }
        public int DeliverablesTotal_Current { get; set; }
    }
}
