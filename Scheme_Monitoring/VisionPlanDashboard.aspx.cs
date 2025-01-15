using System;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VisionPlanDashboard : System.Web.UI.Page
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

        LoadVisionPlanGrid(ProcedureName);
        ToggleDiv(divDashboard);
    }

    

    protected void btnULBWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBWise";
        string ProcedureName = "sp_ULBWiseReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    protected void btnCircleWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_UniqueCirclesWiseVisionReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
        ToggleDiv(divGrid);
    }
    protected void btnPriorityWise_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_SelfPriorityWiseVisionReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
        ToggleDiv(divGrid);
    }
    protected void btnProjectType_Click(object sender, EventArgs e)
    {
        //string GridName = "divULBType";
        string ProcedureName = "sp_ProjectTypeWiseVisionReport_VPReport";

        LoadVisionPlanGrid(ProcedureName);
        ToggleDiv(divGrid);
    }

    private void LoadVisionPlanGrid(string ProcedureName)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalDashbaord(ProcedureName);

        if (dt != null && dt.Rows.Count > 0)
        {
            

            if (ProcedureName == "sp_DashboardSummaryReport_VPReport")
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

                Label8.Text = dt.Rows[0][7].ToString();
                //HeadLabel8.Text = dt.Columns[7].ColumnName;

                Label9.Text = dt.Rows[0][8].ToString();
                HeadLabel9.Text = dt.Columns[8].ColumnName;

                Label10.Text = dt.Rows[0][9].ToString();
                HeadLabel10.Text = dt.Columns[9].ColumnName;

                Label11.Text = dt.Rows[0][10].ToString();
                HeadLabel11.Text = dt.Columns[10].ColumnName; 

                Label12.Text = dt.Rows[0][11].ToString();
                HeadLabel12.Text = dt.Columns[11].ColumnName; 

                Label13.Text = dt.Rows[0][12].ToString();
                HeadLabel13.Text = dt.Columns[12].ColumnName; 

                Label14.Text = dt.Rows[0][13].ToString();
                HeadLabel14.Text = dt.Columns[13].ColumnName;
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
    //public static string btnTotalProjects_Click(int newAkanshi_Id)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        VisionPlan objVisionPlan = new VisionPlan();
    //        dt = objVisionPlan.getTotalProjectFinancialYearWise();

    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            string jsonResult = JsonConvert.SerializeObject(dt);
    //            return jsonResult;
    //        }
    //        else
    //        {
    //            return JsonConvert.SerializeObject(new { error = "Record Not Found" });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle exception (consider logging the error)
    //        return JsonConvert.SerializeObject(new { error = ex.Message });
    //    }
    //}

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

    #region Tile 1 and Tile 2
    [WebMethod]
    public static string btnTotalProjects_Click(int newAkanshi_Id)
    {
        return HandleRequest(() => new VisionPlan().getTotalProjectFinancialYearWise());
    }

    [WebMethod]
    public static string GetTotalProjectsUlbWiseByFYID(int FYID)
    {
        return HandleRequest(() => new VisionPlan().getTotalProjectsUlbWiseByFYID(FYID));
    }

    [WebMethod]
    public static string GetProjectByFYIDandULB(int FYID, int ULBID)
    {
        return HandleRequest(() => new VisionPlan().getProjectByFYIDandULB(FYID, ULBID));
    }
    #endregion

    #region Tile 3
    [WebMethod]
    public static string btnTotalULBRepoted_Click(int newAkanshi_Id)
    {
        return HandleRequest(() => new VisionPlan().getTotalULBFinancialYearWise());
    }

    [WebMethod]
    public static string GetUlbDetailsByFYID(int FYID)
    {
        return HandleRequest(() => new VisionPlan().getULBByFYID(FYID));
    }
    #endregion

    #region Tile 8
    [WebMethod]
    public static string btnTotalDocUp_Click()
    {
        return HandleRequest(() => new VisionPlan().getVisionPlanDocumentFYWise());
    }

    [WebMethod]
    public static string GetTotalDocumentsULBWiseByFYID(int FYID)
    {
        return HandleRequest(() => new VisionPlan().getTotalDocumentsULBWiseByFYID(FYID));
    }
    #endregion

    #region  Tile 10
    [WebMethod]
    public static string btnTotalULBPendingToUp_Click()
    {
        return HandleRequest(() => new VisionPlan().getGetULBPendingToUploadFYWise());
    }

    [WebMethod]
    public static string GetUlBPendingToUpByFYID(int FYID)
    {
        return HandleRequest(() => new VisionPlan().getUlBPendingToUpByFYID(FYID));
    }
    #endregion

    #region Tile 13
    [WebMethod]
    public static string btnTotalDocUpDPR_Click()
    {
        return HandleRequest(() => new VisionPlan().getDPRDocumentFYWise());
    }

    #endregion
    #endregion
}