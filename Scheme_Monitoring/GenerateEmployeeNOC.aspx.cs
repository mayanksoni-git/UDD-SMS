using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GenerateEmployeeNOC : System.Web.UI.Page
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
    private void get_tbl_HRMSEmployee(int Zone_Id, int Circle_Id, int Division_Id,int HRMSEmployeeCode, int HRMSEmployee_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_HRMSEmployeeDetails_Retired(Zone_Id, Circle_Id, Division_Id, HRMSEmployeeCode, HRMSEmployee_Id);
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
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int HRMSEmployeeCode = 0;
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
            HRMSEmployeeCode = Convert.ToInt32(txtEmployeeCode.Text.Trim());
        }
        catch
        {
            HRMSEmployeeCode = 0;
        }
        get_tbl_HRMSEmployee(Zone_Id, Circle_Id, Division_Id, HRMSEmployeeCode, 0);
    }
    
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        hf_HRMSEmployee_Id.Value = HRMSEmployee_Id.ToString();
        divEntry.Visible = true;
        DataSet ds = new DataLayer().get_tbl_NOCQuestionnaire();
        if (AllClasses.CheckDataSet(ds))
        {
            dgvQuestionnaire.DataSource = ds.Tables[0];
            dgvQuestionnaire.DataBind();
        }
        else
        {
            dgvQuestionnaire.DataSource = null;
            dgvQuestionnaire.DataBind();
        }
    }

    protected void dgvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtNOCQuestionnaire_Answer = e.Row.FindControl("txtNOCQuestionnaire_Answer") as TextBox;
            RadioButtonList rbtNOCQuestionnaire_Answer = e.Row.FindControl("rbtNOCQuestionnaire_Answer") as RadioButtonList;
            string NOCQuestionnaire_QuestionType = "S";
            try
            {
                NOCQuestionnaire_QuestionType = e.Row.Cells[1].Text.Trim();
            }
            catch
            {
                NOCQuestionnaire_QuestionType = "S";
            }
            if (NOCQuestionnaire_QuestionType == "Y")
            {
                rbtNOCQuestionnaire_Answer.Visible = true;
                txtNOCQuestionnaire_Answer.Visible = false;
            }
            else
            {
                rbtNOCQuestionnaire_Answer.Visible = false;
                txtNOCQuestionnaire_Answer.Visible = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        tbl_ProjectDPRApproval obj_tbl_ProjectDPRApproval = new tbl_ProjectDPRApproval();
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Status = 1;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        //obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Comments = txtComments.Text;
        //obj_tbl_ProjectDPRApproval.ProjectDPRApproval_Date = txtNOCDate.Text;
        obj_tbl_ProjectDPRApproval.ProjectDPRApproval_ProjectDPR_Id = Convert.ToInt32(hf_HRMSEmployee_Id.Value);

        List<tbl_EmployeeNOCQuestionnaire> obj_tbl_EmployeeNOCQuestionnaire_Li = new List<tbl_EmployeeNOCQuestionnaire>();
        for (int i = 0; i < dgvQuestionnaire.Rows.Count; i++)
        {
            tbl_EmployeeNOCQuestionnaire obj_tbl_EmployeeNOCQuestionnaire = new tbl_EmployeeNOCQuestionnaire();
            obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            string DPRQuestionnaire_QuestionType = "S";
            try
            {
                DPRQuestionnaire_QuestionType = dgvQuestionnaire.Rows[i].Cells[1].Text.Trim();
            }
            catch
            {
                DPRQuestionnaire_QuestionType = "S";
            }
            if (DPRQuestionnaire_QuestionType == "Y")
            {
                try
                {
                    obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Answer = (dgvQuestionnaire.Rows[i].FindControl("rbtDPRQuestionnaire_Answer") as RadioButtonList).SelectedItem.Text.Trim();
                }
                catch
                {

                }
            }
            else
            {
                obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Answer = (dgvQuestionnaire.Rows[i].FindControl("txtDPRQuestionnaire_Answer") as TextBox).Text.Trim();
            }
            obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Remark = (dgvQuestionnaire.Rows[i].FindControl("txtDPRQuestionnaire_Remark") as TextBox).Text.Trim();
            obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Employee_Id = Convert.ToInt32(hf_HRMSEmployee_Id.Value);
            obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Status = 1;
            try
            {
                obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Questionire_Id = Convert.ToInt32(dgvQuestionnaire.Rows[i].Cells[1].Text);
            }
            catch
            {
                obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Questionire_Id = 0;
            }
            if (obj_tbl_EmployeeNOCQuestionnaire.EmployeeNOCQuestionnaire_Answer != "")
                obj_tbl_EmployeeNOCQuestionnaire_Li.Add(obj_tbl_EmployeeNOCQuestionnaire);
        }
        if (obj_tbl_EmployeeNOCQuestionnaire_Li.Count == 0)
        {
            MessageBox.Show("Please Fill Atleast 1 NOC Issue Details");
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving NOC Details.");
            return;
        }
        //if ((new DataLayer()).Insert_tbl_ProjectDPRApproval(obj_tbl_ProjectDPRApproval, obj_tbl_EmployeeNOCQuestionnaire_Li, Convert.ToInt32(hf_Scheme_Id.Value), Convert.ToInt32(hf_Loop.Value), obj_tbl_ProjectDPRDocs_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), isFirst))
        //{
        //    MessageBox.Show("DPR Approved Successfully!");
        //    btnSearch_Click(null, null);
        //    return;
        //}
        //else
        //{
        //    MessageBox.Show("Error In DPR Approved!");
        //    return;
        //}
    }

    protected void dgvQuestionnaire_PreRender(object sender, EventArgs e)
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
