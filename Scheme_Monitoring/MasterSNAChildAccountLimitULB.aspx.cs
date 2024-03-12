using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSNAChildAccountLimitULB : System.Web.UI.Page
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
            get_M_Jurisdiction();

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
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = "1014";
                ddlScheme.Enabled = false;
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
    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
        }
        else
        {
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue));
        }
    }
    private void get_tbl_ULB(int District_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ULB(District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlULB, "ULB_Name", "ULB_Id");
        }
        else
        {
            ddlULB.Items.Clear();
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
        int ULB_Id = 0;
        try
        {
            Scheme_Id = ddlScheme.SelectedValue;
        }
        catch
        {
            Scheme_Id = "";
        }
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        string FromDate = "";
        string TillDate = "";
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text;
            TillDate = txtDateTill.Text;
        }
        ds = (new DataLayer()).get_tbl_SNAAccountLimitExecutePark(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, FromDate, TillDate, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(AssignedLimit)", "").ToString();

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

    protected void lnkUpdateBalance_Click(object sender, EventArgs e)
    {
        LinkButton lnkUpdateBalance = (sender as LinkButton);
        GridViewRow gr = lnkUpdateBalance.Parent.Parent as GridViewRow;
        int SNAAccountMaster_Id = 0;
        try
        {
            SNAAccountMaster_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            SNAAccountMaster_Id = 0;
        }
        //int SNAAccountLimit_Id = 0;
        //try
        //{
        //    SNAAccountLimit_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        //}
        //catch
        //{
        //    SNAAccountLimit_Id = 0;
        //}
        string Account_No = gr.Cells[9].Text.Trim();


        decimal _Balance = 0;

        try
        {
            string _Bal = new DataLayerSNA().getSNA_IA_Balance(SNAAccountMaster_Id);
            _Balance = decimal.Parse(_Bal.Replace(",", ""));

            if (new DataLayer().Update_IA_Balance(SNAAccountMaster_Id, _Balance))
            {
                MessageBox.Show("Balance Updated From PNB.");
            }
            else
            {
                MessageBox.Show("Unable To Get Balance From PNB.");
            }
        }
        catch
        {
            MessageBox.Show("Error From APT.");
        }
    }
}