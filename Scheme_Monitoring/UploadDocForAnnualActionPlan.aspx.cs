using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;

public partial class UploadDocForAnnualActionPlan : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
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
            lblZoneH.Text = Session["Default_Zone"].ToString() + "*";
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            lblDivisionH.Text = Session["Default_Division"].ToString() + "*";
           
            message.InnerText = "";
            get_tbl_Zone();
           
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
           // GetAllData();
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
            MessageBox.Show("Please Select a Financial Year. ");
            ddlFY.Focus();
            return false;
        }
       
        //if (ddlProject.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select a Project. ");
        //    ddlDivision.Focus();
        //    return false;
        //}
        //if (string.IsNullOrWhiteSpace(txtDepositAmount.Text))
        //{
        //    MessageBox.Show("Please enter Deposit Amount!");
        //    txtDepositAmount.Focus();
        //    return false;
        //}

        //if (string.IsNullOrWhiteSpace(txtDepositDate.Text))
        //{
        //    MessageBox.Show("Please enter Deposit Date!");
        //    txtDepositDate.Focus();
        //    return false;
        //}
        else
        {
            return true;
        }
    }
    protected void GetAllData()
    {
        // ULBID = 0;

        var dist = 0;
        var ULB = 0;
        var FY = 0;
       
        var state = Convert.ToInt32(ddlZone.SelectedValue);
        if (ddlCircle.SelectedValue == "0" || ddlCircle.SelectedValue == "")
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
        dt = objLoan.GetDocOfAnnualActionPlan("select", ULB, 0, 0,  dist, FY,  0, "","");
        grdPost.DataSource = dt;
        grdPost.DataBind();
      
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

   
  
    protected void Edit_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());
        Response.Redirect("CreateDocForAnnualActionPlan.aspx?id=" + id);
       
    }

   
  
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());

        DataTable dt = new DataTable();
        dt = objLoan.GetDocOfAnnualActionPlan("Delete", 0, id, 0, 0, 0, 0, "","");
        //dt = objLoan.GetAnnualActionPlan("Delete", 0, id, 0, 0, 0, 0, "", 0, "", 0, "", "", "", "");
        if (dt != null && dt.Rows.Count > 0)
        {
           MessageBox.Show( dt.Rows[0]["Remark"].ToString());

        }

        GetAllData();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        GetAllData();
    }
}