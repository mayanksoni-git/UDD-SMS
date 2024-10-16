using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParkAdoptionStatusReport : System.Web.UI.Page
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
          //  lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            get_tbl_Circle();

            get_tbl_FinancialYear();
            get_tbl_Month();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }
    private void get_tbl_Circle()
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle1();
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    private void get_tbl_Month()
    {
        DataSet ds = (new DataLayer()).get_tbl_Month();
        FillDropDown(ds, ddlMonth, "Month_MonthName", "Month_Id");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        GetAllData();
        }
    protected void GetAllData()
    {
        //ULBID = 0;
        var dist = 0;
        var month = 0;
        var FY = 0;
        var scheme = 0;
        var circle = Convert.ToInt32(ddlCircle.SelectedValue);
        if (ddlCircle.SelectedValue == "0" || ddlCircle.SelectedValue == "")
        {
            dist = 0;
        }
        else
        {
            dist = Convert.ToInt32(ddlCircle.SelectedValue);// == "0"
        }
      
        if (ddlFY.SelectedValue == "0" || ddlFY.SelectedValue == "")
        {
            FY = 0;
        }
        else
        {
            FY = Convert.ToInt32(ddlFY.SelectedValue);// == "0"
        }
        if (ddlMonth.SelectedValue == "0" || ddlMonth.SelectedValue == "")
        {
            month = 0;
        }
        else
        {
            month = Convert.ToInt32(ddlMonth.SelectedValue);// == "0"
        }
        DataTable dt = new DataTable();
        dt = objLoan.GetParkAdoptionReport("select", dist, FY, month);
        grdPost.DataSource = dt;
        grdPost.DataBind();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
        {

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

}