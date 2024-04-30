using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectWorkGalleryView : System.Web.UI.Page
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
                load_Gallary_data();
            }
        }
    }

    private void load_Gallary_data()
    {
        divDeleteList.Visible = false;
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        string App = "";
        try
        {
            App = Request.QueryString["App"].Trim();
        }
        catch
        {
            App = "False";
        }
        string Mode = "";
        try
        {
            Mode = Request.QueryString["Mode"].Trim();
        }
        catch
        {
            Mode = "False";
        }
        get_tbl_ProjectWorkGallery_Group(ProjectWork_Id, App, Mode);
        get_tbl_ProjectWorkGallery(ProjectWork_Id, App, Mode, "");
    }

    private void set_Gallery(DataTable dt, string ProjectWork_Name)
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
            filePath1 = dt.Rows[i]["ProjectWorkGallery_Path"].ToString();
            if (filePath1 != "")
            {
                _inner1 += "<ul class=\"ace-thumbnails clearfix\">";
                _inner1 += "    <li>";
                _inner1 += "        <a href = '" + baseURL + filePath1.Replace("&nbsp;", "") + "' data-rel=\"colorbox\" class=\"cboxElement\" onclick=\"Poppuplose()\">";
                _inner1 += "            <img width = \"600\" height=\"500\" alt=" + ProjectWork_Name + " src='" + baseURL + filePath1.Replace("&nbsp;", "") + "'>";
                _inner1 += "            <div class=\"text\">";
                _inner1 += "                <div class=\"inner\">" + ProjectWork_Name + "</div>";
                _inner1 += "            </div>";
                _inner1 += "        </a>";
                _inner1 += "    </li>";
                _inner1 += "</ul>";
            }
        }
        divGallery.InnerHtml = _inner1;
    }
    private void get_tbl_ProjectWorkGallery_Group(int ProjectWork_Id, string App, string Mode)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGallery_Group(ProjectWork_Id, App, Mode);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            rbtDate.DataTextField = "Total_Photos";
            rbtDate.DataValueField = "Date_Uploaded";
            rbtDate.DataSource = ds.Tables[0];
            rbtDate.DataBind();
        }
        else
        {
            rbtDate.DataSource = null;
            rbtDate.DataBind();
        }
    }
    private void get_tbl_ProjectWorkGallery(int ProjectWork_Id, string App, string Mode, string _Date)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGallery(ProjectWork_Id, App, Mode, _Date);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string ProjectWork_Name = "";
            try
            {
                ProjectWork_Name = ds.Tables[1].Rows[0]["ProjectWork_Name"].ToString();
            }
            catch
            {
                ProjectWork_Name = "";
            }
            set_Gallery(ds.Tables[0], ProjectWork_Name);

            grdGalleryList.DataSource = ds.Tables[0];
            grdGalleryList.DataBind();
        }
        else
        {
            //divGallery.InnerHtml = "Gallery Image Not Uploaded..";
        }
    }

    protected void rbtDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _Date = rbtDate.SelectedValue.Trim();
        if (Request.QueryString.Count > 0)
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            string App = "";
            try
            {
                App = Request.QueryString["App"].Trim();
            }
            catch
            {
                App = "False";
            }
            string Mode = "";
            try
            {
                Mode = Request.QueryString["Mode"].Trim();
            }
            catch
            {
                Mode = "False";
            }
            get_tbl_ProjectWorkGallery(ProjectWork_Id, App, Mode, _Date);
        }
    }

    protected void lnkDeletePic_Click(object sender, EventArgs e)
    {
        divDeleteList.Visible = true;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int ProjectWorkGallery_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (new DataLayer().Delete_tbl_ProjectWorkGallery_Single(ProjectWorkGallery_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            load_Gallary_data();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void grdGalleryList_PreRender(object sender, EventArgs e)
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

    protected void grdGalleryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
