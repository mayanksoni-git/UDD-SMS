using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWork_DataEntrySAR : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
    List<tbl_ProjectWorkPkgTemp> obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
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
            get_NodalDepartment();
            get_tbl_Zone();
            get_Employee_Vendor();
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
            int ProjectWork_Id = 0;
            get_tbl_ProjectIssue(Convert.ToInt32(Session["Default_Scheme"].ToString()));
            if (Request.QueryString.Count > 0)
            {
                ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                int Scheme_Id = Convert.ToInt32(Request.QueryString[1].ToString());
                Load_Project_Details(ProjectWork_Id);
                get_tbl_ProjectWorkGO(ProjectWork_Id);
                get_ProjectWork_Physical_Progress(ProjectWork_Id);
                get_tbl_ProjectWorkPkg_DataEntry_View(ProjectWork_Id);
                get_tbl_ProjectIssue(Scheme_Id);
                get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
                get_UC_Details(ProjectWork_Id);
            }
            else
            {
                ViewState["tbl_ProjectWorkPkgTemp"] = obj_tbl_ProjectWorkPkg_Li;
                grdPackageDetails.DataSource = obj_tbl_ProjectWorkPkg_Li;
                grdPackageDetails.DataBind();
                get_tbl_ProjectWorkIssueDetails(0);
                get_tbl_ProjectWorkGO_Blank();
                get_UC_Details(0);
                get_tbl_Project_Physical_Component();
            }
            get_tbl_ProjectWorkGallery_Top_2(ProjectWork_Id);
            get_tbl_PhysicalBaseLineData();
            mp1.Show();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
    }
    private void get_tbl_ProjectWorkGallery_Top_2(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGallery_Top_2(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            imgPhoto1.ImageUrl = ds.Tables[0].Rows[0]["ProjectWorkGallery_Path"].ToString();
            if (ds.Tables[0].Rows.Count > 1)
            {
                imgPhoto2.ImageUrl = ds.Tables[0].Rows[1]["ProjectWorkGallery_Path"].ToString();
            }
            else
            {
                imgPhoto2.ImageUrl = "assets\\images\\photo_not_available.png";
            }
        }
        else
        {
            imgPhoto1.ImageUrl = "assets\\images\\photo_not_available.png";
            imgPhoto2.ImageUrl = "assets\\images\\photo_not_available.png";
        }
    }
    private void get_tbl_ProjectWorkGO(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "S");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtQuestionnaire"] = ds.Tables[0];
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ProjectWorkGO_Id"] = 0;
                dr["ProjectWorkGO_CentralShare"] = 0;
                dr["ProjectWorkGO_StateShare"] = 0;
                dr["ProjectWorkGO_ULBShare"] = 0;
                dr["ProjectWorkGO_Centage"] = 0;
                dr["ProjectWorkGO_GO_Date"] = "";
                dr["ProjectWorkGO_GO_Number"] = "";
                dr["ProjectWorkGO_IssuingAuthority"] = "";
                dr["ProjectWorkGO_ULB_Id"] = 0;
                dr["ProjectWorkGO_Document_Path"] = "";
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtQuestionnaire"] = ds.Tables[0];
                grdCallProductDtls.DataSource = ds.Tables[0];
                grdCallProductDtls.DataBind();
            }
            catch
            {
                grdCallProductDtls.DataSource = null;
                grdCallProductDtls.DataBind();
            }
        }
    }
    private void get_tbl_ProjectWorkGO_Blank()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO_Blank();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtQuestionnaire"] = ds.Tables[0];
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ProjectWorkGO_Id"] = 0;
                dr["ProjectWorkGO_CentralShare"] = 0;
                dr["ProjectWorkGO_StateShare"] = 0;
                dr["ProjectWorkGO_ULBShare"] = 0;
                dr["ProjectWorkGO_Centage"] = 0;
                dr["ProjectWorkGO_GO_Date"] = "";
                dr["ProjectWorkGO_GO_Number"] = "";
                dr["ProjectWorkGO_IssuingAuthority"] = "";
                dr["ProjectWorkGO_ULB_Id"] = 0;
                dr["ProjectWorkGO_Document_Path"] = "";
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtQuestionnaire"] = ds.Tables[0];
                grdCallProductDtls.DataSource = ds.Tables[0];
                grdCallProductDtls.DataBind();
            }
            catch
            {
                grdCallProductDtls.DataSource = null;
                grdCallProductDtls.DataBind();
            }
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
    private void get_tbl_PhysicalBaseLineData()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PhysicalBaseLineData(0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdBaseLineData.DataSource = ds.Tables[0];
            grdBaseLineData.DataBind();
        }
        else
        {
            grdBaseLineData.DataSource = null;
            grdBaseLineData.DataBind();
        }
    }
    private void get_Employee_Vendor()
    {
        string UserTypeId = "5";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee(UserTypeId, 0, 0, 0, 0, 0, 0, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlVendor1, "Person_Name_Mobile_GST", "Person_Id");
        }
        else
        {
            ddlVendor1.Items.Clear();
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
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtProjectWorkName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Name");
            txtProjectWorkName.Focus();
            return;
        }
        if (ddlNodalDepartment.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Nodal Department");
            ddlNodalDepartment.Focus();
            return;
        }
        if (txtProjectCode.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Code");
            txtProjectCode.Focus();
            return;
        }
        if (txtGODate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide GO Date");
            txtGODate.Focus();
            return;
        }
        if (txtBudget.Text.Trim() == "" || txtBudget.Text.Trim() == "0")
        {
            MessageBox.Show("Please Provide Project Sanctioned Cost");
            txtBudget.Focus();
            return;
        }
        decimal physicalTarget = 0;
        try
        {
            physicalTarget = Convert.ToDecimal(txtPhysicalTarget.Text.Trim());
        }
        catch
        {
            physicalTarget = 0;
        }
        tbl_ProjectWork obj_tbl_ProjectWork = new tbl_ProjectWork();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = Convert.ToDecimal(txtBudget.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Budget_R = Convert.ToDecimal(txtBudgetR.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Budget_R = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = 0;
        }
        if (obj_tbl_ProjectWork.ProjectWork_Budget > obj_tbl_ProjectWork.ProjectWork_Budget_R)
        {
            if (obj_tbl_ProjectWork.ProjectWork_Centage > obj_tbl_ProjectWork.ProjectWork_Budget * Convert.ToDecimal("0.2"))
            {
                MessageBox.Show("Please Fill Centage Amount Properly.");
                txtCentage.Focus();
                return;
            }
        }
        if (obj_tbl_ProjectWork.ProjectWork_Budget_R > obj_tbl_ProjectWork.ProjectWork_Budget)
        {
            if (obj_tbl_ProjectWork.ProjectWork_Centage > obj_tbl_ProjectWork.ProjectWork_Budget_R * Convert.ToDecimal("0.2"))
            {
                MessageBox.Show("Please Fill Centage Amount Properly.");
                txtCentage.Focus();
                return;
            }
        }
        if (physicalTarget > 100)
        {
            MessageBox.Show("Physical Progress Can Not Be More Than 100%.");
            txtPhysicalTarget.Focus();
            return;
        }
        string Client = ConfigurationManager.AppSettings.Get("Client");

        decimal TotalRelease = 0;
        try
        {
            TotalRelease = Convert.ToDecimal(txtTotalReleseAmount.Text);
        }
        catch
        {
            TotalRelease = 0;
        }

        if (obj_tbl_ProjectWork.ProjectWork_Budget > obj_tbl_ProjectWork.ProjectWork_Budget_R)
        {
            if (TotalRelease > obj_tbl_ProjectWork.ProjectWork_Budget)
            {
                MessageBox.Show("Total Release Amount Can Not Be More Than Sanctioned Cost.");
                txtTotalReleseAmount.Focus();
                return;
            }
        }
        if (obj_tbl_ProjectWork.ProjectWork_Budget_R > obj_tbl_ProjectWork.ProjectWork_Budget)
        {
            if (TotalRelease > obj_tbl_ProjectWork.ProjectWork_Budget_R)
            {
                MessageBox.Show("Total Release Amount Can Not Be More Than Sanctioned Cost.");
                txtTotalReleseAmount.Focus();
                return;
            }
        }
        
        obj_tbl_ProjectWork.ProjectWork_GO_Date = txtGODate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_GO_No = txtGONo.Text.Trim();
        obj_tbl_ProjectWorkPkg_Li = (List<tbl_ProjectWorkPkgTemp>)ViewState["tbl_ProjectWorkPkgTemp"];
        tbl_ProjectWorkPkgTemp obj_tbl_ProjectWorkPkgTemp = new tbl_ProjectWorkPkgTemp();
        int Package_Id = 0;
        try
        {
            Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            Package_Id = 0;
        }
        if (Package_Id == 0 && obj_tbl_ProjectWorkPkg_Li != null && obj_tbl_ProjectWorkPkg_Li.Count == 1)
        {
            obj_tbl_ProjectWorkPkg_Li.RemoveAt(0);
        }
        if (Package_Id > 0 || obj_tbl_ProjectWorkPkg_Li == null || obj_tbl_ProjectWorkPkg_Li.Count == 0)
        {
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
            if (txtActualDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Actual Date of Start");
                txtActualDate.Focus();
                return;
            }
            if (txtDueDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Date of Completion As Per Agreement");
                txtDueDate.Focus();
                return;
            }
            if (txtextenddate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Origional Date of Completion");
                txtextenddate.Focus();
                return;
            }
            DateTime AgreementDate = new DateTime();
            DateTime ActualDate = new DateTime();
            DateTime DueDate = new DateTime();
            DateTime DueDateActual = new DateTime();
            try
            {
                AgreementDate = DateTime.ParseExact(txtAgreementDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            { }
            try
            {
                ActualDate = DateTime.ParseExact(txtActualDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            { }
            try
            {
                DueDate = DateTime.ParseExact(txtDueDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            { }
            try
            {
                DueDateActual = DateTime.ParseExact(txtextenddate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            { }
            if (ActualDate < AgreementDate)
            {
                MessageBox.Show("Actual Date Of Start Should Be More Than Agreement Date.");
                txtActualDate.Focus();
                return;
            }
            if (DueDate < AgreementDate)
            {
                MessageBox.Show("Actual Date as Per Agreement Should Be More Than Agreement Date.");
                txtDueDate.Focus();
                return;
            }
            if (DueDateActual < AgreementDate)
            {
                MessageBox.Show("Actual Completion Date Should Be More Than Agreement Date.");
                txtextenddate.Focus();
                return;
            }
            if (DueDateActual < ActualDate)
            {
                MessageBox.Show("Actual Completion Date Should Be More Than Actual Date Of Start.");
                txtextenddate.Focus();
                return;
            }
            //if (ActualDate < DueDate)
            //{
            //    MessageBox.Show("Actual Date as Per Agreement Should Be More Than Agreement Date.");
            //    txtDueDate.Focus();
            //    return;
            //}
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Work_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Work_Id = 0;
                }
            }
            else
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Work_Id = 0;
            }
            try
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Id = 0;
            }
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Agreement_Date = txtAgreementDate.Text.Trim();
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Due_Date = txtDueDate.Text.Trim();
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Agreement_No = txtAgreementNo.Text.Trim();
            try
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_AgreementAmount = Convert.ToDecimal(txtAgreementAmount.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_AgreementAmount = 0;
            }
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Start_Date = txtActualDate.Text.Trim();
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Name = txtPackageName.Text.Trim();
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Code = txtPackageCode.Text.Trim();
            try
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Vendor_Id = Convert.ToInt32(ddlVendor1.SelectedValue);
            }
            catch
            {

            }
            //try
            //{
            //    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Vendor_JV_Id = Convert.ToInt32(ddlVendor2.SelectedValue);
            //}
            //catch
            //{

            //}
            try
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_GST = rbtGSTType.SelectedValue;
            }
            catch
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_GST = "Include GST";
            }
            try
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Percent = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Percent = 18;
            }
            //try
            //{
            //    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_PreviousADP = Convert.ToDecimal(txtExpenditureADP.Text.Trim());
            //}
            //catch
            //{
            //    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_PreviousADP = 12;
            //}
            try
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_PreviousRA = Convert.ToDecimal(txtExpenditureRABill.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_PreviousRA = 0;
            }
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_ExtendDate = txtextenddate.Text;
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Lead_Vendor_PAN = txtLeadContractorPAN.Text.Replace("PAN:", "");
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Lead_Vendor_Name = txtLeadContractorName.Text;
            obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Status = 1;
            for (int i = 0; i < obj_tbl_ProjectWorkPkg_Li.Count; i++)
            {
                if (obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Id == obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Id)
                {
                    obj_tbl_ProjectWorkPkg_Li.RemoveAt(i);
                    break;
                }
            }
            obj_tbl_ProjectWorkPkg_Li.Add(obj_tbl_ProjectWorkPkgTemp);
        }
        if (obj_tbl_ProjectWorkPkg_Li == null || obj_tbl_ProjectWorkPkg_Li.Count == 0)
        {
            MessageBox.Show("Please Add At-Least One Agreement / Work Order Details First");
            return;
        }
        decimal Agreement_Cost = 0;
        decimal Expenditure_Total = 0;
        for (int i = 0; i < obj_tbl_ProjectWorkPkg_Li.Count; i++)
        {
            try
            {
                Agreement_Cost += obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_AgreementAmount;
            }
            catch
            {
                Agreement_Cost += 0;
            }
            try
            { 
                Expenditure_Total += obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_PreviousRA;
            }
            catch
            {
                Expenditure_Total += 0;
            }
        }
        if (obj_tbl_ProjectWork.ProjectWork_Budget > obj_tbl_ProjectWork.ProjectWork_Budget_R)
        {
            if (Agreement_Cost > obj_tbl_ProjectWork.ProjectWork_Budget)
            {
                MessageBox.Show("Total Agreement Cost Can Not Be More Than Sanctioned Cost.");
                txtAgreementAmount.Focus();
                return;
            }
        }
        if (obj_tbl_ProjectWork.ProjectWork_Budget_R > obj_tbl_ProjectWork.ProjectWork_Budget)
        {
            if (Agreement_Cost > obj_tbl_ProjectWork.ProjectWork_Budget_R)
            {
                MessageBox.Show("Total Agreement Cost Can Not Be More Than Sanctioned Cost.");
                txtAgreementAmount.Focus();
                return;
            }
        }
        if (Expenditure_Total > TotalRelease)
        {
            MessageBox.Show("Total Expenditure Can Not Be More Than Total Released Amount.");
            txtExpenditureRABill.Focus();
            return;
        }
        if (Request.QueryString.Count > 0)
        {
            try
            {
                obj_tbl_ProjectWork.ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].ToString());
            }
            catch
            {
                obj_tbl_ProjectWork.ProjectWork_Id = 0;
            }
        }
        else
        {
            obj_tbl_ProjectWork.ProjectWork_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_Contegencytext = 0;
        obj_tbl_ProjectWork.ProjectWork_Is_Phase_1 = 0;
        obj_tbl_ProjectWork.ProjectWork_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

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
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Budget_R = Convert.ToDecimal(txtBudgetR.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Budget_R = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = 0;
        }

        obj_tbl_ProjectWork.ProjectWork_Name = txtProjectWorkName.Text.Trim().Replace("'", "");
        obj_tbl_ProjectWork.ProjectWork_Description = obj_tbl_ProjectWork.ProjectWork_Name;
        obj_tbl_ProjectWork.ProjectWork_ProjectCode = txtProjectCode.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Project_Id = Convert.ToInt32(Session["Default_Scheme"].ToString());
        
        obj_tbl_ProjectWork.ProjectWork_DPR_Id = 0;
        obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = 0;
        
        obj_tbl_ProjectWork.ProjectWork_NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_BlockId = 0;
        try
        {
            obj_tbl_ProjectWork.ProjectWork_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_DivisionId = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_ULB_Id = 0;
        obj_tbl_ProjectWork.ProjectWork_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWork.ProjectWork_Status = 1;

        string FilePath = "";
        List<tbl_ProjectWorkGO> obj_tbl_ProjectWorkGO_Li = new List<tbl_ProjectWorkGO>();
        for (int i = 0; i < grdCallProductDtls.Rows.Count; i++)
        {
            decimal Allocated_Budget = 0;
            decimal CentralShare_Budget = 0;
            decimal StateShare_Budget = 0;
            decimal Centage_Budget = 0;
            decimal ULBShare_Budget = 0;

            TextBox txtFinancialTrans_GO_Date = grdCallProductDtls.Rows[i].FindControl("txtFinancialTrans_GO_Date") as TextBox;
            TextBox txtCentralShare = grdCallProductDtls.Rows[i].FindControl("txtCentralShare") as TextBox;
            TextBox txtStateShare = grdCallProductDtls.Rows[i].FindControl("txtStateShare") as TextBox;
            TextBox txtCentage = grdCallProductDtls.Rows[i].FindControl("txtCentage") as TextBox;
            TextBox txtULBShare = grdCallProductDtls.Rows[i].FindControl("txtULBShare") as TextBox;
            TextBox txtFinancialTrans_GO_Number = grdCallProductDtls.Rows[i].FindControl("txtFinancialTrans_GO_Number") as TextBox;
            FileUpload flUploadGO1 = grdCallProductDtls.Rows[i].FindControl("flUploadGO") as FileUpload;
            FilePath = grdCallProductDtls.Rows[i].Cells[9].Text.Trim();
            try
            {
                CentralShare_Budget = Convert.ToDecimal(txtCentralShare.Text);
            }
            catch
            {
                CentralShare_Budget = 0;
            }
            try
            {
                StateShare_Budget = Convert.ToDecimal(txtStateShare.Text);
            }
            catch
            {
                StateShare_Budget = 0;
            }
            try
            {
                Centage_Budget = Convert.ToDecimal(txtCentage.Text);
            }
            catch
            {
                Centage_Budget = 0;
            }
            try
            {
                ULBShare_Budget = Convert.ToDecimal(txtULBShare.Text);
            }
            catch
            {
                ULBShare_Budget = 0;
            }
            Allocated_Budget = CentralShare_Budget + StateShare_Budget + Centage_Budget + ULBShare_Budget;

            tbl_ProjectWorkGO obj_tbl_ProjectWorkGO = new tbl_ProjectWorkGO();
            try
            {
                obj_tbl_ProjectWorkGO.ProjectWorkGO_Id = Convert.ToInt32(grdCallProductDtls.Rows[i].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkGO.ProjectWorkGO_Id = 0;
            }
            obj_tbl_ProjectWorkGO.ProjectWorkGO_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Date = txtFinancialTrans_GO_Date.Text.Trim();
            obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Number = txtFinancialTrans_GO_Number.Text.Trim();
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    obj_tbl_ProjectWorkGO.ProjectWorkGO_Work_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                }
                catch
                {
                    obj_tbl_ProjectWorkGO.ProjectWorkGO_Work_Id = 0;
                }
            }
            else
            {
                obj_tbl_ProjectWorkGO.ProjectWorkGO_Work_Id = 0;
            }
            obj_tbl_ProjectWorkGO.ProjectWorkGO_Status = 1;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_TotalRelease = Allocated_Budget * 100000;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_CentralShare = CentralShare_Budget * 100000;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_StateShare = StateShare_Budget * 100000;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_ULBShare = ULBShare_Budget * 100000;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_EntryType = "S";
            obj_tbl_ProjectWorkGO.ProjectWorkGO_Centage = Centage_Budget * 100000;
            if (obj_tbl_ProjectWorkGO.ProjectWorkGO_TotalRelease + obj_tbl_ProjectWorkGO.ProjectWorkGO_CentralShare + obj_tbl_ProjectWorkGO.ProjectWorkGO_StateShare + obj_tbl_ProjectWorkGO.ProjectWorkGO_ULBShare + obj_tbl_ProjectWorkGO.ProjectWorkGO_Centage > 0)
            {
                if (txtFinancialTrans_GO_Date.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Installment GO Date");
                    txtFinancialTrans_GO_Date.Focus();
                    return;
                }
                if (txtFinancialTrans_GO_Number.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill GO No");
                    txtFinancialTrans_GO_Number.Focus();
                    return;
                }
                if (flUploadGO1.HasFile)
                {
                    obj_tbl_ProjectWorkGO.ProjectWorkGO_Document_Bytes = flUploadGO1.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectWorkGO.ProjectWorkGO_Document_Bytes = null;
                }
                obj_tbl_ProjectWorkGO_Li.Add(obj_tbl_ProjectWorkGO);
            }
        }

        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);
        for (int i = 0; i < obj_tbl_ProjectWorkIssueDetails_Li.Count; i++)
        {
            DropDownList ddlIssueType = grdIssue.Rows[i].FindControl("ddlIssueType") as DropDownList;
            DropDownList ddlDependency = grdIssue.Rows[i].FindControl("ddlDependency") as DropDownList;
            TextBox txtIssuingDate = grdIssue.Rows[i].FindControl("txtIssuingDate") as TextBox;
            //TextBox txtIssuingCategory = grdIssue.Rows[i].FindControl("txtIssuingCategory") as TextBox;
            TextBox txtComments = grdIssue.Rows[i].FindControl("txtComments") as TextBox;
            FileUpload flUploadIssue = grdIssue.Rows[i].FindControl("flUploadIssue") as FileUpload;
            FilePath = grdIssue.Rows[i].Cells[1].Text.Trim();
            if (!chkNoIssue.Checked && ddlIssueType.SelectedValue == "0")
            {
                MessageBox.Show("Please Select Issue. In Case Of No Issue Please Select No Issue Checkbox.");
                ddlIssueType.Focus();
                return;
            }
            if (!chkNoIssue.Checked && ddlDependency.SelectedValue == "0")
            {
                MessageBox.Show("Please Select Issue Dependency / Sub Issue");
                ddlDependency.Focus();
                return;
            }
            //tbl_ProjectWorkIssueDetails obj_tbl_ProjectWorkIssueDetails = new tbl_ProjectWorkIssueDetails();
            obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Date = txtIssuingDate.Text.Trim();

            obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Category = "";
            obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Comments = txtComments.Text.Trim();
            try
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Issue_Id = Convert.ToInt32(ddlIssueType.SelectedValue);
            }
            catch
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Issue_Id = 0;
            }
            try
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Dependency_Id = Convert.ToInt32(ddlDependency.SelectedValue);
            }
            catch
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Dependency_Id = 0;
            }
            obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Status = 1;
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_WorkId = Convert.ToInt32(Request.QueryString[0].ToString());
                }
                catch
                {
                    obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_WorkId = 0;
                }
            }
            else
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_WorkId = 0;
            }
            try
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Id = Convert.ToInt32(grdIssue.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Id = 0;
            }
            if (obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Issue_Id > 0)
            {
                try
                {
                    if (flUploadIssue.HasFile)
                    {
                        obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Path_Bytes = flUploadIssue.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Path = null;
                    }
                }
                catch
                {

                }
                //obj_tbl_ProjectWorkIssueDetails_Li.Add(obj_tbl_ProjectWorkIssueDetails);
            }
        }

        for (int i = 0; i < obj_tbl_ProjectWorkIssueDetails_Li.Count; i++)
        {
            if (obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Issue_Id == 0)
            { }
        }

        List<tbl_ProjectUC> obj_tbl_ProjectUC_Li = new List<tbl_ProjectUC>();
        for (int i = 0; i < grdUC.Rows.Count; i++)
        {
            TextBox txtUCDate = grdUC.Rows[i].FindControl("txtUCDate") as TextBox;
            TextBox txtUC_Number = grdUC.Rows[i].FindControl("txtUC_Number") as TextBox;
            TextBox txtUCP = grdUC.Rows[i].FindControl("txtUCP") as TextBox;
            FileUpload flUploadUC = grdUC.Rows[i].FindControl("flUploadUC") as FileUpload;
            FilePath = grdUC.Rows[i].Cells[1].Text.Trim().Replace("&nbsp;", "");

            tbl_ProjectUC obj_tbl_ProjectUC = new tbl_ProjectUC();
            obj_tbl_ProjectUC.ProjectUC_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectUC.ProjectUC_SubmitionDate = txtUCDate.Text.Trim();
            obj_tbl_ProjectUC.ProjectUC_Comments = txtUC_Number.Text.Trim();
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                }
                catch
                {
                    obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id = 0;
                }
            }
            else
            {
                obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id = 0;
            }
            
            obj_tbl_ProjectUC.ProjectUC_Status = 1;
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Id = Convert.ToInt32(grdUC.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectUC.ProjectUC_Id = 0;
            }
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Achivment = Convert.ToDecimal(txtUCP.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectUC.ProjectUC_Achivment = 0;
            }
            if (obj_tbl_ProjectUC.ProjectUC_Achivment > 0)
            {
                if (txtUCDate.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill UC Date");
                    return;
                }
                if (txtUC_Number.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill UC Number");
                    return;
                }
                if (FilePath.Replace("&nbsp;", "") == "")
                {
                    if (!flUploadUC.HasFile)
                    {
                        MessageBox.Show("Please Upload UC Document.");
                        return;
                    }
                }
                try
                {
                    if (flUploadUC.HasFile)
                    {
                        obj_tbl_ProjectUC.ProjectUC_Document_Bytes = flUploadUC.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectUC.ProjectUC_Document_Bytes = null;
                    }
                }
                catch
                {

                }
                obj_tbl_ProjectUC_Li.Add(obj_tbl_ProjectUC);
            }
        }

        try
        {
            physicalTarget = Convert.ToDecimal(txtPhysicalTarget.Text.Trim());
        }
        catch
        {
            physicalTarget = 0;
        }

        List<tbl_ProjectPkg_PhysicalProgress> obj_tbl_ProjectPkg_PhysicalProgress = new List<tbl_ProjectPkg_PhysicalProgress>();
        for (int i = 0; i < grdPhysicalProgress.Rows.Count; i++)
        {
            CheckBox checkBox = grdPhysicalProgress.Rows[i].FindControl("chkPostPhysicalProgress") as CheckBox;
            TextBox txtProposedNumber = grdPhysicalProgress.Rows[i].FindControl("txtProposedNumber") as TextBox;
            TextBox txtProgressNumber = grdPhysicalProgress.Rows[i].FindControl("txtProgressNumber") as TextBox;
            FileUpload flUploadReport = grdPhysicalProgress.Rows[i].FindControl("flUploadReport") as FileUpload;
            TextBox txtProposedNumberO = grdPhysicalProgress.Rows[i].FindControl("txtProposedNumberO") as TextBox;
            decimal ProposedNumber_Prev = 0;
            if (checkBox.Checked == true)
            {
                tbl_ProjectPkg_PhysicalProgress obj_tbl_ProjectPkg_PhysicalProgress1 = new tbl_ProjectPkg_PhysicalProgress();
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue = Convert.ToDecimal(txtProposedNumber.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue = 0;
                }
                try
                {
                    ProposedNumber_Prev = Convert.ToDecimal(txtProposedNumber.ToolTip.Trim());
                }
                catch
                {
                    ProposedNumber_Prev = 0;
                }
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF = Convert.ToDecimal(txtProposedNumberO.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF = 0;
                }
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value = Convert.ToDecimal(txtProgressNumber.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value = 0;
                }
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectUC_PhysicalProgress_WithheldProgress = 0;
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Functional = obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value;
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_NonFunctional = 0;
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Remarks = "";
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_PhysicalProgressComponent_Id = Convert.ToInt32(grdPhysicalProgress.Rows[i].Cells[0].Text.Trim());
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_PrjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_PrjectWork_Id = 0;
                }
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Status = 1;

                if (obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF == 0)
                {
                    MessageBox.Show("Proposed (Number) As Per Origional Should Not Be 0 at Sr No " + (i + 1).ToString());
                    return;
                }
                obj_tbl_ProjectPkg_PhysicalProgress.Add(obj_tbl_ProjectPkg_PhysicalProgress1);
            }
        }
        if (grdPhysicalProgress.Rows.Count > 0 && obj_tbl_ProjectPkg_PhysicalProgress.Count == 0)
        {
            MessageBox.Show("Please Fill Component Proposed Data");
            return;
        }
        Dictionary<string, byte[]> file_Upload_Array = new Dictionary<string, byte[]>();

        if (flPhotoUpload1.HasFile)
        {
            file_Upload_Array.Add(flPhotoUpload1.FileName, flPhotoUpload1.FileBytes);
        }
        if (flPhotoUpload2.HasFile)
        {
            file_Upload_Array.Add(flPhotoUpload2.FileName, flPhotoUpload2.FileBytes);
        }
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWork_Data_Entry(obj_tbl_ProjectWork, obj_tbl_ProjectWorkGO_Li, null, obj_tbl_ProjectWorkIssueDetails_Li, physicalTarget, obj_tbl_ProjectWorkPkg_Li, Client, obj_tbl_ProjectUC_Li, file_Upload_Array, obj_tbl_ProjectPkg_PhysicalProgress, null, "", ref Msg))
        {
            if (Msg == "")
            {
                MessageBox.Show("Project Created / Updated Successfully!");
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
        ddlNodalDepartment.SelectedValue = "0";
        txtProjectWorkName.Text = "";
        txtBudget.Text = "0";
        txtBudgetR.Text = "0";
        txtCentage.Text = "0";
        txtGODate.Text = "";
        txtGONo.Text = "";
        txtProjectCode.Text = "";
        txtProjectWorkName.Text = "";
        txtGONo.Text = "";
        txtPhysicalTarget.Text = "";
        txtTotalReleseAmount.Text = "";
        reset_Package();
        obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
        ViewState["tbl_ProjectWorkPkgTemp"] = obj_tbl_ProjectWorkPkg_Li;
        grdPackageDetails.DataSource = obj_tbl_ProjectWorkPkg_Li;
        grdPackageDetails.DataBind();
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
    protected void Load_Project_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Edit(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlNodalDepartment.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_NodalDepartment_Id"].ToString();
            }
            catch
            {
                ddlNodalDepartment.SelectedValue = "0";
            }
            
            try
            {
                ddlZone.SelectedValue = ds.Tables[0].Rows[0]["Circle_ZoneId"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }
            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
            try
            {
                ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Division_CircleId"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_DivisionId"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString();
            txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            txtBudget.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            txtBudgetR.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget_R"].ToString();
            txtCentage.Text = ds.Tables[0].Rows[0]["ProjectWork_Centage"].ToString();
            txtGONo.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
            txtGODate.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();
        }

        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[3];
            grdPhysicalProgress.DataBind();

            ViewState["Component"] = ds.Tables[3];
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
            ViewState["Component"] = null;
        }
    }
    protected void get_ProjectWork_Physical_Progress(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_ProjectWork_Physical_Progress(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtPhysicalTarget.Text = ds.Tables[0].Rows[0]["ProjectWorkPhysicalTarget_Target"].ToString();
        }
    }

    private void get_tbl_ProjectWorkPkg_DataEntry_View(int ProjectWork_Id)
    {
        obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_DataEntry_View(ProjectWork_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_ProjectWorkPkgTemp obj_tbl_ProjectWorkPkgTemp = new tbl_ProjectWorkPkgTemp();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_AgreementAmount = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkPkg_AgreementAmount"].ToString());
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Agreement_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Agreement_Date"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Agreement_No = ds.Tables[0].Rows[i]["ProjectWorkPkg_Agreement_No"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Code = ds.Tables[0].Rows[i]["ProjectWorkPkg_Code"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Due_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Due_Date"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_ExtendDate = ds.Tables[0].Rows[i]["ProjectWorkPkg_ExtendDate"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_GST = ds.Tables[0].Rows[i]["ProjectWorkPkg_GST"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkPkg_Id"].ToString());
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Name = ds.Tables[0].Rows[i]["ProjectWorkPkg_Name"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Percent = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkPkg_Percent"].ToString());
                try
                {
                    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Vendor_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkPkg_Vendor_Id"].ToString());
                }
                catch
                {
                    obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Vendor_Id = 0;
                }
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_PreviousRA = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkPkg_PreviousRA"].ToString());
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Start_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Start_Date"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Lead_Vendor_PAN = ds.Tables[0].Rows[i]["ProjectWorkPkg_Lead_Vendor_PAN"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Lead_Vendor_Name = ds.Tables[0].Rows[i]["ProjectWorkPkg_Lead_Vendor_Name"].ToString();
                obj_tbl_ProjectWorkPkgTemp.ProjectWorkPkg_Work_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkPkg_Work_Id"].ToString());
                obj_tbl_ProjectWorkPkg_Li.Add(obj_tbl_ProjectWorkPkgTemp);
            }
        }
        grdPackageDetails.DataSource = obj_tbl_ProjectWorkPkg_Li;
        grdPackageDetails.DataBind();
        ViewState["tbl_ProjectWorkPkgTemp"] = obj_tbl_ProjectWorkPkg_Li;
        if (obj_tbl_ProjectWorkPkg_Li != null && obj_tbl_ProjectWorkPkg_Li.Count > 0)
        {
            ImageButton btnPackageEdit = grdPackageDetails.Rows[0].FindControl("btnPackageEdit") as ImageButton;
            btnPackageEdit_Click(btnPackageEdit, new ImageClickEventArgs(0, 0));
        }
    }
    protected void grdCallProductDtls_PreRender(object sender, EventArgs e)
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
    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[9].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[1].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkULBShr");
                lnkBtn.Visible = false;
            }
        }
    }

    private void get_tbl_ProjectIssue(int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectIssue(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtProjectIssue"] = ds.Tables[0];
        }
        else
        {
            ViewState["dtProjectIssue"] = null;
        }
    }
    private void get_Dependency(int Project_Id, int Issue_Id, DropDownList ddlDependency)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Dependency(Project_Id, Issue_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDependency, "Dependency_Name", "Dependency_Id");
        }
        else
        {
            ddlDependency.Items.Clear();
        }
    }
    private void get_tbl_ProjectWorkIssueDetails(int ProjectWork_Id)
    {
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        obj_tbl_ProjectWorkIssueDetails_Li = (new DataLayer()).get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
        if (obj_tbl_ProjectWorkIssueDetails_Li != null && obj_tbl_ProjectWorkIssueDetails_Li.Count > 0)
        {
            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
        }
        else
        {
            tbl_ProjectWorkIssueDetails obj_tbl_ProjectWorkIssueDetails = new tbl_ProjectWorkIssueDetails();
            obj_tbl_ProjectWorkIssueDetails.ProjectWorkIssueDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkIssueDetails.ProjectWorkIssueDetails_Id = 0;

            obj_tbl_ProjectWorkIssueDetails_Li.Add(obj_tbl_ProjectWorkIssueDetails);

            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
        }
    }

    private void Add_Questionire_Issue()
    {
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        if (ViewState["dtIssue"] != null)
        {
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);

            tbl_ProjectWorkIssueDetails obj_tbl_ProjectWorkIssueDetails = new tbl_ProjectWorkIssueDetails();
            obj_tbl_ProjectWorkIssueDetails.ProjectWorkIssueDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkIssueDetails.ProjectWorkIssueDetails_Id = 0;

            obj_tbl_ProjectWorkIssueDetails_Li.Add(obj_tbl_ProjectWorkIssueDetails);
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;

            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
        }
        else
        {
            tbl_ProjectWorkIssueDetails obj_tbl_ProjectWorkIssueDetails = new tbl_ProjectWorkIssueDetails();
            obj_tbl_ProjectWorkIssueDetails.ProjectWorkIssueDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkIssueDetails.ProjectWorkIssueDetails_Id = 0;

            obj_tbl_ProjectWorkIssueDetails_Li.Add(obj_tbl_ProjectWorkIssueDetails);
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;

            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;

            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
        }
    }

    protected void grdIssue_PreRender(object sender, EventArgs e)
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

    protected void btnQuestionnaireU_Click(object sender, ImageClickEventArgs e)
    {
        Add_Questionire_Issue();
    }

    protected void btnDeleteQuestionnaireU_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtIssue"] != null)
        {
            List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)ViewState["dtIssue"];
            obj_tbl_ProjectWorkIssueDetails_Li.RemoveAt(gr.RowIndex);
            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
        }
    }

    protected void grdIssue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlIssueType = e.Row.FindControl("ddlIssueType") as DropDownList;

            DropDownList ddlDependency = e.Row.FindControl("ddlDependency") as DropDownList;

            DataTable dtProjectIssue = (DataTable)ViewState["dtProjectIssue"];
            if (AllClasses.CheckDt(dtProjectIssue))
            {
                AllClasses.FillDropDown(dtProjectIssue, ddlIssueType, "ProjectIssue_Name", "ProjectIssue_Id");
            }
            int ProjectWorkIssueDetails_Issue_Id = 0;
            try
            {
                ProjectWorkIssueDetails_Issue_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                ProjectWorkIssueDetails_Issue_Id = 0;
            }
            if (ProjectWorkIssueDetails_Issue_Id > 0)
            {
                try
                {
                    ddlIssueType.SelectedValue = ProjectWorkIssueDetails_Issue_Id.ToString();
                    ddlIssueType_SelectedIndexChanged(ddlIssueType, e);
                }
                catch
                {

                }
            }
            int ProjectWorkIssueDetails_Dependency_Id = 0;
            try
            {
                ProjectWorkIssueDetails_Dependency_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim());
            }
            catch
            {
                ProjectWorkIssueDetails_Dependency_Id = 0;
            }
            if (ProjectWorkIssueDetails_Dependency_Id > 0)
            {
                try
                {
                    ddlDependency.SelectedValue = ProjectWorkIssueDetails_Dependency_Id.ToString();
                }
                catch
                {

                }
            }
            string ProjectIssue_Document = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (ProjectIssue_Document != "")
            {
                e.Row.Cells[4].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkIssueDoc");
                lnkBtn.Visible = false;
            }
        }
    }

    protected void btnDeleteIssue_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkIssueDetails_Id = 0;
        try
        {
            ProjectWorkIssueDetails_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkIssueDetails_Id = 0;
        }
        if (ProjectWorkIssueDetails_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        TextBox txtResolvedDate = gr.FindControl("txtResolvedDate") as TextBox;
        if (txtResolvedDate.Text == "")
        {
            MessageBox.Show("Please Input Issue Resolved Date");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectWorkIssueDetails(ProjectWorkIssueDetails_Id, txtResolvedDate.Text, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
    protected void ddlIssueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlIssueType = sender as DropDownList;
        GridViewRow gr = ddlIssueType.Parent.Parent as GridViewRow;
        DropDownList ddlDependency = gr.FindControl("ddlDependency") as DropDownList;
        if (ddlIssueType.SelectedValue == "0")
        {
            ddlDependency.Items.Clear();
        }
        else
        {
            get_Dependency(Convert.ToInt32(Session["Default_Scheme"].ToString()), Convert.ToInt32(ddlIssueType.SelectedValue), ddlDependency);
        }
    }
    protected void btnAddAdditionalPackage_Click(object sender, EventArgs e)
    {
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
        if (txtAgreementDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Agreement Date");
            txtAgreementDate.Focus();
            return;
        }
        if (txtActualDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Actual Date of Start");
            txtActualDate.Focus();
            return;
        }
        if (txtDueDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Date of Completion As Per Agreement");
            txtDueDate.Focus();
            return;
        }
        if (txtextenddate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Origional Date of Completion");
            txtextenddate.Focus();
            return;
        }
        DateTime AgreementDate = DateTime.ParseExact(txtAgreementDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime ActualDate = DateTime.ParseExact(txtActualDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime DueDate = DateTime.ParseExact(txtDueDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime DueDateActual = DateTime.ParseExact(txtextenddate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if (ActualDate < AgreementDate)
        {
            MessageBox.Show("Actual Date Of Start Should Be More Than Agreement Date.");
            txtActualDate.Focus();
            return;
        }
        if (DueDate < AgreementDate)
        {
            MessageBox.Show("Actual Date as Per Agreement Should Be More Than Agreement Date.");
            txtDueDate.Focus();
            return;
        }
        if (DueDateActual < AgreementDate)
        {
            MessageBox.Show("Actual Completion Date Should Be More Than Agreement Date.");
            txtextenddate.Focus();
            return;
        }
        if (DueDateActual < ActualDate)
        {
            MessageBox.Show("Actual Completion Date Should Be More Than Actual Date Of Start.");
            txtextenddate.Focus();
            return;
        }
        tbl_ProjectWorkPkgTemp obj_tbl_ProjectWork = new tbl_ProjectWorkPkgTemp();
        if (Request.QueryString.Count > 0)
        {
            try
            {
                obj_tbl_ProjectWork.ProjectWorkPkg_Work_Id = Convert.ToInt32(Request.QueryString[0].ToString());
            }
            catch
            {
                obj_tbl_ProjectWork.ProjectWorkPkg_Work_Id = 0;
            }
        }
        else
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
        obj_tbl_ProjectWork.ProjectWorkPkg_Agreement_No = txtAgreementNo.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_AgreementAmount = Convert.ToDecimal(txtAgreementAmount.Text.Trim());
        obj_tbl_ProjectWork.ProjectWorkPkg_Start_Date = txtActualDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_Name = txtPackageName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWorkPkg_Code = txtPackageCode.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Vendor_Id = Convert.ToInt32(ddlVendor1.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Vendor_Id = 0;
        }
        //try
        //{
        //    obj_tbl_ProjectWork.ProjectWorkPkg_Vendor_JV_Id = Convert.ToInt32(ddlVendor2.SelectedValue);
        //}
        //catch
        //{

        //}
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_GST = rbtGSTType.SelectedValue;
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_GST = "Include GST";
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Percent = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_Percent = 12;
        }
        //try
        //{
        //    obj_tbl_ProjectWork.ProjectWorkPkg_PreviousADP = Convert.ToDecimal(txtExpenditureADP.Text.Trim());
        //}
        //catch
        //{
        //    obj_tbl_ProjectWork.ProjectWorkPkg_PreviousADP = 12;
        //}
        try
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_PreviousRA = Convert.ToDecimal(txtExpenditureRABill.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWorkPkg_PreviousRA = 0;
        }
        obj_tbl_ProjectWork.ProjectWorkPkg_Lead_Vendor_PAN = txtLeadContractorPAN.Text.Replace("PAN:", "");
        obj_tbl_ProjectWork.ProjectWorkPkg_Lead_Vendor_Name = txtLeadContractorName.Text;
        obj_tbl_ProjectWork.ProjectWorkPkg_ExtendDate = txtextenddate.Text;
        obj_tbl_ProjectWork.ProjectWorkPkg_Status = 1;
        obj_tbl_ProjectWorkPkg_Li = (List<tbl_ProjectWorkPkgTemp>)ViewState["tbl_ProjectWorkPkgTemp"];
        if (obj_tbl_ProjectWorkPkg_Li == null)
        {
            obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
        }

        for (int i = 0; i < obj_tbl_ProjectWorkPkg_Li.Count; i++)
        {
            if (obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Id == obj_tbl_ProjectWork.ProjectWorkPkg_Id)
            {
                obj_tbl_ProjectWorkPkg_Li.RemoveAt(i);
                break;
            }
        }
        obj_tbl_ProjectWorkPkg_Li.Add(obj_tbl_ProjectWork);
        ViewState["tbl_ProjectWorkPkgTemp"] = obj_tbl_ProjectWorkPkg_Li;

        grdPackageDetails.DataSource = obj_tbl_ProjectWorkPkg_Li;
        grdPackageDetails.DataBind();
        reset_Package();
    }
    private void reset_Package()
    {
        txtPackageCode.Text = "";
        txtPackageName.Text = "";
        txtAgreementNo.Text = "";
        txtAgreementAmount.Text = "";
        txtAgreementDate.Text = "";
        txtActualDate.Text = "";
        txtDueDate.Text = "";
        txtextenddate.Text = "";
        txtExpenditureRABill.Text = "";
        txtLeadContractorPAN.Text = "";
        txtLeadContractorName.Text = "";
        ddlVendor1.SelectedValue = "0";
        ddlVendor1_SelectedIndexChanged(ddlVendor1, new EventArgs());
        hf_ProjectWorkPkg_Id.Value = "0";
    }
    protected void grdPackageDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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

    protected void btnPackageDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnPackageDelete = sender as ImageButton;
        GridViewRow gr = btnPackageDelete.Parent.Parent as GridViewRow;
        int _Indx = gr.RowIndex;
        obj_tbl_ProjectWorkPkg_Li = (List<tbl_ProjectWorkPkgTemp>)ViewState["tbl_ProjectWorkPkgTemp"];
        if (obj_tbl_ProjectWorkPkg_Li == null || obj_tbl_ProjectWorkPkg_Li.Count == 0)
        {
            return;
        }
        else
        {
            obj_tbl_ProjectWorkPkg_Li.RemoveAt(_Indx);
            ViewState["tbl_ProjectWorkPkgTemp"] = obj_tbl_ProjectWorkPkg_Li;

            grdPackageDetails.DataSource = obj_tbl_ProjectWorkPkg_Li;
            grdPackageDetails.DataBind();
            hf_ProjectWorkPkg_Id.Value = "0";
        }
    }

    protected void btnPackageEdit_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < grdPackageDetails.Rows.Count; i++)
        {
            grdPackageDetails.Rows[i].BackColor = Color.Transparent;
        }

        ImageButton btnPackageEdit = sender as ImageButton;
        GridViewRow gr = btnPackageEdit.Parent.Parent as GridViewRow;
        gr.BackColor = Color.LightGreen;
        int ProjectWorkPkg_Id = 0;
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        obj_tbl_ProjectWorkPkg_Li = (List<tbl_ProjectWorkPkgTemp>)ViewState["tbl_ProjectWorkPkgTemp"];
        for (int i = 0; i < obj_tbl_ProjectWorkPkg_Li.Count; i++)
        {
            if (ProjectWorkPkg_Id == obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Id)
            {
                txtAgreementAmount.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_AgreementAmount.ToString();
                txtAgreementDate.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Agreement_Date.ToString();
                txtActualDate.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_AgreementAmount.ToString();
                txtActualDate.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Start_Date.ToString();
                txtDueDate.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Due_Date.ToString();
                txtextenddate.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_ExtendDate.ToString();
                txtLeadContractorPAN.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Lead_Vendor_PAN.Replace("PAN:", "");
                txtLeadContractorName.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Lead_Vendor_Name;
                txtExpenditureRABill.Text = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_PreviousRA.ToString();
                try
                {
                    rbtGSTType.SelectedValue = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_GST.ToString();
                }
                catch
                { }
                try
                {
                    ddlVendor1.SelectedValue = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Vendor_Id.ToString();
                }
                catch
                {
                    ddlVendor1.SelectedValue = "0";
                }
                ddlVendor1_SelectedIndexChanged(ddlVendor1, e);
                hf_ProjectWorkPkg_Id.Value = obj_tbl_ProjectWorkPkg_Li[i].ProjectWorkPkg_Id.ToString();
                break;
            }
        }
    }

    protected void ddlVendor1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendor1.SelectedValue == "" || ddlVendor1.SelectedValue == "0")
        {
            txtLeadContractorName.Text = "";
            txtLeadContractorPAN.Text = "";
        }
        else
        {
            string[] _Data = ddlVendor1.SelectedItem.Text.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
            if (_Data != null && _Data.Length > 0)
            {
                txtLeadContractorName.Text = _Data[0].Trim();
                if (_Data != null && _Data.Length > 1)
                {
                    try
                    {
                        txtLeadContractorPAN.Text = _Data[2].Trim().Replace("PAN:", "");
                    }
                    catch
                    {
                        txtLeadContractorPAN.Text = "";
                    }
                }
            }
            else
            {
                txtLeadContractorName.Text = "";
                txtLeadContractorPAN.Text = "";
            }
        }
    }

    protected void grdUC_PreRender(object sender, EventArgs e)
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

    protected void grdUC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectUC_Document = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (ProjectUC_Document != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkUCDoc");
                lnkBtn.Visible = false;
            }
        }
    }
    
    private void Add_UC()
    {
        DataTable dtUC;
        if (ViewState["dtUC"] != null)
        {
            dtUC = (DataTable)(ViewState["dtUC"]);
            DataRow dr = dtUC.NewRow();
            dtUC.Rows.Add(dr);
            ViewState["dtUC"] = dtUC;

            grdUC.DataSource = dtUC;
            grdUC.DataBind();
        }
        else
        {
            dtUC = new DataTable();

            DataColumn dc_Sr_No = new DataColumn("Sr_No", typeof(int));

            dtUC.Columns.AddRange(new DataColumn[] { dc_Sr_No });

            DataRow dr = dtUC.NewRow();
            dtUC.Rows.Add(dr);
            ViewState["dtUC"] = dtUC;

            grdUC.DataSource = dtUC;
            grdUC.DataBind();
        }
    }

    protected void btnAddUC_Click(object sender, ImageClickEventArgs e)
    {
        Add_UC();
    }

    protected void btnDeleteUC_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectUC_Id = 0;
        try
        {
            ProjectUC_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectUC_Id = 0;
        }
        if (ProjectUC_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectUC(ProjectUC_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            get_UC_Details(ProjectWork_Id);
            MessageBox.Show("Deleted Successfully .");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }

    private void get_UC_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectUC(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdUC.DataSource = ds.Tables[0];
            grdUC.DataBind();
            ViewState["dtUC"] = ds.Tables[0];
        }
        else
        {
            DataTable dt = new DataTable();
            DataColumn dc_1 = new DataColumn("ProjectUC_Id", typeof(int));
            DataColumn dc_2 = new DataColumn("ProjectUC_SubmitionDate", typeof(string));
            DataColumn dc_3 = new DataColumn("ProjectUC_Comments", typeof(string));
            DataColumn dc_4 = new DataColumn("ProjectUC_Achivment", typeof(decimal));
            DataColumn dc_5 = new DataColumn("ProjectUC_Document", typeof(string));
            dt.Columns.AddRange(new DataColumn[] { dc_1, dc_2, dc_3, dc_4, dc_5 });
            DataRow dr = dt.NewRow();
            dr["ProjectUC_Id"] = 0;
            dt.Rows.Add(dr);

            ViewState["dtUC"] = dt;
            grdUC.DataSource = dt;
            grdUC.DataBind();
        }
    }
    private void get_tbl_Project_Physical_Component()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Project_Physical_Component();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[0];
            grdPhysicalProgress.DataBind();
            ViewState["Component"] = ds.Tables[0];
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
            ViewState["Component"] = null;
        }
    }
    protected void imgdeleteUC_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtUC"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtUC"];
            if (dt.Rows.Count > 1)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
                grdUC.DataSource = dt;
                grdUC.DataBind();
                ViewState["dtUC"] = dt;
            }
        }
    }

    protected void grdPhysicalProgress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPostPhysicalProgress = (e.Row.FindControl("chkPostPhysicalProgress") as CheckBox);
            TextBox txtProgressNumber = (e.Row.FindControl("txtProgressNumber") as TextBox);
            txtProgressNumber.Enabled = true;
            if (Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "")) > 0)
            {
                chkPostPhysicalProgress.Checked = true;
            }
            else
            {
                chkPostPhysicalProgress.Checked = false;
            }

            TextBox txtProposedNumberO = e.Row.FindControl("txtProposedNumberO") as TextBox;
            decimal Praposed = 0;
            try
            {
                Praposed = decimal.Parse(txtProposedNumberO.Text.ToString().Trim());
            }
            catch
            {
                Praposed = 0;
            }
            if (Praposed == 0)
            {
                txtProposedNumberO.ReadOnly = false;
            }
            else
            {
                txtProposedNumberO.ReadOnly = true;
            }
            if (Session["UserType"].ToString() == "1")
            {
                txtProposedNumberO.ReadOnly = false;
            }
        }
    }

    protected void chkSelectAllApproveH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelectAllApproveH1 = (sender as CheckBox);
        for (int i = 0; i < grdPhysicalProgress.Rows.Count; i++)
        {
            CheckBox chkSelectAllApprove = grdPhysicalProgress.Rows[i].FindControl("chkPostPhysicalProgress") as CheckBox;
            chkSelectAllApprove.Checked = chkSelectAllApproveH1.Checked;
        }
    }
    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        Add_Questionire("S");
    }

    private void Add_Questionire(string Entry_Type)
    {
        DataTable dtQuestionnaire;
        if (Entry_Type != "U")
        {
            if (ViewState["dtQuestionnaire"] != null)
            {
                dtQuestionnaire = (DataTable)(ViewState["dtQuestionnaire"]);
                DataRow dr = dtQuestionnaire.NewRow();
                dtQuestionnaire.Rows.Add(dr);
                ViewState["dtQuestionnaire"] = dtQuestionnaire;

                grdCallProductDtls.DataSource = dtQuestionnaire;
                grdCallProductDtls.DataBind();
            }
            else
            {
                dtQuestionnaire = new DataTable();

                DataColumn dc_ProjectWorkGO_Id = new DataColumn("ProjectWorkGO_Id", typeof(int));
                DataColumn dc_ProjectWorkGO_GO_Date = new DataColumn("ProjectWorkGO_GO_Date", typeof(string));
                DataColumn dc_ProjectWorkGO_GO_Number = new DataColumn("ProjectWorkGO_GO_Number", typeof(string));
                DataColumn dc_ProjectWorkGO_CentralShare = new DataColumn("ProjectWorkGO_CentralShare", typeof(decimal));
                DataColumn dc_ProjectWorkGO_StateShare = new DataColumn("ProjectWorkGO_StateShare", typeof(decimal));
                DataColumn dc_ProjectWorkGO_ULBShare = new DataColumn("ProjectWorkGO_ULBShare", typeof(decimal));
                DataColumn dc_ProjectWorkGO_Centage = new DataColumn("ProjectWorkGO_Centage", typeof(decimal));
                DataColumn dc_ProjectWorkGO_IssuingAuthority = new DataColumn("ProjectWorkGO_IssuingAuthority", typeof(string));
                DataColumn dc_ProjectWorkGO_ULB_Id = new DataColumn("ProjectWorkGO_ULB_Id", typeof(int));
                DataColumn dc_ProjectWorkGO_Document_Path = new DataColumn("ProjectWorkGO_Document_Path", typeof(string));

                dtQuestionnaire.Columns.AddRange(new DataColumn[] { dc_ProjectWorkGO_Id, dc_ProjectWorkGO_GO_Date, dc_ProjectWorkGO_GO_Number, dc_ProjectWorkGO_CentralShare, dc_ProjectWorkGO_StateShare, dc_ProjectWorkGO_ULBShare, dc_ProjectWorkGO_Centage, dc_ProjectWorkGO_IssuingAuthority, dc_ProjectWorkGO_ULB_Id, dc_ProjectWorkGO_Document_Path });

                DataRow dr = dtQuestionnaire.NewRow();
                dtQuestionnaire.Rows.Add(dr);
                ViewState["dtQuestionnaire"] = dtQuestionnaire;

                grdCallProductDtls.DataSource = dtQuestionnaire;
                grdCallProductDtls.DataBind();

            }
        }
    }
    protected void btnDeleteGO_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkGO_Id = 0;
        try
        {
            ProjectWorkGO_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkGO_Id = 0;
        }
        if (ProjectWorkGO_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectGO(ProjectWorkGO_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            get_tbl_ProjectWorkGO(ProjectWork_Id);
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtQuestionnaire"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtQuestionnaire"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdCallProductDtls.DataSource = dt;
            grdCallProductDtls.DataBind();
            ViewState["dtQuestionnaire"] = dt;
        }
    }
    protected void grdBaseLineData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void chkSelectAllH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelectAllH = (sender as CheckBox);
        for (int i = 0; i < grdBaseLineData.Rows.Count; i++)
        {
            CheckBox chkPostBaseLineData = grdBaseLineData.Rows[i].FindControl("chkPostBaseLineData") as CheckBox;
            chkPostBaseLineData.Checked = chkSelectAllH.Checked;
        }
    }
}