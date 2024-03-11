using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class MasterDownloadsOther : System.Web.UI.Page
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
            get_tbl_DownloadsOther();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }

    private void get_tbl_DownloadsOther()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DownloadsOther();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!flUpload.HasFile)
        {
            MessageBox.Show("Please Upload File To Be Downloaded");
            return;
        }
        if (txtDownloadsOther.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Name");
            txtDownloadsOther.Focus();
            return;
        }
        string Msg = "";
        tbl_DownloadsOther obj_tbl_ProductType = new tbl_DownloadsOther();

        obj_tbl_ProductType.DownloadsOther_Name = txtDownloadsOther.Text.Trim();
        obj_tbl_ProductType.DownloadsOther_Ref_No = txtRefNo.Text.Trim();
        obj_tbl_ProductType.DownloadsOther_Date = txtDate.Text.Trim();
        obj_tbl_ProductType.DownloadsOther_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProductType.DownloadsOther_Status = 1;
        obj_tbl_ProductType.DownloadsOther_Bytes = flUpload.FileBytes;
        if (new DataLayer().Insert_tbl_DownloadsOther(obj_tbl_ProductType))
        {
            MessageBox.Show("Downloads Link Created Successfully ! ");
            reset();
            get_tbl_DownloadsOther();
            return;
        }
        else
        {
            MessageBox.Show("Error ! ");
            return;
        }
    }

    private void reset()
    {
        txtDownloadsOther.Text = "";
        txtDate.Text = "";
        txtRefNo.Text = "";
        get_tbl_DownloadsOther();
    }
    
    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
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

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int DownloadsOther_Id = Convert.ToInt32((btnDelete.Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        int AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_tbl_DownloadsOther(AddedBy, DownloadsOther_Id))
        {
            MessageBox.Show("Downloads Link Deleted Successfully ! ");
            reset();
            get_tbl_DownloadsOther();
            return;
        }
        else
        {
            MessageBox.Show("Downloads Is Not Link Deleted");
            return;
        }
    }
}
