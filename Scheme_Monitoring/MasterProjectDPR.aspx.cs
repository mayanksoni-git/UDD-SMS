using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectDPR : System.Web.UI.Page
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

            get_tbl_Zone();
            get_tbl_ProjectType(0);
            get_tbl_Project();
            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "CNDS")
            {
                chkSkip.Visible = true;
                chkSkip.Checked = true;
            }
            else
            {
                chkSkip.Visible = false;
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

            if (ddlProjectMaster.SelectedValue != "0")
            {
                get_tbl_ProjectDPR(Convert.ToInt32(ddlProjectMaster.SelectedValue));
            }
        }
        //if (Session["UserType"].ToString() == "1")
        //{
        //    divCreate.Visible = true;
        //}
        //else if(Session["UserType"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "4")
        //{
        //    divCreate.Visible = true;
        //}
        //else
        //{
        //    divCreate.Visible = false;
        //}
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
    private void get_tbl_ProjectType(int ProjectId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectType(ProjectId, 0);
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

    protected void get_tbl_ProjectDPR(int Project_Id)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        int NodalDepartment_Id = 0;
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
        int Tranche_Id = 0;
       
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkDPR(Project_Id.ToString(), 0, Zone_Id, Circle_Id, Division_Id, 0, 0, 0, "0", 0, Tranche_Id, NodalDepartment_Id);
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
            AllClasses.FillDropDown(ds.Tables[0], ddlProjectMaster, "Project_Name", "Project_Id");
            try
            {
                ddlProjectMaster.SelectedValue = Session["Default_Scheme"].ToString();
                ddlProjectMaster_SelectedIndexChanged(ddlProjectMaster, new EventArgs());
            }
            catch
            {
                
            }
        }
        else
        {
            ddlProjectMaster.Items.Clear();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlProjectMaster.Focus();
            return;
        }
        if (txtProjectWorkName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Name");
            txtProjectWorkName.Focus();
            return;
        }
        if (txtTentitiveDate.Text== "")
        {
            MessageBox.Show("Please Select DPR Preparation Tentitive Date");
            txtTentitiveDate.Focus();
            return;
        }
        tbl_ProjectDPR obj_tbl_ProjectDPR = new tbl_ProjectDPR();
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_Id = 0;
        }
        obj_tbl_ProjectDPR.ProjectDPR_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_CapexCost = Convert.ToDecimal(txtCapexCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_CapexCost = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_OandM_Cost = Convert.ToDecimal(txtOMCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_OandM_Cost = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_Project_Cost = Convert.ToDecimal(txtProjectCost.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_Project_Cost = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_ProjectTypeId = Convert.ToInt32(ddlProjectType.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_ProjectTypeId = 0;
        }
        try
        {
            obj_tbl_ProjectDPR.ProjectDPR_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectDPR.ProjectDPR_Project_Id = 0;
        }
        obj_tbl_ProjectDPR.ProjectDPR_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectDPR.ProjectDPR_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        obj_tbl_ProjectDPR.ProjectDPR_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectDPR.ProjectDPR_TentitiveDate = txtTentitiveDate.Text.Trim();
        if (chkLandStatus.Items[0].Selected)
        {
            obj_tbl_ProjectDPR.ProjectDPR_LandIdentified = 1;
        }
        else if (chkLandStatus.Items[0].Selected)
        {
            obj_tbl_ProjectDPR.ProjectDPR_LandTransfered = 1;
        }
        else
        {
            obj_tbl_ProjectDPR.ProjectDPR_LandIdentified = 0;
            obj_tbl_ProjectDPR.ProjectDPR_LandTransfered = 0;
        }
        obj_tbl_ProjectDPR.ProjectDPR_Status = 1;

        tbl_ProjectDPRApproval obj_tbl_ProjectDPRApproval = new tbl_ProjectDPRApproval();
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Status = 1;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Comments = "";
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Date = DateTime.Now.ToString("dd/MM/yyyy");
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Status_Id = 1;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Loop = 1;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_SchemeId = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_ProjectDPR_Id = Convert.ToInt32(hf_ProjectDPR_Id.Value);

        if ((new DataLayer()).Insert_tbl_ProjectDPR(obj_tbl_ProjectDPR, null, null, null, null, null, chkSkip.Checked, obj_tbl_ProjectDPRApproval, Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
        {
            MessageBox.Show("Project Created Successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Project!");
            return;
        }
    }

    private void reset()
    {
        hf_ProjectDPR_Id.Value = "0";
        txtProjectWorkName.Text = "";
        txtCapexCost.Text = "";
        txtProjectWorkName.Text = "";
    }
        
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        GridViewRow gr = lnkUpdate.Parent.Parent as GridViewRow;
        hf_ProjectDPR_Id.Value = gr.Cells[0].Text.Trim();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkDPR("", 0, 0, 0, 0, 0, 0, Convert.ToInt32(hf_ProjectDPR_Id.Value), "0", 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlProjectMaster.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_Project_Id"].ToString();
                ddlProjectMaster_SelectedIndexChanged(ddlProjectMaster, e);
            }
            catch
            {
                ddlProjectMaster.SelectedValue = "0";
            }
            try
            {
                ddlProjectType.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_ProjectTypeId"].ToString();
            }
            catch
            {
                ddlProjectType.SelectedValue = "0";
            }
            try
            {
                ddlZone.SelectedValue = ds.Tables[0].Rows[0]["Zone_Id"].ToString();
                ddlZone_SelectedIndexChanged(ddlZone, e);
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }
            try
            {
                ddlCircle.SelectedValue = ds.Tables[0].Rows[0]["Circle_Id"].ToString();
                ddlCircle_SelectedIndexChanged(ddlCircle, e);
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            try
            {
                ddlDivision.SelectedValue = ds.Tables[0].Rows[0]["ProjectDPR_DivisionId"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            int ProjectDPR_LandIdentified = 0;
            int ProjectDPR_LandTransfered = 0;
            try
            {
                ProjectDPR_LandIdentified = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectDPR_LandIdentified"].ToString());
            }
            catch
            {
                ProjectDPR_LandIdentified = 0;
            }
            try
            {
                ProjectDPR_LandTransfered = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectDPR_LandTransfered"].ToString());
            }
            catch
            {
                ProjectDPR_LandTransfered = 0;
            }
            if (ProjectDPR_LandIdentified == 1)
            {
                chkLandStatus.Items[0].Selected = true;
            }
            else
            {
                chkLandStatus.Items[0].Selected = false;
            }
            if (ProjectDPR_LandTransfered == 1)
            {
                chkLandStatus.Items[1].Selected = true;
            }
            else
            {
                chkLandStatus.Items[1].Selected = false;
            }
            txtTentitiveDate.Text = ds.Tables[0].Rows[0]["ProjectDPR_TentitiveDate"].ToString();
            txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectDPR_Name"].ToString();
            txtCapexCost.Text = ds.Tables[0].Rows[0]["ProjectDPR_CapexCost"].ToString();
            txtOMCost.Text = ds.Tables[0].Rows[0]["ProjectDPR_OandM_Cost"].ToString();
            txtProjectCost.Text = ds.Tables[0].Rows[0]["ProjectDPR_Project_Cost"].ToString();
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Person_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_ProjectWorkDPR(Person_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            int ProjectId = 0;
            try
            {
                ProjectId = Convert.ToInt32(ddlProjectMaster.SelectedValue);
            }
            catch
            {
                ProjectId = 0;
            }
            get_tbl_ProjectDPR(ProjectId);
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
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
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
            if (Session["UserType"].ToString() == "1")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void ddlProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            ddlProjectType.Items.Clear();
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
        else
        {
            int ProjectId = 0;
            try
            {
                ProjectId = Convert.ToInt32(ddlProjectMaster.SelectedValue);
            }
            catch
            {
                ProjectId = 0;
            }
            get_tbl_ProjectType(ProjectId);

            get_tbl_ProjectDPR(ProjectId);
        }
    }
}