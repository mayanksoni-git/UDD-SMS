using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Project_With_No_Invoice : System.Web.UI.Page
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
                string Scheme_Id = "";
                int District_Id = 0;
                int ULB_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;

                try
                {
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                    foreach (ListItem listItem in ddlSearchScheme.Items)
                    {
                        listItem.Selected = false;
                    }
                    foreach (ListItem listItem in ddlSearchScheme.Items)
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
                    ddlZone.SelectedValue = Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                    ddlCircle.SelectedValue = Circle_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, e);
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                    ddlDivision.SelectedValue = Division_Id.ToString();
                }
                catch
                {
                    Division_Id = 0;
                }
            }

            btnSearch_Click(btnSearch, e);
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
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchScheme, "Project_Name", "Project_Id");
            try
            {
                ddlSearchScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlSearchScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
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
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        int Pendency_Days = 0;
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
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
            Pendency_Days = Convert.ToInt32(txtPendencyDays.Text);
        }
        catch
        {
            Pendency_Days = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Work_With_No_Invoicing(Project_Id, Zone_Id, Circle_Id, Division_Id, chkRemoveFinancialAll.Checked, chkRemoveFinancialPartial.Checked, Pendency_Days);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["StagnantFinancial"] = ds;
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
        }
        else
        {
            ViewState["StagnantFinancial"] = null;
            divData.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ViewState["StagnantFinancial"] != null)
        {
            ds = (DataSet)ViewState["StagnantFinancial"];
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "StagnantFinancial_Summery.pdf";

                List<tbl_Stagnant_Progress> obj_tbl_Stagnant_Progress_Li = new List<tbl_Stagnant_Progress>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    tbl_Stagnant_Progress obj_tbl_Stagnant_Progress = new tbl_Stagnant_Progress();
                    obj_tbl_Stagnant_Progress.OnGoing = Convert.ToInt32(ds.Tables[1].Rows[i]["OnGoing"].ToString());
                    obj_tbl_Stagnant_Progress.Completed = Convert.ToInt32(ds.Tables[1].Rows[i]["Completed"].ToString());
                    obj_tbl_Stagnant_Progress.Total = Convert.ToInt32(ds.Tables[1].Rows[i]["Total"].ToString());
                    obj_tbl_Stagnant_Progress.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Stagnant_Progress.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Stagnant_Progress.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Stagnant_Progress.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Stagnant_Progress.Header_Text = "Stagnant Financial Progress for more than " + txtPendencyDays.Text + " Days - : " + ddlSearchScheme.SelectedItem.Text;
                    obj_tbl_Stagnant_Progress_Li.Add(obj_tbl_Stagnant_Progress);
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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Fin_Progress_Summery.rpt"));
                crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Li);
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
        if (ViewState["StagnantFinancial"] != null)
        {
            ds = (DataSet)ViewState["StagnantFinancial"];
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "StagnantFinancial_Detail.pdf";

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
                    obj_tbl_Stagnant_Progress_Detail.Header_Text = "Stagnant Financial Progress for more than " + txtPendencyDays.Text + " Days - : " + ddlSearchScheme.SelectedItem.Text;
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
                    obj_tbl_Stagnant_Progress_Detail.Last_Invoice_Date = ds.Tables[0].Rows[i]["Last_Invoice_Date"].ToString();
                    try
                    {
                        obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = Convert.ToInt32(ds.Tables[0].Rows[i]["Days_Diff"].ToString());
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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Fin_Progress_Detail.rpt"));
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[5].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[6].Text = Session["Default_Division"].ToString();
        }
    }
}
