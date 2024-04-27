using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_4 : System.Web.UI.Page
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
            get_tbl_PhysicalProgressComponent();
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                load_Project(ProjectWork_Id);
                get_ProjectWork_Physical_Progress(ProjectWork_Id);
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }
    protected void get_ProjectWork_Physical_Progress(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_ProjectWork_Physical_Progress(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtPhysicalTarget.Text = ds.Tables[0].Rows[0]["ProjectWorkPhysicalTarget_Target"].ToString();
        }
    }
    protected void load_Project(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Edit(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_Project_Id.Value = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
        }
        else
        {
            hf_Project_Id.Value = "";
        }
        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[3];
            grdPhysicalProgress.DataBind();

            ViewState["Component"] = ds.Tables[3];
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
            ViewState["Component"] = null;
        }
    }
    private void get_tbl_PhysicalProgressComponent()
    {
        int ProjectId = Convert.ToInt32(hf_Project_Id.Value);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PhysicalProgressComponent(ProjectId, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[0];
            grdPhysicalProgress.DataBind();
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
        }
    }

    protected void chkSelectAllApproveH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelectAllApproveH1 = (sender as CheckBox);
        for (int i = 0; i < grdPhysicalProgress.Rows.Count; i++)
        {
            CheckBox chkSelectAllApprove = grdPhysicalProgress.Rows[i].FindControl("chkPostPhysicalProgress") as CheckBox;
            chkSelectAllApprove.Checked = chkSelectAllApproveH1.Checked;
        }
    }


    protected void grdPhysicalProgress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string EnableList = e.Row.Cells[2].Text.Trim();
            CheckBox chkPostPhysicalProgress = (e.Row.FindControl("chkPostPhysicalProgress") as CheckBox);
            TextBox txtProgressNumber = (e.Row.FindControl("txtProgressNumber") as TextBox);
            if (Convert.ToInt32(hf_Project_Id.Value) == 1016 && EnableList == "Yes")
            {
                txtProgressNumber.Enabled = false;
            }
            else
            {
                txtProgressNumber.Enabled = true;
            }
            if (Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "")) > 0)
            {
                chkPostPhysicalProgress.Checked = true;
            }
            else
            {
                chkPostPhysicalProgress.Checked = false;
            }

            TextBox txtProposedNumberO = e.Row.FindControl("txtProposedNumberO") as TextBox;
            decimal Praposed = 0;
            try
            {
                Praposed = decimal.Parse(txtProposedNumberO.Text.ToString().Trim());
            }
            catch
            {
                Praposed = 0;
            }
            if (Praposed == 0)
            {
                txtProposedNumberO.ReadOnly = false;
            }
            else
            {
                txtProposedNumberO.ReadOnly = true;
            }
            if (Session["UserType"].ToString() == "1")
            {
                txtProposedNumberO.ReadOnly = false;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal physicalTarget = 0;
        try
        {
            physicalTarget = Convert.ToDecimal(txtPhysicalTarget.Text.Trim());
        }
        catch
        {
            physicalTarget = 0;
        }
        if (physicalTarget > 100)
        {
            MessageBox.Show("Physical Progress Can Not Be More Than 100%.");
            txtPhysicalTarget.Focus();
            return;
        }
        List<tbl_ProjectPkg_PhysicalProgress> obj_tbl_ProjectPkg_PhysicalProgress = new List<tbl_ProjectPkg_PhysicalProgress>();
        for (int i = 0; i < grdPhysicalProgress.Rows.Count; i++)
        {
            CheckBox checkBox = grdPhysicalProgress.Rows[i].FindControl("chkPostPhysicalProgress") as CheckBox;
            TextBox txtProposedNumber = grdPhysicalProgress.Rows[i].FindControl("txtProposedNumber") as TextBox;
            TextBox txtProgressNumber = grdPhysicalProgress.Rows[i].FindControl("txtProgressNumber") as TextBox;
            TextBox txtRemarks = grdPhysicalProgress.Rows[i].FindControl("txtRemarks") as TextBox;
            TextBox txtProposedNumberO = grdPhysicalProgress.Rows[i].FindControl("txtProposedNumberO") as TextBox;
            decimal ProposedNumber_Prev = 0;
            if (checkBox.Checked == true)
            {
                tbl_ProjectPkg_PhysicalProgress obj_tbl_ProjectPkg_PhysicalProgress1 = new tbl_ProjectPkg_PhysicalProgress();
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue = Convert.ToDecimal(txtProposedNumber.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue = 0;
                }
                try
                {
                    ProposedNumber_Prev = Convert.ToDecimal(txtProposedNumber.ToolTip.Trim());
                }
                catch
                {
                    ProposedNumber_Prev = 0;
                }
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF = Convert.ToDecimal(txtProposedNumberO.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF = 0;
                }
                try
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value = Convert.ToDecimal(txtProgressNumber.Text.Trim());
                }
                catch
                {
                    obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value = 0;
                }
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectUC_PhysicalProgress_WithheldProgress = 0;
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Functional = obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value;
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_NonFunctional = 0;
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Remarks = txtRemarks.Text.Trim();
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_PhysicalProgressComponent_Id = Convert.ToInt32(grdPhysicalProgress.Rows[i].Cells[0].Text.Trim());
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_PrjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
                obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Status = 1;

                //if (obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value > obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue)
                //{
                //    MessageBox.Show("Completed (Number) Should Not Be More Than Proposed (Number) ar Sr No " + (i + 1).ToString());
                //    return;
                //}
                if (obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF == 0)
                {
                    MessageBox.Show("Proposed (Number) As Per Origional Should Not Be 0 at Sr No " + (i + 1).ToString());
                    return;
                }
                //if (obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValueF < obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue)
                //{
                //    MessageBox.Show("Proposed (Number) As Per Origional Should Be More Than Or Equal To Proposed (Number) As Per Actual at Sr No " + (i + 1).ToString());
                //    return;
                //}
                //if (Session["UserType"].ToString() != "1")
                //{
                //    if (obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_MasterValue < ProposedNumber_Prev)
                //    {
                //        MessageBox.Show("Proposed (Number) As Per Actual Should Be More Than Or Equal To Proposed (Number) As Per Actual Previously Filled. You can Not Reduce This Figure at Sr No " + (i + 1).ToString());
                //        return;
                //    }
                //}
                if ((obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_NonFunctional + obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Functional) > obj_tbl_ProjectPkg_PhysicalProgress1.ProjectPkg_PhysicalProgress_Value)
                {
                    MessageBox.Show("Functional (Number) + Non Functional (Number) Should Not Be More Than Completed (Number) at Sr No " + (i + 1).ToString());
                    return;
                }
                obj_tbl_ProjectPkg_PhysicalProgress.Add(obj_tbl_ProjectPkg_PhysicalProgress1);
            }
        }
        if (grdPhysicalProgress.Rows.Count > 0 && obj_tbl_ProjectPkg_PhysicalProgress.Count == 0)
        {
            MessageBox.Show("Please Fill Component Proposed Data");
            return;
        }
        if ((new DataLayer()).Insert_tbl_Project_Component_Deleverables(Convert.ToInt32(hf_ProjectWork_Id.Value), Convert.ToInt32(Session["Person_Id"].ToString()), obj_tbl_ProjectPkg_PhysicalProgress, physicalTarget))
        {
            Response.Redirect("MasterProjectWorkMIS_5.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Component"];
        if (AllClasses.CheckDt(dt))
        {
            string Name = "Component";
            //GridViewExportUtil.WriteExcelWithNPOI(ExcelFileType.xlsx, ds, Name, this.Response);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, Name);

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
}