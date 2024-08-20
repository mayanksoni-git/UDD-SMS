using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

public partial class LoanDeposit : System.Web.UI.Page
{
    Loan objLoan = new Loan();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }

    //New Page Load and dropdowns code was added and old was commented.
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {

            lblZoneH.Text = Session["Default_Zone"].ToString() + "*";
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            lblDivisionH.Text = Session["Default_Division"].ToString() + "*";
            get_tbl_Zone();
           
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);

        if (userType == 4 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
        }
        else if (userType == 6 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
            }
        }
        else if (userType == 7 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
                if (divisionId > 0)
                {
                    SetDropdownValueAndDisable(ddlDivision, divisionId);
                }
            }
        }
    }
    private void SetDropdownValueAndDisable(DropDownList ddl, int value)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;
            if (ddl.ID.ToString() == "ddlZone")
            {
                ddlZone_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
            else if (ddl.ID.ToString() == "ddlCircle")
            {
                ddlCircle_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
        }
        catch
        {
            // Handle exception if needed
        }
    }


    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }
    private void get_tbl_Circle(int zoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    private void get_tbl_Scheme(int divisionId)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_ProjectByDivisionId(divisionId, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_ProjectByDivisionId(divisionId, Convert.ToInt32(Session["Person_Id"].ToString()));
        }

        if(ds.Tables[0].Rows.Count>0)
        {
            FillDropDown(ds, ddlScheme, "Project_Name", "Project_Id");
            ddlScheme.SelectedIndex = 1;
            ddlScheme_SelectedIndexChanged(ddlScheme, EventArgs.Empty);
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlProject.Items.Clear();
            reset();
            MessageBox.Show("No any loan type scheme found in the " + ddlDivision.SelectedItem.Text + " division.");
            return;
        }

            
    }
    private void get_tbl_SchemeProject(int schemeId, int divisionId)
    {
        DataSet ds = (new DataLayer()).get_tbl_SchemeProject(schemeId, divisionId);
        FillDropDown(ds, ddlProject, "ProjectWork_Name", "ProjectWork_Id");
    }


    private void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDivision.Items.Clear();
        ddlScheme.Items.Clear();
        ddlProject.Items.Clear();
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
            reset();
        }
    }
    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlScheme.Items.Clear();
        ddlProject.Items.Clear();
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
            

        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
            reset();
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProject.Items.Clear();
        if (ddlDivision.SelectedValue == "0")
        {
            ddlScheme.Items.Clear();
            

        }
        else
        {
            get_tbl_Scheme(Convert.ToInt32(ddlDivision.SelectedValue));
            reset();
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            ddlProject.Items.Clear();
        }
        else
        {
            get_tbl_SchemeProject(Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue));
            reset();
        }
    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProject.SelectedValue == "0")
        {
            lblMessage.Text = "Please Select a Project.";
            ddlProject.Focus();
        }
        else
        {
            LoadLoanDetails();
            LoadLoanEMIs();
            LoadDeposits();
        }
    }

    private void LoadLoanDetails()
    {
        int ProjectWork_Id = Convert.ToInt32(ddlProject.SelectedValue);
        decimal TotalDueAmount = objLoan.GetTotalDueAmount(ProjectWork_Id);
        lblTotalLoanAmount.Text = "Total Loan Amount: ₹ " + objLoan.GetTotalLoanByProjectId(ProjectWork_Id).ToString();
        lblRemainingLoanAmount.Text = "Remaining Loan Amount: ₹ " + objLoan.GetRemainingLoanAmount(ProjectWork_Id).ToString();
        lblTotalDueAmount.Text = "Total Due Amount: ₹ " + TotalDueAmount.ToString();
        if (TotalDueAmount != 0 && Convert.ToInt32(ddlDivision.SelectedValue) != 0)
        {
            txtDepositAmount.Text = TotalDueAmount.ToString();
        }
        else
        {
            txtDepositAmount.Text = "";
        }

        txtDepositDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    private void LoadLoanEMIs()
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetLoanEMIsByProjectId(Convert.ToInt32(ddlProject.SelectedValue));

        if (dt != null && dt.Rows.Count > 0)
        {
            //Session["GridViewData"] = dt;
            gvRecords.DataSource = dt;
            gvRecords.DataBind();
            divData.Visible = true;
            //---- check that EMI Of Loan Record is less than 10 (By Ali 06-07-024)
           


            decimal TotalPaidAmount = dt.Compute("Sum(PaidAmount)", "").ToString().ToDecimal();
            Label lblTotalDepositAmount = (Label)gvRecords.FooterRow.FindControl("lblTotalPaidAmount");
            lblTotalDepositAmount.Text = "Total Paid Amount = ₹ " + TotalPaidAmount.ToString("0.00");

            decimal TotalTotalInstallmentAmount = dt.Compute("Sum(ProjectWorkGO_TotalRelease)", "").ToString().ToDecimal();
            Label lblTotalInstallmentAmount = (Label)gvRecords.FooterRow.FindControl("lblTotalInstallmentAmount");
            lblTotalInstallmentAmount.Text = "Total Released Amount = ₹ " + TotalTotalInstallmentAmount.ToString("0.00");

            decimal TotalRemainingAmount = dt.Compute("Sum(RemainingAmount)", "").ToString().ToDecimal();
            Label lblTotalRemainingAmount = (Label)gvRecords.FooterRow.FindControl("lblTotalRemainingAmount");
            lblTotalRemainingAmount.Text = "Total Remaining Amount = ₹ " + TotalRemainingAmount.ToString("0.00");
            dt = new DataTable();
            dt = objLoan.GetLoanDepositsByProjectId(Convert.ToInt32(ddlProject.SelectedValue));
            if (dt != null && dt.Rows.Count>0)
            {
                if(dt.Rows.Count>=9)
                {
                    txtDepositAmount.Text = TotalRemainingAmount.ToString("0.00");
                    txtDepositAmount.Enabled = false;
                    TxtDepositeCheck.Text = "Please note: You have a maximum of 10 EMIs available. The 10th EMI will require full payment of the remaining loan amount.";
                    TxtDepositeCheck.Attributes["style"] = "color: red; font-weight: bold;";
                }
                else
                {
                    txtDepositAmount.Text = "";
                    txtDepositAmount.Enabled = true;
                    TxtDepositeCheck.Text = "";
                }
            }

        }
        else
        {
            divData.Visible = true;
            gvRecords.DataSource = null;
            gvRecords.DataBind();
            MessageBox.Show("No Records Found in the Loan EMI's table.");
        }
    }

    private void LoadDeposits()
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetLoanDepositsByProjectId(Convert.ToInt32(ddlProject.SelectedValue));

        if (dt != null && dt.Rows.Count > 0)
        {

            //Session["GridViewData"] = dt;
            gvDeposits.DataSource = dt;
            gvDeposits.DataBind();
            divDeposit.Visible = true;

            decimal Deposit = dt.Compute("Sum(DepositAmount)", "").ToString().ToDecimal();
            Label lbl_TotalDepositAmount = (Label)gvDeposits.FooterRow.FindControl("lbl_TotalDepositAmount");
            lbl_TotalDepositAmount.Text = "Total Deposit Amount = ₹ " + Deposit.ToString("0.00");

        }
        else
        {
            divDeposit.Visible = true;
            gvDeposits.DataSource = null;
            gvDeposits.DataBind();
            MessageBox.Show("No Records Found in the Loan Deposit Table.");
        }
    }

    protected void gvEMIs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime dueDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DueDate"));
            bool isPaid = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsPaid"));

            if (dueDate < DateTime.Now && !isPaid)
            {
                e.Row.CssClass = "due";
            }
        }
    }


    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (!ValidateFields())
    //    {
    //        return;
    //    }
    //    decimal depositAmount = Convert.ToDecimal(txtDepositAmount.Text);
    //    int ProjectWork_Id = Convert.ToInt32(ddlProject.SelectedValue);
    //    DateTime depositDate = txtDepositDate.Text.ToString().ToDate();

    //    int result = objLoan.InsertLoanDeposit(ProjectWork_Id, depositAmount, depositDate, Convert.ToInt32(Session["Person_Id"].ToString()));

    //    UpdateEMIStatus(ProjectWork_Id, depositAmount);
    //    LoadLoanDetails();
    //    LoadLoanEMIs();

    //    if (result > 0)
    //    {
    //        MessageBox.Show("Record saved successfully.");
    //        reset();
    //    }
    //    else
    //    {
    //        MessageBox.Show("Something went wrong please try again or contact administrator!");
    //        return;
    //    }
    //}



    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        decimal depositAmount = Convert.ToDecimal(txtDepositAmount.Text);
        int ProjectWork_Id = Convert.ToInt32(ddlProject.SelectedValue);



        string depositDateString = txtDepositDate.Text;

        DateTime depositDate;


        if (!DateTime.TryParse(depositDateString, out depositDate))
        {
            lblMessage.Text = "Invalid deposit date format!";
            return;
        }
       

        string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

        using (SqlConnection connection = new SqlConnection(ConStr))
        {
            connection.Open();

            SqlTransaction transaction = null;

            try
            {
                // Begin transaction
                transaction = connection.BeginTransaction();

                // Set transaction for commands
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                // Execute your database operations
                int result = objLoan.InsertLoanDeposit(ProjectWork_Id, depositAmount, depositDate, Convert.ToInt32(Session["Person_Id"].ToString()));
                
                if (result > 0)
                {
                    // Commit transaction if all operations succeed
                    transaction.Commit();
                    MessageBox.Show("Record saved successfully.");

                    

                    LoadLoanDetails();
                    LoadLoanEMIs();
                    LoadDeposits();
                    txtDepositAmount.Text = "";
                    txtDepositDate.Text = "";
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                }
                else
                {
                    MessageBox.Show("Something went wrong please try again or contact administrator!");
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error: " + ex.Message);

                // Rollback transaction on error
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Rollback error: " + rollbackEx.Message);
                    }
                }
            }
            finally
            {
                // Ensure the connection is closed and resources are released
                if (transaction != null)
                {
                    transaction.Dispose();
                }
                connection.Close();
            }
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }
        int LoanRelease_Id = Convert.ToInt32(hfLoanRelease_Id.Value.ToString());
        tbl_LoanRelease obj_LoanRelease = CreateLoanReleaseObject();
        int result = objLoan.UpdateLoanRelease(obj_LoanRelease, LoanRelease_Id);
        if (result > 0)
        {
            MessageBox.Show("Record updated successfully!");
            reset();
            divData.Visible = false;
            divDeposit.Visible = false;
        }
        else
        {
            MessageBox.Show("Something went wrong please try again or contact administrator!");
            return;
        }
    }
    private tbl_LoanRelease CreateLoanReleaseObject()
    {
        int personId;
        int zone;
        int circle;
        int division;
        double releaseAmount;
        int instNo;


        if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }

        if (!int.TryParse(ddlZone.SelectedValue, out zone))
        {
            throw new FormatException("Invalid Zone selected value.");
        }

        if (!int.TryParse(ddlCircle.SelectedValue, out circle))
        {
            throw new FormatException("Invalid Circle selected value.");
        }

        if (!int.TryParse(ddlDivision.SelectedValue, out division))
        {
            throw new FormatException("Invalid Division selected value.");
        }

        if (!double.TryParse(txtDepositAmount.Text, out releaseAmount))
        {
            releaseAmount = 0; // Default to 0 if parsing fails
        }

        return new tbl_LoanRelease
        {
            AddedBy = personId,
            Zone = zone,
            Circle = circle,
            Division = division,
            ReleaseAmount = releaseAmount,
            ReleaseDate = txtDepositDate.Text.ToString().ToDate()
        };
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }
    public bool ValidateFields()
    {
        
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
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
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Scheme. ");
            ddlDivision.Focus();
            return false;
        }
        if (ddlProject.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Project. ");
            ddlDivision.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtDepositAmount.Text))
        {
            MessageBox.Show("Please enter Deposit Amount!");
            txtDepositAmount.Focus();
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(txtDepositDate.Text))
        {
            MessageBox.Show("Please enter Deposit Date!");
            txtDepositDate.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }
    private void reset()
    {
        divData.Visible = false;
        divDeposit.Visible = false;
        txtDepositDate.Text = "";
        txtDepositAmount.Text = "";
        lblTotalLoanAmount.Text = "";
        lblRemainingLoanAmount.Text = "";
        lblTotalDueAmount.Text = "";
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        gvRecords.DataSource = null;
        gvRecords.DataBind();
        gvDeposits.DataSource = null;
        gvDeposits.DataBind();
    }
    protected void Load_LoanRelease(int LoanRelease_Id)
    {
        DataTable dt = objLoan.getLoanReleaseById(LoanRelease_Id);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfLoanRelease_Id.Value = LoanRelease_Id.ToString();
            
            try
            {
                ddlZone.SelectedValue = dt.Rows[0]["Zone"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }
            //ddlZone.Enabled = false;

            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
            try
            {
                ddlCircle.SelectedValue = dt.Rows[0]["Circle"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            //ddlCircle.Enabled = false;

            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["Division"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            //ddlDivision.Enabled = false;

            txtDepositAmount.Text = dt.Rows[0]["ReleaseAmount"].ToString();


            // DateTime releaseDate = Convert.ToDateTime(dt.Rows[0]["ReleaseDate"]);
            txtDepositDate.Text = dt.Rows[0]["ReleaseDate"].ToString().ToDate().ToString("dd/MMM/yyyy");

            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        else
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;
           
            MessageBox.Show("Record with Loan Release id = " + LoanRelease_Id.ToString() + " does not found please contact administrator.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int LoanRelease_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (LoanRelease_Id > 0)
        {
            Load_LoanRelease(LoanRelease_Id);
            ToggleFormMode(true); // switch to update mode
        }
        else
        {
            return;
        }
    }
    private void ToggleFormMode(bool isUpdateMode)
    {
        // Toggle between save and update modes
        btnSave.Visible = !isUpdateMode;
        btnUpdate.Visible = isUpdateMode;
        btnCancel.Visible = isUpdateMode;
    }
}