using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackageAdditionalDepartmentPayment : System.Web.UI.Page
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
            get_tbl_ADP_Category();
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
    private void get_tbl_ADP_Category()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ADP_Category();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCategory, "ADP_Category_Name", "ADP_Category_Id");
        }
        else
        {
            ddlCategory.Items.Clear();
        }
    }
    private void get_tbl_Deduction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Deduction_Mode(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Deduction"] = ds.Tables[0];
        }
        else
        {
            ViewState["Deduction"] = null;
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

    protected void grdADPItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Package_ADP_Item_Item_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            GridView grdTaxes = e.Row.FindControl("grdTaxes") as GridView;
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_Deduction_WithPackageADPTax(Package_ADP_Item_Item_Id);
            get_tbl_Deduction();
            if (AllClasses.CheckDataSet(ds))
            {
                grdTaxes.DataSource = ds.Tables[0];
                grdTaxes.DataBind();
            }
            else
            {
                grdTaxes.DataSource = null;
                grdTaxes.DataBind();
            }
        }
    }
    protected void grdTaxes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlTaxes = e.Row.FindControl("ddlTaxes") as DropDownList;
            TextBox txtTaxesP = e.Row.FindControl("txtTaxesP") as TextBox;

            if (ViewState["Deduction"] != null)
            {
                DataTable dt = (DataTable)ViewState["Deduction"];
                if (e.Row.RowIndex == 0)
                {
                    AllClasses.FillDropDown(dt, ddlTaxes, "Deduction_Name", "Deduction_Id");
                }
                else
                {
                    AllClasses.FillDropDown_WithOutSelect(dt, ddlTaxes, "Deduction_Name", "Deduction_Id");
                }
            }
            else
            {
                ddlTaxes.Items.Clear();
            }

            try
            {
                ddlTaxes.SelectedValue = e.Row.Cells[1].Text;
            }
            catch
            {

            }
            if (txtTaxesP.Text == "")
            {

                if (e.Row.RowIndex == 0)
                {
                    if (ddlTaxes.SelectedValue == "0")
                    {
                        try
                        {
                            ddlTaxes.SelectedValue = "1013";
                        }
                        catch
                        {
                            ddlTaxes.SelectedValue = "0";
                        }
                    }

                    txtTaxesP.Text = "9";
                }
                if (e.Row.RowIndex == 1)
                {
                    try
                    {
                        ddlTaxes.SelectedValue = "1014";
                    }
                    catch
                    {
                        ddlTaxes.SelectedValue = "0";
                    }

                    txtTaxesP.Text = "9";
                }
            }

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
        Create_DataTable(Convert.ToInt32(hf_Package_Id.Value));
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
        get_tbl_Deduction();
        Create_DataTable(Convert.ToInt32(hf_Package_Id.Value));
    }

    private void Create_DataTable(int Package_Id)
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("Package_ADP_Item_Id", typeof(int));
        DataColumn dc2 = new DataColumn("ADP_Item_Id", typeof(int));
        DataColumn dc3 = new DataColumn("ADP_Item_Category_Id", typeof(int));
        DataColumn dc4 = new DataColumn("ADP_Item_Unit_Id", typeof(int));
        DataColumn dc5 = new DataColumn("ADP_Category_Name", typeof(string));
        DataColumn dc6 = new DataColumn("ADP_Item_Specification", typeof(string));
        DataColumn dc7 = new DataColumn("Unit_Name", typeof(string));
        DataColumn dc8 = new DataColumn("ADP_Item_Rate", typeof(decimal));
        DataColumn dc9 = new DataColumn("ADP_Item_Qty", typeof(decimal));

        dt.Columns.AddRange(new DataColumn[] { dc1, dc2, dc3, dc4, dc5, dc6, dc7, dc8, dc9 });
        ViewState["ADP_Item"] = dt;
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
        decimal Total_Amount = 0;
        tbl_Package_ADP obj_tbl_Package_ADP = new tbl_Package_ADP();
        obj_tbl_Package_ADP.Package_ADP_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_ADP.Package_ADP_Status = 1;
        obj_tbl_Package_ADP.Package_ADP_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_Package_ADP.Package_ADP_RefNo = txtRefNo.Text.Trim();
        obj_tbl_Package_ADP.Package_ADP_Date = txtInvoiceDate.Text.Trim();

        tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
        obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "";
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
        obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 1;
        obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 1;

        List<tbl_Package_ADP_Item> obj_Package_ADP_Item_Li = new List<tbl_Package_ADP_Item>();
        for (int i = 0; i < grdADPItems.Rows.Count; i++)
        {
            TextBox txtRate = grdADPItems.Rows[i].FindControl("txtRate") as TextBox;
            TextBox txtQty = grdADPItems.Rows[i].FindControl("txtQty") as TextBox;
            decimal Sub_Amount = 0;

            tbl_Package_ADP_Item obj_Package_ADP_Item = new tbl_Package_ADP_Item();
            obj_Package_ADP_Item.Package_ADP_Item_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_Package_ADP_Item.Package_ADP_Item_Item_Id = Convert.ToInt32(grdADPItems.Rows[i].Cells[1].Text);
            obj_Package_ADP_Item.Package_ADP_Item_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
            obj_Package_ADP_Item.Package_ADP_Item_Category_Id = Convert.ToInt32(grdADPItems.Rows[i].Cells[2].Text);
            obj_Package_ADP_Item.Package_ADP_Item_Specification = grdADPItems.Rows[i].Cells[6].Text;
            obj_Package_ADP_Item.Package_ADP_Item_Unit_Id = Convert.ToInt32(grdADPItems.Rows[i].Cells[3].Text);
            try
            {
                obj_Package_ADP_Item.Package_ADP_Item_Rate = Convert.ToDecimal(txtRate.Text);
            }
            catch
            {
                obj_Package_ADP_Item.Package_ADP_Item_Rate = 0;
            }
            try
            {
                obj_Package_ADP_Item.Package_ADP_Item_Qty = Convert.ToDecimal(txtQty.Text);
            }
            catch
            {
                obj_Package_ADP_Item.Package_ADP_Item_Qty = 0;
            }
            obj_Package_ADP_Item.Package_ADP_Item_Status = 1;
            Sub_Amount = obj_Package_ADP_Item.Package_ADP_Item_Rate * obj_Package_ADP_Item.Package_ADP_Item_Qty;
            obj_Package_ADP_Item.Package_ADP_Item_Amount = Sub_Amount;

            decimal Item_Tax_Amount = 0;

            List<tbl_Package_ADP_Item_Tax> obj_tbl_Package_ADP_Item_Tax_Li = new List<tbl_Package_ADP_Item_Tax>();
            GridView grdTaxes = grdADPItems.Rows[i].FindControl("grdTaxes") as GridView;
            for (int k = 0; k < grdTaxes.Rows.Count; k++)
            {
                decimal ADP_Item_Tax_Amount = 0;
                tbl_Package_ADP_Item_Tax obj_tbl_Package_ADP_Item_Tax = new tbl_Package_ADP_Item_Tax();
                TextBox txtTaxesP = grdTaxes.Rows[k].FindControl("txtTaxesP") as TextBox;
                DropDownList ddlTaxes = grdTaxes.Rows[k].FindControl("ddlTaxes") as DropDownList;
                obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
                try
                {
                    obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Deduction_Id = Convert.ToInt32(ddlTaxes.SelectedValue);
                }
                catch
                {
                    obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Deduction_Id = 0;
                }
                try
                {
                    obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Value = Convert.ToDecimal(txtTaxesP.Text);
                }
                catch
                {
                    obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Value = 0;
                }
                obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Status = 1;

                if (obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Deduction_Id > 0)
                {
                    obj_tbl_Package_ADP_Item_Tax_Li.Add(obj_tbl_Package_ADP_Item_Tax);
                }
                ADP_Item_Tax_Amount = Sub_Amount * obj_tbl_Package_ADP_Item_Tax.Package_ADP_Item_Tax_Value / 100;
                Item_Tax_Amount += ADP_Item_Tax_Amount;
            }
            obj_Package_ADP_Item.Package_ADP_Item_TotalAmount = Sub_Amount + Item_Tax_Amount;

            if (obj_tbl_Package_ADP_Item_Tax_Li != null)
            {
                obj_Package_ADP_Item.tbl_Package_ADP_Item_Tax = obj_tbl_Package_ADP_Item_Tax_Li;
            }
            obj_Package_ADP_Item_Li.Add(obj_Package_ADP_Item);
            Total_Amount = Total_Amount + Sub_Amount + Item_Tax_Amount;
        }
        obj_tbl_Package_ADP.Package_ADP_ADPTotalAmount = Total_Amount;
        if (obj_Package_ADP_Item_Li == null || obj_Package_ADP_Item_Li.Count == 0)
        {
            MessageBox.Show("Please Select At One One Item.");
            return;
        }
        if (new DataLayer().Insert_tbl_Package_ADP_Item(obj_Package_ADP_Item_Li, obj_tbl_Package_ADP, obj_tbl_PackageADPApproval, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(ddlSearchScheme.SelectedValue)))
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

    protected void grdTaxes_PreRender(object sender, EventArgs e)
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlItems.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Item");
            return;
        }
        else
        {
            DataTable dt = (DataTable)ViewState["ADP_Item"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ADP_Item_Id = Convert.ToInt32(dt.Rows[i]["ADP_Item_Id"].ToString());
                if (ADP_Item_Id == Convert.ToInt32(ddlItems.SelectedValue))
                {
                    MessageBox.Show("This Item has Been Added Already..");
                    return;
                }
            }

            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_ADP_Item(Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlItems.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["Package_ADP_Item_Id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Package_ADP_Item_Id"].ToString());
                dr["ADP_Item_Id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["ADP_Item_Id"].ToString());
                dr["ADP_Item_Category_Id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["ADP_Item_Category_Id"].ToString());
                dr["ADP_Item_Unit_Id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["ADP_Item_Unit_Id"].ToString());
                dr["ADP_Category_Name"] = ds.Tables[0].Rows[0]["ADP_Category_Name"].ToString();
                dr["ADP_Item_Specification"] = ds.Tables[0].Rows[0]["ADP_Item_Specification"].ToString();
                dr["Unit_Name"] = ds.Tables[0].Rows[0]["Unit_Name"].ToString();
                dr["ADP_Item_Rate"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["ADP_Item_Rate"].ToString());
                dr["ADP_Item_Qty"] = Convert.ToDecimal(ds.Tables[0].Rows[0]["ADP_Item_Qty"].ToString());

                dt.Rows.Add(dr);

                grdADPItems.DataSource = dt;
                grdADPItems.DataBind();

                ViewState["ADP_Item"] = dt;
            }
            else
            {

            }
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "0")
        {
            ddlItems.Items.Clear();
        }
        else
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_ADP_Item(Convert.ToInt32(ddlCategory.SelectedValue), 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                AllClasses.FillDropDown(ds.Tables[0], ddlItems, "ADP_Item_Specification", "ADP_Item_Id");
            }
            else
            {
                ddlItems.Items.Clear();
            }
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdditionalDepartmentPaymentItem.aspx");
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        decimal Total_Amount = 0;
        for (int i = 0; i < grdADPItems.Rows.Count; i++)
        {
            TextBox txtRate = grdADPItems.Rows[i].FindControl("txtRate") as TextBox;
            TextBox txtQty = grdADPItems.Rows[i].FindControl("txtQty") as TextBox;
            decimal Sub_Amount = 0;
            decimal ADP_Item_Rate = 0;
            decimal ADP_Item_Qty = 0;
            try
            {
                ADP_Item_Rate = Convert.ToDecimal(txtRate.Text);
            }
            catch
            {
                ADP_Item_Rate = 0;
            }
            try
            {
                ADP_Item_Qty = Convert.ToDecimal(txtQty.Text);
            }
            catch
            {
                ADP_Item_Qty = 0;
            }
            Sub_Amount = ADP_Item_Rate * ADP_Item_Qty;
            decimal Item_Tax_Amount = 0;
            GridView grdTaxes = grdADPItems.Rows[i].FindControl("grdTaxes") as GridView;
            for (int k = 0; k < grdTaxes.Rows.Count; k++)
            {
                decimal ADP_Item_Tax_Value = 0;
                decimal ADP_Item_Tax_Amount = 0;
                TextBox txtTaxesP = grdTaxes.Rows[k].FindControl("txtTaxesP") as TextBox;
                try
                {
                    ADP_Item_Tax_Value = Convert.ToDecimal(txtTaxesP.Text);
                }
                catch
                {
                    ADP_Item_Tax_Value = 0;
                }
                ADP_Item_Tax_Amount = Sub_Amount * ADP_Item_Tax_Value / 100;
                Item_Tax_Amount += ADP_Item_Tax_Amount;
            }
            Total_Amount = Total_Amount + Sub_Amount + Item_Tax_Amount;
        }

        grdADPItems.FooterRow.Cells[10].Text = decimal.Round(Total_Amount, 2, MidpointRounding.AwayFromZero).ToString();
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