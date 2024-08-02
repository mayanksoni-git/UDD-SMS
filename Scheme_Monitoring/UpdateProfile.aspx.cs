using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePassword : System.Web.UI.Page
{
    Pyres objPyres = new Pyres();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
                var res = objPyres.UpdateProfile(txtPersonName.Text, txtPersonFName.Text, txtLandLine.Text, txtAddress.Text, txtMobileNo2.Text, Person_Id,"", "SelectbyId");
                DataRow row = res.Rows[0];

                // Assign values from the DataRow to the text boxes
                txtPersonName.Text = row["Person_Name"].ToString();
                txtPersonFName.Text = row["Person_FName"].ToString();
                txtLandLine.Text = row["Person_TelePhone"].ToString();
                txtAddress.Text = row["Person_AddressLine1"].ToString();
                txtMobileNo2.Text = row["Person_Mobile2"].ToString();
               var  uploadimgs = row["Person_ProfilePIC"].ToString();
                if (!string.IsNullOrEmpty(uploadimgs))
                {
                    uploadimg.Src = uploadimgs;
                    ProfileUrl.Value = uploadimgs.ToString();
                }
                else
                {
                    uploadimg.Src = "assets/images/users/avatar-1.jpg";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {

        var fl = fileupload.FileName.ToString();
        string pathProfile = "", pathProfileRoot = "";
      

        if (fileupload.HasFile)
        {
            int fileSize = fileupload.PostedFile.ContentLength;
           
            // path = FileUpload1.FileName;
            //FileUpload1.SaveAs(Server.MapPath(("Images"+path)));
            string sFilename = Path.GetFileName(fileupload.PostedFile.FileName);
            int fileappent = 0;
            while (File.Exists(Server.MapPath("/Images/ProfileImage/" + sFilename)))
            {
                fileappent++;
                sFilename = Path.GetFileNameWithoutExtension(fileupload.PostedFile.FileName)
                + fileappent.ToString() + Path.GetExtension(fileupload.PostedFile.FileName).ToLower();
            }
            pathProfileRoot = Server.MapPath("/Images/ProfileImage/" + sFilename);

            //string pathProfiles = Path.Combine(pathProfileRoot, sFilename);nn

            fileupload.SaveAs(pathProfileRoot);

            pathProfile = "/Images/ProfileImage/" + sFilename;
        }

        else
        {
            pathProfile = ProfileUrl.Value;
        }
        int Person_Id= Convert.ToInt32(Session["Person_Id"].ToString());
        var res = objPyres.UpdateProfile(txtPersonName.Text, txtPersonFName.Text, txtLandLine.Text, txtAddress.Text, txtMobileNo2.Text, Person_Id, pathProfile, "Update");
        if(res.Rows.Count>0){
            DataRow row = res.Rows[0];
            var messages = row["remark"].ToString();
            if (messages == "Success")
            {
                Session["Profile_Pic"] = "";
                Session["Profile_Pic"] = pathProfile;
                Session["Person_Name"] = txtPersonName.Text;
                MessageBox.Show("Profile  Update Successfully!!");
            }
            else
            {
                MessageBox.Show("Error : Please Try Again");
            }
        }
        else
        {
            MessageBox.Show("Error : Please Try Again");
        }

    }
}