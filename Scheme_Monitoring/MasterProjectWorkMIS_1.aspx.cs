using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_1 : System.Web.UI.Page
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

            get_M_Jurisdiction();
            get_tbl_Zone();
            get_tbl_FundingPattern();
            get_tbl_ProjectType(0);
            get_tbl_Project();
            get_NodalDepartment();
            if (Session["PersonJuridiction_DesignationId"].ToString() == "1" || Session["PersonJuridiction_DesignationId"].ToString() == "33")
            {
                btnSkip.Visible = true;
            }
            else if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "4" || Session["UserType"].ToString() == "6" || Session["UserType"].ToString() == "8")
            {
                btnSkip.Visible = true;
            }
            else
            {
                btnSkip.Visible = false;
            }
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
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                load_Project(ProjectWork_Id);
                get_Package_Wise_Details(ProjectWork_Id);
                get_tbl_ProjectWorkNGT(ProjectWork_Id);
                calculate_total_cost();
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
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

    private void get_tbl_ProjectWorkNGT(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkNGT(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtNGT"] = ds.Tables[0];
            grdNGTDtls.DataSource = ds.Tables[0];
            grdNGTDtls.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ProjectWorkNGT_Id"] = 0;
                dr["ProjectWorkNGT_OA_No"] = "";
                dr["ProjectWorkNGT_CaseNo"] = "";
                dr["ProjectWorkNGT_Status"] = 1;
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtNGT"] = ds.Tables[0];
                grdNGTDtls.DataSource = ds.Tables[0];
                grdNGTDtls.DataBind();
            }
            catch
            {
                grdNGTDtls.DataSource = null;
                grdNGTDtls.DataBind();
            }
        }
    }
    protected void load_Project(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Edit(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_Scheme_Id.Value = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
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
                ddlULBP.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_ULB_Id"].ToString();
            }
            catch
            {
                ddlULBP.SelectedValue = "0";
            }
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
                ddlZone.SelectedValue = ds.Tables[0].Rows[0]["Circle_ZoneId"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }
            ddlZone_SelectedIndexChanged(null, null);
            try
            {
                ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Division_CircleId"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            ddlCircle_SelectedIndexChanged(null, null);
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
                ddlProjectMaster.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
            }
            catch
            {
                ddlProjectMaster.SelectedValue = "0";
            }
            ddlProjectMaster_SelectedIndexChanged(null, null);
            try
            {
                ddlProjectType.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_ProjectType_Id"].ToString();
            }
            catch
            {
                ddlProjectType.SelectedValue = "0";
            }
            try
            {
                ddlNodalDepartment.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_NodalDepartment_Id"].ToString();
            }
            catch
            {
                ddlNodalDepartment.SelectedValue = "0";
            }
            txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString();
            txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            txtProjectWorkDesc.Text = ds.Tables[0].Rows[0]["ProjectWork_Description"].ToString();
            txtBudget.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            txtADPCost.Text = ds.Tables[0].Rows[0]["ProjectWork_ADPCost"].ToString();
            txtCentage.Text = ds.Tables[0].Rows[0]["ProjectWork_Centage"].ToString();
            txtGONo.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
            txtGODate.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();
            txtContegencytext.Text = ds.Tables[0].Rows[0]["ProjectWork_Contegencytext"].ToString();
            txtStartDate.Text = ds.Tables[0].Rows[0]["ProjectWork_StartDate"].ToString();
            txtCompleteDate.Text = ds.Tables[0].Rows[0]["ProjectWork_EndDate"].ToString();
            if (ds.Tables[0].Rows[0]["ProjectWork_UnderNGT"].ToString() == "1")
                chkNGT.SelectedValue = "1";
            else if (ds.Tables[0].Rows[0]["ProjectWork_UnderNGT"].ToString() == "2")
                chkNGT.SelectedValue = "2";
            else if (ds.Tables[0].Rows[0]["ProjectWork_UnderNGT"].ToString() == "3")
                chkNGT.SelectedValue = "3";
            else
            {
                chkNGT.Items[0].Selected = false;
                chkNGT.Items[1].Selected = false;
                chkNGT.Items[2].Selected = false;
            }
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

            grdFundingPattern.FooterRow.Cells[2].Text = "Work Cost (In Lakhs): ";
            grdFundingPattern.FooterRow.Cells[3].Text = ds.Tables[1].Compute("sum(ProjectWorkFundingPattern_Value)", "").ToString();
        }
        else
        {
            get_tbl_FundingPattern();
        }
    }
    protected void btnDownload_GO_Click(object sender, ImageClickEventArgs e)
    {
        FileInfo fl = new FileInfo(Server.MapPath(".") + hf_GO_Path.Value);
        Response.Write("<script>window.open('fl','_blank')</script>");
        Response.Redirect(fl.ToString());
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

    private void get_tbl_ProjectType(int ProjectId)
    {

        if (ProjectId > 0)
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

    private void get_tbl_FundingPattern()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FundingPattern();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdFundingPattern.DataSource = ds.Tables[0];
            grdFundingPattern.DataBind();

            grdFundingPattern.FooterRow.Cells[2].Text = "Work Cost (In Lakhs): ";
            grdFundingPattern.FooterRow.Cells[3].Text = "0.00";
        }
        else
        {
            grdFundingPattern.DataSource = null;
            grdFundingPattern.DataBind();
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
        if (txtStartDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Start Date");
            txtStartDate.Focus();
            return;
        }
        if (txtCompleteDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Completion Date");
            txtCompleteDate.Focus();
            return;
        }
        if (hf_GO_Path.Value.Replace("&nbsp;", "") == "")
        {
            if (!flUploadGO.HasFiles)
            {
                MessageBox.Show("Please Upload GO");
                txtGONo.Focus();
                return;
            }
        }
        if (txtCentage.Text.Trim() == "" || Convert.ToDecimal(txtCentage.Text.Trim().Replace("0.00", "0")) == 0)
        {
            MessageBox.Show("Please Provide Centage Amount");
            txtCentage.Focus();
            return;
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
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Contegencytext = Convert.ToDecimal(txtContegencytext.Text);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Contegencytext = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_ADPCost = Convert.ToDecimal(txtADPCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ADPCost = 0;
        }
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
        decimal Funding_Pattern_Total = 0;
        try
        {
            Funding_Pattern_Total = Convert.ToDecimal(grdFundingPattern.FooterRow.Cells[3].Text);
        }
        catch
        {
            Funding_Pattern_Total = 0;
        }
        //if (obj_tbl_ProjectWork.ProjectWork_Budget != Funding_Pattern_Total || Convert.ToInt32(obj_tbl_ProjectWork.ProjectWork_Budget) == 0.00)
        //{
        //    MessageBox.Show("Budget Sanctioned is not Equal to Funding Pattern Breakup Total");
        //    txtBudget.Focus();
        //    return;
        //}
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_NodalDepartment_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Description = txtProjectWorkDesc.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_ProjectCode = txtProjectCode.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_StartDate = txtStartDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_EndDate = txtCompleteDate.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_UnderNGT = Convert.ToInt32(chkNGT.SelectedItem.Value);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_UnderNGT = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        try
        {
            obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = 0;
        }
        string extGO = "";
        if (flUploadGO.HasFile)
        {
            obj_tbl_ProjectWork.ProjectWork_GO_Path_Bytes = flUploadGO.FileBytes;
            string[] _fname = flUploadGO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            extGO = _fname[_fname.Length - 1];
        }
        obj_tbl_ProjectWork.ProjectWork_DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_BlockId = 0;
        try
        {
            obj_tbl_ProjectWork.ProjectWork_ULB_Id = Convert.ToInt32(ddlULBP.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ULB_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWork.ProjectWork_Status = 1;
        List<tbl_ProjectWorkFundingPattern> obj_tbl_ProjectWorkFundingPattern_Li = new List<tbl_ProjectWorkFundingPattern>();

        for (int i = 0; i < grdFundingPattern.Rows.Count; i++)
        {
            tbl_ProjectWorkFundingPattern obj_tbl_ProjectWorkFundingPattern = new tbl_ProjectWorkFundingPattern();
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_FundingPatternId = Convert.ToInt32(grdFundingPattern.Rows[i].Cells[0].Text.ToString());
            obj_tbl_ProjectWorkFundingPattern.ProjectWorkFundingPattern_Percentage = 0;
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

        List<tbl_ProjectWorkPkg> obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkg>();
        for (int i = 0; i < grdPackageDetails.Rows.Count; i++)
        {
            tbl_ProjectWorkPkg obj_tbl_ProjectWorkPkg = new tbl_ProjectWorkPkg();
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Id = Convert.ToInt32(grdPackageDetails.Rows[i].Cells[0].Text.ToString());
            DropDownList ddlGSTPercentage = grdPackageDetails.Rows[i].FindControl("ddlGSTPercentage") as DropDownList;
            RadioButtonList rbtGSTType = grdPackageDetails.Rows[i].FindControl("rbtGSTType") as RadioButtonList;
            TextBox txtextenddate = grdPackageDetails.Rows[i].FindControl("txtextenddate") as TextBox;
            CheckBox chkPhysicalCompleted = grdPackageDetails.Rows[i].FindControl("chkPhysicalCompleted") as CheckBox;
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_GST = rbtGSTType.SelectedValue;
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Percent = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_ExtendDate = txtextenddate.Text;
            if (chkPhysicalCompleted.Checked)
                obj_tbl_ProjectWorkPkg.ProjectWorkPkg_PhysicallyCompleted = 1;
            else
                obj_tbl_ProjectWorkPkg.ProjectWorkPkg_PhysicallyCompleted = 0;
            obj_tbl_ProjectWorkPkg_Li.Add(obj_tbl_ProjectWorkPkg);
        }
        List<tbl_ProjectWorkNGT> obj_tbl_ProjectWorkNGT_Li = new List<tbl_ProjectWorkNGT>();
        for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
        {
            TextBox txtOANo = grdNGTDtls.Rows[i].FindControl("txtOANo") as TextBox;
            TextBox txtCaseNo = grdNGTDtls.Rows[i].FindControl("txtCaseNo") as TextBox;
            tbl_ProjectWorkNGT obj_tbl_ProjectWorkNGT = new tbl_ProjectWorkNGT();
            obj_tbl_ProjectWorkNGT.ProjectWorkNGT_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkNGT.ProjectWorkNGT_CaseNo = txtCaseNo.Text.Trim();
            obj_tbl_ProjectWorkNGT.ProjectWorkNGT_OA_No = txtOANo.Text.Trim();
            obj_tbl_ProjectWorkNGT.ProjectWorkNGT_Status = 1;
            obj_tbl_ProjectWorkNGT.ProjectWorkNGT_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_ProjectWorkNGT_Li.Add(obj_tbl_ProjectWorkNGT);
        }
        string msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWork_MIS(obj_tbl_ProjectWork, obj_tbl_ProjectWorkFundingPattern_Li, obj_tbl_ProjectWorkPkg_Li, extGO, null, obj_tbl_ProjectWorkNGT_Li, ref msg))
        {
            if (hf_Scheme_Id.Value == "12")
            {
                Response.Redirect("MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
            else if (hf_Scheme_Id.Value == "3")
            {
                Response.Redirect("MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
            else if (hf_Scheme_Id.Value == "1015")
            {
                Response.Redirect("MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
            else
            {
                Response.Redirect("MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
            ddlULBP.Items.Clear();
        }
        else
        {
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue), ddlULB);
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue), ddlULBP);
        }
    }

    private void get_tbl_ULB(int District_Id, DropDownList ddlULB)
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

    private void get_Share_Details(int District_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Share_Details(District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_CS.Value = ds.Tables[0].Rows[0]["Central_Share"].ToString();
            hf_SS.Value = ds.Tables[0].Rows[0]["State_Share"].ToString();
            hf_US.Value = ds.Tables[0].Rows[0]["ULB_Share"].ToString();
        }
        else
        {
            hf_CS.Value = "0";
            hf_SS.Value = "0";
            hf_US.Value = "0";
        }
    }
    protected void ddlProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            txtProjectWorkName.Text = "";
            ddlProjectType.Items.Clear();
        }
        else
        {
            txtProjectWorkName.Text = ddlProjectMaster.SelectedItem.Text.Trim();
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

    protected void get_Package_Wise_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();

            grdPackageDetails.FooterRow.Cells[11].Text = "Tender Cost: ";
            grdPackageDetails.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(ProjectWorkPkg_AgreementAmount)", "").ToString();
        }
        else
        {
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
        }
    }

    protected void txtShareV_TextChanged(object sender, EventArgs e)
    {
        decimal Work_Cost_Total = 0;
        for (int i = 0; i < grdFundingPattern.Rows.Count; i++)
        {
            try
            {
                Work_Cost_Total += Convert.ToDecimal((grdFundingPattern.Rows[i].FindControl("txtShareV") as TextBox).Text.Trim());
            }
            catch
            {
                Work_Cost_Total += 0;
            }
        }
        grdFundingPattern.FooterRow.Cells[3].Text = Work_Cost_Total.ToString();
        calculate_total_cost();
    }

    protected void txtBudget_TextChanged(object sender, EventArgs e)
    {
        decimal total_budget = 0;
        try
        {
            total_budget = Convert.ToDecimal(txtBudget.Text.Trim());
        }
        catch
        {
            total_budget = 0;
        }

        for (int i = 0; i < grdFundingPattern.Rows.Count; i++)
        {
            decimal CS = Convert.ToDecimal(hf_CS.Value);
            decimal SS = Convert.ToDecimal(hf_SS.Value);
            decimal US = Convert.ToDecimal(hf_US.Value);

            int FundingPattern_Id = Convert.ToInt32(grdFundingPattern.Rows[i].Cells[0].Text.Trim());
            TextBox txtShareV = grdFundingPattern.Rows[i].FindControl("txtShareV") as TextBox;
            if (FundingPattern_Id == 1)
            {
                txtShareV.Text = decimal.Round((total_budget * CS) / 100, 2, MidpointRounding.AwayFromZero).ToString();
            }
            if (FundingPattern_Id == 2)
            {
                txtShareV.Text = decimal.Round((total_budget * SS) / 100, 2, MidpointRounding.AwayFromZero).ToString();
            }
            if (FundingPattern_Id == 3)
            {
                txtShareV.Text = decimal.Round((total_budget * US) / 100, 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

        decimal Work_Cost_Total = 0;
        for (int i = 0; i < grdFundingPattern.Rows.Count; i++)
        {
            try
            {
                Work_Cost_Total += Convert.ToDecimal((grdFundingPattern.Rows[i].FindControl("txtShareV") as TextBox).Text.Trim());
            }
            catch
            {
                Work_Cost_Total += 0;
            }
        }
        grdFundingPattern.FooterRow.Cells[3].Text = Work_Cost_Total.ToString();

        calculate_total_cost();
    }

    private void calculate_total_cost()
    {
        decimal total_budget = 0;
        try
        {
            total_budget = Convert.ToDecimal(txtBudget.Text.Trim());
        }
        catch
        {
            total_budget = 0;
        }

        decimal centage = 0;
        try
        {
            centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            centage = 0;
        }

        decimal Contegency = 0;
        try
        {
            Contegency = Convert.ToDecimal(txtContegencytext.Text.Trim());
        }
        catch
        {
            Contegency = 0;
        }

        decimal Funding_Pattern_Total = 0;
        try
        {
            Funding_Pattern_Total = Convert.ToDecimal(grdFundingPattern.FooterRow.Cells[3].Text);
        }
        catch
        {
            Funding_Pattern_Total = 0;
        }
        txtWorkCost_Centage.Text = (total_budget + centage + Contegency).ToString();
    }

    protected void grdFundingPattern_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtShareV = e.Row.FindControl("txtShareV") as TextBox;
            int FundingPattern_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            if (FundingPattern_Id == 1 || FundingPattern_Id == 2 || FundingPattern_Id == 3)
            {
                //txtShareV.ReadOnly = true;
            }
        }
    }

    protected void txtCentage_TextChanged(object sender, EventArgs e)
    {
        calculate_total_cost();
    }

    protected void txtContegencytext_TextChanged(object sender, EventArgs e)
    {
        calculate_total_cost();
    }

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        if (hf_Scheme_Id.Value == "12")
        {
            Response.Redirect("MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
        else if (hf_Scheme_Id.Value == "3")
        {
            Response.Redirect("MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
        else if (hf_Scheme_Id.Value == "1015")
        {
            Response.Redirect("MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
        else
        {
            Response.Redirect("MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
    }

    protected void grdPackageDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string GST = e.Row.Cells[2].Text.Trim();
            string GST_Per = e.Row.Cells[3].Text.Trim();
            CheckBox chkPhysicalCompleted = e.Row.FindControl("chkPhysicalCompleted") as CheckBox;
            RadioButtonList rbtGSTType = e.Row.FindControl("rbtGSTType") as RadioButtonList;
            DropDownList ddlGSTPercentage = e.Row.FindControl("ddlGSTPercentage") as DropDownList;
            try
            {
                rbtGSTType.SelectedValue = GST;
            }
            catch
            {
                rbtGSTType.SelectedValue = "Include GST";
            }
            try
            {
                ddlGSTPercentage.SelectedValue = GST_Per;
            }
            catch
            {
                ddlGSTPercentage.SelectedValue = "12";
            }
            int PhysicallyCompleted = 0;
            try
            {
                PhysicallyCompleted = Convert.ToInt32(e.Row.Cells[8].Text);
            }
            catch
            {
                PhysicallyCompleted = 0;
            }
            if (PhysicallyCompleted > 0)
            {
                chkPhysicalCompleted.Checked = true;
            }
            else
            {
                chkPhysicalCompleted.Checked = false;
            }
        }
    }

    protected void grdNGTDtls_PreRender(object sender, EventArgs e)
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

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtNGT;
        if (ViewState["dtNGT"] != null)
        {
            dtNGT = (DataTable)(ViewState["dtNGT"]);
            DataRow dr = dtNGT.NewRow();
            dtNGT.Rows.Add(dr);
            ViewState["dtNGT"] = dtNGT;

            grdNGTDtls.DataSource = dtNGT;
            grdNGTDtls.DataBind();
        }
        else
        {
            dtNGT = new DataTable();

            DataColumn dc_ProjectWorkNGT_Id = new DataColumn("ProjectWorkNGT_Id", typeof(int));
            DataColumn dc_ProjectWorkNGT_OA_No = new DataColumn("ProjectWorkNGT_OA_No", typeof(string));
            DataColumn dc_ProjectWorkNGT_CaseNo = new DataColumn("ProjectWorkNGT_CaseNo", typeof(string));
            DataColumn dc_ProjectWorkNGT_Status = new DataColumn("ProjectWorkNGT_Status", typeof(int));

            dtNGT.Columns.AddRange(new DataColumn[] { dc_ProjectWorkNGT_Id, dc_ProjectWorkNGT_OA_No, dc_ProjectWorkNGT_CaseNo, dc_ProjectWorkNGT_Status });

            DataRow dr = dtNGT.NewRow();
            dtNGT.Rows.Add(dr);
            ViewState["dtNGT"] = dtNGT;

            grdNGTDtls.DataSource = dtNGT;
            grdNGTDtls.DataBind();
        }
    }

    protected void btnMinus_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtNGT"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtNGT"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdNGTDtls.DataSource = dt;
            grdNGTDtls.DataBind();
            ViewState["dtNGT"] = dt;
        }
    }

    protected void chkNGT_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNGT.SelectedItem.Value == "1")
        {
            divNGT.Visible = true;
        }
        else
        {
            divNGT.Visible = false; 
        }
    }
}