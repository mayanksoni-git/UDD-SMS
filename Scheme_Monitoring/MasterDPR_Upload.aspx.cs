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

public partial class MasterDPR_Upload : System.Web.UI.Page
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

            txtDPRDate.Text = Session["ServerDate"].ToString();
            get_tbl_TrancheType();
            get_tbl_Project();
            get_tbl_Zone();
            get_NodalDepartment();
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divNodal.Visible = true;
            }
            else
            {
                divNodal.Visible = false;
            }

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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnSave.ID;
        up.Triggers.Add(trg1);
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

    private void get_tbl_TrancheType()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_TrancheType();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlTranche, "TrancheType_Name", "TrancheType_Id");
        }
        else
        {
            ddlTranche.Items.Clear();
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

    private void reset()
    {
        hf_ProjectDPR_Id.Value = "0";
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
    protected void grdMultipleFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkDownload") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
        }
    }
    protected void grdMultipleFiles_PreRender(object sender, EventArgs e)
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
            MessageBox.Show("Please Select A Zone");
            return;
        }
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int NodalDepartment_Id = 0;

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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_ProjectWorkDPR(Project_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, 0, 0, 0, "0", 0, Tranche_Id, NodalDepartment_Id);
        }
        else
        {
            string _Designation_Id = Session["PersonJuridiction_DesignationId"].ToString();

            //if (Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
            //{
            //    _Designation_Id = "4, 9, 1056";
            //}
            if (Session["PersonJuridiction_DesignationId"].ToString() == "1035" || Session["PersonJuridiction_DesignationId"].ToString() == "1040")
            {
                _Designation_Id = "1035, 1040";
            }
            ds = (new DataLayer()).get_tbl_ProjectWorkDPR(Project_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, 0, 0, 0, _Designation_Id, Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()), Tranche_Id, NodalDepartment_Id);
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int DPR_Id = 0;
        int ProjectType_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            DPR_Id = 0;
        }
        try
        {
            ProjectType_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            ProjectType_Id = 0;
        }
        int Scheme_Id = 0;
        int Next_Designation_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        }
        catch
        {

        }
        try
        {
            Next_Designation_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Next_Designation_Id = 0;
        }
        int Loop = 0;
        try
        {
            Loop = Convert.ToInt32(gr.Cells[5].Text.Trim());
        }
        catch
        {
            if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 9)
            {
                Loop = 1;
            }
            else
            {
                Loop = 2;
            }
        }
        if (Next_Designation_Id == 0)
        {
            hf_IsFirst.Value = "1";
        }
        else
        {
            hf_IsFirst.Value = "0";
        }
        divEntry.Visible = true;
        hf_ProjectDPR_Id.Value = DPR_Id.ToString();
        hf_Loop.Value = Loop.ToString();
        hf_Scheme_Id.Value = Scheme_Id.ToString();
        get_ProcessConfig_Current(Scheme_Id, Loop.ToString());
        get_tbl_ProjectDPRDocs(DPR_Id);

        if (Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()) == 8)
        {
            DataSet ds = new DataLayer().get_tbl_DPRQuestionnaire(Convert.ToInt32(ddlSearchScheme.SelectedValue), ProjectType_Id);
            if (AllClasses.CheckDataSet(ds))
            {
                dgvQuestionnaire.DataSource = ds.Tables[0];
                dgvQuestionnaire.DataBind();
            }
            else
            {
                dgvQuestionnaire.DataSource = null;
                dgvQuestionnaire.DataBind();
            }
        }
        else
        {
            dgvQuestionnaire.DataSource = null;
            dgvQuestionnaire.DataBind();
        }
    }

    protected void dgvQuestionnaire_PreRender(object sender, EventArgs e)
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

    private void get_tbl_ProjectDPRDocs(int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectDPRDocs(DPR_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdMultipleFiles.DataSource = ds.Tables[0];
            grdMultipleFiles.DataBind();
        }
        else
        {
            grdMultipleFiles.DataSource = null;
            grdMultipleFiles.DataBind();
        }
    }


    private void get_tbl_InvoiceStatus(int ConfigMasterId, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DPR(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_DPR(Scheme_Id, Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
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
    private void get_ProcessConfigMaster_Last(int ProcessConfigMaster_Id_Current, int Scheme_Id, int Loop)
    {
        if (Session["UserType"].ToString() == "1")
        {
            btnSave.Visible = true;
        }
        else
        {
            DataSet ds = new DataSet();

            ds = (new DataLayer()).get_ProcessConfigMaster_Last_DPR(Scheme_Id, Loop, null, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ProcessConfigMaster_Id_Current == Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString()))
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
            }
            else
            {
                btnSave.Visible = false;
            }
        }
    }
    private void get_ProcessConfig_Current(int Scheme_Id, string _loop)
    {
        if (Session["UserType"].ToString() == "1")
        {
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
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "DPR", Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Loop, Convert.ToInt32(hf_ProjectDPR_Id.Value));
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
                btnSave.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        tbl_ProjectDPRApproval obj_tbl_ProjectDPRApproval = new tbl_ProjectDPRApproval();
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Status = 1;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Comments = txtComments.Text;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Date = txtDPRDate.Text;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Loop = Convert.ToInt32(hf_Loop.Value);
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_SchemeId = Convert.ToInt32(hf_Scheme_Id.Value);
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);

        List<tbl_ProjectDPRQuestionire> obj_tbl_ProjectDPRQuestionire_Li = new List<tbl_ProjectDPRQuestionire>();
        for (int i = 0; i < dgvQuestionnaire.Rows.Count; i++)
        {
            tbl_ProjectDPRQuestionire obj_tbl_ProjectDPRQuestionire = new tbl_ProjectDPRQuestionire();
            obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            string DPRQuestionnaire_QuestionType = "S";
            try
            {
                DPRQuestionnaire_QuestionType = dgvQuestionnaire.Rows[i].Cells[3].Text.Trim();
            }
            catch
            {
                DPRQuestionnaire_QuestionType = "S";
            }
            if (DPRQuestionnaire_QuestionType == "Y")
            {
                try
                {
                    obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Answer = (dgvQuestionnaire.Rows[i].FindControl("rbtDPRQuestionnaire_Answer") as RadioButtonList).SelectedItem.Text.Trim();
                }
                catch
                {
                    
                }
            }
            else
            {
                obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Answer = (dgvQuestionnaire.Rows[i].FindControl("txtDPRQuestionnaire_Answer") as TextBox).Text.Trim();
            }
            obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Remark = (dgvQuestionnaire.Rows[i].FindControl("txtDPRQuestionnaire_Remark") as TextBox).Text.Trim();
            obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
            obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Work_Id = 0;
            obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Status = 1;
            try
            {
                obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Questionire_Id = Convert.ToInt32(dgvQuestionnaire.Rows[i].Cells[1].Text);
            }
            catch
            {
                obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Questionire_Id = 0;
            }
            if (obj_tbl_ProjectDPRQuestionire.ProjectDPRQuestionire_Answer != "")
                obj_tbl_ProjectDPRQuestionire_Li.Add(obj_tbl_ProjectDPRQuestionire);
        }

        List<tbl_ProjectDPRDocs> obj_tbl_ProjectDPRDocs_Li = new List<tbl_ProjectDPRDocs>();
        for (int i = 0; i < grdDocumentMaster.Rows.Count; i++)
        {
            FileUpload flUpload = grdDocumentMaster.Rows[i].FindControl("flUpload") as FileUpload;
            TextBox txtDocumentOrderNo = grdDocumentMaster.Rows[i].FindControl("txtDocumentOrderNo") as TextBox;
            TextBox txtDocumentComments = grdDocumentMaster.Rows[i].FindControl("txtDocumentComments") as TextBox;
            if (flUpload.HasFile)
            {
                tbl_ProjectDPRDocs obj_tbl_ProjectDPRDocs = new tbl_ProjectDPRDocs();
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_FileBytes = flUpload.FileBytes;
                string[] _fname = flUpload.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_FileName = _fname[_fname.Length - 1];
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_Status = 1;
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_OrderNo = txtDocumentOrderNo.Text.Trim();
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_Comments = txtDocumentComments.Text.Trim();
                obj_tbl_ProjectDPRDocs.ProjectDPRDocs_Type = grdDocumentMaster.Rows[i].Cells[1].Text.Trim();
                obj_tbl_ProjectDPRDocs_Li.Add(obj_tbl_ProjectDPRDocs);
            }
        }
        bool isFirst = false;
        if (hf_IsFirst.Value == "1")
        {
            isFirst = true;
        }
        else
        {
            isFirst = false;
        }
        if ((new DataLayer()).Insert_tbl_ProjectDPRApproval(obj_tbl_ProjectDPRApproval, obj_tbl_ProjectDPRQuestionire_Li, Convert.ToInt32(hf_Scheme_Id.Value), Convert.ToInt32(hf_Loop.Value), obj_tbl_ProjectDPRDocs_Li, Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), isFirst))
        {
            MessageBox.Show("DPR Approved Successfully!");
            btnSearch_Click(null, null);
            return;
        }
        else
        {
            MessageBox.Show("Error In DPR Approved!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
    }

    protected void dgvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtDPRQuestionnaire_Answer = e.Row.FindControl("txtDPRQuestionnaire_Answer") as TextBox;
            RadioButtonList rbtDPRQuestionnaire_Answer = e.Row.FindControl("rbtDPRQuestionnaire_Answer") as RadioButtonList;
            string DPRQuestionnaire_QuestionType = "S";
            try
            {
                DPRQuestionnaire_QuestionType = e.Row.Cells[3].Text.Trim();
            }
            catch
            {
                DPRQuestionnaire_QuestionType = "S";
            }
            if (DPRQuestionnaire_QuestionType == "Y")
            {
                rbtDPRQuestionnaire_Answer.Visible = true;
                txtDPRQuestionnaire_Answer.Visible = false;
            }
            else
            {
                rbtDPRQuestionnaire_Answer.Visible = false;
                txtDPRQuestionnaire_Answer.Visible = true;
            }
        }
    }

    protected void grdTimeLine_PreRender(object sender, EventArgs e)
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

    protected void btnOpenTimeline_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectDPR_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_ProjectWorkDPR_Approval_History(ProjectDPR_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
}