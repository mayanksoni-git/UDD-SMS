using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Report_DPR_BPM_Report_CNDS : System.Web.UI.Page
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
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divNodal.Visible = true;
            }
            else
            {
                divNodal.Visible = false;
            }
            get_NodalDepartment();
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

            string Scheme_Id = "";
            int District_Id = 0;
            int ULB_Id = 0;
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            if (Request.QueryString.Count > 0)
            {
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
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                }
                catch
                {
                    Scheme_Id = "";
                }
            }

            get_tbl_ProjectWorkDPR();
        }
    }
    private void get_tbl_FundingPatternNodal(string NodalDept_Id_In)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FundingPatternNodal(NodalDept_Id_In);
        if (AllClasses.CheckDataSet(ds))
        {
            divScheme.Visible = true;
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "FundingPattern_Name", "FundingPattern_Id");
        }
        else
        {
            divScheme.Visible = false;
            ddlScheme.Items.Clear();
        }
    }
    private void get_NodalDepartment()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee("12", 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlNodalDept.DataTextField = "Person_Name";
            ddlNodalDept.DataValueField = "Person_Id";
            ddlNodalDept.DataSource = ds.Tables[0];
            ddlNodalDept.DataBind();
        }
        else
        {
            ddlNodalDept.Items.Clear();
        }
    }
    protected void ddlNodalDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        if (NodalDepartment_Id != "")
        {
            get_tbl_FundingPatternNodal(NodalDepartment_Id);
            int Scheme_Id = 0;
            try
            {
                Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
            }
            catch
            {
                Scheme_Id = 0;
            }
            get_tbl_NodalDeptScheme(NodalDepartment_Id, Scheme_Id);
        }
        else
        {
            ddlNodalDeptScheme.Items.Clear();
        }
    }
    private void get_tbl_NodalDeptScheme(string NodalDept_Id_In, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_NodalDeptScheme(NodalDept_Id_In, Scheme_Id);
        if (ds != null)
        {
            ddlNodalDeptScheme.DataTextField = "NodalDeptScheme_Name";
            ddlNodalDeptScheme.DataValueField = "NodalDeptScheme_Id";
            ddlNodalDeptScheme.DataSource = ds.Tables[0];
            ddlNodalDeptScheme.DataBind();
        }
        else
        {
            ddlNodalDeptScheme.Items.Clear();
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
        get_tbl_ProjectWorkDPR();
    }

    protected void get_tbl_ProjectWorkDPR()
    {
        int Tranche_Id = 0;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;

        string Scheme_Id = "";

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

        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = (new DataLayer()).get_DPR_BID_Process_Status_Summery_CNDS(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id);
        ds1 = (new DataLayer()).get_tbl_ProjectWorkDPR_CNDS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, 0, 0, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TotalDPR, -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            (grdPost.FooterRow.FindControl("lnkDPRF") as LinkButton).Text = ds.Tables[0].Compute("sum(TotalDPR)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkPEDoneF") as LinkButton).Text = ds.Tables[0].Compute("sum(PE_Done)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkDPR_PreparedF") as LinkButton).Text = ds.Tables[0].Compute("sum(DPR_Prepared)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkEFC_PFAD_DoneF") as LinkButton).Text = ds.Tables[0].Compute("sum(EFC_PFAD_Done)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkForm_J_DoneF") as LinkButton).Text = ds.Tables[0].Compute("sum(Form_J_Done)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkDPR_Send_HQ_TSF") as LinkButton).Text = ds.Tables[0].Compute("sum(DPR_Send_HQ_TS)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkTS_Approved_HQF") as LinkButton).Text = ds.Tables[0].Compute("sum(TS_Approved_HQ)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkNIT_IssuedF") as LinkButton).Text = ds.Tables[0].Compute("sum(NIT_Issued)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkTender_UploadedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Tender_Uploaded)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkTechnical_Bid_OpenedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Technical_Bid_Opened)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkFinancial_Bid_OpenedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Financial_Bid_Opened)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkL1_SelectedF") as LinkButton).Text = ds.Tables[0].Compute("sum(L1_Selected)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkLOA_IssuedF") as LinkButton).Text = ds.Tables[0].Compute("sum(LOA_Issued)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkWork_Order_IssuedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Work_Issued)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkCB_signedF") as LinkButton).Text = ds.Tables[0].Compute("sum(CB_Date)", "").ToString();

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

        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        {
            grdFinancialFull.DataSource = ds1.Tables[0];
            grdFinancialFull.DataBind();

            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdFinancialFull.Columns.Count; i++)
            {
                if (grdFinancialFull.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic2.Value = jss.Serialize(columnsList);
        }
        else
        {
            grdFinancialFull.DataSource = null;
            grdFinancialFull.DataBind();
        }
    }

    protected void get_tbl_ProjectWorkDPR(int District_Id, int Zone_Id, int Circle_Id, int Division_Id, int ULB_Id, string Scheme_Id, int Tranche_Id, string NodalDepartment_Id, string NodalDepartmentScheme_Id, int _Scheme_Id, DPR_Status _DPR_Status)
    {
        DataSet ds1 = new DataSet();
        ds1 = (new DataLayer()).get_tbl_ProjectWorkDPR_CNDS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, 0, 0, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, _DPR_Status, -1);
        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        {
            grdFinancialFull.Columns[13].Visible = true;
            grdFinancialFull.Columns[16].Visible = true;
            grdFinancialFull.Columns[17].Visible = true;
            grdFinancialFull.Columns[18].Visible = true;
            grdFinancialFull.Columns[19].Visible = true;
            grdFinancialFull.Columns[21].Visible = true;
            grdFinancialFull.Columns[22].Visible = true;
            grdFinancialFull.Columns[23].Visible = true;
            grdFinancialFull.Columns[24].Visible = true;
            grdFinancialFull.Columns[25].Visible = true;
            grdFinancialFull.Columns[26].Visible = true;
            grdFinancialFull.Columns[27].Visible = true;
            grdFinancialFull.Columns[28].Visible = true;
            grdFinancialFull.Columns[29].Visible = true;
            grdFinancialFull.Columns[30].Visible = true;
            grdFinancialFull.Columns[31].Visible = true;
            grdFinancialFull.Columns[34].Visible = true;
            grdFinancialFull.Columns[35].Visible = true;
            grdFinancialFull.Columns[36].Visible = true;
            grdFinancialFull.Columns[39].Visible = true;
            grdFinancialFull.Columns[40].Visible = true;
            grdFinancialFull.Columns[41].Visible = true;
            grdFinancialFull.Columns[45].Visible = true;
            grdFinancialFull.Columns[46].Visible = true;
            grdFinancialFull.Columns[47].Visible = true;
            grdFinancialFull.Columns[48].Visible = true;
            grdFinancialFull.Columns[49].Visible = true;
            grdFinancialFull.Columns[50].Visible = true;

            grdFinancialFull.DataSource = ds1.Tables[0];
            grdFinancialFull.DataBind();

            grdFinancialFull.Columns[13].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[16].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[17].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[18].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[19].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[21].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[22].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[23].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[24].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[25].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[26].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[27].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[28].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[29].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[30].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[31].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[34].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[35].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[36].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[39].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[40].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[41].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[45].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[46].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[47].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[48].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[49].Visible = chkShowDetailedReport.Checked;
            grdFinancialFull.Columns[50].Visible = chkShowDetailedReport.Checked;

            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdFinancialFull.Columns.Count; i++)
            {
                if (grdFinancialFull.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic2.Value = jss.Serialize(columnsList);
        }
        else
        {
            grdFinancialFull.DataSource = null;
            grdFinancialFull.DataBind();
        }
    }
    protected void chkShowDetailedReport_CheckedChanged(object sender, EventArgs e)
    {
        get_tbl_ProjectWorkDPR();
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
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

    protected void grdFinancialFull_PreRender(object sender, EventArgs e)
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

    protected void lnkDPR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TotalDPR);
    }

    protected void lnkDPRF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TotalDPR);
    }

    protected void grdFinancialFull_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }
      
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectDPR_Id = 0;

        try
        {
            ProjectDPR_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectDPR_Id = 0;
        }
        Response.Redirect("MasterProjectDPR_CNDS.aspx?ProjectDPR_Id=" + ProjectDPR_Id.ToString());
    }

    protected void lnkPEDone_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.PE_Done);
    }

    protected void lnkPEDoneF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.PE_Done);
    }

    protected void lnkDPR_Prepared_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Prepared);
    }

    protected void lnkDPR_PreparedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Prepared);
    }

    protected void lnkEFC_PFAD_Done_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.EFC_PFAD_Done);
    }

    protected void lnkEFC_PFAD_DoneF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.EFC_PFAD_Done);
    }

    protected void lnkForm_J_Done_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Form_J_Done);
    }

    protected void lnkForm_J_DoneF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Form_J_Done);
    }

    protected void lnkDPR_Send_HQ_TSF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Send_HQ_TS);
    }

    protected void lnkTS_Approved_HQ_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TS_Approved_HQ);
    }

    protected void lnkTS_Approved_HQF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TS_Approved_HQ);
    }

    protected void lnkNIT_IssuedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.NIT_Issued);
    }

    protected void lnkTender_Uploaded_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Tender_Uploaded);
    }

    protected void lnkTender_UploadedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Tender_Uploaded);
    }

    protected void lnkTechnical_Bid_Opened_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Technical_Bid_Opened);
    }

    protected void lnkTechnical_Bid_OpenedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Technical_Bid_Opened);
    }

    protected void lnkFinancial_Bid_Opened_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Financial_Bid_Opened);
    }

    protected void lnkFinancial_Bid_OpenedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Financial_Bid_Opened);
    }

    protected void lnkL1_Selected_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.L1_Selected);
    }

    protected void lnkL1_SelectedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.L1_Selected);
    }

    protected void lnkLOA_Issued_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.LOA_Issued);
    }

    protected void lnkLOA_IssuedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.LOA_Issued);
    }

    protected void lnkWork_Order_Issued_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Work_Issued);
    }

    protected void lnkWork_Order_IssuedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Work_Issued);
    }

    protected void lnkCB_Signed_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.CB_Date);
    }

    protected void lnkCB_signedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

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
        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.CB_Date);
    }

    protected void lnkDPR_Send_HQ_TS_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Send_HQ_TS);
    }

    protected void lnkNIT_Issued_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
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

        int Tranche_Id = 0;
        string NodalDepartment_Id = "";
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartment_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartment_Id))
        {
            NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
        }
        string NodalDepartmentScheme_Id = "";

        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            if (listItem.Selected)
            {
                NodalDepartmentScheme_Id += listItem.Value + ", ";
            }
        }
        if (!string.IsNullOrEmpty(NodalDepartmentScheme_Id))
        {
            NodalDepartmentScheme_Id = NodalDepartmentScheme_Id.Trim().Substring(0, NodalDepartmentScheme_Id.Trim().Length - 1);
        }
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.NIT_Issued);
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            string NodalDepartment_Id = "";
            foreach (ListItem listItem in ddlNodalDept.Items)
            {
                if (listItem.Selected)
                {
                    NodalDepartment_Id += listItem.Value + ", ";
                }
            }
            if (!string.IsNullOrEmpty(NodalDepartment_Id))
            {
                NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
            }
            int Scheme_Id = 0;
            if (NodalDepartment_Id != "")
            {
                get_tbl_NodalDeptScheme(NodalDepartment_Id, Scheme_Id);
            }
            else
            {
                ddlNodalDeptScheme.Items.Clear();
            }
        }
        else
        {
            string NodalDepartment_Id = "";
            foreach (ListItem listItem in ddlNodalDept.Items)
            {
                if (listItem.Selected)
                {
                    NodalDepartment_Id += listItem.Value + ", ";
                }
            }
            if (!string.IsNullOrEmpty(NodalDepartment_Id))
            {
                NodalDepartment_Id = NodalDepartment_Id.Trim().Substring(0, NodalDepartment_Id.Trim().Length - 1);
            }
            int Scheme_Id = 0;
            try
            {
                Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
            }
            catch
            {
                Scheme_Id = 0;
            }
            if (NodalDepartment_Id != "")
            {
                get_tbl_NodalDeptScheme(NodalDepartment_Id, Scheme_Id);
            }
            else
            {
                ddlNodalDeptScheme.Items.Clear();
            }
        }
    }
}