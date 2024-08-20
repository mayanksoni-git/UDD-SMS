
namespace ePayment_API.Models
{
    public class tbl_Profile
    {
        public int Person_Id { get; set; }
        public string Profile_Mobile { get; set; }
        public string Profile_Name { get; set; }
        public string Profile_Email { get; set; }
        public string Profile_Address { get; set; }
        public string Profile_Pic_File { get; set; }
        public string Profile_Base_64 { get; set; }
        public string Profile_Base_URL { get; set; }
        public string Designation_Name { get; set; }
        public string Department_Name { get; set; }
        public string Zone_Name { get; set; }
        public string Circle_Name { get; set; }
        public string Division_Name { get; set; }
        public string District_Name { get; set; }
        public int District_Id { get; set; }
        public int ULB_Id { get; set; }
        public string ULB_Name { get; set; }
    }
}
