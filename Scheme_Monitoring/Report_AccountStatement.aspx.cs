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

public partial class Report_AccountStatement : System.Web.UI.Page
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
                try
                {
                    lblAccountNo.Text = Request.QueryString[1].ToString();
                }
                catch { }
                get_Project_Wise_Account_Statement(Request.QueryString[0].ToString());
                DataSet ds = new DataSet();
                ds = new DataLayer().get_tbl_SNAAccountMaster(Request.QueryString[0].ToString());
                if (AllClasses.CheckDataSet(ds))
                {
                    lnkTotalBalancePNB.Text = new DataLayerSNA().getSNA_IA_Balance(Convert.ToInt32(ds.Tables[0].Rows[0]["SNAAccountMaster_Id"].ToString()));
                }

                decimal ePayment = 0;
                decimal PNB = 0;
                try
                {
                    ePayment = decimal.Parse(lnkTotalBalanceEPayment.Text);
                }
                catch
                {
                    ePayment = 0;
                }
                try
                {
                    PNB = decimal.Parse(lnkTotalBalancePNB.Text);
                }
                catch
                {
                    PNB = 0;
                }

                decimal Cleared = 0;
                try
                {
                    Cleared = decimal.Parse(lnkCleared.Text);
                }
                catch
                {
                    Cleared = 0;
                }
                
                lnkBalanceDiffrence.Text = AllClasses.convert_To_Indian_No_Format((ePayment - PNB - Cleared).ToString(), "decimal");
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }

    private void get_Project_Wise_Account_Statement(string ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Wise_Account_Statement(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataColumn dcAvailable = new DataColumn("Available", typeof(decimal));

            ds.Tables[0].Columns.Add(dcAvailable);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    decimal _Credit = 0;
                    decimal _Debit = 0;
                    try
                    {
                        _Credit = decimal.Parse(ds.Tables[0].Rows[i]["Cr"].ToString());
                    }
                    catch
                    {
                        _Credit = 0;
                    }
                    try
                    {
                        _Debit = decimal.Parse(ds.Tables[0].Rows[i]["Dr"].ToString());
                    }
                    catch
                    {
                        _Debit = 0;
                    }
                    ds.Tables[0].Rows[i]["Available"] = AllClasses.convert_To_Indian_No_Format((_Credit - +_Debit).ToString(), "decimal");
                }
                else
                {
                    decimal _Credit = 0;
                    decimal _Debit = 0;
                    decimal Pre_Total = 0;
                    try
                    {
                        _Credit = decimal.Parse(ds.Tables[0].Rows[i]["Cr"].ToString());
                    }
                    catch
                    {
                        _Credit = 0;
                    }
                    try
                    {
                        _Debit = decimal.Parse(ds.Tables[0].Rows[i]["Dr"].ToString());
                    }
                    catch
                    {
                        _Debit = 0;
                    }
                    try
                    {
                        Pre_Total = decimal.Parse(ds.Tables[0].Rows[i - 1]["Available"].ToString());
                    }
                    catch
                    {
                        Pre_Total = 0;
                    }
                    ds.Tables[0].Rows[i]["Available"] = AllClasses.convert_To_Indian_No_Format((Pre_Total + _Credit - +_Debit).ToString(), "decimal");
                }
            }

            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Cr)", "").ToString(), "decimal");
            grdPost.FooterRow.Cells[9].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Dr)", "").ToString(), "decimal");

            decimal Credit = 0;
            decimal Debit = 0;
            try
            {
                Credit = decimal.Parse(grdPost.FooterRow.Cells[8].Text);
            }
            catch
            {
                Credit = 0;
            }
            try
            {
                Debit = decimal.Parse(grdPost.FooterRow.Cells[9].Text);
            }
            catch
            {
                Debit = 0;
            }
            lnkTotalBalanceEPayment.Text = AllClasses.convert_To_Indian_No_Format((Credit - Debit).ToString(), "decimal");


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

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            grdPPANotLinked.DataSource = ds.Tables[1];
            grdPPANotLinked.DataBind();
            grdPPANotLinked.FooterRow.Cells[2].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[1].Compute("sum(Batch_Amount)", "").ToString(), "decimal");
            decimal ClearedAmtPNB = 0;
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                string CBSTxnId = "";
                string Batch_Status = "";
                CBSTxnId = ds.Tables[1].Rows[i]["CBSTxnId"].ToString().Trim().Replace("&nbsp;", "");
                Batch_Status = ds.Tables[1].Rows[i]["Batch_Status"].ToString().Trim().Replace("&nbsp;", "");
                decimal Amt = 0;
                try
                {
                    Amt = decimal.Parse(ds.Tables[1].Rows[i]["Batch_Amount"].ToString());
                }
                catch
                {
                    Amt = 0;
                }
                if (CBSTxnId != "" && Batch_Status == "Success")
                {
                    ClearedAmtPNB += Amt;
                }
            }

            lnkCleared.Text = AllClasses.convert_To_Indian_No_Format(ClearedAmtPNB.ToString(), "decimal");
        }
        else
        {
            grdPPANotLinked.DataSource = null;
            grdPPANotLinked.DataBind();
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[8].Text, "decimal");
            e.Row.Cells[9].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[9].Text, "decimal");
            e.Row.Cells[10].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[10].Text, "decimal");

            string PPAVerified = e.Row.Cells[9].Text.Trim();
            if (PPAVerified == "Not Verified")
            {
                e.Row.Cells[13].BackColor = Color.LightPink;
                e.Row.Cells[13].Font.Bold = true;
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

    protected void lnkPPANo_Click(object sender, EventArgs e)
    {
        LinkButton lnkPPANo = sender as LinkButton;
        GridViewRow gr = lnkPPANo.Parent.Parent as GridViewRow;

        PPA_Details obj_PPA_Details = new PPA_Details();
        obj_PPA_Details.PPA_Number = lnkPPANo.Text.Trim();
        obj_PPA_Details.PPA_Date = "";
        obj_PPA_Details.Req_Type = "Batch";
        obj_PPA_Details.Entity_Code = "AMRUT";
        List<PNB_GetPPADetails_Res> obj_PNB_GetPPADetails_Res = new DataLayerSNA().GetPPABatchDetails(obj_PPA_Details);
        grdPPABatchDetails.DataSource = obj_PNB_GetPPADetails_Res;
        grdPPABatchDetails.DataBind();

        List<PNB_GetPPATransactionDetails_Res> obj_PNB_GetPPATransactionDetails_Res = new DataLayerSNA().GetPPATransactionDetails(obj_PPA_Details);
        grdPPATransactionDetails.DataSource = obj_PNB_GetPPATransactionDetails_Res;
        grdPPATransactionDetails.DataBind();

        mp2.Show();
    }

    protected void lnkReplace_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int SNALimitUsed_Id = 0;
        try
        {
            SNALimitUsed_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            SNALimitUsed_Id = 0;
        }
        if (SNALimitUsed_Id > 0)
        {
            hf_SNALimitUsed_Id.Value = SNALimitUsed_Id.ToString();
            hf_UsageType.Value = gr.Cells[3].Text.Trim();
            hf_UsageType_Id.Value = gr.Cells[2].Text.Trim();
            mp3.Show();
        }
    }

    protected void btnUploadPPA_Click(object sender, EventArgs e)
    {
        if (txtPPANoNew.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide PPA No");
            return;
        }
        if (!flPPA.HasFile)
        {
            MessageBox.Show("Please Provide PPA Document");
            return;
        }

        if (new DataLayer().Delete_SNALimitUsed(hf_SNALimitUsed_Id.Value, hf_UsageType.Value, Convert.ToInt32(hf_UsageType_Id.Value), txtPPANoNew.Text.Trim(), flPPA.FileBytes, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            MessageBox.Show("Uploaded Successfully");
            get_Project_Wise_Account_Statement(Request.QueryString[0].ToString());
            return;
        }
        else
        {
            MessageBox.Show("Unable To Update");
            mp3.Show();
            return;
        }
    }

    protected void grdPPANotLinked_PreRender(object sender, EventArgs e)
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

    protected void grdPPANotLinked_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[2].Text, "decimal");
        }
    }
}
