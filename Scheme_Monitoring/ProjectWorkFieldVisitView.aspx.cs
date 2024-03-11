using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectWorkFieldVisitView : System.Web.UI.Page
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
            int ProjectWork_Id = 0;
            int Division_Id = 0;
            int Added_By = 0;
            string Mode = "";
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
                Mode = Request.QueryString["Mode"].ToString();
            }
            catch
            {
                Mode = "";
            }
            try
            {
                Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
            }
            catch
            {
                Division_Id = 0;
            }
            try
            {
                Added_By = Convert.ToInt32(Request.QueryString["Added_By"].ToString());
            }
            catch
            {
                Added_By = 0;
            }
            Load_Project(ProjectWork_Id, Added_By, Division_Id);
        }
    }
    private void Load_Project(int ProjectWork_Id, int Added_By, int Division_Id)
    {
        get_tbl_ProjectWork(ProjectWork_Id, Added_By, Division_Id, "");
        if (ProjectWork_Id > 0)
        {
            get_tbl_ProjectVisit(ProjectWork_Id);
        }
        else
        {
            if (grdPost1.Rows.Count > 0)
            {
                ImageButton btnView1 = grdPost1.Rows[0].FindControl("btnView1") as ImageButton;
                btnView1_Click(btnView1, new ImageClickEventArgs(0, 0));
            }
        }
    }
    protected void btnView1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = ((sender as ImageButton).Parent.Parent as GridViewRow);
        int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        get_tbl_ProjectVisit(ProjectWork_Id);
    }
    protected void get_tbl_ProjectWork(int ProjectWork_Id, int Added_By, int Division_Id, string VType)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork("", 0, 0, 0, Division_Id, 0, 0, "", ProjectWork_Id, Added_By);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost1.DataSource = ds.Tables[0];
            grdPost1.DataBind();
        }
        else
        {
            grdPost1.DataSource = null;
            grdPost1.DataBind();
        }
    }
    private void set_Gallery(DataTable dt)
    {
        string baseURL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace("\\", "/");
        if (Page.Request.Url.Query.Trim() == "")
        {
            baseURL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace("\\", "/");
        }
        else
        {
            baseURL = Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "").Replace("\\", "/");
        }

        string _inner1 = "";
        string filePath1 = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            filePath1 = dt.Rows[i]["ProjectPkgSitePics_SitePic_Path1"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "<ul class=\"ace-thumbnails clearfix\">";
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt='Field Visit Photo' src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">Field Visit Photo</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";
                _inner1 += "</ul>";
            }
            filePath1 = dt.Rows[i]["ProjectPkgSitePics_SitePic_Path2"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "<ul class=\"ace-thumbnails clearfix\">";
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt='Field Visit Photo' src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">Field Visit Photo</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";
                _inner1 += "</ul>";
            }
            filePath1 = dt.Rows[i]["ProjectPkgSitePics_SitePic_Path3"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "<ul class=\"ace-thumbnails clearfix\">";
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt='Field Visit Photo' src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">Field Visit Photo</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";
                _inner1 += "</ul>";
            }
        }
        divGallery.InnerHtml = _inner1;
    }
    private void get_tbl_ProjectVisit(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectVisit(ProjectWork_Id, "");
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
    private void get_tbl_ProjectVisit_Details(int ProjectVisit_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectVisit_Details(ProjectVisit_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            set_Gallery(ds.Tables[2]);
        }
        else
        {
            divGallery.InnerHtml = "Gallery Image Not Uploaded..";
        }
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectVisit_Id = 0;
        try
        {
            ProjectVisit_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectVisit_Id = 0;
        }
        if (ProjectVisit_Id > 0)
        {
            get_tbl_ProjectVisit_Details(ProjectVisit_Id);
        }
    }
    protected void grdPost1_PreRender(object sender, EventArgs e)
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
    protected void grdPost1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }
}
