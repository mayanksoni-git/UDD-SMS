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

public partial class AdoptedParkDetail : System.Web.UI.Page
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
            int Id = 0;
            if (Request.QueryString.Count > 0)
            {
                Id = Convert.ToInt32(Request.QueryString[0].ToString());
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                Load_AdoptedPark_Name_Id(Id);
                //get_tbl_Mandal();
            }
            //get_tbl_Mandal();
            //get_tbl_Month();

            //lblCircleH.Text = Session["Default_Circle"].ToString();
            //lblDivisionH.Text = Session["Default_Division"].ToString();

            if (Session["UserType"].ToString() == "6")
            {
                try
                {

                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        //try
                        //{
                        //    ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                        //    ddlCircle_SelectedIndexChanged(ddlCircle, e);
                        //    ddlCircle.Enabled = false;
                        //}
                        //catch
                        //{ }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7")
            {
                try
                {

                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        //try
                        //{
                        //    ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                        //    ddlCircle_SelectedIndexChanged(ddlCircle, e);
                        //    ddlCircle.Enabled = false;
                        //    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        //    {//Circle
                        //        try
                        //        {
                        //            ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                        //            ddlDivision.Enabled = false;
                        //        }
                        //        catch
                        //        { }
                        //    }
                        //}
                        //catch
                        //{ }
                    }
                }
                catch
                { }
            }

            if (Request.QueryString["Division"] != null)
            {
                //int Zone = Convert.ToInt32(Request.QueryString["Zone"].ToString());
                int Circle = Convert.ToInt32(Request.QueryString["Circle"].ToString());
                int Division = Convert.ToInt32(Request.QueryString["Division"].ToString());
                int Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                int Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                SearchCrematoriumDetail obj_Search = new SearchCrematoriumDetail();
                //obj_Search.Zone = Zone;
                obj_Search.Circle = Circle;
                obj_Search.Division = Division;
                obj_Search.Year = Year;
                obj_Search.Month = Month;
            }
        }
    }


    //private void get_tbl_Mandal()
    //{
    //    DataSet ds = (new DataLayer()).get_tbl_Mandal();
    //    AllClasses.FillDropDown(ds.Tables[0], ddlMandal, "DivName", "DivisionID");
    //    if (ddlMandal.SelectedItem.Value != "0")
    //    {
    //        get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
    //    }
    //}

    //private void get_tbl_Circle(int DivisionID)
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_CircleByDivisionId(DivisionID);
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
    //    }
    //    else
    //    {
    //        ddlCircle.Items.Clear();
    //    }
    //}
    //private void get_tbl_Division(int Circle_Id)
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_Division(Circle_Id);
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
    //    }
    //    else
    //    {
    //        ddlDivision.Items.Clear();
    //    }
    //}
    //private void get_tbl_Month()
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_Month();
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //       // AllClasses.FillDropDown(ds.Tables[0], ddlMonth, "Month_MonthName", "Month_Id");
    //    }
    //    else
    //    {
    //        ddlMandal.Items.Clear();
    //    }
    //}
    protected void grdPost_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    //protected void ddlMandal_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlMandal.SelectedValue == "0")
    //    {
    //        ddlCircle.Items.Clear();
    //        ddlDivision.Items.Clear();
    //    }
    //    else
    //    {
    //        get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
    //    }
    //}
    //protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCircle.SelectedValue == "0")
    //    {
    //        ddlDivision.Items.Clear();
    //    }
    //    else
    //    {
    //        get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Collect single-entry fields
        //string selectedMandal = ddlMandal.SelectedValue;
        //string selectedCircle = ddlCircle.SelectedValue;
        //string selectedDivision = ddlDivision.SelectedValue;
        string AdoptedParkId = Id.Value.ToString();
        var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());

        // Prepare to collect multiple entries
        List<string> plantedTreeNames = new List<string>();
        List<string> speciesOfTrees = new List<string>();
        List<string> facilities = new List<string>();
        List<string> facilitiesAdded = new List<string>();
        List<string> noOfGardeners = new List<string>();
        List<string> frequencies = new List<string>();
        List<string> eventsOrganised = new List<string>();
        List<string> filePaths = new List<string>();

        // Loop through all field groups to collect data
        for (int i = 0; i < Request.Form.Count; i++)
        {
            string key = Request.Form.Keys[i];

            if (key.Contains("txtPlantedtreeName"))
            {
                plantedTreeNames.Add(Request.Form[key]);
            }
            else if (key.Contains("txtSpeciesOftree"))
            {
                speciesOfTrees.Add(Request.Form[key]);
            }
            else if (key.Contains("txFascility"))
            {
                facilities.Add(Request.Form[key]);
            }
            else if (key.Contains("txtFascilityAdded"))
            {
                facilitiesAdded.Add(Request.Form[key]);
            }
            else if (key.Contains("txtNoofGardener"))
            {
                noOfGardeners.Add(Request.Form[key]);
            }
            else if (key.Contains("txtFrequencyMaintenance"))
            {
                frequencies.Add(Request.Form[key]);
            }
            else if (key.Contains("txtEventsOrganised"))
            {
                eventsOrganised.Add(Request.Form[key]);
            }
        }

        // Handle file uploads
        for (int i = 0; i < plantedTreeNames.Count; i++)
        {
            string fileKey = "fileUploadGeotaggedPhotos[]"; // Use the name from the JavaScript array

            // Access the uploaded file
            HttpPostedFile uploadedFile = Request.Files[fileKey]; // Use index to access the right file

            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/ParkImages_And_KML"), Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName));
                uploadedFile.SaveAs(filePath);
                filePaths.Add(filePath);
            }
            else
            {
                filePaths.Add(null); // No file uploaded for this entry
            }
        }

        // Database connection
        string connectionString = ConfigurationManager.AppSettings["conn"].ToString();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            int c = 0;
            conn.Open();
            for (int i = 0; i < plantedTreeNames.Count; i++)
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ParkDetails_and_Facility (AdoptedParkDetailID, Name_of_Tree_Planted, Species_of_Planted_Trees, Facility_Available, Facility_Added, Noof_Gardener, Frequency_of_Maintenance, Events_Organised_in_Parks, Geotagged_Photos, CreatedBy, CreatedDate)" +
                    " VALUES (@AdoptedParkDetailID, @Name_of_Tree_Planted, @Species_of_Planted_Trees, @Facility_Available, @Facility_Added, @Noof_Gardener, @Frequency_of_Maintenance, @Events_Organised_in_Parks, @Geotagged_Photos, @CreatedBy, @CreatedDate)", conn))
                {
                    cmd.Parameters.AddWithValue("@AdoptedParkDetailID", AdoptedParkId);
                    cmd.Parameters.AddWithValue("@Name_of_Tree_Planted", plantedTreeNames[i]);
                    cmd.Parameters.AddWithValue("@Species_of_Planted_Trees", speciesOfTrees[i]);
                    cmd.Parameters.AddWithValue("@Facility_Available", facilities[i]);
                    cmd.Parameters.AddWithValue("@Facility_Added", facilitiesAdded[i]);
                    cmd.Parameters.AddWithValue("@Noof_Gardener", noOfGardeners[i]);
                    cmd.Parameters.AddWithValue("@Frequency_of_Maintenance", frequencies[i]);
                    cmd.Parameters.AddWithValue("@Events_Organised_in_Parks", eventsOrganised[i]);
                    cmd.Parameters.AddWithValue("@Geotagged_Photos", filePaths[i] ?? string.Empty); // Use the file path if available
                    cmd.Parameters.AddWithValue("@CreatedBy", Person_Id);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                   c= cmd.ExecuteNonQuery();
                }
            }
            if (c > 0)
            {
                using (SqlCommand cmd1 = new SqlCommand("update tbl_AdoptedParkMaster_details set IsDetailsCompleted=1 where Id='"+ AdoptedParkId + "'", conn))
                {
                    cmd1.Parameters.AddWithValue("@IsDetailsCompleted", 1);
                    cmd1.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');window.location.href ='AdoptedPark_Edit.aspx';", true);

                }
            }
        }

        // Success message


    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    // Collect single-entry fields
    //    string selectedMandal = ddlMandal.SelectedValue;
    //    string selectedCircle = ddlCircle.SelectedValue;
    //    string selectedDivision = ddlDivision.SelectedValue;
    //    string AdoptedParkId = Id.Value.ToString();
    //    var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
    //    //string selectedMonth = ddlMonth.SelectedValue;

    //    // Prepare to collect multiple entries
    //    List<string> plantedTreeNames = new List<string>();
    //    List<string> speciesOfTrees = new List<string>();
    //    List<string> facilities = new List<string>();
    //    List<string> facilitiesAdded = new List<string>();
    //    List<string> noOfGardeners = new List<string>();
    //    List<string> frequencies = new List<string>();
    //    List<string> eventsOrganised = new List<string>();
    //    List<string> filePaths = new List<string>();

    //    // Loop through all field groups to collect data
    //    for (int i = 0; i < Request.Form.Count; i++)
    //    {
    //        string key = Request.Form.Keys[i];

    //        if (key.Contains("txtPlantedtreeName"))
    //        {
    //            plantedTreeNames.Add(Request.Form[key]);
    //        }
    //        else if (key.Contains("txtSpeciesOftree"))
    //        {
    //            speciesOfTrees.Add(Request.Form[key]);
    //        }
    //        else if (key.Contains("txFascility"))
    //        {
    //            facilities.Add(Request.Form[key]);
    //        }
    //        else if (key.Contains("txtFascilityAdded"))
    //        {
    //            facilitiesAdded.Add(Request.Form[key]);
    //        }
    //        else if (key.Contains("txtNoofGardener"))
    //        {
    //            noOfGardeners.Add(Request.Form[key]);
    //        }
    //        else if (key.Contains("txtFrequencyMaintenance"))
    //        {
    //            frequencies.Add(Request.Form[key]);
    //        }
    //        else if (key.Contains("txtEventsOrganised"))
    //        {
    //            eventsOrganised.Add(Request.Form[key]);
    //        }
    //    }
    //    for (int i = 0; i < plantedTreeNames.Count; i++)
    //    {
    //        string fileKey = "fileUploadGeotaggedPhotos" + (i == 0 ? "" : "[" + i + "]"); // Use an appropriate key format

    //        HttpPostedFile uploadedFile = Request.Files[fileKey];

    //        if (uploadedFile != null && uploadedFile.ContentLength > 0)
    //        {
    //            string filePath = Path.Combine(Server.MapPath("~/ParkImages_And_KML"), Guid.NewGuid() + Path.GetExtension(uploadedFile.FileName));
    //            uploadedFile.SaveAs(filePath);
    //            filePaths.Add(filePath);
    //        }
    //        else
    //        {
    //            filePaths.Add(null); // No file uploaded for this entry
    //        }
    //    }


    //    // Database connection
    //    string connectionString = ConfigurationManager.AppSettings["conn"].ToString();

    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        conn.Open();
    //        for (int i = 0; i < plantedTreeNames.Count; i++)
    //        {
    //            using (SqlCommand cmd = new SqlCommand("INSERT INTO ParkDetails_and_Facility (AdoptedParkDetailID, Name_of_Tree_Planted, Species_of_Planted_Trees, Facility_Available, Facility_Added, Noof_Gardener, Frequency_of_Maintenance, Events_Organised_in_Parks, Geotagged_Photos,CreatedBy,CreatedDate)" +
    //                " VALUES (@AdoptedParkDetailID,@Name_of_Tree_Planted, @Species_of_Planted_Trees, @Facility_Available, @Facility_Added, @Noof_Gardener, @Frequency_of_Maintenance, @Events_Organised_in_Parks, @Geotagged_Photos,@CreatedBy,@CreatedDate)", conn))
    //            {
    //                //cmd.Parameters.AddWithValue("@Mandal", selectedMandal);
    //                //cmd.Parameters.AddWithValue("@Circle", selectedCircle);
    //                //cmd.Parameters.AddWithValue("@Division", selectedDivision);
    //                cmd.Parameters.AddWithValue("@AdoptedParkDetailID", AdoptedParkId);
    //                cmd.Parameters.AddWithValue("@Name_of_Tree_Planted", plantedTreeNames[i]);
    //                cmd.Parameters.AddWithValue("@Species_of_Planted_Trees", speciesOfTrees[i]);
    //                cmd.Parameters.AddWithValue("@Facility_Available", facilities[i]);
    //                cmd.Parameters.AddWithValue("@Facility_Added", facilitiesAdded[i]);
    //                cmd.Parameters.AddWithValue("@Noof_Gardener", noOfGardeners[i]);
    //                cmd.Parameters.AddWithValue("@Frequency_of_Maintenance", frequencies[i]);
    //                cmd.Parameters.AddWithValue("@Events_Organised_in_Parks", eventsOrganised[i]);
    //                cmd.Parameters.AddWithValue("@Geotagged_Photos", "");
    //                cmd.Parameters.AddWithValue("@CreatedBy", Person_Id);
    //                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }

    //    // Success message
    //    Response.Write("<script>alert('Data saved successfully!');</script>");
    //}
    protected void Load_AdoptedPark_Name_Id(int id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_AdoptedPark_Name_ID(id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            Id.Value = ds.Tables[0].Rows[0]["Id"].ToString();
            txtadoptedparkname.Text = ds.Tables[0].Rows[0]["AdoptedParkName"].ToString();
           
        }
    }

}
