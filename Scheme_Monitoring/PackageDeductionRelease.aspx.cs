using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackageDeductionRelease : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            txtInvoiceDate.Text = Session["ServerDate"].ToString();
            get_tbl_Project();
            get_tbl_Zone();
            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlSearchScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlSearchScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    // ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
                    {//ULB
                        try
                        {
                            //ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            //ddlULB.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
        }
    }
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }

    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
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
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchScheme, "Project_Name", "Project_Id");
            try
            {
                ddlSearchScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlSearchScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    private void reset()
    {
        divEntry.Visible = false;
        hf_Package_Id.Value = "0";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divEntry.Visible = true;
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        hf_Package_Id.Value = gr.Cells[0].Text.Trim();
        gr.BackColor = Color.LightGreen;
        get_tbl_DeductionRelease_Item();
    }

    private void get_tbl_DeductionRelease_Item()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DeductionRelease_Item(Convert.ToInt32(hf_Package_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDeductions.DataSource = ds.Tables[0];
            grdDeductions.DataBind();
        }
        else
        {
            grdDeductions.DataSource = null;
            grdDeductions.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Zone");
            return;
        }
        hf_Package_Id.Value = "";
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(0, Project_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            divEntry.Visible = false;
        }
        else
        {
            divData.Visible = false;
            divEntry.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        tbl_Package_DeductionRelease obj_tbl_Package_DeductionRelease = new tbl_Package_DeductionRelease();
        obj_tbl_Package_DeductionRelease.Package_DeductionRelease_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_DeductionRelease.Package_DeductionRelease_Status = 1;
        obj_tbl_Package_DeductionRelease.Package_DeductionRelease_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_Package_DeductionRelease.Package_DeductionRelease_RefNo = txtRefNo.Text.Trim();
        obj_tbl_Package_DeductionRelease.Package_DeductionRelease_Date = txtInvoiceDate.Text.Trim();

        tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "";
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 1;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 1;

        List<tbl_Package_DeductionRelease_Item> obj_Package_DeductionRelease_Item_Li = new List<tbl_Package_DeductionRelease_Item>();
        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            CheckBox chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            TextBox txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
            TextBox txtDeductionRelease = grdDeductions.Rows[i].FindControl("txtDeductionRelease") as TextBox;
            if (chkSelect.Checked == true)
            {
                tbl_Package_DeductionRelease_Item obj_tbl_Package_DeductionRelease_Item = new tbl_Package_DeductionRelease_Item();
                obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text);
                obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
                try
                {
                    obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_DeductionValue = Convert.ToDecimal(txtDeductionValue.Text);
                }
                catch
                {
                    obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_DeductionValue = 0;
                }
                try
                {
                    obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_DeductionReleaseAmount = Convert.ToDecimal(txtDeductionRelease.Text);
                }
                catch
                {
                    obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_DeductionReleaseAmount = 0;
                }
                obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_Status = 1;
                if (obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_DeductionReleaseAmount > obj_tbl_Package_DeductionRelease_Item.Package_DeductionRelease_Item_DeductionValue)
                {
                    MessageBox.Show("Deduction Release Always Less Then Or Equal To Deduction Value.");
                    return;
                }
                obj_Package_DeductionRelease_Item_Li.Add(obj_tbl_Package_DeductionRelease_Item);
            }
        }

        if (obj_Package_DeductionRelease_Item_Li == null || obj_Package_DeductionRelease_Item_Li.Count == 0)
        {
            MessageBox.Show("Please Select At One One Item.");
            return;
        }
        if (new DataLayer().Insert_tbl_Package_DeductionRelease(obj_Package_DeductionRelease_Item_Li, obj_tbl_Package_DeductionRelease, obj_tbl_Package_DeductionReleaseApproval, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(ddlSearchScheme.SelectedValue)))
        {
            MessageBox.Show("Items Saved Successfully");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In  Package Items.");
            return;
        }
    }

    protected void txtDeductionRelease_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;
        TextBox txtDeductionValue = gr.FindControl("txtDeductionValue") as TextBox;
        TextBox txtDeductionRelease = gr.FindControl("txtDeductionRelease") as TextBox;
        if (Convert.ToDecimal(txtDeductionRelease.Text) > Convert.ToDecimal(txtDeductionValue.Text))
        {
            txtDeductionRelease.Text = "0";
            MessageBox.Show("Deduction Release Always Less Then Or Equal To Deduction Value");
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
    }
}