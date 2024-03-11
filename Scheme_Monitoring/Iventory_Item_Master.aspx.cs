using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Iventory_Item_Master : System.Web.UI.Page
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
            get_tbl_Inventory_Category();
            get_tbl_Inventory_Company();
            get_tbl_Inventory_Type();
            get_tbl_Class();
            get_tbl_InventoryUnit();

            get_tbl_InventoryItemDetails();
        }
    }
    
    private void get_tbl_Inventory_Category()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_tbl_Inventory_Category());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProductCategory, "Inventory_Category_Name", "Inventory_Category_Id");
        }
        else
        {
            ddlProductCategory.Items.Clear();
        }
    }
    private void get_tbl_Inventory_Type()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_tbl_Inventory_Type());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProductType, "Inventory_Type_Name", "Inventory_Type_Id");
        }
        else
        {
            ddlProductType.Items.Clear();
        }
    }
    private void get_tbl_Inventory_Company()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_tbl_Inventory_Company());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProductCompany, "Inventory_Company_Name", "Inventory_Company_Id");
        }
        else
        {
            ddlProductCompany.Items.Clear();
        }
    }
    private void get_tbl_Class()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_tbl_Class());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlClass, "Class_Name", "Class_Id");
        }
        else
        {
            ddlClass.Items.Clear();
        }
    }
    private void get_tbl_InventoryUnit()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_tbl_InventoryUnit());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlUnit, "InventoryUnit_Name", "InventoryUnit_Id");
        }
        else
        {
            ddlUnit.Items.Clear();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int InventoryItemDetails_Id = 0;
        try
        {
            InventoryItemDetails_Id = Convert.ToInt32(hf_InventoryItemDetails_Id.Value);
        }
        catch
        {
            InventoryItemDetails_Id = 0;
        }
        if (InventoryItemDetails_Id > 0)
        {
            if (new DataLayer().Delete_InventoryItemDetails(InventoryItemDetails_Id, Person_Id))
            {
                MessageBox.Show("Deleted Successfully");
                reset();
                return;
            }
            else
            {
                MessageBox.Show("Error In Deletetion");
                return;
            }
        }
        else
        {
            MessageBox.Show("Please Select A Inventory Item TO Delete");
            return;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }
    private void reset()
    {
        hf_InventoryItemDetails_Id.Value = "0";
        txtItemName.Text = "";
        ddlClass.SelectedValue = "0";
        ddlProductCategory.SelectedValue = "0";
        ddlProductType.SelectedValue = "0";
        ddlProductCompany.SelectedValue = "0";
        
        get_tbl_InventoryItemDetails();
    }
    private void get_tbl_InventoryItemDetails()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_InventoryItemDetails(0, 0, 0);
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton chk = sender as ImageButton;
        GridViewRow gr = (chk.Parent.Parent as GridViewRow);
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        hf_InventoryItemDetails_Id.Value = gr.Cells[0].Text.Trim();
        try
        {
            ddlClass.SelectedValue = gr.Cells[5].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        try
        {
            ddlUnit.SelectedValue = gr.Cells[1].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        try
        {
            ddlProductCategory.SelectedValue = gr.Cells[2].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        try
        {
            ddlProductType.SelectedValue = gr.Cells[3].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        try
        {
            ddlProductCompany.SelectedValue = gr.Cells[4].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {

        }
        txtItemName.Text = gr.Cells[9].Text.Replace("&nbsp;", "").Trim();
        txtDia.Text = gr.Cells[10].Text.Replace("&nbsp;", "").Trim();
        txtGuage.Text = gr.Cells[11].Text.Replace("&nbsp;", "").Trim();
        txtLength.Text = gr.Cells[12].Text.Replace("&nbsp;", "").Trim();
        txtBredth.Text = gr.Cells[13].Text.Replace("&nbsp;", "").Trim();
        txtHeight.Text = gr.Cells[14].Text.Replace("&nbsp;", "").Trim();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        tbl_InventoryItemDetails obj_tbl_InventoryItemDetails = new tbl_InventoryItemDetails();
        try
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_Id = Convert.ToInt32(hf_InventoryItemDetails_Id.Value);
        }
        catch
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_Id = 0;
        }
        if (txtItemName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Item Name");
            txtItemName.Focus();
            return;
        }
        obj_tbl_InventoryItemDetails.InventoryItemDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_InventoryItemDetails.InventoryItemDetails_ItemName = txtItemName.Text.Trim();
        obj_tbl_InventoryItemDetails.InventoryItemDetails_DIA = txtDia.Text.Trim();
        obj_tbl_InventoryItemDetails.InventoryItemDetails_Guage = txtGuage.Text.Trim();
        obj_tbl_InventoryItemDetails.InventoryItemDetails_Length = txtLength.Text.Trim();
        obj_tbl_InventoryItemDetails.InventoryItemDetails_Bredth = txtBredth.Text.Trim();
        obj_tbl_InventoryItemDetails.InventoryItemDetails_Height = txtHeight.Text.Trim();
        try
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_ClassId = Convert.ToInt32(ddlClass.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_ClassId = 0;
        }
        try
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_CategoryId = Convert.ToInt32(ddlProductCategory.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_CategoryId = 0;
        }
        try
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_TypeId = Convert.ToInt32(ddlProductType.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_TypeId = 0;
        }
        try
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_CompanyId = Convert.ToInt32(ddlProductCompany.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_CompanyId = 0;
        }
        obj_tbl_InventoryItemDetails.InventoryItemDetails_HSNCodeId = 0;
        try
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_UnitId = Convert.ToInt32(ddlUnit.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemDetails.InventoryItemDetails_UnitId = 0;
        }
        obj_tbl_InventoryItemDetails.InventoryItemDetails_Status = 1;
        if ((new DataLayer()).Insert_tbl_InventoryItemDetails(obj_tbl_InventoryItemDetails))
        {
            MessageBox.Show("Inventory Item Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Inventory Item!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
}