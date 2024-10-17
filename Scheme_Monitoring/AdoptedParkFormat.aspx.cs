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

public partial class AdoptedParkFormat : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (ViewState["dtQuestionnaire"] != null)
        {
            AddDynamicFields(); // Recreate GridView rows
        }
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
            int ParkId = 0;
            if (Request.QueryString.Count > 0)
            {
                ParkId = Convert.ToInt32(Request.QueryString[0].ToString());
                Id = Convert.ToInt32(Request.QueryString[1].ToString());
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                Load_AdoptedPark_Ward_Count(ParkId);
                get_tbl_ParkNames(ParkId);
                get_tbl_Mandal();
            }
            else
            {
                tbl_AdoptedParkMaster_details();
                get_tbl_Mandal();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            //get_tbl_Month();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            
            if (Session["UserType"].ToString() == "6")
            {
                try
                {
                 
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" )
            {
                try
                {
                    
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
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
        //else
        //{
        //    tbl_AdoptedParkMaster_details();
        //}
    }

  
    private void get_tbl_Mandal()
    {
        DataSet ds = (new DataLayer()).get_tbl_Mandal();
        AllClasses.FillDropDown(ds.Tables[0], ddlMandal, "DivName", "DivisionID");
        if (ddlMandal.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
        }
    }

    private void get_tbl_Circle(int DivisionID)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_CircleByDivisionId(DivisionID);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }
    //private void get_tbl_month()
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_Month();
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        AllClasses.FillDropDown(ds.Tables[0], MonthId, "month_monthname", "month_id");
    //    }
    //    else
    //    {
    //        //ddlmandal.items.clear();
    //    }
    //}
    private void tbl_AdoptedParkMaster_details()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).tbl_AdoptedParkMaster_details();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtQuestionnaire"] = ds.Tables[0];
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                //dr["ProjectWorkGO_Id"] = 0;
                dr["Id"] = 0;
                dr["AdoptedParkName"] = "";
                dr["ParkLatitude"] = 0;
                dr["ParkLongitude"] = 0;
                dr["SessionId"] = 0;
                dr["MonthId"] = 0;
                dr["NameCSR_NGO"] = "";
                dr["DetailCSR_NGO"] = "";
                dr["GeotaggedPhotographs"] = "";
                dr["MOUAttached"] = "";
                dr["UploadKML"] = "";
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtQuestionnaire"] = ds.Tables[0];
                grdCallProductDtls.DataSource = ds.Tables[0];
                grdCallProductDtls.DataBind();
            }
            catch(Exception ex)
            {
                grdCallProductDtls.DataSource = null;
                grdCallProductDtls.DataBind();
            }
        }
    }
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
   protected void ddlMandal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMandal.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
        }
    }
    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var id = Convert.ToInt32(Id.Value);
        if (id > 0 || id != 0)
        {
            try
            {
                if (!ValidateFields())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All Fields required.');", true);
                    return;
                }

                var Division = Convert.ToInt32(ddlMandal.SelectedValue);
                var circle = Convert.ToInt32(ddlCircle.SelectedValue);
                var ULB = Convert.ToInt32(ddlDivision.SelectedValue);
                var ward = txtWard.Text.Trim();
                var NoOfParkInULB = txtNoOfParkInULB.Text.Trim();
                var NoOfParkAdopted = txtNoOfParkAdopted.Text.Trim();
                var inProcessPark = txtAdoptionInProcess.Text.Trim();
                var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());

                string connectionString = ConfigurationManager.AppSettings["conn"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {

                        string UpdateQuery = @"Update dbo.tbl_AdoptedParkMaster set DivisionID=@DivisionID
                        , Circle_Id=@Circle_Id, ULBID=@ULBID, Ward=@Ward, NoOfParkInULB=@NoOfParkInULB, 
                 NoOfAdoptionInprocessPark=@NoOfAdoptionInprocessPark, NoOfParkAdopted=@NoOfParkAdopted, UpdatedBy=@UpdatedBy, UpdatedOn=@UpdatedOn where Id='" + id + "'";

                        SqlCommand cmdMaster = new SqlCommand(UpdateQuery, conn, transaction);
                        cmdMaster.Parameters.AddWithValue("@Id", id);
                        cmdMaster.Parameters.AddWithValue("@DivisionID", Division);
                        cmdMaster.Parameters.AddWithValue("@Circle_Id", circle);
                        cmdMaster.Parameters.AddWithValue("@ULBID", ULB);
                        cmdMaster.Parameters.AddWithValue("@Ward", ward);
                        cmdMaster.Parameters.AddWithValue("@NoOfParkInULB", NoOfParkInULB);
                        cmdMaster.Parameters.AddWithValue("@NoOfAdoptionInprocessPark", inProcessPark);
                        cmdMaster.Parameters.AddWithValue("@NoOfParkAdopted", NoOfParkAdopted);
                        cmdMaster.Parameters.AddWithValue("@UpdatedBy", Person_Id);
                        cmdMaster.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                        //cmdMaster.Parameters.AddWithValue("@IsActive", true);

                        // Execute and get the new master Id
                       int masterId = Convert.ToInt32(cmdMaster.ExecuteScalar());

                        // Iterate through GridView rows to save details
                        foreach (GridViewRow row in grdCallProductDtls.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                var adoptedParkName = ((TextBox)row.FindControl("AdoptedParkName")).Text.Trim();
                                var parkLatitude = ((TextBox)row.FindControl("ParkLatitude")).Text.Trim();
                                var parkLongitude = ((TextBox)row.FindControl("ParkLongitude")).Text.Trim();
                                //var monthId = Convert.ToInt32(((TextBox)row.FindControl("MonthId")).Text.Trim());
                                var monthId = Convert.ToInt32(((DropDownList)row.FindControl("MonthId")).SelectedValue);
                                //var SessionId = Convert.ToInt32(((TextBox)row.FindControl("SessionId")).Text.Trim());
                                var SessionId = Convert.ToInt32(((DropDownList)row.FindControl("SessionId")).SelectedValue);
                                var nameCSR_NGO = ((TextBox)row.FindControl("NameCSR_NGO")).Text.Trim();
                                var detailCSR_NGO = ((TextBox)row.FindControl("DetailCSR_NGO")).Text.Trim();
                                string geotaggedPhotoPath = "";
                                string mouAttached = "";
                                string uploadKML = "";
                                FileUpload fuGeotaggedPhotographs = (FileUpload)row.FindControl("GeotaggedPhotographs");
                                FileUpload fuMOUAttached = (FileUpload)row.FindControl("MOUAttached");
                                FileUpload fuUploadKML = (FileUpload)row.FindControl("UploadKML");

                                if (fuGeotaggedPhotographs != null && fuGeotaggedPhotographs.HasFile)
                                {
                                    geotaggedPhotoPath = SaveFile(fuGeotaggedPhotographs.PostedFile);
                                }
                                else
                                {

                                    geotaggedPhotoPath = string.Empty;
                                }
                                if (fuMOUAttached != null && fuMOUAttached.HasFile)
                                {
                                    mouAttached = SaveFile(fuMOUAttached.PostedFile);
                                }
                                else
                                {

                                    mouAttached = string.Empty;
                                }
                                if (fuUploadKML != null && fuUploadKML.HasFile)
                                {
                                    uploadKML = SaveFile(fuUploadKML.PostedFile);
                                }
                                else
                                {

                                    uploadKML = string.Empty;
                                }
                                string deletequery = @"Delete from dbo.tbl_AdoptedParkMaster_details  where AdoptedParkId='" + id + "' ";
                                SqlCommand cmdDetail1 = new SqlCommand(deletequery, conn, transaction);
                                cmdDetail1.Parameters.AddWithValue("@Id", id);
                                cmdDetail1.ExecuteNonQuery();


                                string insertDetailQuery = @"INSERT INTO dbo.tbl_AdoptedParkMaster_details 
                        (AdoptedParkId, AdoptedParkName, ParkLatitude, ParkLongitude, SessionId, 
                         MonthId, NameCSR_NGO, DetailCSR_NGO,GeotaggedPhotographs,MOUAttached,UploadKML,  CreatedBy, CreatedOn)
                        VALUES 
                        (@AdoptedParkId, @AdoptedParkName, @ParkLatitude, @ParkLongitude, @SessionId, 
                         @MonthId, @NameCSR_NGO, @DetailCSR_NGO,@GeotaggedPhotographs,@MOUAttached,@UploadKML, @CreatedBy, @CreatedOn);";

                                SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, transaction);
                                cmdDetail.Parameters.AddWithValue("@AdoptedParkId", id);
                                cmdDetail.Parameters.AddWithValue("@AdoptedParkName", adoptedParkName);
                                cmdDetail.Parameters.AddWithValue("@ParkLatitude", parkLatitude);
                                cmdDetail.Parameters.AddWithValue("@ParkLongitude", parkLongitude);
                                cmdDetail.Parameters.AddWithValue("@SessionId", SessionId); // Set appropriately
                                cmdDetail.Parameters.AddWithValue("@MonthId", monthId);
                                cmdDetail.Parameters.AddWithValue("@NameCSR_NGO", nameCSR_NGO);
                                cmdDetail.Parameters.AddWithValue("@DetailCSR_NGO", detailCSR_NGO);
                                cmdDetail.Parameters.AddWithValue("@GeotaggedPhotographs", geotaggedPhotoPath);
                                cmdDetail.Parameters.AddWithValue("@MOUAttached", mouAttached);
                                cmdDetail.Parameters.AddWithValue("@UploadKML", uploadKML);
                                cmdDetail.Parameters.AddWithValue("@CreatedBy", Person_Id);
                                cmdDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                                //cmdDetail.Parameters.AddWithValue("@IsActive", true);

                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction
                        transaction.Commit();

                        // Optionally, display a success message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');window.location.href ='AdoptedPark_Edit.aspx';", true);

                        // Clear the form or redirect as needed
                        ClearForm();

                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction
                        transaction.Rollback();

                        // Log the error (implement logging as per your project)
                        // LogError(ex);

                        // Display an error message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while saving data. Please try again.');", true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            try
            {
                if (!ValidateFields())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All Fields required.');", true);
                    return;
                }

                var Division1 = Convert.ToInt32(ddlMandal.SelectedValue);
                var circle1 = Convert.ToInt32(ddlCircle.SelectedValue);
                var ULB1 = Convert.ToInt32(ddlDivision.SelectedValue);
                var ward1 = txtWard.Text.Trim();
                var NoOfParkInULB1 = txtNoOfParkInULB.Text.Trim();
                var NoOfParkAdopted1 = txtNoOfParkAdopted.Text.Trim();
                var inProcessPark1 = txtAdoptionInProcess.Text.Trim();
                var Person_Id1 = Convert.ToInt32(Session["Person_Id"].ToString());

                string connectionString1 = ConfigurationManager.AppSettings["conn"].ToString();
                using (SqlConnection conn1 = new SqlConnection(connectionString1))
                {
                    conn1.Open();
                    SqlTransaction transaction1 = conn1.BeginTransaction();

                    try
                    {
                        // Insert into tbl_AdoptedParkMaster
                        string insertMasterQuery = @"INSERT INTO dbo.tbl_AdoptedParkMaster 
                (DivisionID, Circle_Id, ULBID, Ward, NoOfParkInULB, 
                 NoOfAdoptionInprocessPark, NoOfParkAdopted, CreatedBy, CreatedOn, IsActive)
                VALUES 
                (@DivisionID, @Circle_Id, @ULBID, @Ward, @NoOfParkInULB, 
                 @NoOfAdoptionInprocessPark, @NoOfParkAdopted, @CreatedBy, @CreatedOn, @IsActive);
                SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdMaster = new SqlCommand(insertMasterQuery, conn1, transaction1);
                        cmdMaster.Parameters.AddWithValue("@DivisionID", Division1);
                        cmdMaster.Parameters.AddWithValue("@Circle_Id", circle1);
                        cmdMaster.Parameters.AddWithValue("@ULBID", ULB1);
                        cmdMaster.Parameters.AddWithValue("@Ward", ward1);
                        cmdMaster.Parameters.AddWithValue("@NoOfParkInULB", NoOfParkInULB1);
                        cmdMaster.Parameters.AddWithValue("@NoOfAdoptionInprocessPark", inProcessPark1);
                        cmdMaster.Parameters.AddWithValue("@NoOfParkAdopted", NoOfParkAdopted1);
                        cmdMaster.Parameters.AddWithValue("@CreatedBy", Person_Id1);
                        cmdMaster.Parameters.AddWithValue("@CreatedOn", DateTime.Now);  
                        cmdMaster.Parameters.AddWithValue("@IsActive", true);

                        // Execute and get the new master Id
                        int masterId1 = Convert.ToInt32(cmdMaster.ExecuteScalar());

                        // Iterate through GridView rows to save details
                        foreach (GridViewRow row1 in grdCallProductDtls.Rows)
                        {
                            if (row1.RowType == DataControlRowType.DataRow)
                            {
                                var adoptedParkName1 = ((TextBox)row1.FindControl("AdoptedParkName")).Text.Trim();
                                var parkLatitude1 = ((TextBox)row1.FindControl("ParkLatitude")).Text.Trim();
                                var parkLongitude1 = ((TextBox)row1.FindControl("ParkLongitude")).Text.Trim();
                                //var monthId = Convert.ToInt32(((TextBox)row.FindControl("MonthId")).Text.Trim());
                                var monthId1 = Convert.ToInt32(((DropDownList)row1.FindControl("MonthId")).SelectedValue);
                                //var SessionId = Convert.ToInt32(((TextBox)row.FindControl("SessionId")).Text.Trim());
                                var SessionId1 = Convert.ToInt32(((DropDownList)row1.FindControl("SessionId")).SelectedValue);
                                var nameCSR_NGO1 = ((TextBox)row1.FindControl("NameCSR_NGO")).Text.Trim();
                                var detailCSR_NGO1 = ((TextBox)row1.FindControl("DetailCSR_NGO")).Text.Trim();
                                string geotaggedPhotoPath1 = "";
                                string mouAttached1 = "";
                                string uploadKML1 = "";
                                FileUpload fuGeotaggedPhotographs1 = (FileUpload)row1.FindControl("GeotaggedPhotographs");
                                FileUpload fuMOUAttached1 = (FileUpload)row1.FindControl("MOUAttached");
                                FileUpload fuUploadKML1 = (FileUpload)row1.FindControl("UploadKML");

                                if (fuGeotaggedPhotographs1 != null && fuGeotaggedPhotographs1.HasFile)
                                {
                                    geotaggedPhotoPath1 = SaveFile(fuGeotaggedPhotographs1.PostedFile);
                                }
                                else
                                {

                                    geotaggedPhotoPath1 = string.Empty;
                                }
                                if (fuMOUAttached1 != null && fuMOUAttached1.HasFile)
                                {
                                    mouAttached1 = SaveFile(fuMOUAttached1.PostedFile);
                                }
                                else
                                {

                                    mouAttached1 = string.Empty;
                                }
                                if (fuUploadKML1 != null && fuUploadKML1.HasFile)
                                {
                                    uploadKML1 = SaveFile(fuUploadKML1.PostedFile);
                                }
                                else
                                {

                                    uploadKML1 = string.Empty;
                                }
                                //string geotaggedPhotoPath = SaveFile(((FileUpload)row.FindControl("fuGeotaggedPhotographs")).PostedFile, "GeotaggedPhotographs");
                                //string mouAttachedPath = SaveFile(((FileUpload)row.FindControl("fuMOUAttached")).PostedFile, "MOUAttached");
                                // string kmlPath = SaveFile(((FileUpload)row.FindControl("fuUploadKML")).PostedFile, "KMLUploads");

                                // Insert into tbl_AdoptedParkMaster_details
                                string insertDetailQuery = @"INSERT INTO dbo.tbl_AdoptedParkMaster_details 
                        (AdoptedParkId, AdoptedParkName, ParkLatitude, ParkLongitude, SessionId, 
                         MonthId, NameCSR_NGO, DetailCSR_NGO,GeotaggedPhotographs,MOUAttached,UploadKML,  CreatedBy, CreatedOn)
                        VALUES 
                        (@AdoptedParkId, @AdoptedParkName, @ParkLatitude, @ParkLongitude, @SessionId, 
                         @MonthId, @NameCSR_NGO, @DetailCSR_NGO,@GeotaggedPhotographs,@MOUAttached,@UploadKML, @CreatedBy, @CreatedOn);";

                                SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn1, transaction1);
                                cmdDetail.Parameters.AddWithValue("@AdoptedParkId", masterId1);
                                cmdDetail.Parameters.AddWithValue("@AdoptedParkName", adoptedParkName1);
                                cmdDetail.Parameters.AddWithValue("@ParkLatitude", parkLatitude1);
                                cmdDetail.Parameters.AddWithValue("@ParkLongitude", parkLongitude1);
                                cmdDetail.Parameters.AddWithValue("@SessionId", SessionId1); // Set appropriately
                                cmdDetail.Parameters.AddWithValue("@MonthId", monthId1);
                                cmdDetail.Parameters.AddWithValue("@NameCSR_NGO", nameCSR_NGO1);
                                cmdDetail.Parameters.AddWithValue("@DetailCSR_NGO", detailCSR_NGO1);
                                cmdDetail.Parameters.AddWithValue("@GeotaggedPhotographs", geotaggedPhotoPath1);
                                cmdDetail.Parameters.AddWithValue("@MOUAttached", mouAttached1);
                                cmdDetail.Parameters.AddWithValue("@UploadKML", uploadKML1);
                                cmdDetail.Parameters.AddWithValue("@CreatedBy", Person_Id1);
                                cmdDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                                //cmdDetail.Parameters.AddWithValue("@IsActive", true);

                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction
                        transaction1.Commit();

                        // Optionally, display a success message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');window.location.href ='AdoptedPark_Edit.aspx';", true);


                        // Clear the form or redirect as needed
                        ClearForm();
                        //Response.Redirect("");

                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction
                        transaction1.Rollback();

                        // Log the error (implement logging as per your project)
                        // LogError(ex);

                        // Display an error message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while saving data. Please try again.');", true);
                    }
                }
            }
            catch
            {

            }
        }
    }

    private string SaveFile(HttpPostedFile file)
    {
        if (file != null && file.ContentLength > 0)
        {
            string folderPath = Server.MapPath("~/ParkImages_And_KML");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Path.GetFileName(file.FileName);
            string uniqueFileName = Guid.NewGuid()+"_"+fileName;
            string filePath = Path.Combine(folderPath, uniqueFileName);
            file.SaveAs(filePath);

            // Return relative path to store in DB
            return "~/ParkImages_And_KML/"+uniqueFileName;
        }

        return string.Empty;
    }
    public bool ValidateFields()
    {
        if (ddlMandal.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlMandal.Focus();
            return false;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            return false;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            return false;
        }
        //if (ddlFY.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select a Financial year. ");
        //    ddlFY.Focus();
        //    return false;
        //}
        if (txtWard.Text == "" || txtWard.Text == null)
        {
            MessageBox.Show("Please Enter Ward. ");
            txtWard.Focus();
            return false;
        }
        if (txtNoOfParkInULB.Text == "" || txtNoOfParkInULB.Text == null)
        {
            MessageBox.Show("Please Enter No Of Park in ULB. ");
            txtNoOfParkInULB.Focus();
            return false;
        }
        if (txtAdoptionInProcess.Text == "" || txtAdoptionInProcess.Text == null)
        {
            MessageBox.Show("Please Enter Park Adoption In Progress. ");
            txtAdoptionInProcess.Focus();
            return false;
        } 
        if (txtNoOfParkAdopted.Text == "" || txtNoOfParkAdopted.Text == null)
        {
            MessageBox.Show("Please Enter No of Park Adopted. ");
            txtNoOfParkAdopted.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }
    private void ClearForm()
    {
        // Clear master fields
        ddlDivision.SelectedIndex = 0;
        ddlCircle.SelectedIndex = 0;
        ddlMandal.SelectedIndex = 0;
        txtWard.Text = string.Empty;
        txtNoOfParkInULB.Text = string.Empty;
        txtAdoptionInProcess.Text = string.Empty;
        txtNoOfParkAdopted.Text = string.Empty;

        // Clear detail sections
        // Implement as needed, possibly by reloading the page or resetting the sections
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {

    }

    protected void grdCallProductDtls_PreRender(object sender, EventArgs e)
    {

        if (ViewState["dtQuestionnaire"] != null)
        {
            grdCallProductDtls.DataSource = (DataTable)ViewState["dtQuestionnaire"];
            grdCallProductDtls.DataBind();
        }
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
    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtQuestionnaire = (DataTable)ViewState["dtQuestionnaire"];
            int rowIndex = e.Row.DataItemIndex;

            if (rowIndex < dtQuestionnaire.Rows.Count)
            {
                DataRow dr = dtQuestionnaire.Rows[rowIndex];

                // Populate the Month dropdown
                DropDownList MonthId = (DropDownList)e.Row.FindControl("MonthId");
                if (AllClasses.CheckDt(dtQuestionnaire))
                {
                    DataSet ds = new DataSet();
                    ds = (new DataLayer()).get_tbl_Month();
                    AllClasses.FillDropDown(ds.Tables[0], MonthId, "Month_MonthName", "Month_Id");
                }

                // Retrieve the monthId from the DataRow, ensuring it's from the right column
                int monthId;
                if (int.TryParse(dr["MonthId"].ToString(), out monthId) && monthId > 0)
                {
                    // Set the selected value of the dropdown
                    MonthId.SelectedValue = monthId.ToString();
                }
                DropDownList SessionId = e.Row.FindControl("SessionId") as DropDownList;
                if (AllClasses.CheckDt(dtQuestionnaire))
                {
                    DataSet ds = new DataSet();
                    ds = (new DataLayer()).get_FinancialYear();
                    AllClasses.FillDropDown(ds.Tables[0], SessionId, "SessionYear", "YearID");
                }
                int sessionId;
                if (int.TryParse(dr["SessionId"].ToString(), out sessionId) && sessionId > 0)
                {
                    // Set the selected value of the dropdown
                    SessionId.SelectedValue = sessionId.ToString();
                }
                // Set other TextBox values
                ((TextBox)e.Row.FindControl("AdoptedParkName")).Text = dr["AdoptedParkName"].ToString();
                ((TextBox)e.Row.FindControl("ParkLatitude")).Text = dr["ParkLatitude"].ToString();
                ((TextBox)e.Row.FindControl("ParkLongitude")).Text = dr["ParkLongitude"].ToString();
                ((TextBox)e.Row.FindControl("NameCSR_NGO")).Text = dr["NameCSR_NGO"].ToString();
                ((TextBox)e.Row.FindControl("DetailCSR_NGO")).Text = dr["DetailCSR_NGO"].ToString();
            }
        }
    }

    
    private void AddDynamicFields()
    {
        DataTable dtQuestionnaire;

        if (ViewState["dtQuestionnaire"] != null)
        {
            dtQuestionnaire = (DataTable)ViewState["dtQuestionnaire"];

            // Preserve existing data from GridView into DataTable
            foreach (GridViewRow row in grdCallProductDtls.Rows)
            {
                DataRow dr = dtQuestionnaire.Rows[row.RowIndex];
                
                // Extract values from TextBoxes and DropDownLists
                dr["AdoptedParkName"] = ((TextBox)row.FindControl("AdoptedParkName")).Text.Trim();
                //dr["ParkLatitude"] = ((TextBox)row.FindControl("ParkLatitude")).Text.Trim();

                decimal latitude;
                dr["ParkLatitude"] = decimal.TryParse(((TextBox)row.FindControl("ParkLatitude")).Text.Trim(), out latitude) ? latitude : 0;
                 decimal longitude;
                dr["ParkLongitude"] = decimal.TryParse(((TextBox)row.FindControl("ParkLongitude")).Text.Trim(), out longitude) ? longitude : 0;

                decimal sessionId;
                dr["SessionId"] = decimal.TryParse(((DropDownList)row.FindControl("SessionId")).SelectedValue, out sessionId) ? sessionId : 0;

                decimal monthId;
                dr["MonthId"] = decimal.TryParse(((DropDownList)row.FindControl("MonthId")).SelectedValue, out monthId) ? monthId : 0;

                dr["NameCSR_NGO"] = ((TextBox)row.FindControl("NameCSR_NGO")).Text.Trim();
                dr["DetailCSR_NGO"] = ((TextBox)row.FindControl("DetailCSR_NGO")).Text.Trim();

                //// Handle FileUpload controls
                //ProcessFileUpload(row, dr, "GeotaggedPhotographs");
                //ProcessFileUpload(row, dr, "MOUAttached");
                //ProcessFileUpload(row, dr, "UploadKML");
                FileUpload fuGeotaggedPhotographs = (FileUpload)row.FindControl("GeotaggedPhotographs");
                if (fuGeotaggedPhotographs.HasFile)
                {
                    string filePath = SaveFile(fuGeotaggedPhotographs.PostedFile);
                    dr["GeotaggedPhotographs"] = filePath;
                }
                else if (dr["GeotaggedPhotographs"] != DBNull.Value) // Check for existing value
                {
                    dr["GeotaggedPhotographs"] = dr["GeotaggedPhotographs"];
                }

                FileUpload fuMOUAttached = (FileUpload)row.FindControl("MOUAttached");
                if (fuMOUAttached.HasFile)
                {
                    string filePath = SaveFile(fuMOUAttached.PostedFile);
                    dr["MOUAttached"] = filePath;
                }
                else if (dr["MOUAttached"] != DBNull.Value) // Check for existing value
                {
                    dr["MOUAttached"] = dr["MOUAttached"];
                }

                FileUpload fuUploadKML = (FileUpload)row.FindControl("UploadKML");
                if (fuUploadKML.HasFile)
                {
                    string filePath = SaveFile(fuUploadKML.PostedFile);
                    dr["UploadKML"] = filePath;
                }
                else if (dr["UploadKML"] != DBNull.Value) // Check for existing value
                {
                    dr["UploadKML"] = dr["UploadKML"];
                }
            }

            // Add a new empty row
            DataRow newRow = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(newRow);
        }
        else
        {
            // Initialize DataTable if ViewState is null
            dtQuestionnaire = InitializeDataTable();

            // Add the first empty row
            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
        }

        // Update ViewState and rebind GridView
        ViewState["dtQuestionnaire"] = dtQuestionnaire;
        grdCallProductDtls.DataSource = dtQuestionnaire;
        grdCallProductDtls.DataBind();
    }

    

    private void ProcessFileUpload(GridViewRow row, DataRow dr, string controlID)
    {
        FileUpload fu = (FileUpload)row.FindControl(controlID);
        if (fu != null && fu.HasFile)
        {
            string filePath = SaveFile(fu.PostedFile);
            dr[controlID] = filePath;
        }
    }

    private DataTable InitializeDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("AdoptedParkName", typeof(string));
        dt.Columns.Add("ParkLatitude", typeof(string));
        dt.Columns.Add("ParkLongitude", typeof(decimal));
        dt.Columns.Add("SessionId", typeof(decimal));
        dt.Columns.Add("MonthId", typeof(decimal));
        dt.Columns.Add("NameCSR_NGO", typeof(string));
        dt.Columns.Add("DetailCSR_NGO", typeof(string));
        dt.Columns.Add("GeotaggedPhotographs", typeof(string));
        dt.Columns.Add("MOUAttached", typeof(string));
        dt.Columns.Add("UploadKML", typeof(string));
        return dt;
    }

    
    protected void DeleteDetails_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void btnDynamic_Click(object sender, ImageClickEventArgs e)
    {
        AddDynamicFields();
    }

    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtQuestionnaire"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtQuestionnaire"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdCallProductDtls.DataSource = dt;
            grdCallProductDtls.DataBind();
            ViewState["dtQuestionnaire"] = dt;
        }
    }

    protected void Load_AdoptedPark_Ward_Count(int ParkId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_AdoptedPark_Edit(ParkId);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlMandal.SelectedValue = ds.Tables[0].Rows[0]["DivisionId"].ToString();
            }
            catch
            {
                ddlMandal.SelectedValue = "0";
            }
            try
            {
                ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Circle_Id"].ToString();

            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            try
            {
                //get_tbl_Division(Convert.ToInt32(ds.Tables[0].Rows[0]["Circle_Id"]));
                ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["ULBID"].ToString(); 
               
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            Id.Value = ds.Tables[0].Rows[0]["Id"].ToString();
            txtWard.Text = ds.Tables[0].Rows[0]["Ward"].ToString();
            txtNoOfParkInULB.Text = ds.Tables[0].Rows[0]["NoOfParkInULB"].ToString();
            txtAdoptionInProcess.Text = ds.Tables[0].Rows[0]["NoOfAdoptionInprocessPark"].ToString();
            txtNoOfParkAdopted.Text = ds.Tables[0].Rows[0]["NoOfParkAdopted"].ToString();
        }
    }
    private void get_tbl_ParkNames(int ParkId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ParkNames(ParkId);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtQuestionnaire"] = ds.Tables[0];
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
            //txtTotalReleseAmount.Text = ds.Tables[0].Compute("sum(ProjectWorkGO_TotalRelease)", "").ToString();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                //dr["ProjectWorkGO_Id"] = 0;
                dr["AdoptedParkName"] = "";
                dr["ParkLatitude"] = 0;
                dr["ParkLongitude"] = 0;
                dr["SessionId"] = 0;
                dr["MonthId"] = 0;
                dr["NameCSR_NGO"] = "";
                dr["DetailCSR_NGO"] = "";
                dr["GeotaggedPhotographs"] = "";
                dr["MOUAttached"] = "";
                dr["UploadKML"] = "";
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtQuestionnaire"] = ds.Tables[0];
                grdCallProductDtls.DataSource = ds.Tables[0];
                grdCallProductDtls.DataBind();
            }
            catch (Exception ex)
            {
                grdCallProductDtls.DataSource = null;
                grdCallProductDtls.DataBind();
            }
        }
    }
}