using NPOI.OpenXmlFormats.Dml.Chart;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterDPR_NextAction : System.Web.UI.Page
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

            get_tbl_TrancheType();
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
            txtEFCDate.Text = Session["ServerDate"].ToString();
            txtGODate.Text = Session["ServerDate"].ToString();
            txtNITIssuedDate.Text = Session["ServerDate"].ToString();
            txtTenderClosingDate.Text = Session["ServerDate"].ToString();
            txtPreBidMeeting.Text = Session["ServerDate"].ToString();
            txtNITIssuedDate1.Text = Session["ServerDate"].ToString();
            txtDateSLTC.Text = Session["ServerDate"].ToString();
            txtSMDDispatchDate.Text = Session["ServerDate"].ToString();
            txtWorkOrderDate.Text = Session["ServerDate"].ToString();
            txtTenderPublished.Text = Session["ServerDate"].ToString();
            txtTenderEndDate.Text = Session["ServerDate"].ToString();
            txtTechnicalBidOpeningDate.Text = Session["ServerDate"].ToString();
            txtTenderEndDateR.Text = Session["ServerDate"].ToString();
            txtTechnicalBidOpeningDateR.Text = Session["ServerDate"].ToString();
            txtTechnicalBidOpening.Text = Session["ServerDate"].ToString();
            txtBiddersEvaluation.Text = Session["ServerDate"].ToString();
            txtFinancialBidOpeningDateF.Text = Session["ServerDate"].ToString();

            if (Client == "CNDS")
            {
                divSkipSLTC.Visible = true;
                divEFCPFADSkip.Visible = true;
            }
            else
            {
                divSkipSLTC.Visible = false;
                divEFCPFADSkip.Visible = false;
            }
            
            get_tbl_DPRTenderSteps();
            get_tbl_Project();
            get_tbl_Zone();
            get_tbl_ProjectWorkNGT();
            get_PQC();

            get_tbl_ProjectDPRBidResponse(0);
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
    private void get_tbl_DPRTenderSteps()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DPRTenderSteps();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlStatus, "DPRTenderSteps_StepName", "DPRTenderSteps_Id");
        }
        else
        {
            ddlStatus.Items.Clear();
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
        ds = (new DataLayer()).get_tbl_ProjectWorkDPR(Project_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, 0, 0, 0, "-1", -1, Tranche_Id, NodalDepartment_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            divEntry.Visible = false;

            int _DPR_Id = 0;
            int DPR_Id = 0;
            try
            {
                _DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
            }
            catch
            {
                _DPR_Id = 0;
            }
            for (int i = 0; i < grdPost.Rows.Count; i++)
            {
                try
                {
                    DPR_Id = Convert.ToInt32(grdPost.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    DPR_Id = 0;
                }
                if (DPR_Id > 0 && _DPR_Id > 0 && DPR_Id == _DPR_Id)
                {
                    ImageButton btnEdit = grdPost.Rows[i].FindControl("btnEdit") as ImageButton;
                    btnEdit_Click(btnEdit, new ImageClickEventArgs(0, 0));
                }
            }
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

        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            DPR_Id = 0;
        }
        divEntry.Visible = true;
        hf_ProjectDPR_Id.Value = DPR_Id.ToString();
        txtACACostApproved.Text = gr.Cells[16].Text.Trim();
        txtCapexCostApproved.Text = gr.Cells[14].Text.Trim();
        txtOMCostApproved.Text = gr.Cells[15].Text.Trim();
        get_tbl_ProjectDPRBidResponse(DPR_Id);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectDPRTender(DPR_Id, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdDPRTimeline.DataSource = ds.Tables[0];
            grdDPRTimeline.DataBind();

            for (int i = 0; i < grdDPRTimeline.Rows.Count; i++)
            {
                ImageButton btnDelete = grdDPRTimeline.Rows[i].FindControl("btnDelete") as ImageButton;
                if (i == grdDPRTimeline.Rows.Count - 1)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }

                int Step_Status = 0;
                try
                {
                    Step_Status = Convert.ToInt32(grdDPRTimeline.Rows[i].Cells[2].Text.Trim());
                }
                catch
                {
                    Step_Status = 0;
                }
                LinkButton lnkActionCorrigendum = grdDPRTimeline.Rows[i].FindControl("lnkActionCorrigendum") as LinkButton;
                if (Step_Status == 6 && i == grdDPRTimeline.Rows.Count - 1)
                {
                    lnkActionCorrigendum.Visible = true;
                }
                else
                {
                    lnkActionCorrigendum.Visible = false;
                }
            }
        }
        else
        {
            grdDPRTimeline.DataSource = null;
            grdDPRTimeline.DataBind();
        }

        if (grdDPRTimeline.Rows.Count == 0)
        {
            ddlStatus.SelectedIndex = 1;
            ddlStatus.Enabled = false;
            ddlStatus_SelectedIndexChanged(ddlStatus, e);
        }
        else
        {
            int Step_Id = 0;
            int Step_Id_Next = 0;
            try
            {
                Step_Id = Convert.ToInt32(grdDPRTimeline.Rows[grdDPRTimeline.Rows.Count - 1].Cells[2].Text.Trim());
                if (Step_Id == 14)
                {
                    ddlStatus.SelectedValue = "0";
                    ddlStatus.Enabled = false;
                    ddlStatus_SelectedIndexChanged(ddlStatus, e);
                    MessageBox.Show("Loop Completed Thankyou....");
                    return;
                }
                else
                {
                    Step_Id_Next = new DataLayer().get_DPR_Step_Next(Step_Id);
                }
            }
            catch
            {
                Step_Id_Next = 0;
            }
            if (Step_Id_Next == 0)
            {
                ddlStatus.SelectedIndex = 1;
                ddlStatus.Enabled = false;
                ddlStatus_SelectedIndexChanged(ddlStatus, e);
            }
            else
            {
                ddlStatus.SelectedValue = Step_Id_Next.ToString();
                ddlStatus.Enabled = false;
                ddlStatus_SelectedIndexChanged(ddlStatus, e);
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
        string Client = ConfigurationManager.AppSettings.Get("Client");
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();

        int DPRStep_Id = 0;
        try
        {
            DPRStep_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        catch
        {
            DPRStep_Id = 0;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (DPRStep_Id == 0)
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        else if (DPRStep_Id == 1)
        {
            if (txtEFCDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Date");
                txtEFCDate.Focus();
                return;
            }
            if (txtCostApproved.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Approved Cost");
                txtCostApproved.Focus();
                return;
            }
            if (Client == "CNDS")
            {
                
            }
            else
            {
                if (!fl_Docs_Step1.HasFile)
                {
                    MessageBox.Show("Please Provide EFC / PFAD Document");
                    fl_Docs_Step1.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtEFCDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep1.Text.Trim();
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CapexCostApproved = Convert.ToDecimal(txtCapexCostApproved.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CapexCostApproved = 0;
            }
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_OMCostApproved = Convert.ToDecimal(txtOMCostApproved.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_OMCostApproved = 0;
            }
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_ACACostApproved = Convert.ToDecimal(txtACACostApproved.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_ACACostApproved = 0;
            }
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CostApproved = Convert.ToDecimal(txtCostApproved.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CostApproved = 0;
            }
            if (fl_Docs_Step1.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step1.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 2)
        {
            if (txtGODate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide GO Date");
                txtGODate.Focus();
                return;
            }
            if (txtGONumber.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide GO Number");
                txtGONumber.Focus();
                return;
            }
            if (txtGOCentralShare.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Central Share");
                txtGOCentralShare.Focus();
                return;
            }
            if (txtGOStateShare.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide State Share");
                txtGOStateShare.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step2.HasFile)
                {
                    MessageBox.Show("Please Provide GO Document");
                    fl_Docs_Step2.Focus();
                    return;
                }
            }
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtGODate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtGONumber.Text.Trim();
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CentralShare = Convert.ToDecimal(txtGOCentralShare.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CentralShare = 0;
            }
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_StateShare = Convert.ToDecimal(txtGOStateShare.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_StateShare = 0;
            }
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_ULBShare = Convert.ToDecimal(txtGOULBShare.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_ULBShare = 0;
            }
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CentageShare = Convert.ToDecimal(txtGOCentage.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_CentageShare = 0;
            }
            if (fl_Docs_Step2.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step2.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 3)
        {
            if (txtNITIssuedDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide NIT Date");
                txtNITIssuedDate.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step3.HasFile)
                {
                    MessageBox.Show("Please Provide NIT Document");
                    fl_Docs_Step3.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtNITIssuedDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep3.Text.Trim();
            if (fl_Docs_Step3.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step3.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 4)
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtNITIssuedDate1.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep4.Text.Trim();
            if (fl_Docs_Step4.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step4.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 5)
        {
            if (txtTenderPublished.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Published Date");
                txtTenderPublished.Focus();
                return;
            }
            if (txtTenderEndDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender End / Closing Date");
                txtTenderEndDate.Focus();
                return;
            }
            if (txtTechnicalBidOpeningDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Technical Bid Opening Date");
                txtTechnicalBidOpeningDate.Focus();
                return;
            }
            if (txtTenderCost.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Cost");
                txtTenderCost.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step5.HasFile)
                {
                    MessageBox.Show("Please Provide Tender Bid RFP Document");
                    fl_Docs_Step5.Focus();
                    return;
                }
                if (!fl_Docs_Step5_A.HasFile)
                {
                    MessageBox.Show("Please Provide Tender Bid Document (eTender)");
                    fl_Docs_Step5_A.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtTenderPublished.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtBidRefrenceNo.Text.Trim();
            try
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderCost = Convert.ToDecimal(txtTenderCost.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderCost = 0;
            }
            obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = txtTenderEndDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = txtTechnicalBidOpeningDate.Text.Trim();
            if (fl_Docs_Step5.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step5.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
            if (fl_Docs_Step5_A.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes_A = fl_Docs_Step5_A.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes_A = null;
            }
            for (int i = 0; i < grdPQC.Rows.Count; i++)
            {
                CheckBox chkApplicable = (grdPQC.Rows[i].FindControl("chkApplicable") as CheckBox);
                if (chkApplicable.Checked)
                {
                    Label txtPQCDetails = (grdPQC.Rows[i].FindControl("txtPQCDetails") as Label);
                    TextBox txtPQCDetails1 = (grdPQC.Rows[i].FindControl("txtPQCDetails1") as TextBox);
                    TextBox txtMinValue = (grdPQC.Rows[i].FindControl("txtMinValue") as TextBox);
                    TextBox txtMaxValue = (grdPQC.Rows[i].FindControl("txtMaxValue") as TextBox);
                    TextBox txtComments = (grdPQC.Rows[i].FindControl("txtComments") as TextBox);

                    tbl_ProjectDPRPQC obj_tbl_ProjectDPRPQC = new tbl_ProjectDPRPQC();
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Status = 1;
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id = 0;
                    }
                    if (obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id > 0)
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCName = txtPQCDetails.Text.Trim();
                    }
                    else
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCName = txtPQCDetails1.Text.Trim();
                    }
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCMinVal = txtMinValue.Text.Trim();
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCMaxVal = txtMaxValue.Text.Trim();
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Comments = txtComments.Text.Trim();

                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_DPR_Id = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Mandatory = Convert.ToInt32(grdPQC.Rows[i].Cells[2].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Mandatory = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(grdPQC.Rows[i].Cells[3].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(grdPQC.Rows[i].Cells[4].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Upload_Document_Count = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(grdPQC.Rows[i].Cells[5].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Auto_Calculated = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCParent_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[6].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCParent_Id = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[7].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Id = 0;
                    }
                    try
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Order = Convert.ToInt32(grdPQC.Rows[i].Cells[8].Text.Trim());
                    }
                    catch
                    {
                        obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Order = 0;
                    }
                    obj_tbl_ProjectDPRPQC_Li.Add(obj_tbl_ProjectDPRPQC);
                }
            }
            if (obj_tbl_ProjectDPRPQC_Li.Count == 0)
            {
                MessageBox.Show("Please Fill Qualification Criteria.");
                return;
            }
        }
        else if (DPRStep_Id == 6)
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtPreBidMeeting.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep6.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = txtTenderEndDateR.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = txtTechnicalBidOpeningDateR.Text.Trim();
            if (fl_Docs_Step6.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step6.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
            for (int i = 0; i < grdQualificationCriteriaR.Rows.Count; i++)
            {
                Label txtPQCDetails = (grdQualificationCriteriaR.Rows[i].FindControl("txtPQCDetails") as Label);
                TextBox txtMinValue = (grdQualificationCriteriaR.Rows[i].FindControl("txtMinValue") as TextBox);
                TextBox txtMaxValue = (grdQualificationCriteriaR.Rows[i].FindControl("txtMaxValue") as TextBox);
                TextBox txtComments = (grdQualificationCriteriaR.Rows[i].FindControl("txtComments") as TextBox);
                tbl_ProjectDPRPQC obj_tbl_ProjectDPRPQC = new tbl_ProjectDPRPQC();
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Status = 1;
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCName = txtPQCDetails.Text.Replace("\r", "").Replace("\n", "").Trim();
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCMinVal = txtMinValue.Text.Replace("\r", "").Replace("\n", "").Trim();
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCMaxVal = txtMaxValue.Text.Replace("\r", "").Replace("\n", "").Trim();
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Comments = txtComments.Text.Replace("\r", "").Replace("\n", "").Trim();
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id = Convert.ToInt32(grdQualificationCriteriaR.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_DPR_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Mandatory = Convert.ToInt32(grdPQC.Rows[i].Cells[2].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Mandatory = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(grdPQC.Rows[i].Cells[3].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(grdPQC.Rows[i].Cells[4].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Upload_Document_Count = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(grdPQC.Rows[i].Cells[5].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Auto_Calculated = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCParent_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[6].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCParent_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[7].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Order = Convert.ToInt32(grdPQC.Rows[i].Cells[8].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Order = 0;
                }
                obj_tbl_ProjectDPRPQC_Li.Add(obj_tbl_ProjectDPRPQC);
            }

            for (int i = 0; i < grdPreBidResponseDocs.Rows.Count; i++)
            {
                TextBox txtDocumentDetails = (grdPreBidResponseDocs.Rows[i].FindControl("txtDocumentDetails") as TextBox);
                FileUpload flResponseDoc = (grdPreBidResponseDocs.Rows[i].FindControl("flResponseDoc") as FileUpload);

                tbl_ProjectDPRBidResponse obj_tbl_ProjectDPRBidResponse = new tbl_ProjectDPRBidResponse();
                obj_tbl_ProjectDPRBidResponse.ProjectDPRBidResponse_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRBidResponse.ProjectDPRBidResponse_Status = 1;
                obj_tbl_ProjectDPRBidResponse.ProjectDPRBidResponse_BidResponseName = txtDocumentDetails.Text.Trim();
                obj_tbl_ProjectDPRBidResponse.ProjectDPRBidResponse_DocumentPath = grdPreBidResponseDocs.Rows[i].Cells[2].Text.Trim();
                if (flResponseDoc.HasFile)
                {
                    obj_tbl_ProjectDPRBidResponse.ProjectDPRBidResponse_DocumentBytes = flResponseDoc.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPRBidResponse.ProjectDPRBidResponse_DocumentPath = grdPreBidResponseDocs.Rows[i].Cells[2].Text.Trim();
                }
                obj_tbl_ProjectDPRBidResponse_Li.Add(obj_tbl_ProjectDPRBidResponse);
            }
        }
        else if (DPRStep_Id == 7)
        {
            if (txtTenderClosingDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Closing Date");
                txtTenderClosingDate.Focus();
                return;
            }
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtTenderClosingDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep7.Text.Trim();
            if (fl_Docs_Step7.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step7.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 8)
        {
            if (txtTechnicalBidOpening.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Technical Bid Opening Date");
                txtTechnicalBidOpening.Focus();
                return;
            }
            if (grdNGTDtls.Rows.Count < 2)
            {
                MessageBox.Show("Please Fill Atleast 2 Participated Bidders Details");
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step8.HasFile)
                {
                    MessageBox.Show("Please Upload Bid Opening Document (From e-tender Portal)");
                    fl_Docs_Step8.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtTechnicalBidOpening.Text.Trim();
            if (fl_Docs_Step8.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step8.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
            for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
            {
                TextBox txtFirmName = (grdNGTDtls.Rows[i].FindControl("txtFirmName") as TextBox);
                TextBox txtFirmPAN = (grdNGTDtls.Rows[i].FindControl("txtFirmPAN") as TextBox);
                TextBox txtFirmGSTIN = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTIN") as TextBox);
                TextBox txtContactNo = (grdNGTDtls.Rows[i].FindControl("txtContactNo") as TextBox);
                TextBox txtShare = (grdNGTDtls.Rows[i].FindControl("txtShare") as TextBox);
                CheckBox chkGSTNA = (grdNGTDtls.Rows[i].FindControl("chkGSTNA") as CheckBox);

                TextBox txtFirmNameP = (grdNGTDtls.Rows[i].FindControl("txtFirmNameP") as TextBox);
                TextBox txtFirmPANP = (grdNGTDtls.Rows[i].FindControl("txtFirmPANP") as TextBox);
                TextBox txtFirmGSTINP = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTINP") as TextBox);
                TextBox txtContactNoP = (grdNGTDtls.Rows[i].FindControl("txtContactNoP") as TextBox);
                TextBox txtShareP = (grdNGTDtls.Rows[i].FindControl("txtShareP") as TextBox);
                CheckBox chkGSTNAP = (grdNGTDtls.Rows[i].FindControl("chkGSTNAP") as CheckBox);

                RadioButtonList ddlISJV = (grdNGTDtls.Rows[i].FindControl("ddlISJV") as RadioButtonList);
                if (!chkGSTNA.Checked)
                {
                    if (txtFirmGSTIN.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Firm GSTIN.");
                        txtFirmGSTIN.Focus();
                        return;
                    }
                    if (txtFirmPAN.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Firm PAN No.");
                        txtFirmPAN.Focus();
                        return;
                    }
                    //if (!txtFirmGSTIN.Text.Contains(txtFirmPAN.Text))
                    //{
                    //    MessageBox.Show("Please Fill Valid GSTIN.");
                    //    txtFirmGSTIN.Focus();
                    //    return;
                    //}
                }
                if (txtFirmName.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Firm Name.");
                    txtFirmName.Focus();
                    return;
                }


                //Regex regex;
                //regex = new Regex("[A-Z]{5}[0-9]{4}[A-Z]{1}$");
                //if (!regex.IsMatch(txtFirmPAN.Text.Trim()))
                //{
                //MessageBox.Show("Please Fill Valid PAN.");
                //txtFirmPAN.Focus();
                //return;
                //}
                //regex = new Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                //if (!regex.IsMatch(txtFirmGSTIN.Text.Trim()))
                //{
                //MessageBox.Show("Please Fill Valid GSTIN.");
                //txtFirmGSTIN.Focus();
                //return;
                //}


                if (ddlISJV.SelectedValue == "Yes")
                {
                    if (!chkGSTNAP.Checked)
                    {
                        if (txtFirmGSTINP.Text.Trim() == "")
                        {
                            MessageBox.Show("Please Fill Firm GSTIN For JV.");
                            txtFirmGSTINP.Focus();
                            return;
                        }
                        if (txtFirmPANP.Text.Trim() == "")
                        {
                            MessageBox.Show("Please Fill Firm PAN No For JV.");
                            txtFirmPANP.Focus();
                            return;
                        }
                        //if (!txtFirmGSTINP.Text.Contains(txtFirmPANP.Text))
                        //{
                        //    MessageBox.Show("Please Fill Valid GSTIN for JV.");
                        //    txtFirmGSTINP.Focus();
                        //    return;
                        //}
                    }
                    if (txtFirmNameP.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Firm Name For JV.");
                        txtFirmNameP.Focus();
                        return;
                    }


                    //regex = new Regex("[A-Z]{5}[0-9]{4}[A-Z]{1}$");
                    //if (!regex.IsMatch(txtFirmPANP.Text.Trim()))
                    //{
                    //MessageBox.Show("Please Fill Valid PAN For JV.");
                    //txtFirmPANP.Focus();
                    //return;
                    //}
                    //regex = new Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                    //if (!regex.IsMatch(txtFirmGSTINP.Text.Trim()))
                    //{
                    //MessageBox.Show("Please Fill Valid GSTIN For JV.");
                    //txtFirmGSTINP.Focus();
                    //return;
                    //}

                    if (txtShare.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Lead Bidder Share.");
                        txtShare.Focus();
                        return;
                    }
                    if (txtShareP.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Partner Bidder Share.");
                        txtShareP.Focus();
                        return;
                    }
                    decimal Share = 0;
                    decimal ShareP = 0;
                    try
                    {
                        Share = Convert.ToDecimal(txtShare.Text.Trim());
                    }
                    catch
                    {
                        Share = 0;
                    }
                    try
                    {
                        ShareP = Convert.ToDecimal(txtShareP.Text.Trim());
                    }
                    catch
                    {
                        ShareP = 0;
                    }
                    if (Share < 51)
                    {
                        MessageBox.Show("Lead Bidder Share Should Be More Than 51%.");
                        txtShare.Focus();
                        return;
                    }
                    if (Share < 30)
                    {
                        MessageBox.Show("Partner Bidder Share Should Be More Than 30%.");
                        txtShareP.Focus();
                        return;
                    }
                }
                tbl_ProjectDPRBidder obj_tbl_ProjectDPRBidder = new tbl_ProjectDPRBidder();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Status = 1;
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderName = txtFirmName.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderPAN = txtFirmPAN.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTIN = txtFirmGSTIN.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderMobile = txtContactNo.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Is_JV = ddlISJV.SelectedItem.Value;
                if (chkGSTNA.Checked)
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTIN_Available = 0;
                else
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTIN_Available = 1;
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShare = Convert.ToDecimal(txtShare.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShare = 0;
                }
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderNameP = txtFirmNameP.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderPANP = txtFirmPANP.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTINP = txtFirmGSTINP.Text.Trim();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderMobileP = txtContactNoP.Text.Trim();
                if (chkGSTNAP.Checked)
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTINP_Available = 0;
                else
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderGSTINP_Available = 1;
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShareP = Convert.ToDecimal(txtShareP.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderShareP = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = 0;
                }
                obj_tbl_ProjectDPRBidder_Li.Add(obj_tbl_ProjectDPRBidder);
            }
        }
        else if (DPRStep_Id == 9)
        {
            if (txtBiddersEvaluation.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Technical Bid Evaluation Date");
                txtBiddersEvaluation.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step9.HasFile)
                {
                    MessageBox.Show("Please Upload Technical Bid Opening Document (From e-tender Portal)");
                    fl_Docs_Step9.Focus();
                    return;
                }
            }
            
            if (fl_Docs_Step9.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step9.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtBiddersEvaluation.Text.Trim();
            bool is_Qualified = false;
            for (int i = 0; i < grdBidderDetails.Rows.Count; i++)
            {
                CheckBox chkQualified = (grdBidderDetails.Rows[i].FindControl("chkQualified") as CheckBox);
                TextBox txtComments = (grdBidderDetails.Rows[i].FindControl("txtComments") as TextBox);
                tbl_ProjectDPRBidder obj_tbl_ProjectDPRBidder = new tbl_ProjectDPRBidder();
                int Total_Response = 0;
                try
                {
                    Total_Response = Convert.ToInt32(grdBidderDetails.Rows[i].Cells[4].Text.Trim());
                }
                catch
                {
                    Total_Response = 0;
                }
                int Total_Questions = 0;
                try
                {
                    Total_Questions = Convert.ToInt32(grdBidderDetails.Rows[i].Cells[5].Text.Trim());
                }
                catch
                {
                    Total_Questions = 0;
                }
                if (Client == "CNDS")
                {
                    
                }
                else
                {
                    if (Total_Response < Total_Questions)
                    {
                        MessageBox.Show("Please Fill Complete Evaluation Response(s) For Bidder No: " + (i + 1).ToString());
                        return;
                    }
                }                
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Status = 1;
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = Convert.ToInt32(grdBidderDetails.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = 0;
                }
                if (chkQualified.Checked)
                {
                    is_Qualified = true;
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_TechnicalQualified = 1;
                }
                else
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_TechnicalQualified = 0;
                }
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Comments = txtComments.Text.Trim();
                obj_tbl_ProjectDPRBidder_Li.Add(obj_tbl_ProjectDPRBidder);
            }
            if (!is_Qualified)
            {
                MessageBox.Show("Please Mark Atleast One Bidder as Qualified For Technical");
                return;
            }
        }
        else if (DPRStep_Id == 10)
        {
            if (txtFinancialBidOpeningDateF.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Tender Financial Bid Evaluation Date");
                txtFinancialBidOpeningDateF.Focus();
                return;
            }
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtFinancialBidOpeningDateF.Text.Trim();
            if (fl_Docs_Step10.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step10.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
            bool is_Qualified = false;
            for (int i = 0; i < grdBiddersFinancial.Rows.Count; i++)
            {
                CheckBox chkQualified = (grdBiddersFinancial.Rows[i].FindControl("chkQualified") as CheckBox);
                TextBox txtQuotedRate = (grdBiddersFinancial.Rows[i].FindControl("txtQuotedRate") as TextBox);
                TextBox txtComments = (grdBiddersFinancial.Rows[i].FindControl("txtComments") as TextBox);
                tbl_ProjectDPRBidder obj_tbl_ProjectDPRBidder = new tbl_ProjectDPRBidder();
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Status = 1;
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = Convert.ToInt32(grdBiddersFinancial.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_DPR_Id = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_TechnicalQualified = Convert.ToInt32(grdBiddersFinancial.Rows[i].Cells[2].Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_TechnicalQualified = 0;
                }
                if (chkQualified.Checked)
                {
                    is_Qualified = true;
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_FinancialQualified = 1;
                }
                else
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_FinancialQualified = 0;
                }
                try
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderAmount = Convert.ToDecimal(txtQuotedRate.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderAmount = 0;
                }
                if (obj_tbl_ProjectDPRBidder.ProjectDPRBidder_BidderAmount == 0)
                {
                    MessageBox.Show("Please Fill Bidders Quoted Rate!");
                    txtQuotedRate.Focus();
                    return;
                }
                obj_tbl_ProjectDPRBidder.ProjectDPRBidder_Comments = txtComments.Text.Trim();
                obj_tbl_ProjectDPRBidder_Li.Add(obj_tbl_ProjectDPRBidder);
            }
            if (!is_Qualified)
            {
                MessageBox.Show("Please Mark Atleast One Bidder as Qualified For Financial");
                return;
            }
        }
        else if (DPRStep_Id == 11)
        {
            if (txtSMDDispatchDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Date of Dispatch To SMD");
                txtSMDDispatchDate.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step11.HasFile)
                {
                    MessageBox.Show("Please Provide Dispatch Document");
                    fl_Docs_Step11.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtSMDDispatchDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep11.Text.Trim();
            if (fl_Docs_Step11.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step11.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 12)
        {
            if (txtDateSLTC.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Date of SLTC");
                txtDateSLTC.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step12.HasFile)
                {
                    MessageBox.Show("Please Provide SLTC Document");
                    fl_Docs_Step12.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtDateSLTC.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep12.Text.Trim();
            if (fl_Docs_Step12.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step12.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 13)
        {
            if (txtWorkOrderDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Date of Work Order Generation");
                txtWorkOrderDate.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step13.HasFile)
                {
                    MessageBox.Show("Please Upload Work Order Document");
                    fl_Docs_Step13.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtWorkOrderDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtCommentStep13.Text.Trim();
            if (fl_Docs_Step13.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step13.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else if (DPRStep_Id == 14)
        {
            if (txtAgreementDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Date of Agreement With L1 Bidded");
                txtAgreementDate.Focus();
                return;
            }
            if (Client == "CNDS")
            {

            }
            else
            {
                if (!fl_Docs_Step14.HasFile)
                {
                    MessageBox.Show("Please Upload Agreement Document");
                    fl_Docs_Step14.Focus();
                    return;
                }
            }
            
            obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtAgreementDate.Text.Trim();
            obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtAgreementNo.Text.Trim();
            if (fl_Docs_Step14.HasFile)
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step14.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
            }
        }
        else
        {

        }
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, false))
        {
            MessageBox.Show("Details Updated Successfully!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }

    private void reset()
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        }
        catch
        {
            DPR_Id = 0;
        }
        divEntry.Visible = true;
        txtEFCDate.Text = Session["ServerDate"].ToString();
        txtGODate.Text = Session["ServerDate"].ToString();
        txtNITIssuedDate.Text = Session["ServerDate"].ToString();
        txtTenderClosingDate.Text = Session["ServerDate"].ToString();
        txtPreBidMeeting.Text = Session["ServerDate"].ToString();
        txtNITIssuedDate1.Text = Session["ServerDate"].ToString();
        txtDateSLTC.Text = Session["ServerDate"].ToString();
        txtSMDDispatchDate.Text = Session["ServerDate"].ToString();
        txtWorkOrderDate.Text = Session["ServerDate"].ToString();

        txtCommentStep1.Text = "";
        txtCommentStep3.Text = "";
        txtCommentStep4.Text = "";
        txtCommentStep6.Text = "";
        txtCommentStep7.Text = "";
        txtCommentStep11.Text = "";
        txtCommentStep12.Text = "";
        txtCommentStep13.Text = "";

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectDPRTender(DPR_Id, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdDPRTimeline.DataSource = ds.Tables[0];
            grdDPRTimeline.DataBind();

            for (int i = 0; i < grdDPRTimeline.Rows.Count; i++)
            {
                ImageButton btnDelete = grdDPRTimeline.Rows[i].FindControl("btnDelete") as ImageButton;
                if (i == grdDPRTimeline.Rows.Count - 1)
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
        }
        else
        {
            grdDPRTimeline.DataSource = null;
            grdDPRTimeline.DataBind();
        }

        if (grdDPRTimeline.Rows.Count == 0)
        {
            ddlStatus.SelectedIndex = 1;
            ddlStatus.Enabled = false;
            ddlStatus_SelectedIndexChanged(ddlStatus, new EventArgs());
        }
        else
        {
            int Step_Id = 0;
            int Step_Id_Next = 0;
            try
            {
                Step_Id = Convert.ToInt32(grdDPRTimeline.Rows[grdDPRTimeline.Rows.Count - 1].Cells[2].Text.Trim());
                Step_Id_Next = new DataLayer().get_DPR_Step_Next(Step_Id);
            }
            catch
            {
                Step_Id_Next = 0;
            }
            if (Step_Id_Next == 0)
            {
                ddlStatus.SelectedIndex = 1;
                ddlStatus.Enabled = false;
                ddlStatus_SelectedIndexChanged(ddlStatus, new EventArgs());
            }
            else
            {
                ddlStatus.SelectedValue = Step_Id_Next.ToString();
                ddlStatus.Enabled = false;
                ddlStatus_SelectedIndexChanged(ddlStatus, new EventArgs());
            }
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

    private void get_tbl_ProjectWorkNGT()
    {
        DataTable dtNGT = new DataTable();

        DataColumn dc_ProjectDPRBidder_Id = new DataColumn("ProjectDPRBidder_Id", typeof(int));
        DataColumn dc_ProjectDPRBidder_DPR_Id = new DataColumn("ProjectDPRBidder_DPR_Id", typeof(int));
        DataColumn dc_ProjectDPRBidder_BidderName = new DataColumn("ProjectDPRBidder_BidderName", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderPAN = new DataColumn("ProjectDPRBidder_BidderPAN", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderGSTIN = new DataColumn("ProjectDPRBidder_BidderGSTIN", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderMobile = new DataColumn("ProjectDPRBidder_BidderMobile", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderShare = new DataColumn("ProjectDPRBidder_BidderShare", typeof(decimal));
        DataColumn dc_ProjectDPRBidder_BidderNameP = new DataColumn("ProjectDPRBidder_BidderNameP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderPANP = new DataColumn("ProjectDPRBidder_BidderPANP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderGSTINP = new DataColumn("ProjectDPRBidder_BidderGSTINP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderMobileP = new DataColumn("ProjectDPRBidder_BidderMobileP", typeof(string));
        DataColumn dc_ProjectDPRBidder_BidderShareP = new DataColumn("ProjectDPRBidder_BidderShareP", typeof(decimal));
        DataColumn dc_ProjectDPRBidder_BidderAmount = new DataColumn("ProjectDPRBidder_BidderAmount", typeof(decimal));
        DataColumn dc_ProjectDPRBidder_Comments = new DataColumn("ProjectDPRBidder_Comments", typeof(string));
        DataColumn dc_ProjectDPRBidder_TechnicalQualified = new DataColumn("ProjectDPRBidder_TechnicalQualified", typeof(int));
        DataColumn dc_ProjectDPRBidder_FinancialQualified = new DataColumn("ProjectDPRBidder_FinancialQualified", typeof(int));
        DataColumn dc_ProjectDPRBidder_Is_JV = new DataColumn("ProjectDPRBidder_Is_JV", typeof(string));

        DataColumn dc_ProjectDPRBidder_BidderGSTIN_Available = new DataColumn("ProjectDPRBidder_BidderGSTIN_Available", typeof(int));
        DataColumn dc_ProjectDPRBidder_BidderGSTINP_Available = new DataColumn("ProjectDPRBidder_BidderGSTINP_Available", typeof(int));

        dtNGT.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidder_Id, dc_ProjectDPRBidder_DPR_Id, dc_ProjectDPRBidder_BidderName, dc_ProjectDPRBidder_BidderPAN, dc_ProjectDPRBidder_BidderGSTIN, dc_ProjectDPRBidder_BidderMobile, dc_ProjectDPRBidder_BidderShare, dc_ProjectDPRBidder_BidderNameP, dc_ProjectDPRBidder_BidderPANP, dc_ProjectDPRBidder_BidderGSTINP, dc_ProjectDPRBidder_BidderMobileP, dc_ProjectDPRBidder_BidderShareP, dc_ProjectDPRBidder_BidderAmount, dc_ProjectDPRBidder_Comments, dc_ProjectDPRBidder_TechnicalQualified, dc_ProjectDPRBidder_FinancialQualified, dc_ProjectDPRBidder_Is_JV, dc_ProjectDPRBidder_BidderGSTIN_Available, dc_ProjectDPRBidder_BidderGSTINP_Available });

        DataRow dr = dtNGT.NewRow();
        dtNGT.Rows.Add(dr);
        ViewState["dtNGT"] = dtNGT;

        grdNGTDtls.DataSource = dtNGT;
        grdNGTDtls.DataBind();
    }

    private void get_tbl_ProjectDPR_Bidder_Order(int ProjectDPRBidder_Id)
    {
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPR_Bidder_Order(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["dtBidder_Order"] = ds.Tables[0];

            grdBidderOrder.DataSource = ds.Tables[0];
            grdBidderOrder.DataBind();
        }
        else
        {
            DataTable dtBidder_Order = new DataTable();
            DataColumn dc_ProjectDPR_Bidder_Order_Id = new DataColumn("ProjectDPR_Bidder_Order_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_DPR_Id = new DataColumn("ProjectDPR_Bidder_Order_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_DPRBidder_Id = new DataColumn("ProjectDPR_Bidder_Order_DPRBidder_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_Name_Of_Work = new DataColumn("ProjectDPR_Bidder_Order_Name_Of_Work", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_StartDate = new DataColumn("ProjectDPR_Bidder_Order_StartDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_EndDate = new DataColumn("ProjectDPR_Bidder_Order_EndDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Amount = new DataColumn("ProjectDPR_Bidder_Order_Amount", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Inflation", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Amount_After_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Amount_After_Inflation", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_JV_Share = new DataColumn("ProjectDPR_Bidder_Order_JV_Share", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_JV_Contract_Value = new DataColumn("ProjectDPR_Bidder_Order_JV_Contract_Value", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Simmilar_Nature = new DataColumn("ProjectDPR_Bidder_Order_Simmilar_Nature", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Completed = new DataColumn("ProjectDPR_Bidder_Order_Completed", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Comments = new DataColumn("ProjectDPR_Bidder_Order_Comments", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_BidderType = new DataColumn("ProjectDPR_Bidder_Order_BidderType", typeof(string));

            DataColumn dc_ProjectDPR_Bidder_Order_OrderPath = new DataColumn("ProjectDPR_Bidder_Order_OrderPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath1", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath2", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate1", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate2", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_ReminderCount = new DataColumn("ProjectDPR_Bidder_Order_ReminderCount", typeof(int));

            dtBidder_Order.Columns.AddRange(new DataColumn[] { dc_ProjectDPR_Bidder_Order_Id, dc_ProjectDPR_Bidder_Order_DPR_Id, dc_ProjectDPR_Bidder_Order_DPRBidder_Id, dc_ProjectDPR_Bidder_Order_Name_Of_Work, dc_ProjectDPR_Bidder_Order_StartDate, dc_ProjectDPR_Bidder_Order_EndDate, dc_ProjectDPR_Bidder_Order_Amount, dc_ProjectDPR_Bidder_Order_Inflation, dc_ProjectDPR_Bidder_Order_Amount_After_Inflation, dc_ProjectDPR_Bidder_Order_JV_Share, dc_ProjectDPR_Bidder_Order_JV_Contract_Value, dc_ProjectDPR_Bidder_Order_Simmilar_Nature, dc_ProjectDPR_Bidder_Order_Completed, dc_ProjectDPR_Bidder_Order_Comments, dc_ProjectDPR_Bidder_Order_BidderType, dc_ProjectDPR_Bidder_Order_OrderPath, dc_ProjectDPR_Bidder_Order_VerificationPath, dc_ProjectDPR_Bidder_Order_VerificationLetterPath, dc_ProjectDPR_Bidder_Order_VerificationLetterPath1, dc_ProjectDPR_Bidder_Order_VerificationLetterPath2, dc_ProjectDPR_Bidder_Order_VerificationLetterDate, dc_ProjectDPR_Bidder_Order_VerificationLetterDate1, dc_ProjectDPR_Bidder_Order_VerificationLetterDate2, dc_ProjectDPR_Bidder_Order_ReminderCount });

            DataRow dr = dtBidder_Order.NewRow();
            dr["ProjectDPR_Bidder_Order_DPRBidder_Id"] = ProjectDPRBidder_Id;
            dtBidder_Order.Rows.Add(dr);
            ViewState["dtBidder_Order"] = dtBidder_Order;

            grdBidderOrder.DataSource = dtBidder_Order;
            grdBidderOrder.DataBind();
        }
    }

    private void get_PQC()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PQC();
        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["dtPQC"] = ds.Tables[0];

            grdPQC.DataSource = ds.Tables[0];
            grdPQC.DataBind();
        }
        else
        {
            DataTable dtPQC = new DataTable();

            DataColumn dc_ProjectDPRPQC_Id = new DataColumn("ProjectDPRPQC_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_DPR_Id = new DataColumn("ProjectDPRPQC_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCParentName = new DataColumn("ProjectDPRPQC_PQCParentName", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQCName = new DataColumn("ProjectDPRPQC_PQCName", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQC_Mandatory = new DataColumn("ProjectDPRPQC_PQC_Mandatory", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Auto_Calculated = new DataColumn("ProjectDPRPQC_PQC_Auto_Calculated", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Upload_Document_Count = new DataColumn("ProjectDPRPQC_PQC_Upload_Document_Count", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Enable_Verification_Document = new DataColumn("ProjectDPRPQC_PQC_Enable_Verification_Document", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCParent_Id = new DataColumn("ProjectDPRPQC_PQCParent_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Id = new DataColumn("ProjectDPRPQC_PQC_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_Order = new DataColumn("ProjectDPRPQC_Order", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCMinVal = new DataColumn("ProjectDPRPQC_PQCMinVal", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQCMaxVal = new DataColumn("ProjectDPRPQC_PQCMaxVal", typeof(string));
            DataColumn dc_ProjectDPRPQC_Comments = new DataColumn("ProjectDPRPQC_Comments", typeof(string));

            dtPQC.Columns.AddRange(new DataColumn[] { dc_ProjectDPRPQC_Id, dc_ProjectDPRPQC_DPR_Id, dc_ProjectDPRPQC_PQCParentName, dc_ProjectDPRPQC_PQCName, dc_ProjectDPRPQC_PQCMinVal, dc_ProjectDPRPQC_PQCMaxVal, dc_ProjectDPRPQC_Comments, dc_ProjectDPRPQC_PQC_Mandatory, dc_ProjectDPRPQC_PQC_Auto_Calculated, dc_ProjectDPRPQC_PQC_Upload_Document_Count, dc_ProjectDPRPQC_PQC_Enable_Verification_Document, dc_ProjectDPRPQC_PQCParent_Id, dc_ProjectDPRPQC_PQC_Id, dc_ProjectDPRPQC_Order });

            DataRow dr = dtPQC.NewRow();
            dtPQC.Rows.Add(dr);
            ViewState["dtPQC"] = dtPQC;

            grdPQC.DataSource = dtPQC;
            grdPQC.DataBind();
        }
    }

    private void get_tbl_ProjectDPRBidResponse(int DPR_Id)
    {
        DataSet ds = new DataSet();
        if (DPR_Id > 0)
        {
            ds = (new DataLayer()).get_tbl_ProjectDPRBidResponse(DPR_Id);
            if (AllClasses.CheckDataSet(ds))
            {
                ViewState["BidResponse"] = ds.Tables[0];

                grdPreBidResponseDocs.DataSource = ds.Tables[0];
                grdPreBidResponseDocs.DataBind();
            }
            else
            {
                DataTable BidResponse = new DataTable();

                DataColumn dc_ProjectDPRBidResponse_Id = new DataColumn("ProjectDPRBidResponse_Id", typeof(int));
                DataColumn dc_ProjectDPRBidResponse_DPR_Id = new DataColumn("ProjectDPRBidResponse_DPR_Id", typeof(int));
                DataColumn dc_ProjectDPRBidResponse_BidResponseName = new DataColumn("ProjectDPRBidResponse_BidResponseName", typeof(string));
                DataColumn dc_ProjectDPRBidResponse_Comments = new DataColumn("ProjectDPRBidResponse_Comments", typeof(string));
                DataColumn dc_ProjectDPRBidResponse_DocumentPath = new DataColumn("ProjectDPRBidResponse_DocumentPath", typeof(string));

                BidResponse.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidResponse_Id, dc_ProjectDPRBidResponse_DPR_Id, dc_ProjectDPRBidResponse_BidResponseName, dc_ProjectDPRBidResponse_Comments, dc_ProjectDPRBidResponse_DocumentPath });

                DataRow dr = BidResponse.NewRow();
                BidResponse.Rows.Add(dr);
                ViewState["BidResponse"] = BidResponse;

                grdPreBidResponseDocs.DataSource = BidResponse;
                grdPreBidResponseDocs.DataBind();
            }
        }
        else
        {
            DataTable BidResponse = new DataTable();

            DataColumn dc_ProjectDPRBidResponse_Id = new DataColumn("ProjectDPRBidResponse_Id", typeof(int));
            DataColumn dc_ProjectDPRBidResponse_DPR_Id = new DataColumn("ProjectDPRBidResponse_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRBidResponse_BidResponseName = new DataColumn("ProjectDPRBidResponse_BidResponseName", typeof(string));
            DataColumn dc_ProjectDPRBidResponse_Comments = new DataColumn("ProjectDPRBidResponse_Comments", typeof(string));
            DataColumn dc_ProjectDPRBidResponse_DocumentPath = new DataColumn("ProjectDPRBidResponse_DocumentPath", typeof(string));

            BidResponse.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidResponse_Id, dc_ProjectDPRBidResponse_DPR_Id, dc_ProjectDPRBidResponse_BidResponseName, dc_ProjectDPRBidResponse_Comments, dc_ProjectDPRBidResponse_DocumentPath });

            DataRow dr = BidResponse.NewRow();
            BidResponse.Rows.Add(dr);
            ViewState["BidResponse"] = BidResponse;

            grdPreBidResponseDocs.DataSource = BidResponse;
            grdPreBidResponseDocs.DataBind();
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
            if (AllClasses.CheckDt(dtNGT) && dtNGT.Rows.Count == grdNGTDtls.Rows.Count)
            {
                for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
                {
                    TextBox txtFirmName = (grdNGTDtls.Rows[i].FindControl("txtFirmName") as TextBox);
                    TextBox txtFirmPAN = (grdNGTDtls.Rows[i].FindControl("txtFirmPAN") as TextBox);
                    TextBox txtFirmGSTIN = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTIN") as TextBox);
                    TextBox txtContactNo = (grdNGTDtls.Rows[i].FindControl("txtContactNo") as TextBox);
                    TextBox txtShare = (grdNGTDtls.Rows[i].FindControl("txtShare") as TextBox);
                    CheckBox chkGSTNA = (grdNGTDtls.Rows[i].FindControl("chkGSTNA") as CheckBox);

                    TextBox txtFirmNameP = (grdNGTDtls.Rows[i].FindControl("txtFirmNameP") as TextBox);
                    TextBox txtFirmPANP = (grdNGTDtls.Rows[i].FindControl("txtFirmPANP") as TextBox);
                    TextBox txtFirmGSTINP = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTINP") as TextBox);
                    TextBox txtContactNoP = (grdNGTDtls.Rows[i].FindControl("txtContactNoP") as TextBox);
                    TextBox txtShareP = (grdNGTDtls.Rows[i].FindControl("txtShareP") as TextBox);
                    CheckBox chkGSTNAP = (grdNGTDtls.Rows[i].FindControl("chkGSTNAP") as CheckBox);

                    RadioButtonList ddlISJV = (grdNGTDtls.Rows[i].FindControl("ddlISJV") as RadioButtonList);
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_Id"] = 0;
                    }
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_DPR_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[1].Text.Trim());
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_DPR_Id"] = 0;
                    }
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderName"] = txtFirmName.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderPAN"] = txtFirmPAN.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTIN"] = txtFirmGSTIN.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderMobile"] = txtContactNo.Text.Trim();
                    if (chkGSTNA.Checked)
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 0;
                    }
                    else
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 1;
                    }
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShare"] = Convert.ToDecimal(txtShare.Text.Trim());
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShare"] = 0;
                    }
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderNameP"] = txtFirmNameP.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderPANP"] = txtFirmPANP.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTINP"] = txtFirmGSTINP.Text.Trim();
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderMobileP"] = txtContactNoP.Text.Trim();
                    if (chkGSTNAP.Checked)
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 0;
                    }
                    else
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 1;
                    }
                    try
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShareP"] = txtShareP.Text.Trim();
                    }
                    catch
                    {
                        dtNGT.Rows[i]["ProjectDPRBidder_BidderShareP"] = 0;
                    }
                    dtNGT.Rows[i]["ProjectDPRBidder_BidderAmount"] = 0;
                    dtNGT.Rows[i]["ProjectDPRBidder_Comments"] = "";
                    dtNGT.Rows[i]["ProjectDPRBidder_TechnicalQualified"] = 0;
                    dtNGT.Rows[i]["ProjectDPRBidder_FinancialQualified"] = 0;
                    dtNGT.Rows[i]["ProjectDPRBidder_Is_JV"] = ddlISJV.SelectedValue.Trim();
                }
            }
            DataRow dr = dtNGT.NewRow();
            dtNGT.Rows.Add(dr);
            ViewState["dtNGT"] = dtNGT;

            grdNGTDtls.DataSource = dtNGT;
            grdNGTDtls.DataBind();
        }
        else
        {
            dtNGT = new DataTable();

            DataColumn dc_ProjectDPRBidder_Id = new DataColumn("ProjectDPRBidder_Id", typeof(int));
            DataColumn dc_ProjectDPRBidder_DPR_Id = new DataColumn("ProjectDPRBidder_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRBidder_BidderName = new DataColumn("ProjectDPRBidder_BidderName", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderPAN = new DataColumn("ProjectDPRBidder_BidderPAN", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderGSTIN = new DataColumn("ProjectDPRBidder_BidderGSTIN", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderMobile = new DataColumn("ProjectDPRBidder_BidderMobile", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderShare = new DataColumn("ProjectDPRBidder_BidderShare", typeof(decimal));
            DataColumn dc_ProjectDPRBidder_BidderNameP = new DataColumn("ProjectDPRBidder_BidderNameP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderPANP = new DataColumn("ProjectDPRBidder_BidderPANP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderGSTINP = new DataColumn("ProjectDPRBidder_BidderGSTINP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderMobileP = new DataColumn("ProjectDPRBidder_BidderMobileP", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderShareP = new DataColumn("ProjectDPRBidder_BidderShareP", typeof(decimal));
            DataColumn dc_ProjectDPRBidder_BidderAmount = new DataColumn("ProjectDPRBidder_BidderAmount", typeof(decimal));
            DataColumn dc_ProjectDPRBidder_Comments = new DataColumn("ProjectDPRBidder_Comments", typeof(string));
            DataColumn dc_ProjectDPRBidder_TechnicalQualified = new DataColumn("ProjectDPRBidder_TechnicalQualified", typeof(int));
            DataColumn dc_ProjectDPRBidder_FinancialQualified = new DataColumn("ProjectDPRBidder_FinancialQualified", typeof(int));
            DataColumn dc_ProjectDPRBidder_Is_JV = new DataColumn("ProjectDPRBidder_Is_JV", typeof(string));
            DataColumn dc_ProjectDPRBidder_BidderGSTIN_Available = new DataColumn("ProjectDPRBidder_BidderGSTIN_Available", typeof(int));
            DataColumn dc_ProjectDPRBidder_BidderGSTINP_Available = new DataColumn("ProjectDPRBidder_BidderGSTINP_Available", typeof(int));
            dtNGT.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidder_Id, dc_ProjectDPRBidder_DPR_Id, dc_ProjectDPRBidder_BidderName, dc_ProjectDPRBidder_BidderPAN, dc_ProjectDPRBidder_BidderGSTIN, dc_ProjectDPRBidder_BidderMobile, dc_ProjectDPRBidder_BidderShare, dc_ProjectDPRBidder_BidderNameP, dc_ProjectDPRBidder_BidderPANP, dc_ProjectDPRBidder_BidderGSTINP, dc_ProjectDPRBidder_BidderMobileP, dc_ProjectDPRBidder_BidderShareP, dc_ProjectDPRBidder_BidderAmount, dc_ProjectDPRBidder_Comments, dc_ProjectDPRBidder_TechnicalQualified, dc_ProjectDPRBidder_FinancialQualified, dc_ProjectDPRBidder_BidderGSTIN_Available, dc_ProjectDPRBidder_BidderGSTINP_Available });

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
            if (AllClasses.CheckDt(dt) && dt.Rows.Count == grdNGTDtls.Rows.Count)
            {
                for (int i = 0; i < grdNGTDtls.Rows.Count; i++)
                {
                    TextBox txtFirmName = (grdNGTDtls.Rows[i].FindControl("txtFirmName") as TextBox);
                    TextBox txtFirmPAN = (grdNGTDtls.Rows[i].FindControl("txtFirmPAN") as TextBox);
                    TextBox txtFirmGSTIN = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTIN") as TextBox);
                    TextBox txtContactNo = (grdNGTDtls.Rows[i].FindControl("txtContactNo") as TextBox);
                    TextBox txtShare = (grdNGTDtls.Rows[i].FindControl("txtShare") as TextBox);
                    CheckBox chkGSTNA = (grdNGTDtls.Rows[i].FindControl("chkGSTNA") as CheckBox);

                    TextBox txtFirmNameP = (grdNGTDtls.Rows[i].FindControl("txtFirmNameP") as TextBox);
                    TextBox txtFirmPANP = (grdNGTDtls.Rows[i].FindControl("txtFirmPANP") as TextBox);
                    TextBox txtFirmGSTINP = (grdNGTDtls.Rows[i].FindControl("txtFirmGSTINP") as TextBox);
                    TextBox txtContactNoP = (grdNGTDtls.Rows[i].FindControl("txtContactNoP") as TextBox);
                    TextBox txtShareP = (grdNGTDtls.Rows[i].FindControl("txtShareP") as TextBox);
                    CheckBox chkGSTNAP = (grdNGTDtls.Rows[i].FindControl("chkGSTNAP") as CheckBox);

                    RadioButtonList ddlISJV = (grdNGTDtls.Rows[i].FindControl("ddlISJV") as RadioButtonList);
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_Id"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_DPR_Id"] = Convert.ToInt32(grdNGTDtls.Rows[i].Cells[1].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_DPR_Id"] = 0;
                    }
                    dt.Rows[i]["ProjectDPRBidder_BidderName"] = txtFirmName.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderPAN"] = txtFirmPAN.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderGSTIN"] = txtFirmGSTIN.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderMobile"] = txtContactNo.Text.Trim();
                    if (chkGSTNA.Checked)
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTIN_Available"] = 1;
                    }
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShare"] = Convert.ToDecimal(txtShare.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShare"] = 0;
                    }
                    dt.Rows[i]["ProjectDPRBidder_BidderNameP"] = txtFirmNameP.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderPANP"] = txtFirmPANP.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderGSTINP"] = txtFirmGSTINP.Text.Trim();
                    dt.Rows[i]["ProjectDPRBidder_BidderMobileP"] = txtContactNoP.Text.Trim();
                    if (chkGSTNAP.Checked)
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderGSTINP_Available"] = 1;
                    }
                    try
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShareP"] = txtShareP.Text.Trim();
                    }
                    catch
                    {
                        dt.Rows[i]["ProjectDPRBidder_BidderShareP"] = 0;
                    }
                    dt.Rows[i]["ProjectDPRBidder_BidderAmount"] = 0;
                    dt.Rows[i]["ProjectDPRBidder_Comments"] = "";
                    dt.Rows[i]["ProjectDPRBidder_TechnicalQualified"] = 0;
                    dt.Rows[i]["ProjectDPRBidder_FinancialQualified"] = 0;
                    dt.Rows[i]["ProjectDPRBidder_Is_JV"] = ddlISJV.SelectedValue.Trim();
                }
            }
            if (dt.Rows.Count > 1)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);
                grdNGTDtls.DataSource = dt;
                grdNGTDtls.DataBind();
                ViewState["dtNGT"] = dt;
            }
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStep9_PQC.Visible = false;
        int DPRStep_Id = 0;
        try
        {
            DPRStep_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        catch
        {
            DPRStep_Id = 0;
        }
        if (DPRStep_Id == 0)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 1)
        {
            divStep1.Visible = true;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 2)
        {
            divStep1.Visible = false;
            divStep2.Visible = true;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 3)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = true;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 4)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = true;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 5)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = true;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 6)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = true;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;

            get_tbl_ProjectDPRPQC(Convert.ToInt32(hf_ProjectDPR_Id.Value));
        }
        else if (DPRStep_Id == 7)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = true;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 8)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = true;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 9)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = true;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;

            get_Bidder_Details(Convert.ToInt32(hf_ProjectDPR_Id.Value), grdBidderDetails, 0);

        }
        else if (DPRStep_Id == 10)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = true;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;

            get_Bidder_Details(Convert.ToInt32(hf_ProjectDPR_Id.Value), grdBiddersFinancial, 1);
        }
        else if (DPRStep_Id == 11)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = true;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 12)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = true;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 13)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = true;
            divStep14.Visible = false;
        }
        else if (DPRStep_Id == 14)
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = true;
        }
        else
        {
            divStep1.Visible = false;
            divStep2.Visible = false;
            divStep3.Visible = false;
            divStep4.Visible = false;
            divStep5.Visible = false;
            divStep6.Visible = false;
            divStep7.Visible = false;
            divStep8.Visible = false;
            divStep9.Visible = false;
            divStep10.Visible = false;
            divStep11.Visible = false;
            divStep12.Visible = false;
            divStep13.Visible = false;
            divStep14.Visible = false;
        }
    }

    private void get_Bidder_Details(int DPR_Id, GridView grd, int Is_Technically_Qualified)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectDPRBidder(DPR_Id, Is_Technically_Qualified);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grd.DataSource = ds.Tables[0];
            grd.DataBind();

            int _Bidder_Id = 0;
            int Bidder_Id = 0;
            try
            {
                _Bidder_Id = Convert.ToInt32(hf_ProjectDPRBidder_Id.Value);
            }
            catch
            {
                _Bidder_Id = 0;
            }
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                try
                {
                    Bidder_Id = Convert.ToInt32(grd.Rows[i].Cells[0].Text.Trim());
                }
                catch
                {
                    Bidder_Id = 0;
                }
                if (Bidder_Id > 0 && _Bidder_Id > 0 && Bidder_Id == _Bidder_Id)
                {
                    LinkButton btnUpdatePQC = grd.Rows[i].FindControl("btnUpdatePQC") as LinkButton;
                    if (btnUpdatePQC != null)
                    {
                        btnUpdatePQC_Click(btnUpdatePQC, new EventArgs());
                    }
                }
            }
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }
    }

    private void get_tbl_ProjectDPRPQC(int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectDPRPQC(DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtPQC"] = ds.Tables[0];

            grdQualificationCriteriaR.DataSource = ds.Tables[0];
            grdQualificationCriteriaR.DataBind();
        }
        else
        {
            grdQualificationCriteriaR.DataSource = null;
            grdQualificationCriteriaR.DataBind();
        }
    }

    protected void grdBidderDetails_PreRender(object sender, EventArgs e)
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

    protected void grdDPRTimeline_PreRender(object sender, EventArgs e)
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

    protected void grdBidder_PreRender(object sender, EventArgs e)
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

    protected void lnkBidder_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }

        int Step_Status = 0;
        try
        {
            Step_Status = Convert.ToInt32(gr.Cells[2].Text.Trim());
        }
        catch
        {
            Step_Status = 0;
        }

        DataSet ds = new DataSet();

        ds = (new DataLayer()).get_tbl_ProjectDPRTender(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdEFC_PFAD.DataSource = ds.Tables[0];
            grdEFC_PFAD.DataBind();

            grdGODetails.DataSource = ds.Tables[0];
            grdGODetails.DataBind();

            grdCommonStep.DataSource = ds.Tables[0];
            grdCommonStep.DataBind();

            grdTenderPublished.DataSource = ds.Tables[0];
            grdTenderPublished.DataBind();
        }
        else
        {
            grdEFC_PFAD.DataSource = null;
            grdEFC_PFAD.DataBind();

            grdGODetails.DataSource = null;
            grdGODetails.DataBind();

            grdCommonStep.DataSource = null;
            grdCommonStep.DataBind();

            grdTenderPublished.DataSource = null;
            grdTenderPublished.DataBind();
        }

        ds = (new DataLayer()).get_tbl_ProjectDPRBidder(Convert.ToInt32(hf_ProjectDPR_Id.Value), 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdBidder.DataSource = ds.Tables[0];
            grdBidder.DataBind();
        }
        else
        {
            grdBidder.DataSource = null;
            grdBidder.DataBind();
        }

        ds = (new DataLayer()).get_tbl_ProjectDPRPQC(Convert.ToInt32(hf_ProjectDPR_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdQualificationCriteria.DataSource = ds.Tables[0];
            grdQualificationCriteria.DataBind();
        }
        else
        {
            grdQualificationCriteria.DataSource = null;
            grdQualificationCriteria.DataBind();
        }

        ds = (new DataLayer()).get_tbl_ProjectDPRBidResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value));
        if (AllClasses.CheckDataSet(ds))
        {
            grdPBMResponseDoc.DataSource = ds.Tables[0];
            grdPBMResponseDoc.DataBind();
        }
        else
        {
            grdPBMResponseDoc.DataSource = null;
            grdPBMResponseDoc.DataBind();
        }

        div_Report_Step1.Visible = false;
        div_Report_Step2.Visible = false;
        div_Report_Step5.Visible = false;
        div_Report_Step_Common.Visible = false;
        divBidder.Visible = false;
        if (Step_Status == 1)
        {
            div_Report_Step1.Visible = true;
        }
        if (Step_Status == 2)
        {
            div_Report_Step2.Visible = true;
        }
        if (Step_Status == 3)
        {
            div_Report_Step_Common.Visible = true;
            if (grdCommonStep.Rows.Count > 0)
                grdCommonStep.Columns[3].HeaderText = "NIT Issue Date";
        }
        if (Step_Status == 5)
        {
            div_Report_Step5.Visible = true;
        }
        if (Step_Status == 6)
        {
            div_Report_Step5.Visible = true;
            if (grdCommonStep.Rows.Count > 0)
            {
                grdTenderPublished.Columns[4].HeaderText = "Pre Bid Meeting On Date";
                grdTenderPublished.Columns[5].HeaderText = "Corrigendum Refrence No";
                grdTenderPublished.Columns[6].HeaderText = "Revised Closing / End Date";
                grdTenderPublished.Columns[7].HeaderText = "Revised Technical Bid Opening Date";
                grdTenderPublished.Columns[9].HeaderText = "Corrigendum Document";
                grdTenderPublished.Columns[9].AccessibleHeaderText = "Corrigendum Document";
                grdTenderPublished.Columns[8].Visible = false;
                grdTenderPublished.Columns[10].Visible = false;
            }
        }
        if (Step_Status == 8)
        {
            divBidder.Visible = true;
        }
        if (Step_Status == 9)
        {
            divBidder.Visible = true;
        }
        if (Step_Status == 10)
        {
            divBidder.Visible = true;
        }
        if (Step_Status == 11)
        {
            div_Report_Step_Common.Visible = true;
        }
        if (Step_Status == 12)
        {
            div_Report_Step_Common.Visible = true;
        }
        if (Step_Status == 13)
        {
            div_Report_Step_Common.Visible = true;
        }
        if (Step_Status == 14)
        {
            div_Report_Step_Common.Visible = true;
        }
        mpBidder.Show();
    }

    protected void grdPQC_PreRender(object sender, EventArgs e)
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

    protected void btnAddPQC_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPQC;
        if (ViewState["dtPQC"] != null)
        {
            dtPQC = (DataTable)(ViewState["dtPQC"]);

            if (AllClasses.CheckDt(dtPQC) && dtPQC.Rows.Count == grdBidderOrder.Rows.Count)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    Label txtPQCDetails = (grdBidderOrder.Rows[i].FindControl("txtPQCDetails") as Label);
                    TextBox txtPQCDetails1 = (grdBidderOrder.Rows[i].FindControl("txtPQCDetails1") as TextBox);
                    TextBox txtMinValue = (grdBidderOrder.Rows[i].FindControl("txtMinValue") as TextBox);
                    TextBox txtMaxValue = (grdBidderOrder.Rows[i].FindControl("txtMaxValue") as TextBox);
                    TextBox txtComments = (grdBidderOrder.Rows[i].FindControl("txtComments") as TextBox);
                    CheckBox chkApplicable = (grdBidderOrder.Rows[i].FindControl("chkApplicable") as CheckBox);

                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_DPR_Id"] = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_DPR_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Mandatory"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[2].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Mandatory"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Enable_Verification_Document"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[3].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Enable_Verification_Document"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Upload_Document_Count"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[4].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Upload_Document_Count"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Auto_Calculated"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[5].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Auto_Calculated"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCParent_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[6].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCParent_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[7].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Order"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[8].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Order"] = 0;
                    }
                    dtPQC.Rows[i]["ProjectDPRPQC_PQCParentName"] = grdBidderOrder.Rows[i].Cells[11].Text.Trim();
                    if (Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim()) > 0)
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCName"] = txtPQCDetails.Text.Trim();
                    }
                    else
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCName"] = txtPQCDetails1.Text.Trim();
                    }

                    dtPQC.Rows[i]["ProjectDPRPQC_PQCMinVal"] = txtMinValue.Text.Trim();
                    dtPQC.Rows[i]["ProjectDPRPQC_PQCMaxVal"] = txtMaxValue.Text.Trim();
                    dtPQC.Rows[i]["ProjectDPRPQC_Comments"] = txtComments.Text.Trim();
                }
            }


            DataRow dr = dtPQC.NewRow();
            dtPQC.Rows.Add(dr);
            ViewState["dtPQC"] = dtPQC;

            grdPQC.DataSource = dtPQC;
            grdPQC.DataBind();
        }
        else
        {
            dtPQC = new DataTable();

            DataColumn dc_ProjectDPRPQC_Id = new DataColumn("ProjectDPRPQC_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_DPR_Id = new DataColumn("ProjectDPRPQC_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCParentName = new DataColumn("ProjectDPRPQC_PQCParentName", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQCName = new DataColumn("ProjectDPRPQC_PQCName", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQC_Mandatory = new DataColumn("ProjectDPRPQC_PQC_Mandatory", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Auto_Calculated = new DataColumn("ProjectDPRPQC_PQC_Auto_Calculated", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Upload_Document_Count = new DataColumn("ProjectDPRPQC_PQC_Upload_Document_Count", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Enable_Verification_Document = new DataColumn("ProjectDPRPQC_PQC_Enable_Verification_Document", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCParent_Id = new DataColumn("ProjectDPRPQC_PQCParent_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Id = new DataColumn("ProjectDPRPQC_PQC_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_Order = new DataColumn("ProjectDPRPQC_Order", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCMinVal = new DataColumn("ProjectDPRPQC_PQCMinVal", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQCMaxVal = new DataColumn("ProjectDPRPQC_PQCMaxVal", typeof(string));
            DataColumn dc_ProjectDPRPQC_Comments = new DataColumn("ProjectDPRPQC_Comments", typeof(string));

            dtPQC.Columns.AddRange(new DataColumn[] { dc_ProjectDPRPQC_Id, dc_ProjectDPRPQC_DPR_Id, dc_ProjectDPRPQC_PQCParentName, dc_ProjectDPRPQC_PQCName, dc_ProjectDPRPQC_PQCMinVal, dc_ProjectDPRPQC_PQCMaxVal, dc_ProjectDPRPQC_Comments, dc_ProjectDPRPQC_PQC_Mandatory, dc_ProjectDPRPQC_PQC_Auto_Calculated, dc_ProjectDPRPQC_PQC_Upload_Document_Count, dc_ProjectDPRPQC_PQC_Enable_Verification_Document, dc_ProjectDPRPQC_PQCParent_Id, dc_ProjectDPRPQC_PQC_Id, dc_ProjectDPRPQC_Order });

            DataRow dr = dtPQC.NewRow();
            dtPQC.Rows.Add(dr);
            ViewState["dtPQC"] = dtPQC;

            grdPQC.DataSource = dtPQC;
            grdPQC.DataBind();
        }
    }

    protected void btnMinusPQC_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtPQC"] != null)
        {
            DataTable dtPQC = (DataTable)ViewState["dtPQC"];

            if (AllClasses.CheckDt(dtPQC) && dtPQC.Rows.Count == grdBidderOrder.Rows.Count)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    Label txtPQCDetails = (grdBidderOrder.Rows[i].FindControl("txtPQCDetails") as Label);
                    TextBox txtPQCDetails1 = (grdBidderOrder.Rows[i].FindControl("txtPQCDetails1") as TextBox);
                    TextBox txtMinValue = (grdBidderOrder.Rows[i].FindControl("txtMinValue") as TextBox);
                    TextBox txtMaxValue = (grdBidderOrder.Rows[i].FindControl("txtMaxValue") as TextBox);
                    TextBox txtComments = (grdBidderOrder.Rows[i].FindControl("txtComments") as TextBox);
                    CheckBox chkApplicable = (grdBidderOrder.Rows[i].FindControl("chkApplicable") as CheckBox);

                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_DPR_Id"] = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_DPR_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Mandatory"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[2].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Mandatory"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Enable_Verification_Document"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[3].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Enable_Verification_Document"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Upload_Document_Count"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[4].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Upload_Document_Count"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Auto_Calculated"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[5].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Auto_Calculated"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCParent_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[6].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCParent_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[7].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQC_Id"] = 0;
                    }
                    try
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Order"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[8].Text.Trim());
                    }
                    catch
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_Order"] = 0;
                    }
                    dtPQC.Rows[i]["ProjectDPRPQC_PQCParentName"] = grdBidderOrder.Rows[i].Cells[11].Text.Trim();
                    if (Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim()) > 0)
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCName"] = txtPQCDetails.Text.Trim();
                    }
                    else
                    {
                        dtPQC.Rows[i]["ProjectDPRPQC_PQCName"] = txtPQCDetails1.Text.Trim();
                    }
                    dtPQC.Rows[i]["ProjectDPRPQC_PQCMinVal"] = txtMinValue.Text.Trim();
                    dtPQC.Rows[i]["ProjectDPRPQC_PQCMaxVal"] = txtMaxValue.Text.Trim();
                    dtPQC.Rows[i]["ProjectDPRPQC_Comments"] = txtComments.Text.Trim();
                }
            }

            if (dtPQC.Rows.Count > 1)
            {
                dtPQC.Rows.RemoveAt(dtPQC.Rows.Count - 1);
                grdPQC.DataSource = dtPQC;
                grdPQC.DataBind();
                ViewState["dtPQC"] = dtPQC;
            }
        }
    }

    protected void grdPQCAnswer_PreRender(object sender, EventArgs e)
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

    protected void btnUpdatePQC_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        for (int i = 0; i < grdBidderDetails.Rows.Count; i++)
        {
            grdBidderDetails.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        btnSaveBidAnswer.Visible = false;
        int ProjectDPRBidder_Id = 0;
        try
        {
            ProjectDPRBidder_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectDPRBidder_Id = 0;
        }
        string ProjectDPRBidder_Is_JV = "";
        ProjectDPRBidder_Is_JV = gr.Cells[7].Text.Trim();
        if (ProjectDPRBidder_Is_JV == "")
        {
            ProjectDPRBidder_Is_JV = "No";
        }
        hf_BidderType.Value = ProjectDPRBidder_Is_JV;
        if (ProjectDPRBidder_Id > 0)
        {
            hf_ProjectDPRBidder_Id.Value = ProjectDPRBidder_Id.ToString();
            divStep9_PQC.Visible = true;

            DataSet dsFilled = new DataSet();
            dsFilled = new DataLayer().get_tbl_ProjectDPRPQCResponse_All_Filled(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id);
            if (AllClasses.CheckDataSet(dsFilled))
            {
                int ProjectDPRPQC_PQC_Id = 0;
                int Is_Filled = 0;
                bool Is_Remaining = false;
                for (int i = 0; i < dsFilled.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        ProjectDPRPQC_PQC_Id = Convert.ToInt32(dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString());
                    }
                    catch
                    {
                        ProjectDPRPQC_PQC_Id = 0;
                    }
                    try
                    {
                        Is_Filled = Convert.ToInt32(dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString());
                    }
                    catch
                    {
                        Is_Filled = 0;
                    }
                    if (Is_Filled == 0)
                    {
                        Is_Remaining = true;
                        break;
                    }
                }

                if (!Is_Remaining)
                {
                    ProjectDPRPQC_PQC_Id = 0;
                }
                Set_Steps_Rings(dsFilled);
                DataSet ds = new DataSet();
                if (ProjectDPRPQC_PQC_Id > 0)
                {
                    ds = new DataLayer().get_tbl_ProjectDPRPQCResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id, ProjectDPRPQC_PQC_Id);
                    if (AllClasses.CheckDataSet(ds))
                    {
                        btnEvaluate.Visible = true;
                        grdPQCAnswer.DataSource = ds.Tables[0];
                        grdPQCAnswer.DataBind();
                    }
                    else
                    {
                        btnEvaluate.Visible = false;
                        grdPQCAnswer.DataSource = null;
                        grdPQCAnswer.DataBind();
                    }
                }
                else
                {
                    grdPQCAnswer.DataSource = null;
                    grdPQCAnswer.DataBind();
                    btnEvaluate.Visible = false;
                    MessageBox.Show("Bidders Evaluation Steps Completed....");
                }
                if (ProjectDPRPQC_PQC_Id == 3 || ProjectDPRPQC_PQC_Id == 10 || ProjectDPRPQC_PQC_Id == 11)
                {
                    divStep9_PQC_Work_Order.Visible = true;
                    chkOrderNotAvailable.Visible = true;
                }
                else
                {
                    divStep9_PQC_Work_Order.Visible = false;
                    chkOrderNotAvailable.Visible = false;
                }
                get_tbl_ProjectDPR_Bidder_Order(ProjectDPRBidder_Id);
            }
            else
            {
                MessageBox.Show("Some Error Occured Please Contact Administrator...!!");
                return;
            }
        }
        else
        {
            hf_ProjectDPRBidder_Id.Value = "0";
            divStep9_PQC.Visible = false;
            MessageBox.Show("Details Not Found");
            return;
        }
    }

    private void Set_Steps_Rings(DataSet dsFilled)
    {
        string _inner = "";
        _inner += "<ul class='steps' style='margin-left: 0'>";
        bool step_1 = false;
        bool step_2 = false;
        bool step_3 = false;
        bool step_4 = false;
        bool step_5 = false;
        bool step_6 = false;
        bool step_7 = false;
        bool step_8 = false;

        for (int i = 0; i < dsFilled.Tables[0].Rows.Count; i++)
        {
            if (!step_1)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "1")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='1' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='1'>";
                    }
                    _inner += "        <span class='step'>1</span>";
                    _inner += "        <span class='title'>Tender Fees</span>";
                    _inner += "    </li>";
                    step_1 = true;
                }
            }

            if (!step_2)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "2")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='2' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='2'>";
                    }
                    _inner += "        <span class='step'>2</span>";
                    _inner += "        <span class='title'>EMD</span>";
                    _inner += "    </li>";
                    step_2 = true;
                }
            }

            if (!step_3)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "5")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='3' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='3'>";
                    }
                    _inner += "        <span class='step'>3</span>";
                    _inner += "        <span class='title'>Net Worth</span>";
                    _inner += "    </li>";
                    step_3 = true;
                }
            }

            if (!step_4)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "9")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='4' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='4'>";
                    }
                    _inner += "        <span class='step'>4</span>";
                    _inner += "        <span class='title'>Solvency</span>";
                    _inner += "    </li>";
                    step_4 = true;
                }
            }

            if (!step_5)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "4")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='5' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='5'>";
                    }
                    _inner += "        <span class='step'>5</span>";
                    _inner += "        <span class='title'>Turnover</span>";
                    _inner += "    </li>";
                    step_5 = true;
                }
            }

            if (!step_6)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "7")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='6' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='6'>";
                    }
                    _inner += "        <span class='step'>6</span>";
                    _inner += "        <span class='title'>Last 3 Years ITR</span>";
                    _inner += "    </li>";
                    step_6 = true;
                }
            }

            if (!step_7)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "6")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='7' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='7'>";
                    }
                    _inner += "        <span class='step'>7</span>";
                    _inner += "        <span class='title'>BID Capacity</span>";
                    _inner += "    </li>";
                    step_7 = true;
                }
            }

            if (!step_8)
            {
                if (dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "3" || dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "10" || dsFilled.Tables[0].Rows[i]["ProjectDPRPQC_PQC_Id"].ToString() == "11")
                {
                    if (dsFilled.Tables[0].Rows[i]["Is_Filled"].ToString() == "1")
                    {
                        _inner += "    <li data-step='8' class='active'>";
                    }
                    else
                    {
                        _inner += "    <li data-step='8'>";
                    }
                    _inner += "        <span class='step'>8</span>";
                    _inner += "        <span class='title'>Similar completed and commissioned Work</span>";
                    _inner += "    </li>";
                    step_8 = true;
                }
            }
        }
        _inner += "</ul>";
        divStepsCounter.InnerHtml = _inner;
    }

    protected void btnSaveBidAnswer_Click(object sender, EventArgs e)
    {
        List<tbl_ProjectDPRPQCResponse> obj_tbl_ProjectDPRPQCResponse_Li = new List<tbl_ProjectDPRPQCResponse>();
        for (int i = 0; i < grdPQCAnswer.Rows.Count; i++)
        {
            tbl_ProjectDPRPQCResponse obj_tbl_ProjectDPRPQCResponse = new tbl_ProjectDPRPQCResponse();

            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id = Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id = 0;
            }
            if (!chkOrderNotAvailable.Checked)
            {
                if (obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id == 11 || obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id == 10 || obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id == 3)
                {
                    DataSet dsOrder = new DataSet();
                    dsOrder = new DataLayer().get_tbl_ProjectDPR_Bidder_Order(Convert.ToInt32(hf_ProjectDPR_Id.Value), Convert.ToInt32(hf_ProjectDPRBidder_Id.Value));
                    if (AllClasses.CheckDataSet(dsOrder))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please Fill Bidders Work Order Details Before Evaluation.");
                        return;
                    }
                }
            }
            FileUpload flDocs1 = (grdPQCAnswer.Rows[i].FindControl("flDocs1") as FileUpload);
            FileUpload flDocs2 = (grdPQCAnswer.Rows[i].FindControl("flDocs2") as FileUpload);
            FileUpload flDocs3 = (grdPQCAnswer.Rows[i].FindControl("flDocs3") as FileUpload);

            FileUpload flVDocs1 = (grdPQCAnswer.Rows[i].FindControl("flVDocs1") as FileUpload);
            FileUpload flVDocs2 = (grdPQCAnswer.Rows[i].FindControl("flVDocs2") as FileUpload);
            FileUpload flVDocs3 = (grdPQCAnswer.Rows[i].FindControl("flVDocs3") as FileUpload);

            TextBox txtBidderValue = (grdPQCAnswer.Rows[i].FindControl("txtBidderValue") as TextBox);

            CheckBox chkBidderValueNA = (grdPQCAnswer.Rows[i].FindControl("chkBidderValueNA") as CheckBox);

            CheckBox chkVerified1 = (grdPQCAnswer.Rows[i].FindControl("chkVerified1") as CheckBox);
            CheckBox chkVerified2 = (grdPQCAnswer.Rows[i].FindControl("chkVerified2") as CheckBox);
            CheckBox chkVerified3 = (grdPQCAnswer.Rows[i].FindControl("chkVerified3") as CheckBox);

            DropDownList ddlPaymentMode = (grdPQCAnswer.Rows[i].FindControl("ddlPaymentMode") as DropDownList);
            TextBox txtRefNo = (grdPQCAnswer.Rows[i].FindControl("txtRefNo") as TextBox);
            TextBox txtBankName = (grdPQCAnswer.Rows[i].FindControl("txtBankName") as TextBox);
            TextBox txtRefDate = (grdPQCAnswer.Rows[i].FindControl("txtRefDate") as TextBox);

            if (txtBidderValue.ReadOnly = false && txtBidderValue.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Bidders Value In This Qualification Criteria!!");
                txtBidderValue.Focus();
                return;
            }
            if (!chkBidderValueNA.Checked)
            {
                if (flDocs1.Visible && !flDocs1.HasFile)
                {
                    MessageBox.Show("Please Upload Relavent Document!!");
                    flDocs1.Focus();
                    return;
                }
                if (flVDocs1.Visible && !flVDocs1.HasFile)
                {
                    MessageBox.Show("Please Upload Relavent Verification Document!!");
                    flVDocs1.Focus();
                    return;
                }

                if (flDocs2.Visible && !flDocs2.HasFile)
                {
                    MessageBox.Show("Please Upload Relavent Document!!");
                    flDocs2.Focus();
                    return;
                }
                if (flVDocs2.Visible && !flVDocs2.HasFile)
                {
                    MessageBox.Show("Please Upload Relavent Verification Document!!");
                    flVDocs2.Focus();
                    return;
                }

                if (flDocs3.Visible && !flDocs3.HasFile)
                {
                    MessageBox.Show("Please Upload Relavent Document!!");
                    flDocs3.Focus();
                    return;
                }
                if (flVDocs3.Visible && !flVDocs3.HasFile)
                {
                    MessageBox.Show("Please Upload Relavent Verification Document!!");
                    flVDocs3.Focus();
                    return;
                }
            }
            if (chkBidderValueNA.Checked)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_BidderValueNotSubmitted = 1;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_BidderValueNotSubmitted = 0;
            }
            obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Status = 1;
            if (grdPQCAnswer.Rows[i].Cells[19].BackColor == Color.LightGreen)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Is_Verified = 1;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Is_Verified = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Id = Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[2].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_DPR_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_DPRPQC_Id = Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_DPRPQC_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id = Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Criteria_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_DPRBidder_Id = Convert.ToInt32(hf_ProjectDPRBidder_Id.Value);
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_DPRBidder_Id = 0;
            }
            if (flDocs1.HasFile)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePath_Byts1 = flDocs1.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePath1 = grdPQCAnswer.Rows[i].Cells[4].Text.Replace("&nbsp;", "").Trim();
            }
            if (flDocs2.HasFile)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePath_Byts2 = flDocs2.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePath2 = grdPQCAnswer.Rows[i].Cells[5].Text.Replace("&nbsp;", "").Trim();
            }
            if (flDocs3.HasFile)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePath_Byts3 = flDocs3.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePath3 = grdPQCAnswer.Rows[i].Cells[6].Text.Replace("&nbsp;", "").Trim();
            }
            if (flVDocs1.HasFile)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePathV_Byts1 = flVDocs1.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePathV1 = grdPQCAnswer.Rows[i].Cells[16].Text.Replace("&nbsp;", "").Trim();
            }
            if (flVDocs2.HasFile)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePathV_Byts2 = flVDocs2.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePathV2 = grdPQCAnswer.Rows[i].Cells[17].Text.Replace("&nbsp;", "").Trim();
            }
            if (flVDocs3.HasFile)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePathV_Byts3 = flVDocs3.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FilePathV3 = grdPQCAnswer.Rows[i].Cells[18].Text.Replace("&nbsp;", "").Trim();
            }
            if (chkVerified1.Checked)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FileVerified1 = 1;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FileVerified1 = 0;
            }
            if (chkVerified2.Checked)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FileVerified2 = 1;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FileVerified2 = 0;
            }
            if (chkVerified3.Checked)
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FileVerified3 = 1;
            }
            else
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_FileVerified3 = 0;
            }
            obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_Response = txtBidderValue.Text.Trim();
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_PaymentMode = ddlPaymentMode.SelectedValue;
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_PaymentMode = "";
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_RefrenceNo = txtRefNo.Text.Trim();
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_RefrenceNo = "";
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_BankName = txtBankName.Text.Trim();
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_BankName = "";
            }
            try
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_RefDate = txtBidRefrenceNo.Text.Trim();
            }
            catch
            {
                obj_tbl_ProjectDPRPQCResponse.ProjectDPRPQCResponse_RefDate = "";
            }
            obj_tbl_ProjectDPRPQCResponse_Li.Add(obj_tbl_ProjectDPRPQCResponse);
        }
        if (new DataLayer().Insert_tbl_ProjectDPRPQCResponse(obj_tbl_ProjectDPRPQCResponse_Li))
        {
            MessageBox.Show("Bidders Qualification Details Saved Successfully");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving Bidders Qualification Details");
            return;
        }
    }

    protected void grdQualificationCriteriaR_PreRender(object sender, EventArgs e)
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

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int ProjectDPRTender_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().delete_tbl_ProjectDPRTender(ProjectDPRTender_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            int DPR_Id = 0;
            try
            {
                DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
            }
            catch
            {
                DPR_Id = 0;
            }
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_ProjectDPRTender(DPR_Id, 0);
            if (AllClasses.CheckDataSet(ds))
            {
                divStep1.Visible = false;
                divStep2.Visible = false;
                divStep3.Visible = false;
                divStep4.Visible = false;
                divStep5.Visible = false;
                divStep6.Visible = false;
                divStep7.Visible = false;
                divStep8.Visible = false;
                divStep9.Visible = false;
                divStep10.Visible = false;
                divStep11.Visible = false;
                divStep12.Visible = false;
                divStep13.Visible = false;
                divStep14.Visible = false;

                grdDPRTimeline.DataSource = ds.Tables[0];
                grdDPRTimeline.DataBind();

                for (int i = 0; i < grdDPRTimeline.Rows.Count; i++)
                {
                    ImageButton btnDelete = grdDPRTimeline.Rows[i].FindControl("btnDelete") as ImageButton;
                    if (i == grdDPRTimeline.Rows.Count - 1)
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }

                    int Step_Status = 0;
                    try
                    {
                        Step_Status = Convert.ToInt32(grdDPRTimeline.Rows[i].Cells[2].Text.Trim());
                    }
                    catch
                    {
                        Step_Status = 0;
                    }
                    LinkButton lnkActionCorrigendum = grdDPRTimeline.Rows[i].FindControl("lnkActionCorrigendum") as LinkButton;
                    if (Step_Status == 6 && i == grdDPRTimeline.Rows.Count - 1)
                    {
                        lnkActionCorrigendum.Visible = true;
                    }
                    else
                    {
                        lnkActionCorrigendum.Visible = false;
                    }
                }
                btnSearch_Click(btnSearch, new EventArgs());
            }
            else
            {
                grdDPRTimeline.DataSource = null;
                grdDPRTimeline.DataBind();
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void grdDPRTimeline_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;
            string DocumentPath = e.Row.Cells[3].Text.Trim().Replace("&nbsp;", "");
            if (DocumentPath != "")
            {
                lnkDownload.Visible = true;
            }
            else
            {
                lnkDownload.Visible = false;
            }
        }
    }

    protected void grdEFC_PFAD_PreRender(object sender, EventArgs e)
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

    protected void grdGODetails_PreRender(object sender, EventArgs e)
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

    protected void grdCommonStep_PreRender(object sender, EventArgs e)
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

    protected void grdTenderPublished_PreRender(object sender, EventArgs e)
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

    protected void grdQualificationCriteria_PreRender(object sender, EventArgs e)
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

    protected void grdPQCAnswer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectDPRPQC_PQC_Mandatory = 0;
            try
            {
                ProjectDPRPQC_PQC_Mandatory = Convert.ToInt32(e.Row.Cells[10].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Mandatory = 0;
            }

            int ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            try
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(e.Row.Cells[11].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            }

            int ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            try
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(e.Row.Cells[12].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            }

            int ProjectDPRPQC_PQC_Auto_Calculated = 0;
            try
            {
                ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(e.Row.Cells[13].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }

            int ProjectDPRPQC_PQCParent_Id = 0;
            try
            {
                ProjectDPRPQC_PQCParent_Id = Convert.ToInt32(e.Row.Cells[14].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQCParent_Id = 0;
            }

            int ProjectDPRPQC_PQC_Id = 0;
            try
            {
                ProjectDPRPQC_PQC_Id = Convert.ToInt32(e.Row.Cells[15].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Id = 0;
            }

            CheckBox chkVerified1 = e.Row.FindControl("chkVerified1") as CheckBox;
            CheckBox chkVerified2 = e.Row.FindControl("chkVerified2") as CheckBox;
            CheckBox chkVerified3 = e.Row.FindControl("chkVerified3") as CheckBox;

            TextBox txtBidderValue = e.Row.FindControl("txtBidderValue") as TextBox;

            FileUpload flDocs1 = e.Row.FindControl("flDocs1") as FileUpload;
            FileUpload flDocs2 = e.Row.FindControl("flDocs2") as FileUpload;
            FileUpload flDocs3 = e.Row.FindControl("flDocs3") as FileUpload;

            FileUpload flVDocs1 = e.Row.FindControl("flVDocs1") as FileUpload;
            FileUpload flVDocs2 = e.Row.FindControl("flVDocs2") as FileUpload;
            FileUpload flVDocs3 = e.Row.FindControl("flVDocs3") as FileUpload;

            if (ProjectDPRPQC_PQC_Auto_Calculated == 1)
            {
                txtBidderValue.ReadOnly = true;
            }
            flDocs1.Visible = false;
            flDocs2.Visible = false;
            flDocs3.Visible = false;

            flVDocs1.Visible = false;
            flVDocs2.Visible = false;
            flVDocs3.Visible = false;

            chkVerified1.Visible = false;
            chkVerified2.Visible = false;
            chkVerified3.Visible = false;

            bool Hide_Tbl_Response = false;
            if (ProjectDPRPQC_PQC_Upload_Document_Count == 0)
            {
                flDocs1.Visible = false;
                flDocs2.Visible = false;
                flDocs3.Visible = false;

                flVDocs1.Visible = false;
                flVDocs2.Visible = false;
                flVDocs3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;

                Hide_Tbl_Response = true;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 1)
            {
                flDocs1.Visible = true;
                flDocs2.Visible = false;
                flDocs3.Visible = false;

                flVDocs1.Visible = true;
                flVDocs2.Visible = false;
                flVDocs3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 2)
            {
                flDocs1.Visible = true;
                flDocs2.Visible = true;
                flDocs3.Visible = false;

                flVDocs1.Visible = true;
                flVDocs2.Visible = true;
                flVDocs3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 3)
            {
                flDocs1.Visible = true;
                flDocs2.Visible = true;
                flDocs3.Visible = true;

                flVDocs1.Visible = true;
                flVDocs2.Visible = true;
                flVDocs3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }
            else
            {
                flDocs1.Visible = true;
                flDocs2.Visible = true;
                flDocs3.Visible = true;

                flVDocs1.Visible = true;
                flVDocs2.Visible = true;
                flVDocs3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }

            if (ProjectDPRPQC_PQC_Enable_Verification_Document == 0)
            {
                flVDocs1.Visible = false;
                flVDocs2.Visible = false;
                flVDocs3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }

            int FileVerified1 = 0;
            int FileVerified2 = 0;
            int FileVerified3 = 0;
            try
            {
                FileVerified1 = Convert.ToInt32(e.Row.Cells[7].Text.Trim());
            }
            catch
            {
                FileVerified1 = 0;
            }
            try
            {
                FileVerified2 = Convert.ToInt32(e.Row.Cells[8].Text.Trim());
            }
            catch
            {
                FileVerified2 = 0;
            }
            try
            {
                FileVerified3 = Convert.ToInt32(e.Row.Cells[9].Text.Trim());
            }
            catch
            {
                FileVerified3 = 0;
            }
            if (FileVerified1 == 1)
            {
                chkVerified1.Checked = true;
            }
            else
            {
                chkVerified1.Checked = false;
            }
            if (FileVerified2 == 1)
            {
                chkVerified2.Checked = true;
            }
            else
            {
                chkVerified2.Checked = false;
            }
            if (FileVerified3 == 1)
            {
                chkVerified3.Checked = true;
            }
            else
            {
                chkVerified3.Checked = false;
            }


            HtmlTable tblAdditionalData = e.Row.FindControl("tblAdditionalData") as HtmlTable;
            HtmlTable tbl_Response = e.Row.FindControl("tbl_Response") as HtmlTable;
            if (Hide_Tbl_Response)
            {
                tbl_Response.Visible = false;
            }

            if (ProjectDPRPQC_PQC_Id == 6)
            {//Bid Capacity
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 2)
            {//EMD
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 1)
            {//Tender Fees
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 3)
            {//Exp 60%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 10)
            {//Exp 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 11)
            {//Exp 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 9)
            {//Solv 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 4)
            {//TurnOver 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 5)
            {//Net Worth
                tblAdditionalData.Visible = true;
            }
            else
            {
                tblAdditionalData.Visible = false;
            }
        }
    }

    protected void btnOpenQualification_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdQualificationResponse.Rows.Count; i++)
        {
            grdQualificationResponse.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int ProjectDPRBidder_Id = 0;
        try
        {
            ProjectDPRBidder_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectDPRBidder_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPRPQCResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdQualificationResponse.DataSource = ds.Tables[0];
            grdQualificationResponse.DataBind();
        }
        else
        {
            grdQualificationResponse.DataSource = null;
            grdQualificationResponse.DataBind();
        }

        ds = new DataLayer().get_tbl_ProjectDPR_Bidder_Order(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdQualificationWorkOrder.DataSource = ds.Tables[0];
            grdQualificationWorkOrder.DataBind();
        }
        else
        {
            grdQualificationWorkOrder.DataSource = null;
            grdQualificationWorkOrder.DataBind();
        }
        mpBidder.Show();
    }

    protected void grdQualificationResponse_PreRender(object sender, EventArgs e)
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

    protected void grdQualificationResponse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            try
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(e.Row.Cells[10].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            }

            int ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            try
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(e.Row.Cells[11].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            }

            int ProjectDPRPQC_PQC_Auto_Calculated = 0;
            try
            {
                ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(e.Row.Cells[12].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }

            int ProjectDPRPQC_PQC_Id = 0;
            try
            {
                ProjectDPRPQC_PQC_Id = Convert.ToInt32(e.Row.Cells[13].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Id = 0;
            }

            CheckBox chkVerified1 = e.Row.FindControl("chkVerified1") as CheckBox;
            CheckBox chkVerified2 = e.Row.FindControl("chkVerified2") as CheckBox;
            CheckBox chkVerified3 = e.Row.FindControl("chkVerified3") as CheckBox;

            LinkButton lnkDownload1 = e.Row.FindControl("lnkDownload1") as LinkButton;
            LinkButton lnkDownload2 = e.Row.FindControl("lnkDownload2") as LinkButton;
            LinkButton lnkDownload3 = e.Row.FindControl("lnkDownload3") as LinkButton;

            LinkButton lnkDownloadV1 = e.Row.FindControl("lnkDownloadV1") as LinkButton;
            LinkButton lnkDownloadV2 = e.Row.FindControl("lnkDownloadV2") as LinkButton;
            LinkButton lnkDownloadV3 = e.Row.FindControl("lnkDownloadV3") as LinkButton;

            lnkDownload1.Visible = false;
            lnkDownload2.Visible = false;
            lnkDownload3.Visible = false;

            lnkDownloadV1.Visible = false;
            lnkDownloadV2.Visible = false;
            lnkDownloadV3.Visible = false;

            chkVerified1.Visible = false;
            chkVerified2.Visible = false;
            chkVerified3.Visible = false;

            bool Hide_Tbl_Response = false;
            if (ProjectDPRPQC_PQC_Upload_Document_Count == 0)
            {
                lnkDownload1.Visible = false;
                lnkDownload2.Visible = false;
                lnkDownload3.Visible = false;

                lnkDownloadV1.Visible = false;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;

                Hide_Tbl_Response = true;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 1)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = false;
                lnkDownload3.Visible = false;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 2)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = false;

                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 3)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = true;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = true;
                lnkDownloadV3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }
            else
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = true;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = true;
                lnkDownloadV3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }

            if (ProjectDPRPQC_PQC_Enable_Verification_Document == 0)
            {
                lnkDownloadV1.Visible = false;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }

            int FileVerified1 = 0;
            int FileVerified2 = 0;
            int FileVerified3 = 0;
            try
            {
                FileVerified1 = Convert.ToInt32(e.Row.Cells[7].Text.Trim());
            }
            catch
            {
                FileVerified1 = 0;
            }
            try
            {
                FileVerified2 = Convert.ToInt32(e.Row.Cells[8].Text.Trim());
            }
            catch
            {
                FileVerified2 = 0;
            }
            try
            {
                FileVerified3 = Convert.ToInt32(e.Row.Cells[9].Text.Trim());
            }
            catch
            {
                FileVerified3 = 0;
            }
            if (FileVerified1 == 1)
            {
                chkVerified1.Checked = true;
            }
            else
            {
                chkVerified1.Checked = false;
            }
            if (FileVerified2 == 1)
            {
                chkVerified2.Checked = true;
            }
            else
            {
                chkVerified2.Checked = false;
            }
            if (FileVerified3 == 1)
            {
                chkVerified3.Checked = true;
            }
            else
            {
                chkVerified3.Checked = false;
            }

            HtmlTable tblAdditionalData = e.Row.FindControl("tblAdditionalData") as HtmlTable;
            HtmlTable tbl_Response = e.Row.FindControl("tbl_Response") as HtmlTable;
            if (Hide_Tbl_Response)
            {
                tbl_Response.Visible = false;
            }

            if (ProjectDPRPQC_PQC_Id == 6)
            {//Bid Capacity
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 2)
            {//EMD
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 1)
            {//Tender Fees
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 3)
            {//Exp 60%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 10)
            {//Exp 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 11)
            {//Exp 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 9)
            {//Solv 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 4)
            {//TurnOver 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 5)
            {//Net Worth
                tblAdditionalData.Visible = true;
            }
            else
            {
                tblAdditionalData.Visible = false;
            }
        }
    }

    protected void grdPQC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectDPRPQC_Id = 0;
            try
            {
                ProjectDPRPQC_Id = Convert.ToInt32(e.Row.Cells[7].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_Id = 0;
            }
            int ProjectDPRPQC_PQC_Mandatory = 0;
            try
            {
                ProjectDPRPQC_PQC_Mandatory = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Mandatory = 0;
            }
            int ProjectDPRPQC_PQC_Auto_Calculated = 0;
            try
            {
                ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(e.Row.Cells[5].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }
            CheckBox chkApplicable = e.Row.FindControl("chkApplicable") as CheckBox;
            Label txtPQCDetails = e.Row.FindControl("txtPQCDetails") as Label;
            TextBox txtPQCDetails1 = e.Row.FindControl("txtPQCDetails1") as TextBox;
            TextBox txtMinValue = e.Row.FindControl("txtMinValue") as TextBox;
            TextBox txtMaxValue = e.Row.FindControl("txtMaxValue") as TextBox;
            if (ProjectDPRPQC_Id > 0)
            {
                txtPQCDetails.Visible = true;
                txtPQCDetails1.Visible = false;
            }
            else
            {
                txtPQCDetails.Visible = false;
                txtPQCDetails1.Visible = true;
            }
            if (ProjectDPRPQC_PQC_Mandatory == 1)
            {
                chkApplicable.Enabled = false;
            }
            if (ProjectDPRPQC_PQC_Auto_Calculated == 1)
            {
                txtMinValue.Enabled = false;
                txtMaxValue.Enabled = false;
            }
        }
    }

    protected void btnAddPQCR_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPQC;
        if (ViewState["dtPQC"] != null)
        {
            dtPQC = (DataTable)(ViewState["dtPQC"]);
            DataRow dr = dtPQC.NewRow();
            dtPQC.Rows.Add(dr);
            ViewState["dtPQC"] = dtPQC;

            grdQualificationCriteriaR.DataSource = dtPQC;
            grdQualificationCriteriaR.DataBind();
        }
        else
        {
            dtPQC = new DataTable();

            DataColumn dc_ProjectDPRPQC_Id = new DataColumn("ProjectDPRPQC_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_DPR_Id = new DataColumn("ProjectDPRPQC_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCParentName = new DataColumn("ProjectDPRPQC_PQCParentName", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQCName = new DataColumn("ProjectDPRPQC_PQCName", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQC_Mandatory = new DataColumn("ProjectDPRPQC_PQC_Mandatory", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Auto_Calculated = new DataColumn("ProjectDPRPQC_PQC_Auto_Calculated", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Upload_Document_Count = new DataColumn("ProjectDPRPQC_PQC_Upload_Document_Count", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Enable_Verification_Document = new DataColumn("ProjectDPRPQC_PQC_Enable_Verification_Document", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCParent_Id = new DataColumn("ProjectDPRPQC_PQCParent_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQC_Id = new DataColumn("ProjectDPRPQC_PQC_Id", typeof(int));
            DataColumn dc_ProjectDPRPQC_Order = new DataColumn("ProjectDPRPQC_Order", typeof(int));
            DataColumn dc_ProjectDPRPQC_PQCMinVal = new DataColumn("ProjectDPRPQC_PQCMinVal", typeof(string));
            DataColumn dc_ProjectDPRPQC_PQCMaxVal = new DataColumn("ProjectDPRPQC_PQCMaxVal", typeof(string));
            DataColumn dc_ProjectDPRPQC_Comments = new DataColumn("ProjectDPRPQC_Comments", typeof(string));

            dtPQC.Columns.AddRange(new DataColumn[] { dc_ProjectDPRPQC_Id, dc_ProjectDPRPQC_DPR_Id, dc_ProjectDPRPQC_PQCParentName, dc_ProjectDPRPQC_PQCName, dc_ProjectDPRPQC_PQCMinVal, dc_ProjectDPRPQC_PQCMaxVal, dc_ProjectDPRPQC_Comments, dc_ProjectDPRPQC_PQC_Mandatory, dc_ProjectDPRPQC_PQC_Auto_Calculated, dc_ProjectDPRPQC_PQC_Upload_Document_Count, dc_ProjectDPRPQC_PQC_Enable_Verification_Document, dc_ProjectDPRPQC_PQCParent_Id, dc_ProjectDPRPQC_PQC_Id, dc_ProjectDPRPQC_Order });

            DataRow dr = dtPQC.NewRow();
            dtPQC.Rows.Add(dr);
            ViewState["dtPQC"] = dtPQC;

            grdQualificationCriteriaR.DataSource = dtPQC;
            grdQualificationCriteriaR.DataBind();
        }
    }

    protected void btnMinusPQCR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtPQC"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtPQC"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdQualificationCriteriaR.DataSource = dt;
            grdQualificationCriteriaR.DataBind();
            ViewState["dtPQC"] = dt;
        }
    }

    protected void grdBidderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gr = e.Row;
            string Is_JV = "No";
            Is_JV = e.Row.Cells[7].Text.Trim().Replace("&nbsp;", "");
            if (Is_JV == "")
            {
                Is_JV = "No";
            }
            HtmlTableRow trPartnerBidder = gr.FindControl("trPartnerBidder") as HtmlTableRow;
            HtmlTableCell tdShare = gr.FindControl("tdShare") as HtmlTableCell;
            HtmlTableCell tdShareP = gr.FindControl("tdShareP") as HtmlTableCell;
            if (Is_JV == "Yes")
            {
                trPartnerBidder.Visible = true;
                tdShare.Visible = true;
                tdShareP.Visible = true;
            }
            else
            {
                trPartnerBidder.Visible = false;
                tdShare.Visible = false;
                tdShareP.Visible = false;
            }

            int Total_Response = 0;
            try
            {
                Total_Response = Convert.ToInt32(e.Row.Cells[4].Text.Trim());
            }
            catch
            {
                Total_Response = 0;
            }
            int Total_Question = 0;
            try
            {
                Total_Question = Convert.ToInt32(e.Row.Cells[5].Text.Trim());
            }
            catch
            {
                Total_Question = 0;
            }
            if (Total_Response >= Total_Question)
            {
                e.Row.Cells[6].BackColor = Color.LightGreen;
            }
        }
    }

    protected void ddlISJV_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList ddlISJV = sender as RadioButtonList;
        GridViewRow gr = ddlISJV.Parent.Parent as GridViewRow;

        HtmlTableRow trPartnerBidder = gr.FindControl("trPartnerBidder") as HtmlTableRow;
        HtmlTableCell tdShare = gr.FindControl("tdShare") as HtmlTableCell;
        HtmlTableCell tdShareP = gr.FindControl("tdShareP") as HtmlTableCell;
        if (ddlISJV.SelectedValue == "Yes")
        {
            trPartnerBidder.Visible = true;
            tdShare.Visible = true;
            tdShareP.Visible = true;
        }
        else
        {
            trPartnerBidder.Visible = false;
            tdShare.Visible = false;
            tdShareP.Visible = false;
        }
    }

    protected void grdBiddersFinancial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gr = e.Row;
            string Is_JV = "No";
            Is_JV = e.Row.Cells[5].Text.Trim().Replace("&nbsp;", "");
            if (Is_JV == "")
            {
                Is_JV = "No";
            }
            HtmlTableRow trPartnerBidder = gr.FindControl("trPartnerBidder") as HtmlTableRow;
            HtmlTableCell tdShare = gr.FindControl("tdShare") as HtmlTableCell;
            HtmlTableCell tdShareP = gr.FindControl("tdShareP") as HtmlTableCell;
            if (Is_JV == "Yes")
            {
                trPartnerBidder.Visible = true;
                tdShare.Visible = true;
                tdShareP.Visible = true;
            }
            else
            {
                trPartnerBidder.Visible = false;
                tdShare.Visible = false;
                tdShareP.Visible = false;
            }
        }
    }

    protected void grdBidder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gr = e.Row;
            string Is_JV = "No";
            Is_JV = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (Is_JV == "")
            {
                Is_JV = "No";
            }
            HtmlTableRow trPartnerBidder = gr.FindControl("trPartnerBidder") as HtmlTableRow;
            HtmlTableCell tdShare = gr.FindControl("tdShare") as HtmlTableCell;
            HtmlTableCell tdShareP = gr.FindControl("tdShareP") as HtmlTableCell;
            if (Is_JV == "Yes")
            {
                trPartnerBidder.Visible = true;
                tdShare.Visible = true;
                tdShareP.Visible = true;
            }
            else
            {
                trPartnerBidder.Visible = false;
                tdShare.Visible = false;
                tdShareP.Visible = false;
            }
        }
    }

    protected void grdBidderOrder_PreRender(object sender, EventArgs e)
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

    protected void btnAddWork_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtBidder_Order;
        if (ViewState["dtBidder_Order"] != null)
        {
            dtBidder_Order = (DataTable)(ViewState["dtBidder_Order"]);

            if (AllClasses.CheckDt(dtBidder_Order) && dtBidder_Order.Rows.Count == grdBidderOrder.Rows.Count)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    TextBox txtNameOfWork = (grdBidderOrder.Rows[i].FindControl("txtNameOfWork") as TextBox);
                    TextBox txtStartDate = (grdBidderOrder.Rows[i].FindControl("txtStartDate") as TextBox);
                    TextBox txtEndDate = (grdBidderOrder.Rows[i].FindControl("txtEndDate") as TextBox);
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    RadioButtonList ddlCompleted = (grdBidderOrder.Rows[i].FindControl("ddlCompleted") as RadioButtonList);
                    TextBox txtVerificationLetter = (grdBidderOrder.Rows[i].FindControl("txtVerificationLetter") as TextBox);
                    FileUpload flWorkOrder = (grdBidderOrder.Rows[i].FindControl("flWorkOrder") as FileUpload);
                    FileUpload flOrderVerification = (grdBidderOrder.Rows[i].FindControl("flOrderVerification") as FileUpload);
                    FileUpload flVerificationLetter = (grdBidderOrder.Rows[i].FindControl("flVerificationLetter") as FileUpload);
                    RadioButtonList rbtBidderType = (grdBidderOrder.Rows[i].FindControl("rbtBidderType") as RadioButtonList);
                    RadioButtonList rbtLetterReminder = (grdBidderOrder.Rows[i].FindControl("rbtLetterReminder") as RadioButtonList);

                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Id"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPR_Id"] = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPR_Id"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPRBidder_Id"] = Convert.ToInt32(hf_ProjectDPRBidder_Id.Value);
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPRBidder_Id"] = 0;
                    }
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Name_Of_Work"] = txtNameOfWork.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_StartDate"] = txtStartDate.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_EndDate"] = txtEndDate.Text.Trim();
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = Convert.ToDecimal(txtOrderAmount.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = Convert.ToDecimal(txtInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = Convert.ToDecimal(txtJVShare.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = 100;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = Convert.ToDecimal(txtJVContractValue.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = ddlSimmilarNature.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = "Yes";
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = ddlCompleted.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = "Yes";
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_BidderType"] = rbtBidderType.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_BidderType"] = "Lead";
                    }
                    int ReminderCount = 0;
                    if (rbtLetterReminder.SelectedValue == "1")
                    {
                        ReminderCount = 1;
                    }
                    else if (rbtLetterReminder.SelectedValue == "2")
                    {
                        ReminderCount = 2;
                    }
                    else
                    {
                        ReminderCount = 0;
                    }
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_ReminderCount"] = ReminderCount;
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_OrderPath"] = grdBidderOrder.Rows[i].Cells[5].Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationPath"] = grdBidderOrder.Rows[i].Cells[6].Text.Trim();

                    if (ReminderCount == 0)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath"] = grdBidderOrder.Rows[i].Cells[7].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate"] = txtVerificationLetter.Text.Trim();
                    }
                    else if (ReminderCount == 1)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath1"] = grdBidderOrder.Rows[i].Cells[9].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate1"] = txtVerificationLetter.Text.Trim();
                    }
                    else
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath2"] = grdBidderOrder.Rows[i].Cells[10].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate2"] = txtVerificationLetter.Text.Trim();
                    }
                }
            }


            DataRow dr = dtBidder_Order.NewRow();
            dr["ProjectDPR_Bidder_Order_DPRBidder_Id"] = Convert.ToInt32(dtBidder_Order.Rows[0]["ProjectDPR_Bidder_Order_DPRBidder_Id"].ToString());
            dtBidder_Order.Rows.Add(dr);
            ViewState["dtBidder_Order"] = dtBidder_Order;

            grdBidderOrder.DataSource = dtBidder_Order;
            grdBidderOrder.DataBind();
        }
        else
        {
            dtBidder_Order = new DataTable();
            DataColumn dc_ProjectDPR_Bidder_Order_Id = new DataColumn("ProjectDPR_Bidder_Order_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_DPR_Id = new DataColumn("ProjectDPR_Bidder_Order_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_DPRBidder_Id = new DataColumn("ProjectDPR_Bidder_Order_DPRBidder_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_Name_Of_Work = new DataColumn("ProjectDPR_Bidder_Order_Name_Of_Work", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_StartDate = new DataColumn("ProjectDPR_Bidder_Order_StartDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_EndDate = new DataColumn("ProjectDPR_Bidder_Order_EndDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Amount = new DataColumn("ProjectDPR_Bidder_Order_Amount", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Inflation", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Amount_After_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Amount_After_Inflation", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_JV_Share = new DataColumn("ProjectDPR_Bidder_Order_JV_Share", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_JV_Contract_Value = new DataColumn("ProjectDPR_Bidder_Order_JV_Contract_Value", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Simmilar_Nature = new DataColumn("ProjectDPR_Bidder_Order_Simmilar_Nature", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Completed = new DataColumn("ProjectDPR_Bidder_Order_Completed", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Comments = new DataColumn("ProjectDPR_Bidder_Order_Comments", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_BidderType = new DataColumn("ProjectDPR_Bidder_Order_BidderType", typeof(string));

            DataColumn dc_ProjectDPR_Bidder_Order_OrderPath = new DataColumn("ProjectDPR_Bidder_Order_OrderPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate", typeof(string));

            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath1", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate1", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath2", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate2", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_ReminderCount = new DataColumn("ProjectDPR_Bidder_Order_ReminderCount", typeof(string));


            dtBidder_Order.Columns.AddRange(new DataColumn[] { dc_ProjectDPR_Bidder_Order_Id, dc_ProjectDPR_Bidder_Order_DPR_Id, dc_ProjectDPR_Bidder_Order_DPRBidder_Id, dc_ProjectDPR_Bidder_Order_Name_Of_Work, dc_ProjectDPR_Bidder_Order_StartDate, dc_ProjectDPR_Bidder_Order_EndDate, dc_ProjectDPR_Bidder_Order_Amount, dc_ProjectDPR_Bidder_Order_Inflation, dc_ProjectDPR_Bidder_Order_Amount_After_Inflation, dc_ProjectDPR_Bidder_Order_JV_Share, dc_ProjectDPR_Bidder_Order_JV_Contract_Value, dc_ProjectDPR_Bidder_Order_Simmilar_Nature, dc_ProjectDPR_Bidder_Order_Completed, dc_ProjectDPR_Bidder_Order_Comments, dc_ProjectDPR_Bidder_Order_BidderType, dc_ProjectDPR_Bidder_Order_OrderPath, dc_ProjectDPR_Bidder_Order_VerificationPath, dc_ProjectDPR_Bidder_Order_VerificationLetterPath, dc_ProjectDPR_Bidder_Order_VerificationLetterDate, dc_ProjectDPR_Bidder_Order_VerificationLetterPath1, dc_ProjectDPR_Bidder_Order_VerificationLetterDate1, dc_ProjectDPR_Bidder_Order_VerificationLetterPath2, dc_ProjectDPR_Bidder_Order_VerificationLetterDate2, dc_ProjectDPR_Bidder_Order_ReminderCount });

            DataRow dr = dtBidder_Order.NewRow();
            dtBidder_Order.Rows.Add(dr);
            ViewState["dtBidder_Order"] = dtBidder_Order;

            grdBidderOrder.DataSource = dtBidder_Order;
            grdBidderOrder.DataBind();
        }
    }

    protected void btnMinusWork_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtBidder_Order"] != null)
        {
            DataTable dtBidder_Order = (DataTable)ViewState["dtBidder_Order"];
            if (AllClasses.CheckDt(dtBidder_Order) && dtBidder_Order.Rows.Count == grdBidderOrder.Rows.Count)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    TextBox txtNameOfWork = (grdBidderOrder.Rows[i].FindControl("txtNameOfWork") as TextBox);
                    TextBox txtStartDate = (grdBidderOrder.Rows[i].FindControl("txtStartDate") as TextBox);
                    TextBox txtEndDate = (grdBidderOrder.Rows[i].FindControl("txtEndDate") as TextBox);
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    RadioButtonList ddlCompleted = (grdBidderOrder.Rows[i].FindControl("ddlCompleted") as RadioButtonList);
                    TextBox txtVerificationLetter = (grdBidderOrder.Rows[i].FindControl("txtVerificationLetter") as TextBox);
                    FileUpload flWorkOrder = (grdBidderOrder.Rows[i].FindControl("flWorkOrder") as FileUpload);
                    FileUpload flOrderVerification = (grdBidderOrder.Rows[i].FindControl("flOrderVerification") as FileUpload);
                    FileUpload flVerificationLetter = (grdBidderOrder.Rows[i].FindControl("flVerificationLetter") as FileUpload);
                    RadioButtonList rbtBidderType = (grdBidderOrder.Rows[i].FindControl("rbtBidderType") as RadioButtonList);
                    RadioButtonList rbtLetterReminder = (grdBidderOrder.Rows[i].FindControl("rbtLetterReminder") as RadioButtonList);

                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Id"] = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Id"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPR_Id"] = Convert.ToInt32(hf_ProjectDPR_Id.Value);
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPR_Id"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPRBidder_Id"] = Convert.ToInt32(hf_ProjectDPRBidder_Id.Value);
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPRBidder_Id"] = 0;
                    }
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Name_Of_Work"] = txtNameOfWork.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_StartDate"] = txtStartDate.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_EndDate"] = txtEndDate.Text.Trim();
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = Convert.ToDecimal(txtOrderAmount.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = Convert.ToDecimal(txtInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = Convert.ToDecimal(txtJVShare.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = 100;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = Convert.ToDecimal(txtJVContractValue.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = ddlSimmilarNature.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = "Yes";
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = ddlCompleted.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = "Yes";
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_BidderType"] = rbtBidderType.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_BidderType"] = "Lead";
                    }
                    int ReminderCount = 0;
                    if (rbtLetterReminder.SelectedValue == "1")
                    {
                        ReminderCount = 1;
                    }
                    else if (rbtLetterReminder.SelectedValue == "2")
                    {
                        ReminderCount = 2;
                    }
                    else
                    {
                        ReminderCount = 0;
                    }
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_ReminderCount"] = ReminderCount;
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_OrderPath"] = grdBidderOrder.Rows[i].Cells[5].Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationPath"] = grdBidderOrder.Rows[i].Cells[6].Text.Trim();

                    if (ReminderCount == 0)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath"] = grdBidderOrder.Rows[i].Cells[7].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate"] = txtVerificationLetter.Text.Trim();
                    }
                    else if (ReminderCount == 1)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath1"] = grdBidderOrder.Rows[i].Cells[9].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate1"] = txtVerificationLetter.Text.Trim();
                    }
                    else
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath2"] = grdBidderOrder.Rows[i].Cells[10].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate2"] = txtVerificationLetter.Text.Trim();
                    }
                }
            }
            if (dtBidder_Order.Rows.Count > 1)
            {
                dtBidder_Order.Rows.RemoveAt(dtBidder_Order.Rows.Count - 1);
                grdBidderOrder.DataSource = dtBidder_Order;
                grdBidderOrder.DataBind();
                ViewState["dtBidder_Order"] = dtBidder_Order;
            }
        }
    }

    protected void btnSaveBidderOrder_Click(object sender, EventArgs e)
    {
        List<tbl_ProjectDPR_Bidder_Order> obj_tbl_ProjectDPR_Bidder_Order_Li = new List<tbl_ProjectDPR_Bidder_Order>();
        for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
        {
            tbl_ProjectDPR_Bidder_Order obj_tbl_ProjectDPR_Bidder_Order = new tbl_ProjectDPR_Bidder_Order();
            TextBox txtNameOfWork = (grdBidderOrder.Rows[i].FindControl("txtNameOfWork") as TextBox);
            TextBox txtStartDate = (grdBidderOrder.Rows[i].FindControl("txtStartDate") as TextBox);
            TextBox txtEndDate = (grdBidderOrder.Rows[i].FindControl("txtEndDate") as TextBox);
            TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
            TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
            TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
            TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
            TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
            RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
            RadioButtonList ddlCompleted = (grdBidderOrder.Rows[i].FindControl("ddlCompleted") as RadioButtonList);
            TextBox txtVerificationLetter = (grdBidderOrder.Rows[i].FindControl("txtVerificationLetter") as TextBox);
            FileUpload flWorkOrder = (grdBidderOrder.Rows[i].FindControl("flWorkOrder") as FileUpload);
            FileUpload flOrderVerification = (grdBidderOrder.Rows[i].FindControl("flOrderVerification") as FileUpload);
            FileUpload flVerificationLetter = (grdBidderOrder.Rows[i].FindControl("flVerificationLetter") as FileUpload);
            RadioButtonList rbtBidderType = (grdBidderOrder.Rows[i].FindControl("rbtBidderType") as RadioButtonList);
            RadioButtonList rbtLetterReminder = (grdBidderOrder.Rows[i].FindControl("rbtLetterReminder") as RadioButtonList);

            if (txtNameOfWork.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Work Name");
                txtNameOfWork.Focus();
                return;
            }
            if (txtStartDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Date of Start");
                txtStartDate.Focus();
                return;
            }
            if (txtEndDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Actual Date of Completion");
                txtEndDate.Focus();
                return;
            }
            if (txtOrderAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Actual Amount of work Done");
                txtOrderAmount.Focus();
                return;
            }
            if (txtVerificationLetter.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill work Order Verificarion Letter Date");
                txtVerificationLetter.Focus();
                return;
            }
            if (!flWorkOrder.HasFile)
            {
                MessageBox.Show("Please Upload work Order Copy");
                flWorkOrder.Focus();
                return;
            }
            if (!flVerificationLetter.HasFile)
            {
                MessageBox.Show("Please Upload work Order Verification Letter Copy");
                flVerificationLetter.Focus();
                return;
            }
            if (!flOrderVerification.HasFile)
            {
                MessageBox.Show("Please Upload work Order Verification Copy");
                flOrderVerification.Focus();
                return;
            }
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Status = 1;
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Id = Convert.ToInt32(grdBidderOrder.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_DPR_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_DPRBidder_Id = Convert.ToInt32(hf_ProjectDPRBidder_Id.Value);
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_DPRBidder_Id = 0;
            }
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Name_Of_Work = txtNameOfWork.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_StartDate = txtStartDate.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_EndDate = txtEndDate.Text.Trim();
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount = Convert.ToDecimal(txtOrderAmount.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Inflation = Convert.ToDecimal(txtInflation.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Inflation = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount_After_Inflation = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount_After_Inflation = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Share = Convert.ToDecimal(txtJVShare.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Share = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Contract_Value = Convert.ToDecimal(txtJVContractValue.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Contract_Value = 0;
            }
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_BidderType = rbtBidderType.SelectedValue;
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Simmilar_Nature = ddlSimmilarNature.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Completed = ddlCompleted.Text.Trim();
            if (rbtLetterReminder.SelectedValue == "1")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_ReminderCount = 1;
            }
            else if (rbtLetterReminder.SelectedValue == "2")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_ReminderCount = 2;
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_ReminderCount = 0;
            }
            if (rbtLetterReminder.SelectedValue == "1")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterDate1 = txtVerificationLetter.Text.Trim();
            }
            else if (rbtLetterReminder.SelectedValue == "2")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterDate2 = txtVerificationLetter.Text.Trim();
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterDate = txtVerificationLetter.Text.Trim();
            }
            if (flWorkOrder.HasFile)
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_OrderBytes = flWorkOrder.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_OrderPath = grdBidderOrder.Rows[i].Cells[5].Text.Replace("&nbsp;", "").Trim();
            }
            if (flOrderVerification.HasFile)
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationByts = flOrderVerification.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationPath = grdBidderOrder.Rows[i].Cells[6].Text.Replace("&nbsp;", "").Trim();
            }
            if (rbtLetterReminder.SelectedValue == "1")
            {
                if (flVerificationLetter.HasFile)
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterByts1 = flVerificationLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterPath1 = grdBidderOrder.Rows[i].Cells[9].Text.Replace("&nbsp;", "").Trim();
                }
            }
            if (rbtLetterReminder.SelectedValue == "2")
            {
                if (flVerificationLetter.HasFile)
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterByts2 = flVerificationLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterPath2 = grdBidderOrder.Rows[i].Cells[10].Text.Replace("&nbsp;", "").Trim();
                }
            }
            else
            {
                if (flVerificationLetter.HasFile)
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterByts = flVerificationLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterPath = grdBidderOrder.Rows[i].Cells[7].Text.Replace("&nbsp;", "").Trim();
                }
            }
            obj_tbl_ProjectDPR_Bidder_Order_Li.Add(obj_tbl_ProjectDPR_Bidder_Order);
        }
        if (new DataLayer().Insert_tbl_ProjectDPR_Bidder_Order(obj_tbl_ProjectDPR_Bidder_Order_Li))
        {
            MessageBox.Show("Bidders Work Order Details Saved Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving Bidders Work Order Details");
            return;
        }
    }

    protected void grdQualificationCriteriaR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectDPRPQC_PQC_Mandatory = 0;
            try
            {
                ProjectDPRPQC_PQC_Mandatory = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Mandatory = 0;
            }
            int ProjectDPRPQC_PQC_Auto_Calculated = 0;
            try
            {
                ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(e.Row.Cells[5].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }
            //CheckBox chkApplicable = e.Row.FindControl("chkApplicable") as CheckBox;
            //if (ProjectDPRPQC_PQC_Mandatory == 1)
            //{
            //    chkApplicable.Enabled = false;
            //}
            TextBox txtMinValue = e.Row.FindControl("txtMinValue") as TextBox;
            TextBox txtMaxValue = e.Row.FindControl("txtMaxValue") as TextBox;
            if (ProjectDPRPQC_PQC_Auto_Calculated == 1)
            {
                txtMinValue.Enabled = false;
                txtMaxValue.Enabled = false;
            }
        }
    }

    protected bool checkDate(DateTime dtFYStart, DateTime dtFYEnd, DateTime dtOrderDate)
    {
        return dtOrderDate >= dtFYStart && dtOrderDate < dtFYEnd;
    }
    protected void Calculate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;
        TextBox txtEndDate = (gr.FindControl("txtEndDate") as TextBox);
        TextBox txtOrderAmount = (gr.FindControl("txtOrderAmount") as TextBox);
        TextBox txtInflation = (gr.FindControl("txtInflation") as TextBox);
        TextBox txtValueAfterInflation = (gr.FindControl("txtValueAfterInflation") as TextBox);
        TextBox txtJVShare = (gr.FindControl("txtJVShare") as TextBox);
        TextBox txtJVContractValue = (gr.FindControl("txtJVContractValue") as TextBox);

        DateTime dtOrderDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dtFYStart = new DateTime(2023, 4, 1);
        DateTime dtFYEnd = new DateTime(2024, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "0";
        }
        dtFYStart = new DateTime(2022, 4, 1);
        dtFYEnd = new DateTime(2023, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1";
        }
        dtFYStart = new DateTime(2021, 4, 1);
        dtFYEnd = new DateTime(2022, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.07";
        }
        dtFYStart = new DateTime(2020, 4, 1);
        dtFYEnd = new DateTime(2021, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.14";
        }
        dtFYStart = new DateTime(2019, 4, 1);
        dtFYEnd = new DateTime(2020, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.21";
        }
        dtFYStart = new DateTime(2018, 4, 1);
        dtFYEnd = new DateTime(2019, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.28";
        }
        dtFYStart = new DateTime(2017, 4, 1);
        dtFYEnd = new DateTime(2018, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.35";
        }
        dtFYStart = new DateTime(2016, 4, 1);
        dtFYEnd = new DateTime(2017, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.42";
        }
        dtFYStart = new DateTime(2015, 4, 1);
        dtFYEnd = new DateTime(2016, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.49";
        }
        dtFYStart = new DateTime(2014, 4, 1);
        dtFYEnd = new DateTime(2015, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.56";
        }
        dtFYStart = new DateTime(2013, 4, 1);
        dtFYEnd = new DateTime(2014, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.63";
        }
        dtFYStart = new DateTime(2012, 4, 1);
        dtFYEnd = new DateTime(2013, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.70";
        }
        dtFYStart = new DateTime(2011, 4, 1);
        dtFYEnd = new DateTime(2012, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.77";
        }
        dtFYStart = new DateTime(2010, 4, 1);
        dtFYEnd = new DateTime(2011, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.84";
        }
        dtFYStart = new DateTime(2009, 4, 1);
        dtFYEnd = new DateTime(2010, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.91";
        }

        decimal OrderAmount = 0;
        try
        {
            OrderAmount = decimal.Parse(txtOrderAmount.Text.Trim());
        }
        catch
        {
            OrderAmount = 0;
        }

        decimal JVShare = 0;
        try
        {
            JVShare = decimal.Parse(txtJVShare.Text.Trim());
        }
        catch
        {
            JVShare = 100;
        }
        decimal JVContractValue = decimal.Round(OrderAmount * JVShare / 100, 2, MidpointRounding.AwayFromZero);
        txtJVContractValue.Text = JVContractValue.ToString();

        decimal Inflation = 0;
        try
        {
            Inflation = decimal.Parse(txtInflation.Text.Trim());
        }
        catch
        {
            Inflation = 0;
        }
        decimal ValueAfterInflation = JVContractValue;
        //decimal ValueAfterInflation = decimal.Round(JVContractValue + (JVContractValue * Inflation / 100), 2, MidpointRounding.AwayFromZero);
        if (Inflation > 0)
        {
            ValueAfterInflation = decimal.Round(JVContractValue * Inflation, 2, MidpointRounding.AwayFromZero);
        }
        txtValueAfterInflation.Text = ValueAfterInflation.ToString();
    }

    protected void btnEvaluate_Click(object sender, EventArgs e)
    {
        decimal BidCapacity = 0;
        DataSet dsCap = new DataSet();
        dsCap = new DataLayer().get_tbl_ProjectDPRPQCResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value), Convert.ToInt32(hf_ProjectDPRBidder_Id.Value), 6);
        if (AllClasses.CheckDataSet(dsCap))
        {
            try
            {
                BidCapacity = Convert.ToDecimal(dsCap.Tables[0].Rows[0]["ProjectDPRPQC_PQCMinVal"].ToString().Trim());
            }
            catch
            {
                BidCapacity = 0;
            }
        }
        else
        {
            BidCapacity = 0;
        }

        TextBox txtBidderValue;
        for (int i = 0; i < grdPQCAnswer.Rows.Count; i++)
        {
            if (!chkOrderNotAvailable.Checked)
            {
                if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 11 || Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 10 || Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 3)
                {
                    DataSet dsOrder = new DataSet();
                    dsOrder = new DataLayer().get_tbl_ProjectDPR_Bidder_Order(Convert.ToInt32(hf_ProjectDPR_Id.Value), Convert.ToInt32(hf_ProjectDPRBidder_Id.Value));
                    if (AllClasses.CheckDataSet(dsOrder))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please Fill Bidders Work Order Details Before Evaluation.");
                        return;
                    }
                }
            }
            txtBidderValue = (grdPQCAnswer.Rows[i].FindControl("txtBidderValue") as TextBox);
            CheckBox chkBidderValueNA = (grdPQCAnswer.Rows[i].FindControl("chkBidderValueNA") as CheckBox);
            decimal BidderValueMaster = 0;
            try
            {
                BidderValueMaster = Convert.ToDecimal(grdPQCAnswer.Rows[i].Cells[21].Text.Trim());
            }
            catch
            {
                BidderValueMaster = 0;
            }
            decimal BidderValue = 0;
            try
            {
                BidderValue = Convert.ToDecimal(txtBidderValue.Text.Trim());
            }
            catch
            {
                BidderValue = 0;
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 6)
            {
                if (!chkBidderValueNA.Checked && BidderValue == 0)
                {
                    MessageBox.Show("Please Provide Bidder Value");
                    txtBidderValue.Focus();
                    return;
                }
            }
            int Eligible_Order = 0;
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 11)
            {
                Eligible_Order = calculate_Exprience_Details(3, BidderValueMaster, BidCapacity);
                if (Eligible_Order >= 3)
                    txtBidderValue.Text = BidderValueMaster.ToString();
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 10)
            {
                Eligible_Order = calculate_Exprience_Details(2, BidderValueMaster, BidCapacity);
                if (Eligible_Order >= 2)
                    txtBidderValue.Text = BidderValueMaster.ToString();
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 3)
            {
                Eligible_Order = calculate_Exprience_Details(1, BidderValueMaster, BidCapacity);
                if (Eligible_Order >= 1)
                    txtBidderValue.Text = BidderValueMaster.ToString();
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 9)
            {//Solvency
                if (hf_BidderType.Value == "Yes")
                {
                    BidderValueMaster = (BidderValueMaster * 51) / 100;
                }
            }
            if (BidderValue >= BidderValueMaster)
            {
                grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.LightGreen;
            }
            else
            {
                grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.OrangeRed;
            }
        }
        bool isQualified = false;
        for (int i = 0; i < grdPQCAnswer.Rows.Count; i++)
        {
            txtBidderValue = (grdPQCAnswer.Rows[i].FindControl("txtBidderValue") as TextBox);
            decimal BidderValue = 0;
            try
            {
                BidderValue = Convert.ToDecimal(txtBidderValue.Text.Trim());
            }
            catch
            {
                BidderValue = 0;
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 11)
            {
                if (BidderValue > 0)
                {
                    isQualified = true;
                    break;
                }
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 10)
            {
                if (BidderValue > 0)
                {
                    isQualified = true;
                    break;
                }
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 3)
            {
                if (BidderValue > 0)
                {
                    isQualified = true;
                    break;
                }
            }
        }
        for (int i = 0; i < grdPQCAnswer.Rows.Count; i++)
        {
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 11)
            {
                if (isQualified)
                {
                    grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.LightGreen;
                }
                else
                {
                    grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.OrangeRed;
                }
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 10)
            {
                if (isQualified)
                {
                    grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.LightGreen;
                }
                else
                {
                    grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.OrangeRed;
                }
            }
            if (Convert.ToInt32(grdPQCAnswer.Rows[i].Cells[15].Text.Trim()) == 3)
            {
                if (isQualified)
                {
                    grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.LightGreen;
                }
                else
                {
                    grdPQCAnswer.Rows[i].Cells[19].BackColor = Color.OrangeRed;
                }
            }
        }
        //txtBidderValue = (grdPQCAnswer.Rows[ii].FindControl("txtBidderValue") as TextBox);
        //txtBidderValue.Text = "0";

        btnSaveBidAnswer.Visible = true;
    }

    private int calculate_Exprience_Details(int Experience_Type, decimal Bid_Capacity_Less, decimal Bid_Capacity)
    {
        int counter = 0;
        decimal Total_Value_Partner = 0;
        decimal Total_Value_Lead = 0;

        decimal Total_Partner = 0;
        decimal Total_Lead = 0;

        if (Experience_Type == 3)
        {
            if (grdBidderOrder.Rows.Count >= 3)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    decimal TempValue_Partner = 0;
                    decimal TempValue_Lead = 0;
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    RadioButtonList rbtBidderType = (grdBidderOrder.Rows[i].FindControl("rbtBidderType") as RadioButtonList);
                    string BidderType = rbtBidderType.SelectedValue;
                    if (ddlSimmilarNature.SelectedValue == "Yes")
                    {
                        if (BidderType == "Lead")
                        {
                            try
                            {
                                TempValue_Lead = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                            }
                            catch
                            {
                                TempValue_Lead = 0;
                            }
                            Total_Lead++;
                        }
                        else
                        {
                            try
                            {
                                TempValue_Partner = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                            }
                            catch
                            {
                                TempValue_Partner = 0;
                            }
                            Total_Partner++;
                        }
                        Total_Value_Lead += TempValue_Lead;
                        Total_Value_Partner += TempValue_Partner;

                        if ((TempValue_Lead + TempValue_Partner) >= Bid_Capacity_Less)
                        {
                            if (Bid_Capacity >= 2500)
                            {
                                //if (TempValue_Lead >= ((Bid_Capacity_Less * 30) / 100))
                                //{
                                //    counter++;
                                //}
                                //else
                                //{
                                //    //FinalValue = 0;
                                //}
                                counter++;
                            }
                            else
                            {
                                counter++;
                            }
                        }
                        else
                        {
                            //FinalValue = 0;
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                //FinalValue = 0;
            }
        }
        else if (Experience_Type == 2)
        {
            if (grdBidderOrder.Rows.Count >= 2)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    decimal TempValue_Partner = 0;
                    decimal TempValue_Lead = 0;
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    string BidderType = grdBidderOrder.Rows[i].Cells[3].Text.Trim();
                    if (ddlSimmilarNature.SelectedValue == "Yes")
                    {
                        if (BidderType == "Lead")
                        {
                            try
                            {
                                TempValue_Lead = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                            }
                            catch
                            {
                                TempValue_Lead = 0;
                            }
                            Total_Lead++;
                        }
                        else
                        {
                            try
                            {
                                TempValue_Partner = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                            }
                            catch
                            {
                                TempValue_Partner = 0;
                            }
                            Total_Partner++;
                        }
                        Total_Value_Lead += TempValue_Lead;
                        Total_Value_Partner += TempValue_Partner;
                        if ((TempValue_Lead + TempValue_Partner) >= Bid_Capacity_Less)
                        {
                            if (Bid_Capacity >= 2500)
                            {
                                //if (TempValue_Lead >= ((Bid_Capacity_Less * 30) / 100))
                                //{
                                //    counter++;
                                //}
                                //else
                                //{
                                //    //FinalValue = 0;
                                //}
                                counter++;
                            }
                            else
                            {
                                counter++;
                            }
                        }
                        else
                        {
                            //FinalValue = 0;
                        }
                    }
                }
            }
            else
            {
                //FinalValue = 0;
            }
        }
        else if (Experience_Type == 1)
        {
            if (grdBidderOrder.Rows.Count >= 1)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    decimal TempValue_Partner = 0;
                    decimal TempValue_Lead = 0;
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    string BidderType = grdBidderOrder.Rows[i].Cells[3].Text.Trim();
                    if (ddlSimmilarNature.SelectedValue == "Yes")
                    {
                        if (BidderType == "Lead")
                        {
                            try
                            {
                                TempValue_Lead = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                            }
                            catch
                            {
                                TempValue_Lead = 0;
                            }
                            Total_Lead++;
                        }
                        else
                        {
                            try
                            {
                                TempValue_Partner = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                            }
                            catch
                            {
                                TempValue_Partner = 0;
                            }
                            Total_Partner++;
                        }
                        Total_Value_Lead += TempValue_Lead;
                        Total_Value_Partner += TempValue_Partner;
                        if ((TempValue_Lead + TempValue_Partner) >= Bid_Capacity_Less)
                        {
                            if (Bid_Capacity >= 2500)
                            {
                                if (TempValue_Lead >= ((Bid_Capacity_Less * 30) / 100))
                                {
                                    counter++;
                                }
                                else
                                {
                                    //FinalValue = 0;
                                }
                                //counter++;
                            }
                            else
                            {
                                counter++;
                            }
                        }
                        else
                        {
                            //FinalValue = 0;
                        }
                    }
                }
            }
            else
            {
                //FinalValue = 0;
            }
        }
        if (counter <= 0)
            return 0;
        else
            return counter;
    }

    protected void grdBidderOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string BidderType = e.Row.Cells[3].Text.Trim();
            if (BidderType == "")
            {
                BidderType = hf_BidderType.Value;
            }
            string Completed = e.Row.Cells[4].Text.Trim();
            string Comissioned = e.Row.Cells[5].Text.Trim();
            string SimmilarNature = e.Row.Cells[9].Text.Trim();
            RadioButtonList rbtBidderType = (e.Row.FindControl("rbtBidderType") as RadioButtonList);
            RadioButtonList ddlSimmilarNature = (e.Row.FindControl("ddlSimmilarNature") as RadioButtonList);
            RadioButtonList ddlCompleted = (e.Row.FindControl("ddlCompleted") as RadioButtonList);
            try
            {
                rbtBidderType.SelectedValue = BidderType;
            }
            catch
            {

            }
            try
            {
                ddlSimmilarNature.SelectedValue = SimmilarNature;
            }
            catch
            {

            }
            try
            {
                ddlCompleted.SelectedValue = Completed;
            }
            catch
            {

            }
        }
    }

    protected void txtTenderCost_TextChanged(object sender, EventArgs e)
    {
        int ProjectDPRPQC_PQC_Auto_Calculated = 0;
        int ProjectDPRPQC_PQC_Id = 0;
        decimal Tender_Cost = 0;
        try
        {
            Tender_Cost = Convert.ToDecimal(txtTenderCost.Text.Trim());
        }
        catch
        {
            Tender_Cost = 0;
        }

        if (Tender_Cost > 0)
        {
            for (int i = 0; i < grdPQC.Rows.Count; i++)
            {
                TextBox txtMinValue = (grdPQC.Rows[i].FindControl("txtMinValue") as TextBox);
                try
                {
                    ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(grdPQC.Rows[i].Cells[5].Text.Trim());
                }
                catch
                {
                    ProjectDPRPQC_PQC_Auto_Calculated = 0;
                }
                try
                {
                    ProjectDPRPQC_PQC_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[7].Text.Trim());
                }
                catch
                {
                    ProjectDPRPQC_PQC_Id = 0;
                }
                if (ProjectDPRPQC_PQC_Id == 6)
                {//Bid Capacity
                    txtMinValue.Text = Tender_Cost.ToString();
                }
                if (ProjectDPRPQC_PQC_Id == 2)
                {//EMD
                    decimal EMD = 0;
                    if (Tender_Cost <= 500)
                    {
                        txtMinValue.Text = decimal.Round((Tender_Cost * 2) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        EMD = decimal.Round((Tender_Cost * 1) / 100, 2, MidpointRounding.AwayFromZero);
                        if (EMD < 10)
                            txtMinValue.Text = "10.00";
                        else
                            txtMinValue.Text = EMD.ToString();
                    }
                }
                if (ProjectDPRPQC_PQC_Id == 1)
                {//Tender Fees
                    if (Tender_Cost <= 40)
                    {
                        txtMinValue.Text = (3000 + (3000 * 18) / 100).ToString();
                    }
                    else if (Tender_Cost > 40 && Tender_Cost <= 100)
                    {
                        txtMinValue.Text = (5000 + (5000 * 18) / 100).ToString();
                    }
                    else if (Tender_Cost > 100 && Tender_Cost <= 1000)
                    {
                        txtMinValue.Text = (10000 + (10000 * 18) / 100).ToString();
                    }
                    else
                    {
                        txtMinValue.Text = (20000 + (20000 * 18) / 100).ToString();
                    }
                }
                if (ProjectDPRPQC_PQC_Id == 3)
                {//Exp 60%
                    txtMinValue.Text = decimal.Round((Tender_Cost * 60) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                }
                if (ProjectDPRPQC_PQC_Id == 10)
                {//Exp 40%
                    txtMinValue.Text = decimal.Round((Tender_Cost * 40) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                }
                if (ProjectDPRPQC_PQC_Id == 11)
                {//Exp 30%
                    txtMinValue.Text = decimal.Round((Tender_Cost * 30) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                }
                if (ProjectDPRPQC_PQC_Id == 9)
                {//Solv 40%
                    txtMinValue.Text = decimal.Round((Tender_Cost * 40) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                }
                if (ProjectDPRPQC_PQC_Id == 4)
                {//TurnOver 30%
                    txtMinValue.Text = decimal.Round((Tender_Cost * 30) / 100, 2, MidpointRounding.AwayFromZero).ToString();
                }
            }
        }
    }

    protected void txtFirmGSTIN_TextChanged(object sender, EventArgs e)
    {
        TextBox txtFirmGSTIN = sender as TextBox;
        GridViewRow gr = txtFirmGSTIN.Parent.Parent as GridViewRow;
        TextBox txtFirmName = gr.FindControl("txtFirmName") as TextBox;
        TextBox txtFirmPAN = gr.FindControl("txtFirmPAN") as TextBox;
        TextBox txtContactNo = gr.FindControl("txtContactNo") as TextBox;
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPRBidder_detail(txtFirmGSTIN.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            txtFirmName.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderName"].ToString().Trim();
            txtFirmName.ReadOnly = true;
            txtFirmPAN.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderPAN"].ToString().Trim();
            txtFirmPAN.ReadOnly = true;
            txtContactNo.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderMobile"].ToString().Trim();
            txtContactNo.ReadOnly = false;
        }
        else
        {
            if (txtFirmGSTIN.Text.ToString().Trim().Length == 15)
            {
                txtFirmPAN.Text = txtFirmGSTIN.Text.ToString().Trim().Substring(2, 10);
            }
        }
    }

    protected void txtFirmGSTINP_TextChanged(object sender, EventArgs e)
    {
        TextBox txtFirmGSTINP = sender as TextBox;
        GridViewRow gr = txtFirmGSTINP.Parent.Parent.Parent.Parent as GridViewRow;
        TextBox txtFirmNameP = gr.FindControl("txtFirmNameP") as TextBox;
        TextBox txtFirmPANP = gr.FindControl("txtFirmPANP") as TextBox;
        TextBox txtContactNoP = gr.FindControl("txtContactNoP") as TextBox;
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPRBidder_detail(txtFirmGSTINP.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            txtFirmNameP.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderName"].ToString().Trim();
            txtFirmNameP.ReadOnly = true;
            txtFirmPANP.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderPAN"].ToString().Trim();
            txtFirmPANP.ReadOnly = true;
            txtContactNoP.Text = ds.Tables[0].Rows[0]["ProjectDPRBidder_BidderMobile"].ToString().Trim();
            txtContactNoP.ReadOnly = false;
        }
        else
        {
            if (txtFirmGSTINP.Text.ToString().Trim().Length == 15)
            {
                txtFirmPANP.Text = txtFirmGSTINP.Text.ToString().Trim().Substring(2, 10);
            }
        }
    }

    protected void btnSkipCorrigendum_Click(object sender, EventArgs e)
    {
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();
        int DPRStep_Id = 0;
        try
        {
            DPRStep_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        catch
        {
            DPRStep_Id = 0;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (DPRStep_Id == 0)
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = "Process Skipped";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
        for (int i = 0; i < grdQualificationCriteriaR.Rows.Count; i++)
        {
            Label txtPQCDetails = (grdQualificationCriteriaR.Rows[i].FindControl("txtPQCDetails") as Label);
            TextBox txtMinValue = (grdQualificationCriteriaR.Rows[i].FindControl("txtMinValue") as TextBox);
            TextBox txtMaxValue = (grdQualificationCriteriaR.Rows[i].FindControl("txtMaxValue") as TextBox);
            TextBox txtComments = (grdQualificationCriteriaR.Rows[i].FindControl("txtComments") as TextBox);
            tbl_ProjectDPRPQC obj_tbl_ProjectDPRPQC = new tbl_ProjectDPRPQC();
            obj_tbl_ProjectDPRPQC.ProjectDPRPQC_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Status = 1;
            obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCName = txtPQCDetails.Text.Replace("\r", "").Replace("\n", "").Trim();
            obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCMinVal = txtMinValue.Text.Replace("\r", "").Replace("\n", "").Trim();
            obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCMaxVal = txtMaxValue.Text.Replace("\r", "").Replace("\n", "").Trim();
            obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Comments = txtComments.Text.Replace("\r", "").Replace("\n", "").Trim();
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id = Convert.ToInt32(grdQualificationCriteriaR.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_DPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_DPR_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Mandatory = Convert.ToInt32(grdPQC.Rows[i].Cells[2].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Mandatory = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(grdPQC.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(grdPQC.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(grdPQC.Rows[i].Cells[5].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCParent_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[6].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQCParent_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Id = Convert.ToInt32(grdPQC.Rows[i].Cells[7].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_PQC_Id = 0;
            }
            try
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Order = Convert.ToInt32(grdPQC.Rows[i].Cells[8].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPRPQC.ProjectDPRPQC_Order = 0;
            }
            obj_tbl_ProjectDPRPQC_Li.Add(obj_tbl_ProjectDPRPQC);
        }
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, false))
        {
            MessageBox.Show("Process Skipped Successfully!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }

    protected void grdPreBidResponseDocs_PreRender(object sender, EventArgs e)
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

    protected void grdPreBidResponseDocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnAddResponse_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtBidResponse;
        if (ViewState["BidResponse"] != null)
        {
            dtBidResponse = (DataTable)(ViewState["BidResponse"]);
            DataRow dr = dtBidResponse.NewRow();
            dtBidResponse.Rows.Add(dr);
            ViewState["BidResponse"] = dtBidResponse;

            grdPreBidResponseDocs.DataSource = dtBidResponse;
            grdPreBidResponseDocs.DataBind();
        }
        else
        {
            dtBidResponse = new DataTable();
            DataColumn dc_ProjectDPRBidResponse_Id = new DataColumn("ProjectDPRBidResponse_Id", typeof(int));
            DataColumn dc_ProjectDPRBidResponse_DPR_Id = new DataColumn("ProjectDPRBidResponse_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPRBidResponse_BidResponseName = new DataColumn("ProjectDPRBidResponse_BidResponseName", typeof(string));
            DataColumn dc_ProjectDPRBidResponse_Comments = new DataColumn("ProjectDPRBidResponse_Comments", typeof(string));
            DataColumn dc_ProjectDPRBidResponse_DocumentPath = new DataColumn("ProjectDPRBidResponse_DocumentPath", typeof(string));

            dtBidResponse.Columns.AddRange(new DataColumn[] { dc_ProjectDPRBidResponse_Id, dc_ProjectDPRBidResponse_DPR_Id, dc_ProjectDPRBidResponse_BidResponseName, dc_ProjectDPRBidResponse_Comments, dc_ProjectDPRBidResponse_DocumentPath });

            DataRow dr = dtBidResponse.NewRow();
            dtBidResponse.Rows.Add(dr);
            ViewState["BidResponse"] = dtBidResponse;

            grdPreBidResponseDocs.DataSource = dtBidResponse;
            grdPreBidResponseDocs.DataBind();
        }
    }

    protected void btnMinusResponse_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["BidResponse"] != null)
        {
            DataTable dt = (DataTable)ViewState["BidResponse"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdPreBidResponseDocs.DataSource = dt;
            grdPreBidResponseDocs.DataBind();
            ViewState["BidResponse"] = dt;
        }
    }

    protected void lnkActionCorrigendum_Click(object sender, EventArgs e)
    {
        ddlStatus.SelectedValue = "6";
        ddlStatus_SelectedIndexChanged(ddlStatus, e);
    }

    protected void grdQualificationWorkOrder_PreRender(object sender, EventArgs e)
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

    protected void grdPBMResponseDoc_PreRender(object sender, EventArgs e)
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

    protected void grdNGTDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectDPRBidder_Is_JV = e.Row.Cells[4].Text.Trim();
            string ProjectDPRBidder_BidderGSTIN_Available = e.Row.Cells[5].Text.Trim();
            string ProjectDPRBidder_BidderGSTINP_Available = e.Row.Cells[6].Text.Trim();
            if (ProjectDPRBidder_Is_JV == "")
            {
                ProjectDPRBidder_Is_JV = "No";
            }
            RadioButtonList ddlISJV = e.Row.FindControl("ddlISJV") as RadioButtonList;
            CheckBox chkGSTNA = e.Row.FindControl("chkGSTNA") as CheckBox;
            CheckBox chkGSTNAP = e.Row.FindControl("chkGSTNAP") as CheckBox;
            if (ProjectDPRBidder_BidderGSTIN_Available == "0")
            {
                chkGSTNA.Checked = true;
            }
            else
            {
                chkGSTNA.Checked = false;
            }
            chkGSTNA_CheckedChanged(chkGSTNA, e);
            if (ProjectDPRBidder_BidderGSTINP_Available == "0")
            {
                chkGSTNAP.Checked = true;
            }
            else
            {
                chkGSTNAP.Checked = false;
            }
            chkGSTNAP_CheckedChanged(chkGSTNAP, e);
            try
            {
                ddlISJV.SelectedValue = ProjectDPRBidder_Is_JV;
            }
            catch
            {
                ddlISJV.SelectedValue = "No";
            }
            ddlISJV_SelectedIndexChanged(ddlISJV, e);
        }
    }

    protected void chkGSTNA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkGSTNA = sender as CheckBox;
        GridViewRow gr = chkGSTNA.Parent.Parent as GridViewRow;
        TextBox txtFirmGSTIN = gr.FindControl("txtFirmGSTIN") as TextBox;
        if (chkGSTNA.Checked)
        {
            txtFirmGSTIN.Enabled = false;
        }
        else
        {
            txtFirmGSTIN.Enabled = true;
        }
    }

    protected void chkGSTNAP_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkGSTNAP = sender as CheckBox;
        GridViewRow gr = chkGSTNAP.Parent.Parent.Parent.Parent as GridViewRow;
        TextBox txtFirmGSTINP = gr.FindControl("txtFirmGSTINP") as TextBox;
        if (chkGSTNAP.Checked)
        {
            txtFirmGSTINP.Enabled = false;
        }
        else
        {
            txtFirmGSTINP.Enabled = true;
        }
    }

    protected void btnPQCView_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int ProjectDPRBidder_Id = 0;
        try
        {
            ProjectDPRBidder_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectDPRBidder_Id = 0;
        }
        string ProjectDPRBidder_Is_JV = "";
        ProjectDPRBidder_Is_JV = gr.Cells[7].Text.Trim();
        if (ProjectDPRBidder_Is_JV == "")
        {
            ProjectDPRBidder_Is_JV = "No";
        }
        hf_BidderType.Value = ProjectDPRBidder_Is_JV;
        if (ProjectDPRBidder_Id > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_ProjectDPRPQCResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id, 0);
            if (AllClasses.CheckDataSet(ds))
            {
                grdPQCAnswerView.DataSource = ds.Tables[0];
                grdPQCAnswerView.DataBind();
                mpBidderResponse.Show();
            }
            else
            {
                grdPQCAnswerView.DataSource = null;
                grdPQCAnswerView.DataBind();
                MessageBox.Show("No Response Filled");
            }
        }
        else
        {
            MessageBox.Show("Details Not Found");
            return;
        }
    }

    protected void grdPQCAnswerView_PreRender(object sender, EventArgs e)
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

    protected void grdPQCAnswerView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            try
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(e.Row.Cells[10].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            }

            int ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            try
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(e.Row.Cells[11].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            }

            int ProjectDPRPQC_PQC_Auto_Calculated = 0;
            try
            {
                ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(e.Row.Cells[12].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }

            int ProjectDPRPQC_PQC_Id = 0;
            try
            {
                ProjectDPRPQC_PQC_Id = Convert.ToInt32(e.Row.Cells[13].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Id = 0;
            }

            CheckBox chkVerified1 = e.Row.FindControl("chkVerified1") as CheckBox;
            CheckBox chkVerified2 = e.Row.FindControl("chkVerified2") as CheckBox;
            CheckBox chkVerified3 = e.Row.FindControl("chkVerified3") as CheckBox;

            LinkButton lnkDownload1 = e.Row.FindControl("lnkDownload1") as LinkButton;
            LinkButton lnkDownload2 = e.Row.FindControl("lnkDownload2") as LinkButton;
            LinkButton lnkDownload3 = e.Row.FindControl("lnkDownload3") as LinkButton;

            LinkButton lnkDownloadV1 = e.Row.FindControl("lnkDownloadV1") as LinkButton;
            LinkButton lnkDownloadV2 = e.Row.FindControl("lnkDownloadV2") as LinkButton;
            LinkButton lnkDownloadV3 = e.Row.FindControl("lnkDownloadV3") as LinkButton;
                        
            lnkDownload1.Visible = false;
            lnkDownload2.Visible = false;
            lnkDownload3.Visible = false;

            lnkDownloadV1.Visible = false;
            lnkDownloadV2.Visible = false;
            lnkDownloadV3.Visible = false;

            chkVerified1.Visible = false;
            chkVerified2.Visible = false;
            chkVerified3.Visible = false;

            bool Hide_Tbl_Response = false;
            if (ProjectDPRPQC_PQC_Upload_Document_Count == 0)
            {
                lnkDownload1.Visible = false;
                lnkDownload2.Visible = false;
                lnkDownload3.Visible = false;

                lnkDownloadV1.Visible = false;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;

                Hide_Tbl_Response = true;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 1)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = false;
                lnkDownload3.Visible = false;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 2)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = false;

                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 3)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = true;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = true;
                lnkDownloadV3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }
            else
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = true;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = true;
                lnkDownloadV3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }

            if (ProjectDPRPQC_PQC_Enable_Verification_Document == 0)
            {
                lnkDownloadV1.Visible = false;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }

            int FileVerified1 = 0;
            int FileVerified2 = 0;
            int FileVerified3 = 0;
            try
            {
                FileVerified1 = Convert.ToInt32(e.Row.Cells[7].Text.Trim());
            }
            catch
            {
                FileVerified1 = 0;
            }
            try
            {
                FileVerified2 = Convert.ToInt32(e.Row.Cells[8].Text.Trim());
            }
            catch
            {
                FileVerified2 = 0;
            }
            try
            {
                FileVerified3 = Convert.ToInt32(e.Row.Cells[9].Text.Trim());
            }
            catch
            {
                FileVerified3 = 0;
            }
            if (FileVerified1 == 1)
            {
                chkVerified1.Checked = true;
            }
            else
            {
                chkVerified1.Checked = false;
            }
            if (FileVerified2 == 1)
            {
                chkVerified2.Checked = true;
            }
            else
            {
                chkVerified2.Checked = false;
            }
            if (FileVerified3 == 1)
            {
                chkVerified3.Checked = true;
            }
            else
            {
                chkVerified3.Checked = false;
            }

            HtmlTable tblAdditionalData = e.Row.FindControl("tblAdditionalData") as HtmlTable;
            HtmlTable tbl_Response = e.Row.FindControl("tbl_Response") as HtmlTable;
            if (Hide_Tbl_Response)
            {
                tbl_Response.Visible = false;
            }

            if (ProjectDPRPQC_PQC_Id == 6)
            {//Bid Capacity
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 2)
            {//EMD
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 1)
            {//Tender Fees
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 3)
            {//Exp 60%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 10)
            {//Exp 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 11)
            {//Exp 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 9)
            {//Solv 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 4)
            {//TurnOver 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 5)
            {//Net Worth
                tblAdditionalData.Visible = true;
            }
            else
            {
                tblAdditionalData.Visible = false;
            }
        }
    }

    protected void chkBidderValueNA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkBidderValueNA = (sender as CheckBox);
        GridViewRow gr = chkBidderValueNA.Parent.Parent as GridViewRow;

        TextBox txtBidderValue = gr.FindControl("txtBidderValue") as TextBox;
        if (chkBidderValueNA.Checked)
        {
            txtBidderValue.ReadOnly = true;
            txtBidderValue.Text = "";
        }
        else
        {
            txtBidderValue.ReadOnly = false;
        }
    }

    protected void btnSkipSLTC_Click(object sender, EventArgs e)
    {
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();
        int DPRStep_Id = 0;
        try
        {
            DPRStep_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        catch
        {
            DPRStep_Id = 0;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (DPRStep_Id == 0)
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = "Process Skipped";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, false))
        {
            MessageBox.Show("Process Skipped Successfully!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }

    protected void btnSkipEFC_Click(object sender, EventArgs e)
    {
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();
        int DPRStep_Id = 0;
        try
        {
            DPRStep_Id = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        catch
        {
            DPRStep_Id = 0;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (DPRStep_Id == 0)
        {
            MessageBox.Show("Please Select Status");
            ddlStatus.Focus();
            return;
        }
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = "Process Skipped";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, false))
        {
            MessageBox.Show("Process Skipped Successfully!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }

    protected void btnTenderCancelled1_Click(object sender, EventArgs e)
    {
        if (txtTenderCancellationReason1.Text == "")
        {
            MessageBox.Show("Please Provide Reason For Cancellation Of Tender");
            txtTenderCancellationReason1.Focus();
            return;
        }
        if (!fl_Docs_Step9.HasFile)
        {
            MessageBox.Show("Please Upload Document For Cancellation Of Tender");
            fl_Docs_Step9.Focus();
            return;
        }
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();
        int DPRStep_Id = 15;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtBiddersEvaluation.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtTenderCancellationReason1.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = "";
        if (fl_Docs_Step9.HasFile)
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step9.FileBytes;
        }
        else
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
        }
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, true))
        {
            MessageBox.Show("This Tender Marked For Cancellation.Please Procees From NIT Step.!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }

    protected void btnTenderCancelled2_Click(object sender, EventArgs e)
    {
        if (txtTenderCancellationReason2.Text == "")
        {
            MessageBox.Show("Please Provide Reason For Cancellation Of Tender");
            txtTenderCancellationReason2.Focus();
            return;
        }
        if (!fl_Docs_Step8.HasFile)
        {
            MessageBox.Show("Please Upload Document For Cancellation Of Tender");
            fl_Docs_Step8.Focus();
            return;
        }
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();
        int DPRStep_Id = 15;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtTechnicalBidOpening.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtTenderCancellationReason2.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = "";
        if (fl_Docs_Step8.HasFile)
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step8.FileBytes;
        }
        else
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
        }
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, true))
        {
            MessageBox.Show("This Tender Marked For Cancellation.Please Procees From NIT Step.!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }

    protected void btnTenderCancelled3_Click(object sender, EventArgs e)
    {
        if (txtTenderCancellationReason3.Text == "")
        {
            MessageBox.Show("Please Provide Reason For Cancellation Of Tender");
            txtTenderCancellationReason3.Focus();
            return;
        }
        if (!fl_Docs_Step10.HasFile)
        {
            MessageBox.Show("Please Upload Document For Cancellation Of Tender");
            fl_Docs_Step10.Focus();
            return;
        }
        tbl_ProjectDPRTender obj_tbl_ProjectDPRTender = new tbl_ProjectDPRTender();
        List<tbl_ProjectDPRBidder> obj_tbl_ProjectDPRBidder_Li = new List<tbl_ProjectDPRBidder>();
        List<tbl_ProjectDPRPQC> obj_tbl_ProjectDPRPQC_Li = new List<tbl_ProjectDPRPQC>();
        List<tbl_ProjectDPRBidResponse> obj_tbl_ProjectDPRBidResponse_Li = new List<tbl_ProjectDPRBidResponse>();
        int DPRStep_Id = 15;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Status = 1;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Step_Status = DPRStep_Id;
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        obj_tbl_ProjectDPRTender.ProjectDPRTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectDPRTender.ProjectDPRTender_ActionDate = txtTechnicalBidOpening.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_Comments = txtTenderCancellationReason3.Text.Trim();
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TenderEndDate = "";
        obj_tbl_ProjectDPRTender.ProjectDPRTender_TechnicalBidOpeningDate = "";
        if (fl_Docs_Step8.HasFile)
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = fl_Docs_Step10.FileBytes;
        }
        else
        {
            obj_tbl_ProjectDPRTender.ProjectDPRTender_DocumentPath_Bytes = null;
        }
        if ((new DataLayer()).Insert_tbl_ProjectDPRTender(obj_tbl_ProjectDPRTender, obj_tbl_ProjectDPRBidder_Li, obj_tbl_ProjectDPRPQC_Li, obj_tbl_ProjectDPRBidResponse_Li, true))
        {
            MessageBox.Show("This Tender Marked For Cancellation.Please Procees From NIT Step.!");
            btnSearch_Click(btnSearch, new EventArgs());
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation!");
            return;
        }
    }
}