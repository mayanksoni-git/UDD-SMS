using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PreviousGenerateInvoiceADP : System.Web.UI.Page
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

            txtMBDate.Text = Session["ServerDate"].ToString();
            get_tbl_Unit();
            get_tbl_ADP_Category();
            get_tbl_Project();
            get_tbl_Zone();
            get_M_Jurisdiction();
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
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
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

    private void get_tbl_Unit()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Unit();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Unit"] = ds.Tables[0];
        }
        else
        {

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
        hf_ProjectWorkPkg_Id.Value = "0";
        txtAmount.Text = "0";
        txtRABillNo.Text = "";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divEntry.Visible = true;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        gr.BackColor = Color.LightGreen;
        string end_Date = gr.Cells[20].Text.Trim();

        DateTime dtEndDate = DateTime.ParseExact(end_Date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        hf_ProjectWorkPkg_Id.Value = gr.Cells[0].Text.Trim();
        hf_ProjectWork_Id.Value = gr.Cells[1].Text.Trim();
        txtAmount.Text = "0";
        txtRABillNo.Text = "";
        get_PreviousInvoiceDetailsADP(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value));
    }

    private void get_PreviousInvoiceDetailsADP(int Package_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PreviousInvoiceDetailsADP(Package_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
            divPreInvoiceDetails.Visible = true;
        }
        else
        {
            divPreInvoiceDetails.Visible = false;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
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
        //if (ddlZone.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Zone");
        //    return;
        //}
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(0, Project_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", false, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            reset_DE();
        }
        else
        {
            divData.Visible = false;
            divEntry.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Package Details Found");
        }
    }

    private void reset_DE()
    {
        divPreInvoiceDetails.Visible = false;
        grdInvoice.DataSource = null;
        grdInvoice.DataBind();
                
        divData.Visible = true;
        divEntry.Visible = false;
        txtAmount.Text = "";
        txtAmpuntPaid.Text = "";
        txtGST.Text = "";
        txtMBDate.Text = "";
        txtRABillNo.Text = "";
        txtTotalAmount.Text = "";
    }

    protected void btnSaveEMB_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Other Department Categery");
            ddlCategory.Focus();
            return;
        }
        if (txtRABillNo.Text == "")
        {
            MessageBox.Show("Please Provide Bill / Refrence No");
            txtRABillNo.Focus();
            return;
        }
        if (txtMBDate.Text == "")
        {
            MessageBox.Show("Please Provide Bill / Refrence Date");
            txtRABillNo.Focus();
            return;
        }
        decimal Allocated_Budget = 0;
        try
        {
            Allocated_Budget = Convert.ToDecimal(txtAmount.Text);
        }
        catch
        {
            Allocated_Budget = 0;
        }

        decimal GST = 0;
        try
        {
            GST = Convert.ToDecimal(txtGST.Text);
        }
        catch
        {
            GST = 0;
        }

        decimal Amount = 0;
        try
        {
            Amount = Convert.ToDecimal(txtTotalAmount.Text);
        }
        catch
        {
            Amount = 0;
        }

        tbl_Package_ADP obj_tbl_Package_ADP = new tbl_Package_ADP();
        obj_tbl_Package_ADP.Package_ADP_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_ADP.Package_ADP_Status = 1;
        obj_tbl_Package_ADP.Package_ADP_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        obj_tbl_Package_ADP.Package_ADP_Category_Id = Convert.ToInt32(ddlCategory.SelectedValue);
        obj_tbl_Package_ADP.Package_ADP_RefNo = txtRABillNo.Text.Trim();
        obj_tbl_Package_ADP.Package_ADP_Date = txtMBDate.Text.Trim();        
        obj_tbl_Package_ADP.Package_ADP_ADPTotalAmount = Amount;

        tbl_FinancialTrans obj_tbl_FinancialTrans = new tbl_FinancialTrans();
        obj_tbl_FinancialTrans.FinancialTrans_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_FinancialTrans.FinancialTrans_Status = 1;
        obj_tbl_FinancialTrans.FinancialTrans_FinancialYear_Id = 0;
        obj_tbl_FinancialTrans.FinancialTrans_Date = txtMBDate.Text;
        obj_tbl_FinancialTrans.FinancialTrans_GO_Number = txtRABillNo.Text;
        obj_tbl_FinancialTrans.FinancialTrans_GO_Date = txtMBDate.Text;
        obj_tbl_FinancialTrans.FinancialTrans_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        obj_tbl_FinancialTrans.FinancialTrans_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_FinancialTrans.FinancialTrans_FilePath1 = "";
        obj_tbl_FinancialTrans.FinancialTrans_TransType = "C";
        obj_tbl_FinancialTrans.FinancialTrans_EntryType = "ADP";
        obj_tbl_FinancialTrans.FinancialTrans_TransAmount = Allocated_Budget;
        obj_tbl_FinancialTrans.FinancialTrans_Amount = Amount;
        obj_tbl_FinancialTrans.FinancialTrans_GST = GST;
        obj_tbl_FinancialTrans.FinancialTrans_Comments = "";

        if (new DataLayer().Insert_tbl_Package_ADP_PreviousDetails(obj_tbl_Package_ADP, obj_tbl_FinancialTrans))
        {
            MessageBox.Show("Other Department Payment Details Saved Successfully");
            reset_DE();
            btnSearch_Click(null, null);
            get_PreviousInvoiceDetailsADP(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value));
            return;
        }
        else
        {
            MessageBox.Show("Unable To Update Details");
            return;
        }
    }        

    protected void btnDeleteInvoice_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_Invoicing_ADP(Invoice_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            get_PreviousInvoiceDetailsADP(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value));
            return;
        }
        else
        {
            MessageBox.Show("Not Deleted.!!");
            return;
        }
    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        getTotal();
    }

    protected void txtGST_TextChanged(object sender, EventArgs e)
    {
        getTotal();
    }
    protected void rbtMappingWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtMappingWith.SelectedValue == "D")
        {
            divZone.Visible = true;
            divCircle.Visible = true;
            divDivision.Visible = true;

            divDistrict.Visible = false;
            divULB.Visible = false;
        }
        else
        {
            divZone.Visible = false;
            divCircle.Visible = false;
            divDivision.Visible = false;

            divDistrict.Visible = true;
            divULB.Visible = true;
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
        }
        else
        {
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue));
        }
    }

    private void get_tbl_ULB(int District_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ULB(District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlULB, "ULB_Name", "ULB_Id");
        }
        else
        {
            ddlULB.Items.Clear();
        }
    }
    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }
    private void getTotal()
    {
        decimal amount = 0;
        decimal gst_amount = 0;
        try
        {
            amount = decimal.Parse(txtAmount.Text.Trim());
        }
        catch
        {
            amount = 0;
        }
        try
        {
            gst_amount = decimal.Parse(txtGST.Text.Trim());
        }
        catch
        {
            gst_amount = 0;
        }
        txtTotalAmount.Text = (amount + gst_amount).ToString();
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