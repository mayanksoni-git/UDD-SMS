using Aspose.Pdf.Operators;
using DocumentFormat.OpenXml.Presentation;
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

public partial class ProjectWorkFeildVisitUploadNew : System.Web.UI.Page
{
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_OHT = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_ZPS = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_TW = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_PH = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_BW = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_Pipeline = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_HC = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_RR = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_IW = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_IPS = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_SPS = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_MPS = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_SewerLine = new List<tbl_ProjectUC_Concent>();
    List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li_STP = new List<tbl_ProjectUC_Concent>();

    List<tbl_ProjectPkgSitePics> obj_tbl_ProjectPkgSitePics_Li = new List<tbl_ProjectPkgSitePics>();

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

            ViewState["OHT"] = obj_tbl_ProjectUC_Concent_Li_OHT;
            ViewState["ZPS"] = obj_tbl_ProjectUC_Concent_Li_ZPS;
            ViewState["TW"] = obj_tbl_ProjectUC_Concent_Li_TW;
            ViewState["PH"] = obj_tbl_ProjectUC_Concent_Li_PH;
            ViewState["BW"] = obj_tbl_ProjectUC_Concent_Li_BW;
            ViewState["Pipeline"] = obj_tbl_ProjectUC_Concent_Li_Pipeline;
            ViewState["HC"] = obj_tbl_ProjectUC_Concent_Li_HC;
            ViewState["RR"] = obj_tbl_ProjectUC_Concent_Li_RR;
            ViewState["IW"] = obj_tbl_ProjectUC_Concent_Li_IW;
            ViewState["IPS"] = obj_tbl_ProjectUC_Concent_Li_IPS;
            ViewState["SPS"] = obj_tbl_ProjectUC_Concent_Li_SPS;
            ViewState["MPS"] = obj_tbl_ProjectUC_Concent_Li_MPS;
            ViewState["SewerLine"] = obj_tbl_ProjectUC_Concent_Li_SewerLine;
            ViewState["STP"] = obj_tbl_ProjectUC_Concent_Li_STP;
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
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
        txtDesignation1.Text = "";
        txtDesignation2.Text = "";
        txtName1.Text = "";
        txtName2.Text = "";
        txtVisitDate.Text = "";
        ddlVisitNumber.SelectedValue = "0";
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
        get_tbl_ProjectVisit(Convert.ToInt32(Session["ProjectWork_Id"].ToString()), "True");
        string[] Location = hf_Location.Value.Replace(",", "").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

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

    protected void ddlSiteInspected_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbl_IW.Visible = false;
        tbl_RR.Visible = false;
        tbl_HC.Visible = false;
        tbl_Pipeline.Visible = false;
        tbl_BW.Visible = false;
        tbl_PH.Visible = false;
        tbl_ZPS.Visible = false;
        tbl_TW.Visible = false;
        tbl_OHT.Visible = false;
        tbl_IPS.Visible = false;
        tbl_SEWERLINE.Visible = false;
        tbl_STP.Visible = false;

        btnSaveBW.Visible = false;
        btnSaveHC.Visible = false;
        btnSaveTW.Visible = false;
        btnSaveOHT.Visible = false;
        btnSavePH.Visible = false;
        btnSavePipeline.Visible = false;
        btnSaveRR.Visible = false;
        btnSaveIW.Visible = false;
        btnSaveZPS.Visible = false;
        btnSaveIPS.Visible = false;
        btnSaveSPS.Visible = false;
        btnSaveMPS.Visible = false;
        btnSaveSewerLine.Visible = false;
        btnSaveSTP.Visible = false;

        if (ddlSiteInspected.SelectedValue == "OHT")
        {
            btnSaveOHT.Visible = true;
            tbl_OHT.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "ZPS")
        {
            btnSaveZPS.Visible = true;
            tbl_ZPS.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "TW")
        {
            btnSaveTW.Visible = true;
            tbl_TW.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "PH")
        {
            btnSavePH.Visible = true;
            tbl_PH.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "BW")
        {
            btnSaveBW.Visible = true;
            tbl_BW.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "PIPELINE")
        {
            btnSavePipeline.Visible = true;
            tbl_Pipeline.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "HC")
        {
            btnSaveHC.Visible = true;
            tbl_HC.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "RR")
        {
            btnSaveRR.Visible = true;
            tbl_RR.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "IW")
        {
            btnSaveIW.Visible = true;
            tbl_IW.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "IPS")
        {
            btnSaveIPS.Visible = true;
            tbl_IPS.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "SPS")
        {
            btnSaveSPS.Visible = true;
            tbl_IPS.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "MPS")
        {
            btnSaveMPS.Visible = true;
            tbl_IPS.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "SEWERLINE")
        {
            btnSaveSewerLine.Visible = true;
            tbl_SEWERLINE.Visible = true;
        }
        else if (ddlSiteInspected.SelectedValue == "STP")
        {
            btnSaveSTP.Visible = true;
            tbl_STP.Visible = true;
        }
        else
        {
            btnSaveBW.Visible = false;
            btnSaveHC.Visible = false;
            btnSaveTW.Visible = false;
            btnSaveOHT.Visible = false;
            btnSavePH.Visible = false;
            btnSavePipeline.Visible = false;
            btnSaveRR.Visible = false;
            btnSaveIW.Visible = false;
            btnSaveZPS.Visible = false;
            btnSaveIPS.Visible = false;
            btnSaveSPS.Visible = false;
            btnSaveMPS.Visible = false;
            btnSaveSewerLine.Visible = false;
            btnSaveSTP.Visible = false;

            tbl_IW.Visible = false;
            tbl_RR.Visible = false;
            tbl_HC.Visible = false;
            tbl_Pipeline.Visible = false;
            tbl_BW.Visible = false;
            tbl_PH.Visible = false;
            tbl_ZPS.Visible = false;
            tbl_TW.Visible = false;
            tbl_OHT.Visible = false;
            tbl_IPS.Visible = false;
            tbl_SEWERLINE.Visible = false;
            tbl_STP.Visible = false;
        }
    }
    protected void btnNewFormat_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectWorkFeildVisitUpload.aspx");
    }

    protected void btnSaveOHT_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_OHT = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Land Available";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTLandAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 2;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Capacity";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTCapacity.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 3;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Staging";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTStaging.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 4;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Soil Testing Done";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTSoilTestingDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 5;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Design Approved";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTDesignApproved.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 6;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FONDATION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTFoundation.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 7;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOTTOM DOME";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTBottomDome.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 8;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "WALLS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTWalls.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 9;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TOP DOME";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTTopDome.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 10;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "LEVEL INDICATOR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTLevelIndicator.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 11;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "LIGHTENING CONDUCTOR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTLightingConduct.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 12;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "VERTCAL PIPES";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTVerticalPipes.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 13;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RECEIVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTReceived.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 14;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TEST REPORTS AVAILABE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTTestReportsAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 15;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FIXED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTFixed.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 16;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DESIGN MIX";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTDesignMix.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 17;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CUBE TEST REPORTS AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTCubeTestAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 18;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CEMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTCement.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 19;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 20;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "AGGREGATES SIEVE ANALYSYS RESULTS AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTSieveAnalysis.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 21;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STEEL MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTSteel.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 22;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTTestReportAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 23;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SAMPLE COLLECTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTSampleCollected.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 24;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC TESTING DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtOHTHydraulicTestDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 25;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TARGET DATE OF COMPLETION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTTargetDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 26;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ANTICIPATED DATE OF COMPLETION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHRAnticipatedDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 27;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ANY ISSUES";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTIssues.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 28;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "OHT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARKS / INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtOHTRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_OHT.Add(obj_tbl_ProjectUC_Concent);

        ViewState["OHT"] = obj_tbl_ProjectUC_Concent_Li_OHT;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flOHTPhoto.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "OHT";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flOHTPhoto.FileBytes;
            string[] _FileName = flOHTPhoto.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }
        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveZPS_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_ZPS = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 29;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Capacity";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSCapacity.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 30;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Land Available";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSLandAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 31;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Soil Testing Done";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSSoilTestingDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 32;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Design Approved";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSDesignApproved.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 33;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FONDATION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSFondation.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 34;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "VERTICAL WALLS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSVerticalWalls.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 35;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLAB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSSlab.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 36;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP HOUSE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSPumpHouse.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 37;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PLASTER";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSPlaster.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 38;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FLOORING /FINISHING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSFlooringFinishing.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 39;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GANTRY GIRDER/CRANE FIXED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSGantryGrinder.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 40;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CHAIN /PULLY";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSChainPully.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 41;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DESIGN MIX";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSDesignMix.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 42;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CUBE TEST REPORTS AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSCubeTestAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 43;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CEMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSCement.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 44;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 45;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "AGGREGATES SIEVE ANALYSYS RESULTS AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSSieveAnalysisAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 46;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STEEL MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSSteelMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 47;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSTestReportAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 48;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SAMPLE COLLECTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSSampleCollected.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 49;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC TESTING DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtZPSHydrolicTestDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 50;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TARGET DATE OF COMPLETION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSTargetDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 51;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ANTICIPATED DATE OF COMPLETION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSAnticipatedDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 52;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ANY ISSUES";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSAnyIssues.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 53;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "ZPS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtZPSRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_ZPS.Add(obj_tbl_ProjectUC_Concent);

        ViewState["ZPS"] = obj_tbl_ProjectUC_Concent_Li_ZPS;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flZPSPhoto.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "ZPS";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flZPSPhoto.FileBytes;
            string[] _FileName = flZPSPhoto.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveTW_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_TW = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 54;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "No Of TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWNoOfTubewell.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 55;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Land Available";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWLandAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 56;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "LOCATION OF TW VISITED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWLocationVisited.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 57;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Size Of TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWSize.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 58;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Start OF DRILLING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWDateOfDrilling.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 59;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Completion OF DRILLING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWDrillingCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 60;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "LOWERING DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWLoweringDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 61;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DEPTH OF TW AND ASSEMBLY";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWDepthAssembly.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 62;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SIZE OF P GRAVEL";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWSizePGravel.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 62;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Start OF DEVELOPMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWDateDevelopment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 63;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "COMPRESSOR HRS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWCompressorHrs.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 63;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SAND CONTENT IN PPM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWSandContentPPM1.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 64;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OVERPUMPING UNIT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWOverPumpingUnit.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 65;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SAND CONTENT IN PPM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWSandContentPPM2.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 66;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISCHARGE MEASUREMENT DONE JOINTLY WITH CIVIL";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWDischargeMeasurment.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 67;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STRATA CHART AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWStrataChart.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 68;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "WATER TEST CHEMICAL";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWWaterTestChemical.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 69;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "WATER TEST BIOLOGICAL";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWWaterTestBiological.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 70;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CHLORINATING PLANT INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWChlorinatingPlantInstalled.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 71;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMPING PLANT CAPACITY OF MOTOR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWPumpingPlantCapacityMotor.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 72;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMPING PLANT CAPACIY OF PUMP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWPumpingPlantCapacityPump.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 73;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMPING PLANT SIZE OF COLUMN PIPE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWPumpingPlantSize.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 74;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMPING PLANT DEPTH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWPumpingPlantDepth.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 75;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "POWER CONNECTION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWPowerConnection.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 76;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ESTIMATE FROM UPPCL RECEIVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtTWEstimateReceived.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 77;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BL FORM AND DEPOSITION OF COST TO UPPCL";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWBLFormDecomposition.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 78;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DATE OF CONNECTION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtDateOfConnection.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 79;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ANY ISSUES";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWIssues.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 80;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "TW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtTWRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_TW.Add(obj_tbl_ProjectUC_Concent);

        ViewState["TW"] = obj_tbl_ProjectUC_Concent_Li_TW;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flTWPhoto.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "TW";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flTWPhoto.FileBytes;
            string[] _FileName = flTWPhoto.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }
        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSavePH_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_PH = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 81;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FONDATION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHFondation.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 82;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PLINTH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHPlinth.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 83;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "WALLS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHWalls.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 84;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLAB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHSlabs.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 85;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FIXING OF MS GIRDER";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHFixingMSGrinder.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 86;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PLASTER";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHPlaster.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 87;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FLOORING /FINISHING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPHFlooringFinishing.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 88;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RAMP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHRamp.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 89;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DOORS AND WINDOWS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHDoorsWindows.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 90;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PAINTING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHPainting.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 91;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CUBE TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPHCubeTestAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 92;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BRICK TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPHBrickTestReportAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 93;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "AGGREGATES SIEVE ANALYSYS RESULTS AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPHSieveAnalysisResultAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 94;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CEMENT TEST REPORT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPHCementTest.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 95;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 96;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTEMPER QUALTIY AND MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHDistamperQualityMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 97;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "APEX PAINT QUALITY AND MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHApexPaintQuality.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 98;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STEEL AND WOOD PAINT QUALITY AND MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHSteelWoodPaint.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 99;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "PH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPHRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_PH.Add(obj_tbl_ProjectUC_Concent);

        ViewState["PH"] = obj_tbl_ProjectUC_Concent_Li_PH;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flPHUpload.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "PH";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flPHUpload.FileBytes;
            string[] _FileName = flPHUpload.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveBW_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_BW = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 100;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FONDATION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWFondation.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 101;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PLINTH";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWPlinth.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 102;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Walls";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWWalls.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 103;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Slab";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWSlab.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 104;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Plaster";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWPlaster.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 105;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FLOORING /FINISHING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWFlooringFinishing.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 106;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DOORS AND WINDOWS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWDoorsWindows.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 107;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PAINTING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWPainting.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 108;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CUBE TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtBWCubeTestReport.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 109;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BRICK TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtBWBrickTestReport.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 110;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "AGGREGATES SIEVE ANALYSYS RESULTS AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtBWSieveAnalysis.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 111;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CEMENT TEST REPORT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtBWCementTest.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 112;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 113;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTEMPER QUALTIY AND MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWDistamperQuality.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 114;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "APEX PAINT QUALITY AND MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWApexPaint.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 115;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STEEL AND WOOD PAINT QUALITY AND MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWSteelWoodPaint.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 115;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "BW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtBWRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_BW.Add(obj_tbl_ProjectUC_Concent);

        ViewState["BW"] = obj_tbl_ProjectUC_Concent_Li_BW;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flBWUpload.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "BW";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flBWUpload.FileBytes;
            string[] _FileName = flBWUpload.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSavePipeline_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_Pipeline = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 116;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS DIA TYPE / MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineRisingMainsType.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 117;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS LENGTH PROPOSED IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineRisingMainsLengthP.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 118;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS LAID IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineRisingMainsLaid.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 119;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPipelineRisingMainsTestReportAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 120;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS EARTH COVER IN M";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineRisingMainsEarthCover.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 121;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS SAMPLE COLLECTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPipelineRisingMainsSampleCollected.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 122;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTRIBUTION SYSTEM DIA TYPE / MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineDistributionMake.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 123;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTRIBUTION SYSTEM LENGTH PROPOSED IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineDistributionLength.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 124;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTRIBUTION SYSTEM LAID IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineDistributionLaid.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 125;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTRIBUTION SYSTEM TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPipelineDistributionTestReportAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 126;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTRIBUTION SYSTEM EARTH COVER IN M";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineDistributionEarthCover.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 127;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISTRIBUTION SYSTEM SAMPLE COLLECTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPipelineDistributionSampleCollected.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 128;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "THRUST BLOCKS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPipelineThrustBlock.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 129;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NO OF LABOURES PRESENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineLabourPresent.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 130;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NO OF EXCAVATERS  EMPLOYED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineExcavatersEmployee.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 131;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SAFTEY MEASURES TAKEN";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtPipelineSaftyMeasuresTaken.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 132;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TARGET Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineTargetDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 133;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EXPECTED Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineExpectedDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 134;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "Pipeline";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtPipelineRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_Pipeline.Add(obj_tbl_ProjectUC_Concent);

        ViewState["Pipeline"] = obj_tbl_ProjectUC_Concent_Li_Pipeline;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flPipelineUpload.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "Pipeline";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flPipelineUpload.FileBytes;
            string[] _FileName = flPipelineUpload.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveHC_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_HC = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 135;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TARGET";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHCTarget.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 136;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "UP-TO DATE PROGRESS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHCUpToDateProgress.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 137;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NO OF GANGS OF LABOUR EMPLOYED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHCNoOfGangs.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 138;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHCDateOfCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 139;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EXPECTED Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHCExpectedDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 140;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ROAD REINSTATED AFTER H.C.";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtHCRoadReInstated.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 141;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "HC";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtHCRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_HC.Add(obj_tbl_ProjectUC_Concent);

        ViewState["HC"] = obj_tbl_ProjectUC_Concent_Li_HC;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flHCUpload.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "HC";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flHCUpload.FileBytes;
            string[] _FileName = flHCUpload.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveRR_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_RR = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 142;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TARGET IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtRRTarget.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 143;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ACHIVEMENT IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtRRAchivment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 144;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TRENCHES BEING FILLED WITH WATER FOR CONSOLIDATION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtRRTrenchesFilled.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 146;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DESIRED SAND FILLING DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtRRDesiredSandFilling.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 147;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OVER ALL QUALITY SATISFACTORY";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtRROverAllSatisfactory.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 148;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtRRDateOfCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 149;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtRRExpectedDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 150;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "RR";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtRRRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_RR.Add(obj_tbl_ProjectUC_Concent);

        ViewState["RR"] = obj_tbl_ProjectUC_Concent_Li_RR;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flRRUpload.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "RR";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flRRUpload.FileBytes;
            string[] _FileName = flRRUpload.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveIW_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_IW = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 151;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SITE CLEARANCE BY IRRIGATION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWSiteClearance.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 152;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FIELD TEST DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWFieldTestDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 153;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DESIGN APPROVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWDesignApproved.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 154;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INTAKE WELL TYPE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = ddIWlIntakeWellType.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 155;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Start";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWDateOfStart.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 156;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "UP-TO-DATE PROGRESS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWUpToDateProgress.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 157;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EXPECTED Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWExpectedDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 158;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOTTOM PLUGGED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWBottomPlugged.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 159;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLAB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWSlabBottom.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 160;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP HOUSE WALLS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWPumphouseWalls.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 161;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLAB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWSlabPumpHouse.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 162;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SCREENS SUPPLIED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWScreenSupplied.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 163;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SCREENS INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWScreenInstalled.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 164;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GATES SUPPLIED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWGatesSupplied.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 165;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GATES INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWGatesInstalled.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 166;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FINISHING WORKS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWFinishingWorks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 167;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP AND VALVES RECEIVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWPumpWallsReceived.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 168;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP FOUNDATION CASTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWPumpFondationCasted.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 169;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CUBE TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWCubeTestReport.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 170;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STEEL TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWSteelTestReport.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 171;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMPS INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWPumpInstalled.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 172;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "COMMON HEADER LAID";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWCommonHeaderLaid.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 172;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP AND VALVES INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWPumpValveInstalled.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 173;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PIPE RECEIVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWPipeReceived.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 174;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PIPE TEST REPORTS  AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWPipeTestReportAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 175;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC TESTIC DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWHydrolicTestDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 176;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAIN TESTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIWRisingMainsTested.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 177;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "IW";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIWRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_IW.Add(obj_tbl_ProjectUC_Concent);

        ViewState["IW"] = obj_tbl_ProjectUC_Concent_Li_IW;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flIWUpload.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "IW";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flIWUpload.FileBytes;
            string[] _FileName = flIWUpload.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlVisitNumber.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Visit Number");
            return;
        }

        int Work_Id = Convert.ToInt32(Session["ProjectWork_Id"].ToString());

        tbl_ProjectVisit obj_tbl_ProjectVisit = new tbl_ProjectVisit();

        obj_tbl_ProjectVisit.ProjectVisit_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectVisit.ProjectVisit_VisitCount = Convert.ToInt32(ddlVisitNumber.SelectedValue);
        obj_tbl_ProjectVisit.ProjectVisit_ProjectWork_Id = Work_Id;
        obj_tbl_ProjectVisit.ProjectVisit_Status = 1;
        obj_tbl_ProjectVisit.ProjectVisit_Type = "N";
        obj_tbl_ProjectVisit.ProjectVisit_TypeOfSource = rbtTypeOfSource.SelectedItem.Text;
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


        if (ViewState["OHT"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_OHT = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_OHT = (List<tbl_ProjectUC_Concent>)ViewState["OHT"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_OHT.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_OHT[i]);
        }

        if (ViewState["ZPS"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_ZPS = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_ZPS = (List<tbl_ProjectUC_Concent>)ViewState["ZPS"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_ZPS.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_ZPS[i]);
        }

        if (ViewState["TW"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_TW = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_TW = (List<tbl_ProjectUC_Concent>)ViewState["TW"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_TW.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_TW[i]);
        }

        if (ViewState["PH"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_PH = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_PH = (List<tbl_ProjectUC_Concent>)ViewState["PH"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_PH.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_PH[i]);
        }

        if (ViewState["BW"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_BW = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_BW = (List<tbl_ProjectUC_Concent>)ViewState["BW"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_BW.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_BW[i]);
        }

        if (ViewState["Pipeline"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_Pipeline = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_Pipeline = (List<tbl_ProjectUC_Concent>)ViewState["Pipeline"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_Pipeline.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_Pipeline[i]);
        }

        if (ViewState["HC"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_HC = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_HC = (List<tbl_ProjectUC_Concent>)ViewState["HC"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_HC.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_HC[i]);
        }

        if (ViewState["RR"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_RR = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_RR = (List<tbl_ProjectUC_Concent>)ViewState["RR"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_RR.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_RR[i]);
        }

        if (ViewState["IW"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_IW = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_IW = (List<tbl_ProjectUC_Concent>)ViewState["IW"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_IW.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_IW[i]);
        }

        if (ViewState["IPS"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_IPS = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_IPS = (List<tbl_ProjectUC_Concent>)ViewState["IPS"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_IPS.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_IPS[i]);
        }

        if (ViewState["SPS"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_SPS = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS = (List<tbl_ProjectUC_Concent>)ViewState["SPS"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_SPS.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_SPS[i]);
        }

        if (ViewState["MPS"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_MPS = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_MPS = (List<tbl_ProjectUC_Concent>)ViewState["MPS"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_MPS.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_MPS[i]);
        }

        if (ViewState["SewerLine"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_SewerLine = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SewerLine = (List<tbl_ProjectUC_Concent>)ViewState["SewerLine"];
        }
        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_SewerLine.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_SewerLine[i]);
        }
        if (ViewState["STP"] == null)
        {
            obj_tbl_ProjectUC_Concent_Li_STP = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_STP = (List<tbl_ProjectUC_Concent>)ViewState["STP"];
        }

        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li_STP.Count; i++)
        {
            obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent_Li_STP[i]);
        }

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (obj_tbl_ProjectUC_Concent_Li.Count == 0)
        {
            MessageBox.Show("Please Add Atleast One Component Visit To Save");
            return;
        }
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

    protected void SaveIPS(string Mode)
    {
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS = new List<tbl_ProjectUC_Concent>();
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS = new List<tbl_ProjectUC_Concent>();
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS = new List<tbl_ProjectUC_Concent>();
        }

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 151;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Land Available";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSLandAvailable.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }


        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 155;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Any Issue In Land Availability";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSLandIssue.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 152;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FIELD SOIL TEST DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSSoilTestDone.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 153;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DESIGN APPROVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSDesignApproved.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 154;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TYPE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = ddlIPSType.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 155;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Start";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 156;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "UP-TO-DATE PROGRESS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSUpToDateProgress.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 157;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EXPECTED Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSDateOfCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 158;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOTTOM PLUGGED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSBottomPlugged.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 159;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLAB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSBottomSlab.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 160;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP HOUSE WALLS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSPumpHouseWalls.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 161;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLAB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSPumpHouseWallsSlab.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 162;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SCREENS SUPPLIED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSScreenSupplied.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 163;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SCREENS INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSScreenInstalled.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 164;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GATES SUPPLIED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSGatesSupplied.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 165;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GATES INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSGatesInstalled.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 166;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "FINISHING WORKS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSFinishingWork.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 167;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP AND VALVES RECEIVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSPumpHouseWallsRec.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 168;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP FOUNDATION CASTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSPumpFoundationCasted.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 169;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CUBE TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSCubeTestReportAvailable.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 170;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STEEL TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSSteelTestReportAvailable.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 171;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMPS INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSPumpsInstalled.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 172;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "COMMON HEADER LAID";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSCommonHeaderLaid.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 172;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PUMP AND VALVES INSTALLED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSPumpValveInstalled.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 173;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PIPE RECEIVED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSPipeReceived.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 174;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PIPE TEST REPORTS  AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSPipeTestReportAvailable.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 175;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC TESTIC DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSHydrolicTestDone.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 176;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAIN TESTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtIPSRaisingMainTested.SelectedItem.Text;
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 177;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = Mode;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtIPSRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        if (Mode == "IPS")
        {
            obj_tbl_ProjectUC_Concent_Li_IPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else if (Mode == "MPS")
        {
            obj_tbl_ProjectUC_Concent_Li_MPS.Add(obj_tbl_ProjectUC_Concent);
        }
        else
        {
            obj_tbl_ProjectUC_Concent_Li_SPS.Add(obj_tbl_ProjectUC_Concent);
        }

        if (Mode == "IPS")
        {
            ViewState[Mode] = obj_tbl_ProjectUC_Concent_Li_IPS;
        }
        else if (Mode == "MPS")
        {
            ViewState[Mode] = obj_tbl_ProjectUC_Concent_Li_MPS;
        }
        else
        {
            ViewState[Mode] = obj_tbl_ProjectUC_Concent_Li_SPS;
        }

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flIPSSoilTestReport.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            if (Mode == "IPS")
            {
                obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "IPS";
            }
            else if (Mode == "MPS")
            {
                obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "MPS";
            }
            else
            {
                obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "SPS";
            }
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flIPSSoilTestReport.FileBytes;
            string[] _FileName = flIPSSoilTestReport.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
        }
        if (flIPSPhoto.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            if (Mode == "IPS")
            {
                obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "IPS";
            }
            else if (Mode == "MPS")
            {
                obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "MPS";
            }
            else
            {
                obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "SPS";
            }
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes2 = flIPSPhoto.FileBytes;
            string[] _FileName = flIPSPhoto.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path2 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
        }
        ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveIPS_Click(object sender, EventArgs e)
    {
        SaveIPS("IPS");
    }

    protected void btnSaveSPS_Click(object sender, EventArgs e)
    {
        SaveIPS("SPS");
    }

    protected void btnSaveMPS_Click(object sender, EventArgs e)
    {
        SaveIPS("MPS");
    }

    protected void btnSaveSewerLine_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_SewerLine = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 178;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS DIA TYPE / MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineRisingMainDia.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 179;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS LENGTH PROPOSED IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineRisingMainLength.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 180;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS LAID IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineRisingMainLaid.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 181;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineRisingMainTestReport.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 182;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS EARTH COVER IN M";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineRisingMainEarthCover.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 183;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "RISING MAINS SAMPLE COLLECTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineRisingMainSampleCollected.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);


        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 184;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE DIA TYPE / MAKE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineDia.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 185;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE LENGTH PROPOSED IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineLengthPraposed.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 186;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE LAID IN KM";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineLaid.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 187;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE TEST REPORT AVAILABLE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineTestReport.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 188;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE EARTH COVER IN M";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineEarthCover.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 189;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE SAMPLE COLLECTED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineSampleCollected.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 190;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE Laying Per Day";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineLayingPerDay.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 191;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE Desired Laying Per Day";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineDesiredLayingPerDay.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 192;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SEWER LINE Slope As Per Design";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineSlopeAsPerDesign.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 193;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NO OF LABOURES PRESENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineLaboursPresent.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 194;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NO OF EXCAVATERS  EMPLOYED";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineExcavatersEmployed.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 195;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SAFTEY MEASURES TAKEN";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineSaftyMeasuresTaken.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 196;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "M.H. Plastered Both Side";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineMHPlastered.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 197;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Propper Qunettee Constructed";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineProperQunettee.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 198;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BEDDING AS PER STANDARDS DONE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSewerLineBeddingStandredDone.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 199;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TARGET Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineDateTarget.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 200;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineDateCompletion.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 201;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "SewerLine";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSewerLineInstructions.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_SewerLine.Add(obj_tbl_ProjectUC_Concent);

        ViewState["SewerLine"] = obj_tbl_ProjectUC_Concent_Li_SewerLine;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flSewerLinePhoto.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "SewerLine";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flSewerLinePhoto.FileBytes;
            string[] _FileName = flSewerLinePhoto.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }

    protected void btnSaveSTP_Click(object sender, EventArgs e)
    {
        obj_tbl_ProjectUC_Concent_Li_STP = new List<tbl_ProjectUC_Concent>();

        tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 202;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STP Land Available";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSTPLandAvailable.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 203;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CTE From PCB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSTPPCB.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 204;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Date Of Applying";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDateApplying.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 205;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC DESIGN Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPHDSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 206;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC DESIGN VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPHDVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 207;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC DESIGN Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPHDApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 208;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC DESIGN Soil Test Report";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSTPHDSoilTestReport.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 209;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "HYDRAULIC DESIGN Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPHDRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 210;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 211;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICDrawingSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 212;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 213;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 214;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 215;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 216;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICExpectedDOC.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 217;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "INLET CHAMBER AND SCREENS Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPICRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 218;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 219;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCDrawingSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 220;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 221;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 222;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 223;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 224;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 225;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "GRIT CHAMBER Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPGCRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 226;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 227;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUDrawingSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 228;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 229;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 230;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 231;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUCompletionTargetDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 232;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDUCompletionExpectedDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 233;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "NITRIFICATION / DENITRIFICATION UNIT Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPNDURemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 234;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 235;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUDrawingSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 236;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 237;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 238;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 239;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 240;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRUExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 241;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PHOSPHORUS REMOVAL UNIT Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPRURemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 242;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 243;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 244;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 245;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 246;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 247;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 248;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTUExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 249;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "PRIMARY TREATMENT UNITS Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPPTURemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 250;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 251;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 252;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 253;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 254;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 255;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 256;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTUExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 257;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SECONDARY TREATMENT UNITS Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSTURemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 258;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 259;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 260;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 261;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 262;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 263;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUTragetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 264;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTUExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 265;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "TERTIARY TREATMENT UNITS Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPTTURemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 266;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 267;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 268;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 269;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 270;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 271;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 272;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDUExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 273;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DISINFECTION UNIT Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPDURemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 274;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 275;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 276;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 277;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 278;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 279;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 280;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 281;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE SUMP Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRSRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 282;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 283;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 284;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 285;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 286;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 287;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 288;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 289;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REUSE PUMPS Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRPRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 290;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 291;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 292;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 293;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 294;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 295;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 296;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 297;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "EFFLUENT LINE Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPELRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 298;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 299;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 300;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 301;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 302;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 303;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 304;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 305;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CENTRIFUSE / DRYING BEDS Remarks/Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCDBRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 306;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT DESCRIBE SCHEME/COMMENT";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUSchemeComment.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 307;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT DESIGN/DRAWING Submission Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUSubmissionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 308;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT VETTING Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUVettingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 309;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT Approval Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 310;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT Start Date";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 311;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT Target Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 312;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SLUDGE RECONDITIONING UNIT Expected Date Of Completion";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSRUExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 313;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Disposal Of SLUDGE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = ddlSTPSRUSludgeDisposal.SelectedItem.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 314;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Laboratory Building DRAWING  APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPLBDrawingApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 315;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Laboratory Building STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPLBScructureApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 316;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Laboratory Building Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPLBStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 317;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Laboratory Building Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPLBTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 318;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Laboratory Building Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPLBExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 319;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "Laboratory Building Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPLBRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 320;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ADMINISTRATIVE BUILDING DRAWING APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPABDrawingApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 321;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ADMINISTRATIVE BUILDING STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPABSrtuctureApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 322;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ADMINISTRATIVE BUILDING Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPABStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 323;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ADMINISTRATIVE BUILDING Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPABTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 324;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ADMINISTRATIVE BUILDING Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPABExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 325;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ADMINISTRATIVE BUILDING Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPABRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 326;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STAFF QUARTERS DRAWING APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSQDrawingApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 327;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STAFF QUARTERS STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSQStructureDesignApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 328;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STAFF QUARTERS Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSQStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 329;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STAFF QUARTERS Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSQTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 330;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STAFF QUARTERS Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSQExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 331;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "STAFF QUARTERS Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSQRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 332;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOUNDARY WALL DRAWING APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPBWDrawingApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 333;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOUNDARY WALL STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPBWStructureDesignApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 334;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOUNDARY WALL Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPBWStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 335;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOUNDARY WALL Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPBWTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 336;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOUNDARY WALL Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPBWExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 337;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "BOUNDARY WALL Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPBWRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 338;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OTHER COMPONENTS i.e. SOLAR SYSTEM Etc DRAWING APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPOCDrawingApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 339;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OTHER COMPONENTS i.e. SOLAR SYSTEM Etc STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPOCStructureDesignApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 340;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OTHER COMPONENTS i.e. SOLAR SYSTEM Etc Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPOCStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 341;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OTHER COMPONENTS i.e. SOLAR SYSTEM Etc Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPOCTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 342;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OTHER COMPONENTS i.e. SOLAR SYSTEM Etc Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPOCExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 343;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "OTHER COMPONENTS i.e. SOLAR SYSTEM Etc Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPOCRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 344;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SUB STATION BUILDING DRAWING APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSSBDesignApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 345;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SUB STATION BUILDING STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSSBStructureDesignApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 346;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SUB STATION BUILDING Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSSBStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 347;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SUB STATION BUILDING Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSSBTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 348;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SUB STATION BUILDING Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSSBExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 349;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "SUB STATION BUILDING Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPSSBRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 350;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "METER ROOM DRAWING APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPMRDrawingApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 351;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "METER ROOM STRCTURAL DESIGN APPROVAL DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPMRStructureDesignApprovalDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 352;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "METER ROOM Start DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPMRStartDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 353;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "METER ROOM Target Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPMRTargetCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 354;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "METER ROOM Expected Completion DATE";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPMRExpectedCompletionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 355;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "METER ROOM Remarks / Instructions";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPMRRemarks.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 356;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "ELECTRICITY CONNECTION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSTPElectricityConnection.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 357;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DATE OF CONNECTION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPConnectionDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 358;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "CTO FROM PCB";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = rbtSTPCTOPCB.SelectedItem.Text;
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 359;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DATE OF APPLYING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPAppicationDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 360;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DATE OF HYDRAULIC TESTING COMPLETION";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPHydrolicTestingDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 361;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "DATE OF COMMISSIONNING";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPCommissioningDate.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = 0;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = 362;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Type = "STP";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Question = "REMARK/INSTRUCTIONS";
        obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtSTPRemarksFinal.Text.Trim().Replace("'", "").Replace("--", "");
        obj_tbl_ProjectUC_Concent_Li_STP.Add(obj_tbl_ProjectUC_Concent);

        ViewState["STP"] = obj_tbl_ProjectUC_Concent_Li_STP;

        obj_tbl_ProjectPkgSitePics_Li = (List<tbl_ProjectPkgSitePics>)ViewState["SitePics"];
        if (flSTPPhoto.HasFile)
        {
            tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value); ;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = 0;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ComponentName = "STP";
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flSTPPhoto.FileBytes;
            string[] _FileName = flSTPPhoto.FileName.Split('.');
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = _FileName[_FileName.Length - 1].Trim().Replace(".", "");
            obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
            ViewState["SitePics"] = obj_tbl_ProjectPkgSitePics_Li;
        }

        MessageBox.Show("Details Saved For This Component. Please Proceed For Another Component or Click On Upload and Save Visit Details TO Complete Your Field Visit.");
    }
}

