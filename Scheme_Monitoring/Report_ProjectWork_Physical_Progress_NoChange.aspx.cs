using Aspose.Pdf;
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

public partial class Report_ProjectWork_Physical_Progress_NoChange : System.Web.UI.Page
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
            get_Physical_Progress_No_DataChange();
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
        get_Physical_Progress_No_DataChange();
    }

    protected void get_Physical_Progress_No_DataChange()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";
        int PendencyDays = 0;
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
        try
        {
            PendencyDays = Convert.ToInt32(txtPendencyDays.Text.Trim());
        }
        catch
        {
            PendencyDays = 0;
        }
        DataSet ds = new DataSet();
        
        ds = (new DataLayer()).get_Physical_Progress_No_DataChange(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, chkRemoveSelected.Checked, PendencyDays);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["StagnantPhysical"] = ds;
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
            ViewState["StagnantPhysical"] = null;
            grdPost.DataSource = null;
            grdPost.DataBind();
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
        if (ViewState["StagnantPhysical"] != null)
        {
            ds = (DataSet)ViewState["StagnantPhysical"];
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "StagnantPhysical_Summery.pdf";

                List<tbl_Stagnant_Progress_Physical> obj_tbl_Stagnant_Progress_Physical_Li = new List<tbl_Stagnant_Progress_Physical>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    tbl_Stagnant_Progress_Physical obj_tbl_Stagnant_Progress_Physical = new tbl_Stagnant_Progress_Physical();
                    obj_tbl_Stagnant_Progress_Physical.Data_1 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_1"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_2 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_2"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_3 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_3"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_4 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_4"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_5 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_5"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Total = Convert.ToInt32(ds.Tables[1].Rows[i]["Total"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Physical.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Physical.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Physical.Header_Text = "Stagnant Physical Progress for more than " + txtPendencyDays.Text + " Days - : " + ddlScheme.SelectedItem.Text;
                    obj_tbl_Stagnant_Progress_Physical_Li.Add(obj_tbl_Stagnant_Progress_Physical);
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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Phy_Progress_Summery.rpt"));
                crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Physical_Li);
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
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Report.");
                return;
            }
        }
    }

    protected void btnExport2_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["StagnantPhysical"] != null)
        {
            ds = (DataSet)ViewState["StagnantPhysical"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "StagnantPhysical_Detail.pdf";

                List<tbl_Stagnant_Progress_Detail> obj_tbl_Stagnant_Progress_Detail_Li = new List<tbl_Stagnant_Progress_Detail>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tbl_Stagnant_Progress_Detail obj_tbl_Stagnant_Progress_Detail = new tbl_Stagnant_Progress_Detail();
                    obj_tbl_Stagnant_Progress_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Header_Text = "Stagnant Physical Progress for more than " + txtPendencyDays.Text + " Days - : " + ddlScheme.SelectedItem.Text;
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Stagnant_Progress_Detail.Financial_Progress = 0;
                    }
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Stagnant_Progress_Detail.Physical_Progress = 0;
                    }
                    obj_tbl_Stagnant_Progress_Detail.Last_Invoice_Date = ds.Tables[0].Rows[i]["ProjectWorkFinancialTarget_AddedOn"].ToString();
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = Convert.ToInt32(ds.Tables[0].Rows[i]["Days_Since_Update"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = 0;
                    }
                    obj_tbl_Stagnant_Progress_Detail.Issue_Reported = ds.Tables[0].Rows[i]["Issue"].ToString();
                    obj_tbl_Stagnant_Progress_Detail_Li.Add(obj_tbl_Stagnant_Progress_Detail);
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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Phy_Progress_Detail.rpt"));
                crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Detail_Li);
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
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Report.");
                return;
            }
        }
    }

    protected void btnExportPPT1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["StagnantPhysical"] != null)
        {
            ds = (DataSet)ViewState["StagnantPhysical"];
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "StagnantPhysical_Summery.pdf";

                List<tbl_Stagnant_Progress_Physical> obj_tbl_Stagnant_Progress_Physical_Li = new List<tbl_Stagnant_Progress_Physical>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    tbl_Stagnant_Progress_Physical obj_tbl_Stagnant_Progress_Physical = new tbl_Stagnant_Progress_Physical();
                    obj_tbl_Stagnant_Progress_Physical.Data_1 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_1"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_2 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_2"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_3 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_3"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_4 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_4"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Data_5 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_5"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Total = Convert.ToInt32(ds.Tables[1].Rows[i]["Total"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Physical.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Physical.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Physical.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Physical.Header_Text = "Stagnant Physical Progress for more than " + txtPendencyDays.Text + " Days - : " + ddlScheme.SelectedItem.Text;
                    obj_tbl_Stagnant_Progress_Physical_Li.Add(obj_tbl_Stagnant_Progress_Physical);
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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Phy_Progress_Summery.rpt"));
                crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Physical_Li);
                crystalReport.Refresh();
                //crystalReport.ReportSource = crystalReport;
                //crystalReport.RefreshReport();
                crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                if (fi.Exists)
                {
                    // Load PDF document
                    Document pdfDocument = new Document(fi.FullName);
                    PptxSaveOptions pptxOptions = new PptxSaveOptions();
                    // Save output file
                    pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);
                    //Download File
                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name.Replace(".pdf", ".pptx") + ";");
                    response.TransmitFile(fi.FullName.Replace(".pdf", ".pptx"));
                    response.Flush();
                    response.End();
                }
                else
                {
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Report.");
                return;
            }
        }
    }

    protected void btnExportPPT2_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["StagnantPhysical"] != null)
        {
            ds = (DataSet)ViewState["StagnantPhysical"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "StagnantPhysical_Detail.pdf";

                List<tbl_Stagnant_Progress_Detail> obj_tbl_Stagnant_Progress_Detail_Li = new List<tbl_Stagnant_Progress_Detail>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tbl_Stagnant_Progress_Detail obj_tbl_Stagnant_Progress_Detail = new tbl_Stagnant_Progress_Detail();
                    obj_tbl_Stagnant_Progress_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                    obj_tbl_Stagnant_Progress_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                    obj_tbl_Stagnant_Progress_Detail.Header_Text = "Stagnant Physical Progress for more than " + txtPendencyDays.Text + " Days - : " + ddlScheme.SelectedItem.Text;
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Stagnant_Progress_Detail.Financial_Progress = 0;
                    }
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Stagnant_Progress_Detail.Physical_Progress = 0;
                    }
                    obj_tbl_Stagnant_Progress_Detail.Last_Invoice_Date = ds.Tables[0].Rows[i]["ProjectWorkFinancialTarget_AddedOn"].ToString();
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = Convert.ToInt32(ds.Tables[0].Rows[i]["Days_Since_Update"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = 0;
                    }
                    obj_tbl_Stagnant_Progress_Detail.Issue_Reported = ds.Tables[0].Rows[i]["Issue"].ToString();
                    obj_tbl_Stagnant_Progress_Detail_Li.Add(obj_tbl_Stagnant_Progress_Detail);
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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Phy_Progress_Detail.rpt"));
                crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Detail_Li);
                crystalReport.Refresh();
                //crystalReport.ReportSource = crystalReport;
                //crystalReport.RefreshReport();
                crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

                FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
                if (fi.Exists)
                {
                    // Load PDF document
                    Document pdfDocument = new Document(fi.FullName);
                    PptxSaveOptions pptxOptions = new PptxSaveOptions();
                    // Save output file
                    pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);
                    //Download File
                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "application/octet-stream";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name.Replace(".pdf", ".pptx") + ";");
                    response.TransmitFile(fi.FullName.Replace(".pdf", ".pptx"));
                    response.Flush();
                    response.End();
                }
                else
                {
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Report.");
                return;
            }
        }
    }
}