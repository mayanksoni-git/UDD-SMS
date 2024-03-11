using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterNodalDeptScheme : System.Web.UI.Page
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
            get_tbl_FundingPattern();
            get_NodalDepartment();
            get_tbl_NodalDeptScheme();
        }
    }

    private void get_NodalDepartment()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee("12", 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlNodalDepartment, "Person_Name", "Person_Id");
        }
        else
        {
            ddlNodalDepartment.Items.Clear();
        }
    }

    private void get_tbl_FundingPattern()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FundingPattern();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlFundingPatern, "FundingPattern_Name", "FundingPattern_Id");
        }
        else
        {
            ddlFundingPatern.Items.Clear();
        }
    }

    private void get_tbl_NodalDeptScheme()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_NodalDeptScheme(0, 0);
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
        tbl_NodalDeptScheme obj_tbl_NodalDeptScheme = new tbl_NodalDeptScheme();
        try
        {
            obj_tbl_NodalDeptScheme.NodalDeptScheme_Id = Convert.ToInt32(hf_NodalDeptScheme_Id.Value);
        }
        catch
        {
            obj_tbl_NodalDeptScheme.NodalDeptScheme_Id = 0;
        }
        if (ddlNodalDepartment.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Nodal Department");
            ddlNodalDepartment.Focus();
            return;
        }
        if (txtNodalDeptSchemeName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Scheme Name");
            txtNodalDeptSchemeName.Focus();
            return;
        }
        obj_tbl_NodalDeptScheme.NodalDeptScheme_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_NodalDeptScheme.NodalDeptScheme_Id = Convert.ToInt32(hf_NodalDeptScheme_Id.Value);
        }
        catch
        {
            obj_tbl_NodalDeptScheme.NodalDeptScheme_Id = 0;
        }
        try
        {
            obj_tbl_NodalDeptScheme.NodalDeptScheme_FundingPattern_Id = Convert.ToInt32(ddlFundingPatern.SelectedValue);
        }
        catch
        {
            obj_tbl_NodalDeptScheme.NodalDeptScheme_FundingPattern_Id = 0;
        }
        obj_tbl_NodalDeptScheme.NodalDeptScheme_Name = txtNodalDeptSchemeName.Text.Trim();
        obj_tbl_NodalDeptScheme.NodalDeptScheme_NodalDept_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        obj_tbl_NodalDeptScheme.NodalDeptScheme_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_NodalDeptScheme.NodalDeptScheme_Status = 1;
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_NodalDeptScheme(obj_tbl_NodalDeptScheme, obj_tbl_NodalDeptScheme.NodalDeptScheme_Id, ref Msg))
        {
            MessageBox.Show("Scheme Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Scheme!");
            return;
        }
    }

    private void reset()
    {
        hf_NodalDeptScheme_Id.Value = "0";
        txtNodalDeptSchemeName.Text = "";
        ddlNodalDepartment.SelectedValue = "0";
        ddlFundingPatern.SelectedValue = "0";
        get_tbl_NodalDeptScheme();
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
        hf_NodalDeptScheme_Id.Value = (chk.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
        GridViewRow grd = chk.Parent.Parent as GridViewRow;
        try
        {
            ddlNodalDepartment.SelectedValue = grd.Cells[1].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        try
        {
            ddlFundingPatern.SelectedValue = grd.Cells[2].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        txtNodalDeptSchemeName.Text = grd.Cells[7].Text.Replace("&nbsp;", "").Trim();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int NodalDeptScheme_Id = Convert.ToInt32(hf_NodalDeptScheme_Id.Value);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());

        if (new DataLayer().Delete_tbl_NodalDeptScheme(NodalDeptScheme_Id, Person_Id))
        {
            reset();
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
}