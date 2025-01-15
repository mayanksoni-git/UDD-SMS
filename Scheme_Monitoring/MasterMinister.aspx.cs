using Obout.Ajax.UI.HTMLEditor.Popups;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterMinister : System.Web.UI.Page
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
            get_tbl_Minister();
            get_Parties();
        }
        //get_Parties();
    }

    private void get_tbl_Minister()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Minister();
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

    private void get_Parties()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Parties();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlParties, "PartyName", "PartyID");
        }
        else
        {
            ddlParties.Items.Clear();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        tbl_Minister obj_tbl_Minister = new tbl_Minister();
        try
        {
            obj_tbl_Minister.UPMinisterID = Convert.ToInt32(hf_MinisterId.Value);
        }
        catch
        {
            obj_tbl_Minister.UPMinisterID = 0;
        }
      
        if (ddlParties.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Party");
            ddlParties.Focus();
            return;
        }

        if (ddlMinisterType.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Minister Type");
            ddlMinisterType.Focus();
            return;
        }

        if (TxtMinistry.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Ministry Name");
            TxtMinistry.Focus();
            return;
        }
        if (txtName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Name");
            txtName.Focus();
            return;
        }
        if (txtFromDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Select the Date ");
            txtName.Focus();
            return;
        }

        obj_tbl_Minister.AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Minister.Name = txtName.Text.ToString();
        obj_tbl_Minister.Ministry = TxtMinistry.Text.ToString();
        obj_tbl_Minister.MinisterType = Convert.ToInt16(ddlMinisterType.SelectedValue);
        
        obj_tbl_Minister.Remark = txtRemark.Text.ToString();
        obj_tbl_Minister.party = Convert.ToInt16(ddlParties.SelectedValue);
        obj_tbl_Minister.IsActive = Convert.ToInt16(chkIsActiveMinister.Checked);
        obj_tbl_Minister.FromDate = Convert.ToDateTime(txtFromDate.Text);
        if(!string.IsNullOrEmpty(txtToDate.Text))
        {
            obj_tbl_Minister.ToDate  = Convert.ToDateTime(txtToDate.Text);
        }

        string Msg = "";
        if ((new DataLayer()).Insert_tbl_Minister(obj_tbl_Minister, obj_tbl_Minister.UPMinisterID, ref Msg))
        {
            MessageBox.Show("Minister Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Minister ");
            return;
        }
    }

    private void reset()
    {
        TxtMinistry.Text = "";
        hf_MinisterId.Value = "0";
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
        
        GridViewRow grd = chk.Parent.Parent as GridViewRow;
        hf_MinisterId.Value = grd.Cells[0].Text.Replace("&nbsp;", "").Trim();
       // ddlParties.SelectedValue = grd.Cells[1].Text.Replace("&nbsp;", "").Trim();
        if (ddlParties.Items.FindByValue(grd.Cells[0].Text.Replace("&nbsp;", "").Trim()) != null)
        {
            ddlParties.SelectedValue = grd.Cells[1].Text.Replace("&nbsp;", "").Trim(); ;
        }
        else
        {
            ddlParties.SelectedIndex = 0; 
        }

        //ddlMinisterType.SelectedValue = grd.Cells[2].Text.Replace("&nbsp;", "").Trim();
        if (ddlMinisterType.Items.FindByValue(grd.Cells[0].Text.Replace("&nbsp;", "").Trim()) != null)
        {
            ddlMinisterType.SelectedValue = grd.Cells[1].Text.Replace("&nbsp;", "").Trim(); ;
        }
        else
        {
            ddlParties.SelectedIndex = 0;
        }


        txtName.Text = grd.Cells[5].Text.Replace("&nbsp;", "").Trim();
        TxtMinistry.Text = grd.Cells[7].Text.Replace("&nbsp;", "").Trim();
        txtFromDate.Text = DateTime.Parse(grd.Cells[8].Text.Replace("&nbsp;", "").Trim()).ToString("yyyy-MM-dd");
       // txtFromDate.Text = grd.Cells[8].Text.Replace("&nbsp;", "").Trim();
        txtToDate.Text = DateTime.Parse(grd.Cells[9].Text.Replace("&nbsp;", "").Trim()).ToString("yyyy-MM-dd");
        //txtToDate.Text = grd.Cells[9].Text.Replace("&nbsp;", "").Trim();
        txtRemark.Text = grd.Cells[10].Text.Replace("&nbsp;", "").Trim();
        chkIsActiveMinister.Checked = Convert.ToBoolean(grd.Cells[11].Text.ToString());

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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        reset();
        divCreateNew.Visible = true;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Minister_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_Division(Minister_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
           //e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
           //e.Row.Cells[5].Text = Session["Default_Division"].ToString();
        }
    }
}