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

public partial class Iventory_Item_Stock_Master : System.Web.UI.Page
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
            get_tbl_Zone();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int InventoryItemStockDetails_Id = 0;
        try
        {
            InventoryItemStockDetails_Id = Convert.ToInt32(hf_InventoryItemStockDetails_Id.Value);
        }
        catch
        {
            InventoryItemStockDetails_Id = 0;
        }
        if (InventoryItemStockDetails_Id > 0)
        {
            if (new DataLayer().Delete_InventoryItemStockDetails(InventoryItemStockDetails_Id, Person_Id))
            {
                MessageBox.Show("Deleted Successfully");
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

    private void get_tbl_InventoryItemStockDetails(int Zone_Id, int Circle_Id, int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_InventoryItemStockDetails(0, 0, 0, Zone_Id, Circle_Id, Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divEntry.Visible = true;
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            divEntry.Visible = false;
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
        hf_InventoryItemStockDetails_Id.Value = gr.Cells[0].Text.Trim();
        hf_InventoryItemDetails_Id.Value = gr.Cells[1].Text.Trim();
        txtQuantity.Text = gr.Cells[24].Text.Replace("&nbsp;", "").Trim();
        txtDepriciationRate.Text = gr.Cells[25].Text.Replace("&nbsp;", "").Trim();
        txtItemRate.Text = gr.Cells[23].Text.Replace("&nbsp;", "").Trim();
        divStockEntry.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Insert_Item_Stock_Details(0);
    }

    private void Insert_Item_Stock_Details(int InventoryItemStockDetails_Id)
    {
        tbl_InventoryItemStockDetails obj_tbl_InventoryItemStockDetails = new tbl_InventoryItemStockDetails();
        obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Id = InventoryItemStockDetails_Id;
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_ItemId = Convert.ToInt32(hf_InventoryItemDetails_Id.Value);
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_ItemId = 0;
        }
        if (txtItemRate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Item Rate");
            txtItemRate.Focus();
            return;
        }
        if (txtQuantity.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Item Available Quantity In Stock");
            txtQuantity.Focus();
            return;
        }
        obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_ZoneId = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_ZoneId = 0;
        }
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_CircleId = 0;
        }
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_DivisionId = 0;
        }
        if (obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_DivisionId + obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_CircleId + obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_ZoneId == 0)
        {
            MessageBox.Show("Please Select Zone / Circle or Division");
            ddlZone.Focus();
            return;
        }
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Rate = Convert.ToDecimal(txtItemRate.Text.Trim());
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Rate = 0;
        }
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Quantity = 0;
        }
        try
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Depriciation = Convert.ToDecimal(txtDepriciationRate.Text.Trim());
        }
        catch
        {
            obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Depriciation = 0;
        }

        obj_tbl_InventoryItemStockDetails.InventoryItemStockDetails_Status = 1;
        if ((new DataLayer()).Insert_tbl_InventoryItemStockDetails(obj_tbl_InventoryItemStockDetails))
        {
            MessageBox.Show("Inventory Item Stock Created Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Inventory Item Stock!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[13].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[14].Text = Session["Default_Circle"].ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
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
        get_tbl_InventoryItemStockDetails(Zone_Id, Circle_Id, Division_Id);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int InventoryItemStockDetails_Id = 0;
        try
        {
            InventoryItemStockDetails_Id = Convert.ToInt32(hf_InventoryItemStockDetails_Id.Value);
        }
        catch
        {
            InventoryItemStockDetails_Id = 0;
        }
        Insert_Item_Stock_Details(InventoryItemStockDetails_Id);
    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdAddNewStock_PreRender(object sender, EventArgs e)
    {

    }

    protected void grdAddNewStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}