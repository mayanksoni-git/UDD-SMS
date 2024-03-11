using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkULB : System.Web.UI.Page
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
            get_M_Jurisdiction();
            get_tbl_Project();
            get_Employee_Vendor(0);
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
            get_tbl_ProjectWork();
        }
        if (Session["UserType"].ToString() == "1")
        {
            divCreate.Visible = true;
        }
        else if(Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
        {
            divCreate.Visible = true;
        }
        else
        {
            divCreate.Visible = false;
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
    
    protected void get_tbl_ProjectWork()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Project_Id = "";

        Project_Id = "1014";
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
        ds = (new DataLayer()).get_tbl_ProjectWork_And_Pkg(Project_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", 0, "", 0, 0);
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
            AllClasses.FillDropDown(ds.Tables[0], ddlProjectMaster, "Project_Name", "Project_Id");
            try
            {
                ddlProjectMaster.SelectedValue = "1014";
                ddlProjectMaster.Enabled = false;
            }
            catch
            {
                
            }
        }
        else
        {
            ddlProjectMaster.Items.Clear();
        }
    }
    private void get_Employee_Vendor(int Division_Id)
    {
        string UserTypeId = "5";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee(UserTypeId, 0, 0, 0, 0, 0, 0, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlVendor, "Person_Name_Mobile", "Person_Id");
        }
        else
        {
            ddlVendor.Items.Clear();
        }
    }

    private void get_Employee_Staff_JEAPE(int Package_Id, int Division_Id)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlProjectMaster.Focus();
            return;
        }

        int Project_Id = 0;
        try
        {
            Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
        string Designation_Id = "8,12, 1043";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee_Reporting("", 0, 0, Division_Id, Designation_Id, Package_Id, Project_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //AllClasses.FillDropDown(ds.Tables[0], ddlStaff, "Person_Name_Mobile", "Person_Id");
            lbReportingStaffJEAPE.DataTextField = "Person_Name_Mobile";
            lbReportingStaffJEAPE.DataValueField = "Person_Id";
            lbReportingStaffJEAPE.DataSource = ds.Tables[0];
            lbReportingStaffJEAPE.DataBind();
        }
        else
        {
            lbReportingStaffJEAPE.Items.Clear();
        }
    }
    private void get_Employee_Staff_AEPE(int Package_Id, int Division_Id)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlProjectMaster.Focus();
            return;
        }

        int Project_Id = 0;
        try
        {
            Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
        string Designation_Id = "10,5";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee_Reporting("", 0, 0, Division_Id, Designation_Id, Package_Id, Project_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //AllClasses.FillDropDown(ds.Tables[0], ddlStaff, "Person_Name_Mobile", "Person_Id");
            lbReportingStaffAEPE.DataTextField = "Person_Name_Mobile";
            lbReportingStaffAEPE.DataValueField = "Person_Id";
            lbReportingStaffAEPE.DataSource = ds.Tables[0];
            lbReportingStaffAEPE.DataBind();
        }
        else
        {
            lbReportingStaffAEPE.Items.Clear();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlProjectMaster.Focus();
            return;
        }
        if (txtProjectWorkName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Name");
            txtProjectWorkName.Focus();
            return;
        }
        if (ddlDistrict.SelectedValue == "0")
        {
            MessageBox.Show("Please Select District");
            ddlDistrict.Focus();
            return;
        }
        if (txtProjectCode.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Code");
            txtProjectCode.Focus();
            return;
        }
        if (txtBudget.Text.Trim() == "" || txtBudget.Text.Trim() == "0")
        {
            MessageBox.Show("Please Provide Project Allocated Budget");
            txtBudget.Focus();
            return;
        }
        if (txtGODate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide GO Date");
            txtGODate.Focus();
            return;
        }
        if (txtGONo.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide GO No");
            txtGONo.Focus();
            return;
        }
        if (txtAgreementAmount.Text.Trim() == "" || txtAgreementAmount.Text.Trim() == "0")
        {
            MessageBox.Show("Please Provide Project Agreement Amount");
            txtAgreementAmount.Focus();
            return;
        }
        if (txtAgreementDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Agreement Date");
            txtAgreementDate.Focus();
            return;
        }
        if (txtAgreementNo.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Agreement No");
            txtAgreementNo.Focus();
            return;
        }
        if (Session["UserType"].ToString() != "1")
        {
            if (txtLastRABillDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Last RA Bill Date. In  Case of New CB Please Fill Zero.");
                txtLastRABillDate.Focus();
                return;
            }
        }
        
        tbl_ProjectWork obj_tbl_ProjectWork = new tbl_ProjectWork();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_Contegencytext = 0;
        obj_tbl_ProjectWork.ProjectWork_Is_Phase_1 = 0;
        obj_tbl_ProjectWork.ProjectWork_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        string extGO = "";
        
        obj_tbl_ProjectWork.ProjectWork_GO_Date = txtGODate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_GO_No = txtGONo.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = Convert.ToDecimal(txtBudget.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_Centage = 0;
        obj_tbl_ProjectWork.ProjectWork_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Description = obj_tbl_ProjectWork.ProjectWork_Name;
        obj_tbl_ProjectWork.ProjectWork_ProjectCode = txtProjectCode.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = 0;
        obj_tbl_ProjectWork.ProjectWork_DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_BlockId = 0;
        obj_tbl_ProjectWork.ProjectWork_ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWork.ProjectWork_Status = 1;
        List<tbl_ProjectWorkFundingPattern> obj_tbl_ProjectWorkFundingPattern_Li = new List<tbl_ProjectWorkFundingPattern>();

        List<tbl_ProjectAdditionalArea> obj_tbl_ProjectAdditionalArea_Li = new List<tbl_ProjectAdditionalArea>();
        
        List<tbl_ProjectPkg_PhysicalProgress> obj_tbl_ProjectPkg_PhysicalProgress = new List<tbl_ProjectPkg_PhysicalProgress>();
                
        tbl_ProjectWorkPkg obj_tbl_ProjectWorkPkg = new tbl_ProjectWorkPkg();
        try
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Work_Id = 0;
        }
        try
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Id = 0;
        }
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Agreement_Date = txtAgreementDate.Text.Trim();
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Due_Date = txtDueDate.Text.Trim();
        try
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_LastRABillNo = Convert.ToInt32(txtLastRABillNo.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_LastRABillNo = 0;
        }
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_LastRABillDate = txtLastRABillDate.Text.Trim();
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Agreement_No = txtAgreementNo.Text.Trim();
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_AgreementAmount = Convert.ToDecimal(txtAgreementAmount.Text.Trim());
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Code = txtProjectCode.Text.Trim();
        try
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Vendor_Id = Convert.ToInt32(ddlVendor.SelectedValue);
        }
        catch
        {

        }

        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_GST = rbtGSTType.SelectedValue;
        }
        catch
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_GST = "Exclude GST";
        }
        try
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Percent = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Percent = 12;
        }
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_ExtendDate = txtextenddate.Text;
        obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Status = 1;

        List<tbl_ProjectWorkPkg_ReportingStaff_JE_APE> obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li = new List<tbl_ProjectWorkPkg_ReportingStaff_JE_APE>();

        foreach (ListItem listItem in lbReportingStaffJEAPE.Items)
        {
            if (listItem.Selected)
            {
                tbl_ProjectWorkPkg_ReportingStaff_JE_APE obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE = new tbl_ProjectWorkPkg_ReportingStaff_JE_APE();
                obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE.ProjectWorkPkg_ReportingStaff_JE_APE_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE.ProjectWorkPkg_ReportingStaff_JE_APE_Person_Id = Convert.ToInt32(listItem.Value);
                obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE.ProjectWorkPkg_ReportingStaff_JE_APE_Status = 1;
                obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li.Add(obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE);
            }
        }

        List<tbl_ProjectWorkPkg_ReportingStaff_AE_PE> obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li = new List<tbl_ProjectWorkPkg_ReportingStaff_AE_PE>();
        foreach (ListItem listItem in lbReportingStaffAEPE.Items)
        {
            if (listItem.Selected)
            {
                tbl_ProjectWorkPkg_ReportingStaff_AE_PE obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE = new tbl_ProjectWorkPkg_ReportingStaff_AE_PE();
                obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE.ProjectWorkPkg_ReportingStaff_AE_PE_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE.ProjectWorkPkg_ReportingStaff_AE_PE_Person_Id = Convert.ToInt32(listItem.Value);
                obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE.ProjectWorkPkg_ReportingStaff_AE_PE_Status = 1;
                obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li.Add(obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE);
            }
        }
        if (Session["UserType"].ToString() != "1")
        {
            if (obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li == null || obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li.Count == 0)
            {
                MessageBox.Show("Please Provide Reporting Staff JE / APE!");
                return;
            }
            if (obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li == null || obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li.Count == 0)
            {
                MessageBox.Show("Please Provide Reporting Staff AE / PE!");
                return;
            }
        }
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWork_And_Pkg(obj_tbl_ProjectWork, obj_tbl_ProjectWorkFundingPattern_Li, extGO, obj_tbl_ProjectPkg_PhysicalProgress, obj_tbl_ProjectWorkPkg, obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li, obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li, obj_tbl_ProjectAdditionalArea_Li, ref Msg))
        {
            if (Msg == "")
            {
                MessageBox.Show("Project Created Successfully!");
                reset();
                return;
            }
            else
            {
                MessageBox.Show(Msg);
                return;
            }
        }
        else
        {
            MessageBox.Show("Error In Creating Project!");
            return;
        }
    }

    private void reset()
    {
        ddlDistrict.SelectedValue = "0";
        ddlULB.Items.Clear();
        hf_ProjectWork_Id.Value = "0";
        txtProjectWorkName.Text = "";
        txtBudget.Text = "0";
        txtGODate.Text = "";
        txtProjectCode.Text = "";
        txtProjectWorkName.Text = "";
        txtGONo.Text = "";
        hf_ProjectWorkPkg_Id.Value = "0";
        ViewState["AdditionalDivision"] = null;
        //dgvAdditionalDivision.DataSource = null;
        //dgvAdditionalDivision.DataBind();
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
        
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        GridViewRow gr = lnkUpdate.Parent.Parent as GridViewRow;
        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        hf_ProjectWorkPkg_Id.Value = gr.Cells[1].Text.Trim();
        int Division_Id = Convert.ToInt32(gr.Cells[5].Text.Trim());
        int District_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        int ULB_Id = Convert.ToInt32(gr.Cells[4].Text.Trim());
        get_Employee_Staff_JEAPE(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value), Division_Id);
        get_Employee_Staff_AEPE(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value), Division_Id);
        get_Employee_Vendor(Division_Id);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_And_Pkg_Edit(Convert.ToInt32(hf_ProjectWork_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlDistrict.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_DistrictId"].ToString();
            }
            catch
            {
                ddlDistrict.SelectedValue = "0";
            }

            ddlDistrict_SelectedIndexChanged(null, null);
            
            try
            {
                ddlULB.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_ULB_Id"].ToString();
            }
            catch
            {
                ddlULB.SelectedValue = "0";
            }
            try
            {
                ddlProjectMaster.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
            }
            catch
            {
                ddlProjectMaster.SelectedValue = "0";
            }
            txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString();
            txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            txtBudget.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            txtGONo.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
            txtGODate.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();

            try
            {
                ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["ProjectWorkPkg_Vendor_Id"].ToString();
            }
            catch
            {
                ddlVendor.SelectedValue = "0";
            }

            txtAgreementAmount.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_AgreementAmount"].ToString();
            txtAgreementNo.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_No"].ToString();
            txtAgreementDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_Date"].ToString();
            txtDueDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Due_Date"].ToString();
            txtLastRABillDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_LastRABillDate"].ToString();
            txtextenddate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_ExtendDate"].ToString();
            try
            {
                rbtGSTType.SelectedValue = ds.Tables[0].Rows[0]["ProjectWorkPkg_GST"].ToString();
            }
            catch
            {
                rbtGSTType.SelectedValue = "Include GST";
            }
            try
            {
                ddlGSTPercentage.SelectedValue = ds.Tables[0].Rows[0]["ProjectWorkPkg_Percent"].ToString();
            }
            catch
            {
                ddlGSTPercentage.SelectedValue = "12";
            }
            txtLastRABillNo.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_LastRABillNo"].ToString();

            string[] List_ReportingStaffJEAPE = ds.Tables[0].Rows[0]["List_ReportingStaff_JEAPE_Id"].ToString().Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (List_ReportingStaffJEAPE.Length > 0)
            {
                for (int i = 0; i < List_ReportingStaffJEAPE.Length; i++)
                {
                    foreach (ListItem listItem in lbReportingStaffJEAPE.Items)
                    {
                        if (List_ReportingStaffJEAPE[i].Trim() == listItem.Value)
                        {
                            listItem.Selected = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (ListItem listItem in lbReportingStaffJEAPE.Items)
                {
                    listItem.Selected = false;
                }
            }

            string[] List_ReportingStaffAEPE = ds.Tables[0].Rows[0]["List_ReportingStaff_AEPE_Id"].ToString().Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (List_ReportingStaffAEPE.Length > 0)
            {
                for (int i = 0; i < List_ReportingStaffAEPE.Length; i++)
                {
                    foreach (ListItem listItem in lbReportingStaffAEPE.Items)
                    {
                        if (List_ReportingStaffAEPE[i].Trim() == listItem.Value)
                        {
                            listItem.Selected = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (ListItem listItem in lbReportingStaffAEPE.Items)
                {
                    listItem.Selected = false;
                }
            }
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Person_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_ProjectWork(Person_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            get_tbl_ProjectWork();
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
}