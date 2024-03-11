using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
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
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_dashboard();
    }
}