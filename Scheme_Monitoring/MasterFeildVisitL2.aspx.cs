using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFeildVisitL2 : System.Web.UI.Page
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
            get_tbl_FeildVisitL1();
            get_tbl_FeildVisitL2();
        }
    }

    private void get_tbl_FeildVisitL1()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FeildVisitL1();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlFeildVisitL1, "FeildVisitL1_Name", "FeildVisitL1_Id");
        }
        else
        {
            ddlFeildVisitL1.Items.Clear();
        }
    }

    private void get_tbl_FeildVisitL2()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FeildVisitL2(0);
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
        tbl_FeildVisitL2 obj_tbl_FeildVisitL2 = new tbl_FeildVisitL2();
        try
        {
            obj_tbl_FeildVisitL2.FeildVisitL2_Id = Convert.ToInt32(hf_FeildVisitL2_Id.Value);
        }
        catch
        {
            obj_tbl_FeildVisitL2.FeildVisitL2_Id = 0;
        }
        if (ddlFeildVisitL1.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Option");
            ddlFeildVisitL1.Focus();
            return;
        }
        if (txtFeildVisitL2Name.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Name");
            txtFeildVisitL2Name.Focus();
            return;
        }
        obj_tbl_FeildVisitL2.FeildVisitL2_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_FeildVisitL2.FeildVisitL2_Id = Convert.ToInt32(hf_FeildVisitL2_Id.Value);
        }
        catch
        {
            obj_tbl_FeildVisitL2.FeildVisitL2_Id = 0;
        }
        obj_tbl_FeildVisitL2.FeildVisitL2_Name= txtFeildVisitL2Name.Text.Trim();
        obj_tbl_FeildVisitL2.FeildVisitL2_L1Id = Convert.ToInt32(ddlFeildVisitL1.SelectedValue);        
        obj_tbl_FeildVisitL2.FeildVisitL2_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_FeildVisitL2.FeildVisitL2_Status = 1;
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_FeildVisitL2(obj_tbl_FeildVisitL2, obj_tbl_FeildVisitL2.FeildVisitL2_Id, ref Msg))
        {
            MessageBox.Show("Field Visit L2 Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Field Visit L2!");
            return;
        }
    }

    private void reset()
    {
        hf_FeildVisitL2_Id.Value = "0";
        txtFeildVisitL2Name.Text = "";
        ddlFeildVisitL1.SelectedValue = "0";
        get_tbl_FeildVisitL2();
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
        hf_FeildVisitL2_Id.Value = (chk.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
        GridViewRow grd = chk.Parent.Parent as GridViewRow;
        ddlFeildVisitL1.SelectedValue = grd.Cells[1].Text.Replace("&nbsp;", "").Trim();
        txtFeildVisitL2Name.Text = grd.Cells[5].Text.Replace("&nbsp;", "").Trim();
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
}