using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Data_Updation_Status : System.Web.UI.Page
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
            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlSearchScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlSearchScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
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
                            //ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            //ddlULB.Enabled = false;
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
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
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
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
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
                int Scheme_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;

                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                }
                catch
                {
                    Scheme_Id = 0;
                }
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                }
                catch
                {
                    Division_Id = 0;
                }
                if (Scheme_Id > 0)
                    ddlScheme.SelectedValue = Scheme_Id.ToString();
                if (ddlZone.Enabled && Zone_Id > 0)
                {
                    ddlZone.SelectedValue = Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                }
                if (ddlCircle.Enabled && Circle_Id > 0)
                {
                    ddlCircle.SelectedValue = Circle_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, e);
                }
                if (ddlDivision.Enabled && Division_Id > 0)
                {
                    ddlDivision.SelectedValue = Division_Id.ToString();
                }

                btnSearch_Click(btnSearch, e);
            }
        }
    }
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
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

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    protected void grdDataUpdationStatusReport_PreRender(object sender, EventArgs e)
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
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Data_Updation_Dashboard_Division_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            divData.Visible = true;
            grdDataUpdationStatusReport.DataSource = ds.Tables[0];
            grdDataUpdationStatusReport.DataBind();

            grdDataUpdationStatusReport.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Project)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Package)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkBOQ_NA_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_BOQ_Not_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Total_Freezed)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkUnFreezed_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Not_Freezed)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Total_Agreement_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Total_BankGurantee_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(Total_Mobelization_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(Total_PerformanceSecurity_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Total_MA_Filled)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Total_RA_Bills_Filled)", "").ToString() + " / " + ds.Tables[0].Compute("sum(Total_RA_Bills)", "").ToString();
        }
        else
        {
            divData.Visible = false;
            grdDataUpdationStatusReport.DataSource = null;
            grdDataUpdationStatusReport.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkBOQ_NA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Division_Id = 0;
        string Scheme_Id = "";
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(0, Scheme_Id, Zone_Id, 0, Division_Id, false, true, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkBOQ_NA_F_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(0, Scheme_Id, Zone_Id, 0, 0, false, true, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkUnFreezed_F_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(0, Scheme_Id, Zone_Id, 0, 0, true, false, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkUnFreezed_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Division_Id = 0;
        string Scheme_Id = "";
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(0, Scheme_Id, Zone_Id, 0, Division_Id, true, false, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdPkgView_PreRender(object sender, EventArgs e)
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
    protected void get_Document_View(int Division_Id, string Scheme_Id, int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_ProjectWorkPkg_Document_Dashboard(Scheme_Id, Zone_Id, 0, Division_Id, 0, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkAgreementUploaded_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Division_Id = 0;
        string Scheme_Id = "";
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        get_Document_View(Division_Id, Scheme_Id, Zone_Id);
    }

    protected void lnkBGUploaded_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Division_Id = 0;
        string Scheme_Id = "";
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        get_Document_View(Division_Id, Scheme_Id, Zone_Id);
    }

    protected void lnkMAUploaded_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Division_Id = 0;
        string Scheme_Id = "";
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        get_Document_View(Division_Id, Scheme_Id, Zone_Id);
    }

    protected void lnkPSUploaded_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Division_Id = 0;
        string Scheme_Id = "";
        int Zone_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        get_Document_View(Division_Id, Scheme_Id, Zone_Id);
    }

    protected void grdPkgView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[19].FindControl("hlBG") as HyperLink).NavigateUrl != "")
            {
                e.Row.Cells[19].BackColor = Color.LightGreen;
            }
            if ((e.Row.Cells[20].FindControl("hlMA") as HyperLink).NavigateUrl != "")
            {
                e.Row.Cells[20].BackColor = Color.LightGreen;
            }
            if ((e.Row.Cells[21].FindControl("hlPS") as HyperLink).NavigateUrl != "")
            {
                e.Row.Cells[21].BackColor = Color.LightGreen;
            }
            if ((e.Row.Cells[22].FindControl("hlAG") as HyperLink).NavigateUrl != "")
            {
                e.Row.Cells[22].BackColor = Color.LightGreen;
            }
        }
    }

    protected void grdDataUpdationStatusReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Division"].ToString();
        }
    }
}
