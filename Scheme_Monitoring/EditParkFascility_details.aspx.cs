using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditParkFascility_details : System.Web.UI.Page
{
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
           
            if (Request.QueryString["ParkDetailFacilityId"] != null)
            {
                int ParkDetailFacilityId = Convert.ToInt32(Request.QueryString["ParkDetailFacilityId"]);
                LoadParkFascilityToEdit(ParkDetailFacilityId);
              
            }
         
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    private void LoadParkFascilityToEdit(int ParkDetailFacilityId)
    {
        // Fetch notification details from the database
        DataSet dt = (new DataLayer()).LoadParkFascilityToEdit(ParkDetailFacilityId);
        if (dt.Tables[0].Rows.Count > 0)
        {
            DataRow row = dt.Tables[0].Rows[0];
            Id.Value = row["parkdetailFacilityId"].ToString();
            txtadoptedparkname.Text = row["AdoptedParkName"].ToString();
            txtPlantedtreeName.Text = row["name_Of_tree_Planted"].ToString();
            txtSpeciesOftree.Text = row["Species_of_Planted_Trees"].ToString();
            txtNoofGardener.Text = row["Noof_Gardener"].ToString();
            //lblGeotaggedPhotosview.Text = row["Geotagged_Photos"].ToString();
            txtEventsOrganised.Text = row["Events_Organised_in_Parks"].ToString();
            linkViewFile.Text= row["Geotagged_Photos"].ToString();

            // Set dropdowns based on the loaded data
            txFascility.SelectedValue = row["Facility_Available"].ToString();
            txtFascilityAdded.SelectedValue = row["Facility_Added"].ToString();
            txtFrequencyMaintenance.SelectedValue = row["Frequency_of_Maintenance"].ToString();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        var id = Id.Value;
        var PlantedtreeName = txtPlantedtreeName.Text;
        var SpeciesOftree = txtSpeciesOftree.Text;
        var Fascility = txFascility.SelectedValue.ToString();
        var FascilityAdded = txtFascilityAdded.SelectedValue.ToString();
        var FrequencyMaintenance = txtFrequencyMaintenance.SelectedValue.ToString();
        var Gardener = txtNoofGardener.Text.Trim();
        var EventsOrganised = txtEventsOrganised.Text.Trim();
        var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());

        HttpPostedFile uploadedFile = Geotagged_Photos.PostedFile;
        string relativePath = "";
        if (uploadedFile != null && uploadedFile.ContentLength > 0)
        {
            string filePath = Path.Combine(Server.MapPath("~/ParkImages_And_KML"), Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName));
            uploadedFile.SaveAs(filePath);
            string uniqueFileName = Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName);
             relativePath = "~/ParkImages_And_KML/" + uniqueFileName;
         
        }
        else
        {
            relativePath= linkViewFile.Text;
        }
        string connectionString = ConfigurationManager.AppSettings["conn"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {

            try
            {

              
                        string UpdateQuery = @"Update ParkDetails_and_Facility set
                                    Name_of_Tree_Planted=@Name_of_Tree_Planted, Species_of_Planted_Trees=@Species_of_Planted_Trees, 
                                            Facility_Available=@Facility_Available, Facility_Added=@Facility_Added, 
                                         Noof_Gardener=@Noof_Gardener, Frequency_of_Maintenance=@Frequency_of_Maintenance,
                                            Geotagged_Photos=@Geotagged_Photos, Events_Organised_in_Parks=@Events_Organised_in_Parks,
                                                ModifiedBy=@ModifiedBy, ModifiedDate=@ModifiedDate where ParkDetailFacilityId=@ParkDetailFacilityId";

                        using (SqlCommand cmdDetail = new SqlCommand(UpdateQuery, conn))
                        {
                            cmdDetail.Parameters.AddWithValue("@ParkDetailFacilityId", id);
                            cmdDetail.Parameters.AddWithValue("@Name_of_Tree_Planted", PlantedtreeName);
                            cmdDetail.Parameters.AddWithValue("@Species_of_Planted_Trees", SpeciesOftree);
                            cmdDetail.Parameters.AddWithValue("@Facility_Available", Fascility);
                            cmdDetail.Parameters.AddWithValue("@Facility_Added", FascilityAdded);
                            cmdDetail.Parameters.AddWithValue("@Frequency_of_Maintenance", FrequencyMaintenance);
                            cmdDetail.Parameters.AddWithValue("@Noof_Gardener", Gardener);
                            cmdDetail.Parameters.AddWithValue("@Geotagged_Photos", relativePath);
                            cmdDetail.Parameters.AddWithValue("@Events_Organised_in_Parks", EventsOrganised);
                            cmdDetail.Parameters.AddWithValue("@ModifiedBy", Person_Id);
                            cmdDetail.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                            cmdDetail.Parameters.AddWithValue("@IsDetailsCompleted", 0);
                    conn.Open();
                        int res=    cmdDetail.ExecuteNonQuery();
                    conn.Close();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data Updated Successfully !!);window.location.href ='ParkDetailsAndFascility.aspx';", true);

                    }
                        }
            }
                
            catch (Exception ex)
            {
              
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while saving data. Please try again.');", true);
            }
        }
      }
}