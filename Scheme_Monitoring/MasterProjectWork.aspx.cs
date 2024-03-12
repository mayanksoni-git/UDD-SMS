using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWork : System.Web.UI.Page
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

            lblZoneHS.Text = Session["Default_Zone"].ToString();
            lblCircleHS.Text = Session["Default_Circle"].ToString();
            lblDivisionHS.Text = Session["Default_Division"].ToString();

            get_NodalDepartment();
            get_M_Jurisdiction();
            get_tbl_Zone();
            //get_tbl_Zone_Addditional();
            get_tbl_FundingPattern();
            get_tbl_ProjectType(0);
            get_tbl_Project();
            get_tbl_Program();
            get_tbl_ProjectWorkNGT(0);
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

            get_tbl_ProjectWork();
        }
        if (Session["UserType"].ToString() == "1")
        {
            divCreate.Visible = true;
        }
        else if (Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
        {
            divCreate.Visible = true;
        }
        else
        {
            divCreate.Visible = false;
        }
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
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
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        //if (ddlSearchZone.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Zone");
        //    return;
        //}
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Project_Id = "";

        try
        {
            foreach (ListItem listItem in ddlSearchScheme.Items)
            {
                if (listItem.Selected)
                {
                    Project_Id += listItem.Value + ", ";
                }
            }
            Project_Id = Project_Id.Trim().Substring(0, Project_Id.Trim().Length - 1);
        }
        catch
        {
            Project_Id = "";
        }
        try
        {
            District_Id = 0;
            //District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork(Project_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", 0, "", 0);
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
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlSearchZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }

    private void get_tbl_Program()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Program();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProgram, "Program_Name", "Program_Id");
        }
        else
        {
            ddlProgram.Items.Clear();
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
    //private void get_tbl_Zone_Addditional()
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_Zone();
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        AllClasses.FillDropDown(ds.Tables[0], ddlAdditionZone, "Zone_Name", "Zone_Id");
    //    }
    //    else
    //    {
    //        ddlAdditionZone.Items.Clear();
    //    }
    //}
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
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            bool is_Selected = false;
            ddlSearchScheme.DataTextField = "Project_Name";
            ddlSearchScheme.DataValueField = "Project_Id";
            ddlSearchScheme.DataSource = ds.Tables[0];
            ddlSearchScheme.DataBind();
            try
            {
                foreach (ListItem listItem in ddlSearchScheme.Items)
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
                ddlSearchScheme.Items[0].Selected = true;
            }
            if (is_Selected == false)
            {
                ddlSearchScheme.Items[0].Selected = true;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
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
            obj_tbl_ProjectWork.ProjectWork_Is_Phase_1 = Convert.ToInt32(ddlProgram.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Is_Phase_1 = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        string extGO = "";
        if (flUploadGO.HasFile)
        {
            obj_tbl_ProjectWork.ProjectWork_GO_Path_Bytes = flUploadGO.FileBytes;
            string[] _fname = flUploadGO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            extGO = _fname[_fname.Length - 1];
        }
        else
        {
            obj_tbl_ProjectWork.ProjectWork_GO_Path_Bytes = null;
            extGO = "";
        }
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
            obj_tbl_ProjectWork.ProjectWork_ADPCost = Convert.ToDecimal(txtADPCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ADPCost = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = 0;
        }

        obj_tbl_ProjectWork.ProjectWork_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Description = txtProjectWorkDesc.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_ProjectCode = txtProjectCode.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        try
        {
            obj_tbl_ProjectWork.ProjectWork_NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_NodalDepartment_Id = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_DPR_Id = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ProjectType_Id = 0;
        }
        if (chkNGT.Checked)
        {
            obj_tbl_ProjectWork.ProjectWork_UnderNGT = 1;
        }
        else
        {
            obj_tbl_ProjectWork.ProjectWork_UnderNGT = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_SAAPYear = txtSAAPYear.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
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
            obj_tbl_ProjectWork.ProjectWork_ULB_Id = Convert.ToInt32(ddlULBP.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_ULB_Id = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWork.ProjectWork_Status = 1;
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

        List<tbl_ProjectAdditionalArea> obj_tbl_ProjectAdditionalArea_Li = new List<tbl_ProjectAdditionalArea>();
        
        List<tbl_ProjectPkg_PhysicalProgress> obj_tbl_ProjectPkg_PhysicalProgress = new List<tbl_ProjectPkg_PhysicalProgress>();
        
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
        string Msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWork(obj_tbl_ProjectWork, obj_tbl_ProjectWorkFundingPattern_Li, extGO, obj_tbl_ProjectPkg_PhysicalProgress, obj_tbl_ProjectWorkNGT_Li, ref Msg))
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
        ddlULBP.Items.Clear();
        hf_ProjectWork_Id.Value = "0";
        txtProjectWorkName.Text = "";
        txtContegencytext.Text = "0";
        //ddlProjectMaster.SelectedValue = "0";
        //get_tbl_ProjectWork();
        txtBudget.Text = "0";
        txtCentage.Text = "0";
        txtGODate.Text = "";
        txtProjectCode.Text = "";
        txtProjectWorkDesc.Text = "";
        txtProjectWorkName.Text = "";
        txtSAAPYear.Text = "";
        chkNGT.Checked = false;
        txtGONo.Text = "";
        ddlProgram.SelectedValue = "0";
        get_tbl_FundingPattern();
        ViewState["AdditionalDivision"] = null;
        //dgvAdditionalDivision.DataSource = null;
        //dgvAdditionalDivision.DataBind();
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
    protected void ddlProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            txtProjectWorkName.Text = "";
            ddlProgram.Items.Clear();
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

    protected void rbtMappingWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtMappingWith.SelectedValue == "D")
        {
            lblULB.Visible = false;
            ddlULB.Visible = false;
            divCircle.Visible = true;
            divDivision.Visible = true;

            lblZoneH.Visible = true;
            ddlZone.Visible = true;
        }
        else
        {
            lblZoneH.Visible = false;
            ddlZone.Visible = false;
            divCircle.Visible = false;
            divDivision.Visible = false;

            lblULB.Visible = true;
            ddlULB.Visible = true;
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

    protected void txtShareP_TextChanged(object sender, EventArgs e)
    {

    }

    protected void grdFundingPattern_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["UserType"].ToString() == "1" || obj_tbl_ePaymentModules.ePaymentModules_EnableEditing_Project == 1)
        {
            ImageButton lnkUpdate = sender as ImageButton;
            hf_ProjectWork_Id.Value = (lnkUpdate.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_ProjectWork_Edit(Convert.ToInt32(hf_ProjectWork_Id.Value));
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
                    hf_ProjectDPR_Id.Value = ds.Tables[0].Rows[0]["ProjectWork_DPR_Id"].ToString();
                }
                catch
                {
                    hf_ProjectDPR_Id.Value = "0";
                }
                try
                {
                    ddlProgram.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_Is_Phase_1"].ToString();
                }
                catch
                {
                    ddlProgram.SelectedValue = "0";
                }
                ddlDistrict_SelectedIndexChanged(ddlDistrict, new EventArgs());

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
                ddlDivision_SelectedIndexChanged(ddlDivision, new EventArgs());
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
                txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString();
                txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
                txtProjectWorkDesc.Text = ds.Tables[0].Rows[0]["ProjectWork_Description"].ToString();
                txtBudget.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
                txtADPCost.Text = ds.Tables[0].Rows[0]["ProjectWork_ADPCost"].ToString();
                txtCentage.Text = ds.Tables[0].Rows[0]["ProjectWork_Centage"].ToString();
                txtGONo.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
                txtGODate.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();
                txtContegencytext.Text = ds.Tables[0].Rows[0]["ProjectWork_Contegencytext"].ToString();
                txtSAAPYear.Text = ds.Tables[0].Rows[0]["ProjectWork_SAAPYear"].ToString();
                if (ds.Tables[0].Rows[0]["ProjectWork_UnderNGT"].ToString() == "1")
                {
                    chkNGT.Checked = true;
                }
                else
                {
                    chkNGT.Checked = false;
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
            get_tbl_ProjectWorkNGT(Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        else
        {
            MessageBox.Show("You Are Not Allowed To Edit The Details. Please Contact Administrator..");
            return;
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
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
        divNGT.Visible = chkNGT.Checked;
    }

    protected void grdDPR_PreRender(object sender, EventArgs e)
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

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            grdDPR.DataSource = null;
            grdDPR.DataBind();
        }
        else
        {
            DataSet ds = new DataSet();
            int District_Id = 0;
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
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
            ds = (new DataLayer()).get_tbl_ProjectWorkDPR(ddlSearchScheme.SelectedValue.ToString(), District_Id, Zone_Id, Circle_Id, Division_Id, 0, 0, 0, "0", 0, 0, 0);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grdDPR.DataSource = ds.Tables[0];
                grdDPR.DataBind();

                int _DPR_Id = 0;
                try
                {
                    _DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                }
                catch
                {
                    _DPR_Id = 0;
                }
                if (_DPR_Id > 0)
                {
                    for (int i = 0; i < grdDPR.Rows.Count; i++)
                    {
                        if (_DPR_Id == Convert.ToInt32(grdDPR.Rows[i].Cells[0].Text.Trim()))
                        {
                            ImageButton btnSelectDPR = grdDPR.Rows[i].FindControl("btnSelectDPR") as ImageButton;
                            btnSelectDPR_Click(btnSelectDPR, new ImageClickEventArgs(0, 0));
                            break;
                        }
                    }
                }
            }
            else
            {
                grdDPR.DataSource = null;
                grdDPR.DataBind();
            }
        }
    }

    protected void btnSelectDPR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdDPR.Rows.Count; i++)
        {
            grdDPR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
    }
}