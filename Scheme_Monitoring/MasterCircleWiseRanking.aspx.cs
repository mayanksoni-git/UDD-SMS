using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterCircleWiseRanking : System.Web.UI.Page
{
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
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();

            get_Circle_Wise_Ranking(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text));
        }
    }

    private void get_Circle_Wise_Ranking(int Month, int Year)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Circle_Wise_Ranking(Month, Year, "Circle");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["Zone_Id"] = 0;
            dr["Circle_Id"] = 0;
            dr["AMRUT_Marks"] = "30 Marks";
            dr["State_Marks"] = "10 Marks";
            dr["DW_Marks"] = "10 Marks";
            dr["Handover_after_trail_run"] = "10 Marks";
            dr["Handover_of_DLP_over_schemes"] = "10 Marks";
            dr["SE_GM_Field_Visit_Marks"] = "10 Marks";
            dr["EE_PM_Field_Visit_Marks"] = "10 Marks";
            dr["Additional_Deposit_Works"] = "5 Marks";
            dr["DPR_approved"] = "5 Marks";
            dr["Total_Marks"] = "100 Marks";

            ds.Tables[0].Rows.InsertAt(dr, 0);

            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
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
        get_Circle_Wise_Ranking(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text));
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Trim() == "0")
            {
                e.Row.BackColor = Color.LightBlue;
                e.Row.Font.Bold = true;
            }
        }
    }

    protected void btnSwitch_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterZoneWiseRanking.aspx");
    }
}
