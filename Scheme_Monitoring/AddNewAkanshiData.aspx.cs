using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddNewAkanshiData : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
    List<tbl_ProjectWorkPkgTemp> obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
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
            int newAkanshiId = 0;
            int newAkanshiDetail_Id = 0;
            if (Request.QueryString.Count > 0)
            {
                newAkanshiId = Convert.ToInt32(Request.QueryString[0].ToString());
                newAkanshiDetail_Id = Convert.ToInt32(Request.QueryString[1].ToString());
                Load_CmFellow(newAkanshiId);
                LoadAkanshiHead(newAkanshiId);
            }
                lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            //get_tbl_AkanshiHead();
            get_tbl_FinancialYear();
            get_tbl_Zone();

            if (Request.QueryString.Count > 0)
            {
            }
            else
            {
                Bind_EmptyFields();
            }
        }
        KeepDataOnChange();
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
    }

    //private void get_tbl_AkanshiHead()
    //{
    //    DataTable dt = new DataTable();
    //    dt = (new MasterDate()).GetAkanshiHead(-1, "");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ViewState["dtAkanshiHead"] = dt;
    //    }
    //    else
    //    {
    //        ViewState["dtAkanshiHead"] = null;
    //    }
    //}

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearFromId(19);
        AllClasses.FillDropDown(ds.Tables[0], ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
            if (ddlZone.SelectedItem.Value != "0")
            {
                get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
            }
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }

    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
            if (ddlCircle.SelectedItem.Value != "0")
            {
                get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
            }
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division_akankshi(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        HashSet<int> encounteredIds = new HashSet<int>();
        bool hasDuplicates = false;

        foreach (GridViewRow row in grdAkanshiHead.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                var ddlAkanshiHead = Convert.ToInt32(((DropDownList)row.FindControl("ddlAkanshiHead")).SelectedValue);
                if (!encounteredIds.Add(ddlAkanshiHead))
                {
                    hasDuplicates = true;
                    break; // Exit the loop early if a duplicate is found
                }
            }
        }

        if (hasDuplicates)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Duplicate AkanshiHead  found. Please ensure each AkanshiHead is unique.');", true);
            return; // Prevent further processing
        }
        //var Akanshi_id = Convert.ToInt32(newAkanshi_Id.Value);
        int Akanshi_id = 0; // Initialize with a default value (like 0 or -1)
        if (!string.IsNullOrEmpty(newAkanshi_Id.Value))
        {
            Akanshi_id = Convert.ToInt32(newAkanshi_Id.Value);
        }

        if (Akanshi_id > 0) 
        {
            try
            {
                if (!ValidateFields())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All Fields required.');", true);
                    return;
                }
                var FY = Convert.ToInt32(ddlFY.SelectedValue);
                var circle = Convert.ToInt32(ddlCircle.SelectedValue);
                var ULB = Convert.ToInt32(ddlDivision.SelectedValue);
                var Zone = Convert.ToInt32(ddlZone.SelectedValue);
                var CMFellowName = txtCMFellowName.Text.Trim();
                var TotalTransferred = txtTotalTransferred.Text.Trim();
                var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());

                // Retrieve TotalAmount from the GridView Footer
                decimal totalAmount = 0;
                TextBox txtTotalAmount = (TextBox)grdAkanshiHead.FooterRow.FindControl("TotalAmount");
                if (!string.IsNullOrEmpty(txtTotalAmount.Text.Trim()))
                {
                    totalAmount = Convert.ToDecimal(txtTotalAmount.Text.Trim());
                }
                string connectionString = ConfigurationManager.AppSettings["conn"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {

                        string UpdateQuery = @"Update dbo.tbl_newAkanshiData set FYID=@FYID, StateId=@StateId, DistrictId=@DistrictId, ULBID=@ULBID, CmFellowName=@CmFellowName, 
                 Total_Transferred=@Total_Transferred, Total_Amount=@Total_Amount, ModifiedBy=@ModifiedBy, ModifiedDate=@ModifiedDate where newAkanshi_Id=@newAkanshi_Id";

                        SqlCommand cmdMaster = new SqlCommand(UpdateQuery, conn, transaction);
                        cmdMaster.Parameters.AddWithValue("@newAkanshi_Id", Akanshi_id);
                        cmdMaster.Parameters.AddWithValue("@FYID", FY);
                        cmdMaster.Parameters.AddWithValue("@StateId", Zone);
                        cmdMaster.Parameters.AddWithValue("@DistrictId", circle);
                        cmdMaster.Parameters.AddWithValue("@ULBID", ULB);
                        cmdMaster.Parameters.AddWithValue("@CmFellowName", CMFellowName);
                        cmdMaster.Parameters.AddWithValue("@Total_Transferred", TotalTransferred);
                        cmdMaster.Parameters.AddWithValue("@Total_Amount", totalAmount);
                        cmdMaster.Parameters.AddWithValue("@ModifiedBy", Person_Id);
                        cmdMaster.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        //cmdMaster.Parameters.AddWithValue("@IsActive", true);

                        // Execute and get the new master Id
                        int masterId = Convert.ToInt32(cmdMaster.ExecuteScalar());
                        string selectQuery = @"SELECT newAkanshiDetail_Id FROM dbo.tbl_NewAkanshiData_HeadDetails";

                        // Dictionary to hold AdoptedParkId from the database
                        HashSet<int?> dbIds = new HashSet<int?>();

                        // Step 1: Retrieve all the AdoptedParkId values from the database
                        using (SqlCommand selectCmd = new SqlCommand(selectQuery, conn, transaction))
                        {
                            using (SqlDataReader reader = selectCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Store the AdoptedParkId in the HashSet
                                    dbIds.Add(Convert.ToInt32(reader["newAkanshiDetail_Id"]));
                                }
                            }
                        }
                        // Iterate through GridView rows to save details
                        foreach (GridViewRow row in grdAkanshiHead.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                int? detailId = null; 

                                string detailIdText = row.Cells[0].Text.Trim(); 
                                if (!string.IsNullOrEmpty(detailIdText))
                                {
                                    int parsedId;
                                    if (int.TryParse(detailIdText, out parsedId))
                                    {
                                        detailId = parsedId;
                                    }
                                    else
                                    {
                                        detailId = null;
                                    }
                                }
                                else
                                {
                                    detailId = null;
                                    Console.WriteLine("The detailIdText was null or empty.");
                                }
                                var ddlAkanshiHead = Convert.ToInt32(((DropDownList)row.FindControl("ddlAkanshiHead")).SelectedValue);
                                var NoOfHead = ((TextBox)row.FindControl("NoOfHead")).Text.Trim();
                                var CostPerHead = ((TextBox)row.FindControl("CostPerHead")).Text.Trim();
                                var Amount = ((TextBox)row.FindControl("Amount")).Text.Trim();
                                // Delete previous details
                                string deletequery = @"Delete from dbo.tbl_NewAkanshiData_HeadDetails where newAkanshiDetail_Id=@newAkanshiDetail_Id";
                                if (detailId != null && dbIds.Contains(detailId))
                                {
                                    // Step 3: If detailId is not found in the database, delete the record
                                    using (SqlCommand deleteCmd = new SqlCommand(deletequery, conn, transaction))
                                    {
                                        deleteCmd.Parameters.AddWithValue("@newAkanshiDetail_Id", detailId);
                                        deleteCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insert into detail table (tbl_NewAkanshiData_HeadDetails)
                                string insertDetailQuery = @"INSERT INTO dbo.tbl_NewAkanshiData_HeadDetails 
                        (newAkanshi_Id, AkanshiHeadId, NoOfHead, CostPerHead, Amount, CreatedBy, CreatedOn)
                        VALUES 
                        (@newAkanshi_Id, @AkanshiHeadId, @NoOfHead, @CostPerHead, @Amount, @CreatedBy, @CreatedOn);";

                                SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, transaction);
                                cmdDetail.Parameters.AddWithValue("@newAkanshi_Id", Akanshi_id);
                                cmdDetail.Parameters.AddWithValue("@AkanshiHeadId", ddlAkanshiHead);
                                cmdDetail.Parameters.AddWithValue("@NoOfHead", NoOfHead);
                                cmdDetail.Parameters.AddWithValue("@CostPerHead", CostPerHead);
                                cmdDetail.Parameters.AddWithValue("@Amount", Amount);
                                cmdDetail.Parameters.AddWithValue("@CreatedBy", Person_Id);
                                cmdDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction
                        transaction.Commit();

                        // Optionally, display a success message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');window.location.href ='NewAkanshiDataList.aspx';", true);

                        // Clear the form or redirect as needed

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

                var FY1 = Convert.ToInt32(ddlFY.SelectedValue);
                var circle1 = Convert.ToInt32(ddlCircle.SelectedValue);
                var ULB1 = Convert.ToInt32(ddlDivision.SelectedValue);
                var Zone1 = Convert.ToInt32(ddlZone.SelectedValue);
                var CMFellowName1 = txtCMFellowName.Text.Trim();
                var TotalTransferred1 = txtTotalTransferred.Text.Trim();
                var Person_Id1 = Convert.ToInt32(Session["Person_Id"].ToString());

                // Retrieve TotalAmount from the GridView Footer
                decimal totalAmount = 0;
                TextBox txtTotalAmount = (TextBox)grdAkanshiHead.FooterRow.FindControl("TotalAmount");
                if (!string.IsNullOrEmpty(txtTotalAmount.Text.Trim()))
                {
                    totalAmount = Convert.ToDecimal(txtTotalAmount.Text.Trim());
                }

                string connectionString1 = ConfigurationManager.AppSettings["conn"].ToString();
                using (SqlConnection conn1 = new SqlConnection(connectionString1))
                {
                    conn1.Open();
                    SqlTransaction transaction1 = conn1.BeginTransaction();

                    try
                    {
                        // Insert into master table (tbl_newAkanshiData)
                        string insertMasterQuery = @"INSERT INTO dbo.tbl_newAkanshiData 
                (FYID, StateId, DistrictId, ULBID, CmFellowName, 
                 Total_Transferred, Total_Amount, CreatedBy, CreatedOn)
                VALUES 
                (@FYID, @StateId, @DistrictId, @ULBID, @CmFellowName, 
                 @Total_Transferred, @Total_Amount, @CreatedBy, @CreatedOn);
                SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdMaster = new SqlCommand(insertMasterQuery, conn1, transaction1);
                        cmdMaster.Parameters.AddWithValue("@FYID", FY1);
                        cmdMaster.Parameters.AddWithValue("@StateId", Zone1);
                        cmdMaster.Parameters.AddWithValue("@DistrictId", circle1);
                        cmdMaster.Parameters.AddWithValue("@ULBID", ULB1);
                        cmdMaster.Parameters.AddWithValue("@CmFellowName", CMFellowName1);
                        cmdMaster.Parameters.AddWithValue("@Total_Transferred", TotalTransferred1);
                        cmdMaster.Parameters.AddWithValue("@Total_Amount", totalAmount); // Insert Total Amount here
                        cmdMaster.Parameters.AddWithValue("@CreatedBy", Person_Id1);
                        cmdMaster.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                        int masterId1 = Convert.ToInt32(cmdMaster.ExecuteScalar());

                        // Iterate through GridView rows to save details
                        foreach (GridViewRow row1 in grdAkanshiHead.Rows)
                        {
                            if (row1.RowType == DataControlRowType.DataRow)
                            {
                                var ddlAkanshiHead = Convert.ToInt32(((DropDownList)row1.FindControl("ddlAkanshiHead")).SelectedValue);
                                var NoOfHead = ((TextBox)row1.FindControl("NoOfHead")).Text.Trim();
                                var CostPerHead = ((TextBox)row1.FindControl("CostPerHead")).Text.Trim();
                                var Amount = ((TextBox)row1.FindControl("Amount")).Text.Trim();

                                // Insert into detail table (tbl_NewAkanshiData_HeadDetails)
                                string insertDetailQuery = @"INSERT INTO dbo.tbl_NewAkanshiData_HeadDetails 
                        (newAkanshi_Id, AkanshiHeadId, NoOfHead, CostPerHead, Amount, CreatedBy, CreatedOn)
                        VALUES 
                        (@newAkanshi_Id, @AkanshiHeadId, @NoOfHead, @CostPerHead, @Amount, @CreatedBy, @CreatedOn);";

                                SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn1, transaction1);
                                cmdDetail.Parameters.AddWithValue("@newAkanshi_Id", masterId1);
                                cmdDetail.Parameters.AddWithValue("@AkanshiHeadId", ddlAkanshiHead);
                                cmdDetail.Parameters.AddWithValue("@NoOfHead", NoOfHead);
                                cmdDetail.Parameters.AddWithValue("@CostPerHead", CostPerHead);
                                cmdDetail.Parameters.AddWithValue("@Amount", Amount);
                                cmdDetail.Parameters.AddWithValue("@CreatedBy", Person_Id1);
                                cmdDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction
                        transaction1.Commit();
                        // Optionally, display a success message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');window.location.href ='NewAkanshiDataList.aspx';", true);
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction in case of an error
                        transaction1.Rollback();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while saving data. Please try again.');", true);
                    }
                }
            }
            catch
            {
                // Handle general errors
            }
        }
      
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (!ValidateFields())
    //        {
    //            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All Fields required.');", true);
    //            return;
    //        }

    //        var FY1 = Convert.ToInt32(ddlFY.SelectedValue);
    //        var circle1 = Convert.ToInt32(ddlCircle.SelectedValue);
    //        var ULB1 = Convert.ToInt32(ddlDivision.SelectedValue);
    //        var Zone1 = Convert.ToInt32(ddlZone.SelectedValue);
    //        var CMFellowName1 = txtCMFellowName.Text.Trim();
    //        var TotalTransferred1 = txtTotalTransferred.Text.Trim();
    //        var Person_Id1 = Convert.ToInt32(Session["Person_Id"].ToString());

    //        string connectionString1 = ConfigurationManager.AppSettings["conn"].ToString();
    //        using (SqlConnection conn1 = new SqlConnection(connectionString1))
    //        {
    //            conn1.Open();
    //            SqlTransaction transaction1 = conn1.BeginTransaction();

    //            try
    //            {

    //                string insertMasterQuery = @"INSERT INTO dbo.tbl_newAkanshiData 
    //            (FYID, StateId, DistrictId, ULBID, CmFellowName, 
    //             Total_Transferred, Total_Amount, CreatedBy, CreatedOn)
    //            VALUES 
    //            (@FYID, @StateId, @DistrictId, @ULBID, @CmFellowName, 
    //             @Total_Transferred, @Total_Amount, @CreatedBy, @CreatedOn);
    //            SELECT SCOPE_IDENTITY();";

    //                SqlCommand cmdMaster = new SqlCommand(insertMasterQuery, conn1, transaction1);
    //                cmdMaster.Parameters.AddWithValue("@FYID", FY1);
    //                cmdMaster.Parameters.AddWithValue("@StateId", Zone1);
    //                cmdMaster.Parameters.AddWithValue("@DistrictId", circle1);
    //                cmdMaster.Parameters.AddWithValue("@ULBID", ULB1);
    //                cmdMaster.Parameters.AddWithValue("@CmFellowName", CMFellowName1);
    //                cmdMaster.Parameters.AddWithValue("@Total_Transferred", TotalTransferred1);
    //                cmdMaster.Parameters.AddWithValue("@Total_Amount", 0);
    //                cmdMaster.Parameters.AddWithValue("@CreatedBy", Person_Id1);
    //                cmdMaster.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
    //                cmdMaster.Parameters.AddWithValue("@IsActive", true);

    //                int masterId1 = Convert.ToInt32(cmdMaster.ExecuteScalar());

    //                // Iterate through GridView rows to save details
    //                foreach (GridViewRow row1 in grdAkanshiHead.Rows)
    //                {
    //                    if (row1.RowType == DataControlRowType.DataRow)
    //                    {
    //                        var ddlAkanshiHead = Convert.ToInt32(((DropDownList)row1.FindControl("ddlAkanshiHead")).SelectedValue);
    //                        var NoOfHead = ((TextBox)row1.FindControl("NoOfHead")).Text.Trim();
    //                        var CostPerHead = ((TextBox)row1.FindControl("CostPerHead")).Text.Trim();
    //                        var Amount = ((TextBox)row1.FindControl("Amount")).Text.Trim();

    //                        // Insert into tbl_AdoptedParkMaster_details
    //                        string insertDetailQuery = @"INSERT INTO dbo.tbl_NewAkanshiData_HeadDetails 
    //                    (newAkanshi_Id, AkanshiHeadId, NoOfHead,CostPerHead, Amount,CreatedBy, CreatedOn)
    //                    VALUES 
    //                    (@newAkanshi_Id, @AkanshiHeadId, @NoOfHead,@CostPerHead, @Amount, @CreatedBy, @CreatedOn);";

    //                        SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn1, transaction1);
    //                        cmdDetail.Parameters.AddWithValue("@newAkanshi_Id", masterId1);
    //                        cmdDetail.Parameters.AddWithValue("@AkanshiHeadId", ddlAkanshiHead);
    //                        cmdDetail.Parameters.AddWithValue("@NoOfHead", NoOfHead);
    //                        cmdDetail.Parameters.AddWithValue("@CostPerHead", CostPerHead);
    //                        cmdDetail.Parameters.AddWithValue("@Amount", Amount);
    //                        cmdDetail.Parameters.AddWithValue("@CreatedBy", Person_Id1);
    //                        cmdDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

    //                        cmdDetail.ExecuteNonQuery();
    //                    }
    //                }
    //                // Commit transaction
    //                transaction1.Commit();
    //                // Optionally, display a success message
    //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Data saved successfully!');window.location.href ='AdoptedPark_Edit.aspx';", true);
    //            }
    //            catch (Exception ex)
    //            {
    //                // Rollback transaction
    //                transaction1.Rollback();
    //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while saving data. Please try again.');", true);
    //            }
    //        }
    //    }
    //    catch
    //    {

    //    }
    //}

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
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
    protected void Load_Project_Details(int ProjectWork_Id)
    {
        
    }
   
    
    protected void grdAkanshiHead_PreRender(object sender, EventArgs e)
    {
        if (ViewState["dtNewAkankshiData"] != null)
        {
            grdAkanshiHead.DataSource = (DataTable)ViewState["dtNewAkankshiData"];
            grdAkanshiHead.DataBind();
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
    protected void grdAkanshiHead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Reset the total amount before processing the rows
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ViewState["TotalAmount"] = 0m; // Reset the total amount at the beginning
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtQuestionnaire = (DataTable)ViewState["dtNewAkankshiData"];
            int rowIndex = e.Row.DataItemIndex;

            if (rowIndex < dtQuestionnaire.Rows.Count)
            {
                DataRow dr = dtQuestionnaire.Rows[rowIndex];

                // Populate the Akanshi Head dropdown
                DropDownList ddlAkanshiHead = (DropDownList)e.Row.FindControl("ddlAkanshiHead");
                if (AllClasses.CheckDt(dtQuestionnaire))
                {
                    DataSet ds = (new DataLayer()).get_tbl_AkanshiHead();
                    AllClasses.FillDropDown(ds.Tables[0], ddlAkanshiHead, "AkanshiHead", "AkanshiHeadID");
                }

                // Retrieve and set the AkanshiHeadID in the dropdown
                int akanshiHeadID;
                if (int.TryParse(dr["AkanshiHead"].ToString(), out akanshiHeadID) && akanshiHeadID > 0)
                {
                    ddlAkanshiHead.SelectedValue = akanshiHeadID.ToString();
                }

                // Retrieve CostPerHead based on AkanshiHeadID
                if (AllClasses.CheckDt(dtQuestionnaire))
                {
                    DataSet ds = (new DataLayer()).get_CostPerHead(akanshiHeadID);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        TextBox txtCostPerHead = (TextBox)e.Row.FindControl("CostPerHead");
                        DataRow dr1 = ds.Tables[0].Rows[0];
                        txtCostPerHead.Text = dr1["CostPerHead"].ToString();
                    }
                }

                // Set NoOfHead TextBox value
                TextBox noOfHeadTextBox = (TextBox)e.Row.FindControl("NoOfHead");
                noOfHeadTextBox.Text = dr["NoOfHead"] != DBNull.Value ? dr["NoOfHead"].ToString() : "0";

                // Calculate Amount for this row
                decimal costPerHead = dr["CostPerHead"] != DBNull.Value ? Convert.ToDecimal(dr["CostPerHead"]) : 0;
                decimal noOfHead = dr["NoOfHead"] != DBNull.Value ? Convert.ToDecimal(dr["NoOfHead"]) : 0;
                decimal rowAmount = costPerHead * noOfHead;

                TextBox txtAmount = (TextBox)e.Row.FindControl("Amount");
                txtAmount.Text = rowAmount.ToString("0.##");

                // Accumulate the total amount
                if (ViewState["TotalAmount"] == null)
                {
                    ViewState["TotalAmount"] = 0m;
                }

                ViewState["TotalAmount"] = (decimal)ViewState["TotalAmount"] + rowAmount;
            }
        }

        // Handle footer row to display the accumulated total
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TextBox txtTotalAmount = (TextBox)e.Row.FindControl("TotalAmount");
            if (txtTotalAmount != null)
            {
                // Display the total amount in the footer
                txtTotalAmount.Text = ((decimal)ViewState["TotalAmount"]).ToString("0.##");
            }
        }
    }
    private void Bind_EmptyFields()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Bind_EmptyFields_Akanshi();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtNewAkankshiData"] = ds.Tables[0];
            grdAkanshiHead.DataSource = ds.Tables[0];
            grdAkanshiHead.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                //dr["ProjectWorkGO_Id"] = 0;
                dr["newAkanshiDetail_Id"] = 0;
                dr["AkanshiHead"] = "";
                dr["CostPerHead"] = 0;
                dr["NoOfHead"] = 0;
                dr["Amount"] = 0;
               
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtNewAkankshiData"] = ds.Tables[0];
                grdAkanshiHead.DataSource = ds.Tables[0];
                grdAkanshiHead.DataBind();
            }
            catch (Exception ex)
            {
                grdAkanshiHead.DataSource = null;
                grdAkanshiHead.DataBind();
            }
        }
    }
    protected void btnDeleteAkashiHead_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        AddDynamicFields();
    }
    private void AddDynamicFields()
    {
        DataTable dtQuestionnaire;

        if (ViewState["dtNewAkankshiData"] != null)
        {
            dtQuestionnaire = (DataTable)ViewState["dtNewAkankshiData"];

            // Preserve existing data from GridView into DataTable
            foreach (GridViewRow row in grdAkanshiHead.Rows)
            {
                DataRow dr = dtQuestionnaire.Rows[row.RowIndex];

                // Extract values from TextBoxes and DropDownLists
                int ddlAkanshiHead;
                dr["AkanshiHead"] = int.TryParse(((DropDownList)row.FindControl("ddlAkanshiHead")).SelectedValue, out ddlAkanshiHead) ? ddlAkanshiHead: 0;
                //dr["MonthId"] = decimal.TryParse(((DropDownList)row.FindControl("MonthId")).SelectedValue, out monthId) ? monthId : 0;

                decimal latitude;
                dr["NoOfHead"] = decimal.TryParse(((TextBox)row.FindControl("NoOfHead")).Text.Trim(), out latitude) ? latitude : 0; 
                decimal CostPerHead;
                dr["CostPerHead"] = decimal.TryParse(((TextBox)row.FindControl("CostPerHead")).Text.Trim(), out CostPerHead) ? CostPerHead : 0;
                decimal longitude;
                dr["Amount"] = decimal.TryParse(((TextBox)row.FindControl("Amount")).Text.Trim(), out longitude) ? longitude : 0;



            }

            // Add a new empty row
            DataRow newRow = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(newRow);


            // Update ViewState and rebind GridView
            ViewState["dtNewAkankshiData"] = dtQuestionnaire;
            grdAkanshiHead.DataSource = dtQuestionnaire;
            grdAkanshiHead.DataBind();
            //UpdateTotalAmount();
        }
    }
    private void KeepDataOnChange()
    {
        DataTable dtQuestionnaire;

        if (ViewState["dtNewAkankshiData"] != null)
        {
            dtQuestionnaire = (DataTable)ViewState["dtNewAkankshiData"];

            foreach (GridViewRow row in grdAkanshiHead.Rows)
            {
                DataRow dr = dtQuestionnaire.Rows[row.RowIndex];

                int ddlAkanshiHead;
                dr["AkanshiHead"] = int.TryParse(((DropDownList)row.FindControl("ddlAkanshiHead")).SelectedValue, out ddlAkanshiHead) ? ddlAkanshiHead : 0;
                //dr["MonthId"] = decimal.TryParse(((DropDownList)row.FindControl("MonthId")).SelectedValue, out monthId) ? monthId : 0;

                decimal latitude;
                dr["NoOfHead"] = decimal.TryParse(((TextBox)row.FindControl("NoOfHead")).Text.Trim(), out latitude) ? latitude : 0;
                decimal CostPerHead;
                dr["CostPerHead"] = decimal.TryParse(((TextBox)row.FindControl("CostPerHead")).Text.Trim(), out CostPerHead) ? CostPerHead : 0;
                decimal longitude;
                dr["Amount"] = decimal.TryParse(((TextBox)row.FindControl("Amount")).Text.Trim(), out longitude) ? longitude : 0;
            }
            ViewState["dtNewAkankshiData"] = dtQuestionnaire;
            grdAkanshiHead.DataSource = dtQuestionnaire;
            grdAkanshiHead.DataBind();
        }
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        
    }

    protected void ddlAkanshiHead_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlAkanshiHead = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlAkanshiHead.NamingContainer;

        int selectedValue = Convert.ToInt32(ddlAkanshiHead.SelectedValue);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_CostPerHead(selectedValue);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            DataRow dr1 = dt.Rows[0];

            TextBox txtCostPerHead = (TextBox)row.FindControl("CostPerHead");
            if (txtCostPerHead != null)
            {
                txtCostPerHead.Text = dr1["CostPerHead"].ToString();
            }
        }
    }

    protected void NoOfHead_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        TextBox txtNoOfHead = (TextBox)row.FindControl("NoOfHead");
        TextBox txtCostPerHead = (TextBox)row.FindControl("CostPerHead");
        TextBox txtAmount = (TextBox)row.FindControl("Amount");

        decimal noOfHead = 0;
        decimal costPerHead = 0;

        decimal.TryParse(txtNoOfHead.Text, out noOfHead);
        decimal.TryParse(txtCostPerHead.Text, out costPerHead);

        decimal amount = noOfHead * costPerHead;
        txtAmount.Text = amount.ToString("F2"); 

    }
    public bool ValidateFields()
    {
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Year. ");
            ddlFY.Focus();
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
        } if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Zone. ");
            ddlZone.Focus();
            return false;
        }
        if (txtCMFellowName.Text == "" || txtCMFellowName.Text == null)
        {
            MessageBox.Show("Please Enter CMFellowName. ");
            txtCMFellowName.Focus();
            return false;
        }
        if (txtTotalTransferred.Text == "" || txtTotalTransferred.Text == null)
        {
            MessageBox.Show("Please Enter TotalTransferred. ");
            txtTotalTransferred.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void Load_CmFellow(int newAkanshiId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_newAkanshiData_Edit(newAkanshiId);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlFY.SelectedValue = ds.Tables[0].Rows[0]["FYID"].ToString();
            }
            catch
            {
                ddlFY.SelectedValue = "0";
            }
            try
            {
                ddlZone.SelectedValue = ds.Tables[0].Rows[0]["StateId"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }
            try
            {
                ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["DistrictId"].ToString();

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
            newAkanshi_Id.Value = ds.Tables[0].Rows[0]["newAkanshi_Id"].ToString();
            txtCMFellowName.Text = ds.Tables[0].Rows[0]["CMFellowName"].ToString();
            txtTotalTransferred.Text = ds.Tables[0].Rows[0]["Total_Transferred"].ToString();
            //txtAdoptionInProcess.Text = ds.Tables[0].Rows[0]["NoOfAdoptionInprocessPark"].ToString();
            //txtNoOfParkAdopted.Text = ds.Tables[0].Rows[0]["NoOfParkAdopted"].ToString();
            //get_tbl_Circle(1);
        }
    }
    private void LoadAkanshiHead(int newAkanshiId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_NewAkanshiData_HeadDetails(newAkanshiId);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtNewAkankshiData"] = ds.Tables[0];
            grdAkanshiHead.DataSource = ds.Tables[0];
            grdAkanshiHead.DataBind();
            //txtTotalReleseAmount.Text = ds.Tables[0].Compute("sum(ProjectWorkGO_TotalRelease)", "").ToString();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                //dr["ProjectWorkGO_Id"] = 0;
                //dr["newAkanshiDetail_Id"] = 0;
                dr["AkanshiHead"] = "";
                dr["CostPerHead"] = 0;
                dr["NoOfHead"] = 0;
                dr["Amount"] = 0;
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtNewAkankshiData"] = ds.Tables[0];
                grdAkanshiHead.DataSource = ds.Tables[0];
                grdAkanshiHead.DataBind();
            }
            catch (Exception ex)
            {
                grdAkanshiHead.DataSource = null;
                grdAkanshiHead.DataBind();
            }
        }
    }
}