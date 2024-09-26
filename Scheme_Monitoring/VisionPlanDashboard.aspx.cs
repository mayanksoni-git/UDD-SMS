using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VisionPlanDashboard : System.Web.UI.Page
{
    Loan objLoan = new Loan();

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

        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void btnDashboard_Click(object sender, EventArgs e)
    {
        //string GridName = "divDashboard";
        string ProcedureName = "sp_DashboardSummaryReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
    }

    protected void btnULBWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBWise";
        string ProcedureName = "sp_ULBWiseReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
    }

    protected void btnCircleWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_UniqueCirclesWiseVisionReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
    }
    protected void btnPriorityWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_SelfPriorityWiseVisionReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
    }

    private void LoadVisionPlanGrid(string ProcedureName)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalDashbaord(ProcedureName);

        if (dt != null && dt.Rows.Count > 0)
        {
            gridDashboard.DataSource = dt;
            gridDashboard.DataBind();
            btnDashboard.Visible = true;
            ToggleDiv(divData);
        }
        else
        {
            btnDashboard.Visible = true;
            gridDashboard.DataSource = null;
            gridDashboard.DataBind();
            MessageBox.Show("No Records Found");
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

    

    private void ToggleDiv(System.Web.UI.HtmlControls.HtmlGenericControl div)
    {
        divData.Visible = false;
        //divMPWise.Visible = false;
        //divMLAWise.Visible = false;
        //divDivisionWise.Visible = false;
        //divWorkPlanWise.Visible = false;
        //divDistrictWise.Visible = false;
        //divRecommendationWise.Visible = false;
        div.Visible = true;
        div.Focus();
    }
}