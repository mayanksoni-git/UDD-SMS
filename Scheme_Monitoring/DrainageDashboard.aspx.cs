using System;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DrainageDashboard : System.Web.UI.Page
{
    Loan objLoan = new Loan();
    VisionPlan objVisionPlan = new VisionPlan();

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
            btnDashboard_Click(null, EventArgs.Empty);
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void btnDashboard_Click(object sender, EventArgs e)
    {
        //string GridName = "divDashboard";
        string ProcedureName = "sp_DashboardSummaryReport_VPReport";

        LoadDrainageGrid(ProcedureName);
        ToggleDiv(divDashboard);
    }


    private void LoadDrainageGrid(string ProcedureName)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalDashbaord(ProcedureName);

        if (dt != null && dt.Rows.Count > 0)
        {
            

            if (ProcedureName == "sp_DashboardSummaryReport_VPReport")
            {
                //Label1.Text = dt.Rows[0][0].ToString();
                //HeadLabel1.Text = dt.Columns[0].ColumnName;
            }
            else
            {
                gridDashboard.DataSource = dt;
                gridDashboard.DataBind();
                btnDashboard.Visible = true;
            }
        }
        else
        {
            btnDashboard.Visible = true;
            gridDashboard.DataSource = null;
            gridDashboard.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void GetNoOfProjects(object sender, CommandEventArgs e)
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


    private void ToggleDiv(System.Web.UI.HtmlControls.HtmlGenericControl div)
    {
        divDashboard.Visible = false;
        divGrid.Visible = false;
        div.Visible = true;
        div.Focus();
    }



    #region Refactored Code
  

    [WebMethod]
    public static string HandleRequest(Func<DataTable> dataFetcher)
    {
        try
        {
            DataTable dt = dataFetcher.Invoke();

            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return JsonConvert.SerializeObject(new { error = "Record Not Found" });
            }
        }
        catch (Exception ex)
        {
            // Handle exception (consider logging the error)
            return JsonConvert.SerializeObject(new { error = ex.Message });
        }
    }

    #region Tile 1
    [WebMethod]
    public static string btnTotalProjects_Click(int newAkanshi_Id)
    {
        return HandleRequest(() => new VisionPlan().getTotalProjectFinancialYearWise());
    }
    #endregion

    #endregion
}