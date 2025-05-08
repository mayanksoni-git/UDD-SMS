using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkDataEntry : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
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
            get_tbl_Project();
            get_tbl_Zone();
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            string Mode = "";
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Mode = Request.QueryString[0].ToString();
                }
                catch
                {
                    Mode = "";
                }
                if (Mode == "GO")
                {
                    btnCreateNew.Visible = false;
                }
                else if (Mode == "UC")
                {
                    btnCreateNew.Visible = false;
                }
                else if (Mode == "Issue")
                {
                    btnCreateNew.Visible = false;
                }
                else if (Mode == "Comp")
                {
                    btnCreateNew.Visible = false;
                }
                else if (Mode == "SO")
                {
                    btnCreateNew.Visible = true;
                }
                else if (Mode == "PMU")
                {
                    btnCreateNew.Visible = true;
                }
                else if (Mode == "G")
                {
                    btnCreateNew.Visible = false;
                }
                else
                {
                    btnCreateNew.Visible = false;
                }
            }
            else
            {
                btnCreateNew.Visible = true;
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



            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                try
                {
                    ddlSearchScheme.SelectedValue = obj_SearchStorage.Scheme_Id;
                }
                catch
                {
                    ddlSearchScheme.SelectedValue = "0";
                }
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
                //if (obj_SearchStorage.Zone_Id + obj_SearchStorage.Circle_Id + obj_SearchStorage.Division_Id > 0)
                //{
                //    btnSearch_Click(btnSearch, e);
                //}
            }
        }
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
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

    protected void grdPost_PreRender(object sender, EventArgs e)
    {
        grdPost.Columns[0].Visible = false;
        grdPost.Columns[1].Visible = false;
        grdPost.Columns[2].Visible = false;
        grdPost.Columns[3].Visible = false;
        grdPost.Columns[4].Visible = false;
       
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A " + Session["Default_Zone"].ToString() + "");
            return;
        }
        string Project_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Project_Id = ddlSearchScheme.SelectedValue;
        }
        catch
        {
            Project_Id = "";
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
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.District_Id = 0;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Project_Id;
        obj_SearchStorage.Search_By = "2";
        obj_SearchStorage.TillDate = "";
        obj_SearchStorage.ULB_Id = 0;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        DataSet ds = new DataSet();
        ds = (new DataLayer().get_Custom_Dashboard_View(Zone_Id, Circle_Id, Division_Id, Project_Id, 0, 0, txtProjectCode.Text.Trim()));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTotal.Text = ds.Tables[0].Rows[0]["Total_Projects"].ToString();
            lnkPhyCompleted.Text = ds.Tables[0].Rows[0]["Projects_Physical_Completed"].ToString();
            lnkPhyNotCompleted.Text = ds.Tables[0].Rows[0]["Projects_Physical_Not_Completed"].ToString();
            lnkFinCompleted.Text = ds.Tables[0].Rows[0]["Projects_Financial_Completed"].ToString();
            lnkFinNotCompleted.Text = ds.Tables[0].Rows[0]["Projects_Financial_Not_Completed"].ToString();
            lnkGalleryUpdated.Text = ds.Tables[0].Rows[0]["Gallery_Available"].ToString();
            lnkGalleryNotUpdated.Text = ds.Tables[0].Rows[0]["Gallery_Not_Available"].ToString();
            lnkInspectionUpdated.Text = ds.Tables[0].Rows[0]["Inspection_Available"].ToString();
            lnkInspectionNotUpdated.Text = ds.Tables[0].Rows[0]["Inspection_Not_Available"].ToString();
            lnkAgreementUpdated.Text = ds.Tables[0].Rows[0]["Agreement_Available"].ToString();
            lnkAgreementNotUpdated.Text = ds.Tables[0].Rows[0]["Agreement_Not_Available"].ToString();
            lnkUCUpload.Text = ds.Tables[0].Rows[0]["UC_Available"].ToString();
            lnkUCNotUpload.Text = ds.Tables[0].Rows[0]["UC_Not_Available"].ToString();
            lnkUCApproved.Text = ds.Tables[0].Rows[0]["UC_Approved"].ToString();
            lnkUCNotApproved.Text = ds.Tables[0].Rows[0]["UC_Not_Approved"].ToString();
        }
        else
        {
            lnkTotal.Text = "0";
            lnkPhyCompleted.Text = "0";
            lnkPhyNotCompleted.Text = "0";
            lnkFinCompleted.Text = "0";
            lnkFinNotCompleted.Text = "0";
            lnkGalleryUpdated.Text = "0";
            lnkGalleryNotUpdated.Text = "0";
            lnkInspectionUpdated.Text = "0";
            lnkInspectionNotUpdated.Text = "0";
            lnkAgreementUpdated.Text = "0";
            lnkAgreementNotUpdated.Text = "0";
            lnkUCUpload.Text = "0";
            lnkUCNotUpload.Text = "0";
            lnkUCApproved.Text = "0";
            lnkUCNotApproved.Text = "0";
        }
        ds = (new DataLayer()).get_tbl_ProjectWork(Project_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", 0, txtProjectCode.Text.Trim(), 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Project_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        int District_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            Response.Redirect("MasterProjectWork_DataEntry.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
        }
        else
        {
            string Mode = "";
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Mode = Request.QueryString[0].ToString();
                }
                catch
                {
                    Mode = "";
                }
                if (Mode == "GO")
                {
                    Response.Redirect("MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&District_Id=" + District_Id + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "UC")
                {
                    Response.Redirect("MasterProjectWorkMIS_6.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "Issue")
                {
                    Response.Redirect("MasterProjectWorkMIS_8.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "Comp")
                {
                    Response.Redirect("MasterProjectWorkMIS_4.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "SO")
                {
                    Response.Redirect("MasterProjectWork_DataEntrySection.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
                }
                else if (Mode == "PMU")
                {
                    Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
                }
                else if (Mode == "G")
                {
                    Response.Redirect("ProjectWorkGalleryView.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Mode=P&App=false");
                }
                else
                {
                    Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
                }
            }
            else
            {
                Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
            }

        }
    }


    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        string Mode = "";
        if (Request.QueryString.Count > 0)
        {
            try
            {
                Mode = Request.QueryString[0].ToString();
            }
            catch
            {
                Mode = "";
            }
        }
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            Response.Redirect("MasterProjectWork_DataEntry.aspx");
        }
        else
        {
            if (Mode == "SO")
            {
                Response.Redirect("MasterProjectWork_DataEntrySO.aspx");
            }
            else
            {
                Response.Redirect("MasterProjectWork_DataEntry2.aspx");
            }
        }
    }

    protected void lnkTotal_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPhyCompleted_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPhyNotCompleted_Click(object sender, EventArgs e)
    {

    }

    protected void lnkFinCompleted_Click(object sender, EventArgs e)
    {

    }

    protected void lnkFinNotCompleted_Click(object sender, EventArgs e)
    {

    }



    protected void lnkGalleryUpdated_Click(object sender, EventArgs e)
    {

    }

    protected void lnkGalleryNotUpdated_Click(object sender, EventArgs e)
    {

    }

    protected void lnkAgreementUpdated_Click(object sender, EventArgs e)
    {

    }

    protected void lnkAgreementNotUpdated_Click(object sender, EventArgs e)
    {

    }

    protected void lnkUCNotUpload_Click(object sender, EventArgs e)
    {

    }

    protected void lnkUCUpload_Click(object sender, EventArgs e)
    {

    }

    protected void lnkUCApproved_Click(object sender, EventArgs e)
    {

    }

    protected void lnkUCNotApproved_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Person_Id"].ToString() != "2288")
        {
            MessageBox.Show("You are not authorized to perform this action.");
            return;
        }
        try
        {
            GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
            int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());

            // Update the record status to 0 (soft delete)
            bool result = (new DataLayer()).Update_ProjectWork_Status(ProjectWork_Id, 0);

            if (result)
            {
                MessageBox.Show("Project deleted successfully.");
                // Rebind the grid to reflect changes
                btnSearch_Click(btnSearch, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Error deleting project.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }

    //protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        // Find the delete button in the row
    //        ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");

    //        if (btnDelete != null)
    //        {
    //            // Show delete button only for Person_Id 2288
    //            btnDelete.Visible = (Session["Person_Id"] != null && Session["Person_Id"].ToString() == "2288");
    //        }
    //    }
    //}
}