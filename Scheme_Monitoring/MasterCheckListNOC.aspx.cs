using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterCheckListNOC : System.Web.UI.Page
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
            ViewState["dtQuestionnaire"] = null;
            DataSet ds = new DataSet();

            ds = new DataLayer().get_tbl_NOCQuestionnaire();
            if (AllClasses.CheckDataSet(ds))
            {
                ViewState["dtQuestionnaire"] = ds.Tables[0];
                dgvQuestionnaire.DataSource = ds.Tables[0];
                dgvQuestionnaire.DataBind();
            }
            else
            {
                Add_Questionire();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<tbl_NOCQuestionnaire> obj_tbl_NOCQuestionnaire_Li = new List<tbl_NOCQuestionnaire>();

        TextBox txtNOCQuestionnaire_Name;
        TextBox txtNOCQuestionnaire_Sr;
        DropDownList ddlQuestionType;
        for (int i = 0; i < dgvQuestionnaire.Rows.Count; i++)
        {
            txtNOCQuestionnaire_Name = dgvQuestionnaire.Rows[i].FindControl("txtNOCQuestionnaire_Name") as TextBox;
            txtNOCQuestionnaire_Sr = dgvQuestionnaire.Rows[i].FindControl("txtNOCQuestionnaire_Sr") as TextBox;
            ddlQuestionType = dgvQuestionnaire.Rows[i].FindControl("ddlQuestionType") as DropDownList;
            if (txtNOCQuestionnaire_Name.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Questionire!!");
                txtNOCQuestionnaire_Name.Focus();
                return;
            }
            tbl_NOCQuestionnaire obj_tbl_NOCQuestionnaire = new tbl_NOCQuestionnaire();
            obj_tbl_NOCQuestionnaire.NOCQuestionnaire_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            try
            {
                obj_tbl_NOCQuestionnaire.NOCQuestionnaire_Id = Convert.ToInt32(dgvQuestionnaire.Rows[i].Cells[1].Text.Trim());
            }
            catch
            {
                obj_tbl_NOCQuestionnaire.NOCQuestionnaire_Id = 0;
            }
            
            obj_tbl_NOCQuestionnaire.NOCQuestionnaire_QuestionType = ddlQuestionType.SelectedValue;
            obj_tbl_NOCQuestionnaire.NOCQuestionnaire_Name = txtNOCQuestionnaire_Name.Text.Trim();
            obj_tbl_NOCQuestionnaire.NOCQuestionnaire_Sr = txtNOCQuestionnaire_Sr.Text.Trim();
            obj_tbl_NOCQuestionnaire.NOCQuestionnaire_Status = 1;
            obj_tbl_NOCQuestionnaire_Li.Add(obj_tbl_NOCQuestionnaire);
        }
        if (new DataLayer().Insert_tbl_NOCQuestionnaire(obj_tbl_NOCQuestionnaire_Li))
        {
            MessageBox.Show("Project DPR Configuration Created Successfully ! ");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error ! ");
            return;
        }
    }

    private void reset()
    {
        ViewState["dtQuestionnaire"] = null;
        dgvQuestionnaire.DataSource = null;
        dgvQuestionnaire.DataBind();
    }
    
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtQuestionnaire"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtQuestionnaire"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            dgvQuestionnaire.DataSource = dt;
            dgvQuestionnaire.DataBind();
            ViewState["dtQuestionnaire"] = dt;
        }
    }

    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        Add_Questionire();
    }

    private void Add_Questionire()
    {
        DataTable dtQuestionnaire;
        if (ViewState["dtQuestionnaire"] != null)
        {
            dtQuestionnaire = (DataTable)(ViewState["dtQuestionnaire"]);
            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
            ViewState["dtQuestionnaire"] = dtQuestionnaire;

            dgvQuestionnaire.DataSource = dtQuestionnaire;
            dgvQuestionnaire.DataBind();
        }
        else
        {
            dtQuestionnaire = new DataTable();

            DataColumn dc_NOCQuestionnaire_Id = new DataColumn("NOCQuestionnaire_Id", typeof(int));
            DataColumn dc_NOCQuestionnaire_Name = new DataColumn("NOCQuestionnaire_Name", typeof(string));
            DataColumn dc_NOCQuestionnaire_QuestionType = new DataColumn("NOCQuestionnaire_QuestionType", typeof(string));
            DataColumn dc_NOCQuestionnaire_Sr = new DataColumn("NOCQuestionnaire_Sr", typeof(string));

            dtQuestionnaire.Columns.AddRange(new DataColumn[] { dc_NOCQuestionnaire_Id, dc_NOCQuestionnaire_Name, dc_NOCQuestionnaire_QuestionType, dc_NOCQuestionnaire_Sr });

            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
            ViewState["dtQuestionnaire"] = dtQuestionnaire;

            dgvQuestionnaire.DataSource = dtQuestionnaire;
            dgvQuestionnaire.DataBind();
        }
    }

    protected void dgvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string NOCQuestionnaire_QuestionType = "S";
            DropDownList ddlQuestionType = e.Row.FindControl("ddlQuestionType") as DropDownList;
            try
            {
                NOCQuestionnaire_QuestionType = e.Row.Cells[1].Text.Trim();
            }
            catch
            {
                NOCQuestionnaire_QuestionType = "S";
            }
            ddlQuestionType.SelectedValue = NOCQuestionnaire_QuestionType;
        }
    }
}
