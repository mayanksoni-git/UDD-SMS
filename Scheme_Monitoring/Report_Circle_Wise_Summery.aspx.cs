using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Circle_Wise_Summery : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            get_tbl_Project();
            if (Session["UserType"].ToString() != "1")
            {
                try
                {
                    if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
                    {
                        //ddlScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
                        ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
                       // ddlScheme.Enabled = false;
                    }
                }
                catch
                {

                }
            }
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["Scheme_Id"] !=null)
                {
                    ddlScheme.SelectedValue = Request.QueryString["Scheme_Id"].ToString();
                }

                if (Request.QueryString["Zone_Id"] != null)
                {
                    get_Circle_Wise_Analysis(Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString()), Convert.ToInt32(Request.QueryString["Zone_Id"].ToString()));
                }
            }
        }
    }
    private void get_Circle_Wise_Analysis(int Scheme_Id, int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Circle_Wise_Analysis(Scheme_Id, Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
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

    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Project(0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Project(Convert.ToInt32(Session["Person_Id"].ToString()));
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlScheme.Items.Clear();
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
            return;
        }
        else
        {
            int Scheme_id = 0;
            Scheme_id = Convert.ToInt32(ddlScheme.SelectedValue);

            int Zone_Id = 0;

            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            if (Request.QueryString["Zone_Id"] != null)
            {
                Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
            }
            get_Circle_Wise_Analysis(Scheme_id, Zone_Id);
        }
    }

    protected void lnkCircle_Click(object sender, EventArgs e)
    {
        LinkButton lnkZone = sender as LinkButton;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        (lnkZone.Parent.Parent as GridViewRow).BackColor = Color.LightGreen;
        GridViewRow gr = ((sender as LinkButton).Parent.Parent as GridViewRow);
        Response.Redirect("Report_Division_Wise_Summery.aspx?Circle_Id=" + gr.Cells[0].Text.Trim() + "&Scheme_Id=" + ddlScheme.SelectedValue);
    }
}