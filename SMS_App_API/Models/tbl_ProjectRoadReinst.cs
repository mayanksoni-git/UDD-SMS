
namespace ePayment_API.Models
{
    public class tbl_ProjectRoadReinst
    {
        public int ProjectRoadReinst_Id { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid_TillDate { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid_Pending { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid_Dismental { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid_Moterable { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid_Reinstatement { get; set; }
        public decimal ProjectRoadReinst_TotalLengthLaid_Reinstatement_And_Moterable { get; set; }
        public string ProjectRoadReinst_Comments { get; set; }
        public int ProjectRoadReinst_AddedBy { get; set; }
        public string ProjectRoadReinst_AddedOn { get; set; }
        public int ProjectRoadReinst_ModifiedBy { get; set; }
        public string ProjectRoadReinst_ModifiedOn { get; set; }
        public int ProjectRoadReinst_ProjectWork_Id { get; set; }
        public decimal ProjectRoadReinst_Latitude { get; set; }
        public decimal ProjectRoadReinst_Longitude { get; set; }
        public int ProjectRoadReinst_Status { get; set; }
        public string ProjectRoadReinst_SitePic_Path1 { get; set; }
        public string ProjectRoadReinst_SitePic_Bytes1 { get; set; }
        public string ProjectRoadReinst_SitePic_Path2 { get; set; }
        public string ProjectRoadReinst_SitePic_Bytes2 { get; set; }
        public string ProjectRoadReinst_SitePic_Path3 { get; set; }
        public string ProjectRoadReinst_SitePic_Bytes3 { get; set; }
    }
}
