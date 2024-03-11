using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_PMIS : System.Web.UI.Page
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
            get_tbl_Project();
            get_tbl_Zone();
            get_M_Jurisdiction();
            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                ddlScheme.SelectedValue = obj_SearchStorage.Scheme_Id.ToString();
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
            if (Request.QueryString.Count > 0)
            {
                int Scheme_Id = 0;
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                }
                catch
                {
                    Scheme_Id = 0;
                }
                if (Scheme_Id > 0)
                {
                    ddlScheme.SelectedValue = Scheme_Id.ToString();
                }
            }
            get_tbl_ProjectType(Convert.ToInt32(ddlScheme.SelectedValue));
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
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divNodalDeptWise.Visible = true;
                divProjectTypeWise.Visible = false;
            }
            else if (Client == "ULB")
            {
                divNodalDeptWise.Visible = false;
                divProjectTypeWise.Visible = false;
            }
            else
            {
                divNodalDeptWise.Visible = false;
                divProjectTypeWise.Visible = true;
            }
            load_dashboard();
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
                ddlScheme.SelectedIndex = 0;
            }

            if (Request.QueryString.Count > 0)
            {
                int Scheme_Id = 0;
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
                    ddlScheme.SelectedValue = Scheme_Id.ToString();
                }
                catch
                {
                    ddlScheme.SelectedIndex = 0;
                }
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

    private void get_tbl_ProjectType(int Scheme_Id)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_ProjectType(Scheme_Id, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_ProjectType(Scheme_Id, Convert.ToInt32(Session["Person_Id"].ToString()));
        }

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProjectType, "ProjectType_Name", "ProjectType_Id");
        }
        else
        {
            ddlProjectType.Items.Clear();
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
    private void load_dashboard()
    {
        SearchStorage obj_SearchStorage = new SearchStorage();
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;

        Session["SearchStorage"] = obj_SearchStorage;
        get_PMIS_Dashboard_View();
    }
    protected void get_PMIS_Dashboard_View()
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkBG.Text = ds.Tables[0].Rows[0]["BG"].ToString();
            lnkPS.Text = ds.Tables[0].Rows[0]["PS"].ToString();
            lnkMA.Text = ds.Tables[0].Rows[0]["MA"].ToString();
            lnkCB.Text = ds.Tables[0].Rows[0]["Agreement"].ToString();
            lnkTotalProjects.Text = ds.Tables[0].Rows[0]["Total_Projects"].ToString();
            lnkWater.Text = ds.Tables[0].Rows[0]["Water_Supply"].ToString();
            lnkBuilding.Text = ds.Tables[0].Rows[0]["Building_Works"].ToString();
            lnkDranage.Text = ds.Tables[0].Rows[0]["Drainage"].ToString();
            lnkSolidWaste.Text = ds.Tables[0].Rows[0]["Solid_Waste"].ToString();
            lnkSeptage.Text = ds.Tables[0].Rows[0]["Septage"].ToString();
            lnkSewarage.Text = ds.Tables[0].Rows[0]["Sewerage"].ToString();
            lnkTotalPgg.Text = ds.Tables[0].Rows[0]["Total_Package"].ToString();
            lnkGO1.Text = ds.Tables[0].Rows[0]["GO_1"].ToString();
            lnkGO2.Text = ds.Tables[0].Rows[0]["GO_2"].ToString();
            lnkGO3.Text = ds.Tables[0].Rows[0]["GO_3"].ToString();
            lnkTarget_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTarget_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkExp_C.Text = ds.Tables[0].Rows[0]["Progress_Current"].ToString();
            lnkExp_P.Text = ds.Tables[0].Rows[0]["Progress_Previous"].ToString();
            lnkExp_O.Text = ds.Tables[0].Rows[0]["Progress_All"].ToString();
            lnkCompleted.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoing.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
            lnkLOI.Text = ds.Tables[0].Rows[0]["LOI"].ToString();
            lnkCB_Stamp.Text = ds.Tables[0].Rows[0]["CB_Stamp"].ToString();
            lnkCBFront.Text = ds.Tables[0].Rows[0]["CB_Front"].ToString();
            lnkCB_ScheduleG.Text = ds.Tables[0].Rows[0]["Schedule_G"].ToString();
            lnkTE.Text = ds.Tables[0].Rows[0]["TE"].ToString();
            lnkLD.Text = ds.Tables[0].Rows[0]["LD_Likly"].ToString();
            lnkBondDateDelay.Text = ds.Tables[0].Rows[0]["Delay_Likly1"].ToString();
            lnkBondDateDelayNotExtended.Text = ds.Tables[0].Rows[0]["Delay_Likly2"].ToString();
        }
        else
        {
            lnkCompleted.Text = "0";
            lnkOnGoing.Text = "0";
            lnkBG.Text = "0";
            lnkPS.Text = "0";
            lnkMA.Text = "0";
            lnkCB.Text = "0";
            lnkDranage.Text = "0";
            lnkSolidWaste.Text = "0";
            lnkTotalProjects.Text = "0";
            lnkWater.Text = "0";
            lnkBuilding.Text = "0";
            lnkSeptage.Text = "0";
            lnkSewarage.Text = "0";
            lnkTotalPgg.Text = "0";
            lnkGO1.Text = "0";
            lnkGO2.Text = "0";
            lnkGO3.Text = "0";
            lnkTarget_C.Text = "0";
            lnkTarget_N.Text = "0";
            lnkExp_C.Text = "0";
            lnkExp_P.Text = "0";
            lnkExp_O.Text = "0";
            lnkTE.Text = "0";
            lnkLOI.Text = "0";
            lnkCB_Stamp.Text = "0";
            lnkCBFront.Text = "0";
            lnkCB_ScheduleG.Text = "0";
            lnkLD.Text = "0";
            lnkBondDateDelay.Text = "0";
            lnkBondDateDelayNotExtended.Text = "0";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_Physical_Financial_Slab(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, -1, "", "", "", "");
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
            grdPMISUpdation.DataSource = null;
            grdPMISUpdation.DataBind();
        }

        ds = (new DataLayer()).get_Variation_Document_Uploaded(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkVariationProject.Text = ds.Tables[0].Rows[0]["Total_Projects"].ToString();
            lnkVariationPackage.Text = ds.Tables[0].Rows[0]["Total_Pkg"].ToString();
        }
        else
        {
            lnkVariationProject.Text = "0";
            lnkVariationPackage.Text = "0";
        }

        int ProjectType_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            ProjectType_Id = 0;
        }
        ds = (new DataLayer()).get_Physical_Component_PMIS_Dashboard(Zone_Id, Circle_Id, Division_Id, Scheme_Id, ProjectType_Id, District_Id, ULB_Id, 0, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysicalComponent.DataSource = ds.Tables[0];
            grdPhysicalComponent.DataBind();
        }
        else
        {
            grdPhysicalComponent.DataSource = null;
            grdPhysicalComponent.DataBind();
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_LD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkLDImposed.Text = ds.Tables[0].Rows[0]["LD_Imposed"].ToString() + " / " + ds.Tables[0].Rows[0]["LD_Amount"].ToString();
            lnkLDWithdrawan.Text = ds.Tables[0].Rows[0]["LD_Withdrawan"].ToString() + " / " + ds.Tables[0].Rows[0]["LD_Withdrawan_Amount"].ToString(); ;
        }
        else
        {
            lnkLDImposed.Text = "0 / 0";
            lnkLDWithdrawan.Text = "0 / 0";
        }

        ds = (new DataLayer()).get_Variation_Document_Upload_Details(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkVariationPending.Text = ds.Tables[0].Rows[0]["Variation_NotUploaded"].ToString();
        }
        else
        {
            lnkVariationPending.Text = "0";
        }

        //ds = (new DataLayer()).get_Financial_Closure_Pending(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 180);
        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    lnkFinancialClosure.Text = ds.Tables[0].Rows.Count.ToString();
        //}
        //else
        //{
        //    lnkFinancialClosure.Text = "0";
        //}
        ds = (new DataLayer()).get_PMIS_Dashboard_Nodal_Department_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdNodalDept.DataSource = ds.Tables[0];
            grdNodalDept.DataBind();

            (grdNodalDept.FooterRow.FindControl("lblTotalProjectsF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
            (grdNodalDept.FooterRow.FindControl("lblCompletedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Projects_Completed)", "").ToString();
            (grdNodalDept.FooterRow.FindControl("lblOngoingF") as LinkButton).Text = ds.Tables[0].Compute("sum(Projects_onGoing)", "").ToString();
        }
        else
        {
            grdNodalDept.DataSource = null;
            grdNodalDept.DataBind();
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1);
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
            grdIssueReportedGlobal.DataSource = ds.Tables[0];
            grdIssueReportedGlobal.DataBind();
        }
        else
        {
            hf_Issue_Analysis.Value = "[]";
            grdIssueReportedGlobal.DataSource = null;
            grdIssueReportedGlobal.DataBind();
        }
        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
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
                    if (obj_Project_Filter.Total_Remaining_Amount < 0)
                    {
                        obj_Project_Filter.Total_Remaining_Amount = 0;
                    }
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
        }
    }
    protected void rbtGraphFor_SelectedIndexChanged(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
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
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        load_dashboard();
    }

    protected void lnkTotalProjects_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: All Projects";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }

    protected void lnkWater_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string Type_Id = "26";
        if (Scheme_Id == "1013")
        {
            Type_Id = "26";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "39";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "32";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "45";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "16";
        }
        else
        {
            Type_Id = "26";
        }
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: Water Supply";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }

    protected void lnkSewarage_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string Type_Id = "24";
        if (Scheme_Id == "1013")
        {
            Type_Id = "24";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "43";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "37";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "30";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "18";
        }
        else
        {
            Type_Id = "24";
        }
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: Sewarage";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }

    protected void lnkSeptage_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string Type_Id = "27";
        if (Scheme_Id == "1013")
        {
            Type_Id = "27";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "46";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "40";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "40";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "21";
        }
        else
        {
            Type_Id = "27";
        }
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: Septage";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }
    protected void lnkSolidWaste_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string Type_Id = "25";
        if (Scheme_Id == "1013")
        {
            Type_Id = "25";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "44";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "35";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "31";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "25";
        }
        else
        {
            Type_Id = "25";
        }
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: Solid Waste";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }

    protected void lnkDranage_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string Type_Id = "23";
        if (Scheme_Id == "1013")
        {
            Type_Id = "23";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "42";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "36";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "29";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "17";
        }
        else
        {
            Type_Id = "23";
        }
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: Drainage";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }
    protected void lnkProjects_C_Click(object sender, EventArgs e)
    {

    }

    protected void lnkProjects_N_Click(object sender, EventArgs e)
    {

    }

    protected void lnkGO1_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&GO=1");
    }

    protected void lnkGO2_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&GO=2");
    }

    protected void lnkGO3_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&GO=3");
    }

    protected void lnkTotalPgg_Click(object sender, EventArgs e)
    {

    }

    protected void lnkCB_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=14");
    }

    protected void lnkBG_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=20");
    }

    protected void lnkPS_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=21");
    }

    protected void lnkTarget_C_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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

        DateTime dt = DateTime.Now;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString());
    }

    protected void lnkTarget_N_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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

        DateTime dt = DateTime.Now.AddMonths(1);
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString());
    }

    protected void lnkExp_C_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
        {
            Response.Redirect("DashboardSMD.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
        {
            Response.Redirect("DashboardSMD.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
        {
            Response.Redirect("Dashboard.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
        {
            Response.Redirect("Dashboard.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
        {
            Response.Redirect("Dashboard.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
        }
        else
        {
            Response.Redirect("DashboardDept.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
        }
    }

    protected void lnkExp_P_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        DateTime dt = DateTime.Now.Date.AddMonths(-1);

        string FromDate = "01/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Year.ToString();
        string TillDate = DateTime.DaysInMonth(dt.Year, dt.Month).ToString().PadLeft(2, '0') + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Year.ToString();
        if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
        {
            Response.Redirect("DashboardSMD.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&FromDate=" + FromDate + "&TillDate=" + TillDate);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
        {
            Response.Redirect("DashboardSMD.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&FromDate=" + FromDate + "&TillDate=" + TillDate);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
        {
            Response.Redirect("Dashboard.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&FromDate=" + FromDate + "&TillDate=" + TillDate);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
        {
            Response.Redirect("Dashboard.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&FromDate=" + FromDate + "&TillDate=" + TillDate);
        }
        else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
        {
            Response.Redirect("Dashboard.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&FromDate=" + FromDate + "&TillDate=" + TillDate);
        }
        else
        {
            Response.Redirect("DashboardDept.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&FromDate=" + FromDate + "&TillDate=" + TillDate);
        }
    }

    protected void lnkMA_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=22");
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

    protected void lnkCompleted_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=Completed");
    }

    protected void lnkOnGoing_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=OnGoing");
    }

    protected void lblUpdation10_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=1&ProgressType=" + ProgressType);
    }

    protected void lblUpdation20_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=2&ProgressType=" + ProgressType);
    }

    protected void lblUpdation30_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=3&ProgressType=" + ProgressType);
    }

    protected void lblUpdation40_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=4&ProgressType=" + ProgressType);
    }

    protected void lblUpdation50_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=5&ProgressType=" + ProgressType);
    }

    protected void lblUpdation60_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=6&ProgressType=" + ProgressType);
    }

    protected void lblUpdation70_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=7&ProgressType=" + ProgressType);
    }

    protected void lblUpdation80_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=8&ProgressType=" + ProgressType);
    }

    protected void lblUpdation90_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=9&ProgressType=" + ProgressType);
    }

    protected void lblUpdationMore90_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=10&ProgressType=" + ProgressType);
    }

    protected void lnkVariationProject_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkVariationDocument.aspx?Scheme_Id=" + Scheme_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkVariationPackage_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkVariationDocument.aspx?Scheme_Id=" + Scheme_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkLOI_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=15");
    }

    protected void lnkCB_Stamp_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=16");
    }

    protected void lnkCBFront_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=17");
    }

    protected void lnkCB_ScheduleG_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=18");
    }

    protected void grdPhysicalComponent_PreRender(object sender, EventArgs e)
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

    protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        int ProjectType_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            ProjectType_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Physical_Component_PMIS_Dashboard(Zone_Id, Circle_Id, Division_Id, Scheme_Id, ProjectType_Id, District_Id, ULB_Id, 0, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysicalComponent.DataSource = ds.Tables[0];
            grdPhysicalComponent.DataBind();
        }
        else
        {
            grdPhysicalComponent.DataSource = null;
            grdPhysicalComponent.DataBind();
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_tbl_ProjectType(Convert.ToInt32(ddlScheme.SelectedValue));
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
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }

    protected void lnkLD_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=0");
    }

    protected void lnkBondDateDelay_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=1");
    }

    protected void lnkBondDateDelayNotExtended_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=2");
    }

    protected void lnkTE_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=19");
    }

    protected void lnkLDImposed_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("Report_ProjectWorkPkg_LD_Imposed.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LDWithdraw=0");
    }

    protected void lnkLDWithdrawan_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("Report_ProjectWorkPkg_LD_Imposed.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LDWithdraw=1");
    }

    protected void lnkVariationPending_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=99");
    }

    protected void lblTotalProjectComp_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Component_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
            Component_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Component_Id = 0;
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Component_Id=" + Component_Id);
    }

    protected void lnkFinancialClosure_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("Report_ProjectWork_Financial_Closure.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkComponentName_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int ProjectType_Id = 0;
        int Component_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
            Component_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Component_Id = 0;
        }
        try
        {
            ProjectType_Id = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            ProjectType_Id = 0;
        }
        Response.Redirect("Report_Component_Details.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Component_Id=" + Component_Id + "&ProjectType_Id=" + ProjectType_Id);
    }

    protected void lnkBuilding_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string Type_Id = "22";
        if (Scheme_Id == "1013")
        {
            Type_Id = "22";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "34";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "28";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "41";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "22";
        }
        else
        {
            Type_Id = "22";
        }
        lnkProjectStatusPopup.ToolTip = Type_Id;
        lnkProjectStatusPopup.Text = "Project Status: Building Works";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }

    protected void lnkCompletedP_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "";
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        try
        {
            Type_Id = ToolTip_Data[0].Trim();
        }
        catch
        {
            Type_Id = "0";
        }
        try
        {
            NodalDept_Id = ToolTip_Data[1].Trim();
        }
        catch
        {
            NodalDept_Id = "0";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Type=Completed&NodalDept_Id=" + NodalDept_Id);
    }

    protected void lnkOnGoingP_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "";
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        try
        {
            Type_Id = ToolTip_Data[0].Trim();
        }
        catch
        {
            Type_Id = "0";
        }
        try
        {
            NodalDept_Id = ToolTip_Data[1].Trim();
        }
        catch
        {
            NodalDept_Id = "0";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Type=OnGoing&NodalDept_Id=" + NodalDept_Id);
    }

    protected void lnkTargetP_C_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "";
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        try
        {
            Type_Id = ToolTip_Data[0].Trim();
        }
        catch
        {
            Type_Id = "0";
        }
        try
        {
            NodalDept_Id = ToolTip_Data[1].Trim();
        }
        catch
        {
            NodalDept_Id = "0";
        }
        DateTime dt = DateTime.Now;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString() + "&NodalDept_Id=" + NodalDept_Id);
    }

    protected void lnkTargetP_N_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "";
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        try
        {
            Type_Id = ToolTip_Data[0].Trim();
        }
        catch
        {
            Type_Id = "0";
        }
        try
        {
            NodalDept_Id = ToolTip_Data[1].Trim();
        }
        catch
        {
            NodalDept_Id = "0";
        }
        DateTime dt = DateTime.Now.AddMonths(1);
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString() + "&NodalDept_Id=" + NodalDept_Id);
    }

    protected void lnkProjectStatusPopup_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "";
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        try
        {
            Type_Id = ToolTip_Data[0].Trim();
        }
        catch
        {
            Type_Id = "0";
        }
        try
        {
            NodalDept_Id = ToolTip_Data[1].Trim();
        }
        catch
        {
            NodalDept_Id = "0";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&NodalDept_Id=" + NodalDept_Id);
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        lnkProjectStatusPopup.ToolTip = Type_Id + "|" + NodalDept_Id.ToString();
        lnkProjectStatusPopup.Text = "Project Status: " + NodalDept_Name + " All Projects";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDept_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDept_Id.ToString(), -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        lnkProjectStatusPopup.ToolTip = Type_Id + "|" + NodalDept_Id.ToString();
        lnkProjectStatusPopup.Text = "Project Status: " + NodalDept_Name + " All Projects";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDept_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDept_Id.ToString(), -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        lnkProjectStatusPopup.ToolTip = Type_Id + "|0";
        lnkProjectStatusPopup.Text = "Project Status: All Projects";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTargetP_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTargetP_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompletedP.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoingP.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
        }
        else
        {
            lnkCompletedP.Text = "0";
            lnkOnGoingP.Text = "0";
            lnkTargetP_C.Text = "0";
            lnkTargetP_N.Text = "0";
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdIssueReported.DataSource = ds.Tables[0];
            grdIssueReported.DataBind();
        }
        else
        {
            grdIssueReported.DataSource = null;
            grdIssueReported.DataBind();
        }
        mp1.Show();
    }

    protected void lnkExp_O_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("Report_Year_Wise_Financial_Progress.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=Completed&NodalDept_Id=" + NodalDept_Id);
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=Completed");
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=OnGoing&NodalDept_Id=" + NodalDept_Id);
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=OnGoing");
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
            Dependency_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Dependency_Id = 0;
        }

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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        try
        {
            Type_Id = ToolTip_Data[0].Trim();
        }
        catch
        {
            Type_Id = "0";
        }
        try
        {
            NodalDept_Id = ToolTip_Data[1].Trim();
        }
        catch
        {
            NodalDept_Id = "0";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id.ToString() + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id);
    }

    protected void lblUpdation100_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=11&ProgressType=" + ProgressType);
    }

    protected void lblUpdation0_Click(object sender, EventArgs e)
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=0&ProgressType=" + ProgressType);
    }

    protected void btnInfo10_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo10 = sender as ImageButton;
        GridViewRow gr = btnInfo10.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 1, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo20_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo20 = sender as ImageButton;
        GridViewRow gr = btnInfo20.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 2, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo30_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo30 = sender as ImageButton;
        GridViewRow gr = btnInfo30.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 3, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo40_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo40 = sender as ImageButton;
        GridViewRow gr = btnInfo40.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 4, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo50_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo50 = sender as ImageButton;
        GridViewRow gr = btnInfo50.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 5, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo60_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo60 = sender as ImageButton;
        GridViewRow gr = btnInfo60.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 6, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo70_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo70 = sender as ImageButton;
        GridViewRow gr = btnInfo70.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 7, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo80_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo80 = sender as ImageButton;
        GridViewRow gr = btnInfo80.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 8, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo90_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo90 = sender as ImageButton;
        GridViewRow gr = btnInfo90.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 9, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoMore90_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfoMore90 = sender as ImageButton;
        GridViewRow gr = btnInfoMore90.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 10, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo100_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo100 = sender as ImageButton;
        GridViewRow gr = btnInfo100.Parent.Parent as GridViewRow;
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 11, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
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
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;
        
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoTotalProject_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoWaterSupply_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        string Type_Id = "26";
        if (Scheme_Id == "1013")
        {
            Type_Id = "26";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "39";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "32";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "45";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "16";
        }
        else
        {
            Type_Id = "26";
        }
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoSewarage_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        string Type_Id = "24";
        if (Scheme_Id == "1013")
        {
            Type_Id = "24";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "43";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "37";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "30";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "18";
        }
        else
        {
            Type_Id = "24";
        }
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoBuildingWork_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        string Type_Id = "22";
        if (Scheme_Id == "1013")
        {
            Type_Id = "22";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "34";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "28";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "41";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "22";
        }
        else
        {
            Type_Id = "22";
        }
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoSeptage_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        string Type_Id = "27";
        if (Scheme_Id == "1013")
        {
            Type_Id = "27";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "46";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "40";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "40";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "21";
        }
        else
        {
            Type_Id = "27";
        }
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoDrainage_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        string Type_Id = "23";
        if (Scheme_Id == "1013")
        {
            Type_Id = "23";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "42";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "36";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "29";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "17";
        }
        else
        {
            Type_Id = "23";
        }
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoSolidWaste_Click(object sender, ImageClickEventArgs e)
    {
        string ProgressType = "";

        string Scheme_Id = ddlScheme.SelectedValue;

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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
        string Type_Id = "25";
        if (Scheme_Id == "1013")
        {
            Type_Id = "25";
        }
        else if (Scheme_Id == "1016")
        {
            Type_Id = "44";
        }
        else if (Scheme_Id == "1015")
        {
            Type_Id = "35";
        }
        else if (Scheme_Id == "3")
        {
            Type_Id = "31";
        }
        else if (Scheme_Id == "12")
        {
            Type_Id = "25";
        }
        else
        {
            Type_Id = "25";
        }
        DataSet ds = new DataSet();

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", Type_Id, NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void grdIssueReportedGlobal_PreRender(object sender, EventArgs e)
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

    protected void lnkTotalIssuesGlobal_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "0";

        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        try
        {
            Issue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Issue_Id = 0;
        }
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
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
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
        string NodalDept_Id = "0";
        
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id.ToString() + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id);
    }
}