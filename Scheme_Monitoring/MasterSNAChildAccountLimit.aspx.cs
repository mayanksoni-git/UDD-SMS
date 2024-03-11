using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSNAChildAccountLimit : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void rbtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtSearchBy.SelectedValue == "1")
        {
            divFromDate.Visible = false;
            divTillDate.Visible = false;
        }
        else
        {
            divFromDate.Visible = true;
            divTillDate.Visible = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            txtDateFrom.Text = Session["ServerDate"].ToString();
            txtDateTill.Text = Session["ServerDate"].ToString();
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
            rbtSearchBy_SelectedIndexChanged(rbtSearchBy, e);
            get_tbl_SNAAccountLimitExecute();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUploadDoc.ID;
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
            bool is_Selected = false;
            ddlScheme.DataTextField = "Project_Name";
            ddlScheme.DataValueField = "Project_Id";
            ddlScheme.DataSource = ds.Tables[0];
            ddlScheme.DataBind();
            try
            {
                foreach (ListItem listItem in ddlScheme.Items)
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
                ddlScheme.Items[0].Selected = true;
            }
            if (is_Selected == false)
            {
                ddlScheme.Items[0].Selected = true;
            }
        }
        else
        {
            ddlScheme.Items.Clear();
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
        get_tbl_SNAAccountLimitExecute();
    }

    protected void get_tbl_SNAAccountLimitExecute()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = "";

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
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
        string FromDate = "";
        string TillDate = "";
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate ="";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text;
            TillDate = txtDateTill.Text;
        }
        ds = (new DataLayer()).get_tbl_SNAAccountLimitExecute(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, FromDate, TillDate, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(AssignedLimit2)", "").ToString();

            string SNAAccountLimit_FilePath = "";
            
            if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
            {
                for (int i = 0; i < grdPost.Rows.Count; i++)
                {
                    SNAAccountLimit_FilePath = grdPost.Rows[i].Cells[5].Text.Trim().Replace("&nbsp;", "");
                    if (SNAAccountLimit_FilePath == "")
                    {
                        (grdPost.Rows[i].FindControl("flUploadDoc") as FileUpload).Visible = true;
                        (grdPost.Rows[i].FindControl("lnkDownloadLetter") as LinkButton).Visible = false;
                    }
                    else
                    {
                        (grdPost.Rows[i].FindControl("flUploadDoc") as FileUpload).Visible = false;
                        (grdPost.Rows[i].FindControl("lnkDownloadLetter") as LinkButton).Visible = true;
                    }
                }
                divUpload.Visible = true;
            }
            else
            {
                for (int i = 0; i < grdPost.Rows.Count; i++)
                {
                    (grdPost.Rows[i].FindControl("flUploadDoc") as FileUpload).Visible = false;
                    SNAAccountLimit_FilePath = grdPost.Rows[i].Cells[5].Text.Trim().Replace("&nbsp;", "");
                    if (SNAAccountLimit_FilePath == "")
                    {
                        (grdPost.Rows[i].FindControl("lnkDownloadLetter") as LinkButton).Visible = false;
                    }
                    else
                    {
                        (grdPost.Rows[i].FindControl("lnkDownloadLetter") as LinkButton).Visible = true;
                    }
                }
                divUpload.Visible = false;
            }
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void lnkGOCount_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int ProjectWork_Id = 0;
        try
        {
            ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWork_Id = 0;
        }
        
        int District_Id = 0;
        try
        {
            District_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            District_Id = 0;
        }
    }

    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        List<tbl_SNAAccountLimit> obj_tbl_SNAAccountLimit_Li = new List<tbl_SNAAccountLimit>();
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            FileUpload flUploadDoc = (grdPost.Rows[i].FindControl("flUploadDoc") as FileUpload);
            tbl_SNAAccountLimit obj_tbl_SNAAccountLimit = new tbl_SNAAccountLimit();
            obj_tbl_SNAAccountLimit.SNAAccountLimit_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_SNAAccountLimit.SNAAccountLimit_ProjectWotk_Id = Convert.ToInt32(grdPost.Rows[i].Cells[2].Text.Trim());
            obj_tbl_SNAAccountLimit.SNAAccountLimit_Id = Convert.ToInt32(grdPost.Rows[i].Cells[1].Text.Trim());
            obj_tbl_SNAAccountLimit.SNAAccountLimit_Status = 1;
            if (flUploadDoc.HasFile)
            {
                obj_tbl_SNAAccountLimit.SNAAccountLimit_FilePathBytes = flUploadDoc.FileBytes;
                obj_tbl_SNAAccountLimit_Li.Add(obj_tbl_SNAAccountLimit);
            }
        }

        if (obj_tbl_SNAAccountLimit_Li.Count > 0)
        {
            if (new DataLayer().Upload_tbl_SNAAccountLimit_Order(obj_tbl_SNAAccountLimit_Li))
            {
                MessageBox.Show("Order Uploaded Successfully");
                get_tbl_SNAAccountLimitExecute();
                return;
            }
            else
            {
                MessageBox.Show("Error In Uploading Order");
                return;
            }
        }
        else
        {
            MessageBox.Show("Nothing To Save");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileUpload flUploadDoc = e.Row.FindControl("flUploadDoc") as FileUpload;
            LinkButton lnkDownloadLetter = e.Row.FindControl("lnkDownloadLetter") as LinkButton;
            string SNAAccountLimit_FilePath = e.Row.Cells[5].Text.Trim().Replace("&nbsp;", "");
            if (SNAAccountLimit_FilePath == "")
            {
                flUploadDoc.Visible = true;
                lnkDownloadLetter.Visible = false;
            }
            else
            {
                flUploadDoc.Visible = false;
                lnkDownloadLetter.Visible = true;
            }
        }
    }
}