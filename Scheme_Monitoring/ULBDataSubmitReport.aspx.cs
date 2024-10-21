using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ULBDataSubmitReport : System.Web.UI.Page
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

            //get_tbl_FinancialYear();
            //get_tbl_Month();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    //private void get_tbl_FinancialYear()
    //{
    //    DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
    //    FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    //}
    private void get_tbl_Circle()
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle1();
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        //else
        //{
        //    ddlDivision.Items.Clear();
        //}
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
    //private void get_tbl_Month()
    //{
    //    DataSet ds = (new DataLayer()).get_tbl_Month();
    //    FillDropDown(ds, ddlMonth, "Month_MonthName", "Month_Id");
    //}
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetAllData();
    }
    protected void GetAllData()
    {
        //ULBID = 0;
        var dist = 0;
        var ULB = 0;
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

        if (ddlDivision.SelectedValue == "0" || ddlDivision.SelectedValue == "")
        {
            ULB = 0;
        }
        else
        {
            ULB = Convert.ToInt32(ddlDivision.SelectedValue);// == "0"
        }
        //if (ddlMonth.SelectedValue == "0" || ddlMonth.SelectedValue == "")
        //{
        //    month = 0;
        //}
        //else
        //{
        //    month = Convert.ToInt32(ddlMonth.SelectedValue);// == "0"
        //}
        DataTable dt = new DataTable();
        var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        dt = objLoan.GetULBdataSubmittedReport("getULBsubmittedDataReport", dist, ULB, Person_Id);
        grdPost.DataSource = dt;
        grdPost.DataBind();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string GetULBDetails(int Circle_Id)
    {
        string connectionString = ConfigurationManager.AppSettings["conn"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                string query = @"
                SELECT 
                    CASE WHEN adp.ULBID IS NOT NULL THEN d.Division_Name END AS SUBMITTED_DATA,
                    CASE WHEN adp.ULBID IS NULL THEN d.Division_Name END AS NOT_SUBMITTED_DATA
                FROM 
                    tbl_Division d 
                LEFT JOIN 
                    tbl_AdoptedParkMaster adp ON d.Division_Id = adp.ULBID 
                WHERE 
                    d.Division_CircleId = @Circle_Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Circle_Id", Circle_Id); // Use parameterized query to prevent SQL injection
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                // Convert DataTable to JSON
                string jsonResult = JsonConvert.SerializeObject(dt);
                return jsonResult;
            }
            catch (Exception ex)
            {
                // Handle exception (consider logging the error)
                return JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }
    }
}