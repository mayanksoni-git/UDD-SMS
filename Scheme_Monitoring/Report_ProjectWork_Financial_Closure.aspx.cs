using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_ProjectWork_Financial_Closure : System.Web.UI.Page
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
            get_tbl_Zone();
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
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
            
            get_Financial_Closure_Pending();
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
        get_Financial_Closure_Pending();
    }

    protected void get_Financial_Closure_Pending()
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
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        if (Request.QueryString.Count > 0)
        {
            ds = (new DataLayer()).get_Financial_Closure_Summery(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 180);
            ds1 = (new DataLayer()).get_Financial_Closure_Pending_New(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 180, false, false, false, false, false, false, false, false);
        }
        else
        {
            int Pendency_Days = 0;
            try
            {
                Pendency_Days = Convert.ToInt32(txtPendencyDays.Text);
            }
            catch
            { }
            ds = (new DataLayer()).get_Financial_Closure_Summery(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, Pendency_Days);
            ds1 = (new DataLayer()).get_Financial_Closure_Pending_New(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, Pendency_Days, false, false, false, false, false, false, false, false);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            (grdPost.FooterRow.FindControl("lnkSchemesCompletedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Schemes_Completed)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkHandoverNoteSendF") as LinkButton).Text = ds.Tables[0].Compute("sum(HandoverNoteSend)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkHandoverDoneF") as LinkButton).Text = ds.Tables[0].Compute("sum(HandoverDone)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkHandoverPendingF") as LinkButton).Text = ds.Tables[0].Compute("sum(HandoverPending)", "").ToString();

            (grdPost.FooterRow.FindControl("lnkHandoverNotInitiatedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Hanover_Not_Initiated)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkUnderDefectLibeltyPeriodF") as LinkButton).Text = ds.Tables[0].Compute("sum(Under_Defect_Libelty_Period)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkFinancialClosureDoneF") as LinkButton).Text = ds.Tables[0].Compute("sum(FinancialClosureDone)", "").ToString();
            (grdPost.FooterRow.FindControl("lnkFinancialClosurePendingF") as LinkButton).Text = ds.Tables[0].Compute("sum(FinancialClosurePending)", "").ToString();
            //(grdPost.FooterRow.FindControl("lnkFinancialClosureNotInitiatedF") as LinkButton).Text = ds.Tables[0].Compute("sum(FinancialClosure_Not_Initiated)", "").ToString();

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

    protected void get_Financial_Closure_Pending_Details(int District_Id, int Zone_Id, int Circle_Id, int Division_Id, int ULB_Id, string Scheme_Id, bool Schemes_Completed, bool HandoverNoteSend, bool HandoverDone, bool HandoverPending, bool Hanover_Not_Initiated, bool Under_Defect_Libelty_Period, bool FinancialClosureDone, bool FinancialClosurePending)
    {
        DataSet ds1 = new DataSet();
        if (Request.QueryString.Count > 0)
        {
            ds1 = (new DataLayer()).get_Financial_Closure_Pending_New(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 180, Schemes_Completed, HandoverNoteSend, HandoverDone, HandoverPending, Hanover_Not_Initiated, Under_Defect_Libelty_Period, FinancialClosureDone, FinancialClosurePending);
        }
        else
        {
            int Pendency_Days = 0;
            try
            {
                Pendency_Days = Convert.ToInt32(txtPendencyDays.Text);
            }
            catch
            { }
            ds1 = (new DataLayer()).get_Financial_Closure_Pending_New(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, Pendency_Days, Schemes_Completed, HandoverNoteSend, HandoverDone, HandoverPending, Hanover_Not_Initiated, Under_Defect_Libelty_Period, FinancialClosureDone, FinancialClosurePending);
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

    protected void lnkSchemesCompleted_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, true, false, false, false, false, false, false, false);
    }

    protected void lnkSchemesCompletedF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, true, false, false, false, false, false, false, false);
    }

    protected void lnkHandoverNoteSend_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, true, false, false, false, false, false, false);
    }

    protected void lnkHandoverNoteSendF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, true, false, false, false, false, false, false);
    }

    protected void lnkHandoverDone_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, true, false, false, false, false, false);
    }

    protected void lnkHandoverDoneF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, true, false, false, false, false, false);
    }

    protected void lnkHandoverPending_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, true, false, false, false, false);
    }

    protected void lnkHandoverPendingF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, true, false, false, false, false);
    }

    protected void lnkHandoverNotInitiated_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, true, false, false, false);
    }

    protected void lnkHandoverNotInitiatedF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, true, false, false, false);
    }

    protected void lnkUnderDefectLibeltyPeriod_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, false, true, false, false);
    }

    protected void lnkUnderDefectLibeltyPeriodF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, false, true, false, false);
    }

    protected void lnkFinancialClosureDone_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, false, false, true, false);
    }

    protected void lnkFinancialClosureDoneF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, false, false, true, false);
    }

    protected void lnkFinancialClosurePending_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, false, false, false, true);
    }

    protected void lnkFinancialClosurePendingF_Click(object sender, EventArgs e)
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
        get_Financial_Closure_Pending_Details(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, false, false, false, false, false, false, false, true);
    }

    protected void lnkFinancialClosureNotInitiated_Click(object sender, EventArgs e)
    {

    }

    protected void lnkFinancialClosureNotInitiatedF_Click(object sender, EventArgs e)
    {

    }

    protected void grdFinancialFull_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();
        }
    }
}