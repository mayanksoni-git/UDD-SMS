using Aspose.Pdf.Operators;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;

public partial class MasterProjectWorkCertificate : System.Web.UI.Page
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

            ViewState["dtQuestionnaire"] = null;

            get_tbl_Unit();
            get_tbl_Project();
            get_M_Jurisdiction();
            get_tbl_Zone();
            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    ddlDistrict.Enabled = false;
                    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
                    {//ULB
                        try
                        {
                            ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            ddlULB.Enabled = false;
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
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
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
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlsearchDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlsearchDivision.Enabled = false;
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
            //get_tbl_ProjectWork();
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
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
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
            bool is_Selected = false;
            ddlScheme.DataTextField = "Project_Name";
            ddlScheme.DataValueField = "Project_Id";
            ddlScheme.DataSource = ds.Tables[0];
            ddlScheme.DataBind();
            try
            {
                foreach (ListItem listItem in ddlScheme.Items)
                {
                    if (listItem.Value == Session["Default_Scheme"].ToString())
                    {
                        is_Selected = true;
                        listItem.Selected = true;
                    }
                }
            }
            catch
            {
                ddlScheme.Items[0].Selected = true;
            }
            if (is_Selected == false)
            {
                ddlScheme.Items[0].Selected = true;
            }
        }
        else
        {
            ddlScheme.Items.Clear();
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
        get_tbl_ProjectWork();
    }

    protected void get_tbl_ProjectWork()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
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
        ds = (new DataLayer()).get_tbl_ProjectWork_Special(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", 0, txtProjectCode.Text.Trim());
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

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlSearchZone.Items.Clear();
        }
    }

    private void get_tbl_Circle_Search(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlSearchCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Search(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlsearchDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlsearchDivision.Items.Clear();
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        GridViewRow gr = lnkUpdate.Parent.Parent as GridViewRow;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        hf_Division_Id.Value = gr.Cells[4].Text.Trim();
        hf_ProjectWork_Name.Value = gr.Cells[11].Text.Trim();
        int Work_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(Work_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();
            divPackage.Visible = true;

            if (ds.Tables[0].Rows.Count == 1)
            {
                ImageButton btnPackageEdit = grdPackageDetails.Rows[0].FindControl("btnPackageEdit") as ImageButton;
                btnPackageEdit_Click(btnPackageEdit, e);
            }
        }
        else
        {
            divPackage.Visible = false;
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
            MessageBox.Show("Package Details Not Found");
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void ddlSearchZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            ddlSearchCircle.Items.Clear();
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Search(Convert.ToInt32(ddlSearchZone.SelectedValue));
        }
    }

    protected void ddlSearchCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Search(Convert.ToInt32(ddlSearchCircle.SelectedValue));
        }
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

    protected void grdPackageDetails_PreRender(object sender, EventArgs e)
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

    protected void btnPackageEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPackageEdit = sender as ImageButton;
        GridViewRow gr = btnPackageEdit.Parent.Parent as GridViewRow;
        for (int i = 0; i < grdPackageDetails.Rows.Count; i++)
        {
            grdPackageDetails.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        hf_ProjectWorkPkg_Id.Value = gr.Cells[0].Text.Trim();
        int WorkPkg_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Work_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        int Division_Id = Convert.ToInt32(hf_Division_Id.Value);
        get_tbl_ProjectWorkPkgCert(0, WorkPkg_Id);
        get_tbl_Division1(Division_Id);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(WorkPkg_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtNameWork.Text = hf_ProjectWork_Name.Value + ", Package: " + ds.Tables[0].Rows[0]["ProjectWorkPkg_Name"].ToString();
            txtNameWork.Enabled = false;
            txtVendorPMIS.Text = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            txtVendorPMIS.Enabled = false;
            txtAgreementNo.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_No"].ToString();
            txtAgreementNo.Enabled = false;
            txtAgreementDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_Date"].ToString();
            txtAgreementDate.Enabled = false;
            txtStartDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_Date"].ToString();
            txtStartDate.Enabled = false;
            txtEndDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Due_Date"].ToString();
            txtEndDate.Enabled = false;
            txtEndDateExtended.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_ExtendDate"].ToString();
            txtEndDateExtended.Enabled = false;
            txtTenderCost.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_AgreementAmount"].ToString();
            txtTenderCost.Enabled = false;
            txtContactDetails.Text = ds.Tables[0].Rows[0]["Person_Contact"].ToString();
            txtContactDetails.ToolTip = ds.Tables[0].Rows[0]["EE_Name"].ToString();
            txtContactDetails.Enabled = false;
        }
        else
        {
            txtNameWork.Text = "";
            txtVendorPMIS.Text = "";
            txtAgreementNo.Text = "";
            txtAgreementDate.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtEndDateExtended.Text = "";
            txtTenderCost.Text = "";
            txtContactDetails.Text = "";
        }

        ds = new DataLayer().get_Physical_Component_Certificate(Work_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["dtQuestionnaire"] = ds.Tables[0];
            dgvQuestionnaire.DataSource = ds.Tables[0];
            dgvQuestionnaire.DataBind();
        }
        else
        {
            Add_Questionire();
        }

        divCertificateDetails.Visible = true;
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtQuestionnaire"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtQuestionnaire"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            dgvQuestionnaire.DataSource = dt;
            dgvQuestionnaire.DataBind();
            ViewState["dtQuestionnaire"] = dt;
        }
    }

    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        Add_Questionire();
    }

    private void Add_Questionire()
    {
        DataTable dtQuestionnaire;
        if (ViewState["dtQuestionnaire"] != null)
        {
            dtQuestionnaire = (DataTable)(ViewState["dtQuestionnaire"]);
            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
            ViewState["dtQuestionnaire"] = dtQuestionnaire;

            dgvQuestionnaire.DataSource = dtQuestionnaire;
            dgvQuestionnaire.DataBind();
        }
        else
        {
            dtQuestionnaire = new DataTable();

            DataColumn dc_ProjectWorkPkgCertComp_Id = new DataColumn("ProjectWorkPkgCertComp_Id", typeof(int));
            DataColumn dc_ProjectWorkPkgCertComp_Unit_Id = new DataColumn("ProjectWorkPkgCertComp_Unit_Id", typeof(int));
            DataColumn dc_ProjectWorkPkgCertComp_CompName = new DataColumn("ProjectWorkPkgCertComp_CompName", typeof(string));
            DataColumn dc_ProjectWorkPkgCertComp_CostAsPerBOQ = new DataColumn("ProjectWorkPkgCertComp_CostAsPerBOQ", typeof(decimal));
            DataColumn dc_ProjectWorkPkgCertComp_CostAsPerActual = new DataColumn("ProjectWorkPkgCertComp_CostAsPerActual", typeof(decimal));
            DataColumn dc_ProjectWorkPkgCertComp_PraposedNoAsPerBOQ = new DataColumn("ProjectWorkPkgCertComp_PraposedNoAsPerBOQ", typeof(decimal));
            DataColumn dc_ProjectWorkPkgCertComp_PraposedNoAsPerActual = new DataColumn("ProjectWorkPkgCertComp_PraposedNoAsPerActual", typeof(decimal));
            DataColumn dc_ProjectWorkPkgCertComp_Completed = new DataColumn("ProjectWorkPkgCertComp_Completed", typeof(decimal));
            DataColumn dc_ProjectWorkPkgCertComp_Functional = new DataColumn("ProjectWorkPkgCertComp_Functional", typeof(decimal));
            DataColumn dc_ProjectWorkPkgCertComp_Remarks = new DataColumn("ProjectWorkPkgCertComp_Remarks", typeof(decimal));

            dtQuestionnaire.Columns.AddRange(new DataColumn[] { dc_ProjectWorkPkgCertComp_Id, dc_ProjectWorkPkgCertComp_Unit_Id, dc_ProjectWorkPkgCertComp_CompName, dc_ProjectWorkPkgCertComp_CostAsPerBOQ, dc_ProjectWorkPkgCertComp_CostAsPerActual, dc_ProjectWorkPkgCertComp_PraposedNoAsPerBOQ, dc_ProjectWorkPkgCertComp_PraposedNoAsPerActual, dc_ProjectWorkPkgCertComp_Completed, dc_ProjectWorkPkgCertComp_Functional, dc_ProjectWorkPkgCertComp_Remarks });

            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
            ViewState["dtQuestionnaire"] = dtQuestionnaire;

            dgvQuestionnaire.DataSource = dtQuestionnaire;
            dgvQuestionnaire.DataBind();
        }
    }

    protected void dgvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectWorkPkgCertComp_Unit_Id = 0;
            DropDownList ddlUnit = e.Row.FindControl("ddlUnit") as DropDownList;
            if (ViewState["Unit"] != null)
            {
                if (e.Row.RowIndex == 0)
                    AllClasses.FillDropDown((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                else
                    AllClasses.FillDropDown_WithOutSelect((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
            }
            try
            {
                ProjectWorkPkgCertComp_Unit_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                ProjectWorkPkgCertComp_Unit_Id = 0;
            }
            ddlUnit.SelectedValue = ProjectWorkPkgCertComp_Unit_Id.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (rbtVendorType.SelectedValue == "L")
        {
            if (txtLeadVendor.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Lead Vendor Details");
                txtLeadVendor.Focus();
                return;
            }
        }
        else
        {
            if (txtLeadVendor.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Lead Vendor Details");
                txtLeadVendor.Focus();
                return;
            }
            if (txtLeadVendorShare.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Lead Vendor Share");
                txtLeadVendorShare.Focus();
                return;
            }
            if (txtPartnerVendor1.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Lead Vendor Details");
                txtPartnerVendor1.Focus();
                return;
            }
            if (txtPartnerVendorShare1.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Lead Vendor Share");
                txtPartnerVendorShare1.Focus();
                return;
            }
        }
        if (txtTestingCommissionedDate.Text == "")
        {
            MessageBox.Show("Please Provide Testing and Commissioned Date");
            txtTestingCommissionedDate.Focus();
            return;
        }
        if (txtGrossAmountCompleted.Text == "")
        {
            MessageBox.Show("Please Provide Gross Amount Completed");
            txtGrossAmountCompleted.Focus();
            return;
        }
        if (rbtQualifiedStaff.SelectedValue == null || rbtQualifiedStaff.SelectedValue == "")
        {
            MessageBox.Show("Please Provide contractor employed qualified Engineer / Overseer during execution of work?");
            rbtQualifiedStaff.Focus();
            return;
        }
        if (ddlQualityWork.SelectedValue == null || ddlQualityWork.SelectedValue == "")
        {
            MessageBox.Show("Please Provide Quality of work");
            ddlQualityWork.Focus();
            return;
        }
        if (ddlTechProficiency.SelectedValue == null || ddlTechProficiency.SelectedValue == "")
        {
            MessageBox.Show("Please Provide Technical Proficiency");
            ddlTechProficiency.Focus();
            return;
        }
        if (ddlFinancialSounness.SelectedValue == null || ddlFinancialSounness.SelectedValue == "")
        {
            MessageBox.Show("Please Provide Financial Sounness");
            ddlFinancialSounness.Focus();
            return;
        }
        if (ddlMobAdequate.SelectedValue == null || ddlMobAdequate.SelectedValue == "")
        {
            MessageBox.Show("Please Provide Mobilization of adequate T&P");
            ddlMobAdequate.Focus();
            return;
        }
        if (ddlMobManpower.SelectedValue == null || ddlMobManpower.SelectedValue == "")
        {
            MessageBox.Show("Please Provide Mobilization of manpower");
            ddlMobManpower.Focus();
            return;
        }
        if (ddlBehavior.SelectedValue == null || ddlBehavior.SelectedValue == "")
        {
            MessageBox.Show("Please Provide General behavior");
            ddlBehavior.Focus();
            return;
        }
        if (txtOfficeAddress.Text == "")
        {
            MessageBox.Show("Please Provide Office Full Address");
            txtOfficeAddress.Focus();
            return;
        }
        if (txtOfficeContact.Text == "")
        {
            MessageBox.Show("Please Provide Office Contact Details");
            txtOfficeContact.Focus();
            return;
        }
        if (txtOfficeEmailID.Text == "")
        {
            MessageBox.Show("Please Provide Office E Mail ID");
            txtOfficeEmailID.Focus();
            return;
        }
        tbl_ProjectWorkPkgCert obj_tbl_ProjectWorkPkgCert = new tbl_ProjectWorkPkgCert();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_AgreementDate = txtAgreementDate.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_AgrrementNo = txtAgreementNo.Text.Trim();
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_AmountAwarded = Convert.ToDecimal(txtAwardedAmount.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_AmountAwarded = 0;
        }
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Behavious = ddlBehavior.SelectedValue;
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_ClaimAmount = Convert.ToDecimal(txtClaimAmount.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_ClaimAmount = 0;
        }
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_EE_Name = txtContactDetails.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_ExEngName = txtContactDetails.ToolTip.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_EndDate = txtEndDate.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_EndDateExtended = txtEndDateExtended.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_CompletedCommissionedDate = txtTestingCommissionedDate.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_FinancialSoundness = ddlFinancialSounness.SelectedValue;
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_GrossAmountCompleted = Convert.ToDecimal(txtGrossAmountCompleted.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_GrossAmountCompleted = 0;
        }
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Is_Arbitration = rbtArbitration.SelectedItem.Value;
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Is_Qualified = rbtQualifiedStaff.SelectedItem.Value;
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_LD_Details = txtLDApplied.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Manpower = ddlMobManpower.SelectedValue;
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_PaidOnReducedRate = Convert.ToDecimal(txtAmountPaidReducedRate.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_PaidOnReducedRate = 0;
        }
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Pkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_QualityOfWork = ddlQualityWork.SelectedValue;
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_StartDate = txtStartDate.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Status = 1;
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_TechnicalProficiency = ddlTechProficiency.SelectedValue;
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_TenderCost = Convert.ToDecimal(txtTenderCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_TenderCost = 0;
        }
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_TP = ddlMobAdequate.SelectedValue;
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_WorkName = txtNameWork.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Comments = txtAdditionalComments.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.Division_Office_ContactNo = txtOfficeContact.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.Division_Office_eMailID = txtOfficeEmailID.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.Division_Office_FullAddress = txtOfficeAddress.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Lead_Bidder = txtLeadVendor.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Partner_Bidder = txtPartnerVendor1.Text.Trim();
        obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Partner_Bidder2 = txtPartnerVendor2.Text.Trim();
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Lead_Bidder_Share = Convert.ToDecimal(txtLeadVendorShare.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Lead_Bidder_Share = 0;
        }
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Partner_Bidder_Share = Convert.ToDecimal(txtPartnerVendorShare1.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Partner_Bidder_Share = 0;
        }
        try
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Partner_Bidder_Share2 = Convert.ToDecimal(txtPartnerVendorShare2.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkgCert.ProjectWorkPkgCert_Partner_Bidder_Share2 = 0;
        }
        List<tbl_ProjectWorkPkgCertComp> obj_tbl_ProjectWorkPkgCertComp_Li = new List<tbl_ProjectWorkPkgCertComp>();
        CheckBox chkSelect;
        TextBox txtComponentName;
        TextBox txtCostAsPerBOQ;
        TextBox txtCostAsPerActual;
        TextBox txtPraposedNoAsPerBOQ;
        TextBox txtPraposedNoAsPerActual;
        TextBox txtCompleted;
        TextBox txtFunctional;
        TextBox txtRemarks;
        DropDownList ddlUnit;
        for (int i = 0; i < dgvQuestionnaire.Rows.Count; i++)
        {
            chkSelect = dgvQuestionnaire.Rows[i].FindControl("chkSelect") as CheckBox;
            txtComponentName = dgvQuestionnaire.Rows[i].FindControl("txtComponentName") as TextBox;
            txtCostAsPerBOQ = dgvQuestionnaire.Rows[i].FindControl("txtCostAsPerBOQ") as TextBox;
            txtCostAsPerActual = dgvQuestionnaire.Rows[i].FindControl("txtCostAsPerActual") as TextBox;
            txtPraposedNoAsPerBOQ = dgvQuestionnaire.Rows[i].FindControl("txtPraposedNoAsPerBOQ") as TextBox;
            txtPraposedNoAsPerActual = dgvQuestionnaire.Rows[i].FindControl("txtPraposedNoAsPerActual") as TextBox;
            txtCompleted = dgvQuestionnaire.Rows[i].FindControl("txtCompleted") as TextBox;
            txtFunctional = dgvQuestionnaire.Rows[i].FindControl("txtFunctional") as TextBox;
            txtRemarks = dgvQuestionnaire.Rows[i].FindControl("txtRemarks") as TextBox;
            ddlUnit = dgvQuestionnaire.Rows[i].FindControl("ddlUnit") as DropDownList;
            if (chkSelect.Checked)
            {
                if (txtComponentName.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Name!!");
                    txtComponentName.Focus();
                    return;
                }
                if (txtCostAsPerBOQ.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Cost As Per Origional BOQ In Lakhs!!");
                    txtCostAsPerBOQ.Focus();
                    return;
                }
                if (txtCostAsPerActual.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Cost As Per Actual In Lakhs!!");
                    txtCostAsPerActual.Focus();
                    return;
                }
                if (txtPraposedNoAsPerBOQ.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Praposed Nos As Per Origional BOQ!!");
                    txtPraposedNoAsPerBOQ.Focus();
                    return;
                }
                if (txtPraposedNoAsPerActual.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Praposed Nos As Per Actual!!");
                    txtPraposedNoAsPerActual.Focus();
                    return;
                }
                if (txtCompleted.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Completed Nos!!");
                    txtCompleted.Focus();
                    return;
                }
                if (txtFunctional.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Component Functional Nos!!");
                    txtFunctional.Focus();
                    return;
                }
                tbl_ProjectWorkPkgCertComp obj_tbl_ProjectWorkPkgCertComp = new tbl_ProjectWorkPkgCertComp();
                obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Unit_Id = Convert.ToInt32(dgvQuestionnaire.Rows[i].Cells[1].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Id = 0;
                }

                obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_CompName = txtComponentName.Text.Trim();
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_CostAsPerBOQ = Convert.ToDecimal(txtCostAsPerBOQ.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_CostAsPerBOQ = 0;
                }
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_CostAsPerActual = Convert.ToDecimal(txtCostAsPerActual.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_CostAsPerActual = 0;
                }
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_PraposedNoAsPerBOQ = Convert.ToDecimal(txtPraposedNoAsPerBOQ.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_PraposedNoAsPerBOQ = 0;
                }
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_PraposedNoAsPerActual = Convert.ToDecimal(txtPraposedNoAsPerActual.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_PraposedNoAsPerActual = 0;
                }
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Completed = Convert.ToDecimal(txtCompleted.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Completed = 0;
                }
                try
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Functional = Convert.ToDecimal(txtFunctional.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Functional = 0;
                }
                obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Remarks = txtRemarks.Text.Trim();
                obj_tbl_ProjectWorkPkgCertComp.ProjectWorkPkgCertComp_Status = 1;
                obj_tbl_ProjectWorkPkgCertComp_Li.Add(obj_tbl_ProjectWorkPkgCertComp);
            }
        }

        int Division_Id = Convert.ToInt32(hf_Division_Id.Value);

        if (new DataLayer().Insert_tbl_ProjectWorkPkgCert(obj_tbl_ProjectWorkPkgCert, obj_tbl_ProjectWorkPkgCertComp_Li, Division_Id))
        {
            MessageBox.Show("Details For Certificate Generation Has Been Saved Successfully");
            get_tbl_ProjectWorkPkgCert(0, Convert.ToInt32(hf_ProjectWorkPkg_Id.Value));
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving Details");
            return;
        }
    }

    protected void grdCertificateDetails_PreRender(object sender, EventArgs e)
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

    protected void get_tbl_ProjectWorkPkgCert(int ProjectWork_Id, int ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkgCert(ProjectWork_Id, ProjectWorkPkg_Id, 0, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdCertificateDetails.DataSource = ds.Tables[0];
            grdCertificateDetails.DataBind();
        }
        else
        {
            grdCertificateDetails.DataSource = null;
            grdCertificateDetails.DataBind();
        }
    }

    protected void get_tbl_Division1(int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(0, Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtOfficeAddress.Text = ds.Tables[0].Rows[0]["Division_OfficeAddress"].ToString();
            txtOfficeContact.Text = ds.Tables[0].Rows[0]["Division_Phone"].ToString();
            txtOfficeEmailID.Text = ds.Tables[0].Rows[0]["Division_EmailID"].ToString();

        }
        else
        {
            txtOfficeAddress.Text = "";
            txtOfficeContact.Text = "";
            txtOfficeEmailID.Text = "";
        }
    }

    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        List<Report_ProjectWorkPkgCert> obj_Report_ProjectWorkPkgCert_Li = new List<Report_ProjectWorkPkgCert>();
        List<Certificate_Component> obj_Certificate_Component_Li = new List<Certificate_Component>();

        ImageButton btnPrint = sender as ImageButton;
        GridViewRow gr = btnPrint.Parent.Parent as GridViewRow;
        for (int i = 0; i < grdCertificateDetails.Rows.Count; i++)
        {
            grdCertificateDetails.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int ProjectWorkPkgCert_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkgCert(0, 0, ProjectWorkPkgCert_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Report_ProjectWorkPkgCert obj_Report_ProjectWorkPkgCert = new Report_ProjectWorkPkgCert();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_AgreementDate = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_AgreementDate"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_AgrrementNo = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_AgrrementNo"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_AmountAwarded = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_AmountAwarded"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Behavious = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Behavious"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_ClaimAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_ClaimAmount"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_EE_Name = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_EE_Name"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_EndDate = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_EndDate"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_EndDateExtended = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_EndDateExtended"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_CompletedCommissionedDate = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_CompletedCommissionedDate"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_FinancialSoundness = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_FinancialSoundness"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_GrossAmountCompleted = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_GrossAmountCompleted"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Id"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Is_Arbitration = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Is_Arbitration"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Is_Qualified = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Is_Qualified"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_LD_Details = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_LD_Details"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Manpower = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Manpower"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Comments = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Comments"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_No = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_No"].ToString();
            //string SavePath = "";
            //obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_No_BarCode = new AllClasses().getBarCode(obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_No.Replace("UPJN/CERT/", ""), false, "gif", "c", "Code 128", ref SavePath);
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_No_BarCode = Generate_QR(obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_No);
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_PaidOnReducedRate = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_PaidOnReducedRate"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_Pkg_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Pkg_Id"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_QualityOfWork = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_QualityOfWork"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_StartDate = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_StartDate"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_TechnicalProficiency = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_TechnicalProficiency"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_TenderCost = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWorkPkgCert_TenderCost"].ToString());
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_TP = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_TP"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_ExEngName = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_ExEngName"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_WorkName = ds.Tables[0].Rows[0]["ProjectWorkPkgCert_WorkName"].ToString() + ", Project Code: " + ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString() + ", Package Code: " + ds.Tables[0].Rows[0]["ProjectWorkPkg_Code"].ToString();
            obj_Report_ProjectWorkPkgCert.Division_Office_FullAddress = "Office Address: " + ds.Tables[0].Rows[0]["Division_OfficeAddress"].ToString();
            obj_Report_ProjectWorkPkgCert.Division_Office_ContactNo = "Mobile No: " + ds.Tables[0].Rows[0]["Division_Phone"].ToString();
            obj_Report_ProjectWorkPkgCert.Division_Office_eMailID = "E-Mail ID: " + ds.Tables[0].Rows[0]["Division_EmailID"].ToString();
            obj_Report_ProjectWorkPkgCert.Division_Office_OfficeName = ds.Tables[0].Rows[0]["Division_Name"].ToString();
            obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_AddedOn = "This Certificate was Issued On: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_AddedOn"].ToString();
            if (ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder2"].ToString().Trim() != "")
            {
                obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_ModifiedOn = "Lead Vendor: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Lead_Bidder"].ToString().Trim() + ", Share: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Lead_Bidder_Share"].ToString().Trim() + ", Partner Vendor: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder"].ToString().Trim() + ", Share: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder_Share"].ToString().Trim() + ", Partner Vendor: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder2"].ToString().Trim() + ", Share: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder_Share2"].ToString().Trim();
            }
            else if (ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder2"].ToString().Trim() == "" && ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder"].ToString().Trim() != "")
            {
                obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_ModifiedOn = "Lead Vendor: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Lead_Bidder"].ToString().Trim() + ", Share: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Lead_Bidder_Share"].ToString().Trim() + ", Partner Vendor: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder"].ToString().Trim() + ", Share: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder_Share"].ToString().Trim();
            }
            else if (ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder2"].ToString().Trim() == "" && ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Partner_Bidder"].ToString().Trim() == "" && ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Lead_Bidder"].ToString().Trim() != "")
            {
                obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_ModifiedOn = "Vendor: " + ds.Tables[0].Rows[0]["ProjectWorkPkgCert_Lead_Bidder"].ToString().Trim();
            }
            else
            {
                obj_Report_ProjectWorkPkgCert.ProjectWorkPkgCert_ModifiedOn = ds.Tables[0].Rows[0]["Person_FName"].ToString();
            }
            obj_Report_ProjectWorkPkgCert_Li.Add(obj_Report_ProjectWorkPkgCert);
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                Certificate_Component obj_Certificate_Component = new Certificate_Component();
                obj_Certificate_Component.Remarks = ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_Remarks"].ToString();
                obj_Certificate_Component.Component_Name = ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_CompName"].ToString();
                try
                {
                    obj_Certificate_Component.Completed = Convert.ToDecimal(ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_Completed"].ToString());
                }
                catch
                { }
                try
                {
                    obj_Certificate_Component.Functional = Convert.ToDecimal(ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_Functional"].ToString());
                }
                catch
                { }
                try
                {
                    obj_Certificate_Component.Praposed_No_Actual = Convert.ToDecimal(ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_PraposedNoAsPerActual"].ToString());
                }
                catch
                { }
                try
                {
                    obj_Certificate_Component.Praposed_No_Origional = Convert.ToDecimal(ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_PraposedNoAsPerBOQ"].ToString());
                }
                catch
                { }
                try
                {
                    obj_Certificate_Component.Total_Cost_As_Per_Actual = Convert.ToDecimal(ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_CostAsPerActual"].ToString());
                }
                catch
                { }
                try
                {
                    obj_Certificate_Component.Total_Cost_As_Per_BOQ = Convert.ToDecimal(ds.Tables[1].Rows[i]["ProjectWorkPkgCertComp_CostAsPerBOQ"].ToString());
                }
                catch
                { }
                obj_Certificate_Component_Li.Add(obj_Certificate_Component);
            }
        }
        if (obj_Report_ProjectWorkPkgCert_Li != null && obj_Report_ProjectWorkPkgCert_Li.Count > 0)
        {
            
        }
        else
        {
            MessageBox.Show("Unable To Generate Report.");
            return;
        }
    }

    protected byte[] Generate_QR(string code)
    {
        byte[] byteImage = null;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byteImage = ms.ToArray();
                //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }
        return byteImage;
    }

    protected void rbtVendorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtVendorType.SelectedValue == "L")
        {
            trPartner1.Visible = false;
            trPartner2.Visible = false;
            txtLeadVendorShare.Visible = false;
        }
        else
        {
            trPartner1.Visible = true;
            trPartner2.Visible = true;
            txtLeadVendorShare.Visible = true;
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkPkgCert_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (new DataLayer().Delete_tbl_ProjectWorkPkgCert(ProjectWorkPkgCert_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            MessageBox.Show("Deleted Successfully");
            get_tbl_ProjectWorkPkgCert(0, Convert.ToInt32(hf_ProjectWorkPkg_Id.Value));
            return;
        }
        else
        {
            MessageBox.Show("Error In Deleting Certificate");
            return;
        }
    }

    protected void grdCertificateDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
            if (Session["UserType"].ToString() == "1")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void btnGeneratecombinedCertificate_Click(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        int Division_Id = Convert.ToInt32(hf_Division_Id.Value);
        get_tbl_ProjectWorkPkgCert(ProjectWork_Id, 0);
        get_tbl_Division1(Division_Id);

        txtNameWork.Text = "";
        txtVendorPMIS.Text = "";
        txtAgreementNo.Text = "";
        txtAgreementDate.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtEndDateExtended.Text = "";
        txtTenderCost.Text = "";
        txtContactDetails.Text = "";

        DataSet ds = new DataLayer().get_Physical_Component_Certificate(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["dtQuestionnaire"] = ds.Tables[0];
            dgvQuestionnaire.DataSource = ds.Tables[0];
            dgvQuestionnaire.DataBind();
        }
        else
        {
            Add_Questionire();
        }

        divCertificateDetails.Visible = true;
    }
}