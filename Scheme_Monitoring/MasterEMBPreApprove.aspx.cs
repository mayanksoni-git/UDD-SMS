using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterEMBPreApprove : System.Web.UI.Page
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
            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
            get_tbl_Project();
            get_tbl_Zone();

            if (Session["UserType"].ToString() == "1")
            {
                rbtSearchBy.SelectedValue = "2";
            }
            else
            {
                rbtSearchBy.SelectedValue = "1";
            }
            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                txtDateFrom.Text = obj_SearchStorage.FromDate;
                txtDateTill.Text = obj_SearchStorage.TillDate;
                ddlScheme.SelectedValue = obj_SearchStorage.Scheme_Id.ToString();
                rbtSearchBy.SelectedValue = obj_SearchStorage.Search_By;
                if (obj_SearchStorage.Zone_Id > 0)
                {
                    ddlZone.SelectedValue = obj_SearchStorage.Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                }
                if (obj_SearchStorage.Circle_Id > 0)
                {
                    ddlCircle.SelectedValue = obj_SearchStorage.Circle_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
                }
                if (obj_SearchStorage.Division_Id > 0)
                {
                    ddlDivision.SelectedValue = obj_SearchStorage.Division_Id.ToString();
                }
            }

            rbtSearchBy_SelectedIndexChanged(rbtSearchBy, e);

            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
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
            ChkShowEMB.Checked = true;
            load_dashboard();

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() == "S")
                {
                    MessageBox.Show("Package EMB Marked Approved");
                }
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() == "B")
                {
                    MessageBox.Show("Package EMB Marked For Billing");
                }
            }
        }
    }

    private void load_dashboard()
    {
        SearchStorage obj_SearchStorage = new SearchStorage();
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        Scheme_Id = ddlScheme.SelectedValue;

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
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        get_tbl_PackageEMB();
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

    private void get_tbl_PackageEMB()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        Scheme_Id = ddlScheme.SelectedValue;

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
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0, "", ChkShowEMB.Checked, "", "", 0, false, 0);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), 0, "", ChkShowEMB.Checked, "", "", 0, false, 0);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0, "", ChkShowEMB.Checked, txtDateFrom.Text, txtDateTill.Text, 0, false, 0);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), 0, "", ChkShowEMB.Checked, txtDateFrom.Text, txtDateTill.Text, 0, false, 0);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();
            //btnMarkMB.Visible = false;
            //btnCombineEMB.Visible = true;
            //for (int i = 0; i < grdEMB.Rows.Count; i++)
            //{
            //    CheckBox chkMarkMB = grdEMB.Rows[i].FindControl("chkMarkMB") as CheckBox;
            //    if (chkMarkMB.Visible)
            //    {
            //        btnMarkMB.Visible = true;
            //    }
            //}
        }
        else
        {
            //btnCombineEMB.Visible = false;
            //btnMarkMB.Visible = false;
            grdEMB.DataSource = null;
            grdEMB.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        string Scheme_Id = "";
        Scheme_Id = ddlScheme.SelectedValue;

        load_dashboard();
    }

    protected void grdEMB_PreRender(object sender, EventArgs e)
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

    protected void btnOpenEMB_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string PackageEMB_Master_Id = gr.Cells[0].Text.Trim();
        string PackageEMB_Master_Type = gr.Cells[4].Text.Trim();
        string Package_Id = gr.Cells[1].Text.Trim();
        string Scheme_Id = gr.Cells[5].Text.Trim();
        if (PackageEMB_Master_Type == "N")
        {
            Response.Redirect("MasterEMBApprove_New.aspx?Package_Id=" + Package_Id + "&EMB_Master_Id=" + PackageEMB_Master_Id + "&Scheme_Id=" + Scheme_Id);
        }
        else
        {
            Response.Redirect("MasterEMBApprove.aspx?Package_Id=" + Package_Id + "&EMB_Master_Id=" + PackageEMB_Master_Id + "&Scheme_Id=" + Scheme_Id);
        }
    }


    protected void ChkShowEMB_CheckedChanged(object sender, EventArgs e)
    {
        get_tbl_PackageEMB();
    }

    protected void rbtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtSearchBy.SelectedValue == "1")
        {
            divFromDate.Visible = false;
            divTillDate.Visible = false;
        }
        else
        {
            divFromDate.Visible = true;
            divTillDate.Visible = true;
        }
    }

    protected void grdEMB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[12].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[13].Text = Session["Default_Division"].ToString();
        }
    }
}