using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSNAChildAccount : System.Web.UI.Page
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
                string Scheme_Id = "";
                int District_Id = 0;
                int ULB_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                string DisplayMode = "";

                try
                {
                    try
                    {
                        Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                    }
                    catch
                    {
                        Scheme_Id = "";
                    }
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        listItem.Selected = false;
                    }
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        if (Scheme_Id == listItem.Value)
                        {
                            listItem.Selected = true;
                            break;
                        }
                    }
                }
                catch
                {
                    Scheme_Id = "";
                }

                try
                {
                    District_Id = Convert.ToInt32(Request.QueryString["District_Id"].ToString());
                }
                catch
                {
                    District_Id = 0;
                }
                try
                {
                    ULB_Id = Convert.ToInt32(Request.QueryString["ULB_Id"].ToString());
                }
                catch
                {
                    ULB_Id = 0;
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

                try
                {
                    DisplayMode = Request.QueryString["Display"].ToString();
                    rbtDisplayType.SelectedValue = DisplayMode;
                }
                catch
                {
                    DisplayMode = "";
                }
            }

            get_tbl_ProjectWork();
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
        get_tbl_ProjectWork();
    }

    protected void get_tbl_ProjectWork()
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
        DataSet ds = new DataSet();
        int DisplayType = 0;
        if (rbtDisplayType.SelectedValue == "1")
        {
            DisplayType = 1;
        }
        else if (rbtDisplayType.SelectedValue == "2")
        {
            DisplayType = 2;
        }
        else if (rbtDisplayType.SelectedValue == "3")
        {
            DisplayType = 3;
        }
        else
        {
            DisplayType = 0;
        }
        ds = (new DataLayer()).get_tbl_SNAAccountMaster(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, 0, DisplayType);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["SNA_ChildAccount"] = ds;
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            if (chkShowBreakup.Checked)
            {
                grdPost.Columns[20].HeaderStyle.CssClass= "displayStyle";
                grdPost.Columns[20].ItemStyle.CssClass= "displayStyle";
                grdPost.Columns[20].FooterStyle.CssClass= "displayStyle";

                grdPost.Columns[21].HeaderStyle.CssClass = "";
                grdPost.Columns[21].ItemStyle.CssClass = "";
                grdPost.Columns[21].FooterStyle.CssClass = "";

                grdPost.Columns[22].HeaderStyle.CssClass = "";
                grdPost.Columns[22].ItemStyle.CssClass = "";
                grdPost.Columns[22].FooterStyle.CssClass = "";

                grdPost.Columns[23].HeaderStyle.CssClass = "";
                grdPost.Columns[23].ItemStyle.CssClass = "";
                grdPost.Columns[23].FooterStyle.CssClass = "";

                grdPost.Columns[24].HeaderStyle.CssClass = "";
                grdPost.Columns[24].ItemStyle.CssClass = "";
                grdPost.Columns[24].FooterStyle.CssClass = "";
            }
            else
            {
                grdPost.Columns[20].HeaderStyle.CssClass = "";
                grdPost.Columns[20].ItemStyle.CssClass = "";
                grdPost.Columns[20].FooterStyle.CssClass = "";

                grdPost.Columns[21].HeaderStyle.CssClass = "displayStyle";
                grdPost.Columns[21].ItemStyle.CssClass = "displayStyle";
                grdPost.Columns[21].FooterStyle.CssClass = "displayStyle";

                grdPost.Columns[22].HeaderStyle.CssClass = "displayStyle";
                grdPost.Columns[22].ItemStyle.CssClass = "displayStyle";
                grdPost.Columns[22].FooterStyle.CssClass = "displayStyle";

                grdPost.Columns[23].HeaderStyle.CssClass = "displayStyle";
                grdPost.Columns[23].ItemStyle.CssClass = "displayStyle";
                grdPost.Columns[23].FooterStyle.CssClass = "displayStyle";

                grdPost.Columns[24].HeaderStyle.CssClass = "displayStyle";
                grdPost.Columns[24].ItemStyle.CssClass = "displayStyle";
                grdPost.Columns[24].FooterStyle.CssClass = "displayStyle";
            }
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

            grdPost.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(SNAAccountLimit_AssignedLimit)", "").ToString();
            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(SNAAccountLimitUsed_UsedLimit)", "").ToString();
            grdPost.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(SNAAccountAvailableLimit)", "").ToString();
            grdPost.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(SNAAccountPipelineLimit)", "").ToString();
            grdPost.FooterRow.Cells[21].Text = ds.Tables[0].Compute("sum(SNAAccountPipelineLimitInvoice)", "").ToString();
            grdPost.FooterRow.Cells[22].Text = ds.Tables[0].Compute("sum(SNAAccountPipelineLimitDR)", "").ToString();
            grdPost.FooterRow.Cells[23].Text = ds.Tables[0].Compute("sum(SNAAccountPipelineLimitADP)", "").ToString();
            grdPost.FooterRow.Cells[24].Text = ds.Tables[0].Compute("sum(SNAAccountPipelineLimitMA)", "").ToString();
            grdPost.FooterRow.Cells[25].Text = ds.Tables[0].Compute("sum(SNAAccountMaster_Balance)", "").ToString();

            sp_Note.InnerHtml = "Balance As Per Bank (PNB) is Last updated On: " + ds.Tables[0].Rows[0]["SNAAccountMaster_BalanceAsOn"].ToString();
        }
        else
        {
            ViewState["SNA_ChildAccount"] = null;
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
        if (SNAAccountMaster_Id > 0)
        {
            lnkBalance.Text = new DataLayerSNA().getSNA_Total_Balance(SNAAccountMaster_Id);
        }
        else
        {
            MessageBox.Show("Unable To Retrive Balance");
        }

        //LinkButton lnkBalance = (sender as LinkButton);
        //GridViewRow gr = lnkBalance.Parent.Parent as GridViewRow;
        //int SNAAccountMaster_Id = 0;
        //try
        //{
        //    SNAAccountMaster_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        //}
        //catch
        //{
        //    SNAAccountMaster_Id = 0;
        //}
        ////int SNAAccountLimit_Id = 0;
        ////try
        ////{
        ////    SNAAccountLimit_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        ////}
        ////catch
        ////{
        ////    SNAAccountLimit_Id = 0;
        ////}
        //int AssignedLimit = 0;
        //try
        //{
        //    AssignedLimit = Convert.ToInt32(Convert.ToDecimal(gr.Cells[19].Text.Trim().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0]) * 100000);
        //}
        //catch
        //{
        //    AssignedLimit = 0;
        //}
        //string Account_No = gr.Cells[14].Text.Trim();
        //if (SNAAccountMaster_Id > 0 && AssignedLimit > 0 && Account_No != "")
        //{
        //    DP_Limit obj_DP_Limit = new DP_Limit();
        //    obj_DP_Limit.AssignedLimit = AssignedLimit;
        //    obj_DP_Limit.SNAAccountLimit_Id = 0;
        //    obj_DP_Limit.SNAAccountMaster_ACCT_NO = Account_No;
        //    obj_DP_Limit.SNAAccountMaster_Id = SNAAccountMaster_Id;
        //    string Msg = "";
        //    if (new DataLayerSNA().setSNA_DP_Limit(obj_DP_Limit, ref Msg))
        //    {
        //        //if (new DataLayer().Update_DP_Limit_Status(obj_DP_Limit.SNAAccountLimit_Id))
        //        //{
        //        //    MessageBox.Show("Set Limit Successfull.");
        //        //}
        //        MessageBox.Show("Set Limit Successfull.");
        //    }
        //    else
        //    {
        //        if (Msg != "")
        //        {
        //            MessageBox.Show(Msg);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Unable To Set Limit");
        //        }
        //    }
        //}
        //else
        //{
        //    MessageBox.Show("Unable To Set Limit");
        //}
    }

    protected void lnkInvoiceCount_Click(object sender, EventArgs e)
    {

    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        if (rbtDisplayType.SelectedValue == "1" || rbtDisplayType.SelectedValue == "2")
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
            DataSet ds = new DataSet();
            
            int DisplayType = 0;
            if (rbtDisplayType.SelectedValue == "1")
            {
                DisplayType = 1;
            }
            else if (rbtDisplayType.SelectedValue == "2")
            {
                DisplayType = 2;
            }
            else if (rbtDisplayType.SelectedValue == "3")
            {
                DisplayType = 3;
            }
            else
            {
                DisplayType = 0;
            }
            ds = (new DataLayer()).get_tbl_SNAAccountMaster_SummeryReport(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, 0, DisplayType);
            if (AllClasses.CheckDataSet(ds))
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "SNA_ChildAccount_Summery.pdf";

                List<tbl_SNA_ChildAccount> obj_tbl_SNA_ChildAccount_Li = new List<tbl_SNA_ChildAccount>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tbl_SNA_ChildAccount obj_tbl_SNA_ChildAccount = new tbl_SNA_ChildAccount();
                    obj_tbl_SNA_ChildAccount.Assigned_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimit_AssignedLimit"].ToString());
                    obj_tbl_SNA_ChildAccount.Available_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountAvailableLimit"].ToString());
                    obj_tbl_SNA_ChildAccount.Available_Limit_PNB = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountMaster_Balance"].ToString());
                    obj_tbl_SNA_ChildAccount.Pipeline_Invoice = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitInvoice"].ToString());
                    obj_tbl_SNA_ChildAccount.Pipeline_ADP = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitADP"].ToString());
                    obj_tbl_SNA_ChildAccount.Pipeline_DR = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitDR"].ToString());
                    obj_tbl_SNA_ChildAccount.Pipeline_MA = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitMA"].ToString());
                    obj_tbl_SNA_ChildAccount.Pipeline_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimit"].ToString());
                    obj_tbl_SNA_ChildAccount.Total_Invoices = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Invoice_Count"].ToString());
                    obj_tbl_SNA_ChildAccount.Used_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimitUsed_UsedLimit"].ToString());
                    obj_tbl_SNA_ChildAccount.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                    obj_tbl_SNA_ChildAccount.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_SNA_ChildAccount.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_SNA_ChildAccount.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_SNA_ChildAccount.Zone_Circle_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                    if (rbtDisplayType.SelectedValue == "1")
                    {
                        obj_tbl_SNA_ChildAccount.Header_Text = "Invoices pending where limit is available and payment can be done - : " + ddlScheme.SelectedItem.Text;
                    }
                    else
                    {
                        obj_tbl_SNA_ChildAccount.Header_Text = "Payment is Pending Due To Unavailability Of SNA Limit - : " + ddlScheme.SelectedItem.Text;
                    }
                    obj_tbl_SNA_ChildAccount_Li.Add(obj_tbl_SNA_ChildAccount);
                }

                string webURI = "";
                if (Page.Request.Url.Query.Trim() == "")
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
                }
                else
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
                }

                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/SNA_Child_Account_Summery.rpt"));
                crystalReport.SetDataSource(obj_tbl_SNA_ChildAccount_Li);
                crystalReport.Refresh();
                //crystalReport.ReportSource = crystalReport;
                //crystalReport.RefreshReport();
                crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                if (fi.Exists)
                {
                    new AllClasses().Render_PDF_Document(ltEmbed, filePath + fileName);
                    mp1.Show();
                }
                else
                {
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
        }
        else
        {
            MessageBox.Show("Please Select A Option From Radio Button");
            return;
        }
    }

    protected void btnExport2_Click(object sender, EventArgs e)
    {
        if (rbtDisplayType.SelectedValue == "1" || rbtDisplayType.SelectedValue == "2")
        {
            DataSet ds = new DataSet();
            if (ViewState["SNA_ChildAccount"] != null)
            {
                ds = (DataSet)ViewState["SNA_ChildAccount"];
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                    if (!Directory.Exists(Server.MapPath(".") + filePath))
                    {
                        Directory.CreateDirectory(Server.MapPath(".") + filePath);
                    }

                    string fileName = "SNA_ChildAccount_Detail.pdf";

                    List<tbl_SNA_ChildAccount_Detail> obj_tbl_SNA_ChildAccount_Detail_Li = new List<tbl_SNA_ChildAccount_Detail>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_SNA_ChildAccount_Detail obj_tbl_SNA_ChildAccount_Detail = new tbl_SNA_ChildAccount_Detail();
                        obj_tbl_SNA_ChildAccount_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                        obj_tbl_SNA_ChildAccount_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                        obj_tbl_SNA_ChildAccount_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                        obj_tbl_SNA_ChildAccount_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                        obj_tbl_SNA_ChildAccount_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                        obj_tbl_SNA_ChildAccount_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                        obj_tbl_SNA_ChildAccount_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                        obj_tbl_SNA_ChildAccount_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                        obj_tbl_SNA_ChildAccount_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                        if (rbtDisplayType.SelectedValue == "1")
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Header_Text = "Invoices pending where limit is available and payment can be done - : " + ddlScheme.SelectedItem.Text;
                        }
                        else
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Header_Text = "Payment is Pending Due To Unavailability Of SNA Limit - : " + ddlScheme.SelectedItem.Text;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Assigned_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimit_AssignedLimit"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Assigned_Limit = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Available_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountAvailableLimit"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Available_Limit = 0;
                        }
                        obj_tbl_SNA_ChildAccount_Detail.Oldest_Invoice_Date = ds.Tables[0].Rows[i]["Min_Date"].ToString();
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Available_Limit_PNB = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountMaster_Balance"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Available_Limit_PNB = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Days_Since_Last_Limit_Assigned = Convert.ToInt32(ds.Tables[0].Rows[i]["Last_Assigned_Day_Diff"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Days_Since_Last_Limit_Assigned = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pendency_Days = Convert.ToInt32(ds.Tables[0].Rows[i]["Max_Pendency"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pendency_Days = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_ADP = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitADP"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_ADP = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_DR = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitDR"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_DR = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_Invoice = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitInvoice"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_Invoice = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_MA = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitMA"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_MA = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimit"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Pipeline_Total = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Total_Invoices = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Invoice_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Total_Invoices = 0;
                        }
                        try
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Used_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimitUsed_UsedLimit"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SNA_ChildAccount_Detail.Used_Limit = 0;
                        }
                        obj_tbl_SNA_ChildAccount_Detail_Li.Add(obj_tbl_SNA_ChildAccount_Detail);
                    }

                    string webURI = "";
                    if (Page.Request.Url.Query.Trim() == "")
                    {
                        webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
                    }
                    else
                    {
                        webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
                    }

                    ReportDocument crystalReport = new ReportDocument();
                    crystalReport.Load(Server.MapPath("~/Crystal/pmis/SNA_Child_Account_Detail.rpt"));
                    crystalReport.SetDataSource(obj_tbl_SNA_ChildAccount_Detail_Li);
                    crystalReport.Refresh();
                    //crystalReport.ReportSource = crystalReport;
                    //crystalReport.RefreshReport();
                    crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                    FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                    if (fi.Exists)
                    {
                        new AllClasses().Render_PDF_Document(ltEmbed, filePath + fileName);
                        mp1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Unable To Generate Report.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
        }
        else
        {
            MessageBox.Show("Please Select A Option From Radio Button");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }
}