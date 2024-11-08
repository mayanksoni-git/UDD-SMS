using System;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WorkProposalDashboard : System.Web.UI.Page
{
    Loan objLoan = new Loan();
    WorkProposal objWP = new WorkProposal();

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
        string ProcedureName = "sp_ReportDashboard_WPReport_Test";

        LoadWorkProposalGrid(ProcedureName);
        ToggleDiv(divDashboard);
    }


    protected void btnULBWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBWise";
        string ProcedureName = "sp_ReportByDivision_WPReport_Test";

        LoadWorkProposalGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    protected void btnULBType_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_ReportByULBType_WPReport_Test";

        LoadWorkProposalGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    protected void btnProjectType_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_ReportByProjectType_WPReport_Test";

        LoadWorkProposalGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    protected void btnProposerType_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_ReportByProposerType_WPReport_Test";

        LoadWorkProposalGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    protected void btnSchemeWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_ReportByProjectName";

        LoadWorkProposalGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    private void LoadWorkProposalGrid(string ProcedureName)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalDashbaord(ProcedureName);

        if (dt != null && dt.Rows.Count > 0)
        {
            

            if (ProcedureName == "sp_ReportDashboard_WPReport_Test")
            {
                Label1.Text = dt.Rows[0][0].ToString();
                HeadLabel1.Text = dt.Columns[0].ColumnName;

                Label2.Text = dt.Rows[0][1].ToString();
                HeadLabel2.Text = dt.Columns[1].ColumnName;

                Label3.Text = dt.Rows[0][2].ToString();
                HeadLabel3.Text = dt.Columns[2].ColumnName;

                Label4.Text = dt.Rows[0][3].ToString();
                HeadLabel4.Text = dt.Columns[3].ColumnName;

                Label5.Text = dt.Rows[0][4].ToString();
                HeadLabel5.Text = dt.Columns[4].ColumnName;

                Label6.Text = dt.Rows[0][5].ToString();
                HeadLabel6.Text = dt.Columns[5].ColumnName;

                Label7.Text = dt.Rows[0][6].ToString();
                HeadLabel7.Text = dt.Columns[6].ColumnName;
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


    #region For Modal Popup
    [WebMethod]
    public static string btnTotalProjects_Click(int newAkanshi_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            VisionPlan objVisionPlan = new VisionPlan();
            dt = objVisionPlan.getTotalProjectFinancialYearWise();

            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonResult = JsonConvert.SerializeObject(dt);
                return jsonResult;
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

    [WebMethod]
    public static string GetTotalProjectsUlbWiseByFYID(int FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            VisionPlan objVisionPlan = new VisionPlan();
            dt = objVisionPlan.getTotalProjectsUlbWiseByFYID(FYID);

            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonResult = JsonConvert.SerializeObject(dt);
                return jsonResult;
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


    [WebMethod]
    public static string GetProjectByFYIDandULB(int FYID, int ULBID)
    {
        try
        {
            DataTable dt = new DataTable();
            VisionPlan objVisionPlan = new VisionPlan();
            dt = objVisionPlan.getProjectByFYIDandULB(FYID, ULBID);

            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonResult = JsonConvert.SerializeObject(dt);
                return jsonResult;
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
    #endregion


}