using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Component_Details : System.Web.UI.Page
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
            txtDateTill.Text = Session["ServerDate"].ToString();
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Project();
            get_tbl_PhysicalProgressComponent(Convert.ToInt32(ddlSearchScheme.SelectedValue));
            get_tbl_Zone();
            get_tbl_ProjectType(Convert.ToInt32(ddlSearchScheme.SelectedValue));
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

            string Scheme_Id = "";
            int District_Id = 0;
            int ULB_Id = 0;
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            int Component_Id = 0;
            int ProjectType_Id = 0;
            if (Request.QueryString.Count > 0)
            {
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
                try
                {
                    Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
                    ddlSearchScheme.SelectedValue = Scheme_Id.ToString();
                    ddlSearchScheme_SelectedIndexChanged(ddlSearchScheme, e);
                }
                catch
                {
                    Scheme_Id = "";
                }
                try
                {
                    Component_Id = Convert.ToInt32(Request.QueryString["Component_Id"].ToString());
                    ddlComponent.SelectedValue = Component_Id.ToString();
                }
                catch
                {
                    Component_Id = 0;
                }
                try
                {
                    ProjectType_Id = Convert.ToInt32(Request.QueryString["ProjectType_Id"].ToString());
                    ddlProjectType.SelectedValue = ProjectType_Id.ToString();
                }
                catch
                {
                    ProjectType_Id = 0;
                }
            }
            get_Component_Data();
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
    private void get_tbl_PhysicalProgressComponent(int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PhysicalProgressComponent(Scheme_Id, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlComponent, "Component_With_Unit", "PhysicalProgressComponent_Id");
        }
        else
        {
            ddlComponent.Items.Clear();
        }
    }
    private void get_Component_Data()
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
        int Component_Id = 0;
        try
        {
            Component_Id = Convert.ToInt32(ddlComponent.SelectedValue);
        }
        catch
        {
            Component_Id = 0;
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
        ds = (new DataLayer()).get_Component_Data(Project_Id, Zone_Id, Circle_Id, Division_Id, Component_Id, ProjectType_Id, txtDateTill.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            tbl_ProgramAnalysisReport_Property1 ProgramAnalysisReport_Property = new tbl_ProgramAnalysisReport_Property1();

            try
            {
                ProgramAnalysisReport_Property.Project_Id = ds.Tables[0].Rows[0]["Component_Id"].ToString();

            }
            catch
            {
                ProgramAnalysisReport_Property.Project_Id = "";
            }
            try
            {
                ProgramAnalysisReport_Property.Project_Name = ds.Tables[0].Rows[0]["Component"].ToString();
            }
            catch
            {
                ProgramAnalysisReport_Property.Project_Id = "";
            }
            try
            {
                ProgramAnalysisReport_Property.AGR = Convert.ToDecimal(ds.Tables[0].Compute("sum(Proposed)", "").ToString());
            }
            catch
            {
                ProgramAnalysisReport_Property.AGR = 0;
            }
            try
            {
                ProgramAnalysisReport_Property.ALD = Convert.ToDecimal(ds.Tables[0].Compute("sum(PhysicalProgress)", "").ToString()); 
            }
            catch(Exception ee)
            {
                ProgramAnalysisReport_Property.ALD = 0;
            }
            
            if (ProgramAnalysisReport_Property != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                hf_PhysicalReport_ZoneWise.Value = jss.Serialize(ProgramAnalysisReport_Property);
            }
            else
            {
                hf_PhysicalReport_ZoneWise.Value = "";
            }
            grdPost.DataSource = ds;
            grdPost.DataBind();

        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
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
        get_Component_Data();
    }

    protected void ddlSearchScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_tbl_PhysicalProgressComponent(Convert.ToInt32(ddlSearchScheme.SelectedValue));
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
}