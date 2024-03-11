using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_5 : System.Web.UI.Page
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
            get_tbl_TradeDocument_BG();
            get_tbl_TradeDocument_LD();
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
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                get_Package_Wise_Details(ProjectWork_Id);
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUploadDocs1.ID;
        PostBackTrigger trg2 = new PostBackTrigger();
        trg2.ControlID = btnUploadDocs2.ID;
        PostBackTrigger trg3 = new PostBackTrigger();
        trg3.ControlID = btnUploadDocs3.ID;
        up.Triggers.Add(trg1);
        up.Triggers.Add(trg2);
        up.Triggers.Add(trg3);
    }
    private void get_tbl_TradeDocument_BG()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_TradeDocument_BG();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtDocBG"] = ds.Tables[0];
        }
        else
        {
            ViewState["dtDocBG"] = null;
        }
    }
    private void get_tbl_TradeDocument_LD()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_TradeDocument_LD();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtDocLD"] = ds.Tables[0];
        }
        else
        {
            ViewState["dtDocLD"] = null;
        }
    }
    protected void get_Package_Wise_Details(int ProjectWork_Id)
    {
        divUpload.Visible = false;
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();

            if (grdPackageDetails.Rows.Count == 1)
            {
                ImageButton btnPackageEdit = grdPackageDetails.Rows[0].FindControl("btnPackageEdit") as ImageButton;
                btnPackageEdit_Click(btnPackageEdit, new ImageClickEventArgs(0, 0));
            }
        }
        else
        {
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
        }
    }

    protected void get_Package_Documents_Details_CB(string ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_Documents_Details_CB(ProjectWorkPkg_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdContractBond.DataSource = ds.Tables[0];
            grdContractBond.DataBind();
        }
        else
        {
            grdContractBond.DataSource = null;
            grdContractBond.DataBind();
        }
    }
    private void get_Package_Documents_Details_BG(string ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_Documents_Details_BG(ProjectWorkPkg_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtBG"] = ds.Tables[0];
            grdBG.DataSource = ds.Tables[0];
            grdBG.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["TradeDocument_Id"] = 0;
                dr["TradeDocument_Name"] = "";
                dr["ProjectWorkPkgDoc_Id"] = 0;
                dr["ProjectWorkPkgDoc_TypeId"] = 0;
                dr["ProjectWorkPkgDoc_Amount"] = 0;
                dr["ProjectWorkPkgDoc_WorkPkg_Id"] = 0;
                dr["ProjectWorkPkgDoc_Path"] = "";
                dr["ProjectWorkPkgDoc_DocumentNo"] = "";
                dr["ProjectWorkPkgDoc_DocumentDate"] = "";
                dr["ProjectWorkPkgDoc_DocumentDate2"] = "";
                dr["ProjectWorkPkgDoc_Comments"] = "";

                ds.Tables[0].Rows.Add(dr);

                ViewState["dtBG"] = ds.Tables[0];
                grdBG.DataSource = ds.Tables[0];
                grdBG.DataBind();
            }
            catch
            {
                grdBG.DataSource = null;
                grdBG.DataBind();
            }
        }
    }

    private void get_Package_Documents_Details_LD(string ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_Documents_Details_LD(ProjectWorkPkg_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtLD"] = ds.Tables[0];
            grdLD.DataSource = ds.Tables[0];
            grdLD.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["TradeDocument_Id"] = 0;
                dr["TradeDocument_Name"] = "";
                dr["ProjectWorkPkgDoc_Id"] = 0;
                dr["ProjectWorkPkgDoc_TypeId"] = 0;
                dr["ProjectWorkPkgDoc_Amount"] = 0;
                dr["ProjectWorkPkgDoc_WorkPkg_Id"] = 0;
                dr["ProjectWorkPkgDoc_Path"] = "";
                dr["ProjectWorkPkgDoc_DocumentNo"] = "";
                dr["ProjectWorkPkgDoc_DocumentDate"] = "";
                dr["ProjectWorkPkgDoc_DocumentDate2"] = "";
                dr["ProjectWorkPkgDoc_Comments"] = "";
                dr["ProjectWorkPkgDoc_PathW"] = "";
                dr["ProjectWorkPkgDoc_CommentsW"] = "";
                dr["ProjectWorkPkgDoc_Withdraw"] = 0;

                ds.Tables[0].Rows.Add(dr);

                ViewState["dtLD"] = ds.Tables[0];
                grdLD.DataSource = ds.Tables[0];
                grdLD.DataBind();
            }
            catch
            {
                grdLD.DataSource = null;
                grdLD.DataBind();
            }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < grdPackageDetails.Rows.Count; i++)
        {
            int AG_Count = 0;
            try
            {
                AG_Count = Convert.ToInt32(grdPackageDetails.Rows[i].Cells[15].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                AG_Count = 0;
            }
            if (AG_Count == 0)
            {
                MessageBox.Show("Please Upload Agreement Document");
                return;
            }
        }

        if (new DataLayer().Update_PMIS_Step_5_Details(Convert.ToInt32(hf_ProjectWork_Id.Value), Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            Response.Redirect("MasterProjectWorkMIS_6.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
        }
    }

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS_6.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
    }

    protected void grdPackageDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void grdContractBond_PreRender(object sender, EventArgs e)
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

    protected void grdContractBond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkNA = e.Row.FindControl("chkNA") as CheckBox;
            int DocumentType_Id = 0;
            int CB = 0;

            try
            {
                CB = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                CB = 0;
            }
            try
            {
                DocumentType_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                DocumentType_Id = 0;
            }
            if (CB == 1)
            {
                chkNA.Checked = true;
            }
            else
            {
                chkNA.Checked = false;
            }
            string filePath = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadCB = (LinkButton)e.Row.FindControl("lnkDownloadCB");
                lnkDownloadCB.Visible = false;
            }
        }
    }

    protected void btnDeleteCB_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkPkgDoc_Id = 0;
        try
        {
            ProjectWorkPkgDoc_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkPkgDoc_Id = 0;
        }
        if (ProjectWorkPkgDoc_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectWorkPkgDoc(ProjectWorkPkgDoc_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            get_Package_Documents_Details_CB(hf_ProjectWorkPkg_Id.Value);
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }

    protected void btnPackageEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        for (int i = 0; i < grdPackageDetails.Rows.Count; i++)
        {
            grdPackageDetails.Rows[i].BackColor = Color.Transparent;
        }
        GridViewRow gr = (lnkUpdate.Parent.Parent as GridViewRow);
        gr.BackColor = Color.LightGreen;
        string ProjectWorkPkg_Id = gr.Cells[0].Text.Trim();
        divUpload.Visible = true;
        hf_ProjectWorkPkg_Id.Value = ProjectWorkPkg_Id;
        get_Package_Documents_Details_CB(ProjectWorkPkg_Id);
        get_Package_Documents_Details_BG(ProjectWorkPkg_Id);
        get_Package_Documents_Details_LD(ProjectWorkPkg_Id);
    }

    protected void grdBG_PreRender(object sender, EventArgs e)
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

    protected void grdLD_PreRender(object sender, EventArgs e)
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

    protected void grdBG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkNA = e.Row.FindControl("chkNA") as CheckBox;
            int DocumentType_Id = 0;
            int BG = 0;
            try
            {
                BG = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                BG = 0;
            }
            try
            {
                DocumentType_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                DocumentType_Id = 0;
            }
            if (BG == 1)
            {
                chkNA.Checked = true;
            }
            else
            {
                chkNA.Checked = false;
            }
            string filePath = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            int TradeDocument_Id = 0;
            try
            {
                TradeDocument_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                TradeDocument_Id = 0;
            }
            DropDownList ddlDocumentType = e.Row.FindControl("ddlDocumentType") as DropDownList;
            DataTable dtDocBG = (DataTable)ViewState["dtDocBG"];
            if (AllClasses.CheckDt(dtDocBG))
            {
                if (e.Row.RowIndex == 0)
                    AllClasses.FillDropDown(dtDocBG, ddlDocumentType, "TradeDocument_Name", "TradeDocument_Id");
                else
                    AllClasses.FillDropDown_WithOutSelect(dtDocBG, ddlDocumentType, "TradeDocument_Name", "TradeDocument_Id");
                if (TradeDocument_Id > 0)
                {
                    ddlDocumentType.SelectedValue = TradeDocument_Id.ToString();
                }
            }
            if (filePath.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadBG = (LinkButton)e.Row.FindControl("lnkDownloadBG");
                lnkDownloadBG.Visible = false;
            }
        }
    }

    protected void grdLD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkNA = e.Row.FindControl("chkNA") as CheckBox;
            CheckBox chkWithdrawn = e.Row.FindControl("chkWithdrawn") as CheckBox;
            int DocumentType_Id = 0;
            int ProjectWorkPkgDoc_Withdraw = 0;
            int LD = 0;
            try
            {
                LD = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                LD = 0;
            }
            try
            {
                ProjectWorkPkgDoc_Withdraw = Convert.ToInt32(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                ProjectWorkPkgDoc_Withdraw = 0;
            }
            try
            {
                DocumentType_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                DocumentType_Id = 0;
            }
            if (LD == 1)
            {
                chkNA.Checked = true;
            }
            else
            {
                chkNA.Checked = false;
            }
            if (ProjectWorkPkgDoc_Withdraw == 1)
            {
                chkWithdrawn.Checked = true;
            }
            else
            {
                chkWithdrawn.Checked = false;
            }
            string filePath = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            string filePathW = e.Row.Cells[4].Text.Trim().Replace("&nbsp;", "");
            int TradeDocument_Id = 0;
            try
            {
                TradeDocument_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                TradeDocument_Id = 0;
            }
            DropDownList ddlDocumentType = e.Row.FindControl("ddlDocumentType") as DropDownList;
            DataTable dtDocLD = (DataTable)ViewState["dtDocLD"];
            if (AllClasses.CheckDt(dtDocLD))
            {
                if (e.Row.RowIndex == 0)
                    AllClasses.FillDropDown(dtDocLD, ddlDocumentType, "TradeDocument_Name", "TradeDocument_Id");
                else
                    AllClasses.FillDropDown_WithOutSelect(dtDocLD, ddlDocumentType, "TradeDocument_Name", "TradeDocument_Id");
                if (TradeDocument_Id > 0)
                {
                    ddlDocumentType.SelectedValue = TradeDocument_Id.ToString();
                }
            }
            if (filePath.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadLD = (LinkButton)e.Row.FindControl("lnkDownloadLD");
                lnkDownloadLD.Visible = false;
            }
            if (filePathW.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadLDWithdraw = (LinkButton)e.Row.FindControl("lnkDownloadLDWithdraw");
                lnkDownloadLDWithdraw.Visible = false;
            }
        }
    }

    protected void btnDeleteBG_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkPkgDoc_Id = 0;
        try
        {
            ProjectWorkPkgDoc_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkPkgDoc_Id = 0;
        }
        if (ProjectWorkPkgDoc_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectWorkPkgDoc(ProjectWorkPkgDoc_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            get_Package_Documents_Details_BG(hf_ProjectWorkPkg_Id.Value);
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
    protected void btnDeleteLD_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkPkgDoc_Id = 0;
        try
        {
            ProjectWorkPkgDoc_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkPkgDoc_Id = 0;
        }
        if (ProjectWorkPkgDoc_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectWorkPkgDoc(ProjectWorkPkgDoc_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            get_Package_Documents_Details_LD(hf_ProjectWorkPkg_Id.Value);
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
    protected void btnAddBG_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtBG;
        if (ViewState["dtBG"] != null)
        {
            dtBG = (DataTable)(ViewState["dtBG"]);
            DataRow dr = dtBG.NewRow();
            dtBG.Rows.Add(dr);
            ViewState["dtBG"] = dtBG;

            grdBG.DataSource = dtBG;
            grdBG.DataBind();
        }
        else
        {
            dtBG = new DataTable();

            DataColumn dc_TradeDocument_Id = new DataColumn("TradeDocument_Id", typeof(int));
            DataColumn dc_TradeDocument_Name = new DataColumn("TradeDocument_Name", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Path = new DataColumn("ProjectWorkPkgDoc_Path", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Id = new DataColumn("ProjectWorkPkgDoc_Id", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_TypeId = new DataColumn("ProjectWorkPkgDoc_TypeId", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_Amount = new DataColumn("ProjectWorkPkgDoc_Amount", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_WorkPkg_Id = new DataColumn("ProjectWorkPkgDoc_WorkPkg_Id", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_DocumentNo = new DataColumn("ProjectWorkPkgDoc_DocumentNo", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_DocumentDate = new DataColumn("ProjectWorkPkgDoc_DocumentDate", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_DocumentDate2 = new DataColumn("ProjectWorkPkgDoc_DocumentDate2", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Comments = new DataColumn("ProjectWorkPkgDoc_Comments", typeof(string));


            dtBG.Columns.AddRange(new DataColumn[] { dc_TradeDocument_Id, dc_TradeDocument_Name, dc_ProjectWorkPkgDoc_Path, dc_ProjectWorkPkgDoc_Id, dc_ProjectWorkPkgDoc_TypeId, dc_ProjectWorkPkgDoc_WorkPkg_Id, dc_ProjectWorkPkgDoc_DocumentNo, dc_ProjectWorkPkgDoc_DocumentDate, dc_ProjectWorkPkgDoc_DocumentDate2, dc_ProjectWorkPkgDoc_Amount, dc_ProjectWorkPkgDoc_Comments });

            DataRow dr = dtBG.NewRow();
            dtBG.Rows.Add(dr);
            ViewState["dtBG"] = dtBG;

            grdBG.DataSource = dtBG;
            grdBG.DataBind();
        }
    }

    protected void btnAddLD_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtLD;
        if (ViewState["dtLD"] != null)
        {
            dtLD = (DataTable)(ViewState["dtLD"]);
            DataRow dr = dtLD.NewRow();
            dtLD.Rows.Add(dr);
            ViewState["dtLD"] = dtLD;

            grdLD.DataSource = dtLD;
            grdLD.DataBind();
        }
        else
        {
            dtLD = new DataTable();

            DataColumn dc_TradeDocument_Id = new DataColumn("TradeDocument_Id", typeof(int));
            DataColumn dc_TradeDocument_Name = new DataColumn("TradeDocument_Name", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Path = new DataColumn("ProjectWorkPkgDoc_Path", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Id = new DataColumn("ProjectWorkPkgDoc_Id", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_TypeId = new DataColumn("ProjectWorkPkgDoc_TypeId", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_Amount = new DataColumn("ProjectWorkPkgDoc_Amount", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_WorkPkg_Id = new DataColumn("ProjectWorkPkgDoc_WorkPkg_Id", typeof(int));
            DataColumn dc_ProjectWorkPkgDoc_DocumentNo = new DataColumn("ProjectWorkPkgDoc_DocumentNo", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_DocumentDate = new DataColumn("ProjectWorkPkgDoc_DocumentDate", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_DocumentDate2 = new DataColumn("ProjectWorkPkgDoc_DocumentDate2", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Comments = new DataColumn("ProjectWorkPkgDoc_Comments", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_PathW = new DataColumn("ProjectWorkPkgDoc_PathW", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_CommentsW = new DataColumn("ProjectWorkPkgDoc_CommentsW", typeof(string));
            DataColumn dc_ProjectWorkPkgDoc_Withdraw = new DataColumn("ProjectWorkPkgDoc_Withdraw", typeof(string));


            dtLD.Columns.AddRange(new DataColumn[] { dc_TradeDocument_Id, dc_TradeDocument_Name, dc_ProjectWorkPkgDoc_Path, dc_ProjectWorkPkgDoc_Id, dc_ProjectWorkPkgDoc_TypeId, dc_ProjectWorkPkgDoc_WorkPkg_Id, dc_ProjectWorkPkgDoc_DocumentNo, dc_ProjectWorkPkgDoc_DocumentDate, dc_ProjectWorkPkgDoc_DocumentDate2, dc_ProjectWorkPkgDoc_Amount, dc_ProjectWorkPkgDoc_Comments, dc_ProjectWorkPkgDoc_PathW, dc_ProjectWorkPkgDoc_CommentsW, dc_ProjectWorkPkgDoc_Withdraw });

            DataRow dr = dtLD.NewRow();
            dtLD.Rows.Add(dr);
            ViewState["dtLD"] = dtLD;

            grdLD.DataSource = dtLD;
            grdLD.DataBind();
        }
    }

    protected void btnRemoveBG_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtBG"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtBG"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdBG.DataSource = dt;
            grdBG.DataBind();
            ViewState["dtBG"] = dt;
        }
    }
    protected void btnRemoveLD_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtLD"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtLD"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdLD.DataSource = dt;
            grdLD.DataBind();
            ViewState["dtLD"] = dt;
        }
    }
    protected void btnUploadDocs2_Click(object sender, EventArgs e)
    {
        string FilePath = "";
        List<tbl_ProjectWorkPkgDoc> obj_tbl_ProjectWorkPkgDoc_Li = new List<tbl_ProjectWorkPkgDoc>();

        for (int i = 0; i < grdBG.Rows.Count; i++)
        {
            TextBox txtBGFrom = grdBG.Rows[i].FindControl("txtBGFrom") as TextBox;
            TextBox txtBGTill = grdBG.Rows[i].FindControl("txtBGTill") as TextBox;
            TextBox txtRef_Number = grdBG.Rows[i].FindControl("txtRef_Number") as TextBox;
            DropDownList ddlDocumentType = grdBG.Rows[i].FindControl("ddlDocumentType") as DropDownList;
            TextBox txtAmount = grdBG.Rows[i].FindControl("txtAmount") as TextBox;
            TextBox txtComments = grdBG.Rows[i].FindControl("txtComments") as TextBox;
            FileUpload flUploadBG = grdBG.Rows[i].FindControl("flUploadBG") as FileUpload;
            CheckBox chkNA = grdBG.Rows[i].FindControl("chkNA") as CheckBox;
            FilePath = grdBG.Rows[i].Cells[2].Text.Trim().Replace("&nbsp;", "");

            tbl_ProjectWorkPkgDoc obj_tbl_ProjectWorkPkgDoc = new tbl_ProjectWorkPkgDoc();
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Id = Convert.ToInt32(grdBG.Rows[i].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Id = 0;
            }
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId = 0;
            }
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Amount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Amount = 0;
            }
            if (chkNA.Checked)
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 1;
            }
            else
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 0;
            }
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentDate = txtBGFrom.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentDate2 = txtBGTill.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentNo = txtRef_Number.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_WorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Status = 1;
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Comments = txtComments.Text.Trim();
            if (obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId > 0)
            {
                if (flUploadBG.HasFile || FilePath.Replace("&nbsp;", "") != "")
                {
                    if (txtRef_Number.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Document Refrence No");
                        return;
                    }
                    if (FilePath.Replace("&nbsp;", "") == "")
                    {
                        if (!flUploadBG.HasFile)
                        {
                            MessageBox.Show("Please Upload CB Document.");
                            return;
                        }
                    }
                    if (flUploadBG.HasFile)
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Path_Bytes = flUploadBG.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Path_Bytes = null;
                    }
                    obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 0;
                    obj_tbl_ProjectWorkPkgDoc_Li.Add(obj_tbl_ProjectWorkPkgDoc);
                }
                else if (chkNA.Checked)
                {
                    obj_tbl_ProjectWorkPkgDoc_Li.Add(obj_tbl_ProjectWorkPkgDoc);
                }
            }
        }
        if (obj_tbl_ProjectWorkPkgDoc_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_ProjectWorkPkgDoc(obj_tbl_ProjectWorkPkgDoc_Li))
            {
                MessageBox.Show("Document Uploaded Successfully");
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                get_Package_Wise_Details(ProjectWork_Id);
                return;
            }
            else
            {
                MessageBox.Show("Error In Uploading Documents");
                return;
            }
        }
        else
        {
            MessageBox.Show("No Document To Upload");
            return;
        }
    }
    protected void btnUploadDocs3_Click(object sender, EventArgs e)
    {
        string FilePath = "";
        string FilePathW = "";
        List<tbl_ProjectWorkPkgDoc> obj_tbl_ProjectWorkPkgDoc_Li = new List<tbl_ProjectWorkPkgDoc>();

        for (int i = 0; i < grdLD.Rows.Count; i++)
        {
            TextBox txtLDFrom = grdLD.Rows[i].FindControl("txtLDFrom") as TextBox;
            TextBox txtRef_Number = grdLD.Rows[i].FindControl("txtRef_Number") as TextBox;
            DropDownList ddlDocumentType = grdLD.Rows[i].FindControl("ddlDocumentType") as DropDownList;
            TextBox txtAmount = grdLD.Rows[i].FindControl("txtAmount") as TextBox;
            TextBox txtComments = grdLD.Rows[i].FindControl("txtComments") as TextBox;
            TextBox txtCommentsWithdrawal = grdLD.Rows[i].FindControl("txtCommentsWithdrawal") as TextBox;
            FileUpload flUploadLD = grdLD.Rows[i].FindControl("flUploadLD") as FileUpload;
            FileUpload flUploadLDWithdraw = grdLD.Rows[i].FindControl("flUploadLDWithdraw") as FileUpload;
            CheckBox chkNA = grdLD.Rows[i].FindControl("chkNA") as CheckBox;
            CheckBox chkWithdrawn = grdLD.Rows[i].FindControl("chkWithdrawn") as CheckBox;
            FilePath = grdLD.Rows[i].Cells[2].Text.Trim().Replace("&nbsp;", "");
            FilePathW = grdLD.Rows[i].Cells[4].Text.Trim().Replace("&nbsp;", "");

            tbl_ProjectWorkPkgDoc obj_tbl_ProjectWorkPkgDoc = new tbl_ProjectWorkPkgDoc();
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Id = Convert.ToInt32(grdLD.Rows[i].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Id = 0;
            }
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId = Convert.ToInt32(ddlDocumentType.SelectedValue);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId = 0;
            }
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Amount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Amount = 0;
            }
            if (chkNA.Checked)
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 1;
            }
            else
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 0;
            }
            if (chkWithdrawn.Checked)
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Withdraw = 1;
            }
            else
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Withdraw = 0;
            }
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentDate = txtLDFrom.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentNo = txtRef_Number.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_WorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Status = 1;
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Comments = txtComments.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_CommentsW = txtCommentsWithdrawal.Text.Trim();
            if (obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId > 0)
            {
                if (flUploadLD.HasFile || FilePath.Replace("&nbsp;", "") != "")
                {
                    if (txtRef_Number.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Letter Number");
                        return;
                    }
                    if (FilePath.Replace("&nbsp;", "") == "")
                    {
                        if (!flUploadLD.HasFile)
                        {
                            MessageBox.Show("Please Upload LD Document.");
                            return;
                        }
                    }
                    if (flUploadLD.HasFile)
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Path_Bytes = flUploadLD.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Path_Bytes = null;
                    }
                }
                if (flUploadLDWithdraw.HasFile || FilePathW.Replace("&nbsp;", "") != "")
                {
                    if (txtCommentsWithdrawal.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill Reason For LD Withdrawal");
                        return;
                    }
                    if (FilePathW.Replace("&nbsp;", "") == "")
                    {
                        if (!flUploadLDWithdraw.HasFile)
                        {
                            MessageBox.Show("Please Upload LD Withdrawal Document.");
                            return;
                        }
                    }
                    if (flUploadLDWithdraw.HasFile)
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_PathW_Bytes = flUploadLDWithdraw.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_PathW_Bytes = null;
                    }
                }
                if (chkNA.Checked)
                {
                    obj_tbl_ProjectWorkPkgDoc_Li.Add(obj_tbl_ProjectWorkPkgDoc);
                }
                else
                {
                    obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 0;
                    obj_tbl_ProjectWorkPkgDoc_Li.Add(obj_tbl_ProjectWorkPkgDoc);
                }
            }
        }
        if (obj_tbl_ProjectWorkPkgDoc_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_ProjectWorkPkgDoc(obj_tbl_ProjectWorkPkgDoc_Li))
            {
                MessageBox.Show("Document Uploaded Successfully");
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                get_Package_Wise_Details(ProjectWork_Id);
                return;
            }
            else
            {
                MessageBox.Show("Error In Uploading Documents");
                return;
            }
        }
        else
        {
            MessageBox.Show("No Document To Upload");
            return;
        }
    }
    protected void btnUploadDocs1_Click(object sender, EventArgs e)
    {
        string FilePath = "";
        List<tbl_ProjectWorkPkgDoc> obj_tbl_ProjectWorkPkgDoc_Li = new List<tbl_ProjectWorkPkgDoc>();

        for (int i = 0; i < grdContractBond.Rows.Count; i++)
        {
            TextBox txtCB_Date = grdContractBond.Rows[i].FindControl("txtCB_Date") as TextBox;
            TextBox txtRef_Number = grdContractBond.Rows[i].FindControl("txtRef_Number") as TextBox;
            TextBox txtComments = grdContractBond.Rows[i].FindControl("txtComments") as TextBox;
            FileUpload flUploadCB = grdContractBond.Rows[i].FindControl("flUploadCB") as FileUpload;
            CheckBox chkNA = grdContractBond.Rows[i].FindControl("chkNA") as CheckBox;
            FilePath = grdContractBond.Rows[i].Cells[2].Text.Trim().Replace("&nbsp;", "");

            tbl_ProjectWorkPkgDoc obj_tbl_ProjectWorkPkgDoc = new tbl_ProjectWorkPkgDoc();
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Id = Convert.ToInt32(grdContractBond.Rows[i].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Id = 0;
            }
            try
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId = Convert.ToInt32(grdContractBond.Rows[i].Cells[1].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId = 0;
            }
            if (chkNA.Checked)
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 1;
            }
            else
            {
                obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 0;
            }
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentDate = txtCB_Date.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_DocumentNo = txtRef_Number.Text.Trim();
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_WorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Status = 1;
            obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Comments = txtComments.Text.Trim();
            if (obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_TypeId > 0)
            {
                if (flUploadCB.HasFile || FilePath.Replace("&nbsp;", "") != "")
                {
                    if (txtCB_Date.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill CB Date");
                        return;
                    }
                    if (txtRef_Number.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Fill CB No");
                        return;
                    }
                    if (FilePath.Replace("&nbsp;", "") == "")
                    {
                        if (!flUploadCB.HasFile)
                        {
                            MessageBox.Show("Please Upload CB Document.");
                            return;
                        }
                    }
                    if (flUploadCB.HasFile)
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Path_Bytes = flUploadCB.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_Path_Bytes = null;
                    }
                    obj_tbl_ProjectWorkPkgDoc.ProjectWorkPkgDoc_IsNotApplicable = 0;
                    obj_tbl_ProjectWorkPkgDoc_Li.Add(obj_tbl_ProjectWorkPkgDoc);
                }
                else if (chkNA.Checked)
                {
                    obj_tbl_ProjectWorkPkgDoc_Li.Add(obj_tbl_ProjectWorkPkgDoc);
                }
            }
        }
        if (obj_tbl_ProjectWorkPkgDoc_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_ProjectWorkPkgDoc(obj_tbl_ProjectWorkPkgDoc_Li))
            {
                MessageBox.Show("Document Uploaded Successfully");
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                get_Package_Wise_Details(ProjectWork_Id);
                return;
            }
            else
            {
                MessageBox.Show("Error In Uploading Documents");
                return;
            }
        }
        else
        {
            MessageBox.Show("No Document To Upload");
            return;
        }
    }

    protected void chkNotApplicable_CheckedChanged(object sender, EventArgs e)
    {

    }
}