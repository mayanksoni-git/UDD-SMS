using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

public partial class DashboardDept : System.Web.UI.Page
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
            SearchStorage obj_SearchStorage = new SearchStorage();
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            btnView4.Text = Session["Default_Zone"].ToString() + " Wise EMB and Invoice Status";

            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
            get_tbl_Project();
            get_tbl_Zone();
            get_M_Jurisdiction();

            if (Request.QueryString.Count > 2)
            {
                string Scheme_Id = "";
                int District_Id = 0;
                int ULB_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                string FromDate = "";
                string TillDate = "";
                try
                {
                    FromDate = Request.QueryString["FromDate"].ToString();
                }
                catch
                {
                    FromDate = "";
                }
                try
                {
                    TillDate = Request.QueryString["TillDate"].ToString();
                }
                catch
                {
                    TillDate = "";
                }

                try
                {
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                }
                catch
                {
                    Scheme_Id = "";
                }

                try
                {
                    District_Id = Convert.ToInt32(Request.QueryString["District_Id"].ToString());
                }
                catch
                {
                    District_Id = 0;
                }
                try
                {
                    ULB_Id = Convert.ToInt32(Request.QueryString["ULB_Id"].ToString());
                }
                catch
                {
                    ULB_Id = 0;
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
                obj_SearchStorage.Circle_Id = Circle_Id;
                obj_SearchStorage.District_Id = District_Id;
                obj_SearchStorage.Division_Id = Division_Id;
                obj_SearchStorage.FromDate = FromDate;
                obj_SearchStorage.Scheme_Id = Scheme_Id;
                obj_SearchStorage.Search_By = "2";
                obj_SearchStorage.TillDate = TillDate;
                obj_SearchStorage.ULB_Id = ULB_Id;
                obj_SearchStorage.Zone_Id = Zone_Id;
                Session["SearchStorage"] = obj_SearchStorage;
            }

            if (Session["SearchStorage"] != null)
            {
                obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                txtDateFrom.Text = obj_SearchStorage.FromDate;
                txtDateTill.Text = obj_SearchStorage.TillDate;

                string[] List_Scheme = obj_SearchStorage.Scheme_Id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (List_Scheme.Length > 0)
                {
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        listItem.Selected = false;
                    }
                    for (int i = 0; i < List_Scheme.Length; i++)
                    {
                        foreach (ListItem listItem in ddlScheme.Items)
                        {
                            if (List_Scheme[i].Trim() == listItem.Value)
                            {
                                listItem.Selected = true;
                                break;
                            }
                        }
                    }
                }
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

            if (Session["SearchStorage"] != null)
            {
                load_dashboard(obj_SearchStorage.Default_Click_Action);
            }
            else
            {
                load_dashboard(1);
            }
                
            get_PMIS_Dashboard_View();
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
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() == "A")
                {
                    MessageBox.Show("Invoice Details Approved Successfully");
                }
            }
        }
    }

    protected void lnkSNALimitAvailable_Click(object sender, EventArgs e)
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        Response.Redirect("MasterSNAChildAccount.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Display=1");
    }

    protected void get_PMIS_Dashboard_View()
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }

        if (Zone_Id + Circle_Id + Division_Id > 0)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Session["ds_Notification"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lnkLD.Text = ds.Tables[0].Rows[0]["LD_Likly"].ToString();
                lnkBondDateDelayNotExtended.Text = ds.Tables[0].Rows[0]["Delay_Likly2"].ToString();
            }
            else
            {
                lnkLD.Text = "0";
                lnkBondDateDelayNotExtended.Text = "0";
            }

            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                lnkStagnantPhysical.Text = ds.Tables[1].Rows[0]["Total_Stagnent_Physical"].ToString();
            }
            else
            {
                lnkStagnantPhysical.Text = "0";
            }

            if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                lnkStagnantFinancial.Text = ds.Tables[2].Rows[0]["Total_Stagnent_Financial"].ToString();
            }
            else
            {
                lnkStagnantFinancial.Text = "0";
            }

            if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
            {
                lnkDocumentNA.Text = ds.Tables[3].Rows[0]["Total_Project_Doc_NA"].ToString();
            }
            else
            {
                lnkDocumentNA.Text = "0";
            }

            if (ds != null && ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
            {
                lnkSNALimitAvailable.Text = ds.Tables[4].Rows[0]["Payment_Can_Be_Done"].ToString();
            }
            else
            {
                lnkSNALimitAvailable.Text = "0";
            }
            mpNotification.Show();
        }
    }
    protected void lnkBondDateDelayNotExtended_Click(object sender, EventArgs e)
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=2");
    }

    protected void lnkLD_Click(object sender, EventArgs e)
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=0");
    }

    protected void lnkStagnantPhysical_Click(object sender, EventArgs e)
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        Response.Redirect("Report_ProjectWork_Physical_Progress_NoChange.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkStagnantFinancial_Click(object sender, EventArgs e)
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        Response.Redirect("Report_Project_With_No_Invoice.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkDocumentNA_Click(object sender, EventArgs e)
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        Response.Redirect("Report_ProjectDocumentNotAvailable.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }
    private void Set_Mark_Status(int PackageInvoice_Id, int Scheme_Id, CheckBox chkMark)
    {
        DataSet ds = new DataSet();
        int _Loop = 0;
        if (Session["UserType"].ToString() == "1")
        {
            _Loop = (new DataLayer()).get_Loop("Invoice", 0, 0, 0, null, null);
        }
        else
        {
            _Loop = (new DataLayer()).get_Loop("Invoice", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, null, null);
        }
        ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "Invoice", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, PackageInvoice_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            int ConfigMaster_Id = 0;
            try
            {
                ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                if (get_tbl_InvoiceStatus(ConfigMaster_Id, "Invoice", Scheme_Id))
                {
                    chkMark.Visible = true;
                }
                else
                {
                    chkMark.Visible = false;
                }
            }
            catch
            { }
        }
    }

    private bool get_tbl_InvoiceStatus(int ConfigMasterId, string Process_Name, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(0, 0, 0, 0, Process_Name);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId, Process_Name);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    private bool get_tbl_InvoiceStatus_ADP(int ConfigMasterId, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    private void load_dashboard(int MultiViewIndex)
    {
        SearchStorage obj_SearchStorage = new SearchStorage();

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        load_Dashboard_PendingAction();
        if (MultiViewIndex == 1)
        {
            load_dashboard_View1();
        }
        else if (MultiViewIndex == 2)
        {
            load_dashboard_View2();
        }
        else if (MultiViewIndex == 3)
        {
            load_dashboard_View3();
        }
        else if (MultiViewIndex == 4)
        {
            load_dashboard_View4();
        }
        else if (MultiViewIndex == 5)
        {
            load_dashboard_View5();
        }
        else
        {
            load_dashboard_View1();
        }
    }

    private void load_Dashboard_PendingAction()
    {
        //Pending Actions
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        get_tbl_PackageInvoice(Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, grdInvoice, true, -1);
        get_tbl_PackageInvoiceADP(true);
        get_tbl_PackageInvoiceMA(true);
        get_tbl_Package_DeductionRelease(true);

        set_Labels();
    }

    private void load_dashboard_View5()
    {
        //View 5
        Data_Updation_Dashboard();

        mvDashboard.ActiveViewIndex = 4;
        btnView1.CssClass = "btn btn-warning";
        btnView2.CssClass = "btn btn-danger";
        btnView3.CssClass = "btn btn-inverse";
        btnView4.CssClass = "btn btn-pink";
        btnView5.CssClass = "btn btn-primary btn-xlg";

        if (Session["SearchStorage"] != null)
        {
            SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
            obj_SearchStorage.Default_Click_Action = 5;

            Session["SearchStorage"] = obj_SearchStorage;
        }
    }

    private void load_dashboard_View4()
    {
        //View 4
        get_Zone_Wise_EMB_Invoice_Dashboard_Report();

        mvDashboard.ActiveViewIndex = 3;
        btnView1.CssClass = "btn btn-warning";
        btnView2.CssClass = "btn btn-danger";
        btnView3.CssClass = "btn btn-inverse";
        btnView4.CssClass = "btn btn-pink btn-xlg";
        btnView5.CssClass = "btn btn-primary";

        if (Session["SearchStorage"] != null)
        {
            SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
            obj_SearchStorage.Default_Click_Action = 4;

            Session["SearchStorage"] = obj_SearchStorage;
        }
    }

    private void load_dashboard_View3()
    {
        //View 3
        get_MA_Invoice_Dashboard_Report();
        get_DR_Invoice_Dashboard_Report();

        mvDashboard.ActiveViewIndex = 2;
        btnView1.CssClass = "btn btn-warning";
        btnView2.CssClass = "btn btn-danger";
        btnView3.CssClass = "btn btn-inverse btn-xlg";
        btnView4.CssClass = "btn btn-pink";
        btnView5.CssClass = "btn btn-primary";

        if (Session["SearchStorage"] != null)
        {
            SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
            obj_SearchStorage.Default_Click_Action = 3;

            Session["SearchStorage"] = obj_SearchStorage;
        }
    }

    private void load_dashboard_View2()
    {
        //View 2
        get_ADP_Invoice_Dashboard_Report();
        get_tbl_Package_ADP_Category();

        mvDashboard.ActiveViewIndex = 1;
        btnView1.CssClass = "btn btn-warning";
        btnView2.CssClass = "btn btn-danger btn-xlg";
        btnView3.CssClass = "btn btn-inverse";
        btnView4.CssClass = "btn btn-pink";
        btnView5.CssClass = "btn btn-primary";

        if (Session["SearchStorage"] != null)
        {
            SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
            obj_SearchStorage.Default_Click_Action = 2;

            Session["SearchStorage"] = obj_SearchStorage;
        }
    }

    private void load_dashboard_View1()
    {
        //View 1
        get_EMB_Dashboard_Report();
        get_Invoice_Dashboard_Report();

        mvDashboard.ActiveViewIndex = 0;
        btnView1.CssClass = "btn btn-warning btn-xlg";
        btnView2.CssClass = "btn btn-danger";
        btnView3.CssClass = "btn btn-inverse";
        btnView4.CssClass = "btn btn-pink";
        btnView5.CssClass = "btn btn-primary";

        if (Session["SearchStorage"] != null)
        {
            SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
            obj_SearchStorage.Default_Click_Action = 1;

            Session["SearchStorage"] = obj_SearchStorage;
        }
    }

    private void set_Labels()
    {
        sp_Invoice.InnerHtml = grdInvoice.Rows.Count.ToString();
        sp_OtherDept.InnerHtml = grdADP.Rows.Count.ToString();
        sp_OtherPayment.InnerHtml = grdMA.Rows.Count.ToString();
        sp_DeductionRelease.InnerHtml = grdDeductionRelease.Rows.Count.ToString();
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

    protected void grdInvoice_PreRender(object sender, EventArgs e)
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

    private void get_EMB_Dashboard_Report()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_EMB_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_EMB_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMBDash.DataSource = ds.Tables[0];
            grdEMBDash.DataBind();

            grdEMBDash.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_EMB)", "").ToString();
            (grdEMBDash.FooterRow.FindControl("lnkEMBTotalPF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_EMB_N)", "").ToString();
            grdEMBDash.FooterRow.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount_N)", "").ToString(), "Int");
        }
        else
        {
            grdEMBDash.DataSource = null;
            grdEMBDash.DataBind();
        }
    }
    private void Data_Updation_Dashboard()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        ds = (new DataLayer()).get_Data_Updation_Dashboard(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDataUpdationStatusReport.DataSource = ds.Tables[0];
            grdDataUpdationStatusReport.DataBind();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkTotal_P_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Project)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Package)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkBOQ_NA_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_BOQ_Not_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Total_Freezed)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkUnFreezed_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Not_Freezed)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Total_RA_Bills_Filled)", "").ToString() + " / " + ds.Tables[0].Compute("sum(Total_RA_Bills)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_0_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_0)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_1_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_1)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_2_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_2)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_3_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_3)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_4_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_4)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_5_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_5)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_6_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_6)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_7_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_7)", "").ToString();
        }
        else
        {
            grdDataUpdationStatusReport.DataSource = null;
            grdDataUpdationStatusReport.DataBind();
        }
    }

    private void get_Zone_Wise_EMB_Invoice_Dashboard_Report()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? exclude_Division = null;
        bool? exclude_Rejected = null;
        bool? include_Payment = null;
        if (chkFilter.Items[0].Selected)
            exclude_Division = true;
        if (chkFilter.Items[1].Selected)
            exclude_Rejected = true;
        if (chkFilter.Items[2].Selected)
            include_Payment = true;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Zone_Wise_EMB_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", exclude_Division, exclude_Rejected, include_Payment, false, 0, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Zone_Wise_EMB_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, txtDateFrom.Text, txtDateTill.Text, exclude_Division, exclude_Rejected, include_Payment, false, 0, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMBAndInvStatus.DataSource = ds.Tables[0];
            grdEMBAndInvStatus.DataBind();

            grdEMBAndInvStatus.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_EMB_N)", "").ToString();
            grdEMBAndInvStatus.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount_N)", "").ToString(), "Int");
            grdEMBAndInvStatus.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            grdEMBAndInvStatus.FooterRow.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value)", "").ToString(), "Int");

            grdEMBAndInvStatus.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(Total_Invoice_ADP)", "").ToString();
            grdEMBAndInvStatus.FooterRow.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value_ADP)", "").ToString(), "Int");

            grdEMBAndInvStatus.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Total_Invoice_DR)", "").ToString();
            grdEMBAndInvStatus.FooterRow.Cells[10].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value_DR)", "").ToString(), "Int");

            grdEMBAndInvStatus.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(Total_Invoice_MA)", "").ToString();
            grdEMBAndInvStatus.FooterRow.Cells[12].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value_MA)", "").ToString(), "Int");
        }
        else
        {
            grdEMBAndInvStatus.DataSource = null;
            grdEMBAndInvStatus.DataBind();
        }
    }

    private void get_Invoice_Dashboard_Report()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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

        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }
        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoiceDash.DataSource = ds.Tables[0];
            grdInvoiceDash.DataBind();

            //grdInvoiceDash.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            (grdInvoiceDash.FooterRow.FindControl("lnkTotalInvoice") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            grdInvoiceDash.FooterRow.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value)", "").ToString(), "Int");
        }
        else
        {
            grdInvoiceDash.DataSource = null;
            grdInvoiceDash.DataBind();
        }
    }

    private void get_ADP_Invoice_Dashboard_Report()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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

        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_ADP_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_ADP_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }
        if (AllClasses.CheckDataSet(ds))
        {
            grdADPInvDash.DataSource = ds.Tables[0];
            grdADPInvDash.DataBind();

            (grdADPInvDash.FooterRow.FindControl("lblTotalInvoiceADP") as Label).Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            grdADPInvDash.FooterRow.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value)", "").ToString(), "Int");
        }
        else
        {
            grdADPInvDash.DataSource = null;
            grdADPInvDash.DataBind();
        }
    }

    private void get_MA_Invoice_Dashboard_Report()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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

        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_MA_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_MA_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }
        if (AllClasses.CheckDataSet(ds))
        {
            grdMAInvDash.DataSource = ds.Tables[0];
            grdMAInvDash.DataBind();

            (grdMAInvDash.FooterRow.FindControl("lblTotalInvoiceMA") as Label).Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            grdMAInvDash.FooterRow.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value)", "").ToString(), "Int");
        }
        else
        {
            grdMAInvDash.DataSource = null;
            grdMAInvDash.DataBind();
        }
    }

    private void get_DR_Invoice_Dashboard_Report()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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

        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_DR_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_DR_Invoice_Dashboard_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }
        if (AllClasses.CheckDataSet(ds))
        {
            grdDRInvDash.DataSource = ds.Tables[0];
            grdDRInvDash.DataBind();

            (grdDRInvDash.FooterRow.FindControl("lblTotalInvoiceDR") as Label).Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            grdDRInvDash.FooterRow.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Value)", "").ToString(), "Int");
        }
        else
        {
            grdDRInvDash.DataSource = null;
            grdDRInvDash.DataBind();
        }
    }

    private void get_tbl_PackageInvoice(int Org_Id, int Designation_Id, string Scheme_Id, GridView grdInvoice, bool is_Pendency, int IgnoreDivisionInvoices)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, "", "", 0, 0, -1, false, IgnoreDivisionInvoices, isDefered, "", District_Id, ULB_Id);
        }
        else
        {
            if (is_Pendency)
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, "", "", 0, 0, -1, false, IgnoreDivisionInvoices, isDefered, "", District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, txtDateFrom.Text, txtDateTill.Text, 0, 0, -1, false, IgnoreDivisionInvoices, isDefered, "", District_Id, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMark.Visible = false;
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdInvoice.Rows.Count; i++)
                {
                    CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMark.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMark.Visible = false;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
    }
    private void get_tbl_PackageInvoiceADP(bool is_Pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", 0, 0, 0, false, isDefered, ULB_Id);
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_Pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, 0, 0, 0, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkADP.Visible = false;
            grdADP.DataSource = ds.Tables[0];
            grdADP.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdADP.Rows.Count; i++)
                {
                    CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMarkADP.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMarkADP.Visible = false;
            grdADP.DataSource = null;
            grdADP.DataBind();
        }
    }

    private void get_tbl_PackageInvoiceMA(bool is_Pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? isDefered = null;
        DataSet ds = new DataSet();
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_Pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }

            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkMA.Visible = false;
            grdMA.DataSource = ds.Tables[0];
            grdMA.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdMA.Rows.Count; i++)
                {
                    CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMarkMA.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMarkMA.Visible = false;
            grdMA.DataSource = null;
            grdMA.DataBind();
        }
    }

    private void get_tbl_Package_DeductionRelease(bool is_pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkDR.Visible = false;
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
            if (is_pendency)
            {
                for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
                {
                    CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMarkDR.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMarkDR.Visible = false;
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
    }

    private void get_tbl_Package_ADP_Category()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP_Category(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, "", "", false, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP_Category(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, txtDateFrom.Text, txtDateTill.Text, false, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdADP_Category.DataSource = ds.Tables[0];
            grdADP_Category.DataBind();
            grdADP_Category.FooterRow.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(TotalAmount)", "").ToString(), "Int");
        }
        else
        {
            grdADP_Category.DataSource = null;
            grdADP_Category.DataBind();
        }
    }
    private void get_tbl_PackageEMB(bool ShowOnlyEMB, int Designation_Id, int Org_Id, string Scheme_Id, int TAT_Range)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, 0, "", ShowOnlyEMB, "", "", TAT_Range, false, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, 0, "", ShowOnlyEMB, txtDateFrom.Text, txtDateTill.Text, TAT_Range, false, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();
        }
        else
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();
        }
    }

    protected void btnOpenInvoice_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = 0;
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }
        string Package_Id = gr.Cells[1].Text.Trim();
        string Scheme_Id = gr.Cells[6].Text.Trim();
        string PackageInvoice_Type = gr.Cells[30].Text.Trim();

        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;

        if (Invoice_Id == 0)
        {
            if (Session["Invoice_C"] == null)
            {
                if (PackageInvoice_Type == "N")
                {
                    Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=" + Package_Id + "&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
                }
                else
                {
                    Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=" + Package_Id + "&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
                }
            }
            else if (Session["Invoice_C"].ToString() == "1")
            {
                if (PackageInvoice_Type == "N")
                {
                    Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=" + Package_Id + "&Invoice_Id=0&Scheme_Id=" + Scheme_Id);
                }
                else
                {
                    Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=" + Package_Id + "&Invoice_Id=0&Scheme_Id=" + Scheme_Id);
                }
            }
            else
            {
                MessageBox.Show("Invoice Not Generated. Please Generate First.");
                return;
            }
        }
        else
        {
            if (PackageInvoice_Type == "N")
            {
                Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=0&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
            }
            else
            {
                Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=0&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
            }
        }
    }
    protected void btnOpenInvoiceV_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = 0;
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }
        string Package_Id = gr.Cells[1].Text.Trim();
        string Scheme_Id = gr.Cells[6].Text.Trim();
        string PackageInvoice_Type = gr.Cells[30].Text.Trim();

        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;

        if (Invoice_Id == 0)
        {
            if (Session["Invoice_C"] == null)
            {
                if (PackageInvoice_Type == "N")
                {
                    Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=" + Package_Id + "&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
                }
                else
                {
                    Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=" + Package_Id + "&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
                }
            }
            else if (Session["Invoice_C"].ToString() == "1")
            {
                if (PackageInvoice_Type == "N")
                {
                    Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=" + Package_Id + "&Invoice_Id=0&Scheme_Id=" + Scheme_Id);
                }
                else
                {
                    Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=" + Package_Id + "&Invoice_Id=0&Scheme_Id=" + Scheme_Id);
                }
            }
            else
            {
                MessageBox.Show("Invoice Not Generated. Please Generate First.");
                return;
            }
        }
        else
        {
            if (PackageInvoice_Type == "N")
            {
                Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=0&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
            }
            else
            {
                Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=0&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
            }
        }
    }
    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[13].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[14].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[15].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnCover = e.Row.FindControl("btnCover") as ImageButton;
            ImageButton btnOpenInvoice = e.Row.FindControl("btnOpenInvoice") as ImageButton;
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int PackageInvoiceCover_Id = 0;

            int Designation_Id = 0;
            int Organization_Id = 0;
            int PackageInvoice_Id = 0;
            int Scheme_Id = 0;
            try
            {
                PackageInvoice_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                PackageInvoice_Id = 0;
            }
            try
            {
                Organization_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Organization_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Designation_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[6].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }

            if (Session["UserType"].ToString() == "1")
            {
                btnOpenInvoice.Visible = true;
            }
            else
            {
                if (Session["Person_BranchOffice_Id"].ToString() == Organization_Id.ToString() && Session["PersonJuridiction_DesignationId"].ToString() == Designation_Id.ToString())
                {
                    btnOpenInvoice.Visible = true;
                }
                else
                {
                    btnOpenInvoice.Visible = false;
                }
            }

            if (Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "33" || Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
            {
                try
                {
                    PackageInvoiceCover_Id = Convert.ToInt32(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""));
                }
                catch
                {
                    PackageInvoiceCover_Id = 0;
                }
                if (PackageInvoiceCover_Id > 0)
                {
                    btnOpenInvoice.Visible = true;
                }
                else
                {
                    btnOpenInvoice.Visible = false;
                }
                btnCover.Visible = true;
            }
            else
            {
                btnCover.Visible = false;
            }


            Set_Mark_Status(PackageInvoice_Id, Scheme_Id, chkMark);
        }
    }
    protected void grdADP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int _Loop = 0;
            int Package_ADP_Id = 0;
            int Scheme_Id = 0;
            try
            {
                _Loop = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                _Loop = 0;
            }
            try
            {
                Package_ADP_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Package_ADP_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }

            DataSet ds = new DataSet();

            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "PackageADP", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, Package_ADP_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    if (get_tbl_InvoiceStatus_ADP(ConfigMaster_Id, Scheme_Id))
                    {
                        chkMark.Visible = true;
                    }
                    else
                    {
                        chkMark.Visible = false;
                    }
                }
                catch
                { }
            }
            else
            {
                chkMark.Visible = false;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        if (Session["SearchStorage"] != null)
        {
            load_dashboard(obj_SearchStorage.Default_Click_Action);
        }
        else
        {
            load_dashboard(1);
        }
    }

    protected void btnMark_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_PackageInvoiceApproval> obj_tbl_PackageInvoiceApproval_Li = new List<tbl_PackageInvoiceApproval>();
        for (int i = 0; i < grdInvoice.Rows.Count; i++)
        {
            int PackageInvoice_Id = 0;
            try
            {
                PackageInvoice_Id = Convert.ToInt32(grdInvoice.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                PackageInvoice_Id = 0;
            }
            CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (PackageInvoice_Id > 0)
                {
                    tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = "";
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 4;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
                    obj_tbl_PackageInvoiceApproval.Scheme_Id = Convert.ToInt32(Convert.ToInt32(grdInvoice.Rows[i].Cells[6].Text.Trim()));
                    obj_tbl_PackageInvoiceApproval_Li.Add(obj_tbl_PackageInvoiceApproval);
                }
            }
        }
        if (obj_tbl_PackageInvoiceApproval_Li.Count == 0)
        {
            MessageBox.Show("Please Select At-least One Invoice To Recommend");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageInvoice_Mark(obj_tbl_PackageInvoiceApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Invoice Marked Successfully");
                string Scheme_Id = "";
                foreach (ListItem listItem in ddlScheme.Items)
                {
                    if (listItem.Selected)
                    {
                        Scheme_Id += listItem.Value + ", ";
                    }
                }
                Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
                get_tbl_PackageInvoice(Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, grdInvoice, true, -1);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Invoice");
                return;
            }
        }
    }
    protected void btnMarkADP_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_PackageADPApproval> obj_tbl_PackageADPApproval_Li = new List<tbl_PackageADPApproval>();

        for (int i = 0; i < grdADP.Rows.Count; i++)
        {
            int PackageADP_Id = 0;
            try
            {
                PackageADP_Id = Convert.ToInt32(grdADP.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                PackageADP_Id = 0;
            }

            CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (PackageADP_Id > 0)
                {
                    tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
                    obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "";
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 4;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = PackageADP_Id;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(grdADP.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 1;
                    obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(grdADP.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(grdADP.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_PackageADPApproval_Li.Add(obj_tbl_PackageADPApproval);
                }
            }
        }
        if (obj_tbl_PackageADPApproval_Li.Count == 0 || obj_tbl_PackageADPApproval_Li == null)
        {
            MessageBox.Show("Please Select At-least One Other Departmental To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_Package_ADP_Item_Mark(obj_tbl_PackageADPApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Other Departmental Marked Successfully");
                get_tbl_PackageInvoiceADP(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Other Departmental");
                return;
            }
        }
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

    protected void grdEMB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[12].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[13].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnOpenEMB = e.Row.FindControl("btnOpenEMB") as ImageButton;
            CheckBox chkMarkMB = e.Row.FindControl("chkMarkMB") as CheckBox;
            int Designation_Id = 0;
            int Organization_Id = 0;
            int PackageEMB_Master_Id = 0;
            try
            {
                PackageEMB_Master_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                PackageEMB_Master_Id = 0;
            }
            try
            {
                Organization_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Organization_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Designation_Id = 0;
            }
            if (Session["UserType"].ToString() == "1")
            {
                btnOpenEMB.Visible = true;
            }
            else
            {
                if (Session["Person_BranchOffice_Id"].ToString() == Organization_Id.ToString() && Session["PersonJuridiction_DesignationId"].ToString() == Designation_Id.ToString())
                {
                    btnOpenEMB.Visible = true;
                }
                else
                {
                    btnOpenEMB.Visible = false;
                }
            }
        }
    }

    protected void btnOpenEMB_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string PackageEMB_Master_Id = gr.Cells[0].Text.Trim();
        string Package_Id = gr.Cells[1].Text.Trim();
        string PackageEMB_Master_Type = gr.Cells[4].Text.Trim();
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

    protected void btnCover_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string Invoice_Id = gr.Cells[0].Text.Trim();
        string Package_Id = gr.Cells[1].Text.Trim();
        string ProcessedBy = gr.Cells[2].Text.Trim().Replace("&nbsp;", "");
        Response.Redirect("MasterGenerateCoverLetter?Invoice_Id=" + Invoice_Id);
    }

    protected void grdEMBDash_PreRender(object sender, EventArgs e)
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

    protected void lnkEMBTotal_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdEMBDash.Rows.Count; i++)
        {
            grdEMBDash.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int Designation_Id = 0;
        int Scheme_Id = 0;
        int Org_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[7].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        get_tbl_PackageEMB(false, Designation_Id, Org_Id, Scheme_Id.ToString(), 0);
    }

    protected void lnkEMBTotalP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdEMBDash.Rows.Count; i++)
        {
            grdEMBDash.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int Designation_Id = 0;
        int Org_Id = 0;
        string Mode = gr.Cells[8].Text.Trim();
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[7].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        if (Mode == "EMB")
        {
            get_tbl_PackageEMB(true, Designation_Id, Org_Id, Scheme_Id.ToString(), 0);
        }
        else
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            get_tbl_PackageInvoice(Org_Id, Designation_Id, Scheme_Id.ToString(), grdInvoiceDashV, false, 0);
        }
    }

    protected void grdInvoiceDash_PreRender(object sender, EventArgs e)
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

    protected void lnkTotalInvoice_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdInvoiceDash.Rows.Count; i++)
        {
            grdInvoiceDash.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int Designation_Id = 0;
        int Org_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[6].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        grdEMB.DataSource = null;
        grdEMB.DataBind();

        grdInvoiceDashV.DataSource = null;
        grdInvoiceDashV.DataBind();

        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        get_tbl_PackageInvoice(Org_Id, Designation_Id, Scheme_Id.ToString(), grdInvoiceDashV, false, 1);
    }

    protected void grdInvoiceDashV_PreRender(object sender, EventArgs e)
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

    protected void lnkBOQ_NA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;
        string Scheme_Id = "";
        int District_Id = 0;
        int ULB_Id = 0;

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
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
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
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, false, true, ULB_Id);
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
    protected void lnkTotal_Project_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;
        string Scheme_Id = "";
        int Circle_Id = 0;
        int Division_Id = 0;

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
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
        ds = (new DataLayer()).get_tbl_ProjectWork(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", 0, "", 0);
        if (AllClasses.CheckDataSet(ds))
        {
            GridproView.DataSource = ds.Tables[0];
            GridproView.DataBind();
            ProViewSummery.Show();
        }
        else
        {
            GridproView.DataSource = null;
            GridproView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkUnFreezed_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;
        string Scheme_Id = "";
        int District_Id = 0;
        int ULB_Id = 0;


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
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
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
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, true, false, ULB_Id);
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
    protected void grdProView_PreRender(object sender, EventArgs e)
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

    protected void grdEMBDash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[6].Text.Trim().Replace("&nbsp;", ""), "Int");

            int Org_Id = 0;
            int Designation_Id = 0;
            string Mode = e.Row.Cells[8].Text.Trim();
            try
            {
                Org_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            }
            catch
            {
                Org_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                Designation_Id = 0;
            }


            int Scheme_Id = 0;
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[7].Text.Trim());
            }
            catch
            {
                Scheme_Id = 0;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[6].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void grdInvoiceDash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");

            LinkButton lnkRejectionWise = e.Row.FindControl("lnkRejectionWise") as LinkButton;
            LinkButton lnkInfoExp = e.Row.FindControl("lnkInfoExp") as LinkButton;

            int Org_Id = 0;
            int Designation_Id = 0;
            try
            {
                Org_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            }
            catch
            {
                Org_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                Designation_Id = 0;
            }
            if (Org_Id == 2 && Designation_Id == 33)
            {
                lnkInfoExp.Visible = true;
                lnkRejectionWise.Visible = false;
            }
            else if (Org_Id == 1 && Designation_Id == 33)
            {
                lnkInfoExp.Visible = true;
                lnkRejectionWise.Visible = false;
            }
            else if (Org_Id == -1 && Designation_Id == -1)
            {
                lnkInfoExp.Visible = true;
                lnkRejectionWise.Visible = false;
            }
            else if (Org_Id == -2 && Designation_Id == -2)
            {
                lnkInfoExp.Visible = true;
                lnkRejectionWise.Visible = true;
            }
            else
            {
                lnkInfoExp.Visible = false;
                lnkRejectionWise.Visible = false;
            }

            int Scheme_Id = 0;
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[6].Text.Trim());
            }
            catch
            {
                Scheme_Id = 0;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void chkMarkH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkH = sender as CheckBox;
        for (int i = 0; i < grdInvoice.Rows.Count; i++)
        {
            CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkH.Checked;
        }
    }

    private void get_Invoice_On_Expert(int TAT_Range, int Org_Id, int Designation_Id, string Scheme_Id)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        string ExpertType = "";
        if (Org_Id == 1)
        {
            ExpertType = "JNUP";
        }
        else if (Org_Id == 2)
        {
            ExpertType = "SMD";
        }
        else
        {
            ExpertType = "";
        }
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, Org_Id, Designation_Id, "", "", false, ExpertType, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, Org_Id, Designation_Id, txtDateFrom.Text, txtDateTill.Text, false, ExpertType, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Expert Wise Invoice Count";
            hf_ClickMode.Value = "Expert Pending";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdExpertWiseDtls_PreRender(object sender, EventArgs e)
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

    protected void grdEMBAndInvStatus_PreRender(object sender, EventArgs e)
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

    protected void grdEMBAndInvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Zone"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""), "Int");
            e.Row.Cells[6].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[6].Text.Trim().Replace("&nbsp;", ""), "Int");
            e.Row.Cells[8].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[8].Text.Trim().Replace("&nbsp;", ""), "Int");
            e.Row.Cells[10].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[10].Text.Trim().Replace("&nbsp;", ""), "Int");
            e.Row.Cells[12].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[12].Text.Trim().Replace("&nbsp;", ""), "Int");
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
        int District_Id = 0;
        int ULB_Id = 0;


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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, false, true, ULB_Id);
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
    protected void lnkTotal_Project_F_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        ds = (new DataLayer()).get_tbl_ProjectWork(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", 0, "", 0);
        if (AllClasses.CheckDataSet(ds))
        {
            GridproView.DataSource = ds.Tables[0];
            GridproView.DataBind();
            ProViewSummery.Show();
        }
        else
        {
            GridproView.DataSource = null;
            GridproView.DataBind();
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
        int District_Id = 0;
        int ULB_Id = 0;


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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, true, false, ULB_Id);
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

    protected void grdADPInvDash_PreRender(object sender, EventArgs e)
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

    protected void lnkTotalInvoiceADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdADPInvDash.Rows.Count; i++)
        {
            grdADPInvDash.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int Designation_Id = 0;
        int Org_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        get_tbl_Package_ADP_Invoice(Org_Id, Designation_Id);
    }
    private void get_tbl_Package_ADP_Invoice(int Org_Id, int Designation_Id)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", 0, 0, 0, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, 0, 0, 0, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdADPV.DataSource = ds.Tables[0];
            grdADPV.DataBind();
        }
        else
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }
    protected void grdADPInvDash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");

            LinkButton lnkRejectionWiseADP = e.Row.FindControl("lnkRejectionWiseADP") as LinkButton;
            LinkButton lnkInfoExpADP = e.Row.FindControl("lnkInfoExpADP") as LinkButton;

            int Org_Id = 0;
            int Designation_Id = 0;
            try
            {
                Org_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            }
            catch
            {
                Org_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                Designation_Id = 0;
            }
            if (Org_Id == 2 && Designation_Id == 33)
            {
                lnkInfoExpADP.Visible = true;
                lnkRejectionWiseADP.Visible = false;
            }
            else if (Org_Id == 1 && Designation_Id == 33)
            {
                lnkInfoExpADP.Visible = true;
                lnkRejectionWiseADP.Visible = false;
            }
            else if (Org_Id == -1 && Designation_Id == -1)
            {
                lnkInfoExpADP.Visible = true;
                lnkRejectionWiseADP.Visible = false;
            }
            else if (Org_Id == -2 && Designation_Id == -2)
            {
                lnkInfoExpADP.Visible = true;
                lnkRejectionWiseADP.Visible = true;
            }
            else
            {
                lnkInfoExpADP.Visible = false;
                lnkRejectionWiseADP.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void grdADPV_PreRender(object sender, EventArgs e)
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
    protected void grdADP_PreRender(object sender, EventArgs e)
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
    protected void btnEditADP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        int Invoice_Id = 0;
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }
        Response.Redirect("PackageAdditionalDepartmentPaymentApproval.aspx?Invoice_Id=" + Invoice_Id.ToString() + "&Scheme_Id=" + Scheme_Id);
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

    protected void chkMarkAH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkAH = sender as CheckBox;
        for (int i = 0; i < grdADP.Rows.Count; i++)
        {
            CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkAH.Checked;
        }
    }

    protected void grdADP_Category_PreRender(object sender, EventArgs e)
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

    protected void grdADP_Category_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }
    private bool get_tbl_InvoiceStatus_MA(int ConfigMasterId, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_MA(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_MA(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    protected void grdMA_PreRender(object sender, EventArgs e)
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

    protected void grdMA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int _Loop = 0;
            int MobilizationAdvance_Id = 0;
            int Scheme_Id = 0;
            try
            {
                _Loop = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                _Loop = 0;
            }
            try
            {
                MobilizationAdvance_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                MobilizationAdvance_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }

            DataSet ds = new DataSet();

            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "Package_MobilizationAdvance", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, MobilizationAdvance_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    if (get_tbl_InvoiceStatus_MA(ConfigMaster_Id, Scheme_Id))
                    {
                        chkMark.Visible = true;
                    }
                    else
                    {
                        chkMark.Visible = false;
                    }
                }
                catch
                { }
            }
            else
            {
                chkMark.Visible = false;
            }
        }
    }

    protected void btnEditMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int MobilizationAdvance_Id = 0;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        try
        {
            MobilizationAdvance_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            MobilizationAdvance_Id = 0;
        }
        Response.Redirect("ApprovalPackageMobilizationRelease.aspx?MobilizationAdvance_Id=" + MobilizationAdvance_Id.ToString() + "&Scheme_Id=" + Scheme_Id);
    }

    protected void btnMarkMA_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_Package_MobilizationAdvanceApproval> obj_tbl_Package_MobilizationAdvanceApproval_Li = new List<tbl_Package_MobilizationAdvanceApproval>();

        for (int i = 0; i < grdMA.Rows.Count; i++)
        {
            int MobilizationAdvance_Id = 0;
            try
            {
                MobilizationAdvance_Id = Convert.ToInt32(grdMA.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                MobilizationAdvance_Id = 0;
            }

            CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (MobilizationAdvance_Id > 0)
                {
                    tbl_Package_MobilizationAdvanceApproval obj_tbl_Package_MobilizationAdvanceApproval = new tbl_Package_MobilizationAdvanceApproval();
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Comments = "";
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status_Id = 4;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_MobilizationAdvance_Id = MobilizationAdvance_Id;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_Id = Convert.ToInt32(grdMA.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Step_Count = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Scheme_Id = Convert.ToInt32(grdMA.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Loop = Convert.ToInt32(grdMA.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval_Li.Add(obj_tbl_Package_MobilizationAdvanceApproval);
                }
            }
        }
        if (obj_tbl_Package_MobilizationAdvanceApproval_Li.Count == 0 || obj_tbl_Package_MobilizationAdvanceApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Mobilization Advance To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_Package_MA_Item_Mark(obj_tbl_Package_MobilizationAdvanceApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Mobilization Advance Marked Successfully");
                get_tbl_PackageInvoiceMA(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Mobilization Advance");
                return;
            }
        }
    }

    protected void chkMarkMA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkMA = sender as CheckBox;
        for (int i = 0; i < grdMA.Rows.Count; i++)
        {
            CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkMA.Checked;
        }
    }

    private void get_Invoice_ADP_On_Expert(int TAT_Range, int Org_Id, int Designation_Id)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        ds = (new DataLayer()).get_Invoice_ADP_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, Org_Id, Designation_Id, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Expert Wise Other Dept. Invoice Count";
            hf_ClickMode.Value = "Expert Pending ADP";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdDeductionRelease_PreRender(object sender, EventArgs e)
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
    private bool get_tbl_InvoiceStatus_DeductionRelease(int ConfigMasterId, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    protected void grdDeductionRelease_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int _Loop = 0;
            int Package_DeductionRelease_Id = 0;
            int Scheme_Id = 0;
            try
            {
                _Loop = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                _Loop = 0;
            }
            try
            {
                Package_DeductionRelease_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Package_DeductionRelease_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }

            DataSet ds = new DataSet();

            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "PackageDeductionRelease", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, Package_DeductionRelease_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    if (get_tbl_InvoiceStatus_DeductionRelease(ConfigMaster_Id, Scheme_Id))
                    {
                        chkMark.Visible = true;
                    }
                    else
                    {
                        chkMark.Visible = false;
                    }
                }
                catch
                { }
            }
            else
            {
                chkMark.Visible = false;
            }
        }
    }

    protected void btnEditDR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_DeductionRelease_Id = 0;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        try
        {
            Package_DeductionRelease_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Package_DeductionRelease_Id = 0;
        }
        Response.Redirect("PackageDeductionReleaseApproval.aspx?Package_DeductionRelease_Id=" + Package_DeductionRelease_Id.ToString() + "&Scheme_Id="+Scheme_Id);
    }

    protected void btnMarkDR_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_Package_DeductionReleaseApproval> obj_tbl_Package_DeductionReleaseApproval_Li = new List<tbl_Package_DeductionReleaseApproval>();

        for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
        {
            int Package_DeductionRelease_Id = 0;
            try
            {
                Package_DeductionRelease_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                Package_DeductionRelease_Id = 0;
            }

            CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (Package_DeductionRelease_Id > 0)
                {
                    tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "";
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 4;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Package_DeductionRelease_Id;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Scheme_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Loop = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 1;
                    obj_tbl_Package_DeductionReleaseApproval_Li.Add(obj_tbl_Package_DeductionReleaseApproval);
                }
            }
        }
        if (obj_tbl_Package_DeductionReleaseApproval_Li.Count == 0 || obj_tbl_Package_DeductionReleaseApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Deduction Release To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_Package_DeductionRelease_Item_Mark(obj_tbl_Package_DeductionReleaseApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Deduction Release Marked Successfully");
                get_tbl_Package_DeductionRelease(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Deduction Release");
                return;
            }
        }
    }

    protected void chkMarkDR_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkDR = sender as CheckBox;
        for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
        {
            CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkDR.Checked;
        }
    }

    protected void lnkZone_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

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
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
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
        Response.Redirect("Report_Data_Updation_Status.aspx?Scheme_Id=" + Scheme_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkInfoExp_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[6].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        get_Invoice_On_Expert(4, Org_Id, Designation_Id, Scheme_Id.ToString());
    }

    protected void lnkRejectionWise_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[6].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
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
        string ExpertType = "";
        //if (Scheme_Id == 10)
        //{
        //    ExpertType = "JNUP";
        //}
        //else if (Scheme_Id == 11)
        //{
        //    ExpertType = "SMD";
        //}
        //else
        //{
        //    ExpertType = "";
        //}
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id.ToString(), 5, "", "", false, ExpertType, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id.ToString(), 5, txtDateFrom.Text, txtDateTill.Text, false, ExpertType, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise";
            hf_Org_Id.Value = "-2";
            hf_Designation_Id.Value = "-2";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTATWise_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[6].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis";
            hf_ClickMode.Value = "TAT";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTotalInvoiceCount_Click(object sender, EventArgs e)
    {
        LinkButton lnkTotalInvoiceCount = sender as LinkButton;
        GridViewRow gr = lnkTotalInvoiceCount.Parent.Parent as GridViewRow;
        if (hf_ClickMode.Value == "0")
        {
            mpViewSummeryExpert.Show();
        }
        else
        {
            int Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
            if (hf_ClickMode.Value == "Expert Pending")
            {
                get_InvoiceDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value, -1);
            }
            else if (hf_ClickMode.Value == "Deferred Type Wise")
            {
                get_InvoiceDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value, -1);
            }
            else if (hf_ClickMode.Value == "TAT")
            {
                get_InvoiceDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value, -1);
            }
            else if (hf_ClickMode.Value == "TAT All")
            {
                get_InvoiceDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value, -1);
            }
            else if (hf_ClickMode.Value == "Zone")
            {
                get_InvoiceDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value, -1);
            }
            else if (hf_ClickMode.Value == "Expert Pending ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "Deferred Type Wise ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT ADP All")
            {
                get_Invoice_ADP_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT EMB")
            {
                get_EMBDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT EMB All")
            {
                get_EMBDetails_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT MA")
            {
                get_Invoice_MA_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT MA All")
            {
                get_Invoice_MA_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "Expert Pending MA")
            {
                get_Invoice_MA_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "Deferred Type Wise MA")
            {
                get_Invoice_MA_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT DR")
            {
                get_Invoice_DR_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "TAT DR All")
            {
                get_Invoice_DR_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "Expert Pending DR")
            {
                get_Invoice_DR_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else if (hf_ClickMode.Value == "Deferred Type Wise DR")
            {
                get_Invoice_DR_Details_According_ToPopup(Convert.ToInt32(hf_Org_Id.Value), Convert.ToInt32(hf_Designation_Id.Value), Id, hf_ClickMode.Value);
            }
            else
            {
                mpViewSummeryExpert.Show();
            }
        }
    }

    private void get_EMBDetails_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Pending EMB")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise EMB")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        int TAT_Range = -1;
        if (DataMode == "TAT" || DataMode == "TAT EMB" || DataMode == "TAT EMB All")
        {
            TAT_Range = Id;
        }
        else
        {
            TAT_Range = -1;
        }

        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, 0, "", true, "", "", TAT_Range, false, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageEMB_Approve(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, 0, "", true, txtDateFrom.Text, txtDateTill.Text, TAT_Range, false, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdEMB.DataSource = ds.Tables[0];
            grdEMB.DataBind();
        }
        else
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }
    private void get_InvoiceDetails_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode, int IgnoreDivisionInvoices)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (DataMode == "Zone")
        {
            Zone_Id = Id;
        }
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Pending")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        int TAT_Range = -1;

        if (DataMode == "TAT" || DataMode == "TAT EMB" || DataMode == "TAT All")
        {
            TAT_Range = Id;
        }
        else
        {
            TAT_Range = -1;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, IgnoreDivisionInvoices, isDefered, "", District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, IgnoreDivisionInvoices, isDefered, "", District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdInvoiceDashV.DataSource = ds.Tables[0];
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }

    private void get_Invoice_ADP_Details_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Pending ADP")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise ADP")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        int TAT_Range = -1;

        if (DataMode == "TAT ADP" || DataMode == "TAT ADP All")
        {
            TAT_Range = Id;
        }
        else
        {
            TAT_Range = -1;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdADPV.DataSource = ds.Tables[0];
            grdADPV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
        else
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }

    protected void lnkTATWiseE_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Mode = gr.Cells[8].Text.Trim();
        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[7].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (Mode == "EMB")
        {
            if (rbtSearchBy.SelectedValue == "1")
            {
                ds = (new DataLayer()).get_EMB_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_EMB_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }

            if (AllClasses.CheckDataSet(ds))
            {
                sp_HeaderText.InnerHtml = "EMB TAT Analysis";
                hf_ClickMode.Value = "TAT EMB";
                hf_Org_Id.Value = Org_Id.ToString();
                hf_Designation_Id.Value = Designation_Id.ToString();
                grdExpertWiseDtls.DataSource = ds.Tables[0];
                grdExpertWiseDtls.DataBind();

                grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
                grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
                mpViewSummeryExpert.Show();
            }
            else
            {
                grdExpertWiseDtls.DataSource = null;
                grdExpertWiseDtls.DataBind();
                MessageBox.Show("No Records Found");
            }
        }
        else
        {
            if (rbtSearchBy.SelectedValue == "1")
            {
                ds = (new DataLayer()).get_Invoice_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_Invoice_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }

            if (AllClasses.CheckDataSet(ds))
            {
                sp_HeaderText.InnerHtml = "TAT Analysis";
                hf_ClickMode.Value = "TAT";
                hf_Org_Id.Value = Org_Id.ToString();
                hf_Designation_Id.Value = Designation_Id.ToString();
                grdExpertWiseDtls.DataSource = ds.Tables[0];
                grdExpertWiseDtls.DataBind();

                grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
                grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
                mpViewSummeryExpert.Show();
            }
            else
            {
                grdExpertWiseDtls.DataSource = null;
                grdExpertWiseDtls.DataBind();
                MessageBox.Show("No Records Found");
            }
        }
    }

    protected void btnOpenTimelineE_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string PackageEMB_Master_Id = gr.Cells[0].Text.Trim();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMBApproval_History(0, PackageEMB_Master_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimeline_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_InvoiceApproval_History_Combined(Invoice_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdTimeLine_PreRender(object sender, EventArgs e)
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

    protected void lnkTATWiseF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis All";
            hf_ClickMode.Value = "TAT All";
            hf_Org_Id.Value = "0";
            hf_Designation_Id.Value = "0";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTATWiseADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_ADP_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_ADP_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis Other Depertmental Payments";
            hf_ClickMode.Value = "TAT ADP";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkInfoExpADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        get_Invoice_ADP_On_Expert(4, Org_Id, Designation_Id);
    }

    protected void lnkRejectionWiseADP_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_ADP_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_ADP_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Other Dept Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise ADP";
            hf_Org_Id.Value = "-2";
            hf_Designation_Id.Value = "-2";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTATWiseADPF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_ADP_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_ADP_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis Other Depertmental Payments All";
            hf_ClickMode.Value = "TAT ADP All";
            hf_Org_Id.Value = "0";
            hf_Designation_Id.Value = "0";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTATWiseEF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_EMB_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_EMB_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "EMB TAT Analysis All";
            hf_ClickMode.Value = "TAT EMB All";
            hf_Org_Id.Value = "0";
            hf_Designation_Id.Value = "0";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdMAInvDash_PreRender(object sender, EventArgs e)
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
    protected void grdDRInvDash_PreRender(object sender, EventArgs e)
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
    protected void grdMAV_PreRender(object sender, EventArgs e)
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
    protected void grdDRV_PreRender(object sender, EventArgs e)
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
    protected void grdMAInvDash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");

            LinkButton lnkRejectionWiseMA = e.Row.FindControl("lnkRejectionWiseMA") as LinkButton;
            LinkButton lnkInfoExpMA = e.Row.FindControl("lnkInfoExpMA") as LinkButton;

            int Org_Id = 0;
            int Designation_Id = 0;
            try
            {
                Org_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            }
            catch
            {
                Org_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                Designation_Id = 0;
            }
            if (Org_Id == 2 && Designation_Id == 33)
            {
                lnkInfoExpMA.Visible = true;
                lnkRejectionWiseMA.Visible = false;
            }
            else if (Org_Id == 1 && Designation_Id == 33)
            {
                lnkInfoExpMA.Visible = true;
                lnkRejectionWiseMA.Visible = false;
            }
            else if (Org_Id == -1 && Designation_Id == -1)
            {
                lnkInfoExpMA.Visible = true;
                lnkRejectionWiseMA.Visible = false;
            }
            else if (Org_Id == -2 && Designation_Id == -2)
            {
                lnkInfoExpMA.Visible = true;
                lnkRejectionWiseMA.Visible = true;
            }
            else
            {
                lnkInfoExpMA.Visible = false;
                lnkRejectionWiseMA.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void lnkTotalInvoiceMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdMAInvDash.Rows.Count; i++)
        {
            grdMAInvDash.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int Designation_Id = 0;
        int Org_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        get_tbl_Package_MA_Invoice(Org_Id, Designation_Id);
    }
    private void get_tbl_Package_MA_Invoice(int Org_Id, int Designation_Id)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", 0, 0, 0, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, 0, 0, 0, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdMAV.DataSource = ds.Tables[0];
            grdMAV.DataBind();
        }
        else
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }
    protected void lnkTATWiseMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_MA_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_MA_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis Mobilization Advance / Designe and Drawing Payments";
            hf_ClickMode.Value = "TAT MA";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void get_Invoice_MA_Details_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Pending MA")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise MA")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        int TAT_Range = -1;

        if (DataMode == "TAT MA" || DataMode == "TAT MA All")
        {
            TAT_Range = Id;
        }
        else
        {
            TAT_Range = -1;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdMAV.DataSource = ds.Tables[0];
            grdMAV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();
        }
    }
    protected void lnkInfoExpMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        get_Invoice_MA_On_Expert(4, Org_Id, Designation_Id);
    }
    private void get_Invoice_MA_On_Expert(int TAT_Range, int Org_Id, int Designation_Id)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        ds = (new DataLayer()).get_Invoice_MA_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, Org_Id, Designation_Id, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Expert Wise  Mobilization Advance / Designe and Drawing Invoice Count";
            hf_ClickMode.Value = "Expert Pending MA";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkRejectionWiseMA_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_MA_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_MA_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Mobilization Advance / Designe and Drawing Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise MA";
            hf_Org_Id.Value = "-2";
            hf_Designation_Id.Value = "-2";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTATWiseMAF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_MA_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_MA_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis Mobilization Advance / Designe and Drawing Payments All";
            hf_ClickMode.Value = "TAT MA All";
            hf_Org_Id.Value = "0";
            hf_Designation_Id.Value = "0";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdDRInvDash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");

            LinkButton lnkRejectionWiseDR = e.Row.FindControl("lnkRejectionWiseDR") as LinkButton;
            LinkButton lnkInfoExpDR = e.Row.FindControl("lnkInfoExpDR") as LinkButton;

            int Org_Id = 0;
            int Designation_Id = 0;
            try
            {
                Org_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            }
            catch
            {
                Org_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                Designation_Id = 0;
            }
            if (Org_Id == 2 && Designation_Id == 33)
            {
                lnkInfoExpDR.Visible = true;
                lnkRejectionWiseDR.Visible = false;
            }
            else if (Org_Id == 1 && Designation_Id == 33)
            {
                lnkInfoExpDR.Visible = true;
                lnkRejectionWiseDR.Visible = false;
            }
            else if (Org_Id == -1 && Designation_Id == -1)
            {
                lnkInfoExpDR.Visible = true;
                lnkRejectionWiseDR.Visible = false;
            }
            else if (Org_Id == -2 && Designation_Id == -2)
            {
                lnkInfoExpDR.Visible = true;
                lnkRejectionWiseDR.Visible = true;
            }
            else
            {
                lnkInfoExpDR.Visible = false;
                lnkRejectionWiseDR.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void lnkTotalInvoiceDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdDRInvDash.Rows.Count; i++)
        {
            grdDRInvDash.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int Designation_Id = 0;
        int Org_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        get_tbl_Package_DR_Invoice(Org_Id, Designation_Id);
    }
    private void get_tbl_Package_DR_Invoice(int Org_Id, int Designation_Id)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", 0, 0, 0, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, 0, 0, 0, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = ds.Tables[0];
            grdDRV.DataBind();
        }
        else
        {
            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();
        }
    }
    protected void lnkTATWiseDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_DR_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_DR_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis Deduction Release Payments";
            hf_ClickMode.Value = "TAT DR";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    private void get_Invoice_DR_Details_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Pending DR")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise DR")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        int TAT_Range = -1;

        if (DataMode == "TAT DR" || DataMode == "TAT DR All")
        {
            TAT_Range = Id;
        }
        else
        {
            TAT_Range = -1;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdDRV.DataSource = ds.Tables[0];
            grdDRV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdEMB.DataSource = null;
            grdEMB.DataBind();
        }
    }
    protected void lnkInfoExpDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        get_Invoice_DR_On_Expert(4, Org_Id, Designation_Id);
    }
    private void get_Invoice_DR_On_Expert(int TAT_Range, int Org_Id, int Designation_Id)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        ds = (new DataLayer()).get_Invoice_DR_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, Org_Id, Designation_Id, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Expert Wise Deduction Release Invoice Count";
            hf_ClickMode.Value = "Expert Pending DR";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkRejectionWiseDR_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_DR_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_DR_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Deduction Release Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise DR";
            hf_Org_Id.Value = "-2";
            hf_Designation_Id.Value = "-2";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTATWiseDRF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_DR_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_DR_TAT_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, 0, 0, Scheme_Id, 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "TAT Analysis Deduction Release Payments All";
            hf_ClickMode.Value = "TAT DR All";
            hf_Org_Id.Value = "0";
            hf_Designation_Id.Value = "0";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        get_Zone_Wise_EMB_Invoice_Dashboard_Report();
    }

    protected void grdExpertWiseDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void lnkEMBTotalPF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        get_tbl_PackageEMB(true, 0, 0, Scheme_Id, 0);
    }

    protected void btnOpenTimelineADP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_ADP_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageADPApproval_History(Package_ADP_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimelineDR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int DeductionRelease_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageDRApproval_History(DeductionRelease_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimelineMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int MobilizationAdvance_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageMAApproval_History(MobilizationAdvance_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkStep_1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 1);
    }

    private void get_Step_Data(int Zone_Id, int step_Count)
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Step_Status(Scheme_Id, 0, Zone_Id, 0, 0, 0, step_Count);
        if (AllClasses.CheckDataSet(ds))
        {
            grdProjects.DataSource = ds.Tables[0];
            grdProjects.DataBind();
            mpMISStatus.Show();
        }
        else
        {
            grdProjects.DataSource = null;
            grdProjects.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkStep_1_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 1);
    }

    protected void grdProjects_PreRender(object sender, EventArgs e)
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

    protected void lnkStep_2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 2);
    }

    protected void lnkStep_2_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 2);
    }

    protected void lnkStep_3_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 3);
    }

    protected void lnkStep_3_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 3);
    }

    protected void lnkStep_4_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 4);
    }

    protected void lnkStep_4_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 4);
    }

    protected void lnkStep_5_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 5);
    }

    protected void lnkStep_5_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 5);
    }

    protected void lnkStep_6_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 6);
    }

    protected void lnkStep_6_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 6);
    }

    protected void lnkStep_7_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 7);
    }

    protected void lnkStep_7_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 7);
    }

    protected void lnkStep_0_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 0);
    }

    protected void lnkStep_0_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 0);
    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = null;
        for (int i = 0; i < grdDataUpdationStatusReport.Rows.Count; i++)
        {
            RadioButton rbtChoose = grdDataUpdationStatusReport.Rows[i].FindControl("rbtChoose") as RadioButton;
            if (rbtChoose.Checked)
            {
                gr = grdDataUpdationStatusReport.Rows[i];
            }
        }

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (ViewState["isChecked"] != null && bool.Parse(ViewState["isChecked"].ToString()) == true)
        {
            try
            {
                Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        string Scheme_Id = "";
        string Scheme_Name = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
                Scheme_Name += listItem.Text + "_";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        DateTime dt;
        if (rbtSearchBy.SelectedValue == "1")
        {
            dt = DateTime.Now;
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, -1, false, "", "");
        }
        else
        {
            dt = DateTime.ParseExact(txtDateTill.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, -1, false, txtDateFrom.Text, txtDateTill.Text);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["Project Name"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Project Name"].ToString()).Trim();
                try
                {
                    ds.Tables[0].Rows[i]["Project Description"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Project Description"].ToString()).Trim();
                }
                catch
                { }
                try
                {
                    ds.Tables[0].Rows[i]["Issue"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Issue"].ToString()).Trim();
                }
                catch
                { }
            }
            string Name = "PMIS_" + Scheme_Name.Replace("State Sector", "State").Replace("DEPOSIT WORK", "DEPOSIT").Replace("1.0", "").Replace("2.0", "") + dt.Month.ToString().PadLeft(2, '0') + "_" + dt.Day.ToString().PadLeft(2, '0') + "_" + dt.Year.ToString() + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            if (Name.Length > 25)
            {
                Name = "PMIS_" + Scheme_Id.Replace(",", "_").Trim() + dt.Month.ToString().PadLeft(2, '0') + "_" + dt.Day.ToString().PadLeft(2, '0') + "_" + dt.Year.ToString() + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0], Name);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdDataUpdationStatusReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Zone"].ToString();
        }
    }

    protected void rbtChoose_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rbtChoose = sender as RadioButton;
        for (int i = 0; i < grdDataUpdationStatusReport.Rows.Count; i++)
        {
            (grdDataUpdationStatusReport.Rows[i].FindControl("rbtChoose") as RadioButton).Checked = false;
        }
        rbtChoose.Checked = true;
        ViewState["isChecked"] = true;
    }

    protected void btnOpenTimeline1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_InvoiceApproval_History_Combined(Invoice_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimelineADP1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_ADP_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageADPApproval_History(Package_ADP_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimelineMA1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int MobilizationAdvance_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageMAApproval_History(MobilizationAdvance_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimelineDR1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int DeductionRelease_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageDRApproval_History(DeductionRelease_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTotalInvoice_Click1(object sender, EventArgs e)
    {
        grdEMB.DataSource = null;
        grdEMB.DataBind();

        grdInvoiceDashV.DataSource = null;
        grdInvoiceDashV.DataBind();

        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        get_tbl_PackageInvoice(0, 0, Scheme_Id, grdInvoiceDashV, false, 1);
    }

    protected void btnRecommend_Click(object sender, EventArgs e)
    {

    }

    protected void lnkZoneWiseDtls_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[6].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_Zone_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_Zone_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = Session["Default_Zone"].ToString() + " Analysis";
            hf_ClickMode.Value = "Zone";
            hf_Org_Id.Value = Org_Id.ToString();
            hf_Designation_Id.Value = Designation_Id.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void btnMIS_Click(object sender, EventArgs e)
    {
        Response.Redirect("Report_PMIS_Dump.aspx");
    }
    //protected void btnMIS_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("MasterProjectWorkMIS.aspx");
    //}

    protected void lnkZoneWiseDtlsE_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Mode = gr.Cells[8].Text.Trim();
        int Org_Id = 0;
        int Designation_Id = 0;
        try
        {
            Org_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Org_Id = 0;
        }
        try
        {
            Designation_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Designation_Id = 0;
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[7].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
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
        if (Mode == "Invoice")
        {
            if (rbtSearchBy.SelectedValue == "1")
            {
                ds = (new DataLayer()).get_Invoice_Zone_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_Invoice_Zone_Wise_Analysis(Zone_Id, Circle_Id, Division_Id, Org_Id, Designation_Id, Scheme_Id.ToString(), 5, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }

            if (AllClasses.CheckDataSet(ds))
            {
                sp_HeaderText.InnerHtml = Session["Default_Zone"].ToString() + " Analysis";
                hf_ClickMode.Value = "Zone";
                hf_Org_Id.Value = Org_Id.ToString();
                hf_Designation_Id.Value = Designation_Id.ToString();
                grdExpertWiseDtls.DataSource = ds.Tables[0];
                grdExpertWiseDtls.DataBind();

                grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
                grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
                mpViewSummeryExpert.Show();
            }
            else
            {
                grdExpertWiseDtls.DataSource = null;
                grdExpertWiseDtls.DataBind();
                MessageBox.Show("No Records Found");
            }
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

    protected void mvDashboard_ActiveViewChanged(object sender, EventArgs e)
    {

    }

    protected void btnView1_Click(object sender, EventArgs e)
    {
        load_dashboard_View1();
    }

    protected void btnView2_Click(object sender, EventArgs e)
    {
        load_dashboard_View2();
    }

    protected void btnView3_Click(object sender, EventArgs e)
    {
        load_dashboard_View3();
    }

    protected void btnView4_Click(object sender, EventArgs e)
    {
        load_dashboard_View4();
    }

    protected void btnView5_Click(object sender, EventArgs e)
    {
        load_dashboard_View5();
    }

    protected void grdInvoiceDashV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[13].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[14].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdADPV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdMAV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdDRV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdPkgView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdProjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }

    protected void GridproView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }
}