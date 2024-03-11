using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_ProjectDocumentNotAvailable : System.Web.UI.Page
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

            if (Request.QueryString.Count > 0)
            {
                string Scheme_Id = "";
                int District_Id = 0;
                int ULB_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;

                try
                {
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        listItem.Selected = false;
                    }
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        if (Scheme_Id == listItem.Value)
                        {
                            listItem.Selected = true;
                            break;
                        }
                    }
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
            }
            get_Document_Not_Available_Dashboard();
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
        get_Document_Not_Available_Dashboard();
    }

    protected void get_Document_Not_Available_Dashboard()
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
        ds = (new DataLayer()).get_Document_Not_Available_Dashboard(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, false);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

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

        ds = new DataSet();
        ds = (new DataLayer()).get_Document_Not_Available_Dashboard(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, true);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Document_NA"] = ds;
        }
        else
        {
            ViewState["Document_NA"] = null;
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
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();
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

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["Document_NA"] != null)
        {
            ds = (DataSet)ViewState["Document_NA"];
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "Document_NA_Summery.pdf";

                List<tbl_Document_NA_Summery> obj_tbl_Document_NA_Summery_Li = new List<tbl_Document_NA_Summery>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    tbl_Document_NA_Summery obj_tbl_Document_NA_Summery = new tbl_Document_NA_Summery();
                    obj_tbl_Document_NA_Summery.Agreement_Stamp = Convert.ToInt32(ds.Tables[1].Rows[i]["CB_Stamp"].ToString());
                    obj_tbl_Document_NA_Summery.BG = Convert.ToInt32(ds.Tables[1].Rows[i]["BG"].ToString());
                    obj_tbl_Document_NA_Summery.CB = Convert.ToInt32(ds.Tables[1].Rows[i]["Agreement"].ToString());
                    obj_tbl_Document_NA_Summery.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Document_NA_Summery.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Document_NA_Summery.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Document_NA_Summery.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Document_NA_Summery.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Document_NA_Summery.CB_Front = Convert.ToInt32(ds.Tables[1].Rows[i]["CB_Front"].ToString());
                    obj_tbl_Document_NA_Summery.Financial_Closure = Convert.ToInt32(ds.Tables[1].Rows[i]["FC"].ToString());
                    obj_tbl_Document_NA_Summery.First_GO = Convert.ToInt32(ds.Tables[1].Rows[i]["GO_1"].ToString());
                    obj_tbl_Document_NA_Summery.LD = Convert.ToInt32(ds.Tables[1].Rows[i]["LD"].ToString());
                    obj_tbl_Document_NA_Summery.LOI = Convert.ToInt32(ds.Tables[1].Rows[i]["LOI"].ToString());
                    obj_tbl_Document_NA_Summery.MA = Convert.ToInt32(ds.Tables[1].Rows[i]["MA"].ToString());
                    obj_tbl_Document_NA_Summery.Package_Count = Convert.ToInt32(ds.Tables[1].Rows[i]["Total_Package"].ToString());
                    obj_tbl_Document_NA_Summery.Physical_Closure = 0;
                    obj_tbl_Document_NA_Summery.PS = Convert.ToInt32(ds.Tables[1].Rows[i]["PS"].ToString());
                    obj_tbl_Document_NA_Summery.Schedule_G = Convert.ToInt32(ds.Tables[1].Rows[i]["Schedule_G"].ToString());
                    obj_tbl_Document_NA_Summery.Second_GO = Convert.ToInt32(ds.Tables[1].Rows[i]["GO_2"].ToString());
                    obj_tbl_Document_NA_Summery.TE = Convert.ToInt32(ds.Tables[1].Rows[i]["TE"].ToString());
                    obj_tbl_Document_NA_Summery.Third_GO = Convert.ToInt32(ds.Tables[1].Rows[i]["GO_3"].ToString());
                    obj_tbl_Document_NA_Summery.Variation = Convert.ToInt32(ds.Tables[1].Rows[i]["Variation"].ToString());
                    obj_tbl_Document_NA_Summery.Header_Text = "PMIS Documents not available - Number of Packages : " + ddlScheme.SelectedItem.Text;
                    obj_tbl_Document_NA_Summery_Li.Add(obj_tbl_Document_NA_Summery);
                }

                string webURI = "";
                if (Page.Request.Url.Query.Trim() == "")
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
                }
                else
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
                }

                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Document_Miss_Summary.rpt"));
                crystalReport.SetDataSource(obj_tbl_Document_NA_Summery_Li);
                crystalReport.Refresh();
                //crystalReport.ReportSource = crystalReport;
                //crystalReport.RefreshReport();
                crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                if (fi.Exists)
                {
                    new AllClasses().Render_PDF_Document(ltEmbed, filePath + fileName);
                    mp1.Show();
                }
                else
                {
                    MessageBox.Show("Unable To Generate Achivment Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Achivment Report.");
                return;
            }
        }
    }

    protected void btnExport2_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["Document_NA"] != null)
        {
            ds = (DataSet)ViewState["Document_NA"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "Document_NA_Detail.pdf";

                List<tbl_Document_NA_Detail> obj_tbl_Document_NA_Detail_Li = new List<tbl_Document_NA_Detail>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tbl_Document_NA_Detail obj_tbl_Document_NA_Detail = new tbl_Document_NA_Detail();
                    obj_tbl_Document_NA_Detail.Agreement_Stamp = Convert.ToInt32(ds.Tables[0].Rows[i]["CB_Stamp"].ToString());
                    obj_tbl_Document_NA_Detail.BG = Convert.ToInt32(ds.Tables[0].Rows[i]["BG"].ToString());
                    obj_tbl_Document_NA_Detail.CB = Convert.ToInt32(ds.Tables[0].Rows[i]["Agreement"].ToString());
                    obj_tbl_Document_NA_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Document_NA_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Document_NA_Detail.Division_Id= Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                    obj_tbl_Document_NA_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Document_NA_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Document_NA_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                    obj_tbl_Document_NA_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                    obj_tbl_Document_NA_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                    obj_tbl_Document_NA_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                    obj_tbl_Document_NA_Detail.CB_Front = Convert.ToInt32(ds.Tables[0].Rows[i]["CB_Front"].ToString());
                    obj_tbl_Document_NA_Detail.Financial_Closure = Convert.ToInt32(ds.Tables[0].Rows[i]["FC"].ToString());
                    obj_tbl_Document_NA_Detail.First_GO = Convert.ToInt32(ds.Tables[0].Rows[i]["GO_1"].ToString());
                    obj_tbl_Document_NA_Detail.LD = Convert.ToInt32(ds.Tables[0].Rows[i]["LD"].ToString());
                    obj_tbl_Document_NA_Detail.LOI = Convert.ToInt32(ds.Tables[0].Rows[i]["LOI"].ToString());
                    obj_tbl_Document_NA_Detail.MA = Convert.ToInt32(ds.Tables[0].Rows[i]["MA"].ToString());
                    obj_tbl_Document_NA_Detail.Package_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Package"].ToString());
                    obj_tbl_Document_NA_Detail.Physical_Closure = 0;
                    obj_tbl_Document_NA_Detail.PS = Convert.ToInt32(ds.Tables[0].Rows[i]["PS"].ToString());
                    obj_tbl_Document_NA_Detail.Schedule_G = Convert.ToInt32(ds.Tables[0].Rows[i]["Schedule_G"].ToString());
                    obj_tbl_Document_NA_Detail.Second_GO = Convert.ToInt32(ds.Tables[0].Rows[i]["GO_2"].ToString());
                    obj_tbl_Document_NA_Detail.TE = Convert.ToInt32(ds.Tables[0].Rows[i]["TE"].ToString());
                    obj_tbl_Document_NA_Detail.Third_GO = Convert.ToInt32(ds.Tables[0].Rows[i]["GO_3"].ToString());
                    obj_tbl_Document_NA_Detail.Variation = Convert.ToInt32(ds.Tables[0].Rows[i]["Variation"].ToString());
                    obj_tbl_Document_NA_Detail.Header_Text = "PMIS Documents not available - Project Details : " + ddlScheme.SelectedItem.Text;
                    obj_tbl_Document_NA_Detail_Li.Add(obj_tbl_Document_NA_Detail);
                }

                string webURI = "";
                if (Page.Request.Url.Query.Trim() == "")
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
                }
                else
                {
                    webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
                }

                ReportDocument crystalReport = new ReportDocument();
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Document_Miss_Descriptive.rpt"));
                crystalReport.SetDataSource(obj_tbl_Document_NA_Detail_Li);
                crystalReport.Refresh();
                //crystalReport.ReportSource = crystalReport;
                //crystalReport.RefreshReport();
                crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                if (fi.Exists)
                {
                    new AllClasses().Render_PDF_Document(ltEmbed, filePath + fileName);
                    mp1.Show();
                }
                else
                {
                    MessageBox.Show("Unable To Generate Achivment Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Achivment Report.");
                return;
            }
        }
    }
}