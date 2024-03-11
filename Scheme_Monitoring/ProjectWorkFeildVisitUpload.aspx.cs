using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectWorkFeildVisitUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Default_Zone = ConfigurationManager.AppSettings.Get("Default_Zone");
            string Default_Circle = ConfigurationManager.AppSettings.Get("Default_Circle");
            string Default_Division = ConfigurationManager.AppSettings.Get("Default_Division");
            string Client = ConfigurationManager.AppSettings.Get("Client");
            string Default_Scheme = ConfigurationManager.AppSettings.Get("Default_Scheme");
            Session["Default_Zone"] = Default_Zone;
            Session["Default_Circle"] = Default_Circle;
            Session["Default_Division"] = Default_Division;
            Session["Client"] = Client;
            Session["Default_Scheme"] = Default_Scheme;

            ViewState["IsValid"] = "false";
            Session.Add("rno", 0);
            Random randomclass = new Random();
            Session["rno"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(randomclass.Next().ToString(), "MD5");

            txtVisitDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("-", "/");

            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Project();
            get_tbl_Zone();
            //mp1.Show();
            if (Session["Person_Id"] != null && Session["Person_Id"].ToString() != "0" && (Session["ProjectWork_Id"] == null || Session["ProjectWork_Id"].ToString() == "0"))
            {
                divLogin.Visible = false;
                divSearch.Visible = true;
                divLoginStatus.Visible = true;
                lblWelcome.InnerHtml = "Welcome: " + Session["Person_Name"].ToString();
            }
            else if (Session["ProjectWork_Id"] != null && Session["ProjectWork_Id"].ToString() != "0")
            {
                divLogin.Visible = false;
                divLoginStatus.Visible = true;
                lblWelcome.InnerHtml = "Welcome: " + Session["Person_Name"].ToString();
                Load_Project();
            }
            else
            {
                divLoginStatus.Visible = false;
                divLogin.Visible = true;
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        btnLogin.Attributes.Add("onclick", "javascript:return md5auth('" + Convert.ToString(Session["rno"]) + "');");
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUpload.ID;
        up.Triggers.Add(trg1);
    }
    public string ReturnHash(string strPassword, string token)
    {
        string randomNo = token;
#pragma warning disable 618
        return FormsAuthentication.HashPasswordForStoringInConfigFile((randomNo + strPassword), "MD5");
#pragma warning restore 618
    }
    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
        e.IsValid = Captcha1.UserValidated;
        if (e.IsValid)
        {
            ViewState["IsValid"] = "true";
        }
        else
        {
            ViewState["IsValid"] = "false";
        }
    }
    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Project(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
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
        reset();
        get_tbl_ProjectWork();
    }

    protected void reset()
    {
        txtCompliance.Text = "";
        txtDaviation.Text = "";
        txtDelayReason.Text = "";
        txtDesignation1.Text = "";
        txtDesignation2.Text = "";
        txtHaltedReason.Text = "";
        txtInstContractor.Text = "";
        txtInstOfficer.Text = "";
        txtIssueContractor.Text = "";
        txtIssueRelatedOfficers.Text = "";
        txtLabourCount.Text = "";
        txtMajorIssue.Text = "";
        //txtMajorIssueQuality.Text = "";
        txtName1.Text = "";
        txtName2.Text = "";
        txtNonConfirmQuality.Text = "";
        txtOtherConcerns.Text = "";
        txtPhysicalProgress.Text = "";
        //txtProjectCode.Text = "";
        txtQualityReport.Text = "";
        txtVisitDate.Text = "";
        rbtAsPerCost.Items[0].Selected = false;
        rbtAsPerCost.Items[1].Selected = false;
        rbtAsPerDPR.Items[0].Selected = false;
        rbtAsPerDPR.Items[1].Selected = false;
        rbtDelayReason.Items[0].Selected = false;
        rbtDelayReason.Items[1].Selected = false;
        rbtHalted.Items[0].Selected = false;
        rbtHalted.Items[1].Selected = false;
        rbtIssueContractor.Items[0].Selected = false;
        rbtIssueContractor.Items[1].Selected = false;
        rbtIssueRelatedOfficers.Items[0].Selected = false;
        rbtIssueRelatedOfficers.Items[1].Selected = false;
        rbtMajorIssue.Items[0].Selected = false;
        rbtMajorIssue.Items[1].Selected = false;
        ddlQuality.SelectedValue = "0";
        ddlQualityWorkmanship.SelectedValue = "0";
        ddlVisitNumber.SelectedValue = "0";
        divCompliance.Visible = false;
        divComplianceOpen.Visible = false;
        grdPost1.DataSource = null;
        grdPost1.DataBind();

        grdPost.DataSource = null;
        grdPost.DataBind();
    }

    protected void get_Physical_Component(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Physical_Component_PMIS_Dashboard(0, 0, 0, "", 0, 0, 0, ProjectWork_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[0];
            grdPhysicalProgress.DataBind();
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
        }
    }
    protected void get_tbl_ProjectWork()
    {
        int ProjectType_Id = 0;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        if (ddlScheme.SelectedValue == "0")
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

        divUpload.Visible = false;
        hf_Financial_Progress.Value = "";
        hf_Physical_Progress.Value = "";
        Session["ProjectWork_Id"] = "0";

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork(ddlScheme.SelectedValue, 0, Zone_Id, Circle_Id, Division_Id, 0, "", ProjectType_Id, txtProjectCode.Text.Trim(), 0);
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

    protected void get_tbl_ProjectWork(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork("", 0, 0, 0, 0, 0, "", 0, "", ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost1.DataSource = ds.Tables[0];
            grdPost1.DataBind();
        }
        else
        {
            grdPost1.DataSource = null;
            grdPost1.DataBind();
        }
    }

    protected void get_tbl_ProjectVisit(int ProjectWork_Id, string VType)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectVisit(ProjectWork_Id, VType);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            int CurrentVisit = 0;
            try
            {
                CurrentVisit = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectVisit_VisitCount"].ToString());
            }
            catch
            {
                CurrentVisit = 0;
            }
            try
            {
                ddlVisitNumber.SelectedValue = (CurrentVisit + 1).ToString();
                ddlVisitNumber.Enabled = false;
            }
            catch
            {
                ddlVisitNumber.Enabled = true;
            }
            AllClasses.FillDropDown(ds.Tables[0], ddlVisitsMade, "ProjectVisit_Text", "ProjectVisit_Id");
        }
        else
        {
            ddlVisitNumber.SelectedValue = "1";
            ddlVisitsMade.Items.Clear();
        }

        if (ddlVisitNumber.SelectedValue == "1")
        {
            divCompliance.Visible = false;
            divComplianceOpen.Visible = false;
        }
        else
        {
            divCompliance.Visible = true;
            divComplianceOpen.Visible = true;
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
        GridViewRow gr = (lnkUpdate.Parent.Parent as GridViewRow);
        Session["ProjectWork_Id"] = gr.Cells[0].Text.Trim();
        hf_Physical_Progress.Value = gr.Cells[15].Text.Trim();
        hf_Financial_Progress.Value = gr.Cells[16].Text.Trim();
        Load_Project();
    }

    private void Load_Project()
    {
        divSearch.Visible = false;
        divLogin.Visible = false;
        get_tbl_ProjectWork(Convert.ToInt32(Session["ProjectWork_Id"].ToString()));
        get_Physical_Component(Convert.ToInt32(Session["ProjectWork_Id"].ToString()));
        get_tbl_ProjectVisit(Convert.ToInt32(Session["ProjectWork_Id"].ToString()), "False");
        string[] Location = hf_Location.Value.Replace(",", "").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        if (Location != null && Location.Length > 1)
        {
            lblLat.Text = Location[0];
            lblLong.Text = Location[1];
        }
        else
        {
            lblLat.Text = "-";
            lblLong.Text = "-";
        }
        lblAddress.Text = hf_Address.Value.Replace(",", "");
        divUpload.Visible = true;
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlVisitNumber.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Visit Number");
            return;
        }
        if (txtPhysicalProgress.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Current Physical Progress");
            txtPhysicalProgress.Focus();
            return;
        }
        if (txtPhysicalProgressScheduled.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Scheduled Physical Progress");
            txtPhysicalProgressScheduled.Focus();
            return;
        }
        if (txtTimeDaviationReason.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for work going on as per estimated time.");
            txtTimeDaviationReason.Focus();
            return;
        }
        if (txtTimeDaviationReason.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for work going on as per estimated time Atleast 50 Words.");
            txtTimeDaviationReason.Focus();
            return;
        }
        if (txtCostDaviationReason.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for work going on as per estimated cost.");
            txtCostDaviationReason.Focus();
            return;
        }
        if (txtCostDaviationReason.Text.Trim().Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for work going on as per estimated cost Atleast 50 Words.");
            txtCostDaviationReason.Focus();
            return;
        }

        if (txtHaltedReason.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for halted/stopped.");
            txtHaltedReason.Focus();
            return;
        }
        if (txtHaltedReason.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for halted/stopped Atleast 50 Words.");
            txtHaltedReason.Focus();
            return;
        }

        if (txtDelayReason.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for work delayed due to any specific reason.");
            txtDelayReason.Focus();
            return;
        }
        if (txtDelayReason.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for work delayed due to any specific reason Atleast 50 Words.");
            txtDelayReason.Focus();
            return;
        }

        if (txtDaviation.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for Work going on as per specifications given in the Contract Agreement.");
            txtDaviation.Focus();
            return;
        }
        if (txtDaviation.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for Work going on as per specifications given in the Contract Agreement Atleast 50 Words.");
            txtDaviation.Focus();
            return;
        }
        if (ddlQuality.SelectedValue == "Not Acceptable")
        {
            if (txtQualityReport.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Comments for report on the quality of construction materials used in the project and ascertain from the records.");
                txtQualityReport.Focus();
                return;
            }
            if (txtQualityReport.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
            {
                MessageBox.Show("Please Fill Comments for report on the quality of construction materials used in the project and ascertain from the records Atleast 50 Words.");
                txtQualityReport.Focus();
                return;
            }
        }
        if (ddlQualityWorkmanship.SelectedValue == "Not Acceptable")
        {
            if (txtNonConfirmQuality.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Comments for any cases of non-conformity from quality reviews based on available documents and interactions.");
                txtNonConfirmQuality.Focus();
                return;
            }
            if (txtNonConfirmQuality.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
            {
                MessageBox.Show("Please Fill Comments for any cases of non-conformity from quality reviews based on available documents and interactions Atleast 50 Words.");
                txtNonConfirmQuality.Focus();
                return;
            }
        }
        if (ddlVisitNumber.SelectedValue != "1")
        {
            if (txtCompliance.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Comments for Compliance Requirements from the Previous Visit.");
                txtCompliance.Focus();
                return;
            }
            if (txtCompliance.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
            {
                MessageBox.Show("Please Fill Comments for Compliance Requirements from the Previous Visit Atleast 50 Words.");
                txtCompliance.Focus();
                return;
            }
        }
        if (txtMajorIssue.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for major issues noticed during the visit.");
            txtMajorIssue.Focus();
            return;
        }
        if (txtMajorIssue.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for major issues noticed during the visit Atleast 50 Words.");
            txtMajorIssue.Focus();
            return;
        }
        if (txtIssueRelatedOfficers.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for issues raised by Key Concerned Officer(s) during the visit.");
            txtIssueRelatedOfficers.Focus();
            return;
        }
        if (txtIssueRelatedOfficers.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for issues raised by Key Concerned Officer(s) during the visit Atleast 50 Words.");
            txtIssueRelatedOfficers.Focus();
            return;
        }
        if (txtIssueContractor.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments for Any issues raised by the Contractor during the visit.");
            txtIssueContractor.Focus();
            return;
        }
        if (txtIssueContractor.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 50)
        {
            MessageBox.Show("Please Fill Comments for Any issues raised by the Contractor during the visit Atleast 50 Words.");
            txtIssueContractor.Focus();
            return;
        }
        if (!flAnnexture1.HasFile)
        {
            MessageBox.Show("Please Upload Site Photograph 1.");
            return;
        }
        if (!flAnnexture2.HasFile)
        {
            MessageBox.Show("Please Upload Site Photograph 2.");
            return;
        }
        if (!flAnnexture3.HasFile)
        {
            MessageBox.Show("Please Upload Site Photograph 3.");
            return;
        }
        string[] fNameArr1 = flAnnexture1.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr2 = flAnnexture2.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr3 = flAnnexture3.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr4 = flVideo.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr5 = flInspectionReport.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        string ext1 = fNameArr1[fNameArr1.Length - 1].Trim().Replace(".", "");
        string ext2 = fNameArr2[fNameArr2.Length - 1].Trim().Replace(".", "");
        string ext3 = fNameArr3[fNameArr3.Length - 1].Trim().Replace(".", "");
        string ext4 = "";
        string ext5 = "";

        if (fNameArr4.Length > 0)
        {
            ext4 = fNameArr4[fNameArr4.Length - 1].Trim().Replace(".", "");
        }
        else
        {
            ext4 = "";
        }
        if (fNameArr5.Length > 0)
        {
            ext5 = fNameArr5[fNameArr5.Length - 1].Trim().Replace(".", "");
        }
        else
        {
            ext5 = "";
        }
        int Work_Id = Convert.ToInt32(Session["ProjectWork_Id"].ToString());

        tbl_ProjectVisit obj_tbl_ProjectVisit = new tbl_ProjectVisit();

        obj_tbl_ProjectVisit.ProjectVisit_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectVisit.ProjectVisit_VisitCount = Convert.ToInt32(ddlVisitNumber.SelectedValue);
        obj_tbl_ProjectVisit.ProjectVisit_Comments = txtOtherConcerns.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectVisit.ProjectVisit_ProjectWork_Id = Work_Id;
        obj_tbl_ProjectVisit.ProjectVisit_Status = 1;
        try
        {
            obj_tbl_ProjectVisit.ProjectVisit_Latitude = Convert.ToDecimal(lblLat.Text);
        }
        catch
        {
            obj_tbl_ProjectVisit.ProjectVisit_Latitude = null;
        }
        try
        {
            obj_tbl_ProjectVisit.ProjectVisit_Longitude = Convert.ToDecimal(lblLong.Text);
        }
        catch
        {
            obj_tbl_ProjectVisit.ProjectVisit_Longitude = null;
        }
        obj_tbl_ProjectVisit.ProjectVisit_Status = 1;
        obj_tbl_ProjectVisit.ProjectVisit_SubmitionDate = txtVisitDate.Text.Trim();
        try
        {
            obj_tbl_ProjectVisit.ProjectVisit_PhysicalProgress = Convert.ToDecimal(hf_Physical_Progress.Value.Replace(",", ""));
        }
        catch
        {
            obj_tbl_ProjectVisit.ProjectVisit_PhysicalProgress = 0;
        }
        try
        {
            obj_tbl_ProjectVisit.ProjectVisit_FinancialProgress = Convert.ToDecimal(hf_Financial_Progress.Value.Replace(",", ""));
        }
        catch
        {
            obj_tbl_ProjectVisit.ProjectVisit_FinancialProgress = 0;
        }
        List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q1.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtName1.Text.Trim().Replace("'", "").Replace("--", "") + ", Designation: " + txtDesignation1.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 2;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q2.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtName2.Text.Trim().Replace("'", "").Replace("--", "") + ", Designation: " + txtDesignation2.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 3;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q3.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = ddlVisitNumber.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 4;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q4.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtVisitDate.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 5;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q5.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPhysicalProgress.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 36;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q41.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPhysicalProgressScheduled.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 6;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q6.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtAsPerTime.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 30;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q35.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTimeDaviationReason.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);
        
        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 34;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q39.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtAsPerCost.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 35;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q40.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtCostDaviationReason.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);
        
        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 7;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q7.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtHalted.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 8;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q8.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHaltedReason.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 9;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q9.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtDelayReason.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 10;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q10.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtDelayReason.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 11;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q11.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtLabourCount.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 37;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q42.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtLabourScheduled.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 12;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q12.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = ddlQuality.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 31;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q36.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtQualityOfWorkReason.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 13;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q13.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtQualityReport.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 14;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q14.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtAsPerDPR.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 15;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q15.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtDaviation.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        //obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        //obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        //obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        //obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 16;
        //obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        //obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q16.InnerHtml;
        //obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtMajorIssueQuality.Text.Trim().Replace("'", "").Replace("--", "");
        //obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 17;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q17.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = ddlQualityWorkmanship.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 32;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q37.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtWorkmanshipReason.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 18;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q18.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtNonConfirmQuality.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 33;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q38.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtCompliance.SelectedValue.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 19;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q19.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtCompliance.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 20;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q20.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtMajorIssue.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 21;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q21.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtMajorIssue.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 22;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q22.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIssueRelatedOfficers.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 23;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q23.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIssueRelatedOfficers.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 24;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q24.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIssueContractor.SelectedItem.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 25;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q25.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIssueContractor.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 26;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q26.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtInstOfficer.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 27;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q27.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtInstContractor.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 28;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q28.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOtherConcerns.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 29;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = Q29.InnerHtml;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = "Lat: " + lblLat.Text.Trim() + ", Long: " + lblLong.Text.Trim();
        obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);

        List<tbl_ProjectPkgSitePics> obj_tbl_ProjectPkgSitePics_Li = new List<tbl_ProjectPkgSitePics>();
        tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Work_Id;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flAnnexture1.FileBytes;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes2 = flAnnexture2.FileBytes;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes3 = flAnnexture3.FileBytes;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes4 = flVideo.FileBytes;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes5 = flInspectionReport.FileBytes;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = ext1;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path2 = ext2;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path3 = ext3;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path4 = ext4;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path5 = ext5;
        obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
        string msg = "";
        if (new DataLayer().Update_tbl_ProjectDPR_WorkStatus(obj_tbl_ProjectVisit, obj_tbl_ProjectUC_Concent_Li, obj_tbl_ProjectPkgSitePics_Li, ref msg))
        {
            MessageBox.Show("Field Visit Details Saved Successfully");
            btnSearch_Click(null, null);
            return;
        }
        else
        {
            MessageBox.Show("Unable To Save " + msg);
            return;
        }
    }

    protected void ddlVisitNumber_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnClose1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }

    protected void grdPost1_PreRender(object sender, EventArgs e)
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

    protected void grdPhysicalProgress_PreRender(object sender, EventArgs e)
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

    protected void grdVisitDetails_PreRender(object sender, EventArgs e)
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

    protected void btnGetVisitData_Click(object sender, EventArgs e)
    {
        if (ddlVisitsMade.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Visit No.");
            grdVisitDetails.DataSource = null;
            grdVisitDetails.DataBind();
            return;
        }
        get_tbl_ProjectVisit_Details(Convert.ToInt32(ddlVisitsMade.SelectedValue));
    }

    protected void get_tbl_ProjectVisit_Details(int ProjectVisit_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectVisit_Details(ProjectVisit_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_ProjectVisit_Id.Value = ProjectVisit_Id.ToString();
            divMap.Visible = true;
            divReportVerification.Visible = true;
            divMap1.InnerHtml = "<a onclick='return openPopup(this);' role='button' class='bigger bg-primary white' data-toggle='modal' lat='" + ds.Tables[0].Rows[0]["ProjectVisit_Latitude"].ToString() + "' long='" + ds.Tables[0].Rows[0]["ProjectVisit_Longitude"].ToString() + "'>&nbsp;View On Map</a>";
        }
        else
        {
            hf_ProjectVisit_Id.Value = "0";
            divMap.Visible = false;
            divReportVerification.Visible = false;
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            grdVisitDetails.DataSource = ds.Tables[1];
            grdVisitDetails.DataBind();
        }
        else
        {
            grdVisitDetails.DataSource = null;
            grdVisitDetails.DataBind();
        }

        if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            grdSitePics.DataSource = ds.Tables[2];
            grdSitePics.DataBind();
        }
        else
        {
            grdSitePics.DataSource = null;
            grdSitePics.DataBind();
        }
    }

    protected void grdSitePics_PreRender(object sender, EventArgs e)
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim().Replace("'", "") == "")
        {
            MessageBox.Show("Please Provide Valid User Name..!!");
            txtUserName.Focus();
            return;
        }
        if (txtPassowrd.Text.Trim().Replace("'", "") == "")
        {
            MessageBox.Show("Please Provide Password!!");
            txtPassowrd.Focus();
            return;
        }
        if (txtCaptcha.Text.Trim() == "")
        {
            MessageBox.Show("Please Solve Captcha Puzzle..!!");
            txtCaptcha.Focus();
            return;
        }
        if (ViewState["IsValid"].ToString() == "false")
        {
            MessageBox.Show("Please Solve Captcha Puzzle..!!");
            txtCaptcha.Focus();
            return;
        }
        DataSet ds = new DataSet();
        ds = new DataLayer().getLoginDetails(txtUserName.Text.Trim().Replace("'", ""));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string pass_MD5 = AllClasses.CreateMD5(ds.Tables[0].Rows[0]["Login_password"].ToString().Trim());
            var orignalpassword = ReturnHash(pass_MD5.ToUpper(), Convert.ToString(Session["rno"]));
            if (txtPassowrd.Text.Trim().Replace("'", "").Replace("<script>", "") == orignalpassword)
            {
                divLogin.Visible = false;
                divSearch.Visible = true;
                divLoginStatus.Visible = true;
                init_Session(ds);
            }
        }
        else
        {
            MessageBox.Show("Invalid Login Credentials!!");
            return;
        }
    }

    private void init_Session(DataSet ds)
    {
        Session["LoginHistory_Id"] = (new DataLayer()).Insert_tbl_LoginHistory(ds.Tables[0].Rows[0]["Person_Id"].ToString());
        Session["Login_Id"] = ds.Tables[0].Rows[0]["Login_Id"].ToString().Trim();
        Session["Person_BranchOffice_Id"] = ds.Tables[0].Rows[0]["Person_BranchOffice_Id"].ToString().Trim();
        Session["UserName"] = ds.Tables[0].Rows[0]["Login_UserName"].ToString().Trim();
        Session["UserType"] = ds.Tables[0].Rows[0]["PersonJuridiction_UserTypeId"].ToString();
        Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserType_Desc_E"].ToString();
        Session["Person_Id"] = ds.Tables[0].Rows[0]["Person_Id"].ToString();
        Session["Person_Name"] = ds.Tables[0].Rows[0]["Person_Name"].ToString();
        Session["Person_Mobile1"] = ds.Tables[0].Rows[0]["Person_Mobile1"].ToString();
        Session["ServerDate"] = ds.Tables[0].Rows[0]["ServerDate"].ToString();
        Session["M_Level_Id"] = ds.Tables[0].Rows[0]["M_Level_Id"].ToString();
        Session["M_Jurisdiction_Id"] = ds.Tables[0].Rows[0]["M_Jurisdiction_Id"].ToString();
        Session["PersonJuridiction_DesignationId"] = ds.Tables[0].Rows[0]["PersonJuridiction_DesignationId"].ToString();
        Session["PersonJuridiction_DepartmentId"] = ds.Tables[0].Rows[0]["PersonJuridiction_DepartmentId"].ToString();
        Session["PersonJuridiction_Project_Id"] = ds.Tables[0].Rows[0]["PersonJuridiction_Project_Id"].ToString();
        Session["Department_Name"] = ds.Tables[0].Rows[0]["Department_Name"].ToString();
        Session["Designation_DesignationName"] = ds.Tables[0].Rows[0]["Designation_DesignationName"].ToString();
        Session["TypingMode"] = "G";
        Session["District_Id"] = ds.Tables[0].Rows[0]["District_Id"].ToString();
        Session["District_Name"] = ds.Tables[0].Rows[0]["District_Name"].ToString();
        Session["ULB_Id"] = ds.Tables[0].Rows[0]["ULB_Id"].ToString();
        Session["ULB_Name"] = ds.Tables[0].Rows[0]["ULB_Name"].ToString();
        Session["Zone_Name"] = ds.Tables[0].Rows[0]["Zone_Name"].ToString();
        Session["Circle_Name"] = ds.Tables[0].Rows[0]["Circle_Name"].ToString();
        Session["Division_Name"] = ds.Tables[0].Rows[0]["Division_Name"].ToString();
        Session["PersonJuridiction_ZoneId"] = ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString();
        Session["PersonJuridiction_CircleId"] = ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString();
        Session["PersonJuridiction_DivisionId"] = ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString();
        Session["Login_IsDefault"] = ds.Tables[0].Rows[0]["Login_IsDefault"].ToString();
        lblWelcome.InnerHtml = "Welcome: " + ds.Tables[0].Rows[0]["Person_Name"].ToString();
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        divLogin.Visible = true;
        divLoginStatus.Visible = false;
        divSearch.Visible = false;
        divUpload.Visible = false;
    }

    protected void btnSearchAnother_Click(object sender, EventArgs e)
    {
        divLogin.Visible = false;
        divLoginStatus.Visible = true;
        divSearch.Visible = true;
        divUpload.Visible = false;
        Session["ProjectWork_Id"] = "0";
        hf_Physical_Progress.Value = "";
        hf_Financial_Progress.Value = "";
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdPost1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }

    protected void btnReportVerification_Click(object sender, EventArgs e)
    {
        tf_Marks.InnerHtml = "0 / 100";
        hf_MarksTotal.Value = "0";
        chk_App_1.Checked = false;
        chk_App_2.Checked = false;
        chk_App_3.Checked = false;
        chk_App_4.Checked = false;
        chk_App_5.Checked = false;
        chk_App_6.Checked = false;
        chk_App_7.Checked = false;
        chk_App_8.Checked = false;
        chk_App_9.Checked = false;
        chk_App_10.Checked = false;
        chk_NApp_1.Checked = false;
        chk_NApp_2.Checked = false;
        chk_NApp_3.Checked = false;
        chk_NApp_4.Checked = false;
        chk_NApp_5.Checked = false;
        chk_NApp_6.Checked = false;
        chk_NApp_7.Checked = false;
        chk_NApp_8.Checked = false;
        chk_NApp_9.Checked = false;
        chk_NApp_10.Checked = false;
        chk_NFilled_1.Checked = false;
        chk_NFilled_2.Checked = false;
        chk_NFilled_3.Checked = false;
        chk_NFilled_4.Checked = false;
        chk_NFilled_5.Checked = false;
        chk_NFilled_6.Checked = false;
        chk_NFilled_7.Checked = false;
        chk_NFilled_8.Checked = false;
        chk_NFilled_9.Checked = false;
        chk_NFilled_10.Checked = false;
        mp1.Show();
    }

    protected void btnSaveVerificationDetails_Click(object sender, EventArgs e)
    {
        tbl_ProjectVisitVerification obj_tbl_ProjectVisitVerification = new tbl_ProjectVisitVerification();
        obj_tbl_ProjectVisitVerification.ProjectVisitVerification_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_ProjectVisitVerification.ProjectVisitVerification_Marks = Convert.ToInt32(hf_MarksTotal.Value.Replace(",", ""));
        }
        catch
        {
            obj_tbl_ProjectVisitVerification.ProjectVisitVerification_Marks = 0;
        }
        obj_tbl_ProjectVisitVerification.ProjectVisitVerification_ProjectVisit_Id = Convert.ToInt32(hf_ProjectVisit_Id.Value.Replace(",", ""));
        obj_tbl_ProjectVisitVerification.ProjectVisitVerification_Status = 1;

        List<tbl_ProjectVisitVerificationAnswer> obj_tbl_ProjectVisitVerificationAnswer_Li = new List<tbl_ProjectVisitVerificationAnswer>();

        tbl_ProjectVisitVerificationAnswer obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_0.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_0.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_0.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE0.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_1.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_1.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_1.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_1.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE1.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);

        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_2.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_2.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_2.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_2.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 2;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE2.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_3.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_3.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_3.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_3.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 3;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE3.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_4.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_4.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_4.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_4.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 4;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE4.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_5.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_5.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_5.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_5.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 5;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE5.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_6.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_6.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_6.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_6.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 6;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE6.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_7.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_7.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_7.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 7;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE7.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_8.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_8.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_8.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        if (chk_NotApp_8.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotApplicable = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 8;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE8.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_9.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_9.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_9.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 9;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE9.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);


        obj_tbl_ProjectVisitVerificationAnswer = new tbl_ProjectVisitVerificationAnswer();
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (chk_App_10.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Appropriate = 0;
        if (chk_NApp_10.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotAppropriate = 0;
        if (chk_NFilled_10.Checked)
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 1;
        else
            obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_NotFilled = 0;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Status = 1;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_QuestionId = 10;
        obj_tbl_ProjectVisitVerificationAnswer.ProjectVisitVerificationAnswer_Question = QE10.InnerHtml;
        obj_tbl_ProjectVisitVerificationAnswer_Li.Add(obj_tbl_ProjectVisitVerificationAnswer);

        string msg = "";
        if (new DataLayer().Insert_tbl_ProjectVisitVerification(obj_tbl_ProjectVisitVerification, obj_tbl_ProjectVisitVerificationAnswer_Li, ref msg))
        {
            MessageBox.Show("Field Visit Verification Details Saved Successfully");
            btnSearch_Click(null, null);
            return;
        }
        else
        {
            MessageBox.Show("Unable To Save " + msg);
            mp1.Show();
            return;
        }
    }

    protected void EvaluateMarks(object sender, EventArgs e)
    {
        int TotalMarks = 0;
        try
        {
            TotalMarks = Convert.ToInt32(hf_MarksTotal.Value.Replace(",", ""));
        }
        catch
        {
            TotalMarks = 0;
        }

        RadioButton chk = sender as RadioButton;
        string[] ID = chk.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        if (Session["chk"] != null)
        {
            RadioButton rbt = (RadioButton)Session["chk"];
            string[] _ID = rbt.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (ID[2] == _ID[2])
            {
                if (ID[1] == "App")
                {
                    TotalMarks -= 10;
                }
                if (ID[1] == "NApp")
                {
                    TotalMarks -= 5;
                }
                if (ID[1] == "NotApp")
                {
                    TotalMarks -= 10;
                }
                if (ID[1] == "NFilled")
                {
                    TotalMarks -= 0;
                }
                else
                {

                }
            }
        }

        if (chk.Checked)
        {
            if (ID[1] == "App")
            {
                TotalMarks += 10;
            }
            if (ID[1] == "NApp")
            {
                TotalMarks += 5;
            }
            if (ID[1] == "NotApp")
            {
                TotalMarks += 10;
            }
            if (ID[1] == "NFilled")
            {
                TotalMarks += 0;
            }
            else
            {

            }
        }
        else
        {
            if (ID[1] == "App")
            {
                TotalMarks -= 10;
            }
            if (ID[1] == "NApp")
            {
                TotalMarks -= 5;
            }
            if (ID[1] == "NotApp")
            {
                TotalMarks -= 10;
            }
            if (ID[1] == "NFilled")
            {
                TotalMarks -= 0;
            }
            else
            {

            }
        }
        hf_MarksTotal.Value = TotalMarks.ToString().Replace(",", "");
        tf_Marks.InnerHtml = TotalMarks.ToString() + " / 100";
        Session["chk"] = chk;
        mp1.Show();
    }

    protected void EvaluateMarks1(object sender, EventArgs e)
    {
        int TotalMarks = 0;
        try
        {
            TotalMarks = Convert.ToInt32(hf_MarksTotal.Value.Replace(",", ""));
        }
        catch
        {
            TotalMarks = 0;
        }

        RadioButton chk = sender as RadioButton;
        string[] ID = chk.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        if (Session["chk"] != null)
        {
            RadioButton rbt = (RadioButton)Session["chk"];
            string[] _ID = rbt.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (ID[2] == _ID[2])
            {
                if (ID[1] == "App")
                {
                    TotalMarks -= 5;
                }
                if (ID[1] == "NApp")
                {
                    TotalMarks -= 3;
                }
                if (ID[1] == "NotApp")
                {
                    TotalMarks -= 5;
                }
                if (ID[1] == "NFilled")
                {
                    TotalMarks -= 0;
                }
                else
                {

                }
            }
        }

        if (chk.Checked)
        {
            if (ID[1] == "App")
            {
                TotalMarks += 5;
            }
            if (ID[1] == "NApp")
            {
                TotalMarks += 3;
            }
            if (ID[1] == "NotApp")
            {
                TotalMarks += 5;
            }
            if (ID[1] == "NFilled")
            {
                TotalMarks += 0;
            }
            else
            {

            }
        }
        else
        {
            if (ID[1] == "App")
            {
                TotalMarks -= 5;
            }
            if (ID[1] == "NApp")
            {
                TotalMarks -= 3;
            }
            if (ID[1] == "NotApp")
            {
                TotalMarks -= 5;
            }
            if (ID[1] == "NFilled")
            {
                TotalMarks -= 0;
            }
            else
            {

            }
        }
        hf_MarksTotal.Value = TotalMarks.ToString().Replace(",", "");
        tf_Marks.InnerHtml = TotalMarks.ToString() + " / 100";
        Session["chk"] = chk;
        mp1.Show();
    }

    protected void btnNewFormat_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectWorkFeildVisitUploadNew.aspx");
    }
}

