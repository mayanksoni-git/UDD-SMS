using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkSNALimitULB : System.Web.UI.Page
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
            get_M_Jurisdiction();
            get_tbl_Project();
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
            get_tbl_ProjectWork_Assign_SNA_Limit_Park_Header_Summery();
            get_tbl_ProjectWork_Assign_SNA_Limit_Summery();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUpdateLimit.ID;
        up.Triggers.Add(trg1);
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
        
    protected void get_tbl_ProjectWork_Assign_SNA_Limit_Park_Header_Summery()
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        string Project_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        
        try
        {
            Project_Id = ddlScheme.SelectedValue;
        }
        catch
        {
            Project_Id = "";
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit_Park_Header_Summery(Project_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkAssigned.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(SNAAccountLimit_AssignedLimit)", "").ToString(), "Decimal");
            lnkUtilized.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(SNAAccountLimitUsed_UsedLimit)", "").ToString(), "Decimal");
            lnkAvailable.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(SNAAccountAvailableLimit)", "").ToString(), "Decimal");
        }
        else
        {
            lnkAssigned.Text = "0";
            lnkUtilized.Text = "0";
            lnkAvailable.Text = "0";
        }

        lnkTotalBalance.Text = new DataLayerSNA().getSNA_Total_Balance("1");
    }
    protected void lnkTotalBalance_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";

        Scheme_Id = ddlScheme.SelectedValue;
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        Response.Redirect("Report_AccountStatementSNA.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        get_tbl_ProjectWork_Assign_SNA_Limit_Park_Header_Summery();
        get_tbl_ProjectWork_Assign_SNA_Limit_Summery();
        grdPost.DataSource = null;
        grdPost.DataBind();
        divData.Visible = false;
        //get_tbl_ProjectWork();
    }

    protected void get_tbl_ProjectWork(string _Type)
    {
        string Scheme_Id = "";

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit_Park(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, _Type, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divData.Visible = true;
            btnUpdateLimit.Visible = true;
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            divData.Visible = false;
            btnUpdateLimit.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void get_tbl_ProjectWork_Assign_SNA_Limit_Summery()
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit_Summery_Park(Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdAssignedLimitSummery.DataSource = ds.Tables[0];
            grdAssignedLimitSummery.DataBind();

            grdAssignedLimitSummery.FooterRow.Cells[2].Text = ds.Tables[0].Compute("sum(Total_ULB)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(AssignedLimit)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(UsedLimit)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(AvailableLimit)", "").ToString();
        }
        else
        {
            grdAssignedLimitSummery.DataSource = null;
            grdAssignedLimitSummery.DataBind();
        }
    }

   
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
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
        int GO_Count = 0;
        try
        {
            GO_Count = Convert.ToInt32((sender as LinkButton).Text.Trim());
        }
        catch
        {
            GO_Count = 0;
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
        if (ProjectWork_Id > 0 && GO_Count > 0)
        {
            get_tbl_ULB(District_Id);
            get_tbl_ProjectWorkGO(ProjectWork_Id);
            mp1.Show();
        }
        else
        {
            MessageBox.Show("GO Details Not Found");
            return;
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
    private void get_tbl_ProjectWorkGO(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "S");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
        }
        else
        {
            grdCallProductDtls.DataSource = null;
            grdCallProductDtls.DataBind();
        }
        ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "U");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdULBShare.DataSource = ds.Tables[0];
            grdULBShare.DataBind();
        }
        else
        {
            grdULBShare.DataSource = null;
            grdULBShare.DataBind();
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
    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[1].Text.Trim();
            if (filePath.Trim().Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkSCGO");
                lnkBtn.Visible = false;
            }
        }
    }
    protected void grdULBShare_PreRender(object sender, EventArgs e)
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
    protected void grdULBShare_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[1].Text.Trim();
            if (filePath.Trim().Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkULBGO");
                lnkBtn.Visible = false;
            }
        }
    }

    protected void btnUpdateLimit_Click(object sender, EventArgs e)
    {
        decimal Total_Current_Limit = 0;
        decimal Total_Assigned_Limit = 0;
        try
        {
            Total_Assigned_Limit = Convert.ToDecimal(lnkAssigned.Text.Trim().Replace(",", ""));
        }
        catch
        {
            Total_Assigned_Limit = 0;
        }

        decimal Total_Available_Limit = 0;
        try
        {
            Total_Available_Limit = Convert.ToDecimal(lnkTotalBalance.Text.Trim().Replace(",", ""));
        }
        catch
        {
            Total_Available_Limit = 0;
        }

        List<tbl_SNAAccountLimit> obj_tbl_SNAAccountLimit_Li = new List<tbl_SNAAccountLimit>();
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            FileUpload flUploadDoc = (grdPost.Rows[i].FindControl("flUploadDoc") as FileUpload);
            tbl_SNAAccountLimit obj_tbl_SNAAccountLimit = new tbl_SNAAccountLimit();
            obj_tbl_SNAAccountLimit.SNAAccountLimit_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            try
            {
                obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit = Convert.ToDecimal((grdPost.Rows[i].FindControl("txtLimit") as TextBox).Text.Trim());
                Total_Current_Limit += obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit;
            }
            catch
            {
                obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit = 0;
            }
            obj_tbl_SNAAccountLimit.SNAAccountLimit_ULB_Id = Convert.ToInt32(grdPost.Rows[i].Cells[0].Text.Trim());
            obj_tbl_SNAAccountLimit.SNAAccountLimit_Status = 1;
            if (flUploadDoc.HasFile)
            {
                obj_tbl_SNAAccountLimit.SNAAccountLimit_FilePathBytes = flUploadDoc.FileBytes;
            }
            else
            {
                MessageBox.Show("Please Upload Relavent Document");
                return;
            }
            if (obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit != 0)
            {
                obj_tbl_SNAAccountLimit_Li.Add(obj_tbl_SNAAccountLimit);
            }
        }

        //decimal Total_Assigned_Current = Total_Current_Limit + Total_Assigned_Limit;
        decimal Total_Assigned_Current = Total_Current_Limit;

        //if (Total_Assigned_Current > Total_Available_Limit)
        //{
        //    MessageBox.Show("Total Assigned Limit is More Than Total Available Balance");
        //    return;
        //}

        if (obj_tbl_SNAAccountLimit_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_SNAAccountLimit(obj_tbl_SNAAccountLimit_Li))
            {
                MessageBox.Show("Limit Updated Successfully");
                btnSearch_Click(btnSearch, e);
                return;
            }
            else
            {
                MessageBox.Show("Error In Limit Updation");
                return;
            }
        }
        else
        {
            MessageBox.Show("Nothing To Save");
            return;
        }
    }

    protected void lnkType_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        get_tbl_ProjectWork(gr.RowIndex.ToString());
    }

    protected void grdAssignedLimitSummery_PreRender(object sender, EventArgs e)
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

    protected void lnkTypeF_Click(object sender, EventArgs e)
    {
        get_tbl_ProjectWork("");
    }
}