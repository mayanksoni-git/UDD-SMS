using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackageAdditionalDepartmentPaymentApproval : System.Web.UI.Page
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

            btnRecommend.Visible = false;
            btnRevert.Visible = false;
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

            if (Request.QueryString.Count > 0)
            {
                int _Package_ADP_Id = 0;
                int Scheme_Id = 0;
                try
                {
                    _Package_ADP_Id = Convert.ToInt32(Request.QueryString["Invoice_Id"].ToString().Trim());
                }
                catch
                {
                    _Package_ADP_Id = 0;
                }
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                    ddlScheme.SelectedValue = Scheme_Id.ToString();
                    ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
                }
                catch
                {
                    Scheme_Id = 0;
                }
                if (_Package_ADP_Id > 0)
                {
                    btnSearch_Click(btnSearch, e);
                    for (int i = 0; i < grdPost.Rows.Count; i++)
                    {
                        ImageButton btnEdit = grdPost.Rows[i].FindControl("btnEdit") as ImageButton;
                        int Package_ADP_Id = 0;
                        try
                        {
                            Package_ADP_Id = Convert.ToInt32(grdPost.Rows[i].Cells[3].Text.Trim());
                        }
                        catch
                        {
                            Package_ADP_Id = 0;
                        }
                        if (_Package_ADP_Id == Package_ADP_Id)
                        {
                            btnEdit_Click(btnEdit, new ImageClickEventArgs(0, 0));
                            break;
                        }
                    }
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnApprove.ID;
        up.Triggers.Add(trg1);
    }
    protected void get_tbl_ProjectWork_Assign_SNA_Limit(int ProjectWork_Id)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = hf_Scheme_Id.Value; ;

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, ProjectWork_Id, "", 0, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divBalance.InnerHtml = ds.Tables[0].Compute("sum(SNAAccountAvailableLimitRs)", "").ToString();
            divAccountNo.InnerHtml = "<b>Account No: " + ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString() + "</b>";
        }
        else
        {
            divBalance.InnerHtml = "0";
            divAccountNo.InnerHtml = "";
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
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
                ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
            }
            catch
            {

            }
        }
        else
        {
            ddlScheme.Items.Clear();
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
        hf_ProjectWork_Id.Value = "0";
        hf_Loop.Value = "0";
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
        hf_Package_ADP_Id.Value = gr.Cells[3].Text.Trim();
        hf_Scheme_Id.Value = gr.Cells[2].Text.Trim();
        int Work_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        hf_ProjectWork_Id.Value = Work_Id.ToString();
        hf_Loop.Value = gr.Cells[4].Text.Trim();
        int Scheme_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        txtInvoiceDate.Text = gr.Cells[8].Text.Trim();
        txtRefNo.Text = gr.Cells[7].Text.Trim();
        gr.BackColor = Color.LightGreen;
        get_ProcessConfig_Current(Scheme_Id, hf_Loop.Value);
        get_tbl_Package_ADP_Item();
        if (Scheme_Id == 1013 || Scheme_Id == 1016)
        {
            get_tbl_ProjectWork_Assign_SNA_Limit(Work_Id);
        }
    }

    private void get_tbl_Package_ADP_Item()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_ADP_Item(Convert.ToInt32(hf_Package_ADP_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdADPItems.DataSource = ds.Tables[0];
            grdADPItems.DataBind();
            grdADPItems.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(ADP_Item_Qty)", "").ToString();
            grdADPItems.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(ADP_Item_Amount)", "").ToString();
            grdADPItems.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(ADP_Item_TotalAmount)", "").ToString();
            lblTotalAmount.Text = ds.Tables[0].Compute("sum(ADP_Item_TotalAmount)", "").ToString();
            txtFudTransfered.Text = lblTotalAmount.Text;
        }
        else
        {
            grdADPItems.DataSource = null;
            grdADPItems.DataBind();
        }
        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            grdMultipleFiles.DataSource = ds.Tables[1];
            grdMultipleFiles.DataBind();
        }
        else
        {
            grdMultipleFiles.DataSource = null;
            grdMultipleFiles.DataBind();
        }
    }

    protected void grdMultipleFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkDownload") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
        }
    }
    protected void grdDocumentMaster_PreRender(object sender, EventArgs e)
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

    protected void grdDocumentMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

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
        if (ddlScheme.SelectedValue == "-")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        hf_Package_Id.Value = "";
        hf_ProjectWork_Id.Value = "";
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
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);
        }
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
    protected void grdADPItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Package_ADP_Item_Item_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            GridView grdTaxes = e.Row.FindControl("grdTaxes") as GridView;
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_Deduction_WithPackage_ADP_Item_Tax(Package_ADP_Item_Item_Id);
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
                ddlTaxes.SelectedValue = e.Row.Cells[0].Text;
            }
            catch
            {

            }
            ddlTaxes.Enabled = false;
            txtTaxesP.Enabled = false;

        }
    }
    private void get_tbl_InvoiceStatus(int ConfigMasterId, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlStatus, "InvoiceStatus_Name", "InvoiceStatus_Id");
        }
        else
        {
            ddlStatus.Items.Clear();
        }
    }
    private void get_ProcessConfig_Current(int Scheme_Id, string _loop)
    {
        divAdditionalReason.Visible = false;
        if (Session["UserType"].ToString() == "1")
        {
            btnApprove.Visible = true;
            get_tbl_InvoiceStatus(0, Scheme_Id);
        }
        else
        {
            DataSet ds = new DataSet();
            int Loop = 0;
            try
            {
                Loop = Convert.ToInt32(_loop);
            }
            catch
            {
                Loop = 1;
            }
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "PackageADP", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Loop, Convert.ToInt32(hf_Package_ADP_Id.Value));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    get_tbl_TradeDocument(ConfigMaster_Id);
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);

                    get_ProcessConfigMaster_Last(ConfigMaster_Id, Scheme_Id, Loop);
                }
                catch
                {
                    grdDocumentMaster.DataSource = null;
                    grdDocumentMaster.DataBind();
                    ConfigMaster_Id = 0;
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);
                }
                btnApprove.Visible = true;
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Transfer_Allowed"].ToString() == "1")
                {
                    divAmountTransfered.Visible = true;
                    divAdditionalReason.Visible = true;
                    divPPA.Visible = true;
                    if (Scheme_Id == 1013 || Scheme_Id == 1016)
                    {
                        divPPA.Visible = true;
                        divPPAMessage.Visible = true;
                    }
                    else
                    {
                        divPPA.Visible = false;
                        divPPAMessage.Visible = false;
                    }
                }
                else
                {
                    divAdditionalReason.Visible = false;
                    divAmountTransfered.Visible = false;
                    divPPA.Visible = false;
                    divPPAMessage.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Document_Updation_Allowed"].ToString() == "1")
                {
                    chkUpdateExisting.Visible = true;
                    chkUpdateExisting.Checked = true;
                }
                else
                {
                    chkUpdateExisting.Visible = false;
                    chkUpdateExisting.Checked = false;
                }
            }
            else
            {
                btnApprove.Visible = false;
            }
        }
    }

    private void get_tbl_TradeDocument(int ConfigMaster_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_TradeDocument(ConfigMaster_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDocumentMaster.DataSource = ds.Tables[0];
            grdDocumentMaster.DataBind();
        }
        else
        {
            grdDocumentMaster.DataSource = null;
            grdDocumentMaster.DataBind();
        }
    }

    private void get_ProcessConfigMaster_Last(int ProcessConfigMaster_Id_Current, int Scheme_Id, int Loop)
    {
        if (Session["UserType"].ToString() == "1")
        {
            btnApprove.Visible = true;
        }
        else
        {
            DataSet ds = new DataSet();

            ds = (new DataLayer()).get_ProcessConfigMaster_Last_PackageADP(Scheme_Id, Loop, null, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ProcessConfigMaster_Id_Current == Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString()))
                {
                    btnApprove.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                }
            }
            else
            {
                btnApprove.Visible = false;
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        decimal Total_Amount = 0;
        decimal Transfred_Amount = 0;
        decimal PPA_Amount = 0;
        string PPA_Status = "";
        string Debit_Account_No = "";
        int ProjectWork_Id = 0;
        try
        {
            ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            ProjectWork_Id = 0;
        }
        if (divAmountTransfered.Visible)
        {
            try
            {
                Transfred_Amount = decimal.Parse(txtFudTransfered.Text.Trim());
            }
            catch
            {
                Transfred_Amount = 0;
            }
            if (Transfred_Amount == 0)
            {
                MessageBox.Show("Transfred Amount Should Be More Than Zero.");
                return;
            }

            try
            {
                Total_Amount = decimal.Parse(lblTotalAmount.Text.Trim());
            }
            catch
            {
                Total_Amount = 0;
            }
            //if (Transfred_Amount > Total_Amount)
            //{
            //    MessageBox.Show("Transfred Amount Should Be Less or Equal To Total Amount.");
            //    return;
            //}
        }
        if (divPPA.Visible)
        {
            if (hf_Scheme_Id.Value == "1013" || hf_Scheme_Id.Value == "1016")
            {
                if (txtPPANo.Text.Trim() == "")
                {
                    MessageBox.Show("Please Input A Valid PPA No.");
                    txtPPANo.Focus();
                    return;
                }
                if (divPPAVerification.Visible == false)
                {
                    MessageBox.Show("Please Provide A Valid PPA No");
                    txtPPANo.Focus();
                    return;
                }
                if (grdPPAVerification.Rows.Count == 0)
                {
                    MessageBox.Show("Please Provide A Valid PPA No");
                    txtPPANo.Focus();
                    return;
                }
                try
                {
                    PPA_Amount = decimal.Parse(grdPPAVerification.Rows[0].Cells[1].Text.Trim());
                }
                catch
                {
                    PPA_Amount = 0;
                }
                try
                {
                    PPA_Status = grdPPAVerification.Rows[0].Cells[5].Text.Trim();
                }
                catch
                {
                    PPA_Status = "";
                }
                try
                {
                    Debit_Account_No = grdPPAVerification.Rows[0].Cells[2].Text.Trim();
                }
                catch
                {
                    Debit_Account_No = "";
                }
                string _Debit_Account_No = divAccountNo.InnerHtml.Replace("<b>", "").Replace("</b>", "").Replace("Account No:", "").Trim();
                if (_Debit_Account_No != Debit_Account_No)
                {
                    MessageBox.Show("Provided PPA is not Linked With The Accounr Linked With The Project. Please Upload Correct PPA");
                    txtPPANo.Focus();
                    return;
                }
                if (PPA_Status != "Success")
                {
                    MessageBox.Show("Provided PPA is not Cleared From PNB. Please Upload Correct PPA");
                    txtPPANo.Focus();
                    return;
                }
                if (Transfred_Amount != PPA_Amount)
                {
                    MessageBox.Show("Amount Of PPA and Invoice Amount is Mismatch. Please Upload Correct PPA");
                    txtPPANo.Focus();
                    return;
                }
            }
        }
        if (ddlStatus.SelectedValue == "" || ddlStatus.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        if (ddlStatus.SelectedValue == "2" || ddlStatus.SelectedValue == "3")
        {
            if (divAmountTransfered.Visible)
            {
                if (ddlAdditionalReason.SelectedValue == "0")
                {
                    MessageBox.Show("Please Select Deferred Reason");
                    ddlAdditionalReason.Focus();
                    return;
                }
            }
            if (txtComments.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Comments / Reason");
                return;
            }
        }
        List<tbl_Package_ADP_Item_Approval> obj_tbl_Package_ADP_Item_Approval_Li = new List<tbl_Package_ADP_Item_Approval>();
        for (int i = 0; i < grdADPItems.Rows.Count; i++)
        {
            TextBox txtRate = grdADPItems.Rows[i].FindControl("txtRate") as TextBox;
            TextBox txtQty = grdADPItems.Rows[i].FindControl("txtQty") as TextBox;

            tbl_Package_ADP_Item_Approval obj_tbl_Package_ADP_Item_Approval = new tbl_Package_ADP_Item_Approval();
            obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            try
            {
                obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_Rate = Convert.ToDecimal(txtRate.Text.Trim());
            }
            catch
            {
                obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_Rate = 0;
            }
            try
            {
                obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_Qty = Convert.ToDecimal(txtQty.Text.Trim());
            }
            catch
            {
                obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_Qty = 0;
            }
            obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_Package_ADP_Item_Id = Convert.ToInt32(grdADPItems.Rows[i].Cells[0].Text.Trim());
            obj_tbl_Package_ADP_Item_Approval.Package_ADP_Item_Approval_Status = 1;
            obj_tbl_Package_ADP_Item_Approval_Li.Add(obj_tbl_Package_ADP_Item_Approval);
        }

        List<tbl_Package_ADPDocs> obj_tbl_Package_ADPDocs_Li = new List<tbl_Package_ADPDocs>();
        for (int i = 0; i < grdDocumentMaster.Rows.Count; i++)
        {
            FileUpload flUpload = grdDocumentMaster.Rows[i].FindControl("flUpload") as FileUpload;
            TextBox txtDocumentOrderNo = grdDocumentMaster.Rows[i].FindControl("txtDocumentOrderNo") as TextBox;
            TextBox txtDocumentComments = grdDocumentMaster.Rows[i].FindControl("txtDocumentComments") as TextBox;
            if (flUpload.HasFile)
            {
                tbl_Package_ADPDocs obj_tbl_Package_ADPDocs = new tbl_Package_ADPDocs();
                obj_tbl_Package_ADPDocs.Package_ADPDocs_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_Package_ADPDocs.Package_ADPDocs_FileBytes = flUpload.FileBytes;
                string[] _fname = flUpload.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_Package_ADPDocs.Package_ADPDocs_FileName = _fname[_fname.Length - 1];
                obj_tbl_Package_ADPDocs.Package_ADPDocs_Package_ADP_Id = Convert.ToInt32(hf_Package_ADP_Id.Value);
                obj_tbl_Package_ADPDocs.Package_ADPDocs_Status = 1;
                obj_tbl_Package_ADPDocs.Package_ADPDocs_OrderNo = txtDocumentOrderNo.Text.Trim();
                obj_tbl_Package_ADPDocs.Package_ADPDocs_Comments = txtDocumentComments.Text.Trim();
                obj_tbl_Package_ADPDocs.Package_ADPDocs_Type = Convert.ToInt32(grdDocumentMaster.Rows[i].Cells[1].Text.Trim());
                obj_tbl_Package_ADPDocs_Li.Add(obj_tbl_Package_ADPDocs);
            }
            else
            {
                if (chkUpdateExisting.Visible && !chkUpdateExisting.Checked)
                {
                    if (ddlStatus.SelectedValue == "1" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedValue == "5")
                    {
                        MessageBox.Show("Please Upload Document.");
                        return;
                    }
                }
                if (chkUpdateExisting.Visible && !chkUpdateExisting.Checked)
                {
                    MessageBox.Show("Please Upload Document.");
                    return;
                }
            }
        }

        tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
        obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageADPApproval.PackageADPApproval_Comments = txtComments.Text.Trim();
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = Convert.ToInt32(hf_Package_ADP_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
        obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(hf_Loop.Value); ;
        obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value); ;
        obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        try
        {
            obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = Convert.ToInt32(ddlAdditionalReason.SelectedValue);
        }
        catch
        {
            obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = 0;
        }
        obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 1;

        tbl_FinancialTrans obj_tbl_FinancialTrans = null;
        if (divAmountTransfered.Visible && obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id == 1 && Transfred_Amount != 0)
        {
            obj_tbl_FinancialTrans = new tbl_FinancialTrans();
            obj_tbl_FinancialTrans.FinancialTrans_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_FinancialTrans.FinancialTrans_Amount = Total_Amount;
            obj_tbl_FinancialTrans.FinancialTrans_Comments = txtComments.Text.Trim();
            obj_tbl_FinancialTrans.FinancialTrans_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_FinancialTrans.FinancialTrans_EntryType = "C";
            obj_tbl_FinancialTrans.FinancialTrans_FinancialYear_Id = 0;
            obj_tbl_FinancialTrans.FinancialTrans_Invoice_Id = 0;
            obj_tbl_FinancialTrans.FinancialTrans_PackageADP_Id = Convert.ToInt32(hf_Package_ADP_Id.Value);
            obj_tbl_FinancialTrans.FinancialTrans_Status = 1;
            obj_tbl_FinancialTrans.FinancialTrans_TransAmount = Transfred_Amount;
            obj_tbl_FinancialTrans.FinancialTrans_TransType = "C";
        }
        tbl_SNAAccountLimitUsed obj_tbl_SNAAccountLimitUsed = null;
        if (divPPA.Visible)
        {
            if (hf_Scheme_Id.Value == "1013" || hf_Scheme_Id.Value == "1016")
            {
                decimal AvailableBal = 0;
                try
                {
                    AvailableBal = decimal.Parse(divBalance.InnerHtml);
                }
                catch
                {
                    AvailableBal = 0;
                }
                if (AvailableBal < Transfred_Amount)
                {
                    MessageBox.Show("Available Balance is Less Than The Invoice Amount.");
                    return;
                }

                obj_tbl_SNAAccountLimitUsed = new tbl_SNAAccountLimitUsed();
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_BatchId = "";
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_PPAAmount = 0;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_PPANumber = txtPPANo.Text.Trim();
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_PPAVerified = 0;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_ProjectWotk_Id = ProjectWork_Id;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_Status = 1;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsageType = "A";
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_Usage_Id = Convert.ToInt32(hf_Package_ADP_Id.Value);
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsedLimit = Transfred_Amount;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsedOn = Session["ServerDate"].ToString();
            }
        }
        if (obj_tbl_Package_ADP_Item_Approval_Li == null || obj_tbl_Package_ADP_Item_Approval_Li.Count == 0)
        {
            MessageBox.Show("Please Select At One One Item.");
            return;
        }
        if (new DataLayer().Insert_tbl_Package_ADP_Item_Approved(obj_tbl_Package_ADP_Item_Approval_Li, obj_tbl_PackageADPApproval, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Convert.ToInt32(hf_Scheme_Id.Value), Convert.ToInt32(hf_Loop.Value), obj_tbl_Package_ADPDocs_Li, chkUpdateExisting.Checked, obj_tbl_FinancialTrans, obj_tbl_SNAAccountLimitUsed))
        {
            reset();
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")//Administrator
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "2")//District
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "3")//ULB
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "4")//Zone Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "5")//Contractor Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "6")//Circle Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "7")//Division Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else
                {
                    Response.Redirect("DashboardDept.aspx?T=A");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "9")//Operator
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In  Package Items.");
            return;
        }
    }
    protected void btnRecommend_Click(object sender, EventArgs e)
    {
        if (txtComments.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Comments / Reason");
            return;
        }

        List<tbl_PackageADPApproval> obj_tbl_PackageADPApproval_Li = new List<tbl_PackageADPApproval>();
        tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
        obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageADPApproval.PackageADPApproval_Comments = txtComments.Text.Trim();
        obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        obj_tbl_PackageADPApproval.PackageADPApproval_Next_Organisation_Id = 2;
        obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = 1;
        obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 10;
        obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = 0;
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = Convert.ToInt32(hf_Package_ADP_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
        obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(hf_Loop.Value); ;
        obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);
        obj_tbl_PackageADPApproval_Li.Add(obj_tbl_PackageADPApproval);

        if (new DataLayer().Update_tbl_PackageADP_Rejected(obj_tbl_PackageADPApproval_Li))
        {
            txtComments.Text = "";
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")//Administrator
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "2")//District
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "3")//ULB
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "4")//Zone Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "5")//Contractor Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "6")//Circle Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "7")//Division Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else
                {
                    Response.Redirect("DashboardDept.aspx?T=A");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "9")//Operator
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation.");
            return;
        }
    }
    protected void btnRevert_Click(object sender, EventArgs e)
    {
        if (hf_Loop.Value == "" || hf_Loop.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        if (txtComments.Text == "")
        {
            MessageBox.Show("Please Provide Comments");
            return;
        }
        int Loop = 1;
        try
        {
            Loop = Convert.ToInt32(hf_Loop.Value);
        }
        catch
        {
            Loop = 1;
        }

        List<tbl_PackageADPApproval> obj_tbl_PackageADPApproval_Li = new List<tbl_PackageADPApproval>();
        tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
        obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageADPApproval.PackageADPApproval_Comments = txtComments.Text.Trim();
        obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        obj_tbl_PackageADPApproval.PackageADPApproval_Next_Organisation_Id = 1;
        if (Loop == 1)
            obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = 9;
        else
            obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = 4;
        obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 10;
        try
        {
            obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = Convert.ToInt32(ddlAdditionalReason.SelectedValue);
        }
        catch
        {
            obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = 0;
        }
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(hf_Package_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = Convert.ToInt32(hf_Package_ADP_Id.Value);
        obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
        obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(hf_Loop.Value); ;
        obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);
        obj_tbl_PackageADPApproval_Li.Add(obj_tbl_PackageADPApproval);

        if (new DataLayer().Update_tbl_PackageADP_Rejected(obj_tbl_PackageADPApproval_Li))
        {
            hf_Package_Id.Value = "0";
            txtComments.Text = "";
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")//Administrator
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "2")//District
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "3")//ULB
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "4")//Zone Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "5")//Contractor Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "6")//Circle Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "7")//Division Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else
                {
                    Response.Redirect("DashboardDept.aspx?T=A");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "9")//Operator
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation.");
            return;
        }
    }
    protected void grdPPAVerification_PreRender(object sender, EventArgs e)
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

    protected void txtPPANo_TextChanged(object sender, EventArgs e)
    {
        PPA_Details obj_PPA_Details = new PPA_Details();
        obj_PPA_Details.PPA_Number = txtPPANo.Text.Trim();
        obj_PPA_Details.PPA_Date = "";
        obj_PPA_Details.Req_Type = "Batch";
        obj_PPA_Details.Entity_Code = "AMRUT";

        List<PNB_GetPPADetails_Res> obj_PNB_GetPPADetails_Res = new DataLayerSNA().GetPPABatchDetails(obj_PPA_Details);
        if (obj_PNB_GetPPADetails_Res != null && obj_PNB_GetPPADetails_Res.Count > 0)
        {
            divPPAVerification.Visible = true;
            grdPPAVerification.DataSource = obj_PNB_GetPPADetails_Res;
            grdPPAVerification.DataBind();
        }
        else
        {
            divPPAVerification.Visible = false;
            grdPPAVerification.DataSource = null;
            grdPPAVerification.DataBind();
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "2")
        {
            divAdditionalReason.Visible = true;
        }
        else
        {
            divAdditionalReason.Visible = false;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecommend.Visible = false;
        btnRevert.Visible = false;
        string Scheme_Id = ddlScheme.SelectedValue;
        if ((Scheme_Id == "1013" || Scheme_Id == "1016") && Session["PersonJuridiction_DesignationId"].ToString() == "34")
        {
            btnRecommend.Visible = true;
        }
        else
        {
            btnRecommend.Visible = false;
        }
        if ((Scheme_Id == "1013" || Scheme_Id == "1016") && Session["PersonJuridiction_DesignationId"].ToString() == "33")
        {
            btnRevert.Visible = true;
        }
        else
        {
            btnRevert.Visible = false;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }
}