using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterProjectWork_DataEntry_View : System.Web.UI.Page
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
            string Client = ConfigurationManager.AppSettings.Get("Client");
            
            if (Request.QueryString.Count > 0)
            {
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int Mode = 0;
                
                try
                {
                    Mode = Convert.ToInt32(Request.QueryString["Mode"].ToString());
                }
                catch
                {
                    Mode = 0;
                }
                
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                }
                catch
                {
                    Division_Id = 0;
                }
                get_Data_Updation_Status_Details(Zone_Id, Circle_Id, Division_Id, Mode);
            }
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

    protected void get_Data_Updation_Status_Details(int Zone_Id, int Circle_Id, int Division_Id, int Mode)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Data_Updation_Status_Details(Zone_Id, Circle_Id, Division_Id, Mode);
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        GridViewRow gr = lnkUpdate.Parent.Parent as GridViewRow;
        string Client = System.Configuration.ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            Response.Redirect("MasterProjectWork_DataEntry.aspx?ProjectWork_Id=" + gr.Cells[0].Text.Trim() + "&Scheme_Id=" + gr.Cells[1].Text.Trim());
        }
        else
        {
            Response.Redirect("MasterProjectWorkMIS_1.aspx?ProjectWork_Id=" + gr.Cells[0].Text.Trim());
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlGenericControl divMISSteps = e.Row.FindControl("divMISSteps") as HtmlGenericControl;
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divMISSteps.Visible = false;
            }
            else
            {
                divMISSteps.Visible = true;
            }
        }
    }
}