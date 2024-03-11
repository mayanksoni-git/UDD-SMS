//using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Invoice_Status_Change : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    private void get_tbl_InvoiceStatus()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_InvoiceStatus(0, 0, 0, 0, "Invoice");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlInvoiceStatus, "InvoiceStatus_Name", "InvoiceStatus_Id");
        }
        else
        {
            ddlInvoiceStatus.Items.Clear();
        }
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
            get_tbl_InvoiceStatus();
            get_tbl_Designation();
            get_Branch_Office_Details();
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

            if (Session["UserType"].ToString() == "1")
            {

            }
            else if (Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
                {
                    ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                    ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                }
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
                {
                    ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                    ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                    ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                    ddlOrganization.Enabled = false;
                    ddlDesignation.Enabled = false;
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                    ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                    ddlOrganization.Enabled = false;
                    ddlDesignation.Enabled = false;
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                    ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                    ddlOrganization.Enabled = false;
                    ddlDesignation.Enabled = false;
                }
                else
                {
                    ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                    ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                }
            }
            else
            {
                ddlOrganization.SelectedValue = Session["Person_BranchOffice_Id"].ToString();
                ddlDesignation.SelectedValue = Session["PersonJuridiction_DesignationId"].ToString();
                ddlOrganization.Enabled = false;
                ddlDesignation.Enabled = false;
            }
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
    private void get_tbl_Designation()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "Designation_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    private void get_Branch_Office_Details()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Branch_Office_Details(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlOrganization, "OfficeBranch_Name", "OfficeBranch_Id");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlSearchScheme.Focus();
            return;
        }
        if (ddlInvoiceStatus.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Status");
            ddlInvoiceStatus.Focus();
            return;
        }
        if (ddlOrganization.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Organization");
            ddlOrganization.Focus();
            return;
        }
        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Designation");
            ddlDesignation.Focus();
            return;
        }
        int Project_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int InvoiceStatus_Id = 0;
        int Designation_Id = 0;
        int Organization_Id = 0;

        try
        {
            Designation_Id = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Organization_Id = Convert.ToInt32(ddlOrganization.SelectedValue);
        }
        catch
        {
            Organization_Id = 0;
        }
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
            InvoiceStatus_Id = Convert.ToInt32(ddlInvoiceStatus.SelectedValue);
        }
        catch
        {
            InvoiceStatus_Id = 0;
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
        int Added_By_Person_Id = 0;
        try
        {
            Added_By_Person_Id = Convert.ToInt32(ddlEmployee.SelectedValue);
        }
        catch
        {
            Added_By_Person_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Invoice_Status_History(Zone_Id, Circle_Id, Division_Id, Project_Id, txtFromDate.Text.Trim(), txtTillDate.Text.Trim(), InvoiceStatus_Id, Organization_Id, Designation_Id, chkShowUpToDate.Checked, Added_By_Person_Id, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
            divData.Visible = true;
        }
        else
        {
            divData.Visible = false;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
            MessageBox.Show("No Records Found");
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

    protected void grdEMBHistory_PreRender(object sender, EventArgs e)
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

    protected void grdInvoiceHistory_PreRender(object sender, EventArgs e)
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

    protected void btnOpenInvoice_Click(object sender, ImageClickEventArgs e)
    {
        divHistory.Visible = true;
        int Invoice_Id = 0;
        int Package_Id = 0;
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }

        try
        {
            Package_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Package_Id = 0;
        }
        get_tbl_PackageInvoiceApproval_History(Invoice_Id);
        get_tbl_PackageEMBApproval_History(Invoice_Id, "");
    }

    private void get_tbl_PackageInvoiceApproval_History(int PackageInvoice_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoiceApproval_History(PackageInvoice_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoiceHistory.DataSource = ds.Tables[0];
            grdInvoiceHistory.DataBind();
        }
        else
        {
            grdInvoiceHistory.DataSource = null;
            grdInvoiceHistory.DataBind();
        }
    }

    private void get_tbl_PackageEMBApproval_History(int PackageInvoice_Id, string PackageEMBMaster_Id_In)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageEMBApproval_History(PackageInvoice_Id, PackageEMBMaster_Id_In);

        if (AllClasses.CheckDataSet(ds))
        {
            grdEMBHistory.DataSource = ds.Tables[0];
            grdEMBHistory.DataBind();
        }
        else
        {
            grdEMBHistory.DataSource = null;
            grdEMBHistory.DataBind();
        }
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganization.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Organization");
            ddlOrganization.Focus();
            return;
        }
        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Designation");
            ddlDesignation.Focus();
            return;
        }
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int Designation_Id = 0;
        int Organization_Id = 0;

        try
        {
            Designation_Id = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            Designation_Id = 0;
        }
        try
        {
            Organization_Id = Convert.ToInt32(ddlOrganization.SelectedValue);
        }
        catch
        {
            Organization_Id = 0;
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
        string UserTypeId = "1, 2, 4, 6, 7, 8, 11, 3";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee(UserTypeId, 0, 0, 0, Zone_Id, Circle_Id, Division_Id, 0, ddlDesignation.SelectedValue, "", 0, 0, Convert.ToInt32(ddlSearchScheme.SelectedValue));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlEmployee, "Person_Name", "Person_Id");
        }
        else
        {
            ddlEmployee.Items.Clear();
        }
    }

    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[13].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[14].Text = Session["Default_Division"].ToString();
        }
    }
}
