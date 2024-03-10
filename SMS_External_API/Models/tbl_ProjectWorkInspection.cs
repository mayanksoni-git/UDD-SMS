
using System;

namespace SMS_External_API.Models
{
    public class tbl_ProjectWorkInspection
    {
        public int ProjectWorkInspection_Id { get; set; }
        public string ProjectWorkInspection_Date { get; set; }
        public string ProjectWorkInspection_InspectionBy { get; set; }
        public int ProjectWorkInspection_Work_Id { get; set; }
        public string ProjectWorkInspection_Photo_Path1 { get; set; }
        public string ProjectWorkInspection_Photo_Path2 { get; set; }
        public string ProjectWorkInspection_Photo_Path3 { get; set; }
        public string ProjectWorkInspection_Photo_Path4 { get; set; }
        public string ProjectWorkInspection_Comments { get; set; }
        public decimal ProjectWorkInspection_Latitude { get; set; }
        public decimal ProjectWorkInspection_Longitude { get; set; }
    }
}
