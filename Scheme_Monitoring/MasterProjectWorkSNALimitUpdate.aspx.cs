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

public partial class MasterProjectWorkSNALimitUpdate : System.Web.UI.Page
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
            get_tbl_ProjectWork_Assign_SNA_Limit_Header_Summery();
            get_tbl_ProjectWork_Assign_SNA_Limit_Summery();
        }
    }

    protected void get_tbl_ProjectWork_Assign_SNA_Limit_Header_Summery()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = "";

        Scheme_Id = ddlScheme.SelectedValue;
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        decimal Pool_Balance_A = 0;
        decimal Pool_Balance_U = 0;
        decimal Pool_Balance_1 = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit_Header_Summery(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                Pool_Balance_A = Convert.ToDecimal(ds.Tables[0].Rows[0]["SNAAccountMaster_Balance"].ToString());
            }
            catch
            {
                Pool_Balance_A = 0;
            }
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0 && Scheme_Id == "1013")
        {
            try
            {
                Pool_Balance_U = Convert.ToDecimal(ds.Tables[1].Rows[0]["ULB_Pool_Balance"].ToString());
            }
            catch
            {
                Pool_Balance_U = 0;
            }
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkAssigned.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountLimit_AssignedLimit"].ToString(), "Decimal");
            lnkUtilized.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountLimitUsed_UsedLimit"].ToString(), "Decimal");
            lnkAvailable.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountAvailableLimit"].ToString(), "Decimal");
            if (Scheme_Id == "1013")
            {
                lnkPoolBalance.Text = AllClasses.convert_To_Indian_No_Format((Pool_Balance_A + Pool_Balance_U).ToString(), "Decimal");
            }
            else
            {
                lnkPoolBalance.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountMaster_Balance"].ToString(), "Decimal");
            }
            if (Scheme_Id == "1013")
            {
                try
                {
                    Pool_Balance_1 = Convert.ToDecimal(ds.Tables[0].Rows[0]["SNAAccountMaster_Balance_1"].ToString());
                }
                catch
                {
                    Pool_Balance_1 = 0;
                }
                lnkPoolBalance_1.Text = AllClasses.convert_To_Indian_No_Format((Pool_Balance_1 + Pool_Balance_U).ToString(), "Decimal");
                lnkPoolBalance_2.Text = "0";
                lnkPoolBalance_3.Text = "0";
            }
            else if (Scheme_Id == "1016")
            {
                lnkPoolBalance_1.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountMaster_Balance_2"].ToString(), "Decimal");
                lnkPoolBalance_2.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountMaster_Balance_3"].ToString(), "Decimal");
                lnkPoolBalance_3.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["SNAAccountMaster_Balance_4"].ToString(), "Decimal");
            }
            else
            {
                lnkPoolBalance_1.Text = "0";
                lnkPoolBalance_2.Text = "0";
                lnkPoolBalance_3.Text = "0";
            }
        }
        else
        {
            lnkAssigned.Text = "0";
            lnkUtilized.Text = "0";
            lnkAvailable.Text = "0";
            lnkPoolBalance.Text = "0";
            lnkPoolBalance_1.Text = "0";
            lnkPoolBalance_2.Text = "0";
            lnkPoolBalance_3.Text = "0";
        }
        Set_SNA_Account_Balances(Scheme_Id);
    }

    private void Set_SNA_Account_Balances(string Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_SNAAccountParentMaster(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    H_sna1.InnerHtml = "Account No: " + ds.Tables[0].Rows[i]["SNAAccountParentMaster_ACCT_NO"].ToString();
                    lnkTotalBalance_1.Text = new DataLayerSNA().getSNA_Total_Balance(ds.Tables[0].Rows[i]["SNAAccountParentMaster_Id"].ToString());
                }
                if (i == 1)
                {
                    H_sna2.InnerHtml = "Account No: " + ds.Tables[0].Rows[i]["SNAAccountParentMaster_ACCT_NO"].ToString();
                    lnkTotalBalance_2.Text = new DataLayerSNA().getSNA_Total_Balance(ds.Tables[0].Rows[i]["SNAAccountParentMaster_Id"].ToString());
                }
                if (i == 2)
                {
                    H_sna3.InnerHtml = "Account No: " + ds.Tables[0].Rows[i]["SNAAccountParentMaster_ACCT_NO"].ToString();
                    lnkTotalBalance_3.Text = new DataLayerSNA().getSNA_Total_Balance(ds.Tables[0].Rows[i]["SNAAccountParentMaster_Id"].ToString());
                }
            }
            divSNAAccount_1.Visible = false;
            divSNAAccount_2.Visible = false;
            divSNAAccount_3.Visible = false;
            if (ds.Tables[0].Rows.Count == 3)
            {
                divSNAAccount_1.Visible = true;
                divSNAAccount_2.Visible = true;
                divSNAAccount_3.Visible = true;
            }
            else if (ds.Tables[0].Rows.Count == 2)
            {
                divSNAAccount_1.Visible = true;
                divSNAAccount_2.Visible = true;
            }
            else if (ds.Tables[0].Rows.Count == 1)
            {
                divSNAAccount_1.Visible = true;
            }
            else
            {
                divSNAAccount_1.Visible = false;
                divSNAAccount_2.Visible = false;
                divSNAAccount_3.Visible = false;
            }
        }

        lnkTotalBalance.Text = AllClasses.convert_To_Indian_No_Format((Convert.ToDecimal(lnkTotalBalance_1.Text) + Convert.ToDecimal(lnkTotalBalance_2.Text) + Convert.ToDecimal(lnkTotalBalance_3.Text)).ToString(), "Decimal");

        decimal LiveBalance = 0;
        decimal LiveBalance_1 = 0;
        decimal LiveBalance_2 = 0;
        decimal LiveBalance_3 = 0;
        decimal PoolBalance = 0;
        decimal PoolBalance_1 = 0;
        decimal PoolBalance_2 = 0;
        decimal PoolBalance_3 = 0;
        try
        {
            LiveBalance = decimal.Parse(lnkTotalBalance.Text);
        }
        catch
        {
            LiveBalance = 0;
        }
        try
        {
            LiveBalance_1 = decimal.Parse(lnkTotalBalance_1.Text);
        }
        catch
        {
            LiveBalance_1 = 0;
        }
        try
        {
            LiveBalance_2 = decimal.Parse(lnkTotalBalance_2.Text);
        }
        catch
        {
            LiveBalance_2 = 0;
        }
        try
        {
            LiveBalance_3 = decimal.Parse(lnkTotalBalance_3.Text);
        }
        catch
        {
            LiveBalance = 0;
        }
        try
        {
            PoolBalance = decimal.Parse(lnkPoolBalance.Text);
        }
        catch
        {
            PoolBalance = 0;
        }
        try
        {
            PoolBalance_1 = decimal.Parse(lnkPoolBalance_1.Text);
        }
        catch
        {
            PoolBalance_1 = 0;
        }
        try
        {
            PoolBalance_2 = decimal.Parse(lnkPoolBalance_2.Text);
        }
        catch
        {
            PoolBalance_2 = 0;
        }
        try
        {
            PoolBalance_3 = decimal.Parse(lnkPoolBalance_3.Text);
        }
        catch
        {
            PoolBalance_3 = 0;
        }
        lnkActualBalance.Text = AllClasses.convert_To_Indian_No_Format((LiveBalance - PoolBalance).ToString(), "Decimal");
        lnkActualBalance_1.Text = AllClasses.convert_To_Indian_No_Format((LiveBalance_1 - PoolBalance_1).ToString(), "Decimal");
        lnkActualBalance_2.Text = AllClasses.convert_To_Indian_No_Format((LiveBalance_2 - PoolBalance_2).ToString(), "Decimal");
        lnkActualBalance_3.Text = AllClasses.convert_To_Indian_No_Format((LiveBalance_3 - PoolBalance_3).ToString(), "Decimal");
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
            }
            catch
            {
                ddlScheme.SelectedIndex = 1;
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
        get_tbl_ProjectWork_Assign_SNA_Limit_Header_Summery();
        get_tbl_ProjectWork_Assign_SNA_Limit_Summery();
        grdPost.DataSource = null;
        grdPost.DataBind();
        divData.Visible = false;
        get_tbl_ProjectWork("");
    }

    protected void get_tbl_ProjectWork(string _Type)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = "";

        Scheme_Id = ddlScheme.SelectedValue;
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit_Update(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, _Type, 0, txtProjectCode.Text.Trim());
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

        Scheme_Id = ddlScheme.SelectedValue;
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit_Summery(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdAssignedLimitSummery.DataSource = ds.Tables[0];
            grdAssignedLimitSummery.DataBind();

            grdAssignedLimitSummery.FooterRow.Cells[2].Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(AssignedLimit)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(UsedLimit)", "").ToString();
            grdAssignedLimitSummery.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(AvailableLimit)", "").ToString();
        }
        else
        {
            grdAssignedLimitSummery.DataSource = null;
            grdAssignedLimitSummery.DataBind();
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
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
            ViewState["dtULB"] = ds.Tables[0];
        }
        else
        {
            ViewState["dtULB"] = null;
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
        //decimal Total_Current_Limit = 0;
        //decimal Total_Assigned_Limit = 0;
        //try
        //{
        //    Total_Assigned_Limit = Convert.ToDecimal(lnkAssigned.Text.Trim().Replace(",", ""));
        //}
        //catch
        //{
        //    Total_Assigned_Limit = 0;
        //}

        //decimal Total_Available_Limit = 0;
        //try
        //{
        //    Total_Available_Limit = Convert.ToDecimal(lnkTotalBalance.Text.Trim().Replace(",", ""));
        //}
        //catch
        //{
        //    Total_Available_Limit = 0;
        //}

        List<tbl_SNAAccountLimit> obj_tbl_SNAAccountLimit_Li = new List<tbl_SNAAccountLimit>();
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            CheckBox chkSelect = grdPost.Rows[i].FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked)
            {
                tbl_SNAAccountLimit obj_tbl_SNAAccountLimit = new tbl_SNAAccountLimit();
                obj_tbl_SNAAccountLimit.SNAAccountLimit_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit = Convert.ToDecimal((grdPost.Rows[i].FindControl("txtLimit") as TextBox).Text.Trim());
                    //Total_Current_Limit += obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit;
                }
                catch
                {
                    obj_tbl_SNAAccountLimit.SNAAccountLimit_AssignedLimit = 0;
                }
                try
                {
                    obj_tbl_SNAAccountLimit.SNAAccountLimit_Id = Convert.ToInt32(grdPost.Rows[i].Cells[3].Text.Trim());
                }
                catch
                {
                    obj_tbl_SNAAccountLimit.SNAAccountLimit_Id = 0;
                }
                obj_tbl_SNAAccountLimit.SNAAccountLimit_ProjectWotk_Id = Convert.ToInt32(grdPost.Rows[i].Cells[0].Text.Trim());
                obj_tbl_SNAAccountLimit.SNAAccountLimit_Status = 1;
                if (obj_tbl_SNAAccountLimit.SNAAccountLimit_Id > 0)
                {
                    obj_tbl_SNAAccountLimit_Li.Add(obj_tbl_SNAAccountLimit);
                }
            }
        }

        //decimal Total_Assigned_Current = Total_Current_Limit + Total_Assigned_Limit;

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
        get_tbl_ProjectWork_Assign_SNA_Limit_Header_Summery();
        get_tbl_ProjectWork_Assign_SNA_Limit_Summery();
    }

    protected void lnkSNA_Click(object sender, EventArgs e)
    {
        if (divSNAAccounts.Visible == true)
        {
            divSNAAccounts.Visible = false;
        }
        else
        {
            divSNAAccounts.Visible = true;
        }
    }

    protected void lnkPoolBalance_1_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPoolBalance_2_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPoolBalance_3_Click(object sender, EventArgs e)
    {

    }

    protected void lnkActualBalance_1_Click(object sender, EventArgs e)
    {

    }

    protected void lnkActualBalance_2_Click(object sender, EventArgs e)
    {

    }

    protected void lnkActualBalance_3_Click(object sender, EventArgs e)
    {

    }
}