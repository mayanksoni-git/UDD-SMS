using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectWorkFeildVisitUploadView : System.Web.UI.Page
{
    private void get_tbl_FeildVisitL1()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FeildVisitL1();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlFeildVisitL1, "FeildVisitL1_Name", "FeildVisitL1_Id");
        }
        else
        {
            ddlFeildVisitL1.Items.Clear();
        }
    }

    private void get_tbl_FeildVisitL2(int FeildVisitL1_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FeildVisitL2(FeildVisitL1_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlFeildVisitL2, "FeildVisitL2_Name", "FeildVisitL2_Id");
        }
        else
        {
            ddlFeildVisitL2.Items.Clear();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            get_tbl_FeildVisitL1();

            int ProjectWork_Id = 0;
            int Division_Id = 0;
            int Added_By = 0;
            string VType = "";
            string Mode = "";
            try
            {
                ProjectWork_Id = Convert.ToInt32(Request.QueryString["ProjectWork_Id"].ToString());
            }
            catch
            {
                ProjectWork_Id = 0;
            }
            try
            {
                Mode = Request.QueryString["Mode"].ToString();
            }
            catch
            {
                Mode = "";
            }
            try
            {
                Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
            }
            catch
            {
                Division_Id = 0;
            }
            try
            {
                Added_By = Convert.ToInt32(Request.QueryString["Added_By"].ToString());
            }
            catch
            {
                Added_By = 0;
            }
            try
            {
                VType = Request.QueryString["VType"].ToString();
            }
            catch
            {
                VType = "";
            }
            hf_VType.Value = VType;
            Load_Project(ProjectWork_Id, Added_By, Division_Id, VType);
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUpload.ID;
        up.Triggers.Add(trg1);
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
    protected void get_tbl_ProjectWork(int ProjectWork_Id, int Added_By, int Division_Id, string VType)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork("", 0, 0, 0, Division_Id, 0, 0, "", ProjectWork_Id, Added_By);
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
            AllClasses.FillDropDown(ds.Tables[0], ddlVisitsMade, "ProjectVisit_Text", "ProjectVisit_Id");
            if (ds.Tables[0].Rows.Count > 1)
            {
                ddlVisitsMade.SelectedIndex = 1;
                btnGetVisitData_Click(btnGetVisitData, new EventArgs());
            }
        }
        else
        {
            ddlVisitsMade.Items.Clear();
        }
    }


    private void Load_Project(int ProjectWork_Id, int Added_By, int Division_Id, string VType)
    {
        get_tbl_ProjectWork(ProjectWork_Id, Added_By, Division_Id, VType);
        if (ProjectWork_Id > 0)
        {
            get_Physical_Component(ProjectWork_Id);
            get_tbl_ProjectVisit(ProjectWork_Id, VType);
        }
        else
        {
            if (grdPost1.Rows.Count > 0)
            {
                ImageButton btnView = grdPost1.Rows[0].FindControl("btnView") as ImageButton;
                btnView_Click(btnView, new ImageClickEventArgs(0, 0));
            }
        }
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
        hf_ProjectVisit_Id.Value = ProjectVisit_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectVisit_Details(ProjectVisit_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divMap.Visible = true;
            btnAddComments.Visible = true;
            divMap1.InnerHtml = "<a onclick='return openPopup(this);' role='button' class='bigger bg-primary white' data-toggle='modal' lat='" + ds.Tables[0].Rows[0]["ProjectVisit_Latitude"].ToString() + "' long='" + ds.Tables[0].Rows[0]["ProjectVisit_Longitude"].ToString() + "'>&nbsp;View On Map</a>";
        }
        else
        {
            divMap.Visible = false;
            btnAddComments.Visible = false;
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

        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            grdComments.DataSource = ds.Tables[3];
            grdComments.DataBind();
        }
        else
        {
            grdComments.DataSource = null;
            grdComments.DataBind();
        }

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ProjectVisit_AddedBy"].ToString() == Session["Person_Id"].ToString())
            {
                (grdSitePics.Rows[0].FindControl("btnEdit") as ImageButton).Visible = true;
            }
            else
            {
                (grdSitePics.Rows[0].FindControl("btnEdit") as ImageButton).Visible = false;
            }
        }
        else
        {
            (grdSitePics.Rows[0].FindControl("btnEdit") as ImageButton).Visible = false;
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

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = ((sender as ImageButton).Parent.Parent as GridViewRow);
        int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();

        get_Physical_Component(ProjectWork_Id);
        get_tbl_ProjectVisit(ProjectWork_Id, hf_VType.Value);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
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
        obj_tbl_ProjectVisit.ProjectVisit_Id = Convert.ToInt32(hf_ProjectVisit_Id.Value);
        obj_tbl_ProjectVisit.ProjectVisit_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        obj_tbl_ProjectVisit.ProjectVisit_Status = 1;

        tbl_ProjectPkgSitePics obj_tbl_ProjectPkgSitePics = new tbl_ProjectPkgSitePics();
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_ProjectWork_Id = Work_Id;
        obj_tbl_ProjectPkgSitePics.ProjectPkgSitePics_VisitId = Convert.ToInt32(hf_ProjectVisit_Id.Value);
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
        string msg = "";
        if (new DataLayer().Update_tbl_ProjectDPR_WorkStatus_Pics(obj_tbl_ProjectVisit, obj_tbl_ProjectPkgSitePics, ref msg))
        {
            MessageBox.Show("Field Visit Details Updated Successfully");
            btnGetVisitData_Click(btnGetVisitData, e);
            divVisitPics.Visible = false;
            return;
        }
        else
        {
            MessageBox.Show("Unable To Update " + msg);
            return;
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divVisitPics.Visible = true;
    }

    protected void grdPost1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void btnAddComments_Click(object sender, ImageClickEventArgs e)
    {
        mp1.Show();
    }

    protected void grdComments_PreRender(object sender, EventArgs e)
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

    protected void btnSaveComments_Click(object sender, EventArgs e)
    {
        if (txtComments.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Comments / Reply");
            mp1.Show();
            return;
        }
        tbl_ProjectVisitComment obj_tbl_ProjectVisitComment = new tbl_ProjectVisitComment();
        obj_tbl_ProjectVisitComment.ProjectVisitComment_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectVisitComment.ProjectVisitComment_ProjectVisit_Id = Convert.ToInt32(hf_ProjectVisit_Id.Value);
        try
        {
            obj_tbl_ProjectVisitComment.ProjectVisitComment_L1Id = Convert.ToInt32(ddlFeildVisitL1.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectVisitComment.ProjectVisitComment_L1Id = 0;
        }
        try
        {
            obj_tbl_ProjectVisitComment.ProjectVisitComment_L2Id = Convert.ToInt32(ddlFeildVisitL2.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectVisitComment.ProjectVisitComment_L2Id = 0;
        }
        obj_tbl_ProjectVisitComment.ProjectVisitComment_Comments = txtComments.Text.Trim();
        obj_tbl_ProjectVisitComment.ProjectVisitComment_Status = 1;

        if (new DataLayer().Insert_tbl_ProjectVisitComment(obj_tbl_ProjectVisitComment))
        {
            MessageBox.Show("Field Visit Comments Added Successfully");
            btnGetVisitData_Click(btnGetVisitData, e);
            divVisitPics.Visible = false;
            return;
        }
        else
        {
            MessageBox.Show("Unable To Add Comments ");
            return;
        }

    }

    protected void ddlFeildVisitL1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFeildVisitL1.SelectedValue == "0")
        {
            ddlFeildVisitL2.Items.Clear();
        }
        else
        {
            get_tbl_FeildVisitL2(Convert.ToInt32(ddlFeildVisitL1.SelectedValue));
        }
        mp1.Show();
    }
}

