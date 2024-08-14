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
                
                
                //GetEditExpenseList(ULBID.Value, FYID.Value);
               
                //AddSection.Visible = false;
            }
  
           
            get_tbl_Zone();
           

            get_tbl_FinancialYear();

            GetEditExpenseList();
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
            
            ddlDivision.Focus();
        }
        else
        {
           // GetAllData(Convert.ToInt32(ddlDivision.SelectedValue));
            //BindLoanReleaseGridByULB();
        }
    }
   protected void GetEditExpenseList()
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetVisionPlan("select",0,0,0,0,"",0,0,"","","",0,"",0,"","","","","");
      
        if (dt != null && dt.Rows.Count > 0)
        {
            rptSearchResult.DataSource = dt;
            rptSearchResult.DataBind ();
            
           


        }
        else
        {
            // exportToExcel.Visible = false;
            MessageBox.Show("Record Not Found");

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

    protected void rptSearchResult_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                var pk = Convert.ToInt16(e.CommandArgument.ToString());
                DataTable dt = new DataTable();
                dt = objLoan.GetVisionPlan("delete", 0, 0, pk, 0, "", 0, 0, "", "", "", 0, "", 0, "", "", "", "", "");

                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBox.Show(dt.Rows[0]["Remark"].ToString());
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
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        }
      protected void BtnSearch_Click(object sender, EventArgs e)
    {
        var state = Convert.ToInt32(ddlZone.SelectedValue);
        var dist = Convert.ToInt32(ddlCircle.SelectedValue);
        var ulb = 0;
        if(ddlDivision.SelectedValue!="")
        {
            ulb =Convert.ToInt32(ddlDivision.SelectedValue);
        }
       
        var fy = Convert.ToInt32(ddlFY.SelectedValue);
        var priority = DdlPriority.SelectedValue;
        DataTable dt = new DataTable();
        dt = objLoan.GetVisionPlan("select", 0, ulb, 0, state, "", dist, fy, "", "", "", 0, "", 0, "", priority, "", "", "");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptSearchResult.DataSource = dt;
            rptSearchResult.DataBind();




        }
        else
        {
            // exportToExcel.Visible = false;
            MessageBox.Show("Record Not Found");
            GetEditExpenseList();
        }

    }
}