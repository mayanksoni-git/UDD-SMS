using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Project_Financial_Progress : System.Web.UI.Page
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
            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
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
            btnSearch_Click(btnSearch, e);
        }
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
        string FromDate = "";
        string TillDate = "";
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }   
        else
        {
            FromDate = txtDateFrom.Text;
            TillDate = txtDateTill.Text;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Work_Financial_Progress_Detailed(Project_Id, Zone_Id, Circle_Id, Division_Id, FromDate, TillDate);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(MA_Amount)", "").ToString();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(PrevInvoice_Amount)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(PrevADP_Amount)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(EMB_Amount)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Invoice_Amount)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(ADP_Amount)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Total_Achivment)", "").ToString();

            divData.Visible = true;
        }
        else
        {
            divData.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Work_Financial_Progress_Monthly(Project_Id, Zone_Id, Circle_Id, Division_Id, txtDateFrom.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            string filePath = "\\Downloads\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "Financial_Progress_Monthly_" + txtDateFrom.Text.Replace("/", "_") + ".pdf";

            List<tbl_Financial_Progress_Monthly> obj_tbl_Financial_Progress_Monthly_Li = new List<tbl_Financial_Progress_Monthly>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_Financial_Progress_Monthly obj_tbl_Financial_Progress_Monthly = new tbl_Financial_Progress_Monthly();
                obj_tbl_Financial_Progress_Monthly.Achivment_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Achivment_In_Current_Month"].ToString());
                obj_tbl_Financial_Progress_Monthly.ADP_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Other_Dept_Current_Month"].ToString());
                obj_tbl_Financial_Progress_Monthly.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Financial_Progress_Monthly.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Financial_Progress_Monthly.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Financial_Progress_Monthly.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Financial_Progress_Monthly.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_Financial_Progress_Monthly.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_Financial_Progress_Monthly.Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " : " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Financial_Progress_Monthly.Deduction_Release_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Deduction_Release_Current_Month"].ToString());
                obj_tbl_Financial_Progress_Monthly.EMB_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["EMB_Current_Month"].ToString());
                obj_tbl_Financial_Progress_Monthly.Invoice_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Invoice_Value_Current_Month"].ToString());
                obj_tbl_Financial_Progress_Monthly.MA_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Moblization_Adv_Current_Month"].ToString());
                obj_tbl_Financial_Progress_Monthly.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_Financial_Progress_Monthly.Header_Text = "Project Wise Financial Progress (" + txtDateFrom.Text + "): " + ddlSearchScheme.SelectedItem.Text;
                obj_tbl_Financial_Progress_Monthly_Li.Add(obj_tbl_Financial_Progress_Monthly);
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
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Financial_Progress_Monthly.rpt"));
            crystalReport.SetDataSource(obj_tbl_Financial_Progress_Monthly_Li);
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
                MessageBox.Show("Unable To Generate Progress Report.");
                return;
            }
        }
        else
        {
            MessageBox.Show("Unable To Generate Progress Report.");
            return;
        }
    }
}
