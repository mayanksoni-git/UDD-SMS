using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectWorkInspectionUpdate : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

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



            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                try
                {
                    ddlSearchScheme.SelectedValue = obj_SearchStorage.Scheme_Id;
                }
                catch
                {
                    ddlSearchScheme.SelectedValue = "0";
                }
                if (obj_SearchStorage.Zone_Id > 0)
                {
                    ddlZone.SelectedValue = obj_SearchStorage.Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                }
                if (obj_SearchStorage.Circle_Id > 0)
                {
                    ddlCircle.SelectedValue = obj_SearchStorage.Circle_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
                }
                if (obj_SearchStorage.Division_Id > 0)
                {
                    ddlDivision.SelectedValue = obj_SearchStorage.Division_Id.ToString();
                }
                if (obj_SearchStorage.Zone_Id + obj_SearchStorage.Circle_Id + obj_SearchStorage.Division_Id > 0)
                {
                    btnSearch_Click(btnSearch, e);
                }
            }
        }
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
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.District_Id = 0;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Project_Id;
        obj_SearchStorage.Search_By = "2";
        obj_SearchStorage.TillDate = "";
        obj_SearchStorage.ULB_Id = 0;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        DataSet ds = new DataSet();

        int ProjectWork_Id = 0;
        try
        {
            ProjectWork_Id = Convert.ToInt32(Request.QueryString["ProjectWork_Id"].ToString());
        }
        catch
        {
            ProjectWork_Id = 0;
        }

        ds = (new DataLayer()).get_tbl_ProjectWork(Project_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, 0, "", ProjectWork_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
        }
        else
        {
            divData.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void get_tbl_ProjectQuestionnaire(int Project_Id)
    {
        DataSet ds = new DataLayer().get_tbl_ProjectQuestionnaire(Project_Id);
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
    protected void dgvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int QuestionnaireId = 0;
            try
            {
                QuestionnaireId = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                QuestionnaireId = 0;
            }
            if (QuestionnaireId > 0)
            {
                DropDownList ddlQuestionnaireAnswer = (e.Row.FindControl("ddlQuestionnaireAnswer") as DropDownList);
                TextBox txtQuestionnaireAnswer = (e.Row.FindControl("txtQuestionnaireAnswer") as TextBox);
                DataSet ds = new DataLayer().get_tbl_ProjectAnswer(QuestionnaireId);
                if (AllClasses.CheckDataSet(ds))
                {
                    ddlQuestionnaireAnswer.Visible = true;
                    txtQuestionnaireAnswer.Visible = false;
                    AllClasses.FillDropDown(ds.Tables[0], ddlQuestionnaireAnswer, "ProjectAnswer_Name", "ProjectAnswer_Id");
                }
                else
                {
                    ddlQuestionnaireAnswer.Visible = false;
                    txtQuestionnaireAnswer.Visible = true;
                }
            }
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Project_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        string Client = ConfigurationManager.AppSettings.Get("Client");
        divInspectionForm.Visible = true;
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        hf_Physical.Value = gr.Cells[16].Text.Trim();
        hf_Financial.Value = gr.Cells[17].Text.Trim();
        get_tbl_ProjectQuestionnaire(Project_Id);
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        tbl_ProjectVisit obj_tbl_ProjectVisit = new tbl_ProjectVisit();
        List<tbl_ProjectPkgSitePics> obj_tbl_ProjectPkgSitePics_Li = new List<tbl_ProjectPkgSitePics>();
        obj_tbl_ProjectVisit.ProjectVisit_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_tbl_ProjectVisit.ProjectVisit_FinancialProgress = Convert.ToInt32(hf_Financial.Value);
        }
        catch
        {
            
        }
        obj_tbl_ProjectVisit.ProjectVisit_Comments = txtComments.Text.Trim();
        obj_tbl_ProjectVisit.ProjectVisit_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_ProjectVisit.ProjectVisit_Status = 1;
        obj_tbl_ProjectVisit.ProjectVisit_SubmitionDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        try
        {
            obj_tbl_ProjectVisit.ProjectVisit_PhysicalProgress = Convert.ToInt32(hf_Physical.Value);
        }
        catch
        {
            
        }
        obj_tbl_ProjectVisit.ProjectVisit_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li = new List<tbl_ProjectUC_Concent>();

        for (int i = 0; i < dgvQuestionnaire.Rows.Count; i++)
        {
            DropDownList ddlQuestionnaireAnswer = (dgvQuestionnaire.Rows[i].FindControl("ddlQuestionnaireAnswer") as DropDownList);
            TextBox txtQuestionnaireAnswer = (dgvQuestionnaire.Rows[i].FindControl("txtQuestionnaireAnswer") as TextBox);
            //if (ddlQuestionnaireAnswer.Visible && ddlQuestionnaireAnswer.SelectedValue == "0")
            //{
            //    MessageBox.Show("Please Select Answer For Selected Questionire");
            //    ddlQuestionnaireAnswer.Focus();
            //    return;
            //}
            //if (txtQuestionnaireAnswer.Visible && txtQuestionnaireAnswer.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please Fill Answer For Selected Questionire");
            //    txtQuestionnaireAnswer.Focus();
            //    return;
            //}
            tbl_ProjectUC_Concent obj_tbl_ProjectUC_Concent = new tbl_ProjectUC_Concent();
            obj_tbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            try
            {
                obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id = Convert.ToInt32(ddlQuestionnaireAnswer.SelectedValue);
            }
            catch
            {

            }
            if (txtQuestionnaireAnswer.Visible)
            {
                obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = txtQuestionnaireAnswer.Text.Trim();
            }
            else
            {
                obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer = "";
            }
            obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id = Convert.ToInt32(dgvQuestionnaire.Rows[i].Cells[1].Text.Trim());
            obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Status = 1;
            if (obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer != "" || obj_tbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id > 0)
                obj_tbl_ProjectUC_Concent_Li.Add(obj_tbl_ProjectUC_Concent);
        }

        tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
        obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_Status = 1;
        if (flPhoto1.HasFile)
        {   
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes1 = flPhoto1.FileBytes;
            string[] FileName = flPhoto1.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path1 = FileName[FileName.Length - 1];
        }
        if (flPhoto2.HasFile)
        {
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes2 = flPhoto2.FileBytes;
            string[] FileName = flPhoto2.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path2 = FileName[FileName.Length - 1];
        }
        if (flPhoto3.HasFile)
        {
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes3 = flPhoto3.FileBytes;
            string[] FileName = flPhoto3.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path3 = FileName[FileName.Length - 1];
        }
        if (flPhoto4.HasFile)
        {
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Bytes4 = flPhoto4.FileBytes;
            string[] FileName = flPhoto4.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_SitePic_Path4 = FileName[FileName.Length - 1];
        }
        obj_tbl_ProjectPkgSitePics_Li.Add(obj_tbl_ProjectPkgSitePics);
        string msg = "";
        if (new DataLayer().Update_tbl_ProjectDPR_WorkStatus(obj_tbl_ProjectVisit, obj_tbl_ProjectUC_Concent_Li, obj_tbl_ProjectPkgSitePics_Li, ref msg))
        {
            MessageBox.Show("Inspection Details Updated Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Unable To Update Inspection Details");
            return;
        }
    }
}