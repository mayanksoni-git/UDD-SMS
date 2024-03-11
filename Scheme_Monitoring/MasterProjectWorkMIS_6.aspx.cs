using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_6 : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                get_tbl_ProjectIssue(Convert.ToInt32(hf_Scheme_Id.Value));
                get_UC_Details(ProjectWork_Id);
                get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);

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

    private void get_UC_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectUC(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
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
            grdCallProductDtls.DataSource = dt;
            grdCallProductDtls.DataBind();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FilePath = "";
        List<tbl_ProjectUC> obj_tbl_ProjectUC_Li = new List<tbl_ProjectUC>();
        for (int i = 0; i < grdCallProductDtls.Rows.Count; i++)
        {
            TextBox txtUCDate = grdCallProductDtls.Rows[i].FindControl("txtUCDate") as TextBox;
            TextBox txtUC_Number = grdCallProductDtls.Rows[i].FindControl("txtUC_Number") as TextBox;
            TextBox txtUCP = grdCallProductDtls.Rows[i].FindControl("txtUCP") as TextBox;
            FileUpload flUploadUC = grdCallProductDtls.Rows[i].FindControl("flUploadUC") as FileUpload;
            FilePath = grdCallProductDtls.Rows[i].Cells[1].Text.Trim();

            tbl_ProjectUC obj_tbl_ProjectUC = new tbl_ProjectUC();
            obj_tbl_ProjectUC.ProjectUC_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectUC.ProjectUC_SubmitionDate = txtUCDate.Text.Trim();
            obj_tbl_ProjectUC.ProjectUC_Comments = txtUC_Number.Text.Trim();
            obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_ProjectUC.ProjectUC_Status = 1;
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Id = Convert.ToInt32(grdCallProductDtls.Rows[i].Cells[0].Text.Trim());
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
            obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_WorkId = Convert.ToInt32(hf_ProjectWork_Id.Value);
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
                if (FilePath.Replace("&nbsp;", "") == "")
                {
                    if (!flUploadIssue.HasFile) // && obj_tbl_ProjectWorkIssueDetails_Li[i].ProjectWorkIssueDetails_Issue_Id != 0)
                    {
                        MessageBox.Show("Please Upload Issue Document.");
                        return;
                    }
                }
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
        bool flag = false;
        try
        {
            DataLayer dataLayer = new DataLayer();
            flag = dataLayer.Update_tbl_ProjectUC(obj_tbl_ProjectUC_Li, obj_tbl_ProjectWorkIssueDetails_Li, Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        catch (Exception ee)
        {

        }
        if (flag != false)
        {
            Response.Redirect("MasterProjectWorkMIS_7.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
        }
    }

    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        Add_Questionire("U");
    }

    private void Add_Questionire(string Entry_Type)
    {
        if (Entry_Type == "U")
        {
            DataTable dtUC;
            if (ViewState["dtUC"] != null)
            {
                dtUC = (DataTable)(ViewState["dtUC"]);
                DataRow dr = dtUC.NewRow();
                dtUC.Rows.Add(dr);
                ViewState["dtUC"] = dtUC;

                grdCallProductDtls.DataSource = dtUC;
                grdCallProductDtls.DataBind();
            }
            else
            {
                dtUC = new DataTable();

                DataColumn dc_Sr_No = new DataColumn("Sr_No", typeof(int));

                dtUC.Columns.AddRange(new DataColumn[] { dc_Sr_No });

                DataRow dr = dtUC.NewRow();
                dtUC.Rows.Add(dr);
                ViewState["dtUC"] = dtUC;

                grdCallProductDtls.DataSource = dtUC;
                grdCallProductDtls.DataBind();
            }
        }
        else
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
    }

    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtUC"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtUC"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdCallProductDtls.DataSource = dt;
            grdCallProductDtls.DataBind();
            ViewState["dtUC"] = dt;
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
        Add_Questionire("I");
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

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS_7.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
    }

    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void lnkConLog_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        divLog.Visible = true;
        List<tbl_PMIS_ProjectWorkIssueHistory> ProjectWorkIssueDetail_History_Li = new List<tbl_PMIS_ProjectWorkIssueHistory>();
        List <tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        if (ViewState["dtIssue"] != null)
        {
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);
            if (obj_tbl_ProjectWorkIssueDetails_Li[gr.RowIndex].ProjectWorkIssueDetail_History_Li != null)
            {
                ProjectWorkIssueDetail_History_Li = obj_tbl_ProjectWorkIssueDetails_Li[gr.RowIndex].ProjectWorkIssueDetail_History_Li;
            }
            if (ProjectWorkIssueDetail_History_Li != null && ProjectWorkIssueDetail_History_Li.Count > 0)
            {
                grdLog.DataSource = ProjectWorkIssueDetail_History_Li;
                grdLog.DataBind();
            }
            else
            {
                tbl_PMIS_ProjectWorkIssueHistory obj_tbl_PMIS_ProjectWorkIssueHistory = new tbl_PMIS_ProjectWorkIssueHistory();
                obj_tbl_PMIS_ProjectWorkIssueHistory.PMIS_ProjectWorkIssueHistory_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                ProjectWorkIssueDetail_History_Li.Add(obj_tbl_PMIS_ProjectWorkIssueHistory);
                obj_tbl_ProjectWorkIssueDetails_Li[gr.RowIndex].ProjectWorkIssueDetail_History_Li = ProjectWorkIssueDetail_History_Li;
                grdLog.DataSource = ProjectWorkIssueDetail_History_Li;
                grdLog.DataBind();
                ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
            }
            (grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value = gr.RowIndex.ToString();
        }
    }

    protected void grdLog_PreRender(object sender, EventArgs e)
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

    protected void btnAddLog_Click(object sender, ImageClickEventArgs e)
    {
        int Issue_Row_Indx = Convert.ToInt32((grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value);

        List<tbl_PMIS_ProjectWorkIssueHistory> ProjectWorkIssueDetail_History_Li = new List<tbl_PMIS_ProjectWorkIssueHistory>();
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        if (ViewState["dtIssue"] != null)
        {
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);

            ProjectWorkIssueDetail_History_Li = obj_tbl_ProjectWorkIssueDetails_Li[Issue_Row_Indx].ProjectWorkIssueDetail_History_Li;

            tbl_PMIS_ProjectWorkIssueHistory obj_tbl_PMIS_ProjectWorkIssueHistory = new tbl_PMIS_ProjectWorkIssueHistory();
            obj_tbl_PMIS_ProjectWorkIssueHistory.PMIS_ProjectWorkIssueHistory_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            ProjectWorkIssueDetail_History_Li.Add(obj_tbl_PMIS_ProjectWorkIssueHistory);
            obj_tbl_ProjectWorkIssueDetails_Li[Issue_Row_Indx].ProjectWorkIssueDetail_History_Li = ProjectWorkIssueDetail_History_Li;
            grdLog.DataSource = ProjectWorkIssueDetail_History_Li;
            grdLog.DataBind();
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
            (grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value = Issue_Row_Indx.ToString();
        }
    }

    protected void btnRemoveLog_Click(object sender, ImageClickEventArgs e)
    {
        int Issue_Row_Indx = Convert.ToInt32((grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value);

        List<tbl_PMIS_ProjectWorkIssueHistory> ProjectWorkIssueDetail_History_Li = new List<tbl_PMIS_ProjectWorkIssueHistory>();
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        if (ViewState["dtIssue"] != null)
        {
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);

            ProjectWorkIssueDetail_History_Li = obj_tbl_ProjectWorkIssueDetails_Li[Issue_Row_Indx].ProjectWorkIssueDetail_History_Li;
            ProjectWorkIssueDetail_History_Li.RemoveAt(ProjectWorkIssueDetail_History_Li.Count-1);

            obj_tbl_ProjectWorkIssueDetails_Li[Issue_Row_Indx].ProjectWorkIssueDetail_History_Li = ProjectWorkIssueDetail_History_Li;
            grdLog.DataSource = ProjectWorkIssueDetail_History_Li;
            grdLog.DataBind();
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
            (grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value = Issue_Row_Indx.ToString();
        }
    }

    protected void btnDeleteLog_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        List<tbl_PMIS_ProjectWorkIssueHistory> PMIS_ProjectWorkIssueHistory_Li = new List<tbl_PMIS_ProjectWorkIssueHistory>();
        int index = gr.RowIndex;
        if (ViewState["dtLog"] != null)
        {
            PMIS_ProjectWorkIssueHistory_Li = (List<tbl_PMIS_ProjectWorkIssueHistory>)ViewState["dtLog"];
            PMIS_ProjectWorkIssueHistory_Li.RemoveAt(index - 1);
            grdLog.DataSource = PMIS_ProjectWorkIssueHistory_Li;
            grdLog.DataBind();
            ViewState["dtLog"] = PMIS_ProjectWorkIssueHistory_Li;

            //DataTable dt = (DataTable)ViewState["dtLog"];
            //dt.Rows.RemoveAt(dt.Rows.Count - 1);
            //grdLog.DataSource = dt;
            //grdLog.DataBind();
            //ViewState["dtLog"] = dt;
        }
    }

    protected void btnSaveLog_Click(object sender, EventArgs e)
    {
        int Issue_Row_Indx = Convert.ToInt32((grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value);
        string FilePath = "";
        List<tbl_PMIS_ProjectWorkIssueHistory> ProjectWorkIssueDetail_History_Li = new List<tbl_PMIS_ProjectWorkIssueHistory>();
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        if (ViewState["dtIssue"] != null)
        {
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);

            ProjectWorkIssueDetail_History_Li = obj_tbl_ProjectWorkIssueDetails_Li[Issue_Row_Indx].ProjectWorkIssueDetail_History_Li;
            for (int i = 0; i < ProjectWorkIssueDetail_History_Li.Count; i++)
            {
                TextBox txtLogDate = grdLog.Rows[i].FindControl("txtLogDate") as TextBox;
                TextBox txtSubject = grdLog.Rows[i].FindControl("txtSubject") as TextBox;
                TextBox txtFrom = grdLog.Rows[i].FindControl("txtFrom") as TextBox;
                TextBox txtTo = grdLog.Rows[i].FindControl("txtTo") as TextBox;
                TextBox txtLogComments = grdLog.Rows[i].FindControl("txtLogComments") as TextBox;
                FileUpload flUploadLog = grdLog.Rows[i].FindControl("flUploadLog") as FileUpload;
                FilePath = grdLog.Rows[i].Cells[2].Text.Trim();
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_AddedBy= Convert.ToInt32(Session["Person_Id"].ToString());
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_Comments= txtLogComments.Text.Trim();
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_Date= txtLogDate.Text.Trim();
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_LetterWrittenBy= txtFrom.Text.Trim();
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_LetterWrittenTo= txtTo.Text.Trim();
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_Subject= txtSubject.Text.Trim();
                ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_Status = 1; 
                if (FilePath.Replace("&nbsp;", "") == "")
                {
                    if (!flUploadLog.HasFile)
                    {
                        //MessageBox.Show("Please Upload Issue Latter Document.");
                        //return;
                    }
                }
                try
                {
                    if (flUploadLog.HasFile)
                    {
                        ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_Path_Bytes = flUploadLog.FileBytes;
                    }
                    else
                    {
                        ProjectWorkIssueDetail_History_Li[i].PMIS_ProjectWorkIssueHistory_Path = null;
                    }
                }
                catch
                {

                }
            }
            obj_tbl_ProjectWorkIssueDetails_Li[Issue_Row_Indx].ProjectWorkIssueDetail_History_Li = ProjectWorkIssueDetail_History_Li;
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
            (grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value = Issue_Row_Indx.ToString();
        }
        else
        {
            MessageBox.Show("Error In saving Conversation !! ");
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
            get_Dependency(Convert.ToInt32(hf_Scheme_Id.Value), Convert.ToInt32(ddlIssueType.SelectedValue), ddlDependency);
        }
    }
}
