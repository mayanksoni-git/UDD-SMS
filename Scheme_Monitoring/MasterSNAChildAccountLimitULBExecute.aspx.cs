using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSNAChildAccountLimitULBExecute : System.Web.UI.Page
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
            get_M_Jurisdiction();
            get_tbl_SNAAccountLimitExecute();
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
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng", "M_Jurisdiction_Id");
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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_SNAAccountLimitExecutePark(Scheme_Id, District_Id, ULB_Id);
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


    protected void lnkBalance_Click(object sender, EventArgs e)
    {
        LinkButton lnkBalance = (sender as LinkButton);
        GridViewRow gr = lnkBalance.Parent.Parent as GridViewRow;
        int SNAAccountMaster_Id = 0;
        try
        {
            SNAAccountMaster_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            SNAAccountMaster_Id = 0;
        }
        int SNAAccountLimit_Id = 0;
        try
        {
            SNAAccountLimit_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            SNAAccountLimit_Id = 0;
        }
        int AssignedLimit = 0;
        try
        {
            AssignedLimit = Convert.ToInt32(gr.Cells[9].Text.Trim().Split(new char[] { '.'}, StringSplitOptions.RemoveEmptyEntries)[0]);
        }
        catch
        {
            AssignedLimit = 0;
        }
        decimal Available_Bal_PNB = 0;
        try
        {
            Available_Bal_PNB = Convert.ToDecimal(gr.Cells[13].Text.Trim());
        }
        catch
        {
            Available_Bal_PNB = 0;
        }
        decimal AssignedLimit_Current = 0;
        try
        {
            AssignedLimit_Current = Convert.ToDecimal(gr.Cells[9].Text.Trim());
        }
        catch
        {
            AssignedLimit_Current = 0;
        }
        string Account_No = gr.Cells[8].Text.Trim();
        if (SNAAccountMaster_Id > 0 && SNAAccountLimit_Id > 0 && AssignedLimit > 0 && Account_No != "")
        {
            DP_Limit obj_DP_Limit = new DP_Limit();
            obj_DP_Limit.AssignedLimit = Convert.ToInt32(Available_Bal_PNB + AssignedLimit_Current);
            obj_DP_Limit.SNAAccountLimit_Id = SNAAccountLimit_Id;
            obj_DP_Limit.SNAAccountMaster_ACCT_NO = Account_No;
            obj_DP_Limit.SNAAccountMaster_Id = SNAAccountMaster_Id;
            string Msg = "";
            if (new DataLayerSNA().setSNA_DP_Limit(obj_DP_Limit, ref Msg))
            {
                if (new DataLayer().Update_DP_Limit_Status(obj_DP_Limit.SNAAccountLimit_Id))
                {
                    MessageBox.Show("Set Limit Successfull.");
                    get_tbl_SNAAccountLimitExecute();
                }
            }
            else
            {
                if (Msg != "")
                {
                    MessageBox.Show(Msg);
                }
                else
                {
                    MessageBox.Show("Unable To Set Limit");
                }
            }
        }
        else
        {
            MessageBox.Show("Unable To Set Limit");
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkBalance = e.Row.FindControl("lnkBalance") as LinkButton;
            int SNAAccountLimit_Executed = 0;
            try
            {
                SNAAccountLimit_Executed = Convert.ToInt32(e.Row.Cells[4].Text.Trim());
            }
            catch
            {
                SNAAccountLimit_Executed = 0;
            }
            if (SNAAccountLimit_Executed == 0)
            {
                lnkBalance.Visible = true;
            }
            else
            {
                lnkBalance.Visible = false;
            }
        }
    }
}