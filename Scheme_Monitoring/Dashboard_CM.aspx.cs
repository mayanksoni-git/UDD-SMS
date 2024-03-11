using Aspose.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Dashboard_CM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Scheme_Id"] = "1013";
            get_PMIS_Dashboard_View(ViewState["Scheme_Id"].ToString());
            get_tbl_ProjectWork_CM_Dashboard(ViewState["Scheme_Id"].ToString());
            mp1.Show();
        }
    }
    protected void get_PMIS_Dashboard_View(string Scheme_Id)
    {
        int District_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_CM_Dashboard_View(Scheme_Id, District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTotalProjects.Text = ds.Tables[0].Rows[0]["Total_Projects"].ToString();
            lnkWater.Text = ds.Tables[0].Rows[0]["Water_Supply"].ToString();
            lnkBuilding.Text = ds.Tables[0].Rows[0]["Building_Works"].ToString();
            lnkDranage.Text = ds.Tables[0].Rows[0]["Drainage"].ToString();
            lnkSolidWaste.Text = ds.Tables[0].Rows[0]["Solid_Waste"].ToString();
            lnkSeptage.Text = ds.Tables[0].Rows[0]["Septage"].ToString();
            lnkSewarage.Text = ds.Tables[0].Rows[0]["Sewerage"].ToString();
            lnkTarget_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTarget_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompleted.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoing.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompleted.Text = "0";
            lnkOnGoing.Text = "0";
            lnkDranage.Text = "0";
            lnkSolidWaste.Text = "0";
            lnkTotalProjects.Text = "0";
            lnkWater.Text = "0";
            lnkBuilding.Text = "0";
            lnkSeptage.Text = "0";
            lnkSewarage.Text = "0";
            lnkTarget_C.Text = "0";
            lnkTarget_N.Text = "0";
        }
    }
    private void get_tbl_ProjectWork_CM_Dashboard(string Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_CM_Dashboard(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Projects)", "").ToString(), "int");
            grdPost.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Sanction_Cost)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(tender_cost)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Expenditure)", "").ToString(), "decimal");

            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdPost.Columns.Count; i++)
            {
                if (grdPost.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic1.Value = jss.Serialize(columnsList);
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

    protected void lnkDistrict_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        try
        {
            District_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            District_Id = 0;
        }
        if (District_Id > 0)
        {
            Response.Redirect("Dashboard_CM_District.aspx?District_Id=" + District_Id.ToString() + "&Scheme_Id=" + ViewState["Scheme_Id"].ToString());
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[3].Text, "int");
            e.Row.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[4].Text, "decimal");
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text, "decimal");
            e.Row.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[6].Text, "decimal");
        }
    }

    protected void lnkTotalProjects_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard_CM_District.aspx?District_Id=0&Scheme_Id=" + ViewState["Scheme_Id"].ToString());
    }

    protected void lnkWater_Click(object sender, EventArgs e)
    {

    }

    protected void lnkSewarage_Click(object sender, EventArgs e)
    {

    }

    protected void lnkBuilding_Click(object sender, EventArgs e)
    {

    }

    protected void lnkSeptage_Click(object sender, EventArgs e)
    {

    }

    protected void lnkDranage_Click(object sender, EventArgs e)
    {

    }

    protected void lnkSolidWaste_Click(object sender, EventArgs e)
    {

    }

    protected void lnkCompleted_Click(object sender, EventArgs e)
    {

    }

    protected void lnkOnGoing_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTarget_C_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTarget_N_Click(object sender, EventArgs e)
    {

    }

    protected void btnAmrut1_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Scheme_Id"] = "1013";
        get_PMIS_Dashboard_View(ViewState["Scheme_Id"].ToString());
        get_tbl_ProjectWork_CM_Dashboard(ViewState["Scheme_Id"].ToString());
    }

    protected void btnAmrut2_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Scheme_Id"] = "1016";
        get_PMIS_Dashboard_View(ViewState["Scheme_Id"].ToString());
        get_tbl_ProjectWork_CM_Dashboard(ViewState["Scheme_Id"].ToString());
    }
}
