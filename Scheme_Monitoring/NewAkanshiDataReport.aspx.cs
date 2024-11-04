using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewAkanshiDataReport : System.Web.UI.Page
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
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            //get_tbl_AkanshiHead();
            get_tbl_FinancialYear();
            get_tbl_Circle(1);
        }
    }
    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearFromId(19);
        AllClasses.FillDropDown(ds.Tables[0], ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }



    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
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
 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetAllData();
    }
    protected void GetAllData()
    {
        //ULBID = 0;
        var dist = 0;
        var Division = 0;
        var FY = 0;
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
        if (ddlDivision.SelectedValue == "0" || ddlDivision.SelectedValue == "")
        {
            Division = 0;
        }
        else
        {
            Division = Convert.ToInt32(ddlDivision.SelectedValue);// == "0"
        }
        DataTable dt = new DataTable();
        var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        dt = objLoan.GetNewAkanshiDataReport("select", dist, FY, Division, Person_Id);
        if (dt.Rows.Count > 0)
        {
            // Clear existing columns
            grdPost.Columns.Clear();

           

            // Bind data to GridView
            grdPost.DataSource = dt;
            grdPost.DataBind();

            // Calculate and set footer totals
            CalculateTotals(dt);
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
       

    }
    private void CalculateTotals(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName.StartsWith("Total")) // Assuming columns start with 'Total'
                {
                    // Sum the values in the column, handling nulls
                    decimal total = dt.AsEnumerable()
                        .Sum(row => row.Field<decimal?>(column.ColumnName) ?? 0); // Use nullable decimal and default to 0 if null

                    // Find the corresponding footer label
                    Label lblFooter = (Label)grdPost.FooterRow.FindControl("lblTotal" + column.ColumnName);
                    if (lblFooter != null)
                    {
                        lblFooter.Text = total.ToString("0.##"); // Format the total as needed
                    }
                }
            }

        }
    }

}