using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class DashboardNew : System.Web.UI.Page
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
            get_tbl_Zone();
            if (Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "33" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
            {
                divMIS.Visible = true;
            }
            else if (Session["UserType"].ToString() == "1")
            {
                divMIS.Visible = false;
            }
            else if (Session["UserType"].ToString() == "3")
            {
                divMIS.Visible = true;
            }
            else
            {
                divMIS.Visible = false;
            }
            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
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
            Load_dashboard();
        }
    }

    private void Load_dashboard()
    {
        SearchStorage obj_SearchStorage = new SearchStorage();
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;

        Session["SearchStorage"] = obj_SearchStorage;

        DataSet ds = new DataSet();

        //ds = (new DataLayer()).get_PMIS_Dashboard_FinancialYearWise(0, 0, 0, "", 0, 0, "", "", "", -1, "", "");
        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    List<Project_Over_All_Status> obj_Project_Over_All_Status_Li = new List<Project_Over_All_Status>();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        Project_Over_All_Status obj_Project_Over_All_Status = new Project_Over_All_Status();
        //        obj_Project_Over_All_Status.Data_Type = ds.Tables[0].Rows[i]["Data_Type"].ToString();
        //        try
        //        {
        //            obj_Project_Over_All_Status.Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total"].ToString());
        //        }
        //        catch
        //        {
        //            obj_Project_Over_All_Status.Total = 0;
        //        }
        //        try
        //        {
        //            obj_Project_Over_All_Status.WithIn_UP = Convert.ToDecimal(ds.Tables[0].Rows[i]["WithIn_UP"].ToString());
        //        }
        //        catch
        //        {
        //            obj_Project_Over_All_Status.WithIn_UP = 0;
        //        }
        //        try
        //        {
        //            obj_Project_Over_All_Status.OutSide_UP = Convert.ToDecimal(ds.Tables[0].Rows[i]["OutSide_UP"].ToString());
        //        }
        //        catch
        //        {
        //            obj_Project_Over_All_Status.OutSide_UP = 0;
        //        }
        //        try
        //        {
        //            obj_Project_Over_All_Status.Completed = Convert.ToDecimal(ds.Tables[0].Rows[i]["Completed"].ToString());
        //        }
        //        catch
        //        {
        //            obj_Project_Over_All_Status.Completed = 0;
        //        }
        //        try
        //        {
        //            obj_Project_Over_All_Status.OnGoing = Convert.ToDecimal(ds.Tables[0].Rows[i]["OnGoing"].ToString());
        //        }
        //        catch
        //        {
        //            obj_Project_Over_All_Status.OnGoing = 0;
        //        }
        //        obj_Project_Over_All_Status_Li.Add(obj_Project_Over_All_Status);
        //    }
        //    hf_Over_All_Data.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Over_All_Status_Li);
        //}
        //else
        //{
        //    hf_Over_All_Data.Value = "[]";
        //}
















        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_PMIS_Dashboard_Detailed(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", -1, "", "", 0);
        }
        else
        {
            ds = (new DataLayer()).get_PMIS_Dashboard_Detailed(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", -1, "", "", Convert.ToInt32(Session["Person_Id"].ToString()));
        }

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            int TotalRecords = ds.Tables[0].Rows.Count;
            int RowCount = TotalRecords / 4;
            int GAP = TotalRecords - RowCount * 4;

            int _AddIndex = -1;
            DataRow[] drAdd = null;

            DataView dv = new DataView(ds.Tables[0]);
            if (GAP > 0)
            {
                _AddIndex = RowCount * 4 + 1;
            }
            dv.RowFilter = "SrNo >= 1 and SrNo <= " + RowCount.ToString();
            DataTable dt1 = dv.ToTable();
            if (AllClasses.CheckDt(dt1))
            {
                if (_AddIndex > -1)
                {
                    drAdd = ds.Tables[0].Select("SrNo = " + _AddIndex);
                    if (drAdd != null && drAdd.Length > 0)
                    {
                        dt1.Rows.Add(drAdd[0].ItemArray);
                    }
                    _AddIndex = -1;
                }
                grdScheme_1.DataSource = dt1;
                grdScheme_1.DataBind();
            }
            else
            {
                if (_AddIndex > -1)
                {
                    drAdd = ds.Tables[0].Select("SrNo = " + _AddIndex);
                    if (drAdd != null && drAdd.Length > 0)
                    {
                        dt1.Rows.Add(drAdd[0].ItemArray);
                        if (AllClasses.CheckDt(dt1))
                        {
                            grdScheme_1.DataSource = dt1;
                            grdScheme_1.DataBind();
                        }
                    }
                    _AddIndex = -1;
                }
                else
                {
                    grdScheme_1.DataSource = null;
                    grdScheme_1.DataBind();
                }
            }

            dv = new DataView(ds.Tables[0]);
            if (GAP > 1)
            {
                _AddIndex = RowCount * 4 + 2;
            }
            dv.RowFilter = "SrNo >= " + (RowCount + 1).ToString() + " and SrNo <= " + (RowCount * 2).ToString();
            DataTable dt2 = dv.ToTable();
            if (AllClasses.CheckDt(dt2))
            {
                drAdd = ds.Tables[0].Select("SrNo = " + _AddIndex);
                if (drAdd != null && drAdd.Length > 0)
                {
                    dt2.Rows.Add(drAdd[0].ItemArray);
                }
                _AddIndex = -1;
                grdScheme_2.DataSource = dt2;
                grdScheme_2.DataBind();
            }
            else
            {
                if (_AddIndex > -1)
                {
                    drAdd = ds.Tables[0].Select("SrNo = " + _AddIndex);
                    if (drAdd != null && drAdd.Length > 0)
                    {
                        dt2.Rows.Add(drAdd[0].ItemArray);
                        if (AllClasses.CheckDt(dt2))
                        {
                            grdScheme_2.DataSource = dt2;
                            grdScheme_2.DataBind();
                        }
                    }
                    _AddIndex = -1;
                }
                else
                {
                    grdScheme_2.DataSource = null;
                    grdScheme_2.DataBind();
                }
            }

            dv = new DataView(ds.Tables[0]);
            if (GAP > 2)
            {
                _AddIndex = RowCount * 4 + 3;
            }
            dv.RowFilter = "SrNo >= " + (RowCount * 2 + 1).ToString() + " and SrNo <= " + (RowCount * 3).ToString();
            DataTable dt3 = dv.ToTable();
            if (AllClasses.CheckDt(dt3))
            {
                drAdd = ds.Tables[0].Select("SrNo = " + _AddIndex);
                if (drAdd != null && drAdd.Length > 0)
                {
                    dt3.Rows.Add(drAdd[0].ItemArray);
                }
                _AddIndex = -1;
                grdScheme_3.DataSource = dt3;
                grdScheme_3.DataBind();
            }
            else
            {
                if (_AddIndex > -1)
                {
                    drAdd = ds.Tables[0].Select("SrNo = " + _AddIndex);
                    if (drAdd != null && drAdd.Length > 0)
                    {
                        dt3.Rows.Add(drAdd[0].ItemArray);
                        if (AllClasses.CheckDt(dt3))
                        {
                            grdScheme_3.DataSource = dt3;
                            grdScheme_3.DataBind();
                        }
                    }
                    _AddIndex = -1;
                }
                else
                {
                    grdScheme_3.DataSource = null;
                    grdScheme_3.DataBind();
                }
            }

            dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "SrNo >= " + (RowCount * 3 + 1).ToString() + " and SrNo <= " + (RowCount * 4).ToString();
            DataTable dt4 = dv.ToTable();
            if (AllClasses.CheckDt(dt4))
            {
                grdScheme_4.DataSource = dt4;
                grdScheme_4.DataBind();
            }
            else
            {
                grdScheme_4.DataSource = null;
                grdScheme_4.DataBind();
            }
        }
        else
        {
            grdScheme_1.DataSource = null;
            grdScheme_1.DataBind();

            grdScheme_2.DataSource = null;
            grdScheme_2.DataBind();

            grdScheme_3.DataSource = null;
            grdScheme_3.DataBind();

            grdScheme_4.DataSource = null;
            grdScheme_4.DataBind();
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

    protected void grdScheme_1_PreRender(object sender, EventArgs e)
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

    protected void grdScheme_2_PreRender(object sender, EventArgs e)
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

    protected void grdScheme_3_PreRender(object sender, EventArgs e)
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

    protected void grdScheme_4_PreRender(object sender, EventArgs e)
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
    protected void btnAnalysisReport1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Menu_Analysis.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnEMBDashbaord1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("DashboardDept.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnAnalysisReport2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Menu_Analysis.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnEMBDashbaord2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("DashboardDept.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnAnalysisReport3_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Menu_Analysis.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnEMBDashbaord3_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("DashboardDept.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnAnalysisReport4_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("Menu_Analysis.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnEMBDashbaord4_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("DashboardDept.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }
    protected void btnMIS_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_dashboard();
    }
}