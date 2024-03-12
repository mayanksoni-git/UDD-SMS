using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_PPA_Analysis : System.Web.UI.Page
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

            get_Project_Work_Pre_Close();
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
        get_Project_Work_Pre_Close();
    }

    protected void get_Project_Work_Pre_Close()
    {
        int PPA_PNB = 0;
        decimal PPA_PNB_Val = 0;

        int PPA_EE = 0;
        decimal PPA_EE_Val = 0;

        int PPA_Pipeline = 0;
        decimal PPA_Pipeline_Val = 0;

        int Scheme_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            Scheme_Id = 0;
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
        ds = (new DataLayer()).get_PPA_Analysis_Pending_With_Bank(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString();
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

            lnkPPAPNB.Text = ds.Tables[0].Rows.Count.ToString() + " / " + AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString(), "decimal");

            try
            {
                PPA_PNB = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            }
            catch
            {
                PPA_PNB = 0;
            }

            try
            {
                PPA_PNB_Val = Convert.ToDecimal(ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString());
            }
            catch
            {
                PPA_PNB_Val = 0;
            }
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
            lnkPPAPNB.Text = "0/0";
        }

        ds = (new DataLayer()).get_PPA_Analysis_Pending_With_EE(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdFinancialFull.DataSource = ds.Tables[0];
            grdFinancialFull.DataBind();
            grdFinancialFull.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString();
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
            lnkPPAEE.Text = ds.Tables[0].Rows.Count.ToString() + " / " + AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString(), "decimal");

            try
            {
                PPA_EE = Convert.ToInt32(ds.Tables[0].Rows.Count.ToString());
            }
            catch
            {
                PPA_EE = 0;
            }

            try
            {
                PPA_EE_Val = Convert.ToDecimal(ds.Tables[0].Compute("sum(Batch_Amount)", "").ToString());
            }
            catch
            {
                PPA_EE_Val = 0;
            }
        }
        else
        {
            lnkPPAEE.Text = "0/0";
            grdFinancialFull.DataSource = null;
            grdFinancialFull.DataBind();
        }

        ds = (new DataLayer()).get_PPA_Analysis_Pending_With_EE_To_Generate(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPPAPending.DataSource = ds.Tables[0];
            grdPPAPending.DataBind();
            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdPPAPending.Columns.Count; i++)
            {
                if (grdPPAPending.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic3.Value = jss.Serialize(columnsList);
        }
        else
        {
            grdPPAPending.DataSource = null;
            grdPPAPending.DataBind();
        }

        ds = (new DataLayer()).get_PPA_Analysis_Invalid_PPA_Linked(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPPAInvalid.DataSource = ds.Tables[0];
            grdPPAInvalid.DataBind();
            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdPPAInvalid.Columns.Count; i++)
            {
                if (grdPPAInvalid.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic4.Value = jss.Serialize(columnsList);
        }
        else
        {
            grdPPAInvalid.DataSource = null;
            grdPPAInvalid.DataBind();
        }

        ds = (new DataLayer()).get_PPA_Pipeline_Details(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkInPipeline.Text = ds.Tables[0].Compute("sum(Total_PPA)", "").ToString() + " / " + AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Pipeline)", "").ToString(), "decimal");

            try
            {
                PPA_Pipeline = Convert.ToInt32(ds.Tables[0].Compute("sum(Total_PPA)", "").ToString());
            }
            catch
            {
                PPA_Pipeline = 0;
            }

            try
            {
                PPA_Pipeline_Val = Convert.ToDecimal(ds.Tables[0].Compute("sum(Pipeline)", "").ToString());
            }
            catch
            {
                PPA_Pipeline_Val = 0;
            }
        }
        else
        {
            lnkInPipeline.Text = "0/0";
        }

        int PPA_Pending_Count = PPA_Pipeline - PPA_PNB - PPA_EE;
        if (PPA_Pending_Count < 0)
            PPA_Pending_Count = 0;

        decimal PPA_Pending_Amount = PPA_Pipeline_Val - PPA_PNB_Val - PPA_EE_Val;
        if (PPA_Pending_Amount < 0)
            PPA_Pending_Amount = 0;

        lnkPPAPending.Text = PPA_Pending_Count.ToString() + " / " + AllClasses.convert_To_Indian_No_Format(PPA_Pending_Amount.ToString(), "decimal");
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();
        }
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

    protected void grdPPAPending_PreRender(object sender, EventArgs e)
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

    protected void grdPPAPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdPPAInvalid_PreRender(object sender, EventArgs e)
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

    protected void grdPPAInvalid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }
}