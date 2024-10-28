using System;
using System.Data;
using System.Text;
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
    protected void btnTotalProjects_Click(object sender, EventArgs e)
    {
        mp1.Show();
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
                HeadLabel8.Text = dt.Columns[7].ColumnName;

                Label9.Text = dt.Rows[0][8].ToString();
                HeadLabel9.Text = dt.Columns[8].ColumnName;

                Label10.Text = dt.Rows[0][9].ToString();
                HeadLabel10.Text = dt.Columns[9].ColumnName;

                Label11.Text = dt.Rows[0][10].ToString();
                HeadLabel11.Text = dt.Columns[10].ColumnName;
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


    //private void LoadVisionPlanGrid(string ProcedureName)
    //{
    //    DataTable dt = new DataTable();
    //    dt = objLoan.getWorkProposalDashbaord(ProcedureName);

    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        gridDashboard.DataSource = dt;
    //        gridDashboard.DataBind();
    //        btnDashboard.Visible = true;

    //        // Assuming you have predefined labels up to Label8 and HeadLabel8 on your form
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            // Dynamically find the label controls
    //            Label valueLabel = (Label)FindControl("Label"+i + 1);
    //            Label headerLabel = (Label)FindControl("HeadeLabel"+i + 1);

    //            if (valueLabel != null && headerLabel != null)
    //            {
    //                // Set the value from the first row of the DataTable
    //                valueLabel.Text = dt.Rows[0][i].ToString();
    //                // Set the column name as the header
    //                headerLabel.Text = dt.Columns[i].ColumnName;
    //            }
    //        }

    //        ToggleDiv(divData);
    //    }
    //    else
    //    {
    //        btnDashboard.Visible = true;
    //        gridDashboard.DataSource = null;
    //        gridDashboard.DataBind();
    //        MessageBox.Show("No Records Found");
    //    }
    //}



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

    protected void btnclose_Click(object sender, EventArgs e)
    {
        // Code to hide the modal popup
        Panel1.Style["display"] = "none";
    }
}