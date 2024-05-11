using Aspose.Pdf;
using Aspose.Pdf.Operators;
using ClosedXML.Excel;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_PMIS_Dump : System.Web.UI.Page
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

            get_Year_Data();
            get_tbl_Project();
            get_tbl_Zone();
            get_M_Jurisdiction();
            get_NodalDepartment();
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divNodal.Visible = true;
            }
            else
            {
                divNodal.Visible = false;
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
            int Issue_Id = 0;
            int Zone_Id = 0;
            int Slab_Id = 0;
            int Program_Id = 0;
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Program_Id = Convert.ToInt32(Request.QueryString["Program_Id"].ToString());
                }
                catch
                {
                    Program_Id = 0;
                }
                try
                {
                    Issue_Id = Convert.ToInt32(Request.QueryString["IssueId"].ToString());
                }
                catch
                {
                    Issue_Id = 0;
                }
                try
                {
                    Slab_Id = Convert.ToInt32(Request.QueryString["Slab_Id"].ToString());
                }
                catch
                {
                    Slab_Id = 0;
                }
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["ZoneId"].ToString());
                    ddlZone.SelectedValue = Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                }
                catch
                {
                    Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
                }
                get_tbl_ProjectWork_Data_Dump_PMIS(Issue_Id, Slab_Id, Program_Id);
            }
        }
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
    private void get_NodalDepartment()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee("12", 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlNodalDepartment, "Person_Name", "Person_Id");
        }
        else
        {
            ddlNodalDepartment.Items.Clear();
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
    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
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
        string[] sanction_Year = ddlSanctionYear.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year != null && sanction_Year.Length == 2)
        {
            FromDate = sanction_Year[0].Replace("-", "");
            TillDate = sanction_Year[1].Replace("-", "");
        }
        string Scheme_Name = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Name += listItem.Text + "_";
            }
        }

        DataSet ds = new DataSet();
        DateTime dt;
        dt = DateTime.Now;
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "ULB")
        {
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_PMS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, false, FromDate, TillDate);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, -1, false, "", "");
        }
        if (AllClasses.CheckDataSet(ds))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    ds.Tables[0].Rows[i]["Project Name"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Project Name"].ToString()).Trim();
                }
                catch
                { }
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
                try
                {
                    ds.Tables[0].Rows[i]["Vendor"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Vendor"].ToString()).Trim();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Issue_Id = 0;
        int Zone_Id = 0;
        int Slab_Id = 0;
        int Program_Id = 0;
        if (Request.QueryString.Count > 0)
        {
            try
            {
                Program_Id = Convert.ToInt32(Request.QueryString["Program_Id"].ToString());
            }
            catch
            {
                Program_Id = 0;
            }
            try
            {
                Issue_Id = Convert.ToInt32(Request.QueryString["IssueId"].ToString());
            }
            catch
            {
                Issue_Id = 0;
            }
            try
            {
                Slab_Id = Convert.ToInt32(Request.QueryString["Slab_Id"].ToString());
            }
            catch
            {
                Slab_Id = 0;
            }
            try
            {
                Zone_Id = Convert.ToInt32(Request.QueryString["ZoneId"].ToString());
                ddlZone.SelectedValue = Zone_Id.ToString();
                ddlZone_SelectedIndexChanged(ddlZone, e);
            }
            catch
            {
                Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
            }
        }
        get_tbl_ProjectWork_Data_Dump_PMIS(Issue_Id, Slab_Id, Program_Id);
    }

    protected void get_tbl_ProjectWork_Data_Dump_PMIS(int Issue_Id, int Slab_Id, int Program_Id)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int NodalDepartment_Id = 0;
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
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        string[] sanction_Year = ddlSanctionYear.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year != null && sanction_Year.Length == 2)
        {
            FromDate = sanction_Year[0].Replace("-", "");
            TillDate = sanction_Year[1].Replace("-", "");
        }
        DataSet ds = new DataSet();

        ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_PMIS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, Issue_Id, Slab_Id, Program_Id, ULB_Id, txtProjectCode.Text.Trim(), NodalDepartment_Id, FromDate, TillDate);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string jsonString = e.Row.Cells[4].Text.Trim().Replace("&nbsp;", "").Replace("&quot;", "\"");
            if (jsonString != "")
            {
                GridView grdPhysicalComponent = e.Row.FindControl("grdPhysicalComponent") as GridView;
                PhysicalComponentRoot onj_PhysicalComponentRoot = new PhysicalComponentRoot();
                onj_PhysicalComponentRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<PhysicalComponentRoot>(jsonString);
                if (onj_PhysicalComponentRoot != null && onj_PhysicalComponentRoot.Physical_Component != null && onj_PhysicalComponentRoot.Physical_Component.Count > 0)
                {
                    grdPhysicalComponent.DataSource = onj_PhysicalComponentRoot.Physical_Component;
                    grdPhysicalComponent.DataBind();
                }
                else
                {
                    grdPhysicalComponent.DataSource = null;
                    grdPhysicalComponent.DataBind();
                }
            }
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
    protected void Export_Data_One_Pager(List<One_Pager_Lite> obj_One_Pager_Lite_Li)
    {
        
    }
    protected void lnkOnePager_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int NodalDepartment_Id = 0;
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
        try
        {
            NodalDepartment_Id = Convert.ToInt32(ddlNodalDepartment.SelectedValue);
        }
        catch
        {
            NodalDepartment_Id = 0;
        }
        string[] sanction_Year = ddlSanctionYear.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year != null && sanction_Year.Length == 2)
        {
            FromDate = sanction_Year[0].Replace("-", "");
            TillDate = sanction_Year[1].Replace("-", "");
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_One_Pager(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, txtProjectCode.Text.Trim(), false);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            List<One_Pager_Lite> obj_One_Pager_Lite_Li = new List<One_Pager_Lite>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                One_Pager_Lite obj_One_Pager_Lite = new One_Pager_Lite();
                obj_One_Pager_Lite.Project_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_One_Pager_Lite.Project_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_One_Pager_Lite.Project_Type = ds.Tables[0].Rows[i]["ProjectType_Name"].ToString();
                obj_One_Pager_Lite.District = ds.Tables[0].Rows[i]["Jurisdiction_Name_Eng"].ToString();
                obj_One_Pager_Lite.Person_CE = ds.Tables[0].Rows[i]["ZCE_Person_Contact"].ToString();
                obj_One_Pager_Lite.Person_SE = ds.Tables[0].Rows[i]["SE_Person_Contact"].ToString();
                obj_One_Pager_Lite.Person_EE = ds.Tables[0].Rows[i]["Person_Contact"].ToString();
                obj_One_Pager_Lite.Sanctioned_Cost = ds.Tables[0].Rows[i]["ProjectWork_Budget"].ToString();
                obj_One_Pager_Lite.Work_Cost = ds.Tables[0].Rows[i]["Work_Cost"].ToString();
                obj_One_Pager_Lite.Centage = ds.Tables[0].Rows[i]["Centage"].ToString();
                obj_One_Pager_Lite.ULB_Share_GO = ds.Tables[0].Rows[i]["ULB_Share"].ToString();
                obj_One_Pager_Lite.Agreement_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Start_Date"].ToString();
                obj_One_Pager_Lite.End_Date_Agreement = ds.Tables[0].Rows[i]["Target_Date_Agreement"].ToString();
                obj_One_Pager_Lite.Extended_Date = ds.Tables[0].Rows[i]["Target_Date_Agreement_Extended"].ToString();
                obj_One_Pager_Lite.Tender_Cost = ds.Tables[0].Rows[i]["Tender_Cost_1"].ToString();
                obj_One_Pager_Lite.Tender_Cost_GST = ds.Tables[0].Rows[i]["Tender_Cost"].ToString();
                obj_One_Pager_Lite.ULB_Share_Tender_Cost = ds.Tables[0].Rows[i]["ULB_Share_As_Per_TenderCost"].ToString();

                //obj_One_Pager_Lite.Installment_1_Central_State = ds.Tables[0].Rows[i]["Instalment_1_state_central"].ToString();
                //obj_One_Pager_Lite.Installment_2_Central_State = ds.Tables[0].Rows[i]["Instalment_2_state_central"].ToString();
                //obj_One_Pager_Lite.Installment_3_Central_State = ds.Tables[0].Rows[i]["Instalment_3_state_central"].ToString();
                //obj_One_Pager_Lite.Installment_Central_State = ds.Tables[0].Rows[i]["Instalment_state_central"].ToString();

                obj_One_Pager_Lite.Installment_1_Central_State = ds.Tables[0].Rows[i]["GO_CentralShare_1"].ToString();
                obj_One_Pager_Lite.Installment_2_Central_State = ds.Tables[0].Rows[i]["GO_CentralShare_2"].ToString();
                obj_One_Pager_Lite.Installment_3_Central_State = ds.Tables[0].Rows[i]["GO_CentralShare_3"].ToString();
                obj_One_Pager_Lite.Installment_Central_State = ds.Tables[0].Rows[i]["GO_CentralShare"].ToString();

                obj_One_Pager_Lite.Additional_Field_1 = ds.Tables[0].Rows[i]["GO_StateShare_1"].ToString();
                obj_One_Pager_Lite.Additional_Field_2 = ds.Tables[0].Rows[i]["GO_StateShare_2"].ToString();
                obj_One_Pager_Lite.Additional_Field_3 = ds.Tables[0].Rows[i]["GO_StateShare_3"].ToString();
                obj_One_Pager_Lite.Additional_Field_4 = ds.Tables[0].Rows[i]["GO_StateShare"].ToString();

                obj_One_Pager_Lite.Additional_Field_5 = ds.Tables[0].Rows[i]["GO_Centage_1"].ToString();
                obj_One_Pager_Lite.Additional_Field_6 = ds.Tables[0].Rows[i]["GO_Centage_2"].ToString();
                obj_One_Pager_Lite.Additional_Field_7 = ds.Tables[0].Rows[i]["GO_Centage_3"].ToString();
                obj_One_Pager_Lite.Additional_Field_8 = ds.Tables[0].Rows[i]["GO_Centage"].ToString();

                obj_One_Pager_Lite.Installment_ULB = ds.Tables[0].Rows[i]["ULB_Share_Received"].ToString();


                decimal Tender_Cost_GST = 0;
                decimal Share_Calculation = 0;
                decimal ULB_Share_Received = 0;
                decimal ULB_Share_Tender_Cost = 0;
                try
                {
                    Share_Calculation = Convert.ToDecimal(ds.Tables[0].Rows[i]["Share_Calculation"].ToString());
                }
                catch
                {
                    Share_Calculation = 0;
                }
                try
                {
                    ULB_Share_Received = Convert.ToDecimal(ds.Tables[0].Rows[i]["ULB_Share_Received"].ToString());
                }
                catch
                {
                    ULB_Share_Received = 0;
                }
                try
                {
                    Tender_Cost_GST = Convert.ToDecimal(obj_One_Pager_Lite.Tender_Cost_GST);
                }
                catch
                {
                    Tender_Cost_GST = 0;
                }
                try
                {
                    ULB_Share_Tender_Cost = Convert.ToDecimal(obj_One_Pager_Lite.ULB_Share_Tender_Cost);
                }
                catch
                {
                    ULB_Share_Tender_Cost = 0;
                }
                decimal ULB_Share_Work_Cost = 0;
                try
                {
                    ULB_Share_Work_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["ULB_Share_As_Per_WorkCost"].ToString());
                }
                catch
                {
                    ULB_Share_Work_Cost = 0;
                }
                decimal Work_Cost = 0;
                try
                {
                    Work_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Work_Cost"].ToString());
                }
                catch
                {
                    Work_Cost = 0;
                }

                if (Tender_Cost_GST > Work_Cost)
                {
                    obj_One_Pager_Lite.Total_Funds_Available = decimal.Round(((Share_Calculation * Work_Cost) / 100) + ULB_Share_Received, 2, MidpointRounding.AwayFromZero).ToString();
                    obj_One_Pager_Lite.ULB_Share_Balance = decimal.Round(ULB_Share_Work_Cost - ULB_Share_Received, 2, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    obj_One_Pager_Lite.Total_Funds_Available = decimal.Round(((Share_Calculation * Tender_Cost_GST) / 100) + ULB_Share_Received, 2, MidpointRounding.AwayFromZero).ToString();
                    obj_One_Pager_Lite.ULB_Share_Balance = decimal.Round(ULB_Share_Tender_Cost - ULB_Share_Received, 2, MidpointRounding.AwayFromZero).ToString();
                }
                //obj_One_Pager_Lite.ULB_Share_Balance = decimal.Round(ULB_Share_Tender_Cost - ULB_Share_Received, 2, MidpointRounding.AwayFromZero).ToString();
                obj_One_Pager_Lite.Total_Expenditure = ds.Tables[0].Rows[i]["Total_Expenditure_Till_Date"].ToString();
                obj_One_Pager_Lite.Physical_Progress = ds.Tables[0].Rows[i]["Physical_Progress"].ToString();
                decimal Financial_Progress = 0;
                try
                {
                    Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                }
                catch
                {
                    Financial_Progress = 0;
                }
                if (Financial_Progress > 100)
                {
                    Financial_Progress = 100;
                }
                obj_One_Pager_Lite.Financial_Progress = Financial_Progress.ToString();
                if (ds.Tables[0].Rows[i]["Physical_Handover_Done"].ToString().Trim() == "")
                {
                    obj_One_Pager_Lite.Physical_Closure = "No";
                }
                else
                {
                    obj_One_Pager_Lite.Physical_Closure = ds.Tables[0].Rows[i]["Physical_Handover_Done"].ToString();
                }
                if (ds.Tables[0].Rows[i]["Financial_Handover_Done"].ToString().Trim() == "")
                {
                    obj_One_Pager_Lite.Financial_closure = "No";
                }
                else
                {
                    obj_One_Pager_Lite.Financial_closure = ds.Tables[0].Rows[i]["Financial_Handover_Done"].ToString();
                }
                obj_One_Pager_Lite.Vendor = ds.Tables[0].Rows[i]["Vendor"].ToString();
                obj_One_Pager_Lite.Issues = ds.Tables[0].Rows[i]["Issue"].ToString();

                obj_One_Pager_Lite_Li.Add(obj_One_Pager_Lite);
            }
            Export_Data_One_Pager(obj_One_Pager_Lite_Li);
        }
        else
        {
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnMPR_Click(object sender, ImageClickEventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
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
        string[] sanction_Year = ddlSanctionYear.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year != null && sanction_Year.Length == 2)
        {
            FromDate = sanction_Year[0].Replace("-", "");
            TillDate = sanction_Year[1].Replace("-", "");
        }
        string Scheme_Name = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Name += listItem.Text + "_";
            }
        }

        DataSet ds = new DataSet();
        string Client = ConfigurationManager.AppSettings.Get("Client");
        ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MPR(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            List<MPR_Detailed> obj_MPR_Detailed_Li = new List<MPR_Detailed>();
            List<MPR_Component> obj_MPR_Component_Li = new List<MPR_Component>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MPR_Detailed obj_MPR_Detailed = new MPR_Detailed();
                
                obj_MPR_Detailed.Project_Name = ds.Tables[0].Rows[i]["Project Name"].ToString();
                obj_MPR_Detailed.Project_Code = ds.Tables[0].Rows[i]["Project Code"].ToString();
                obj_MPR_Detailed.Zone_Name = ds.Tables[0].Rows[i]["Zone"].ToString();
                obj_MPR_Detailed.Circle_Name = ds.Tables[0].Rows[i]["Circle"].ToString();
                obj_MPR_Detailed.Division_Name = ds.Tables[0].Rows[i]["Division"].ToString();
                obj_MPR_Detailed.Project_Type = ds.Tables[0].Rows[i]["Project Type"].ToString();

                obj_MPR_Detailed.District = ds.Tables[0].Rows[i]["Town"].ToString();
                obj_MPR_Detailed.Person_CE = ds.Tables[0].Rows[i]["Concern ZCE"].ToString();
                obj_MPR_Detailed.Person_SE = ds.Tables[0].Rows[i]["Concern SE"].ToString();
                obj_MPR_Detailed.Person_EE = ds.Tables[0].Rows[i]["Concern EE"].ToString();
                try
                {
                    obj_MPR_Detailed.Sanctioned_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total Cost Including Centage (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Work_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Work Cost (In Lakhs)"].ToString());
                }
                catch
                { }
                obj_MPR_Detailed.Agreement_Date = ds.Tables[0].Rows[i]["Agreement / Date Of Start"].ToString();
                obj_MPR_Detailed.End_Date_Agreement = ds.Tables[0].Rows[i]["Completion Date As Per Agreement"].ToString();
                obj_MPR_Detailed.Extended_Date = ds.Tables[0].Rows[i]["Completion Date As Extended"].ToString();
                try
                {
                    obj_MPR_Detailed.Tender_Cost_WithOut_GST = Convert.ToDecimal(ds.Tables[0].Rows[i]["Tender Cost Excluding GST (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Tender_Cost_With_GST = Convert.ToDecimal(ds.Tables[0].Rows[i]["Tender Cost Including GST (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_1_Central_State = Convert.ToDecimal(ds.Tables[0].Rows[i]["Central + State Share 1st Instalment (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_1_Centage = Convert.ToDecimal(ds.Tables[0].Rows[i]["Centage 1st Instalment (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_2_Central_State = Convert.ToDecimal(ds.Tables[0].Rows[i]["Central + State Share 2nd Instalment (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_2_Centage = Convert.ToDecimal(ds.Tables[0].Rows[i]["Centage 2nd Instalment (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_3_Central_State = Convert.ToDecimal(ds.Tables[0].Rows[i]["Central + State Share 3rd Instalment (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_3_Centage = Convert.ToDecimal(ds.Tables[0].Rows[i]["Centage 3rd Instalment (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_Central_State = Convert.ToDecimal(ds.Tables[0].Rows[i]["Work  (Central +State Share) (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_ULB = Convert.ToDecimal(ds.Tables[0].Rows[i]["ULB Share Released (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Installment_Total_With_ULB = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total (WORK + ULB) (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Total_Cost_With_Centage = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total (With centage) (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total Expenditure Till Date (In Lakhs)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical Progress (%)"].ToString());
                }
                catch
                { }
                try
                {
                    obj_MPR_Detailed.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial Progress (%)"].ToString());
                }
                catch
                { }
                //if (ds.Tables[0].Rows[i]["Physical_Handover_Done"].ToString().Trim() == "")
                //{
                //    obj_MPR_Detailed.Physical_Closure = "No";
                //}
                //else
                //{
                //    obj_MPR_Detailed.Physical_Closure = ds.Tables[0].Rows[i]["Physical_Handover_Done"].ToString();
                //}
                //if (ds.Tables[0].Rows[i]["Financial_Handover_Done"].ToString().Trim() == "")
                //{
                //    obj_MPR_Detailed.Financial_closure = "No";
                //}
                //else
                //{
                //    obj_MPR_Detailed.Financial_closure = ds.Tables[0].Rows[i]["Financial_Handover_Done"].ToString();
                //}
                obj_MPR_Detailed.Vendor = ds.Tables[0].Rows[i]["Vendor"].ToString();
                //obj_MPR_Detailed.Issues = ds.Tables[0].Rows[i]["Issue"].ToString();
                

                string jsonString = ds.Tables[0].Rows[i]["Physical_Component"].ToString();
                if (jsonString != "")
                {
                    PhysicalComponentRoot onj_PhysicalComponentRoot = new PhysicalComponentRoot();
                    onj_PhysicalComponentRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<PhysicalComponentRoot>(jsonString);
                    if (onj_PhysicalComponentRoot != null && onj_PhysicalComponentRoot.Physical_Component != null && onj_PhysicalComponentRoot.Physical_Component.Count > 0)
                    {
                        for (int j = 0; j < onj_PhysicalComponentRoot.Physical_Component.Count; j++)
                        {
                            MPR_Component obj_MPR_Component = new MPR_Component();
                            obj_MPR_Component.Additional_Field_1 = obj_MPR_Detailed.Project_Code;
                            obj_MPR_Component.Component_Name = onj_PhysicalComponentRoot.Physical_Component[j].Component;
                            obj_MPR_Component.Completed = onj_PhysicalComponentRoot.Physical_Component[j].PhysicalProgress;
                            obj_MPR_Component.Functional = onj_PhysicalComponentRoot.Physical_Component[j].Functional;
                            obj_MPR_Component.Praposed_No_Actual = onj_PhysicalComponentRoot.Physical_Component[j].Proposed;
                            obj_MPR_Component.Praposed_No_Origional = onj_PhysicalComponentRoot.Physical_Component[j].Proposed_Origional;
                            obj_MPR_Component.Unit_Name = onj_PhysicalComponentRoot.Physical_Component[j].Unit_Name;
                            obj_MPR_Component.Remarks = onj_PhysicalComponentRoot.Physical_Component[j].Remarks;
                            obj_MPR_Component_Li.Add(obj_MPR_Component);
                        }                        
                    }
                }

                obj_MPR_Detailed_Li.Add(obj_MPR_Detailed);
            }
            Export_Data_MPR(obj_MPR_Detailed_Li, obj_MPR_Component_Li);
        }
        else
        {
            MessageBox.Show("No Records Found");
        }
    }

    protected void Export_Data_MPR(List<MPR_Detailed> obj_MPR_Detailed_Li, List<MPR_Component> obj_MPR_Component_Li)
    {
        
    }
}