using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class VisionPlan : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    string ConStr = ConfigurationManager.AppSettings.Get("conn");
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
                ULBID.Value = Request.QueryString["ULBID"].ToString() ;
                FYID.Value = Request.QueryString["FYID"].ToString();
            }
           
            get_tbl_Zone();
            get_tbl_FinancialYear();
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new ULBFund()).getFYDetail();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }
    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Mandal();
        }
    }
    private void get_tbl_Mandal()
    {
        DataSet ds = (new DataLayer()).get_tbl_Mandal();
        FillDropDown(ds, ddlMandal, "DivName", "DivisionID");
        if (ddlMandal.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
        }
    }
    private void get_tbl_Circle(int MandalId)
    {
        DataSet ds = (new DataLayer()).get_tbl_CircleByDivisionId(MandalId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    private void get_tbl_Division(int circleId, string ULBType)
    {
        DataSet ds = (new DataLayer()).get_tbl_DivisionByULBType(circleId, ULBType);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlMandal.Items.Clear();
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Mandal();
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
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
        }
    }
    protected void ddlULBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlULBType.SelectedValue == "-1")
        {
            lblMessage.Text = "Please Select a ULB Type.";
            ddlULBType.Focus();
        }
        else if(ddlCircle.SelectedValue=="")
        {
            lblMessage.Text = "Please Select District before ULB Type.";
            ddlULBType.Focus();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
        }
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
            //BindLoanReleaseGridByULB();
        }
    }
    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFY.SelectedValue == "0")
        {

            ddlFY.Focus();
        }
        else
        {
            // GetAllData(Convert.ToInt32(ddlDivision.SelectedValue));
            //BindLoanReleaseGridByULB();
           // GetEditExpenseList();
        }
    }

    protected void GetEditExpenseList()
    {
        var dist = 0;
        var ULB = 0;
        var FY = 0;
        var state = Convert.ToInt32(ddlZone.SelectedValue);
        if (ddlCircle.SelectedValue=="0"|| ddlCircle.SelectedValue == "")
        {
            dist = 0;
        }
        else
        {
            dist = Convert.ToInt32(ddlCircle.SelectedValue);// == "0"
        }
        if (ddlDivision.SelectedValue == "0" || ddlDivision.SelectedValue == "")
        {
            ULB = 0;
        }
        else
        {
            ULB = Convert.ToInt32(ddlDivision.SelectedValue);// == "0"
        }
        if (ddlFY.SelectedValue == "0" || ddlFY.SelectedValue == "")
        {
            FY = 0;
        }
        else
        {
            FY = Convert.ToInt32(ddlFY.SelectedValue);// == "0"
        }
        DataTable dt = new DataTable();
        dt = objLoan.GetVisionPlan("select",0,ULB,0,state,"",dist,FY,"","","",0,"",0,"","","","","","",0,0);
      
        if (dt != null && dt.Rows.Count > 0)
        {
            grdPost.DataSource = dt;
            grdPost.DataBind ();
        }
        else
        {
            grdPost.DataSource = dt;
            grdPost.DataBind();
            // exportToExcel.Visible = false;
            // MessageBox.Show("Record Not Found");
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
    protected void grdPost_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                var pk = Convert.ToInt16(e.CommandArgument.ToString());
                DataTable dt = new DataTable();
                dt = objLoan.GetVisionPlan("delete", 0, 0, pk, 0, "", 0, 0, "", "", "", 0, "", 0, "", "", "", "", "","",0,0);

                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBox.Show(dt.Rows[0]["Remark"].ToString());
                    GetEditExpenseList();
                }

            }
            if (e.CommandName == "edit")
            {

                string[] args = e.CommandArgument.ToString().Split('|');
                string PlanID = args[0];
                string Distid = args[1];
                string ULBID = args[2];
                string FYID = args[3];
                //var pk = Convert.ToInt16(e.CommandArgument.ToString());
                Response.Redirect("CreateVisionPlan.aspx?id=" + PlanID+"&&Dist="+Distid+"&&ULBID="+ULBID+"&&FYID="+FYID+"");
            }

            if (e.CommandName == "Action")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string PlanID = args[0];
                string Distid = args[1];
                string ULBID = args[2];
                string FYID = args[3];
                //var pk = Convert.ToInt16(e.CommandArgument.ToString());
                Response.Redirect("ActionOnVisionPlan.aspx?id=" + PlanID + "&&Dist=" + Distid + "&&ULBID=" + ULBID + "&&FYID=" + FYID + "");


            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        var check = "";
        if(RadioButton1.Checked==true)
        {
            check = "1";
        }
        if (RadioButton2.Checked == true)
        {
            check = "2";
        }
        if (RadioButton3.Checked == true)
        {
            check = "3";
        }
        var state = Convert.ToInt32(ddlZone.SelectedValue);
        var mandal = Convert.ToInt32(ddlMandal.SelectedValue);
        var dist = 0;

        try
        {
            dist = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            dist = 0;
        }

        string UlbType = "-1";
        try
        {
            UlbType = ddlULBType.SelectedValue.ToString();
        }
        catch
        {
            UlbType = "-1";
        }

        var ulb = 0;
        if(ddlDivision.SelectedValue!="")
        {
            ulb =Convert.ToInt32(ddlDivision.SelectedValue);
        }
        var ExpAmtLess = 0;
        var ExpAmtGret = 0;

        ExpAmtLess = string.IsNullOrEmpty(txtExpAmtLess.Text) ? 0 : Convert.ToInt32(txtExpAmtLess.Text);
        ExpAmtGret = string.IsNullOrEmpty(txtExpAmtGret.Text) ? 0 : Convert.ToInt32(txtExpAmtGret.Text);

        var year = txtYear.Text;
        var fy = Convert.ToInt32(ddlFY.SelectedValue);
        var priority = DdlPriority.SelectedValue;

        string FromDate = "", ToDate = "";

        if (txtFromDate.Text == "")
        {
            FromDate = "1900-01-01";
        }
        else
        {
            FromDate = txtFromDate.Text;
        }

        if (txtToDate.Text == "")
        {
            ToDate = "9999-12-31";
        }
        else
        {
            ToDate = txtToDate.Text;
        }

        DataTable dt = new DataTable();
        dt = objLoan.GetVisionPlanForReport("select", ulb, state, check, dist, fy, year, priority, -1, UlbType, mandal, ExpAmtLess, ExpAmtGret, FromDate, ToDate, Convert.ToInt32(Session["Person_Id"].ToString()));

        if (dt != null && dt.Rows.Count > 0)
        {
            grdPost.DataSource = dt;
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = dt;
            grdPost.DataBind();
        }

    }
    protected void grdPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // Check if the session variable "ReadOnly" is set to 1
            if (Session["Person_Id"] != null && Session["UserType"].ToString() == "8")
            {
                // Find the last column (in this case, it's the <td> containing the LinkButtons)
                HtmlTableCell lastColumn = e.Item.FindControl("LastColumn") as HtmlTableCell;

                if (lastColumn != null)
                {
                    // Hide the last column
                    lastColumn.Visible = false;
                }
            }
        }
    }

    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        //int MandalId = Convert.ToInt32(Session["MandalId"]);
        //int MandalId = Session["MandalId"] is int mandalId ? mandalId : Convert.ToInt32(Session["MandalId"]);
        int MandalId = Session["MandalId"] != null && !string.IsNullOrEmpty(Session["MandalId"].ToString())
               ? Convert.ToInt32(Session["MandalId"])
               : 0;
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
            if (MandalId>0)
            {
                SetDropdownValueAndDisable(ddlMandal, MandalId);
                if (circleId > 0)
                {
                    SetDropdownValueAndDisable(ddlCircle, circleId);
                    if (divisionId > 0)
                    {
                        SetDropdownValueAndDisable(ddlDivision, divisionId);
                        ddlULBType.Enabled = false;
                    }
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

            else if(ddl.ID.ToString()== "ddlMandal")
            {
                ddlMandal_SelectedIndexChanged(ddl, EventArgs.Empty);
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

    //protected void ExportExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //                // Disable paging and rebind the GridView to include all data
    //                //gridMPWise.AllowPaging = false;

    //                var dist = 0;
    //                var ULB = 0;
    //                var FY = 0;
    //                var state = Convert.ToInt32(ddlZone.SelectedValue);
    //                if (ddlCircle.SelectedValue == "0" || ddlCircle.SelectedValue == "")
    //                {
    //                    dist = 0;
    //                }
    //                else
    //                {
    //                    dist = Convert.ToInt32(ddlCircle.SelectedValue);// == "0"
    //                }
    //                if (ddlDivision.SelectedValue == "0" || ddlDivision.SelectedValue == "")
    //                {
    //                    ULB = 0;
    //                }
    //                else
    //                {
    //                    ULB = Convert.ToInt32(ddlDivision.SelectedValue);// == "0"
    //                }
    //                if (ddlFY.SelectedValue == "0" || ddlFY.SelectedValue == "")
    //                {
    //                    FY = 0;
    //                }
    //                else
    //                {
    //                    FY = Convert.ToInt32(ddlFY.SelectedValue);// == "0"
    //                }
    //                DataTable dt = new DataTable();
    //                dt = objLoan.GetVisionPlan("select", 0, ULB, 0, state, "", dist, FY, "", "", "", 0, "", 0, "", "", "", "", "");

    //                //dt = objLoan.getFYWiseData(WorkProposalID);
    //                //grdPost.AllowPaging = false;


    //                if (dt != null && dt.Rows.Count > 0)
    //                {
    //                    grdPost.DataSource = dt;
    //                    grdPost.DataBind();
    //            //ExportToExcel();

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //        return;
    //    }
    //}

}