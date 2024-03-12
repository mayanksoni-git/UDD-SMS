using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkUpload : System.Web.UI.Page
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

            get_tbl_Project();
            get_tbl_Zone();
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

            if (Request.QueryString.Count > 0)
            {
                string Show = "0";
                int Scheme_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int ProjectType_Id = 0;
                try
                {
                    Show = Request.QueryString["Show"].ToString();
                }
                catch
                {
                    Show = "0";
                }
                if (Show == "1")
                {
                    chkShow.Checked = true;
                }
                else
                {
                    chkShow.Checked = false;
                }
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                    ddlScheme.SelectedValue = Scheme_Id.ToString();
                    ddlScheme_SelectedIndexChanged(ddlScheme, e);
                }
                catch
                {
                    Scheme_Id = 0;
                }
                try
                {
                    ProjectType_Id = Convert.ToInt32(Request.QueryString["ProjectType_Id"].ToString());
                    ddlProjectType.SelectedValue = ProjectType_Id.ToString();
                }
                catch
                {
                    ProjectType_Id = 0;
                }
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                    ddlSearchZone.SelectedValue = Zone_Id.ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                    ddlSearchCircle.SelectedValue = Circle_Id.ToString();
                    ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                    ddlsearchDivision.SelectedValue = Division_Id.ToString();
                }
                catch
                {
                    Division_Id = 0;
                }
                get_tbl_ProjectWork();
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUpload.ID;
        up.Triggers.Add(trg1);
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
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
                ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
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
        get_tbl_ProjectWork();
    }

    protected void get_tbl_ProjectWork()
    {
        int ProjectType_Id = 0;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Show = "0";
        if (chkShow.Checked)
            Show = "1";
        else
            Show = "0";
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
        try
        {
            ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            ProjectType_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Annexture(ddlScheme.SelectedValue, 0, Zone_Id, Circle_Id, Division_Id, 0, "", ProjectType_Id, "", Show);
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
        hf_ProjectWork_Id.Value = gr.Cells[0].Text.Trim();
        divUpload.Visible = true;
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
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
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            ddlProjectType.Items.Clear();
        }
        else
        {
            int ProjectId = 0;
            try
            {
                ProjectId = Convert.ToInt32(ddlScheme.SelectedValue);
            }
            catch
            {
                ProjectId = 0;
            }
            get_tbl_ProjectType(ProjectId);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!flReport.HasFile)
        {
            MessageBox.Show("Please Upload Report File in PDF Format.");
            return;
        }
        if (!flAnnexture1.HasFile)
        {
            MessageBox.Show("Please Upload Annexture 1 of the Report in PDF Format.");
            return;
        }
        if (!flAnnexture2.HasFile)
        {
            MessageBox.Show("Please Upload Annexture 2 of the Report in PDF Format.");
            return;
        }
        if (!flAnnexture3.HasFile)
        {
            MessageBox.Show("Please Upload Annexture 3 of the Report in PDF Format.");
            return;
        }
        string[] fNameArr = flReport.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr1 = flAnnexture1.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr2 = flAnnexture2.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string[] fNameArr3 = flAnnexture3.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (!fNameArr[fNameArr.Length - 1].Contains("pdf"))
        {
            MessageBox.Show("Please Upload Report File in PDF Format.");
            return;
        }
        if (!fNameArr1[fNameArr1.Length - 1].Contains("pdf"))
        {
            MessageBox.Show("Please Upload Annexture 1 of the Report in PDF Format.");
            return;
        }
        if (!fNameArr2[fNameArr2.Length - 1].Contains("pdf"))
        {
            MessageBox.Show("Please Upload Annexture 2 of the Report in PDF Format.");
            return;
        }
        if (!fNameArr3[fNameArr3.Length - 1].Contains("pdf"))
        {
            MessageBox.Show("Please Upload Annexture 3 of the Report in PDF Format.");
            return;
        }
        tbl_ProjectWorkAnnexture obj_tbl_ProjectWorkAnnexture = new tbl_ProjectWorkAnnexture();
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_Path1_Bytes = flAnnexture1.FileBytes;
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_Path2_Bytes = flAnnexture2.FileBytes;
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_Path3_Bytes = flAnnexture3.FileBytes;
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_Report_Bytes = flReport.FileBytes;
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_Status = 1;
        obj_tbl_ProjectWorkAnnexture.ProjectWorkAnnexture_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        if (new DataLayer().Insert_tbl_ProjectWorkAnnexture(obj_tbl_ProjectWorkAnnexture))
        {
            MessageBox.Show("Report Uploaded Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error In Uploading Report");
            return;
        }
    }
}

