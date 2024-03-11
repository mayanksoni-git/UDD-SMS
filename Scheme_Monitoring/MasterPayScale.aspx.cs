using System;
using System.Collections;
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

public partial class MasterPayScale : System.Web.UI.Page
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
            get_tbl_PayBand();
            get_tbl_PayScale();
        }
    }

    private void get_tbl_PayBand()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayBand();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlPayBandMaster, "PayBand_Name", "PayBand_Id");
        }
        else
        {
            ddlPayBandMaster.Items.Clear();
        }
    }

    private void get_tbl_PayScale()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayScale(0);
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
        tbl_PayScale obj_tbl_PayScale = new tbl_PayScale();
        try
        {
            obj_tbl_PayScale.PayScale_Id = Convert.ToInt32(hf_PayScale_Id.Value);
        }
        catch
        {
            obj_tbl_PayScale.PayScale_Id = 0;
        }
        if (ddlPayBandMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Pay Band");
            ddlPayBandMaster.Focus();
            return;
        }
        if (txtPayScaleName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Pay Scale Name");
            txtPayScaleName.Focus();
            return;
        }
        obj_tbl_PayScale.PayScale_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_PayScale.PayScale_Id = Convert.ToInt32(hf_PayScale_Id.Value);
        }
        catch
        {
            obj_tbl_PayScale.PayScale_Id = 0;
        }
        obj_tbl_PayScale.PayScale_Name = txtPayScaleName.Text.Trim();
        obj_tbl_PayScale.PayScale_PayBandId= Convert.ToInt32(ddlPayBandMaster.SelectedValue);
        obj_tbl_PayScale.PayScale_ValueFrom = Convert.ToInt32(txtValueFrom.Text);
        obj_tbl_PayScale.PayScale_ValueTill = Convert.ToInt32(txtValueTill.Text);
        obj_tbl_PayScale.PayScale_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PayScale.PayScale_Status = 1;
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_PayScale(obj_tbl_PayScale, obj_tbl_PayScale.PayScale_Id, ref Msg))
        {
            MessageBox.Show("Pay Scale Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Pay Scale!");
            return;
        }
    }

    private void reset()
    {
        hf_PayScale_Id.Value = "0";
        txtPayScaleName.Text = "";
        txtValueFrom.Text = "";
        txtValueTill.Text = "";
        ddlPayBandMaster.SelectedValue = "0";
        get_tbl_PayScale();
        divCreateNew.Visible = false;
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        ImageButton chk = sender as ImageButton;
        divCreateNew.Visible = true;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
         (chk.Parent.Parent as GridViewRow).BackColor = Color.LightGreen;
        hf_PayScale_Id.Value = (chk.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
        GridViewRow grd = chk.Parent.Parent as GridViewRow;
        ddlPayBandMaster.SelectedValue = grd.Cells[1].Text.Replace("&nbsp;", "").Trim();
        txtPayScaleName.Text = grd.Cells[4].Text.Replace("&nbsp;", "").Trim();
        txtValueFrom.Text = grd.Cells[5].Text.Replace("&nbsp;", "").Trim();
        txtValueTill.Text = grd.Cells[6].Text.Replace("&nbsp;", "").Trim();
        divCreateNew.Focus();
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
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int PayScale_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (new DataLayer().Delete_PayScale(PayScale_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully !!");
            get_tbl_PayScale();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion !!");
            return;
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        reset();
        divCreateNew.Visible = true;
    }
}