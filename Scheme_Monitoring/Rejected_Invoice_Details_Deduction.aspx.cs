using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rejected_Invoice_Details_Deduction : System.Web.UI.Page
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
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            if (Session["PersonJuridiction_DesignationId"].ToString() == "33")
            {
                btnRollback.Visible = true;
            }
            else
            {
                btnRollback.Visible = false;
            }
            get_tbl_Designation_Level_Wise();
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
    private void get_tbl_Designation_Level_Wise()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation_Level_Wise(Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "ProcessConfigMaster_Designation_Id");
        }
        else
        {
            ddlDesignation.Items.Clear();
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

    private void get_tbl_DeductionRelease_Documents_Details(int Scheme_Id, int Package_DeductionRelease_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DeductionRelease_Documents_Details(Scheme_Id, Package_DeductionRelease_Id, true);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDocumentMaster.DataSource = ds.Tables[0];
            grdDocumentMaster.DataBind();
        }
        else
        {
            grdDocumentMaster.DataSource = null;
            grdDocumentMaster.DataBind();
        }
    }

    private void get_tbl_Package_DeductionRelease()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        Scheme_Id = ddlSearchScheme.SelectedValue;
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
        if (rbtInvoiceTypeToDisplay.SelectedValue == "1")
        {
            isDefered = true;
        }
        else
        {
            isDefered = false;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, 0, "", "", -2, -2, false, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
        }
        else
        {
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
    }

    private DataSet filter_Data(DataSet ds)
    {
        DataSet dsF = new DataSet();
        DataView dv = new DataView(ds.Tables[0]);
        dv.RowFilter = "Package_DeductionReleaseApproval_Status_Id = 2";
        dsF.Tables.Add(dv.ToTable("FilteredTable"));
        return dsF;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_tbl_Package_DeductionRelease();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hf_Invoice_Id.Value == "" || hf_Invoice_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Designation");
            return;
        }
        if (txtComments.Text.Trim() == "")
        {
            MessageBox.Show("Please Input Comments");
            return;
        }
        string[] _data = ddlDesignation.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        List<tbl_Package_DeductionReleaseApproval> obj_tbl_Package_DeductionReleaseApproval_Li = new List<tbl_Package_DeductionReleaseApproval>();
        tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = txtComments.Text.Trim();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 2;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = Convert.ToInt32(_data[0]);
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = Convert.ToInt32(_data[1]);
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Additional_Status_Id = 0;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
        obj_tbl_Package_DeductionReleaseApproval_Li.Add(obj_tbl_Package_DeductionReleaseApproval);

        if (new DataLayer().Update_tbl_PackageDeductionRelease_Rejected(obj_tbl_Package_DeductionReleaseApproval_Li))
        {
            MessageBox.Show("Updated Successfully.");
            hf_Invoice_Id.Value = "0";
            txtComments.Text = "";
            divUpdate.Visible = false;
            get_tbl_Package_DeductionRelease();
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation.");
            return;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (hf_Invoice_Id.Value == "" || hf_Invoice_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        List<tbl_Package_DeductionRelease_Docs> obj_tbl_Package_DeductionRelease_Docs_Li = new List<tbl_Package_DeductionRelease_Docs>();
        for (int i = 0; i < grdDocumentMaster.Rows.Count; i++)
        {
            FileUpload flUpload = grdDocumentMaster.Rows[i].FindControl("flUpload") as FileUpload;
            TextBox txtDocumentOrderNo = grdDocumentMaster.Rows[i].FindControl("txtDocumentOrderNo") as TextBox;
            TextBox txtDocumentComments = grdDocumentMaster.Rows[i].FindControl("txtDocumentComments") as TextBox;
            if (flUpload.HasFile)
            {
                tbl_Package_DeductionRelease_Docs obj_tbl_Package_DeductionRelease_Docs = new tbl_Package_DeductionRelease_Docs();
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_AddedBy= Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_FileBytes = flUpload.FileBytes;
                string[] _fname = flUpload.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_FileName = _fname[_fname.Length - 1];
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_Package_DeductionRelease_Id = Convert.ToInt32(hf_Invoice_Id.Value);
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_Status = 1;
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_OrderNo = txtDocumentOrderNo.Text.Trim();
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_Comments = txtDocumentComments.Text.Trim();
                obj_tbl_Package_DeductionRelease_Docs.Package_DeductionRelease_Docs_Type = Convert.ToInt32(grdDocumentMaster.Rows[i].Cells[0].Text.Trim());
                obj_tbl_Package_DeductionRelease_Docs_Li.Add(obj_tbl_Package_DeductionRelease_Docs);
            }
        }
        tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = txtComments.Text.Trim();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 1;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = 35;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = 1;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 5;

        if (obj_tbl_Package_DeductionRelease_Docs_Li.Count > 0)
        {
            if (new DataLayer().Update_tbl_PackageDeductionRelease_Rejected(obj_tbl_Package_DeductionRelease_Docs_Li, obj_tbl_Package_DeductionReleaseApproval))
            {
                MessageBox.Show("Document Updated Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error!!");
                return;
            }
        }
        else
        {
            MessageBox.Show("Please Attache Atleast One Document To Update");
            return;
        }
    }

    protected void grdDocumentMaster_PreRender(object sender, EventArgs e)
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

    protected void grdDocumentMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //FileUpload flUpload = e.Row.FindControl("flUpload") as FileUpload;
            //PostBackTrigger trigger = new PostBackTrigger();
            //trigger.ControlID = flUpload.ID;
            //up.Triggers.Add(trigger);
        }
    }

    protected void btnRollback_Click(object sender, EventArgs e)
    {
        if (hf_Invoice_Id.Value == "" || hf_Invoice_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        if (txtComments.Text.Trim() == "")
        {
            MessageBox.Show("Please Input Comments");
            return;
        }
        List<tbl_Package_DeductionReleaseApproval> obj_tbl_Package_DeductionReleaseApproval_Li = new List<tbl_Package_DeductionReleaseApproval>();
        tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = txtComments.Text.Trim();
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 2;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString());
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 8;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Additional_Status_Id = 0;
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
        obj_tbl_Package_DeductionReleaseApproval_Li.Add(obj_tbl_Package_DeductionReleaseApproval);

        if (new DataLayer().Update_tbl_PackageDeductionRelease_Rejected(obj_tbl_Package_DeductionReleaseApproval_Li))
        {
            MessageBox.Show("Rollback Successfully.");
            hf_Invoice_Id.Value = "0";
            txtComments.Text = "";
            divUpdate.Visible = false;
            get_tbl_Package_DeductionRelease();
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation.");
            return;
        }
    }

    protected void btnEditDR_Click(object sender, ImageClickEventArgs e)
    {
        int Package_DeductionRelease_Id = 0;
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
        {
            grdDeductionRelease.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        try
        {
            Package_DeductionRelease_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Package_DeductionRelease_Id = 0;
        }
        hf_Invoice_Id.Value = Package_DeductionRelease_Id.ToString();
        get_tbl_DeductionRelease_Documents_Details(Convert.ToInt32(ddlSearchScheme.SelectedValue), Package_DeductionRelease_Id);
        divUpdate.Visible = true;
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

    protected void lnkExport_Click(object sender, ImageClickEventArgs e)
    {
        GridViewExportUtil.Export("Rejected_Invoices.xls", grdDeductionRelease);
    }

    protected void grdDeductionRelease_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }
}
