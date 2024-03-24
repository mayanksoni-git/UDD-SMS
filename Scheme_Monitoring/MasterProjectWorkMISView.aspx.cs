using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMISView : System.Web.UI.Page
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
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                btnCreateNew.Visible = true;
                btnDownload.Visible = true;
                divCreateNew.Visible = true;
                chkShowIssue.Checked = false;
                chkShowIssue.Visible = true;
            }
            else if (Client == "ULB")
            {
                btnCreateNew.Visible = true;
                btnDownload.Visible = false;
                divCreateNew.Visible = true;
                chkShowIssue.Checked = true;
                chkShowIssue.Visible = true;
            }
            else if (Client == "SAR")
            {
                btnCreateNew.Visible = true;
                btnDownload.Visible = true;
                divCreateNew.Visible = true;
                chkShowIssue.Visible = true;
            }
            else
            {
                btnCreateNew.Visible = false;
                btnDownload.Visible = false;
                divCreateNew.Visible = false;
                chkShowIssue.Checked = true;
                chkShowIssue.Visible = true;
            }
            if (Request.QueryString.Count > 0)
            {
                string FromDate = "";
                string TillDate = "";
                string Scheme_Id = "";
                int District_Id = 0;
                int ULB_Id = 0;
                int Zone_Id = 0;
                int Circle_Id = 0;
                int Division_Id = 0;
                int Updation = -1;
                int Month = 0;
                int Year = 0;
                int LD = -1;
                string PhysicalProgressType = "";
                int GO = 0;
                int DocType = 0;
                string ProgressType = "";
                string ProjectStatus = "";
                int Component_Id = 0;
                string NodalDept_Id = "";
                string NodalDepartmentScheme_Id = "";
                int Jurisdiction_In = -1;
                int _Scheme_Id = 0;
                try
                {
                    _Scheme_Id = Convert.ToInt32(Request.QueryString["_Scheme_Id"].ToString());
                }
                catch
                {
                    _Scheme_Id = 0;
                }
                try
                {
                    Jurisdiction_In = Convert.ToInt32(Request.QueryString["Jurisdiction_In"].ToString());
                }
                catch
                {
                    Jurisdiction_In = -1;
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
                    NodalDept_Id = Request.QueryString["NodalDept_Id"].ToString().Trim();
                }
                catch
                {
                    NodalDept_Id = "";
                }
                try
                {
                    ProjectStatus = Request.QueryString["ProjectStatus"].ToString();
                }
                catch
                {
                    ProjectStatus = "";
                }
                try
                {
                    ProgressType = Request.QueryString["ProgressType"].ToString();
                }
                catch
                {
                    ProgressType = "";
                }
                try
                {
                    Component_Id = Convert.ToInt32(Request.QueryString["Component_Id"].ToString());
                }
                catch
                {
                    Component_Id = 0;
                }
                try
                {
                    DocType = Convert.ToInt32(Request.QueryString["DocType"].ToString());
                }
                catch
                {
                    DocType = 0;
                }
                try
                {
                    LD = Convert.ToInt32(Request.QueryString["LD"].ToString());
                }
                catch
                {
                    LD = -1;
                }
                try
                {
                    GO = Convert.ToInt32(Request.QueryString["GO"].ToString());
                }
                catch
                {
                    GO = 0;
                }
                try
                {
                    PhysicalProgressType = Request.QueryString["Type"].ToString();
                }
                catch
                {
                    PhysicalProgressType = "";
                }
                try
                {
                    Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                }
                catch
                {
                    Year = 0;
                }
                try
                {
                    Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                }
                catch
                {
                    Month = 0;
                }
                try
                {
                    Updation = Convert.ToInt32(Request.QueryString["Updation"].ToString());
                }
                catch
                {
                    Updation = -1;
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

                int FromRange = -1;
                int TillRange = -1;
                try
                {
                    FromRange = Convert.ToInt32(txtFromRange.Text.ToString());
                }
                catch
                {
                    FromRange = -1;
                }
                try
                {
                    TillRange = Convert.ToInt32(txtTillRange.Text.ToString());
                }
                catch
                {
                    TillRange = -1;
                }

                int Issue_Id = 0;
                int Dependency_Id = 0;

                try
                {
                    Issue_Id = Convert.ToInt32(Request.QueryString["Issue_Id"].ToString());
                }
                catch
                {
                    Issue_Id = 0;
                }
                try
                {
                    Dependency_Id = Convert.ToInt32(Request.QueryString["Dep_Id"].ToString());
                }
                catch
                {
                    Dependency_Id = 0;
                }
                try
                {
                    NodalDepartmentScheme_Id = Request.QueryString["NodalDepartmentScheme_Id"].ToString();
                }
                catch
                {
                    NodalDepartmentScheme_Id = "";
                }


                if (ProgressType == "Physical")
                {
                    rbtFilterBy.SelectedValue = "P";
                    if (Updation == 0)
                    {
                        txtFromRange.Text = "0";
                        txtTillRange.Text = "0";
                    }
                    else if (Updation == 1)
                    {
                        txtFromRange.Text = "0";
                        txtTillRange.Text = "10";
                    }
                    else if (Updation == 2)
                    {
                        txtFromRange.Text = "10";
                        txtTillRange.Text = "20";
                    }
                    else if (Updation == 3)
                    {
                        txtFromRange.Text = "20";
                        txtTillRange.Text = "30";
                    }
                    else if (Updation == 4)
                    {
                        txtFromRange.Text = "30";
                        txtTillRange.Text = "40";
                    }
                    else if (Updation == 5)
                    {
                        txtFromRange.Text = "40";
                        txtTillRange.Text = "50";
                    }
                    else if (Updation == 6)
                    {
                        txtFromRange.Text = "50";
                        txtTillRange.Text = "60";
                    }
                    else if (Updation == 7)
                    {
                        txtFromRange.Text = "60";
                        txtTillRange.Text = "70";
                    }
                    else if (Updation == 8)
                    {
                        txtFromRange.Text = "70";
                        txtTillRange.Text = "80";
                    }
                    else if (Updation == 9)
                    {
                        txtFromRange.Text = "80";
                        txtTillRange.Text = "90";
                    }
                    else if (Updation == 10)
                    {
                        txtFromRange.Text = "90";
                        txtTillRange.Text = "100";
                    }
                    else if (Updation == 25)
                    {
                        txtFromRange.Text = "0";
                        txtTillRange.Text = "25";
                    }
                    else if (Updation == 50)
                    {
                        txtFromRange.Text = "26";
                        txtTillRange.Text = "50";
                    }
                    else if (Updation == 75)
                    {
                        txtFromRange.Text = "51";
                        txtTillRange.Text = "75";
                    }
                    else if (Updation == 99)
                    {
                        txtFromRange.Text = "76";
                        txtTillRange.Text = "100";
                    }
                    else if (Updation == 11)
                    {
                        txtFromRange.Text = "100";
                        txtTillRange.Text = "100";
                    }
                    else
                    {
                        txtFromRange.Text = "";
                        txtTillRange.Text = "";
                    }
                }
                else if (ProgressType == "Financial")
                {
                    rbtFilterBy.SelectedValue = "F";
                    if (Updation == 0)
                    {
                        txtFromRange.Text = "0";
                        txtTillRange.Text = "0";
                    }
                    else if (Updation == 1)
                    {
                        txtFromRange.Text = "0";
                        txtTillRange.Text = "10";
                    }
                    else if (Updation == 2)
                    {
                        txtFromRange.Text = "10";
                        txtTillRange.Text = "20";
                    }
                    else if (Updation == 3)
                    {
                        txtFromRange.Text = "20";
                        txtTillRange.Text = "30";
                    }
                    else if (Updation == 4)
                    {
                        txtFromRange.Text = "30";
                        txtTillRange.Text = "40";
                    }
                    else if (Updation == 5)
                    {
                        txtFromRange.Text = "40";
                        txtTillRange.Text = "50";
                    }
                    else if (Updation == 6)
                    {
                        txtFromRange.Text = "50";
                        txtTillRange.Text = "60";
                    }
                    else if (Updation == 7)
                    {
                        txtFromRange.Text = "60";
                        txtTillRange.Text = "70";
                    }
                    else if (Updation == 8)
                    {
                        txtFromRange.Text = "70";
                        txtTillRange.Text = "80";
                    }
                    else if (Updation == 9)
                    {
                        txtFromRange.Text = "80";
                        txtTillRange.Text = "90";
                    }
                    else if (Updation == 10)
                    {
                        txtFromRange.Text = "90";
                        txtTillRange.Text = "100";
                    }
                    else if (Updation == 25)
                    {
                        txtFromRange.Text = "0";
                        txtTillRange.Text = "25";
                    }
                    else if (Updation == 50)
                    {
                        txtFromRange.Text = "26";
                        txtTillRange.Text = "50";
                    }
                    else if (Updation == 75)
                    {
                        txtFromRange.Text = "51";
                        txtTillRange.Text = "75";
                    }
                    else if (Updation == 99)
                    {
                        txtFromRange.Text = "76";
                        txtTillRange.Text = "100";
                    }
                    else if (Updation == 11)
                    {
                        txtFromRange.Text = "100";
                        txtTillRange.Text = "100";
                    }
                    else
                    {
                        txtFromRange.Text = "";
                        txtTillRange.Text = "";
                    }
                }
                else
                {

                }
                try
                {
                    FromDate = Request.QueryString["FromDate"].ToString().Trim();
                }
                catch
                {
                    FromDate = "";
                }
                try
                {
                    TillDate = Request.QueryString["TillDate"].ToString().Trim();
                }
                catch
                {
                    TillDate = "";
                }
                get_tbl_ProjectWork(District_Id, Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, Updation, Month, Year, PhysicalProgressType, GO, DocType, ProjectStatus, ULB_Id, LD, Component_Id, rbtFilterBy.SelectedValue, FromRange, TillRange, ProgressType, NodalDept_Id, Issue_Id, Dependency_Id, NodalDepartmentScheme_Id, FromDate, TillDate, Jurisdiction_In, _Scheme_Id);
            }
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

    protected void get_tbl_ProjectWork(int District_Id, int Zone_Id, int Circle_Id, int Division_Id, string Scheme_Id, int ProjectType_Id, int Updation, int Month, int Year, string PhysicalProgressType, int GO, int DocType, string ProjectStatus, int ULB_Id, int LD, int Component_Id, string FilterBy, int FromRange, int TillRange, string ProgressType, string NodalDept_Id, int Issue_Id, int Dependency_Id, string NodalDepartmentScheme_Id, string FromDate, string TillDate, int Jurisdiction_In, int FundingPattern_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_PMIS_Dashboard(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", ProjectType_Id, "", Updation, Month, Year, PhysicalProgressType, GO, DocType, ProjectStatus, LD, Component_Id, FilterBy, FromRange, TillRange, ProgressType, NodalDept_Id, Issue_Id, Dependency_Id, NodalDepartmentScheme_Id, FromDate, TillDate, Jurisdiction_In, FundingPattern_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (LD == 0 || LD == 2)
            {
                ViewState["TimeOverRun"] = ds;
            }
            else
            {
                ViewState["TimeOverRun"] = null;
            }
            grdPost.Columns[23].Visible = true;
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                if (chkShowIssue.Checked)
                {
                    grdPost.Columns[23].HeaderStyle.CssClass = "";
                    grdPost.Columns[23].ItemStyle.CssClass = "";
                    grdPost.Columns[23].FooterStyle.CssClass = "";
                    grdPost.Columns[24].Visible = false;
                }
                else
                {
                    grdPost.Columns[23].HeaderStyle.CssClass = "displayStyle";
                    grdPost.Columns[23].ItemStyle.CssClass = "displayStyle";
                    grdPost.Columns[23].FooterStyle.CssClass = "displayStyle";
                    grdPost.Columns[24].Visible = true;
                }
            }
            else
            {
                if (chkShowIssue.Checked)
                {
                    grdPost.Columns[23].HeaderStyle.CssClass = "displayStyle";
                    grdPost.Columns[23].ItemStyle.CssClass = "displayStyle";
                    grdPost.Columns[23].FooterStyle.CssClass = "displayStyle";
                }
                else
                {
                    grdPost.Columns[23].HeaderStyle.CssClass = "";
                    grdPost.Columns[23].ItemStyle.CssClass = "";
                    grdPost.Columns[23].FooterStyle.CssClass = "";
                }
                grdPost.Columns[24].Visible = false;
            }
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
            ViewState["TimeOverRun"] = null;
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        GridViewRow gr = lnkUpdate.Parent.Parent as GridViewRow;
        string Client = System.Configuration.ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            Response.Redirect("MasterProjectWork_DataEntry.aspx?ProjectWork_Id=" + gr.Cells[0].Text.Trim() + "&Scheme_Id=" + gr.Cells[1].Text.Trim());
        }
        else if (Client == "ULB")
        {
            Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + gr.Cells[0].Text.Trim() + "&Scheme_Id=" + gr.Cells[1].Text.Trim());
        }
        else if (Client == "SAR")
        {
            Response.Redirect("MasterProjectWork_DataEntrySAR.aspx?ProjectWork_Id=" + gr.Cells[0].Text.Trim() + "&Scheme_Id=" + gr.Cells[1].Text.Trim());
        }
        else
        {
            Response.Redirect("MasterProjectWorkMIS_1.aspx?ProjectWork_Id=" + gr.Cells[0].Text.Trim());
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlGenericControl divMISSteps = e.Row.FindControl("divMISSteps") as HtmlGenericControl;
            HtmlGenericControl divPhoto = e.Row.FindControl("divPhoto") as HtmlGenericControl;
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                divMISSteps.Visible = false;
                divPhoto.Visible = true;
            }
            else if (Client == "SAR")
            {
                divMISSteps.Visible = false;
                divPhoto.Visible = true;
            }
            else if (Client == "ULB")
            {
                divMISSteps.Visible = false;
                divPhoto.Visible = true;
            }
            else
            {
                divMISSteps.Visible = true;
                divPhoto.Visible = false;
            }
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            string Scheme_Id = "";
            int District_Id = 0;
            int ULB_Id = 0;
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            int Updation = -1;
            int Month = 0;
            int Year = 0;
            int LD = -1;
            string PhysicalProgressType = "";
            int GO = 0;
            int DocType = 0;
            string ProgressType = "";
            string ProjectStatus = "";
            int Component_Id = 0;
            string NodalDept_Id = "";
            string NodalDepartmentScheme_Id = "";
            string FromDate = "";
            string TillDate = "";
            int Jurisdiction_In = -1;
            int _Scheme_Id = 0;
            try
            {
                _Scheme_Id = Convert.ToInt32(Request.QueryString["_Scheme_Id"].ToString());
            }
            catch
            {
                _Scheme_Id = 0;
            }
            try
            {
                Jurisdiction_In = Convert.ToInt32(Request.QueryString["Jurisdiction_In"].ToString());
            }
            catch
            {
                Jurisdiction_In = -1;
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
                NodalDepartmentScheme_Id = Request.QueryString["NodalDepartmentScheme_Id"].ToString();
            }
            catch
            {
                NodalDepartmentScheme_Id = "";
            }
            try
            {
                NodalDept_Id = Request.QueryString["NodalDept_Id"].ToString().ToString();
            }
            catch
            {
                NodalDept_Id = "";
            }
            try
            {
                ProjectStatus = Request.QueryString["ProjectStatus"].ToString();
            }
            catch
            {
                ProjectStatus = "";
            }
            try
            {
                ProgressType = Request.QueryString["ProgressType"].ToString();
            }
            catch
            {
                ProgressType = "";
            }
            try
            {
                Component_Id = Convert.ToInt32(Request.QueryString["Component_Id"].ToString());
            }
            catch
            {
                Component_Id = 0;
            }
            try
            {
                DocType = Convert.ToInt32(Request.QueryString["DocType"].ToString());
            }
            catch
            {
                DocType = 0;
            }
            try
            {
                LD = Convert.ToInt32(Request.QueryString["LD"].ToString());
            }
            catch
            {
                LD = -1;
            }
            try
            {
                GO = Convert.ToInt32(Request.QueryString["GO"].ToString());
            }
            catch
            {
                GO = 0;
            }
            try
            {
                PhysicalProgressType = Request.QueryString["Type"].ToString();
            }
            catch
            {
                PhysicalProgressType = "";
            }
            try
            {
                Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
            }
            catch
            {
                Year = 0;
            }
            try
            {
                Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
            }
            catch
            {
                Month = 0;
            }
            try
            {
                Updation = Convert.ToInt32(Request.QueryString["Updation"].ToString());
            }
            catch
            {
                Updation = -1;
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

            int FromRange = -1;
            int TillRange = -1;
            try
            {
                FromRange = Convert.ToInt32(txtFromRange.Text.ToString());
            }
            catch
            {
                FromRange = -1;
            }
            try
            {
                TillRange = Convert.ToInt32(txtTillRange.Text.ToString());
            }
            catch
            {
                TillRange = -1;
            }

            int Issue_Id = 0;
            int Dependency_Id = 0;

            try
            {
                Issue_Id = Convert.ToInt32(Request.QueryString["Issue_Id"].ToString());
            }
            catch
            {
                Issue_Id = 0;
            }
            try
            {
                Dependency_Id = Convert.ToInt32(Request.QueryString["Dep_Id"].ToString());
            }
            catch
            {
                Dependency_Id = 0;
            }
            try
            {
                FromDate = Request.QueryString["FromDate"].ToString().Trim();
            }
            catch
            {
                FromDate = "";
            }
            try
            {
                TillDate = Request.QueryString["TillDate"].ToString().Trim();
            }
            catch
            {
                TillDate = "";
            }
            get_tbl_ProjectWork(District_Id, Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, Updation, Month, Year, PhysicalProgressType, GO, DocType, ProjectStatus, ULB_Id, LD, Component_Id, rbtFilterBy.SelectedValue, FromRange, TillRange, ProgressType, NodalDept_Id, Issue_Id, Dependency_Id, NodalDepartmentScheme_Id, FromDate, TillDate, Jurisdiction_In, _Scheme_Id);
        }
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWork_DataEntry.aspx");
    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        string Scheme_Id = "";
        int District_Id = 0;
        int ULB_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int Updation = -1;
        int Month = 0;
        int Year = 0;
        int LD = -1;
        string PhysicalProgressType = "";
        int GO = 0;
        int DocType = 0;
        string ProgressType = "";
        string ProjectStatus = "";
        int Component_Id = 0;
        string NodalDept_Id = "";
        string NodalDepartmentScheme_Id = "";
        string FromDate = "";
        string TillDate = "";
        int Jurisdiction_In = -1;
        int _Scheme_Id = 0;
        try
        {
            _Scheme_Id = Convert.ToInt32(Request.QueryString["_Scheme_Id"].ToString());
        }
        catch
        {
            _Scheme_Id = 0;
        }
        try
        {
            Jurisdiction_In = Convert.ToInt32(Request.QueryString["Jurisdiction_In"].ToString());
        }
        catch
        {
            Jurisdiction_In = -1;
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
            NodalDepartmentScheme_Id = Request.QueryString["NodalDepartmentScheme_Id"].ToString();
        }
        catch
        {
            NodalDepartmentScheme_Id = "";
        }
        try
        {
            NodalDept_Id = Request.QueryString["NodalDept_Id"].ToString().ToString();
        }
        catch
        {
            NodalDept_Id = "";
        }
        try
        {
            ProjectStatus = Request.QueryString["ProjectStatus"].ToString();
        }
        catch
        {
            ProjectStatus = "";
        }
        try
        {
            ProgressType = Request.QueryString["ProgressType"].ToString();
        }
        catch
        {
            ProgressType = "";
        }
        try
        {
            Component_Id = Convert.ToInt32(Request.QueryString["Component_Id"].ToString());
        }
        catch
        {
            Component_Id = 0;
        }
        try
        {
            DocType = Convert.ToInt32(Request.QueryString["DocType"].ToString());
        }
        catch
        {
            DocType = 0;
        }
        try
        {
            LD = Convert.ToInt32(Request.QueryString["LD"].ToString());
        }
        catch
        {
            LD = -1;
        }
        try
        {
            GO = Convert.ToInt32(Request.QueryString["GO"].ToString());
        }
        catch
        {
            GO = 0;
        }
        try
        {
            PhysicalProgressType = Request.QueryString["Type"].ToString();
        }
        catch
        {
            PhysicalProgressType = "";
        }
        try
        {
            Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
        }
        catch
        {
            Year = 0;
        }
        try
        {
            Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
        }
        catch
        {
            Month = 0;
        }
        try
        {
            Updation = Convert.ToInt32(Request.QueryString["Updation"].ToString());
        }
        catch
        {
            Updation = -1;
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

        int FromRange = -1;
        int TillRange = -1;
        try
        {
            FromRange = Convert.ToInt32(txtFromRange.Text.ToString());
        }
        catch
        {
            FromRange = -1;
        }
        try
        {
            TillRange = Convert.ToInt32(txtTillRange.Text.ToString());
        }
        catch
        {
            TillRange = -1;
        }

        int Issue_Id = 0;
        int Dependency_Id = 0;

        try
        {
            Issue_Id = Convert.ToInt32(Request.QueryString["Issue_Id"].ToString());
        }
        catch
        {
            Issue_Id = 0;
        }
        try
        {
            Dependency_Id = Convert.ToInt32(Request.QueryString["Dep_Id"].ToString());
        }
        catch
        {
            Dependency_Id = 0;
        }
        try
        {
            FromDate = Request.QueryString["FromDate"].ToString().Trim();
        }
        catch
        {
            FromDate = "";
        }
        try
        {
            TillDate = Request.QueryString["TillDate"].ToString().Trim();
        }
        catch
        {
            TillDate = "";
        }

        DataSet ds = new DataSet();
        DateTime dt;
        dt = DateTime.Now;
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS_CNDS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, -1, false, "", "");
        }
        else
        {
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, -1, false, "", "");
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
            string Name = "PMIS_" + dt.Month.ToString().PadLeft(2, '0') + "_" + dt.Day.ToString().PadLeft(2, '0') + "_" + dt.Year.ToString() + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
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

    protected void chkShowIssue_CheckedChanged(object sender, EventArgs e)
    {
        btnFilter_Click(btnFilter, new EventArgs());
    }

    protected void btnIssueList_Click(object sender, EventArgs e)
    {
        LinkButton btnIssueList = sender as LinkButton;
        GridViewRow gr = btnIssueList.Parent.Parent as GridViewRow;
        int ProjectWork_Id = 0;
        try
        {
            ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWork_Id = 0;
        }
        if (ProjectWork_Id > 0)
        {
            get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
        }
    }

    private void get_tbl_ProjectWorkIssueDetails(int ProjectWork_Id)
    {
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        obj_tbl_ProjectWorkIssueDetails_Li = (new DataLayer()).get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
        if (obj_tbl_ProjectWorkIssueDetails_Li != null && obj_tbl_ProjectWorkIssueDetails_Li.Count > 0)
        {
            mpIssue.Show();
            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
        }
        else
        {
            MessageBox.Show("No Records Found");
            grdIssue.DataSource = null;
            grdIssue.DataBind();
        }
    }

    protected void grdIssue_PreRender(object sender, EventArgs e)
    {

    }
}