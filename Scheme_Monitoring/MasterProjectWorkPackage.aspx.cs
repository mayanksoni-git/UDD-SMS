using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkPackage : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
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
            get_tbl_Project();
            get_tbl_Zone();
            get_tbl_Zone_Addditional();
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            lblZoneHA.Text = Session["Default_Zone"].ToString();
            lblCircleHA.Text = Session["Default_Circle"].ToString();
            lblDivisionHA.Text = Session["Default_Division"].ToString();

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
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
    }
    private void get_Employee_Vendor(int Division_Id)
    {
        string UserTypeId = "5";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee(UserTypeId, 0, 0, 0, 0, 0, 0, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlVendor1, "Person_Name_Mobile_GST", "Person_Id");
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlVendor2, "Person_Name_Mobile_GST", "Person_Id");
        }
        else
        {
            ddlVendor1.Items.Clear();
            ddlVendor2.Items.Clear();
        }
    }

    private void get_tbl_Zone_Addditional()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlAdditionZone.Items.Clear();
        }
    }
    private void get_Employee_Staff_JEAPE(int Package_Id, int Division_Id)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }

        int Project_Id = 0;
        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
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
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        int Project_Id = 0;
        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtProjectWorkName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Name");
            txtProjectWorkName.Focus();
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
        tbl_ProjectWorkPkg obj_tbl_ProjectWork = new tbl_ProjectWorkPkg();
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Work_Id = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWorkPkg_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        obj_tbl_ProjectWork.ProjectWorkPkg_Agreement_Date = txtAgreementDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_Due_Date = txtDueDate.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_LastRABillNo = Convert.ToInt32(txtLastRABillNo.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_LastRABillNo = 0;
        }
        obj_tbl_ProjectWork.ProjectWorkPkg_LastRABillDate = txtLastRABillDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_Agreement_No = txtAgreementNo.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_AgreementAmount = Convert.ToDecimal(txtAgreementAmount.Text.Trim());
        obj_tbl_ProjectWork.ProjectWorkPkg_Start_Date = txtActualDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_Code = txtPackageCode.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Vendor_Id = Convert.ToInt32(ddlVendor1.SelectedValue);
        }
        catch
        {

        }
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Vendor_JV_Id = Convert.ToInt32(ddlVendor2.SelectedValue);
        }
        catch
        {

        }
        obj_tbl_ProjectWork.ProjectWorkPkg_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_GST = rbtGSTType.SelectedValue;
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_GST = "Exclude GST";
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Percent = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Percent = 12;
        }
        obj_tbl_ProjectWork.ProjectWorkPkg_ExtendDate = txtextenddate.Text;
        obj_tbl_ProjectWork.ProjectWorkPkg_Status = 1;

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
        //if (Session["UserType"].ToString() != "1")
        //{
        //    if (obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li == null || obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li.Count == 0)
        //    {
        //        MessageBox.Show("Please Provide Reporting Staff JE / APE!");
        //        return;
        //    }
        //    if (obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li == null || obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li.Count == 0)
        //    {
        //        MessageBox.Show("Please Provide Reporting Staff AE / PE!");
        //        return;
        //    }
        //}

        List<tbl_ProjectAdditionalArea> obj_tbl_ProjectAdditionalArea_Li = new List<tbl_ProjectAdditionalArea>();
        for (int i = 0; i < dgvAdditionalDivision.Rows.Count; i++)
        {
            tbl_ProjectAdditionalArea obj_tbl_ProjectAdditionalArea = new tbl_ProjectAdditionalArea();
            obj_tbl_ProjectAdditionalArea.ProjectAdditionalArea_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectAdditionalArea.ProjectAdditionalArea_ZoneId = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[0].Text.Trim());
            obj_tbl_ProjectAdditionalArea.ProjectAdditionalArea_CircleId = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[1].Text.Trim());
            obj_tbl_ProjectAdditionalArea.ProjectAdditionalArea_DevisionId = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[2].Text.Trim());
            obj_tbl_ProjectAdditionalArea.ProjectAdditionalArea_Status = 1;
            obj_tbl_ProjectAdditionalArea_Li.Add(obj_tbl_ProjectAdditionalArea);
        }
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWorkPkg(obj_tbl_ProjectWork, obj_tbl_ProjectWorkPkg_ReportingStaff_JE_APE_Li, obj_tbl_ProjectWorkPkg_ReportingStaff_AE_PE_Li, obj_tbl_ProjectAdditionalArea_Li, ref Msg))
        {
            if (Msg == "")
            {
                MessageBox.Show("Project Package Created Successfully!");
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
            MessageBox.Show("Error In Creating Project Package!");
            return;
        }
    }

    private void reset()
    {
        //ddlDistrict.SelectedValue = "0";
        ddlULB.Items.Clear();
        hf_ProjectWork_Id.Value = "0";
        hf_ProjectWorkPkg_Id.Value = "0";
        txtProjectWorkName.Text = "";
        ViewState["AdditionalDivision"] = null;
        dgvAdditionalDivision.DataSource = null;
        dgvAdditionalDivision.DataBind();
        divEntry.Visible = false;
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
            MessageBox.Show("Please Select A " + Session["Default_Zone"].ToString() + "");
            return;
        }
        string Project_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Project_Id = ddlSearchScheme.SelectedValue;
        }
        catch
        {
            Project_Id = "";
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
        ds = (new DataLayer()).get_tbl_ProjectWork(Project_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", 0, "", 0);
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divEntry.Visible = true;
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        int Division_Id = Convert.ToInt32(gr.Cells[4].Text.Trim());
        get_Employee_Staff_JEAPE(0, Division_Id);
        get_Employee_Staff_AEPE(0, Division_Id);
        get_Employee_Vendor(Division_Id);
        ViewState["AdditionalDivision"] = null;
        dgvAdditionalDivision.DataSource = null;
        dgvAdditionalDivision.DataBind();
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Work_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(Work_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();
            mp1.Show();
        }
        else
        {
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
            MessageBox.Show("Package Details Not Found");
        }
        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        int Division_Id = Convert.ToInt32(gr.Cells[4].Text.Trim());
        get_Employee_Staff_JEAPE(0, Division_Id);
        get_Employee_Staff_AEPE(0, Division_Id);
        get_Employee_Vendor(Division_Id);
    }

    protected void btnPackageEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["UserType"].ToString() == "1" || obj_tbl_ePaymentModules.ePaymentModules_EnableEditing_Package == 1)
        {
            ImageButton lnkUpdate = sender as ImageButton;
            hf_ProjectWorkPkg_Id.Value = (lnkUpdate.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
            int Division_Id = 0;
            try
            {
                Division_Id = Convert.ToInt32((lnkUpdate.Parent.Parent as GridViewRow).Cells[15].Text.Trim());
            }
            catch
            { }
            get_Employee_Staff_JEAPE(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value), Division_Id);
            get_Employee_Staff_AEPE(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value), Division_Id);
            DataSet ds = new DataSet();
            ds = (new DataLayer()).CheckPackageApproval(hf_ProjectWorkPkg_Id.Value);
            //if (AllClasses.CheckDataSet(ds) || Session["UserType"].ToString() == "1")
            //{
            ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Edit(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    ddlVendor1.SelectedValue = ds.Tables[0].Rows[0]["ProjectWorkPkg_Vendor_Id"].ToString();
                }
                catch
                {
                    ddlVendor1.SelectedValue = "0";
                }
                try
                {
                    ddlVendor2.SelectedValue = ds.Tables[0].Rows[0]["ProjectWorkPkg_Vendor_JV_Id"].ToString();
                }
                catch
                {
                    ddlVendor2.SelectedValue = "0";
                }
                txtPackageCode.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Code"].ToString();
                txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Name"].ToString();
                txtAgreementAmount.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_AgreementAmount"].ToString();
                txtAgreementNo.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_No"].ToString();
                txtAgreementDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_Date"].ToString();
                txtActualDate.Text = ds.Tables[0].Rows[0]["ProjectWorkPkg_Start_Date"].ToString();
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
                divEntry.Visible = true;
                divEntry.Focus();
            }

            if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
            {
                ViewState["AdditionalDivision"] = ds.Tables[3];
                dgvAdditionalDivision.DataSource = ds.Tables[3];
                dgvAdditionalDivision.DataBind();
            }
            else
            {
                ViewState["AdditionalDivision"] = null;
                dgvAdditionalDivision.DataSource = null;
                dgvAdditionalDivision.DataBind();
            }
        }
        else
        {
            MessageBox.Show("You Are Not Allowed To Edit The Details. Please Contact Administrator..");
            return;
        }
    }

    protected void btnPackageDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Person_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_tbl_ProjectWorkPkg(Person_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void grdPackageDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = e.Row.FindControl("btnPackageDelete") as ImageButton;
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
    protected void ddlAdditionZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdditionZone.SelectedValue == "0")
        {
            ddlAdditionalCircle.Items.Clear();
            ddlAdditionalDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Additional(Convert.ToInt32(ddlAdditionZone.SelectedValue));
        }
    }

    protected void ddlAdditionalCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdditionalCircle.SelectedValue == "0")
        {
            ddlAdditionalDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Additional(Convert.ToInt32(ddlAdditionalCircle.SelectedValue));
        }
    }
    private void get_tbl_Circle_Additional(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionalCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlAdditionalCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Additional(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionalDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlAdditionalDivision.Items.Clear();
        }
    }

    protected void btnAddAdditionalDivision_Click(object sender, EventArgs e)
    {
        if (ddlAdditionZone.SelectedValue == "0" || ddlAdditionZone.SelectedValue == "")
        {
            MessageBox.Show("Please Select " + Session["Default_Zone"].ToString() + "");
            ddlAdditionZone.Focus();
            return;
        }
        if (ddlAdditionalCircle.SelectedValue == "0" || ddlAdditionalCircle.SelectedValue == "")
        {
            MessageBox.Show("Please Select Circle");
            ddlAdditionalCircle.Focus();
            return;
        }
        if (ddlAdditionalDivision.SelectedValue == "0" || ddlAdditionalDivision.SelectedValue == "")
        {
            MessageBox.Show("Please Select Divison");
            ddlAdditionalDivision.Focus();
            return;
        }

        if (ViewState["AdditionalDivision"] != null)
        {
            DataTable dt = (DataTable)ViewState["AdditionalDivision"];

            DataRow dr = dt.NewRow();

            dr["ProjectAdditionalArea_ZoneId"] = ddlAdditionZone.SelectedValue;
            dr["ProjectAdditionalArea_CircleId"] = ddlAdditionalCircle.SelectedValue;
            dr["ProjectAdditionalArea_DevisionId"] = ddlAdditionalDivision.SelectedValue;
            dr["Zone_Name"] = ddlAdditionZone.SelectedItem.Text.Trim();
            dr["Circle_Name"] = ddlAdditionalCircle.SelectedItem.Text.Trim();
            dr["Division_Name"] = ddlAdditionalDivision.SelectedItem.Text.Trim();
            dt.Rows.Add(dr);

            ViewState["AdditionalDivision"] = dt;

            dgvAdditionalDivision.DataSource = dt;
            dgvAdditionalDivision.DataBind();
            ddlAdditionZone.SelectedValue = "0";
            ddlAdditionalCircle.Items.Clear();
            ddlAdditionalDivision.Items.Clear();
        }
        else
        {
            DataSet ds = (new DataLayer()).get_tbl_ProjectWork_Edit(0);
            try
            {
                ViewState["AdditionalDivision"] = ds.Tables[2];
            }
            catch
            {
                ViewState["AdditionalDivision"] = null;
            }
        }
    }

    protected void lnkDeleteAdditionalDivision_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["AdditionalDivision"] != null)
        {
            if (index >= 0)
            {
                DataTable dt = (DataTable)ViewState["AdditionalDivision"];
                dt.Rows.RemoveAt(index);
                dgvAdditionalDivision.DataSource = dt;
                dgvAdditionalDivision.DataBind();
                ViewState["AdditionalDivision"] = dt;
            }
        }
    }

    protected void btnAddBOQItem_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkUpdate = sender as ImageButton;
            string DivisionId = (lnkUpdate.Parent.Parent as GridViewRow).Cells[2].Text.Trim();
            DataSet ds = new DataSet();
            ds = (new DataLayer()).CheckPackageBOQ(hf_ProjectWorkPkg_Id.Value);
            if (AllClasses.CheckDataSet(ds))
            {
                Response.Redirect("MasterProjectWorkPackageAddBOQItemDivisionWise.aspx?ProjectWorkPkg_Id=" + hf_ProjectWorkPkg_Id.Value.Trim() + "&DivisionId=" + DivisionId);
            }
            else
            {
                MessageBox.Show("Please Add BOQ Items!");
                return;
            }
        }
        catch
        {

        }
    }

    protected void dgvAdditionalDivision_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[5].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[6].Text = Session["Default_Division"].ToString();
        }
    }
}