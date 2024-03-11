using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_1 : System.Web.UI.Page
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
            get_M_Jurisdiction();
            get_tbl_Project();
            get_tbl_Program();
            if (Session["PersonJuridiction_DesignationId"].ToString() == "1" || Session["PersonJuridiction_DesignationId"].ToString() == "33")
            {
                btnSkip.Visible = true;
            }
            else if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "4" || Session["UserType"].ToString() == "6" || Session["UserType"].ToString() == "8")
            {
                btnSkip.Visible = true;
            }
            else
            {
                btnSkip.Visible = false;
            }
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
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                load_Project(ProjectWork_Id);
                get_Package_Wise_Details(ProjectWork_Id);
            }
        }
    }
    protected void load_Project(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Edit(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_Scheme_Id.Value = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
            try
            {
                ddlDistrict.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_DistrictId"].ToString();
            }
            catch
            {
                ddlDistrict.SelectedValue = "0";
            }

            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            try
            {
                ddlULB.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_ULB_Id"].ToString();
            }
            catch
            {
                ddlULB.SelectedValue = "0";
            }
            try
            {
                ddlProjectMaster.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
            }
            catch
            {
                ddlProjectMaster.SelectedValue = "0";
            }
            try
            {
                ddlProgram.SelectedValue = ds.Tables[0].Rows[0]["ProjectWork_Is_Phase_1"].ToString();
            }
            catch
            {
                ddlProgram.SelectedValue = "0";
            }
            txtProjectCode.Text = ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString();
            txtProjectWorkName.Text = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            txtBudget.Text = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            txtCentage.Text = ds.Tables[0].Rows[0]["ProjectWork_Centage"].ToString();
            txtGONo.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
            txtGODate.Text = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();
            txtStartDate.Text = ds.Tables[0].Rows[0]["ProjectWork_StartDate"].ToString();
            txtCompleteDate.Text = ds.Tables[0].Rows[0]["ProjectWork_EndDate"].ToString();
            hf_GO_Path.Value = ds.Tables[0].Rows[0]["ProjectWork_GO_Path"].ToString();
            if (hf_GO_Path.Value=="")
            {
                aGO.Visible = false;
            }
        }
    }
    protected void btnDownload_GO_Click(object sender, ImageClickEventArgs e)
    {
        FileInfo fl = new FileInfo(Server.MapPath(".") + hf_GO_Path.Value);
        Response.Write("<script>window.open('fl','_blank')</script>");
        Response.Redirect(fl.ToString());
    }
    
    private void get_tbl_Program()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Program();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlProgram, "Program_Name", "Program_Id");
        }
        else
        {
            ddlProgram.Items.Clear();
        }
    }

    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
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
            AllClasses.FillDropDown(ds.Tables[0], ddlProjectMaster, "Project_Name", "Project_Id");
            try
            {
                ddlProjectMaster.SelectedValue = Session["Default_Scheme"].ToString();
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
        if (ddlDistrict.SelectedValue == "0")
        {
            MessageBox.Show("Please Select District");
            ddlDistrict.Focus();
            return;
        }
        if (txtProjectCode.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Code");
            txtProjectCode.Focus();
            return;
        }
        if (txtBudget.Text.Trim() == "" || txtBudget.Text.Trim() == "0")
        {
            MessageBox.Show("Please Provide Project Allocated Budget");
            txtBudget.Focus();
            return;
        }
        if (txtGODate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide GO Date");
            txtGODate.Focus();
            return;
        }
        if (txtGONo.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide GO No");
            txtGONo.Focus();
            return;
        }
        if (txtStartDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Start Date");
            txtStartDate.Focus();
            return;
        }
        if (txtCompleteDate.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Project Completion Date");
            txtCompleteDate.Focus();
            return;
        }
        if (hf_GO_Path.Value.Replace("&nbsp;", "") == "")
        {
            if (!flUploadGO.HasFiles)
            {
                MessageBox.Show("Please Upload GO");
                txtGONo.Focus();
                return;
            }
        }
        if (txtCentage.Text.Trim() == "" || Convert.ToDecimal(txtCentage.Text.Trim().Replace("0.00","0")) == 0)
        {
            MessageBox.Show("Please Provide Centage Amount");
            txtCentage.Focus();
            return;
        }
        tbl_ProjectWork obj_tbl_ProjectWork = new tbl_ProjectWork();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Id = 0;
        }
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Is_Phase_1 = Convert.ToInt32(ddlProgram.SelectedValue);
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Is_Phase_1 = 0;
        }
        obj_tbl_ProjectWork.ProjectWork_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        obj_tbl_ProjectWork.ProjectWork_GO_Date = txtGODate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_GO_No = txtGONo.Text.Trim();
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = Convert.ToDecimal(txtBudget.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Budget = 0;
        }
        
        try
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            obj_tbl_ProjectWork.ProjectWork_Centage = 0;
        }
        
        obj_tbl_ProjectWork.ProjectWork_Name = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Description = txtProjectWorkName.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_ProjectCode = txtProjectCode.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_StartDate = txtStartDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_EndDate = txtCompleteDate.Text.Trim();
        obj_tbl_ProjectWork.ProjectWork_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        string extGO = "";
        if (flUploadGO.HasFile)
        {
            obj_tbl_ProjectWork.ProjectWork_GO_Path_Bytes = flUploadGO.FileBytes;
            string[] _fname = flUploadGO.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            extGO = _fname[_fname.Length - 1];
        }
        obj_tbl_ProjectWork.ProjectWork_DistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        obj_tbl_ProjectWork.ProjectWork_DivisionId = 0;
        obj_tbl_ProjectWork.ProjectWork_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectWork.ProjectWork_Status = 1;
        List<tbl_ProjectWorkFundingPattern> obj_tbl_ProjectWorkFundingPattern_Li = new List<tbl_ProjectWorkFundingPattern>();

        List<tbl_ProjectWorkPkg> obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkg>();
        for (int i = 0; i < grdPackageDetails.Rows.Count; i++)
        {
            tbl_ProjectWorkPkg obj_tbl_ProjectWorkPkg = new tbl_ProjectWorkPkg();
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Id = Convert.ToInt32(grdPackageDetails.Rows[i].Cells[0].Text.ToString());
            DropDownList ddlGSTPercentage = grdPackageDetails.Rows[i].FindControl("ddlGSTPercentage") as DropDownList;
            RadioButtonList rbtGSTType = grdPackageDetails.Rows[i].FindControl("rbtGSTType") as RadioButtonList;
            TextBox txtextenddate = grdPackageDetails.Rows[i].FindControl("txtextenddate") as TextBox;
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_GST = rbtGSTType.SelectedValue;
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_Percent = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
            obj_tbl_ProjectWorkPkg.ProjectWorkPkg_ExtendDate = txtextenddate.Text;
            obj_tbl_ProjectWorkPkg_Li.Add(obj_tbl_ProjectWorkPkg);
        }

        string msg = "";
        if ((new DataLayer()).Insert_tbl_ProjectWork_MIS(obj_tbl_ProjectWork, obj_tbl_ProjectWorkFundingPattern_Li, obj_tbl_ProjectWorkPkg_Li, extGO,  null, null,ref msg))
        {
            if (hf_Scheme_Id.Value == "12")
            {
                Response.Redirect("MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
            else if (hf_Scheme_Id.Value == "3")
            {
                Response.Redirect("MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
            else if (hf_Scheme_Id.Value == "1015")
            {
                Response.Redirect("MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
            else
            {
                Response.Redirect("MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
            }
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
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

    protected void grdPackageDetails_PreRender(object sender, EventArgs e)
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

    protected void get_Package_Wise_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();

            grdPackageDetails.FooterRow.Cells[11].Text = "Agreement Amount (Tender Cost): ";
            grdPackageDetails.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(ProjectWorkPkg_AgreementAmount)", "").ToString();
        }
        else
        {
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
        }
    }

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        if (hf_Scheme_Id.Value == "12")
        {
            Response.Redirect("MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
        else if (hf_Scheme_Id.Value == "3")
        {
            Response.Redirect("MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
        else if (hf_Scheme_Id.Value == "1015")
        {
            Response.Redirect("MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
        else
        {
            Response.Redirect("MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&District_Id=" + ddlDistrict.SelectedValue + "&Id=" + hf_Scheme_Id.Value);
        }
    }

    protected void grdPackageDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string GST = e.Row.Cells[2].Text.Trim();
            string GST_Per = e.Row.Cells[3].Text.Trim();
            RadioButtonList rbtGSTType = e.Row.FindControl("rbtGSTType") as RadioButtonList;
            DropDownList ddlGSTPercentage = e.Row.FindControl("ddlGSTPercentage") as DropDownList;
            try
            {
                rbtGSTType.SelectedValue = GST;
            }
            catch
            {
                rbtGSTType.SelectedValue = "Include GST";
            }
            try
            {
                ddlGSTPercentage.SelectedValue = GST_Per;
            }
            catch
            {
                ddlGSTPercentage.SelectedValue = "12";
            }
        }
    }
}