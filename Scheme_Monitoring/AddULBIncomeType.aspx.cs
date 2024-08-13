using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data.SqlClient;

public partial class AddULBIncomeType : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    string ConStr = ConfigurationManager.AppSettings.Get("conn");
    int totalRow = 0;
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
            if (Request.QueryString.Count > 0)
            {
                ULBID.Value = Request.QueryString["ULBID"].ToString();
                FYID.Value = Request.QueryString["FYID"].ToString();
                GetEditIncomeList(ULBID.Value, FYID.Value);
                HeadingSec.InnerText = "UPDATE INCOME TYPE";
                HeadingSec2.InnerText = "Update Income Type";
                //AddSection.Visible = false;
            }
            get_tbl_ULBIncomeType();
            get_tbl_Zone();
            get_tbl_Project();

            get_tbl_FinancialYear();


            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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

   


    private void get_tbl_Project()
    {
        DataSet ds = (new DataLayer()).get_tbl_Project(0);
        //FillDropDown(ds, ddlProjectMaster, "Project_Name", "Project_Id");
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY1, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY2, "FinancialYear_Comments", "FinancialYear_Id");
    }

    private void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown2(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
        }
    }

    private void get_tbl_Circle(int zoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
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
    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            lblMessage.Text = "Please Select a ULB.";
            ddlDivision.Focus();
        }
        else
        {
           // GetAllData(Convert.ToInt32(ddlDivision.SelectedValue));
            //BindLoanReleaseGridByULB();
        }
    }

    private void get_tbl_ULBIncomeType()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ULBIncomeType();
        totalRow = ds.Tables[0].Rows.Count;

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GrdULBFund.DataSource = ds.Tables[0];
            GrdULBFund.DataBind();
        }
        else
        {
            GrdULBFund.DataSource = null;
            GrdULBFund.DataBind();
        }

    }
    protected void GetEditIncomeList(string ULBID, string FYID)
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetDataForEditOfIncome(ULBID, FYID);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlZone.SelectedValue = dt.Rows[0]["stateId"].ToString();
            ddlCircle.SelectedValue = dt.Rows[0]["CircleId"].ToString();
            get_tbl_Division(Convert.ToInt32(dt.Rows[0]["CircleId"].ToString()));

            ddlDivision.SelectedValue = dt.Rows[0]["ULBID"].ToString();

            ddlFY.SelectedValue = dt.Rows[0]["FYId"].ToString();

            GrdULBFund.DataSource = dt;
            GrdULBFund.DataBind();
            ButtonUpdate.Visible = true;
            BtnSubmit.Visible = false;

        }
        else
        {
            // exportToExcel.Visible = false;

        }
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
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial. ");
            ddlFY.Focus();
            return false;
        }
       
        else
        {
            return true;
        }
    }
    protected void BtnSubmit_Command(object sender, CommandEventArgs e)
    {
        // var transac str
        string msg = "";
  
        try
        {


            if (!ValidateFields())
            {
                return;
            }
            List<Tbl_ULBIncomeTypeChild> ulbexp = new List<Tbl_ULBIncomeTypeChild>();
            for (int i = 0; i < GrdULBFund.Rows.Count; i++)
            {
                var check = (GrdULBFund.Rows[i].FindControl("Amounts") as TextBox).Text.Trim();
                if (check != null && check != "")
                {

                    Tbl_ULBIncomeTypeChild ul = new Tbl_ULBIncomeTypeChild();
                    ul.Action = "Insert";
                    ul.stateId = Convert.ToInt32(ddlZone.SelectedValue);
                    ul.CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
                    ul.ULBID = Convert.ToInt32(ddlDivision.SelectedValue);
                    ul.FYID = Convert.ToInt32(ddlFY.SelectedValue);
                    ul.HeadID = Convert.ToInt32(GrdULBFund.Rows[i].Cells[0].Text.ToString());

                    ul.Amount = Convert.ToDecimal((GrdULBFund.Rows[i].FindControl("Amounts") as TextBox).Text.Trim());
                    ul.createdBy= Convert.ToInt32(Session["Person_Id"].ToString());
                    ul.createdOn = DateTime.Now.ToString();
                    ul.updateOn = null;
                    ul.IsActive = true;
                    ulbexp.Add(ul);
                }
            }

            if (new ULBFund().InsertULBFundIncome(ulbexp, msg))
            {
                MessageBox.Show("Income Type data Created Successfully ! ");
                for (int i = 0; i < GrdULBFund.Rows.Count; i++)
                {
                    (GrdULBFund.Rows[i].FindControl("Amounts") as TextBox).Text = "";
                    var validator2 = GrdULBFund.Rows[i].FindControl("rfvAmounts") as RequiredFieldValidator;

                    if (validator2 != null)
                    {

                        validator2.Enabled = false;
                    }
                }
               // Response.Redirect("ListOfAllULB_IncomeType.aspx");

                get_tbl_Project();
                return;
            }
            else
            {
                if (msg == "A")
                {
                    MessageBox.Show("This Income Type data Already Exist. Give another! ");
                }
                else
                {
                    MessageBox.Show("Error ! ");
                }
                return;
            }
           
        }
        catch(Exception ex)
        {
            MessageBox.Show("Error : " + ex.Message);
        }
    }

    protected void ButtonUpdate_Command(object sender, CommandEventArgs e)
    {
        string msg = "";

        try
        {


            if (!ValidateFields())
            {
                return;
            }
            List<Tbl_ULBIncomeTypeChild> ulbexp = new List<Tbl_ULBIncomeTypeChild>();
            var checkinsert = 0;
            for (int i = 0; i < GrdULBFund.Rows.Count; i++)
            {
                var check = (GrdULBFund.Rows[i].FindControl("Amounts") as TextBox).Text.Trim();
                if (check != null && check != "")
                {

                    Tbl_ULBIncomeTypeChild ul = new Tbl_ULBIncomeTypeChild();
                    ul.Action = "Insert";
                    ul.stateId = Convert.ToInt32(ddlZone.SelectedValue);
                    ul.CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
                    ul.ULBID = Convert.ToInt32(ddlDivision.SelectedValue);
                    ul.FYID = Convert.ToInt32(ddlFY.SelectedValue);
                    ul.HeadID = Convert.ToInt32(GrdULBFund.Rows[i].Cells[0].Text.ToString());
                    ul.Amount = Convert.ToDecimal((GrdULBFund.Rows[i].FindControl("Amounts") as TextBox).Text.Trim());
                    ul.updateBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    ul.updateOn = DateTime.Now.ToString();
                    ul.IsActive = true;
                   
                    ulbexp.Add(ul);
                    checkinsert++;
                }
            }
             if(checkinsert==0)
            {
                MessageBox.Show("Can Not Insert 0 data !!1");
                return;
            }
           
            if (new ULBFund().UpdateULBFundIncome(ulbexp, msg))
            {
                MessageBox.Show("Income Type data Update Successfully ! ");
                for (int i = 0; i < GrdULBFund.Rows.Count; i++)
                {
                    //(GrdULBFund.Rows[i].FindControl("Amounts") as TextBox).Text = "";
                    var validator2 = GrdULBFund.Rows[i].FindControl("rfvAmounts") as RequiredFieldValidator;

                    if (validator2 != null)
                    {
                        
                        validator2.Enabled = false;
                    }
                }
                get_tbl_Project();
                return;
            }
            else
            {
                if (msg == "A")
                {
                    MessageBox.Show("This Scheme Already Exist. Give another! ");
                }
                else
                {
                    MessageBox.Show("This Expense Type data Already Exist. Give another!  ");

                }
                return;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Error : " + ex.Message);
        }
    }

    protected void GrdULBFund_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var chec = GrdULBFund.Rows.Count;
        if (ULBID.Value != null && FYID.Value != null)
        {
            DataTable dt = new DataTable();
            dt = objLoan.GetDataForEditOfIncome(ULBID.Value, FYID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {

                //GrdULBFund_RowDataBound(, GrdULBFund)
                int i = 1;
                foreach (GridViewRow row in GrdULBFund.Rows)
                {

                    // Find the TextBox control in the current row

                    TextBox txtName = (TextBox)row.FindControl("Amounts");

                    //var check= dt.Rows[i]["Amount"].ToString();
                    if (txtName != null && totalRow >= dt.Rows.Count)
                    {
                        if (dt.Rows.Count >= i)
                        {
                            txtName.Text = dt.Rows[(i-1)]["Amount"].ToString(); // You can use a dynamic value based on your logic
                        }
                       
                    }
                   
                 
                    i++;


                }
                //GrdULBFund.DataSource = dt;
                //GrdULBFund.DataBind();
            }
        }
    }
}