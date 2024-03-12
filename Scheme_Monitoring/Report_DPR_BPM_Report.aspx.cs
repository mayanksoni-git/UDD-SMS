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

public partial class Report_DPR_BPM_Report : System.Web.UI.Page
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
            get_tbl_TrancheType();
            get_tbl_Project();
            get_M_Jurisdiction();
            get_tbl_Zone();

            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    ddlDistrict.Enabled = false;
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
                    District_Id = Convert.ToInt32(Request.QueryString["District_Id"].ToString());
                    ddlDistrict.SelectedValue = District_Id.ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                }
                catch
                {
                    District_Id = 0;
                }
                try
                {
                    ULB_Id = Convert.ToInt32(Request.QueryString["ULB_Id"].ToString());
                    ddlULB.SelectedValue = ULB_Id.ToString();
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
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        if (listItem.Value == Scheme_Id)
                        {
                            listItem.Selected = true;
                        }
                    }
                }
                catch
                {
                    Scheme_Id = "";
                }
            }

            get_tbl_ProjectWorkDPR();
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
            AllClasses.FillDropDown(ds.Tables[0], ddlNodalDepartment, "Person_Name", "Person_Id");
        }
        else
        {
            ddlNodalDepartment.Items.Clear();
        }
    }
    private void get_tbl_TrancheType()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_TrancheType();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlTranche, "TrancheType_Name", "TrancheType_Id");
        }
        else
        {
            ddlTranche.Items.Clear();
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
        int NodalDepartment_Id = 0;
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
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
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
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = (new DataLayer()).get_DPR_BID_Process_Status_Summery(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, Tranche_Id, NodalDepartment_Id.ToString(), "");
        ds1 = (new DataLayer()).get_tbl_ProjectWorkDPR_BPM(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, 0, 0, -1, Tranche_Id, NodalDepartment_Id.ToString(), "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            (grdPost.FooterRow.FindControl("lnkDPRF") as LinkButton).Text = ds.Tables[0].Compute("sum(TotalDPR)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkNotStartedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Process_Not_Started)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkEFC_PFADF") as LinkButton).Text = ds.Tables[0].Compute("sum(EFC_PFAD)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkGO_IssuedF") as LinkButton).Text = ds.Tables[0].Compute("sum(GO_Issued)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkNIT_IssuedF") as LinkButton).Text = ds.Tables[0].Compute("sum(NIT_Issued)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkNIT_Issued2F") as LinkButton).Text = ds.Tables[0].Compute("sum(NIT_Issued2)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkTender_PublishedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Tender_Published)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkPre_Bid_MeetingF") as LinkButton).Text = ds.Tables[0].Compute("sum(Pre_Bid_Meeting)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkTechnical_Bid_OpenedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Technical_Bid_Opened)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkBidders_Evaluation_TechnicalF") as LinkButton).Text = ds.Tables[0].Compute("sum(Bidders_Evaluation_Technical)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkFinancial_Bid_OpenedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Financial_Bid_Opened)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkSend_To_SMD_For_ApprovalF") as LinkButton).Text = ds.Tables[0].Compute("sum(Send_To_SMD_For_Approval)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkSLTC_Meeting_After_Tender_ApprovalF") as LinkButton).Text = ds.Tables[0].Compute("sum(SLTC_Meeting_After_Tender_Approval)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkWork_Order_IssuedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Work_Order_Issued)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkAgreement_With_BidderF") as LinkButton).Text = ds.Tables[0].Compute("sum(Agreement_With_Bidder)", "").ToString();

            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                grdPost.Columns[9].HeaderText = "Technical Sanction";
                grdPost.Columns[10].HeaderText = "NIT Issued";
                grdPost.Columns[10].Visible = true;
                grdPost.Columns[16].Visible = false;
            }
            else
            {
                grdPost.Columns[9].HeaderText = "NIT Issued";
                grdPost.Columns[10].HeaderText = "";
                grdPost.Columns[10].Visible = false;
                grdPost.Columns[16].Visible = true;
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

    protected void get_tbl_ProjectWorkDPR(int District_Id, int Zone_Id, int Circle_Id, int Division_Id, int ULB_Id, string Scheme_Id, int Step_Id, int Tranche_Id, string NodalDepartment_Id, string NodalDepartmentScheme_Id)
    {
        DataSet ds1 = new DataSet();
        ds1 = (new DataLayer()).get_tbl_ProjectWorkDPR_BPM(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, 0, 0, Step_Id, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id);
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
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                e.Row.Cells[9].Text = "Technical Sanction";
                e.Row.Cells[10].Text = "NIT Issued";
            }
            else
            {
                e.Row.Cells[9].Text = "NIT Issued";
                e.Row.Cells[10].Text = "";
            }
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
    protected void rbtMappingWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtMappingWith.SelectedValue == "D")
        {
            divZone.Visible = true;
            divCircle.Visible = true;
            divDivision.Visible = true;

            divDistrict.Visible = false;
            divULB.Visible = false;
        }
        else
        {
            divZone.Visible = false;
            divCircle.Visible = false;
            divDivision.Visible = false;

            divDistrict.Visible = true;
            divULB.Visible = true;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, -1, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkDPRF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, -1, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void grdFinancialFull_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }


    protected void lnkNotStarted_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkNotStartedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkEFC_PFAD_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 1, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkEFC_PFADF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 1, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkGO_Issued_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 2, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkGO_IssuedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 2, Tranche_Id, NodalDepartment_Id.ToString(), "");
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 3, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkNIT_IssuedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 3, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkTender_Published_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 5, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkTender_PublishedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 5, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkPre_Bid_Meeting_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 6, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkPre_Bid_MeetingF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 6, Tranche_Id, NodalDepartment_Id.ToString(), "");
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 8, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkTechnical_Bid_OpenedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 8, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkBidders_Evaluation_Technical_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 9, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkBidders_Evaluation_TechnicalF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 9, Tranche_Id, NodalDepartment_Id.ToString(), "");
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 10, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkFinancial_Bid_OpenedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 10, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkSend_To_SMD_For_Approval_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 11, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkSend_To_SMD_For_ApprovalF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 11, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkSLTC_Meeting_After_Tender_Approval_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 12, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkSLTC_Meeting_After_Tender_ApprovalF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 12, Tranche_Id, NodalDepartment_Id.ToString(), "");
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 13, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkWork_Order_IssuedF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 13, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkAgreement_With_Bidder_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 14, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkAgreement_With_BidderF_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 14, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkEFC_PFAD_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(1, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkGO_Issued_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(2, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkNIT_Issued_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(3, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkTender_Published_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(5, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkPre_Bid_Meeting_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(6, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkTechnical_Bid_Opened_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(8, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkBidders_Evaluation_Technical_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(9, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkFinancial_Bid_Opened_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(10, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkSend_To_SMD_For_Approval_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(11, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkSLTC_Meeting_After_Tender_Approval_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(12, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkWork_Order_Issued_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(13, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    protected void lnkAgreement_With_Bidder_D_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        int ProjectDPRTender_Id = 0;
        try
        {
            ProjectDPRTender_Id = Convert.ToInt32((sender as LinkButton).ToolTip.Trim());
        }
        catch
        {
            ProjectDPRTender_Id = 0;
        }
        Open_Details(14, Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRTender_Id);
    }

    private void Open_Details(int Step_Status, int ProjectDPR_Id, int ProjectDPRTender_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectDPRTender(ProjectDPR_Id, ProjectDPRTender_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdEFC_PFAD.DataSource = ds.Tables[0];
            grdEFC_PFAD.DataBind();

            grdGODetails.DataSource = ds.Tables[0];
            grdGODetails.DataBind();

            grdCommonStep.DataSource = ds.Tables[0];
            grdCommonStep.DataBind();

            grdTenderPublished.DataSource = ds.Tables[0];
            grdTenderPublished.DataBind();
        }
        else
        {
            grdEFC_PFAD.DataSource = null;
            grdEFC_PFAD.DataBind();

            grdGODetails.DataSource = null;
            grdGODetails.DataBind();

            grdCommonStep.DataSource = null;
            grdCommonStep.DataBind();

            grdTenderPublished.DataSource = null;
            grdTenderPublished.DataBind();
        }

        ds = (new DataLayer()).get_tbl_ProjectDPRBidder(ProjectDPR_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdBidder.DataSource = ds.Tables[0];
            grdBidder.DataBind();
        }
        else
        {
            grdBidder.DataSource = null;
            grdBidder.DataBind();
        }

        ds = (new DataLayer()).get_tbl_ProjectDPRPQC(ProjectDPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdQualificationCriteria.DataSource = ds.Tables[0];
            grdQualificationCriteria.DataBind();
        }
        else
        {
            grdQualificationCriteria.DataSource = null;
            grdQualificationCriteria.DataBind();
        }

        ds = (new DataLayer()).get_tbl_ProjectDPRBidResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value));
        if (AllClasses.CheckDataSet(ds))
        {
            grdPBMResponseDoc.DataSource = ds.Tables[0];
            grdPBMResponseDoc.DataBind();
        }
        else
        {
            grdPBMResponseDoc.DataSource = null;
            grdPBMResponseDoc.DataBind();
        }

        div_Report_Step1.Visible = false;
        div_Report_Step2.Visible = false;
        div_Report_Step5.Visible = false;
        div_Report_Step_Common.Visible = false;
        divBidder.Visible = false;
        if (Step_Status == 1)
        {
            div_Report_Step1.Visible = true;
        }
        if (Step_Status == 2)
        {
            div_Report_Step2.Visible = true;
        }
        if (Step_Status == 3)
        {
            div_Report_Step_Common.Visible = true;
            if (grdCommonStep.Rows.Count > 0)
                grdCommonStep.Columns[3].HeaderText = "NIT Issue Date";
        }
        if (Step_Status == 5)
        {
            div_Report_Step5.Visible = true;
        }
        if (Step_Status == 6)
        {
            div_Report_Step5.Visible = true;
            if (grdCommonStep.Rows.Count > 0)
            {
                grdTenderPublished.Columns[4].HeaderText = "Pre Bid Meeting On Date";
                grdTenderPublished.Columns[5].HeaderText = "Corrigendum Refrence No";
                grdTenderPublished.Columns[6].HeaderText = "Revised Closing / End Date";
                grdTenderPublished.Columns[7].HeaderText = "Revised Technical Bid Opening Date";
                grdTenderPublished.Columns[9].HeaderText = "Corrigendum Document";
                grdTenderPublished.Columns[9].AccessibleHeaderText = "Corrigendum Document";
                grdTenderPublished.Columns[8].Visible = false;
                grdTenderPublished.Columns[10].Visible = false;
            }
        }
        if (Step_Status == 8)
        {
            divBidder.Visible = true;
        }
        if (Step_Status == 9)
        {
            divBidder.Visible = true;
        }
        if (Step_Status == 10)
        {
            divBidder.Visible = true;
        }
        if (Step_Status == 11)
        {
            div_Report_Step_Common.Visible = true;
        }
        if (Step_Status == 12)
        {
            div_Report_Step_Common.Visible = true;
        }
        if (Step_Status == 13)
        {
            div_Report_Step_Common.Visible = true;
        }
        if (Step_Status == 14)
        {
            div_Report_Step_Common.Visible = true;
        }
        mpBidder.Show();
    }

    protected void grdQualificationResponse_PreRender(object sender, EventArgs e)
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

    protected void grdQualificationResponse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            try
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = Convert.ToInt32(e.Row.Cells[10].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Enable_Verification_Document = 0;
            }

            int ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            try
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = Convert.ToInt32(e.Row.Cells[11].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Upload_Document_Count = 0;
            }

            int ProjectDPRPQC_PQC_Auto_Calculated = 0;
            try
            {
                ProjectDPRPQC_PQC_Auto_Calculated = Convert.ToInt32(e.Row.Cells[12].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Auto_Calculated = 0;
            }

            int ProjectDPRPQC_PQC_Id = 0;
            try
            {
                ProjectDPRPQC_PQC_Id = Convert.ToInt32(e.Row.Cells[13].Text.Trim());
            }
            catch
            {
                ProjectDPRPQC_PQC_Id = 0;
            }

            CheckBox chkVerified1 = e.Row.FindControl("chkVerified1") as CheckBox;
            CheckBox chkVerified2 = e.Row.FindControl("chkVerified2") as CheckBox;
            CheckBox chkVerified3 = e.Row.FindControl("chkVerified3") as CheckBox;

            LinkButton lnkDownload1 = e.Row.FindControl("lnkDownload1") as LinkButton;
            LinkButton lnkDownload2 = e.Row.FindControl("lnkDownload2") as LinkButton;
            LinkButton lnkDownload3 = e.Row.FindControl("lnkDownload3") as LinkButton;

            LinkButton lnkDownloadV1 = e.Row.FindControl("lnkDownloadV1") as LinkButton;
            LinkButton lnkDownloadV2 = e.Row.FindControl("lnkDownloadV2") as LinkButton;
            LinkButton lnkDownloadV3 = e.Row.FindControl("lnkDownloadV3") as LinkButton;

            lnkDownload1.Visible = false;
            lnkDownload2.Visible = false;
            lnkDownload3.Visible = false;

            lnkDownloadV1.Visible = false;
            lnkDownloadV2.Visible = false;
            lnkDownloadV3.Visible = false;

            chkVerified1.Visible = false;
            chkVerified2.Visible = false;
            chkVerified3.Visible = false;

            bool Hide_Tbl_Response = false;
            if (ProjectDPRPQC_PQC_Upload_Document_Count == 0)
            {
                lnkDownload1.Visible = false;
                lnkDownload2.Visible = false;
                lnkDownload3.Visible = false;

                lnkDownloadV1.Visible = false;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;

                Hide_Tbl_Response = true;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 1)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = false;
                lnkDownload3.Visible = false;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 2)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = false;

                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = false;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Upload_Document_Count == 3)
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = true;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = true;
                lnkDownloadV3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }
            else
            {
                lnkDownload1.Visible = true;
                lnkDownload2.Visible = true;
                lnkDownload3.Visible = true;

                lnkDownloadV1.Visible = true;
                lnkDownloadV2.Visible = true;
                lnkDownloadV3.Visible = true;

                chkVerified1.Visible = true;
                chkVerified2.Visible = true;
                chkVerified3.Visible = true;
            }

            if (ProjectDPRPQC_PQC_Enable_Verification_Document == 0)
            {
                lnkDownloadV1.Visible = false;
                lnkDownloadV2.Visible = false;
                lnkDownloadV3.Visible = false;

                chkVerified1.Visible = false;
                chkVerified2.Visible = false;
                chkVerified3.Visible = false;
            }

            int FileVerified1 = 0;
            int FileVerified2 = 0;
            int FileVerified3 = 0;
            try
            {
                FileVerified1 = Convert.ToInt32(e.Row.Cells[7].Text.Trim());
            }
            catch
            {
                FileVerified1 = 0;
            }
            try
            {
                FileVerified2 = Convert.ToInt32(e.Row.Cells[8].Text.Trim());
            }
            catch
            {
                FileVerified2 = 0;
            }
            try
            {
                FileVerified3 = Convert.ToInt32(e.Row.Cells[9].Text.Trim());
            }
            catch
            {
                FileVerified3 = 0;
            }
            if (FileVerified1 == 1)
            {
                chkVerified1.Checked = true;
            }
            else
            {
                chkVerified1.Checked = false;
            }
            if (FileVerified2 == 1)
            {
                chkVerified2.Checked = true;
            }
            else
            {
                chkVerified2.Checked = false;
            }
            if (FileVerified3 == 1)
            {
                chkVerified3.Checked = true;
            }
            else
            {
                chkVerified3.Checked = false;
            }

            HtmlTable tblAdditionalData = e.Row.FindControl("tblAdditionalData") as HtmlTable;
            HtmlTable tbl_Response = e.Row.FindControl("tbl_Response") as HtmlTable;
            if (Hide_Tbl_Response)
            {
                tbl_Response.Visible = false;
            }

            if (ProjectDPRPQC_PQC_Id == 6)
            {//Bid Capacity
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 2)
            {//EMD
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 1)
            {//Tender Fees
                tblAdditionalData.Visible = true;
            }
            else if (ProjectDPRPQC_PQC_Id == 3)
            {//Exp 60%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 10)
            {//Exp 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 11)
            {//Exp 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 9)
            {//Solv 40%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 4)
            {//TurnOver 30%
                tblAdditionalData.Visible = false;
            }
            else if (ProjectDPRPQC_PQC_Id == 5)
            {//Net Worth
                tblAdditionalData.Visible = true;
            }
            else
            {
                tblAdditionalData.Visible = false;
            }
        }
    }

    protected void btnOpenQualification_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdQualificationResponse.Rows.Count; i++)
        {
            grdQualificationResponse.Rows[i].BackColor = System.Drawing.Color.Transparent;
        }
        gr.BackColor = System.Drawing.Color.LightGreen;
        int ProjectDPRBidder_Id = 0;
        try
        {
            ProjectDPRBidder_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectDPRBidder_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = new DataLayer().get_tbl_ProjectDPRPQCResponse(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdQualificationResponse.DataSource = ds.Tables[0];
            grdQualificationResponse.DataBind();
        }
        else
        {
            grdQualificationResponse.DataSource = null;
            grdQualificationResponse.DataBind();
        }

        ds = new DataLayer().get_tbl_ProjectDPR_Bidder_Order(Convert.ToInt32(hf_ProjectDPR_Id.Value), ProjectDPRBidder_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdQualificationWorkOrder.DataSource = ds.Tables[0];
            grdQualificationWorkOrder.DataBind();
        }
        else
        {
            grdQualificationWorkOrder.DataSource = null;
            grdQualificationWorkOrder.DataBind();
        }
        mpBidder.Show();
    }

    protected void grdBidder_PreRender(object sender, EventArgs e)
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

    protected void grdQualificationCriteria_PreRender(object sender, EventArgs e)
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

    protected void grdTenderPublished_PreRender(object sender, EventArgs e)
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

    protected void grdCommonStep_PreRender(object sender, EventArgs e)
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

    protected void grdEFC_PFAD_PreRender(object sender, EventArgs e)
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

    protected void grdGODetails_PreRender(object sender, EventArgs e)
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

    protected void grdPBMResponseDoc_PreRender(object sender, EventArgs e)
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

    protected void grdBidder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gr = e.Row;
            string Is_JV = "No";
            Is_JV = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (Is_JV == "")
            {
                Is_JV = "No";
            }
            HtmlTableRow trPartnerBidder = gr.FindControl("trPartnerBidder") as HtmlTableRow;
            HtmlTableCell tdShare = gr.FindControl("tdShare") as HtmlTableCell;
            HtmlTableCell tdShareP = gr.FindControl("tdShareP") as HtmlTableCell;
            if (Is_JV == "Yes")
            {
                trPartnerBidder.Visible = true;
                tdShare.Visible = true;
                tdShareP.Visible = true;
            }
            else
            {
                trPartnerBidder.Visible = false;
                tdShare.Visible = false;
                tdShareP.Visible = false;
            }
        }
    }

    protected void grdQualificationWorkOrder_PreRender(object sender, EventArgs e)
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

    protected void lnkNIT_Issued2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 4, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }

    protected void lnkNIT_Issued2F_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
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
        int Tranche_Id = 0;
        try
        {
            Tranche_Id = Convert.ToInt32(ddlTranche.SelectedValue);
        }
        catch
        {
            Tranche_Id = 0;
        }
        int NodalDepartment_Id = 0;
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 4, Tranche_Id, NodalDepartment_Id.ToString(), "");
    }
}