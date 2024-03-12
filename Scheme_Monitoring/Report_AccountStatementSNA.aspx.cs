
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Report_AccountStatementSNA : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                get_tbl_SNAAccountParentMaster(Request.QueryString["Scheme_Id"].ToString());
                Set_SNA_Account_Balances(Request.QueryString["Scheme_Id"].ToString());
                get_Project_Wise_Account_Statement(Request.QueryString["Scheme_Id"].ToString());
            }
        }
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

        lnkActalBalance.Text = AllClasses.convert_To_Indian_No_Format((Convert.ToDecimal(lnkTotalBalance_1.Text) + Convert.ToDecimal(lnkTotalBalance_2.Text) + Convert.ToDecimal(lnkTotalBalance_3.Text)).ToString(), "Decimal");
    }
    private void get_tbl_SNAAccountParentMaster(string Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_SNAAccountParentMaster(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSNAAccount, "SNAAccountParentMaster_ACCT_NO", "SNAAccountParentMaster_Id");
        }
        else
        {
            ddlSNAAccount.Items.Clear();
        }
    }
    private void get_Project_Wise_Account_Statement(string Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Wise_Account_Statement_SNA(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(Cr)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Dr)", "").ToString();
            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdPost.Columns.Count; i++)
            {
                if (grdPost.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic1.Value = jss.Serialize(columnsList);
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
        decimal Credit = 0;
        decimal Debit = 0;
        try
        {
            Credit = Convert.ToDecimal(ds.Tables[1].Rows[0]["Cr"].ToString());
        }
        catch
        {
            Credit = 0;
        }
        try
        {
            Debit = Convert.ToDecimal(ds.Tables[2].Rows[0]["Dr"].ToString());
        }
        catch
        {
            Debit = 0;
        }

        lnkTotalBalanceCredit.Text = AllClasses.convert_To_Indian_No_Format(Credit.ToString(), "Decimal");

        lnkTotalBalanceDebit.Text = AllClasses.convert_To_Indian_No_Format(Debit.ToString(), "Decimal"); 

        lnkTotalBalance.Text = AllClasses.convert_To_Indian_No_Format((Credit - Debit).ToString(), "Decimal");
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[7].Text, "decimal");
            e.Row.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[8].Text, "decimal");

            string PPAVerified = e.Row.Cells[9].Text.Trim();
            if (PPAVerified == "Not Verified")
            {
                e.Row.Cells[9].BackColor = Color.LightPink;
                e.Row.Cells[9].Font.Bold = true;
            }
        }
    }
    protected void grdMultipleFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkDownload") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
        }
    }
    protected void lnkOtherDocs_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[2].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = gr.Cells[3].Text.Trim();

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void btnAddFund_Click(object sender, EventArgs e)
    {
        mp2.Show();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        try
        {
            Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
        }
        catch
        {
            Scheme_Id = "";
        }
        int Parent_Account_Id = 0;
        try
        {
            Parent_Account_Id = Convert.ToInt32(ddlSNAAccount.SelectedValue);
        }
        catch
        {
            Parent_Account_Id = 0;
        }
        decimal Fund = 0;
        try
        {
            Fund = Convert.ToDecimal(txtAmount.Text.Trim());
        }
        catch
        {
            Fund = 0;
        }
        if (txtDate.Text == "")
        {
            MessageBox.Show("Please Input Date");
            mp2.Show();
            return;
        }
        if (Fund > 0)
        {
            if (new DataLayer().insert_tbl_SNAAccountMasterLimit(Fund, Convert.ToInt32(Session["Person_Id"].ToString()), txtDate.Text, txtRefNo.Text.Trim(), Parent_Account_Id))
            {
                MessageBox.Show("Transaction Details Added Successfully");
                get_Project_Wise_Account_Statement(Scheme_Id);
            }
            else
            {
                MessageBox.Show("Error");
                mp2.Show();
            }
        }
        else
        {
            MessageBox.Show("Please Add Fund To Add");
            mp2.Show();
        }
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
}
