﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterDPRConfiguration : System.Web.UI.Page
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
            get_tbl_Project();
        }
    }

    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        int Person_Id = 0;
        if (Session["UserType"].ToString() == "4")
        {
            Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Person_Id = 0;
        }
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
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<tbl_DPRQuestionnaire> obj_tbl_DPRQuestionnaire_Li = new List<tbl_DPRQuestionnaire>();

        TextBox txtDPRQuestionnaire_Name;
        TextBox txtDPRQuestionnaire_Sr;
        DropDownList ddlProjectType;
        DropDownList ddlQuestionType;
        for (int i = 0; i < dgvQuestionnaire.Rows.Count; i++)
        {
            txtDPRQuestionnaire_Name = dgvQuestionnaire.Rows[i].FindControl("txtDPRQuestionnaire_Name") as TextBox;
            txtDPRQuestionnaire_Sr = dgvQuestionnaire.Rows[i].FindControl("txtDPRQuestionnaire_Sr") as TextBox;
            ddlProjectType = dgvQuestionnaire.Rows[i].FindControl("ddlProjectType") as DropDownList;
            ddlQuestionType = dgvQuestionnaire.Rows[i].FindControl("ddlQuestionType") as DropDownList;
            if (txtDPRQuestionnaire_Name.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Questionire!!");
                txtDPRQuestionnaire_Name.Focus();
                return;
            }
            tbl_DPRQuestionnaire obj_tbl_DPRQuestionnaire = new tbl_DPRQuestionnaire();
            obj_tbl_DPRQuestionnaire.DPRQuestionnaire_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_DPRQuestionnaire.DPRQuestionnaire_ProjectId = Convert.ToInt32(hf_Project_Id.Value);
            try
            {
                obj_tbl_DPRQuestionnaire.DPRQuestionnaire_Id = Convert.ToInt32(dgvQuestionnaire.Rows[i].Cells[1].Text.Trim());
            }
            catch
            {
                obj_tbl_DPRQuestionnaire.DPRQuestionnaire_Id = 0;
            }
            try
            {
                obj_tbl_DPRQuestionnaire.DPRQuestionnaire_ProjectTypeId = Convert.ToInt32(ddlProjectType.SelectedValue);
            }
            catch
            {
                obj_tbl_DPRQuestionnaire.DPRQuestionnaire_ProjectTypeId = 0;
            }
            obj_tbl_DPRQuestionnaire.DPRQuestionnaire_QuestionType = ddlQuestionType.SelectedValue;
            obj_tbl_DPRQuestionnaire.DPRQuestionnaire_Name = txtDPRQuestionnaire_Name.Text.Trim();
            obj_tbl_DPRQuestionnaire.DPRQuestionnaire_Sr = txtDPRQuestionnaire_Sr.Text.Trim();
            obj_tbl_DPRQuestionnaire.DPRQuestionnaire_Status = 1;
            obj_tbl_DPRQuestionnaire_Li.Add(obj_tbl_DPRQuestionnaire);
        }


        if (new DataLayer().Insert_tbl_DPRQuestionnaire(obj_tbl_DPRQuestionnaire_Li))
        {
            MessageBox.Show("Project DPR Configuration Created Successfully ! ");
            reset();
            get_tbl_Project();
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
        hf_Project_Id.Value = "0";
        get_tbl_Project();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["dtQuestionnaire"] = null;
        ViewState["ProjectType"] = null;
        GridViewRow gr = ((sender as ImageButton).Parent.Parent as GridViewRow);
        int Project_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        hf_Project_Id.Value = Project_Id.ToString();
        DataSet ds = new DataSet();

        ds = new DataLayer().get_tbl_ProjectType(Project_Id, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["ProjectType"] = ds.Tables[0];
        }
        else
        {
            ViewState["ProjectType"] = null;
        }

        ds = new DataLayer().get_tbl_DPRQuestionnaire(Project_Id, 0);
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

        
        divhide.Visible = true;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Project_Id = Convert.ToInt32(hf_Project_Id.Value);
        if (new DataLayer().Delete_Project(Project_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
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

            DataColumn dc_DPRQuestionnaire_ProjectId = new DataColumn("DPRQuestionnaire_ProjectId", typeof(int));
            DataColumn dc_DPRQuestionnaire_Id = new DataColumn("DPRQuestionnaire_Id", typeof(int));
            DataColumn dc_DPRQuestionnaire_Name = new DataColumn("DPRQuestionnaire_Name", typeof(string));
            DataColumn dc_DPRQuestionnaire_ProjectTypeId = new DataColumn("DPRQuestionnaire_ProjectTypeId", typeof(int));
            DataColumn dc_DPRQuestionnaire_QuestionType = new DataColumn("DPRQuestionnaire_QuestionType", typeof(string));
            DataColumn dc_DPRQuestionnaire_Sr = new DataColumn("DPRQuestionnaire_Sr", typeof(string));

            dtQuestionnaire.Columns.AddRange(new DataColumn[] { dc_DPRQuestionnaire_ProjectId, dc_DPRQuestionnaire_Id, dc_DPRQuestionnaire_Name, dc_DPRQuestionnaire_ProjectTypeId, dc_DPRQuestionnaire_QuestionType, dc_DPRQuestionnaire_Sr });

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
            int DPRQuestionnaire_ProjectTypeId = 0;
            string DPRQuestionnaire_QuestionType = "S";
            DropDownList ddlProjectType = e.Row.FindControl("ddlProjectType") as DropDownList;
            DropDownList ddlQuestionType = e.Row.FindControl("ddlQuestionType") as DropDownList;
            try
            {
                DPRQuestionnaire_ProjectTypeId = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                DPRQuestionnaire_ProjectTypeId = 0;
            }
            try
            {
                DPRQuestionnaire_QuestionType = e.Row.Cells[3].Text.Trim();
            }
            catch
            {
                DPRQuestionnaire_QuestionType = "S";
            }
            if (ViewState["ProjectType"] != null)
            {
                DataTable dt = (DataTable)ViewState["ProjectType"];
                if (AllClasses.CheckDt(dt))
                {
                    AllClasses.FillDropDown_WithOutSelect(dt, ddlProjectType, "ProjectType_Name", "ProjectType_Id");

                    if (DPRQuestionnaire_ProjectTypeId > 0)
                    {
                        ddlProjectType.SelectedValue = DPRQuestionnaire_ProjectTypeId.ToString();
                    }
                }
            }
            ddlQuestionType.SelectedValue = DPRQuestionnaire_QuestionType;
        }

    }
}
