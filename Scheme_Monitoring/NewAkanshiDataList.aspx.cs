using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewAkanshiDataList : System.Web.UI.Page
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
        dt = objLoan.GetNewAkanshiDataList("select", dist, FY, Division, Person_Id);
        grdPost.DataSource = dt;
        grdPost.DataBind();

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
         GridViewRow gr = (sender as ImageButton).NamingContainer as GridViewRow;
        int newAkanshi_Id = Convert.ToInt32(grdPost.DataKeys[gr.RowIndex].Values["newAkanshi_Id"]);
        int newAkanshiDetail_Id = Convert.ToInt32(grdPost.DataKeys[gr.RowIndex].Values["newAkanshiDetail_Id"]);
        //Response.Redirect("AddNewAkanshiData.aspx?newAkanshi_Id=" + newAkanshi_Id.ToString() + "&newAkanshiDetail_Id=" + newAkanshiDetail_Id.ToString());
        Response.Redirect("AddNewAkanshiData.aspx?newAkanshi_Id=" + newAkanshi_Id.ToString());

    }
    [WebMethod]
    public static string GetHeadDetails(int newAkanshi_Id)
    {
        string connectionString = ConfigurationManager.AppSettings["conn"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                string query = @"
               select AkanshiHead,AHD.CostPerHead,NoOfHead,Amount from tbl_NewAkanshiData_HeadDetails AHD left join uddadmindbuser.AkanshiHeadMaster AHM 
                on AHD.AkanshiHeadId=AHM.AkanshiHeadID Where AHD.newAkanshi_Id=@newAkanshi_Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newAkanshi_Id", newAkanshi_Id); // Use parameterized query to prevent SQL injection
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