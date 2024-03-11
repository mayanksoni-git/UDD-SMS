using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkVariationDocument : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                string Scheme_Id = "";
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int ProjectWork_Id = 0;

                try
                {
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                }
                catch
                {
                    Scheme_Id = "";
                }

                try
                {
                    ProjectWork_Id = Convert.ToInt32(Request.QueryString["ProjectWork_Id"].ToString());
                }
                catch
                {
                    ProjectWork_Id = 0;
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
                
                get_tbl_ProjectWork(Zone_Id, Circle_Id, Division_Id, Scheme_Id, ProjectWork_Id);
            }
        }
    }
   
    protected void get_tbl_ProjectWork(int Zone_Id, int Circle_Id, int Division_Id, string Scheme_Id, int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Variation_Document_Log(Zone_Id, Circle_Id, Division_Id, Scheme_Id, ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdVariationDocuments.DataSource = ds.Tables[0];
            grdVariationDocuments.DataBind();
        }
        else
        {
            grdVariationDocuments.DataSource = null;
            grdVariationDocuments.DataBind();
        }
    }

    protected void grdVariationDocuments_PreRender(object sender, EventArgs e)
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

    protected void grdVariationDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[6].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[7].Text = Session["Default_Division"].ToString();
        }
    }
}