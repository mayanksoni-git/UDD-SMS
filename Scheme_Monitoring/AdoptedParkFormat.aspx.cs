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
            tbl_AdoptedParkMaster_details();

            get_tbl_Mandal();
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
                dr["AdoptedParkName"] = "";
                dr["ParkLatitude"] = 0;
                dr["ParkLongitude"] = 0;
                dr["SessionId"] = 0;
                dr["MonthId"] = 0;
                dr["NameCSR_NGO"] = "";
                dr["DetailCSR_NGO"] = "";
                dr["GeotaggedPhotographs"] = "";
                dr["MOUAttached"] = "";
                //dr["UploadKML"] = "";
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
                    // Insert into tbl_AdoptedParkMaster
                    string insertMasterQuery = @"INSERT INTO dbo.tbl_AdoptedParkMaster 
                (DivisionID, Circle_Id, ULBID, Ward, NoOfParkInULB, 
                 NoOfAdoptionInprocessPark, NoOfParkAdopted, CreatedBy, CreatedOn, IsActive)
                VALUES 
                (@DivisionID, @Circle_Id, @ULBID, @Ward, @NoOfParkInULB, 
                 @NoOfAdoptionInprocessPark, @NoOfParkAdopted, @CreatedBy, @CreatedOn, @IsActive);
                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdMaster = new SqlCommand(insertMasterQuery, conn, transaction);
                    cmdMaster.Parameters.AddWithValue("@DivisionID", Division);
                    cmdMaster.Parameters.AddWithValue("@Circle_Id", circle);
                    cmdMaster.Parameters.AddWithValue("@ULBID", ULB);
                    cmdMaster.Parameters.AddWithValue("@Ward", ward);
                    cmdMaster.Parameters.AddWithValue("@NoOfParkInULB", NoOfParkInULB);
                    cmdMaster.Parameters.AddWithValue("@NoOfAdoptionInprocessPark", inProcessPark);
                    cmdMaster.Parameters.AddWithValue("@NoOfParkAdopted", NoOfParkAdopted);
                    cmdMaster.Parameters.AddWithValue("@CreatedBy", Person_Id);
                    cmdMaster.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmdMaster.Parameters.AddWithValue("@IsActive", true);

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
                            FileUpload fuGeotaggedPhotographs = (FileUpload)row.FindControl("GeotaggedPhotographs");
                            FileUpload fuMOUAttached = (FileUpload)row.FindControl("MOUAttached");

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

                            //string geotaggedPhotoPath = SaveFile(((FileUpload)row.FindControl("fuGeotaggedPhotographs")).PostedFile, "GeotaggedPhotographs");
                            //string mouAttachedPath = SaveFile(((FileUpload)row.FindControl("fuMOUAttached")).PostedFile, "MOUAttached");
                            // string kmlPath = SaveFile(((FileUpload)row.FindControl("fuUploadKML")).PostedFile, "KMLUploads");

                            // Insert into tbl_AdoptedParkMaster_details
                            string insertDetailQuery = @"INSERT INTO dbo.tbl_AdoptedParkMaster_details 
                        (AdoptedParkId, AdoptedParkName, ParkLatitude, ParkLongitude, SessionId, 
                         MonthId, NameCSR_NGO, DetailCSR_NGO,GeotaggedPhotographs,MOUAttached,  CreatedBy, CreatedOn)
                        VALUES 
                        (@AdoptedParkId, @AdoptedParkName, @ParkLatitude, @ParkLongitude, @SessionId, 
                         @MonthId, @NameCSR_NGO, @DetailCSR_NGO,@GeotaggedPhotographs,@MOUAttached, @CreatedBy, @CreatedOn);";

                            SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@AdoptedParkId", masterId);
                            cmdDetail.Parameters.AddWithValue("@AdoptedParkName", adoptedParkName);
                            cmdDetail.Parameters.AddWithValue("@ParkLatitude", parkLatitude);
                            cmdDetail.Parameters.AddWithValue("@ParkLongitude", parkLongitude);
                            cmdDetail.Parameters.AddWithValue("@SessionId", SessionId); // Set appropriately
                            cmdDetail.Parameters.AddWithValue("@MonthId", monthId);
                            cmdDetail.Parameters.AddWithValue("@NameCSR_NGO", nameCSR_NGO);
                            cmdDetail.Parameters.AddWithValue("@DetailCSR_NGO", detailCSR_NGO);
                            cmdDetail.Parameters.AddWithValue("@GeotaggedPhotographs", geotaggedPhotoPath);
                            cmdDetail.Parameters.AddWithValue("@MOUAttached", mouAttached);
                            //cmdDetail.Parameters.AddWithValue("@UploadKML", kmlPath);
                            cmdDetail.Parameters.AddWithValue("@CreatedBy", Person_Id);
                            cmdDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                            //cmdDetail.Parameters.AddWithValue("@IsActive", true);

                            cmdDetail.ExecuteNonQuery();
                        }
                    }

                    // Commit transaction
                    transaction.Commit();

                    // Optionally, display a success message
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');", true);

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
        catch
        {

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

            DropDownList SessionId = e.Row.FindControl("SessionId") as DropDownList;
            DataTable dtQuestionnaire = (DataTable)ViewState["dtQuestionnaire"];
            if (AllClasses.CheckDt(dtQuestionnaire))
            {
                DataSet ds = new DataSet();
                ds = (new DataLayer()).get_tbl_FinancialYear();
                AllClasses.FillDropDown(ds.Tables[0], SessionId, "FinancialYear_Comments", "FinancialYear_Id");
                //AllClasses.FillDropDown(dtQuestionnaire, SessionId, "SessionYear", "YearID");
            }
            int sessionId = 0;
            try
            {
                sessionId = Convert.ToInt32(e.Row.Cells[5].Text.Trim());
            }
            catch
            {
                sessionId = 0;
            }
            if (sessionId > 0)
            {
                try
                {
                    SessionId.SelectedValue = sessionId.ToString();
                    //ddlIssueType_SelectedIndexChanged(ddlIssueType, e);
                }
                catch
                {

                }
            }
            DropDownList MonthId = e.Row.FindControl("MonthId") as DropDownList;
            DataTable dtQuestionnaire1 = (DataTable)ViewState["dtQuestionnaire"];
            if (AllClasses.CheckDt(dtQuestionnaire1))
            {
                DataSet ds = new DataSet();
                ds = (new DataLayer()).get_tbl_Month();
                AllClasses.FillDropDown(ds.Tables[0], MonthId, "Month_MonthName", "Month_Id");
            }
            int monthId = 0;
            try
            {
                monthId = Convert.ToInt32(e.Row.Cells[6].Text.Trim());
            }
            catch
            {
                monthId = 0;
            }
            if (monthId > 0)
            {
                try
                {
                    MonthId.SelectedValue = sessionId.ToString();
                    //ddlIssueType_SelectedIndexChanged(ddlIssueType, e);
                }
                catch
                {

                }
            }
            string filePath = e.Row.Cells[9].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[1].BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkULBShr");
                //lnkBtn.Visible = false;
            }
        }
    }
    private void AddDynamicFields()
    {
        DataTable dtQuestionnaire;
    
            if (ViewState["dtQuestionnaire"] != null)
            {
                dtQuestionnaire = (DataTable)(ViewState["dtQuestionnaire"]);
                DataRow dr = dtQuestionnaire.NewRow();
                dtQuestionnaire.Rows.Add(dr);
                ViewState["dtQuestionnaire"] = dtQuestionnaire;

                grdCallProductDtls.DataSource = dtQuestionnaire;
                grdCallProductDtls.DataBind();
            }
            else
            {
                dtQuestionnaire = new DataTable();

                //DataColumn dc_ProjectWorkGO_Id = new DataColumn("ProjectWorkGO_Id", typeof(int));
                DataColumn txtAdoptedParkName = new DataColumn("txtAdoptedParkName", typeof(string));
                DataColumn txtLatitude = new DataColumn("txtLatitude", typeof(string));
                DataColumn txtLongitude = new DataColumn("txtLongitude", typeof(decimal));
                DataColumn ddlYear = new DataColumn("ddlYear", typeof(decimal));
                DataColumn ddlMonth = new DataColumn("ddlMonth", typeof(decimal));
                DataColumn txtNameCSR_NGO = new DataColumn("txtNameCSR_NGO", typeof(decimal));
                DataColumn txtDetailCSR_NGO = new DataColumn("txtDetailCSR_NGO", typeof(string));
                DataColumn GeotaggedPhotographs = new DataColumn("GeotaggedPhotographs", typeof(string));
                DataColumn fileUploadMOUAttached = new DataColumn("fileUploadMOUAttached", typeof(string));
                DataColumn UploadKML = new DataColumn("UploadKML", typeof(string));

                dtQuestionnaire.Columns.AddRange(new DataColumn[] { txtAdoptedParkName, txtLatitude, txtLongitude, ddlYear, ddlMonth, txtNameCSR_NGO, txtDetailCSR_NGO, GeotaggedPhotographs, fileUploadMOUAttached, UploadKML });

                DataRow dr = dtQuestionnaire.NewRow();
                dtQuestionnaire.Rows.Add(dr);
                ViewState["dtQuestionnaire"] = dtQuestionnaire;

                grdCallProductDtls.DataSource = dtQuestionnaire;
                grdCallProductDtls.DataBind();

            }
        
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
}