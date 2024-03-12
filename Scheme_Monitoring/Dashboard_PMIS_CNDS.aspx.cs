using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_PMIS_CNDS : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            get_M_Jurisdiction();
            get_tbl_Zone();
            get_Employee();
            get_Year_Data();
            //if (Session["UserType"].ToString() == "13")
            //{
            //    ddlSanctionYear.SelectedValue = "01/01/2023|31/12/2023";
            //}
            divScheme.Visible = false;
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
                if (obj_SearchStorage.District_Id > 0)
                {
                    ddlDistrict.SelectedValue = obj_SearchStorage.District_Id.ToString();
                }
                if (obj_SearchStorage.Division_Id > 0)
                {
                    ddlDivision.SelectedValue = obj_SearchStorage.Division_Id.ToString();
                }
                if (obj_SearchStorage.View_Mode != "")
                {
                    rbtViewMode.SelectedValue = obj_SearchStorage.View_Mode;
                    rbtViewMode_SelectedIndexChanged(rbtViewMode, e);
                }
                if (obj_SearchStorage.NodalDepartment_Id.Trim() != "")
                {
                    string[] _Nodal_Department = obj_SearchStorage.NodalDepartment_Id.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (ListItem listItem in ddlNodalDept.Items)
                    {
                        if (_Nodal_Department != null && _Nodal_Department.Length > 0)
                        {
                            for (int i = 0; i < _Nodal_Department.Length; i++)
                            {
                                if (_Nodal_Department[i].Trim() == listItem.Value)
                                {
                                    listItem.Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                    ddlNodalDept_SelectedIndexChanged(ddlNodalDept, new EventArgs());
                }
                else
                {
                    foreach (ListItem listItem in ddlNodalDept.Items)
                    {
                        listItem.Selected = false;
                    }
                }
                if (obj_SearchStorage.FundingPattern_Id > 0)
                {
                    try
                    {
                        ddlScheme.SelectedValue = obj_SearchStorage.FundingPattern_Id.ToString();
                    }
                    catch
                    {

                    }
                }
                if (obj_SearchStorage.NodalDepartmentScheme_Id.Trim() != "")
                {
                    string[] _Nodal_Department_Scheme = obj_SearchStorage.NodalDepartmentScheme_Id.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (ListItem listItem in ddlNodalDeptScheme.Items)
                    {
                        if (_Nodal_Department_Scheme != null && _Nodal_Department_Scheme.Length > 0)
                        {
                            for (int i = 0; i < _Nodal_Department_Scheme.Length; i++)
                            {
                                if (_Nodal_Department_Scheme[i].Trim() == listItem.Value)
                                {
                                    listItem.Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (obj_SearchStorage.Jurisdiction_In == -1)
                {
                    chkJurisdictionFilter.Items[0].Selected = true;
                    chkJurisdictionFilter.Items[1].Selected = true;
                }
                else if (obj_SearchStorage.Jurisdiction_In == 1)
                {
                    chkJurisdictionFilter.Items[0].Selected = true;
                    chkJurisdictionFilter.Items[1].Selected = false;
                }
                else if (obj_SearchStorage.Jurisdiction_In == 0)
                {
                    chkJurisdictionFilter.Items[0].Selected = false;
                    chkJurisdictionFilter.Items[1].Selected = true;
                }
                else
                {

                }

                chkFY_Wise.Checked = obj_SearchStorage.SanctionYear_Mode;
                chkFY_Wise_CheckedChanged(chkFY_Wise, e);
                if (obj_SearchStorage.SanctionYear_Value != null && obj_SearchStorage.SanctionYear_Value != "")
                {
                    foreach (ListItem listItem in ddlSanctionYear.Items)
                    {
                        if (obj_SearchStorage.SanctionYear_Value.Trim() == listItem.Value)
                        {
                            listItem.Selected = true;
                            break;
                        }
                    }
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

            if (ddlCircle.Enabled && ddlCircle.Items.Count == 0)
            {
                get_tbl_Circle(0);
            }
            if (ddlDivision.Enabled && ddlDivision.Items.Count == 0)
            {
                get_tbl_Division(0);
            }
            if (Session["UserType"].ToString() == "13")
            {
                btnMIS.Visible = false;
            }
            else
            {
                btnMIS.Visible = true;
            }
            get_PMIS_Dashboard_View();
        }
    }
    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
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

    private void get_tbl_Division_Mapped_Nodel(int Zone_Id, int Circle_Id, string NodalDepartment_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division_Mapped_Nodel(Zone_Id, Circle_Id, NodalDepartment_Id);
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
    private void get_Employee()
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
    private void get_tbl_Nodal_Circle_Link(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Nodal_Circle_Link(Circle_Id, false);
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

    protected void get_PMIS_Dashboard_View()
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            _Scheme_Id = 0;
        }
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        DataSet ds = new DataSet();

        int Session_Zone_Id = 0;
        int Session_Circle_Id = 0;
        int Session_Division_Id = 0;

        if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {//Zone
            try
            {
                Session_Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Session_Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Session_Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Session_Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Session_Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Session_Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Session_Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Session_Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Session_Zone_Id = 0;
            }
        }

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Jurisdiction_In = Jurisdiction_In;
        obj_SearchStorage.NodalDepartmentScheme_Id = NodalDepartmentScheme_Id;
        obj_SearchStorage.NodalDepartment_Id = NodalDepartment_Id;
        obj_SearchStorage.SanctionYear_Mode = chkFY_Wise.Checked;
        obj_SearchStorage.SanctionYear_Value = ddlSanctionYear.SelectedValue;
        obj_SearchStorage.FundingPattern_Id = _Scheme_Id;
        obj_SearchStorage.View_Mode = rbtViewMode.SelectedValue;
        Session["SearchStorage"] = obj_SearchStorage;

        if (Session_Zone_Id == 0 && Session_Circle_Id == 0 && Session_Division_Id == 0)
        {
            divShowDeptWise.Visible = true;
            divOverAllStatus.Visible = true;
            ds = (new DataLayer()).get_PMIS_Dashboard_CNDS(0, 0, 0, "", 0, 0, "", "", 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<Project_Over_All_Status> obj_Project_Over_All_Status_Li = new List<Project_Over_All_Status>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Project_Over_All_Status obj_Project_Over_All_Status = new Project_Over_All_Status();
                    obj_Project_Over_All_Status.Data_Type = ds.Tables[0].Rows[i]["Data_Type"].ToString();
                    try
                    {
                        obj_Project_Over_All_Status.Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total"].ToString());
                    }
                    catch
                    {
                        obj_Project_Over_All_Status.Total = 0;
                    }
                    try
                    {
                        obj_Project_Over_All_Status.WithIn_UP = Convert.ToDecimal(ds.Tables[0].Rows[i]["WithIn_UP"].ToString());
                    }
                    catch
                    {
                        obj_Project_Over_All_Status.WithIn_UP = 0;
                    }
                    try
                    {
                        obj_Project_Over_All_Status.OutSide_UP = Convert.ToDecimal(ds.Tables[0].Rows[i]["OutSide_UP"].ToString());
                    }
                    catch
                    {
                        obj_Project_Over_All_Status.OutSide_UP = 0;
                    }
                    try
                    {
                        obj_Project_Over_All_Status.Completed = Convert.ToDecimal(ds.Tables[0].Rows[i]["Completed"].ToString());
                    }
                    catch
                    {
                        obj_Project_Over_All_Status.Completed = 0;
                    }
                    try
                    {
                        obj_Project_Over_All_Status.OnGoing = Convert.ToDecimal(ds.Tables[0].Rows[i]["OnGoing"].ToString());
                    }
                    catch
                    {
                        obj_Project_Over_All_Status.OnGoing = 0;
                    }
                    obj_Project_Over_All_Status_Li.Add(obj_Project_Over_All_Status);
                }
                hf_Over_All_Data.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Over_All_Status_Li);
                grdOverAllStatus.DataSource = ds.Tables[0];
                grdOverAllStatus.DataBind();
            }
            else
            {
                hf_Over_All_Data.Value = "[]";
                grdOverAllStatus.DataSource = null;
                grdOverAllStatus.DataBind();
            }


            ds = (new DataLayer()).get_PMIS_Dashboard_Nodal_Department_Wise(0, 0, 0, "", 0, 0, "", "", "", -1, "", "", "", 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divNodalDeptWise.Visible = false;
                grdNodalDept.DataSource = ds.Tables[0];
                grdNodalDept.DataBind();

                (grdNodalDept.FooterRow.FindControl("lblTotalProjectsF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblTotalSanctionedCostF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Sanction)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblTotalReleasedAmountF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Release)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblTotalExpenditureF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Expenditure)", "").ToString();

                (grdNodalDept.FooterRow.FindControl("lblCompletedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Projects_Completed)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblCompletedSanctionedCostF") as LinkButton).Text = ds.Tables[0].Compute("sum(Completed_Sanction)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblCompletedReleasedAmountF") as LinkButton).Text = ds.Tables[0].Compute("sum(Completed_Release)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblCompletedExpenditureF") as LinkButton).Text = ds.Tables[0].Compute("sum(Completed_Expenditure)", "").ToString();

                (grdNodalDept.FooterRow.FindControl("lblOngoingF") as LinkButton).Text = ds.Tables[0].Compute("sum(Projects_onGoing)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblOngoingSanctionedCostF") as LinkButton).Text = ds.Tables[0].Compute("sum(OnGoing_Sanction)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblOngoingReleasedAmountF") as LinkButton).Text = ds.Tables[0].Compute("sum(Ongoing_Release)", "").ToString();
                (grdNodalDept.FooterRow.FindControl("lblOngoingExpenditureF") as LinkButton).Text = ds.Tables[0].Compute("sum(Ongoing_Expenditure)", "").ToString();
            }
            else
            {
                divNodalDeptWise.Visible = false;
                grdNodalDept.DataSource = null;
                grdNodalDept.DataBind();
            }
        }
        else
        {
            divShowDeptWise.Visible = false;
            divOverAllStatus.Visible = false;
            hf_Over_All_Data.Value = "[]";
            grdOverAllStatus.DataSource = null;
            grdOverAllStatus.DataBind();
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_Physical_Financial_Slab(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, Jurisdiction_In, NodalDepartment_Id, NodalDepartmentScheme_Id, FromDate, TillDate);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Project_Physical_Financial_Status obj_Project_Physical_Status = new Project_Physical_Financial_Status();
            Project_Physical_Financial_Status obj_Project_Financial_Status = new Project_Physical_Financial_Status();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Data_Type"].ToString() == "Physical Progress")
                {
                    obj_Project_Physical_Status.Data_Type = "Physical Progress";
                    try
                    {
                        obj_Project_Physical_Status.Total = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.Total = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.Zero = Convert.ToInt32(ds.Tables[0].Rows[i]["Zero"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.Zero = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.Less_10 = Convert.ToInt32(ds.Tables[0].Rows[i]["Less_10"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.Less_10 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_10_20 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_10_20"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_10_20 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_20_30 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_20_30"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_20_30 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_30_40 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_30_40"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_30_40 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_40_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_40_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_40_50 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_50_60 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_50_60"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_50_60 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_60_70 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_60_70"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_60_70 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_70_80 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_70_80"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_70_80 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_80_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_80_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_80_90 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.More_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.More_90 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.More_100 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_100"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.More_100 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_0_25 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_0_25"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_0_25 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_26_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_26_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_26_50 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_51_75 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_51_75"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_51_75 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_76_99 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_76_99"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_76_99 = 0;
                    }
                    if (obj_Project_Physical_Status.Total > 0)
                    {
                        obj_Project_Physical_Status.Percentage_Zero = (obj_Project_Physical_Status.Zero * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_Less_10 = (obj_Project_Physical_Status.Less_10 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_10_20 = (obj_Project_Physical_Status.BW_10_20 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_20_30 = (obj_Project_Physical_Status.BW_20_30 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_30_40 = (obj_Project_Physical_Status.BW_30_40 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_40_50 = (obj_Project_Physical_Status.BW_40_50 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_50_60 = (obj_Project_Physical_Status.BW_50_60 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_60_70 = (obj_Project_Physical_Status.BW_60_70 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_70_80 = (obj_Project_Physical_Status.BW_70_80 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_80_90 = (obj_Project_Physical_Status.BW_80_90 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_More_90 = (obj_Project_Physical_Status.More_90 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_More_100 = (obj_Project_Physical_Status.More_100 * 100) / obj_Project_Physical_Status.Total;

                        obj_Project_Physical_Status.Percentage_BW_0_25 = (obj_Project_Physical_Status.BW_0_25 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_26_50 = (obj_Project_Physical_Status.BW_26_50 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_51_75 = (obj_Project_Physical_Status.BW_51_75 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_76_99 = (obj_Project_Physical_Status.BW_76_99 * 100) / obj_Project_Physical_Status.Total;
                    }
                }
                else if (ds.Tables[0].Rows[i]["Data_Type"].ToString() == "Financial Progress")
                {
                    obj_Project_Financial_Status.Data_Type = "Physical Progress";
                    try
                    {
                        obj_Project_Financial_Status.Total = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.Total = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.Zero = Convert.ToInt32(ds.Tables[0].Rows[i]["Zero"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.Zero = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.Less_10 = Convert.ToInt32(ds.Tables[0].Rows[i]["Less_10"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.Less_10 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_10_20 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_10_20"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_10_20 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_20_30 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_20_30"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_20_30 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_30_40 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_30_40"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_30_40 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_40_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_40_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_40_50 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_50_60 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_50_60"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_50_60 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_60_70 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_60_70"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_60_70 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_70_80 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_70_80"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_70_80 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_80_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_80_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_80_90 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.More_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.More_90 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.More_100 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_100"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.More_100 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_0_25 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_0_25"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_0_25 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_26_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_26_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_26_50 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_51_75 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_51_75"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_51_75 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_76_99 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_76_99"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_76_99 = 0;
                    }
                    if (obj_Project_Financial_Status.Total > 0)
                    {
                        obj_Project_Financial_Status.Percentage_Zero = (obj_Project_Financial_Status.Zero * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_Less_10 = (obj_Project_Financial_Status.Less_10 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_10_20 = (obj_Project_Financial_Status.BW_10_20 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_20_30 = (obj_Project_Financial_Status.BW_20_30 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_30_40 = (obj_Project_Financial_Status.BW_30_40 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_40_50 = (obj_Project_Financial_Status.BW_40_50 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_50_60 = (obj_Project_Financial_Status.BW_50_60 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_60_70 = (obj_Project_Financial_Status.BW_60_70 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_70_80 = (obj_Project_Financial_Status.BW_70_80 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_80_90 = (obj_Project_Financial_Status.BW_80_90 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_More_90 = (obj_Project_Financial_Status.More_90 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_More_100 = (obj_Project_Financial_Status.More_100 * 100) / obj_Project_Financial_Status.Total;

                        obj_Project_Financial_Status.Percentage_BW_0_25 = (obj_Project_Financial_Status.BW_0_25 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_26_50 = (obj_Project_Financial_Status.BW_26_50 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_51_75 = (obj_Project_Financial_Status.BW_51_75 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_76_99 = (obj_Project_Financial_Status.BW_76_99 * 100) / obj_Project_Financial_Status.Total;
                    }
                }
                else
                {

                }
            }
            hf_Physical_Progress_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Physical_Status);
            hf_Financal_Progress_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Financial_Status);
            grdPMISUpdation.DataSource = ds.Tables[0];
            grdPMISUpdation.DataBind();
        }
        else
        {
            hf_Physical_Progress_Filter.Value = "[]";
            hf_Financal_Progress_Filter.Value = "[]";
            grdPMISUpdation.DataSource = null;
            grdPMISUpdation.DataBind();
        }
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, "", FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTotal_Projects.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Count"].ToString(), "Int");
            lnkTotal_Projects_Completed.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Completed_Count"].ToString(), "Int");
            lnkTotal_Projects_Ongoing.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["OnGoing_Count"].ToString(), "Int");

            lnk_Total_Sanction_Cost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lnk_Completed_Sanction_Cost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Completed_Sanction"].ToString(), "Decimal");
            lnk_Ongoing_Sanction_Cost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["OnGoing_Sanction"].ToString(), "Decimal");

            lnk_Total_Release.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lnk_Completed_Release.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Completed_Release"].ToString(), "Decimal");
            lnk_Ongoing_Release.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["OnGoing_Release"].ToString(), "Decimal");

            lnk_Total_Expenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lnk_Completed_Expenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Completed_Expenditure"].ToString(), "Decimal");
            lnk_Ongoing_Expenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["OnGoing_Expenditure"].ToString(), "Decimal");

            lnk_Total_Remaining.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            lnk_Completed_Remaining.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Completed_Remaining_Amount"].ToString(), "Decimal");
            lnk_Ongoing_Remaining.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["OnGoing_Remaining_Amount"].ToString(), "Decimal");

            Project_Filter obj_Project_Filter = new Project_Filter();
            if (rbtGraphFor.SelectedValue == "A")
            {
                obj_Project_Filter.Data_Type = "Data For Total Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            else if (rbtGraphFor.SelectedValue == "O")
            {
                obj_Project_Filter.Data_Type = "Data For OnGoing Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["OnGoing_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            else if (rbtGraphFor.SelectedValue == "C")
            {
                obj_Project_Filter.Data_Type = "Data For Completed Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Completed_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            hf_Data_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Filter);
        }
        else
        {
            hf_Data_Filter.Value = "[]";
            lnkTotal_Projects.Text = "0";
            lnkTotal_Projects_Completed.Text = "0";
            lnkTotal_Projects_Ongoing.Text = "0";

            lnk_Total_Sanction_Cost.Text = "0";
            lnk_Completed_Sanction_Cost.Text = "0";
            lnk_Ongoing_Sanction_Cost.Text = "0";

            lnk_Total_Release.Text = "0";
            lnk_Completed_Release.Text = "0";
            lnk_Ongoing_Release.Text = "0";

            lnk_Total_Expenditure.Text = "0";
            lnk_Completed_Expenditure.Text = "0";
            lnk_Ongoing_Expenditure.Text = "0";

            lnk_Total_Remaining.Text = "0";
            lnk_Completed_Remaining.Text = "0";
            lnk_Ongoing_Remaining.Text = "0";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Delay_Analysis(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, NodalDepartmentScheme_Id, chkDelayOngoing.Checked, FromDate, TillDate, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkNoDelay.Text = ds.Tables[0].Rows[0]["No_Delay"].ToString();
            //lnkdelayWithIssue.Text = ds.Tables[0].Rows[0]["Delay_With_Issue"].ToString();
            //lnkdelayWithoutIssue.Text = ds.Tables[0].Rows[0]["Delay_Without_Issue"].ToString();
            lnkDelay.Text = ds.Tables[0].Rows[0]["Delay"].ToString();

            Project_Delay_Analysis obj_Project_Delay_Analysis = new Project_Delay_Analysis();
            try
            {
                obj_Project_Delay_Analysis.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Projects"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Total_Projects = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_No_Delay = Convert.ToInt32(ds.Tables[0].Rows[0]["No_Delay"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_No_Delay = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_Issue = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay_With_Issue"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_Issue = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_No_Issue = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay_Without_Issue"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_No_Issue = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_Delay = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_Delay = 0;
            }
            hf_Delay_Analysis.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Delay_Analysis);
        }
        else
        {
            hf_Delay_Analysis.Value = "[]";
            lnkNoDelay.Text = "0";
            //lnkdelayWithIssue.Text = "0";
            //lnkdelayWithoutIssue.Text = "0";
            lnkDelay.Text = "0";
        }
        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_CNDS(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, NodalDepartmentScheme_Id, chkIssueOngoing.Checked, true, FromDate, TillDate, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            List<Project_Issue_Analysis> obj_Project_Issue_Analysis_Li = new List<Project_Issue_Analysis>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = ds.Tables[0].Rows[i]["ProjectIssue_Name"].ToString();
                try
                {
                    obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects_With_Issue"].ToString());
                }
                catch
                {
                    obj_Project_Issue_Analysis.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Isues"].ToString());
                }
                catch
                {
                    obj_Project_Issue_Analysis.Total_Issues = 0;
                }
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }

            if (obj_Project_Issue_Analysis_Li.Count == 4)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 3)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 2)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 1)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            hf_Issue_Analysis.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Issue_Analysis_Li);
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();

            grdIssueReported.FooterRow.Cells[2].Text = "Total: ";
            (grdIssueReported.FooterRow.FindControl("lnkTotalIssuesF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Isues)", "").ToString();
        }
        else
        {
            hf_Issue_Analysis.Value = "[]";
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }

        ds = (new DataLayer()).get_DPR_BID_Process_Status_Dashboard_CNDS(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, Jurisdiction_In);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTotal_Projects_New.Text = ds.Tables[0].Rows[0]["TotalDPR"].ToString();
            lnk_New_Sanction_Cost.Text = ds.Tables[0].Rows[0]["TotalDPRCost"].ToString();

            sp_Nodal.InnerHtml = ds.Tables[0].Rows[0]["TotalDPR"].ToString();
            sp_NodalCost.InnerHtml = ds.Tables[0].Rows[0]["TotalDPRCost"].ToString();

            sp_GO.InnerHtml = ds.Tables[0].Rows[0]["GO_Done"].ToString();
            sp_GOCost.InnerHtml = ds.Tables[0].Rows[0]["GO_DoneCost"].ToString();

            sp_GO_Pending.InnerHtml = ds.Tables[0].Rows[0]["GO_Pending"].ToString();
            sp_GO_PendingCost.InnerHtml = ds.Tables[0].Rows[0]["GO_PendingCost"].ToString();

            sp_DPRPrepared.InnerHtml = ds.Tables[0].Rows[0]["DPR_To_Be_Prepared"].ToString();
            sp_DPRPreparedCost.InnerHtml = ds.Tables[0].Rows[0]["DPR_To_Be_PreparedCost"].ToString();

            sp_DPR_Send_Client.InnerHtml = ds.Tables[0].Rows[0]["DPR_Prepared_And_Send_Client"].ToString();
            sp_DPR_Send_ClientCost.InnerHtml = ds.Tables[0].Rows[0]["DPR_Prepared_And_Send_ClientCost"].ToString();

            sp_DPR_Approved_Client.InnerHtml = ds.Tables[0].Rows[0]["DPR_Approved_By_Client"].ToString();
            sp_DPR_Approved_ClientCost.InnerHtml = ds.Tables[0].Rows[0]["DPR_Approved_By_ClientCost"].ToString();

            sp_TS_Done.InnerHtml = ds.Tables[0].Rows[0]["TS_Done_HQ"].ToString();
            sp_TS_DoneCost.InnerHtml = ds.Tables[0].Rows[0]["TS_Done_HQCost"].ToString();

            sp_TS_Pending.InnerHtml = ds.Tables[0].Rows[0]["TS_Pending_HQ"].ToString();
            sp_TS_PendingCost.InnerHtml = ds.Tables[0].Rows[0]["TS_Pending_HQCost"].ToString();

            sp_DPR_Under_Prepration.InnerHtml = ds.Tables[0].Rows[0]["DPR_Under_Preparation"].ToString();
            sp_DPR_Under_PreprationCost.InnerHtml = ds.Tables[0].Rows[0]["DPR_Under_PreparationCost"].ToString();

            sp_NIT_Floated.InnerHtml = ds.Tables[0].Rows[0]["NIT_Floated"].ToString();
            sp_NIT_FloatedCost.InnerHtml = ds.Tables[0].Rows[0]["NIT_FloatedCost"].ToString();

            sp_Bid_Opened_Technical.InnerHtml = ds.Tables[0].Rows[0]["Technical_Bid_Opened"].ToString();
            sp_Bid_Opened_TechnicalCost.InnerHtml = ds.Tables[0].Rows[0]["Technical_Bid_OpenedCost"].ToString();

            sp_Bid_To_Be_Opened_Technical.InnerHtml = ds.Tables[0].Rows[0]["Technical_Bid_To_Opened"].ToString();
            sp_Bid_To_Be_Opened_TechnicalCost.InnerHtml = ds.Tables[0].Rows[0]["Technical_Bid_To_OpenedCost"].ToString();

            sp_Bid_Opened_Financial.InnerHtml = ds.Tables[0].Rows[0]["Financial_Bid_Opened"].ToString();
            sp_Bid_Opened_FinancialCost.InnerHtml = ds.Tables[0].Rows[0]["Financial_Bid_OpenedCost"].ToString();

            sp_Bid_To_Be_Opened_Financial.InnerHtml = ds.Tables[0].Rows[0]["Financial_Bid_To_Opened"].ToString();
            sp_Bid_To_Be_Opened_FinancialCost.InnerHtml = ds.Tables[0].Rows[0]["Financial_Bid_To_OpenedCost"].ToString();

            sp_LOA_Issued.InnerHtml = ds.Tables[0].Rows[0]["LOA_Issued"].ToString();
            sp_LOA_IssuedCost.InnerHtml = ds.Tables[0].Rows[0]["LOA_IssuedCost"].ToString();

            sp_Work_Started.InnerHtml = ds.Tables[0].Rows[0]["Work_Started"].ToString();
            sp_Work_StartedCost.InnerHtml = ds.Tables[0].Rows[0]["Work_StartedCost"].ToString();
        }
        else
        {
            sp_Nodal.InnerHtml = "0";
            sp_NodalCost.InnerHtml = "0";

            sp_GO.InnerHtml = "0";
            sp_GOCost.InnerHtml = "0";

            sp_GO_Pending.InnerHtml = "0";
            sp_GO_PendingCost.InnerHtml = "0";

            sp_DPRPrepared.InnerHtml = "0";
            sp_DPRPreparedCost.InnerHtml = "0";

            sp_DPR_Send_Client.InnerHtml = "0";
            sp_DPR_Send_ClientCost.InnerHtml = "0";

            sp_DPR_Approved_Client.InnerHtml = "0";
            sp_DPR_Approved_ClientCost.InnerHtml = "0";

            sp_TS_Done.InnerHtml = "0";
            sp_TS_DoneCost.InnerHtml = "0";

            sp_TS_Pending.InnerHtml = "0";
            sp_TS_PendingCost.InnerHtml = "0";

            sp_DPR_Under_Prepration.InnerHtml = "0";
            sp_DPR_Under_PreprationCost.InnerHtml = "0";

            sp_NIT_Floated.InnerHtml = "0";
            sp_NIT_FloatedCost.InnerHtml = "0";

            sp_Bid_Opened_Technical.InnerHtml = "0";
            sp_Bid_Opened_TechnicalCost.InnerHtml = "0";

            sp_Bid_To_Be_Opened_Technical.InnerHtml = "0";
            sp_Bid_To_Be_Opened_TechnicalCost.InnerHtml = "0";

            sp_Bid_Opened_Financial.InnerHtml = "0";
            sp_Bid_Opened_FinancialCost.InnerHtml = "0";

            sp_Bid_To_Be_Opened_Financial.InnerHtml = "0";
            sp_Bid_To_Be_Opened_FinancialCost.InnerHtml = "0";

            sp_LOA_Issued.InnerHtml = "0";
            sp_LOA_IssuedCost.InnerHtml = "0";

            sp_Work_Started.InnerHtml = "0";
            sp_Work_StartedCost.InnerHtml = "0";
        }

        get_tbl_FinancialTarget_DashboardView(Zone_Id, Circle_Id, Division_Id);
        get_tbl_PhysicalTargetDashboardView(Zone_Id, Circle_Id, Division_Id);

        get_Project_Work_Field_Visit_Completed_AddedBy(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, Jurisdiction_In);
    }

    protected void get_Project_Work_Field_Visit_Completed_AddedBy(string Scheme_Id, int Zone_Id, int Circle_Id, int Division_Id, int District_Id, int ULB_Id, string NodalDepartment_Id, string NodalDepartmentScheme_Id, int Jurisdiction_In)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Work_Field_Visit_Completed_AddedBy(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 0, false, NodalDepartment_Id, NodalDepartmentScheme_Id, Jurisdiction_In);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdSiteVisit2.DataSource = ds.Tables[0];
            grdSiteVisit2.DataBind();
        }
        else
        {
            grdSiteVisit2.DataSource = null;
            grdSiteVisit2.DataBind();
        }
    }
    protected void grdPMISUpdation_PreRender(object sender, EventArgs e)
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
    protected void grdIssueReported_PreRender(object sender, EventArgs e)
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
        get_PMIS_Dashboard_View();
    }

    protected void grdOverAllStatus_PreRender(object sender, EventArgs e)
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

    protected void lnkTotalProjects_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        Type_Id = "0";

        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=-1");
    }

    protected void lnkWithInUP_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        string NodalDepartment_Id = "";
        Type_Id = "0";
        int Jurisdiction_In = 1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In);
    }

    protected void lnkOutsideUP_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        string NodalDepartment_Id = "";
        Type_Id = "0";
        int Jurisdiction_In = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In);
    }

    protected void lnkCompleted_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        string NodalDepartment_Id = "";
        Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed");
    }

    protected void lnkOngoing_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        string NodalDepartment_Id = "";
        Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing");
    }

    protected void grdOverAllStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkTotalProjects = e.Row.FindControl("lnkTotalProjects") as LinkButton;
            LinkButton lnkWithInUP = e.Row.FindControl("lnkWithInUP") as LinkButton;
            LinkButton lnkOutsideUP = e.Row.FindControl("lnkOutsideUP") as LinkButton;
            LinkButton lnkCompleted = e.Row.FindControl("lnkCompleted") as LinkButton;
            LinkButton lnkOngoing = e.Row.FindControl("lnkOngoing") as LinkButton;
            if (e.Row.RowIndex == 0)
            {
                lnkTotalProjects.Text = AllClasses.convert_To_Indian_No_Format(lnkTotalProjects.Text, "Int");
                lnkWithInUP.Text = AllClasses.convert_To_Indian_No_Format(lnkWithInUP.Text, "Int");
                lnkOutsideUP.Text = AllClasses.convert_To_Indian_No_Format(lnkOutsideUP.Text, "Int");
                lnkCompleted.Text = AllClasses.convert_To_Indian_No_Format(lnkCompleted.Text, "Int");
                lnkOngoing.Text = AllClasses.convert_To_Indian_No_Format(lnkOngoing.Text, "Int");
            }
            else
            {
                lnkTotalProjects.Text = AllClasses.convert_To_Indian_No_Format(lnkTotalProjects.Text, "Int");
                lnkWithInUP.Text = AllClasses.convert_To_Indian_No_Format(lnkWithInUP.Text, "Decimal");
                lnkOutsideUP.Text = AllClasses.convert_To_Indian_No_Format(lnkOutsideUP.Text, "Decimal");
                lnkCompleted.Text = AllClasses.convert_To_Indian_No_Format(lnkCompleted.Text, "Decimal");
                lnkOngoing.Text = AllClasses.convert_To_Indian_No_Format(lnkOngoing.Text, "Decimal");
            }
        }
    }

    protected void rbtViewMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtViewMode.SelectedValue == "N")
        {
            ddlDivision.SelectedValue = "0";

            ddlDivision.Enabled = true;
            ddlCircle.Enabled = true;
            ddlZone.Enabled = true;

            ddlZone.SelectedValue = "0";
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();

            ddlZone_SelectedIndexChanged(ddlZone, e);
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
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
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
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
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
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

            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
            {//Circle
                get_tbl_Nodal_Circle_Link(Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()));
            }
            else
            {
                get_Employee();
            }
        }
        else
        {
            ddlZone.SelectedValue = "0";
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
            get_Employee();
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
        }
    }

    protected void lnkTotalIssues_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        try
        {
            Issue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Issue_Id = 0;
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }
    protected void lnkTotal_Projects_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnkTotal_Projects_Ongoing_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
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

        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnkTotal_Projects_Completed_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Sanction_Cost_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Completed_Sanction_Cost_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Total_Release_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Release_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Completed_Release_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Total_Expenditure_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Expenditure_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Completed_Expenditure_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Total_Remaining_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Remaining_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnk_Completed_Remaining_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id + "&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void chkFY_Wise_CheckedChanged(object sender, EventArgs e)
    {
        get_Year_Data();
    }

    private void get_Year_Data()
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("Year_Text", typeof(string));
        DataColumn dc2 = new DataColumn("Year_Value", typeof(string));
        dt.Columns.AddRange(new DataColumn[] { dc1, dc2 });
        DataRow dr = dt.NewRow();
        dr["Year_Text"] = "All Data";
        dr["Year_Value"] = "-|-";
        dt.Rows.Add(dr);
        if (chkFY_Wise.Checked)
        {
            dr = dt.NewRow();
            dr["Year_Text"] = "Before FY 2019-2020";
            dr["Year_Value"] = "-|31/03/2019";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2019-2020";
            dr["Year_Value"] = "31/03/2019|31/03/2020";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2020-2021";
            dr["Year_Value"] = "31/03/2020|31/03/2021";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2021-2022";
            dr["Year_Value"] = "31/03/2021|31/03/2022";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2022-2023";
            dr["Year_Value"] = "31/03/2022|31/03/2023";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2023-2024";
            dr["Year_Value"] = "31/03/2023|31/03/2024";
            dt.Rows.Add(dr);
        }
        else
        {
            dr = dt.NewRow();
            dr["Year_Text"] = "Before 2020";
            dr["Year_Value"] = "-|31/12/2019";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2020";
            dr["Year_Value"] = "01/01/2020|31/12/2020";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2021";
            dr["Year_Value"] = "01/01/2021|31/12/2021";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2022";
            dr["Year_Value"] = "01/01/2022|31/12/2022";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2023";
            dr["Year_Value"] = "01/01/2023|31/12/2023";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2024";
            dr["Year_Value"] = "01/01/2024|31/12/2024";
            dt.Rows.Add(dr);
        }

        ddlSanctionYear.DataTextField = "Year_Text";
        ddlSanctionYear.DataValueField = "Year_Value";
        ddlSanctionYear.DataSource = dt;
        ddlSanctionYear.DataBind();
    }

    protected void rbtGraphFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_PMIS_Dashboard_View();
    }
    protected void lblUpdation0_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=0&ProgressType=" + ProgressType + "&NodalDept_Id=" + NodalDepartment_Id + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString() + "&Jurisdiction_In= " + Jurisdiction_In);
    }
    protected void lblUpdation50_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=50&ProgressType=" + ProgressType + "&NodalDept_Id=" + NodalDepartment_Id + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString() + "&Jurisdiction_In= " + Jurisdiction_In);
    }


    protected void lblUpdation100_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=11&ProgressType=" + ProgressType + "&NodalDept_Id=" + NodalDepartment_Id + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString() + "&Jurisdiction_In= " + Jurisdiction_In);
    }
    protected void lblUpdation25_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=25&ProgressType=" + ProgressType + "&NodalDept_Id=" + NodalDepartment_Id + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString() + "&Jurisdiction_In= " + Jurisdiction_In);
    }

    protected void lblUpdation75_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=75&ProgressType=" + ProgressType + "&NodalDept_Id=" + NodalDepartment_Id + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString() + "&Jurisdiction_In= " + Jurisdiction_In);
    }

    protected void lblUpdation99_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=99&ProgressType=" + ProgressType + "&NodalDept_Id=" + NodalDepartment_Id + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString() + "&Jurisdiction_In= " + Jurisdiction_In);
    }

    protected void btnMIS_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS.aspx");
    }

    protected void chkSelectAllNodal_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem listItem in ddlNodalDept.Items)
        {
            listItem.Selected = chkSelectAllNodal.Checked;
        }
        ddlNodalDept_SelectedIndexChanged(ddlNodalDept, e);
    }

    protected void btnInfo25_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo25 = sender as ImageButton;
        GridViewRow gr = btnInfo25.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 25, ProgressType, FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lblRemainingAmount.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            mp1.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
            lblRemainingAmount.Text = "0";
        }
    }

    protected void btnInfo50_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo25 = sender as ImageButton;
        GridViewRow gr = btnInfo25.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 50, ProgressType, FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lblRemainingAmount.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            mp1.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
            lblRemainingAmount.Text = "0";
        }
    }

    protected void btnInfo75_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo25 = sender as ImageButton;
        GridViewRow gr = btnInfo25.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 75, ProgressType, FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lblRemainingAmount.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            mp1.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
            lblRemainingAmount.Text = "0";
        }
    }

    protected void btnInfo99_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo25 = sender as ImageButton;
        GridViewRow gr = btnInfo25.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 99, ProgressType, FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lblRemainingAmount.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            mp1.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
            lblRemainingAmount.Text = "0";
        }
    }

    protected void btnInfo100_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo25 = sender as ImageButton;
        GridViewRow gr = btnInfo25.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 11, ProgressType, FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lblRemainingAmount.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            mp1.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
            lblRemainingAmount.Text = "0";
        }
    }

    protected void chkJurisdictionFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkJurisdictionFilter.Items[2].Selected)
        {
            rbtViewMode.SelectedValue = "N";
            rbtViewMode_SelectedIndexChanged(rbtViewMode, e);
            foreach (ListItem listItem in ddlNodalDept.Items)
            {
                if (listItem.Value == "17205")
                {
                    listItem.Selected = true;
                }
            }
            btnSearch_Click(btnSearch, e);
        }
    }

    protected void btnInfo0_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo0 = sender as ImageButton;
        GridViewRow gr = btnInfo0.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, FromDate, TillDate, NodalDepartmentScheme_Id, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            lblRemainingAmount.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString(), "Decimal");
            mp1.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
            lblRemainingAmount.Text = "0";
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
            get_tbl_FundingPatternNodal(NodalDepartment_Id);
            int Zone_Id = 0;
            int Circle_Id = 0;
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
            get_tbl_Division_Mapped_Nodel(Zone_Id, Circle_Id, NodalDepartment_Id);
        }
        else
        {
            ddlNodalDeptScheme.Items.Clear();
            ddlScheme.Items.Clear();
        }
    }
    protected void lnkNoDelay_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        if (chkDelayOngoing.Checked)
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=4&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
        else
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=4&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnkdelayWithoutIssue_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        if (chkDelayOngoing.Checked)
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=5&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
        else
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=5&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnkdelayWithIssue_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        if (chkDelayOngoing.Checked)
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=6&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
        else
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=6&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }
    protected void chkDelayOngoing_CheckedChanged(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Delay_Analysis(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, NodalDepartmentScheme_Id, chkDelayOngoing.Checked, FromDate, TillDate, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkNoDelay.Text = ds.Tables[0].Rows[0]["No_Delay"].ToString();
            //lnkdelayWithIssue.Text = ds.Tables[0].Rows[0]["Delay_With_Issue"].ToString();
            //lnkdelayWithoutIssue.Text = ds.Tables[0].Rows[0]["Delay_Without_Issue"].ToString();
            lnkDelay.Text = ds.Tables[0].Rows[0]["Delay"].ToString();

            Project_Delay_Analysis obj_Project_Delay_Analysis = new Project_Delay_Analysis();
            try
            {
                obj_Project_Delay_Analysis.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Projects"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Total_Projects = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_No_Delay = Convert.ToInt32(ds.Tables[0].Rows[0]["No_Delay"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_No_Delay = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_Issue = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay_With_Issue"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_Issue = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_No_Issue = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay_Without_Issue"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_Delay_With_No_Issue = 0;
            }
            try
            {
                obj_Project_Delay_Analysis.Projects_Delay = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay"].ToString());
            }
            catch
            {
                obj_Project_Delay_Analysis.Projects_Delay = 0;
            }
            hf_Delay_Analysis.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Delay_Analysis);
        }
        else
        {
            hf_Delay_Analysis.Value = "[]";
            lnkNoDelay.Text = "0";
            //lnkdelayWithIssue.Text = "0";
            //lnkdelayWithoutIssue.Text = "0";
            lnkDelay.Text = "0";
        }
    }

    protected void grdYearWiseDataAnalysis_PreRender(object sender, EventArgs e)
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

    protected void grdYearWiseDataAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[3].Text, "Decimal");
            e.Row.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[4].Text, "Decimal");
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text, "Decimal");
            e.Row.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[6].Text, "Decimal");
        }
    }

    protected void lnkYear_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTotalYear_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = new DataLayer().get_PMIS_Dashboard_CNDS_Financial_Year_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, "", "", "", NodalDepartmentScheme_Id, "", chkFY_Wise.Checked, _Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            divYearWiseBreakup.Visible = true;
            grdYearWiseDataAnalysis.DataSource = ds.Tables[0];
            grdYearWiseDataAnalysis.DataBind();
        }
        else
        {
            divYearWiseBreakup.Visible = false;
            grdYearWiseDataAnalysis.DataSource = null;
            grdYearWiseDataAnalysis.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkOngoingYear_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = new DataLayer().get_PMIS_Dashboard_CNDS_Financial_Year_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, "", "", "", NodalDepartmentScheme_Id, "OnGoing", chkFY_Wise.Checked, _Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            divYearWiseBreakup.Visible = true;
            grdYearWiseDataAnalysis.DataSource = ds.Tables[0];
            grdYearWiseDataAnalysis.DataBind();
        }
        else
        {
            divYearWiseBreakup.Visible = false;
            grdYearWiseDataAnalysis.DataSource = null;
            grdYearWiseDataAnalysis.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkcompletedYear_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        ds = new DataLayer().get_PMIS_Dashboard_CNDS_Financial_Year_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, "", "", "", NodalDepartmentScheme_Id, "Completed", chkFY_Wise.Checked, _Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            divYearWiseBreakup.Visible = true;
            grdYearWiseDataAnalysis.DataSource = ds.Tables[0];
            grdYearWiseDataAnalysis.DataBind();
        }
        else
        {
            divYearWiseBreakup.Visible = false;
            grdYearWiseDataAnalysis.DataSource = null;
            grdYearWiseDataAnalysis.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void chkIssueOngoing_CheckedChanged(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_CNDS(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, NodalDepartmentScheme_Id, chkIssueOngoing.Checked, true, FromDate, TillDate, _Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            List<Project_Issue_Analysis> obj_Project_Issue_Analysis_Li = new List<Project_Issue_Analysis>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = ds.Tables[0].Rows[i]["ProjectIssue_Name"].ToString();
                try
                {
                    obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects_With_Issue"].ToString());
                }
                catch
                {
                    obj_Project_Issue_Analysis.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Isues"].ToString());
                }
                catch
                {
                    obj_Project_Issue_Analysis.Total_Issues = 0;
                }
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }

            if (obj_Project_Issue_Analysis_Li.Count == 4)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 3)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 2)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 1)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            hf_Issue_Analysis.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Issue_Analysis_Li);
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();

            grdIssueReported.FooterRow.Cells[2].Text = "Total: ";
            grdIssueReported.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Isues)", "").ToString();
        }
        else
        {
            hf_Issue_Analysis.Value = "[]";
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
    }

    protected void chkSelectAllNodalScheme_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem listItem in ddlNodalDeptScheme.Items)
        {
            listItem.Selected = chkSelectAllNodalScheme.Checked;
        }
    }

    protected void lnkDelay_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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

        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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
        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        if (chkDelayOngoing.Checked)
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=7&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
        else
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&LD=7&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
    }

    protected void lnkNewNodal_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TotalDPR);
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

    protected void lnkNewGO_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.GO_Done);
    }
    protected void lnkNewGOPending_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.GO_Pending);
    }
    protected void lnkNewDPRPrepared_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_To_Be_Prepared);
    }

    protected void lnkNewDPRSendClient_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Prepared_And_Send_Client);
    }

    protected void lnkNewDPR_Approved_Client_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Approved_By_Client);
    }

    protected void lnkNewTS_Done_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TS_Approved_HQ);
    }

    protected void lnkNewTS_Pending_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.TS_Not_Approved_HQ);
    }

    protected void lnkNewDPR_Under_Prepration_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.DPR_Under_Preparation);
    }

    protected void lnkNewNIT_Floated_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.NIT_Issued);
    }

    protected void lnkNewBid_Opened_Technical_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Technical_Bid_Opened);
    }

    protected void lnkNewBid_To_Be_Opened_Technical_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Technical_Bid_To_Be_Opened);
    }

    protected void lnkNewBid_Opened_Financial_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Financial_Bid_Opened);
    }

    protected void lnkNewBid_To_Be_Opened_Financial_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Financial_Bid_To_Be_Opened);
    }

    protected void lnkNewLOA_Issued_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.LOA_Issued);
    }

    protected void lnkNewWork_Started_Click(object sender, EventArgs e)
    {
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        get_tbl_ProjectWorkDPR(District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, Scheme_Id, 0, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, DPR_Status.Work_Issued);
    }

    protected void get_tbl_ProjectWorkDPR(int District_Id, int Zone_Id, int Circle_Id, int Division_Id, int ULB_Id, string Scheme_Id, int Tranche_Id, string NodalDepartment_Id, string NodalDepartmentScheme_Id, int _Scheme_Id, DPR_Status _DPR_Status)
    {
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
        }
        DataSet ds1 = new DataSet();
        ds1 = (new DataLayer()).get_tbl_ProjectWorkDPR_CNDS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, 0, 0, Tranche_Id, NodalDepartment_Id, NodalDepartmentScheme_Id, _Scheme_Id, _DPR_Status, Jurisdiction_In);
        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        {
            Session["DPR_Dash_Data"] = ds1;
            Response.Redirect("Report_DPR_BPM_Report_CNDS_View.aspx");
        }
        else
        {
            Session["DPR_Dash_Data"] = null;
        }
    }

    protected void lnkNewWorks_Click(object sender, EventArgs e)
    {
        divNewProjects.Visible = true;
        divYearWiseBreakup.Visible = false;
    }

    protected void lnkTotal_Projects_New_Click(object sender, EventArgs e)
    {
        divNewProjects.Visible = true;
        divYearWiseBreakup.Visible = false;
    }

    protected void lnk_New_Sanction_Cost_Click(object sender, EventArgs e)
    {
        divNewProjects.Visible = true;
        divYearWiseBreakup.Visible = false;
    }

    protected void grdPhysical_PreRender(object sender, EventArgs e)
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
    private void get_tbl_FinancialTarget_DashboardView(int Zone_Id, int Circle_Id, int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FinancialTarget_DashboardView(Zone_Id, Circle_Id, Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            decimal YearTarget = 0;
            decimal YearAchivment = 0;
            decimal YearAchivmentPer = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_YearTarget"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_YearAchivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["YearAchivment_Per"] = YearAchivmentPer.ToString();

                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q1Target"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q1Achivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["Q1Achivment_Per"] = YearAchivmentPer.ToString();

                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q2Target"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q2Achivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["Q2Achivment_Per"] = YearAchivmentPer.ToString();

                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q3Target"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q3Achivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["Q3Achivment_Per"] = YearAchivmentPer.ToString();

                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q4Target"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionFinancialTarget_Q4Achivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["Q4Achivment_Per"] = YearAchivmentPer.ToString();
            }
            grdFinancial.DataSource = ds.Tables[0];
            grdFinancial.DataBind();

            grdFinancial.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_YearTarget)", "").ToString();
            grdFinancial.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_YearAchivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdFinancial.FooterRow.Cells[4].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdFinancial.FooterRow.Cells[5].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdFinancial.FooterRow.Cells[6].Text = YearAchivmentPer.ToString();

            grdFinancial.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q1Target)", "").ToString();
            grdFinancial.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q1Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdFinancial.FooterRow.Cells[7].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdFinancial.FooterRow.Cells[8].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdFinancial.FooterRow.Cells[9].Text = YearAchivmentPer.ToString();

            grdFinancial.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q2Target)", "").ToString();
            grdFinancial.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q2Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdFinancial.FooterRow.Cells[10].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdFinancial.FooterRow.Cells[11].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdFinancial.FooterRow.Cells[12].Text = YearAchivmentPer.ToString();

            grdFinancial.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q3Target)", "").ToString();
            grdFinancial.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q3Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdFinancial.FooterRow.Cells[13].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdFinancial.FooterRow.Cells[14].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdFinancial.FooterRow.Cells[15].Text = YearAchivmentPer.ToString();

            grdFinancial.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q4Target)", "").ToString();
            grdFinancial.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q4Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdFinancial.FooterRow.Cells[16].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdFinancial.FooterRow.Cells[17].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdFinancial.FooterRow.Cells[18].Text = YearAchivmentPer.ToString();
        }
        else
        {
            grdFinancial.DataSource = null;
            grdFinancial.DataBind();
        }
    }
    private void get_tbl_PhysicalTargetDashboardView(int Zone_Id, int Circle_Id, int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PhysicalTargetDashboardView(Zone_Id, Circle_Id, Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            decimal YearTarget = 0;
            decimal YearAchivment = 0;
            decimal YearAchivmentPer = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionPhysicalTarget_PhysicalCompletionTarget"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionPhysicalTarget_PhysicalCompletionAchivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }

                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["PhysicalCompletionAchivment_Per"] = YearAchivmentPer.ToString();


                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionPhysicalTarget_PhysicalHandoverTarget"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionPhysicalTarget_PhysicalHandoverAchivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["PhysicalHandoverAchivment_Per"] = YearAchivmentPer.ToString();

                YearTarget = 0;
                YearAchivment = 0;
                try
                {
                    YearTarget = decimal.Parse(ds.Tables[0].Rows[i]["DivisionPhysicalTarget_FinancialHandoverTarget"].ToString());
                }
                catch
                {
                    YearTarget = 0;
                }
                try
                {
                    YearAchivment = decimal.Parse(ds.Tables[0].Rows[i]["DivisionPhysicalTarget_FinancialHandoverAchivment"].ToString());
                }
                catch
                {
                    YearAchivment = 0;
                }
                YearAchivmentPer = 0;
                if (YearTarget == 0)
                {
                    YearAchivmentPer = 0;
                }
                else
                {
                    YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
                }
                ds.Tables[0].Rows[i]["FinancialHandoverAchivment_Per"] = YearAchivmentPer.ToString();
            }

            grdPhysical.DataSource = ds.Tables[0];
            grdPhysical.DataBind();

            grdPhysical.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalCompletionTarget)", "").ToString();
            grdPhysical.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalCompletionAchivment)", "").ToString();

            YearTarget = 0;
            YearAchivment = 0;
            YearAchivmentPer = 0;

            try
            {
                YearTarget = decimal.Parse(grdPhysical.FooterRow.Cells[4].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPhysical.FooterRow.Cells[5].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdPhysical.FooterRow.Cells[6].Text = YearAchivmentPer.ToString();

            grdPhysical.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalHandoverTarget)", "").ToString();
            grdPhysical.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalHandoverAchivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPhysical.FooterRow.Cells[7].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPhysical.FooterRow.Cells[8].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdPhysical.FooterRow.Cells[9].Text = YearAchivmentPer.ToString();

            grdPhysical.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_FinancialHandoverTarget)", "").ToString();
            grdPhysical.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_FinancialHandoverAchivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPhysical.FooterRow.Cells[10].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPhysical.FooterRow.Cells[11].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 0, MidpointRounding.AwayFromZero);
            }
            grdPhysical.FooterRow.Cells[12].Text = YearAchivmentPer.ToString();
        }
        else
        {
            grdPhysical.DataSource = null;
            grdPhysical.DataBind();
        }
    }
    protected void grdPhysical_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[2].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[3].Text = Session["Default_Division"].ToString();

            e.Row.Cells[4].BackColor = Color.FromArgb(255, 153, 51);
            e.Row.Cells[4].ForeColor = Color.White;
            e.Row.Cells[5].BackColor = Color.FromArgb(255, 153, 51);
            e.Row.Cells[5].ForeColor = Color.White;
            e.Row.Cells[6].BackColor = Color.FromArgb(255, 153, 51);
            e.Row.Cells[6].ForeColor = Color.White;

            e.Row.Cells[10].BackColor = Color.Green;
            e.Row.Cells[10].ForeColor = Color.White;
            e.Row.Cells[11].BackColor = Color.Green;
            e.Row.Cells[11].ForeColor = Color.White;
            e.Row.Cells[12].BackColor = Color.Green;
            e.Row.Cells[12].ForeColor = Color.White;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void grdFinancial_PreRender(object sender, EventArgs e)
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

    protected void grdFinancial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[2].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[3].Text = Session["Default_Division"].ToString();

            e.Row.Cells[4].BackColor = ColorTranslator.FromHtml("#f15a25");
            e.Row.Cells[4].ForeColor = Color.White;
            e.Row.Cells[5].BackColor = ColorTranslator.FromHtml("#f15a25");
            e.Row.Cells[5].ForeColor = Color.White;
            e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#f15a25");
            e.Row.Cells[6].ForeColor = Color.White;

            e.Row.Cells[7].BackColor = ColorTranslator.FromHtml("#39beb9");
            e.Row.Cells[7].ForeColor = Color.White;
            e.Row.Cells[8].BackColor = ColorTranslator.FromHtml("#39beb9");
            e.Row.Cells[8].ForeColor = Color.White;
            e.Row.Cells[9].BackColor = ColorTranslator.FromHtml("#39beb9");
            e.Row.Cells[9].ForeColor = Color.White;

            e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#f2ad22");
            e.Row.Cells[10].ForeColor = Color.White;
            e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#f2ad22");
            e.Row.Cells[11].ForeColor = Color.White;
            e.Row.Cells[12].BackColor = ColorTranslator.FromHtml("#f2ad22");
            e.Row.Cells[12].ForeColor = Color.White;

            e.Row.Cells[13].BackColor = ColorTranslator.FromHtml("#bcd63d");
            e.Row.Cells[13].ForeColor = Color.White;
            e.Row.Cells[14].BackColor = ColorTranslator.FromHtml("#bcd63d");
            e.Row.Cells[14].ForeColor = Color.White;
            e.Row.Cells[15].BackColor = ColorTranslator.FromHtml("#bcd63d");
            e.Row.Cells[15].ForeColor = Color.White;

            e.Row.Cells[16].BackColor = ColorTranslator.FromHtml("#5360cf");
            e.Row.Cells[16].ForeColor = Color.White;
            e.Row.Cells[17].BackColor = ColorTranslator.FromHtml("#5360cf");
            e.Row.Cells[17].ForeColor = Color.White;
            e.Row.Cells[18].BackColor = ColorTranslator.FromHtml("#5360cf");
            e.Row.Cells[18].ForeColor = Color.White;
        }
    }

    protected void grdNodalDept_PreRender(object sender, EventArgs e)
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

    protected void lblNodalDept_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int NodalDept_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        NodalDept_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        string NodalDept_Name = (gr.FindControl("lblNodalDept") as LinkButton).Text.Trim();
        string Type_Id = "0";
        Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=");
    }

    protected void lblTotalProjects_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int NodalDept_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        NodalDept_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        string NodalDept_Name = (gr.FindControl("lblNodalDept") as LinkButton).Text.Trim();
        string Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=");
    }

    protected void lblTotalProjectsF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=");
    }

    protected void lblCompleted_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        int NodalDept_Id = 0;
        try
        {
            NodalDept_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            NodalDept_Id = 0;
        }
        string Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=&Month=0&Year=0&NodalDept_Id="+NodalDept_Id+"&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed");
    }

    protected void lblCompletedF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=&Month=0&Year=0&NodalDept_Id=&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=Completed");
    }

    protected void lblOngoing_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        int NodalDept_Id = 0;
        try
        {
            NodalDept_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            NodalDept_Id = 0;
        }
        string Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id + "&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing");
    }

    protected void lblOngoingF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string Type_Id = "0";
        int Jurisdiction_In = -1;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=&Month=0&Year=0&NodalDept_Id=&Issue_Id=0&Dep_Id=0&Jurisdiction_In=" + Jurisdiction_In + "&Type=OnGoing");
    }

    protected void lnkTotalIssuesF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        try
        {
            Issue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Issue_Id = 0;
        }
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
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
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
        Type_Id = "0";
        int Jurisdiction_In = -1;
        if (chkJurisdictionFilter.Items[0].Selected && chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = -1;
        }
        else if (chkJurisdictionFilter.Items[0].Selected)
        {
            Jurisdiction_In = 1;
        }
        else if (chkJurisdictionFilter.Items[1].Selected)
        {
            Jurisdiction_In = 0;
        }
        else
        {
            Jurisdiction_In = -1;
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

        string[] sanction_Year1 = null;
        string[] sanction_Year2 = null;

        foreach (ListItem listItem in ddlSanctionYear.Items)
        {
            if (listItem.Selected)
            {
                if (sanction_Year1 == null)
                {
                    sanction_Year1 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                }
                sanction_Year2 = listItem.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year1 != null && sanction_Year1.Length == 2)
        {
            FromDate = sanction_Year1[0].Replace("-", "");
        }
        if (sanction_Year2 != null && sanction_Year2.Length == 2)
        {
            TillDate = sanction_Year2[1].Replace("-", "");
        }
        if (chkIssueOngoing.Checked)
        {
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=-1&Dep_Id=" + Dependency_Id.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&Type=OnGoing&_Schme_Id=" + _Scheme_Id.ToString());
        }
        else
        {
            Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDepartment_Id + "&Issue_Id=-1&Dep_Id=" + Dependency_Id.ToString() + "&NodalDepartmentScheme_Id=" + NodalDepartmentScheme_Id.ToString() + "&Jurisdiction_In=" + Jurisdiction_In.ToString() + "&FromDate=" + FromDate + "&TillDate=" + TillDate + "&_Schme_Id=" + _Scheme_Id.ToString());
        }
    }
    protected void grdSiteVisit2_PreRender(object sender, EventArgs e)
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
    protected void lnkDepartmentWise_Click(object sender, EventArgs e)
    {
        if (divNodalDeptWise.Visible)
        {
            divNodalDeptWise.Visible = false;
        }
        else
        {
            divNodalDeptWise.Visible = true;
        }
    }
}