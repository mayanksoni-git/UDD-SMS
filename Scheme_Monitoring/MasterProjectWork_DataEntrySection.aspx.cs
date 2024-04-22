using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWork_DataEntrySection : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Zone();
            get_tbl_Project();
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
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                int Scheme_Id = Convert.ToInt32(Request.QueryString[1].ToString());
                Load_Project_Details(ProjectWork_Id);
                get_tbl_ProjectWorkGO(ProjectWork_Id);
                get_ProjectWork_Physical_Progress(ProjectWork_Id);
                get_UC_Details(ProjectWork_Id);
            }
            else
            {
                get_tbl_ProjectWorkGO_Blank();
                get_UC_Details(0);
                get_tbl_FundingPattern();
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
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

    private void get_tbl_LokSabha(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_LokSabha(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlLokSabha, "LokSabha_Name", "LokSabha_Id");
        }
        else
        {
            ddlLokSabha.Items.Clear();
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
                ddlProjectMaster.SelectedValue = Session["Default_Scheme"].ToString();
                ddlProjectMaster_SelectedIndexChanged(ddlProjectMaster, new EventArgs());
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtProjectWorkName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Name");
            txtProjectWorkName.Focus();
            return;
        }
        if (txtGODate2.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide GO Date");
            txtGODate2.Focus();
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
        if (physicalTarget > 100)
        {
            MessageBox.Show("Physical Progress Can Not Be More Than 100%.");
            txtPhysicalTarget.Focus();
            return;
        }
        string Client = ConfigurationManager.AppSettings.Get("Client");

        decimal TotalRelease = 0;
        for (int i = 0; i < grdCallProductDtls.Rows.Count; i++)
        {
            decimal Allocated_Budget = 0;
            decimal CentralShare_Budget = 0;
            decimal StateShare_Budget = 0;
            decimal Centage_Budget = 0;
            decimal ULBShare_Budget = 0;

            TextBox txtGODate = grdCallProductDtls.Rows[i].FindControl("txtFinancialTrans_GO_Date") as TextBox;
            TextBox txtCentralShare = grdCallProductDtls.Rows[i].FindControl("txtCentralShare") as TextBox;
            TextBox txtStateShare = grdCallProductDtls.Rows[i].FindControl("txtStateShare") as TextBox;
            TextBox txtCentage = grdCallProductDtls.Rows[i].FindControl("txtCentage") as TextBox;
            TextBox txtULBShare = grdCallProductDtls.Rows[i].FindControl("txtULBShare") as TextBox;
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
            TotalRelease += Allocated_Budget;
        }
        if (obj_tbl_ProjectWork.ProjectWork_Budget > obj_tbl_ProjectWork.ProjectWork_Budget_R)
        {
            if (TotalRelease > obj_tbl_ProjectWork.ProjectWork_Budget)
            {
                MessageBox.Show("Total Release Amount Can Not Be More Than Sanctioned Cost.");
                return;
            }
        }
        
        obj_tbl_ProjectWork.ProjectWork_GO_Date = txtGODate2.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_GO_No = txtGONo.Text.Trim();
                
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
        string extGO = "";
        if (flUploadGO.HasFile)
        {
            obj_tbl_ProjectWork.ProjectWork_GO_Path_Bytes = flUploadGO.FileBytes;
            string[] _fname = flUploadGO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            extGO = _fname[_fname.Length - 1];
        }
        obj_tbl_ProjectWork.ProjectWork_GO_Date = txtGODate2.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_GO_No = txtGONo.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = Convert.ToDecimal(txtBudget.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_Budget_R = 0;
        obj_tbl_ProjectWork.ProjectWork_Centage = 0;

        obj_tbl_ProjectWork.ProjectWork_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Description = obj_tbl_ProjectWork.ProjectWork_Name;
        obj_tbl_ProjectWork.ProjectWork_ProjectCode = "";
        obj_tbl_ProjectWork.ProjectWork_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_NodalDepartment_Id = 0;
        obj_tbl_ProjectWork.ProjectWork_NodalDeptScheme_Id = 0;

        obj_tbl_ProjectWork.ProjectWork_DPR_Id = 0;
        try
        {
            obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_DistrictId = 0;
        obj_tbl_ProjectWork.ProjectWork_BlockId = 0;
        try
        {
            obj_tbl_ProjectWork.ProjectWork_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_DivisionId = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_VidhanSabha_Id = Convert.ToInt32(ddlVidhanSabha.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_VidhanSabha_Id = 0;
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

            TextBox txtGODate = grdCallProductDtls.Rows[i].FindControl("txtFinancialTrans_GO_Date") as TextBox;
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
            obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Date = txtGODate.Text.Trim();
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
                if (txtGODate.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill GO Date");
                    return;
                }
                if (txtFinancialTrans_GO_Number.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill GO No");
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

        List<tbl_ProjectWorkFundingPattern> obj_tbl_ProjectWorkFundingPattern_Li = new List<tbl_ProjectWorkFundingPattern>();

        for (int i = 0; i < grdFundingPattern.Rows.Count; i++)
        {
            tbl_ProjectWorkFundingPattern obj_tbl_ProjectWorkFundingPattern = new tbl_ProjectWorkFundingPattern();
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_FundingPatternId = Convert.ToInt32(grdFundingPattern.Rows[i].Cells[0].Text.ToString());
            try
            {
                obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Percentage = Convert.ToDecimal((grdFundingPattern.Rows[i].FindControl("txtShareP") as TextBox).Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Percentage = 0;
            }
            try
            {
                obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Value = Convert.ToDecimal((grdFundingPattern.Rows[i].FindControl("txtShareV") as TextBox).Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Value = 0;
            }
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_ProjectWorkId = obj_tbl_ProjectWork.ProjectWork_Id;
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Status = 1;
            if (obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Value + obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Percentage > 0)
            {
                obj_tbl_ProjectWorkFundingPattern_Li.Add(obj_tbl_ProjectWorkFundingPattern);
            }
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
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWork_Data_Entry(obj_tbl_ProjectWork, obj_tbl_ProjectWorkGO_Li, obj_tbl_ProjectWorkFundingPattern_Li, null, physicalTarget, null, Client, obj_tbl_ProjectUC_Li, null, null, null, extGO, ref Msg))
        {
            if (Msg == "")
            {
                MessageBox.Show("Project Created / Updated Successfully!");
                Response.Redirect("MasterProjectWorkDataEntry.aspx");
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
            ddlLokSabha.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
            get_tbl_LokSabha(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }
    private void get_tbl_ProjectType(int ProjectId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectType(ProjectId, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProjectType, "ProjectType_Name", "ProjectType_Id");
        }
        else
        {
            ddlProjectType.Items.Clear();
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

            try
            {
                ddlLokSabha.SelectedValue = ds.Tables[0].Rows[0]["VidhanSabha_LokSabhaId"].ToString();
            }
            catch
            {
                ddlLokSabha.SelectedValue = "0";
            }
            ddlLokSabha_SelectedIndexChanged(ddlLokSabha, new EventArgs());
            try
            {
                ddlVidhanSabha.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_VidhanSabha_Id"].ToString();
            }
            catch
            {
                ddlVidhanSabha.SelectedValue = "0";
            }
            try
            {
                ddlProjectMaster.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
            }
            catch
            {
                ddlProjectMaster.SelectedValue = "0";
            }
            try
            {
                ddlProjectType.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_ProjectType_Id"].ToString();
            }
            catch
            {
                ddlProjectType.SelectedValue = "0";
            }
            txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            txtBudget.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            txtGONo.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
            txtGODate2.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();
            hf_GO_Path.Value = ds.Tables[0].Rows[0]["ProjectWork_GO_Path"].ToString();
            if (hf_GO_Path.Value == "")
            {
                aGO.Visible = false;
            }
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            grdFundingPattern.DataSource = ds.Tables[1];
            grdFundingPattern.DataBind();
        }
        else
        {
            grdFundingPattern.DataSource = null;
            grdFundingPattern.DataBind();
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
                e.Row.Cells[1].BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkULBShr");
                lnkBtn.Visible = false;
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
    
    protected void ddlLokSabha_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLokSabha.SelectedValue == "0")
        {
            ddlVidhanSabha.Items.Clear();
        }
        else
        {
            get_tbl_VidhanSabha(Convert.ToInt32(ddlLokSabha.SelectedValue));
        }
    }

    private void get_tbl_VidhanSabha(int LokSabha_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_VidhanSabha(LokSabha_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlVidhanSabha, "VidhanSabha_Name", "VidhanSabha_Id");
        }
        else
        {
            ddlVidhanSabha.Items.Clear();
        }
    }

    protected void ddlProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            ddlProjectType.Items.Clear();
        }
        else
        {
            int ProjectId = 0;
            try
            {
                ProjectId = Convert.ToInt32(ddlProjectMaster.SelectedValue);
            }
            catch
            {
                ProjectId = 0;
            }
            get_tbl_ProjectType(ProjectId);
        }
    }
    protected void grdFundingPattern_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void get_tbl_FundingPattern()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FundingPattern();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdFundingPattern.DataSource = ds.Tables[0];
            grdFundingPattern.DataBind();
        }
        else
        {
            grdFundingPattern.DataSource = null;
            grdFundingPattern.DataBind();
        }
    }
    protected void grdFundingPattern_PreRender(object sender, EventArgs e)
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
                e.Row.Cells[2].BackColor = System.Drawing.Color.LightGreen;
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

    protected void btnAction_Click(object sender, ImageClickEventArgs e)
    {
        mp1.Show();
    }
    protected void btnUpdateAction_Click(object sender, EventArgs e)
    {

    }
}