
namespace ePayment_API.Models
{
    public class tbl_Person
    {
        public int Person_Id { get; set; }
        public string Person_Mobile { get; set; }
        public string Person_Mobile2 { get; set; }
        public string Person_UserName { get; set; }
        public string Person_Password { get; set; }
        public string Person_Email { get; set; }
        public string response { get; set; }
        public string District_Name { get; set; }
        public string OTP { get; set; }
        public string ULB_Name { get; set; }
        public int ULB_Id { get; set; }
        public int District_Id { get; set; }
        public int Role_ULB { get; set; }
        public int Role_Inspection { get; set; }
        public int Role_Vendor { get; set; }
        public int FinancialYear_Id { get; set; }
        public int ProjectDPR_Id { get; set; }
        public int ProjectWork_Id { get; set; }
        public int ProjectUC_Id { get; set; }
        public string Project_Status { get; set; }
        public int Project_Id { get; set; }
        public string Base_URL { get; set; }
        public int Department_Id { get; set; }
        public string Department_Name { get; set; }
        public int Designation_Id { get; set; }
        public string Designation_Name { get; set; }
        public string Person_Name { get; set; }
        public int Zone_Id { get; set; }
        public string Zone_Name { get; set; }
        public int Circle_Id { get; set; }
        public string Circle_Name { get; set; }
        public int Division_Id { get; set; }
        public string Division_Name { get; set; }
        public string Person_TelePhone { get; set; }
        public string Person_ProfilePIC { get; set; }
        public string UserType_Desc { get; set; }
        public string Person_FName { get; set; }
        public string Person_Address { get; set; }
        public string Person_Pic_File { get; set; }
        public string Person_Base_64 { get; set; }
        public string Client_Code { get; set; }
        public string Error_Message { get; set; }
        public string URL_About_Department { get; set; }
        public string URL_About_PMS { get; set; }
        public int Enable_Physical_Graph_On_Dash { get; set; }
        public int Enable_Financial_Graph_On_Dash { get; set; }
        public int Enable_Physical_Graph_On_L1 { get; set; }
        public int Enable_Financial_Graph_On_L1 { get; set; }
        public int Enable_Physical_Graph_On_Site_Progress { get; set; }
        public int Enable_Financial_Graph_On_Site_Progress { get; set; }
    }
}
