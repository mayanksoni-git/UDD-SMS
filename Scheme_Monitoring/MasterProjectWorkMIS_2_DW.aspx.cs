using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_2_DW : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                int District_Id = Convert.ToInt32(Request.QueryString["District_Id"].ToString().Trim());
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                get_tbl_ProjectWorkGO(ProjectWork_Id);
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }

    private void get_tbl_ProjectWorkGO(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "U");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtQuestionnaireU"] = ds.Tables[0];
            grdULBShare.DataSource = ds.Tables[0];
            grdULBShare.DataBind();

            grdULBShare.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(ProjectWorkGO_ULBShare)", "").ToString();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ProjectWorkGO_Id"] = 0;
                dr["ProjectWorkGO_CentralShare"] = 0;
                dr["ProjectWorkGO_StateShare"] = 0;
                dr["ProjectWorkGO_ULBShare"] = 0;
                dr["ProjectWorkGO_Centage"] = 0;
                dr["ProjectWorkGO_GO_Date"] = "";
                dr["ProjectWorkGO_GO_Number"] = "";
                dr["ProjectWorkGO_IssuingAuthority"] = "";
                dr["ProjectWorkGO_ULB_Id"] = 0;
                dr["ProjectWorkGO_Document_Path"] = "";
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtQuestionnaireU"] = ds.Tables[0];
                grdULBShare.DataSource = ds.Tables[0];
                grdULBShare.DataBind();
            }
            catch
            {
                grdULBShare.DataSource = null;
                grdULBShare.DataBind();
            }
        }
    }

    protected void grdCallProductDtls_PreRender(object sender, EventArgs e)
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FilePath = "";
        int scheme_Id = Convert.ToInt32(Request.QueryString["Id"].Trim());
        List<tbl_ProjectWorkGO> obj_tbl_ProjectWorkGO_Li = new List<tbl_ProjectWorkGO>();
        for (int i = 0; i < grdULBShare.Rows.Count; i++)
        {
            decimal Allocated_Budget = 0;
            decimal ULBShare_Budget = 0;

            TextBox txtGODate = grdULBShare.Rows[i].FindControl("txtFinancialTrans_GO_Date") as TextBox;
            TextBox txtULBShare = grdULBShare.Rows[i].FindControl("txtULBShare") as TextBox;
            TextBox txtFinancialTrans_GO_Number = grdULBShare.Rows[i].FindControl("txtFinancialTrans_GO_Number") as TextBox;
            TextBox txtDepartent_Name = grdULBShare.Rows[i].FindControl("txtDepartent_Name") as TextBox;
            DropDownList ddlIssuingAuthority = grdULBShare.Rows[i].FindControl("ddlIssuingAuthority") as DropDownList;
            FileUpload flUploadGO = grdULBShare.Rows[i].FindControl("flUploadULBGO") as FileUpload;
            FilePath = grdULBShare.Rows[i].Cells[8].Text.Trim();
            try
            {
                ULBShare_Budget = Convert.ToDecimal(txtULBShare.Text);
            }
            catch
            {
                ULBShare_Budget = 0;
            }

            Allocated_Budget = ULBShare_Budget;

            tbl_ProjectWorkGO obj_tbl_ProjectWorkGO = new tbl_ProjectWorkGO();
            try
            {
                obj_tbl_ProjectWorkGO.ProjectWorkGO_Id = Convert.ToInt32(grdULBShare.Rows[i].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkGO.ProjectWorkGO_Id = 0;
            }
            obj_tbl_ProjectWorkGO.ProjectWorkGO_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Date = txtGODate.Text.Trim();
            obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Number = txtFinancialTrans_GO_Number.Text.Trim();
            obj_tbl_ProjectWorkGO.ProjectWorkGO_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_ProjectWorkGO.ProjectWorkGO_Status = 1;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_EntryType = "U";
            obj_tbl_ProjectWorkGO.ProjectWorkGO_TotalRelease = Allocated_Budget * 100000;
            obj_tbl_ProjectWorkGO.ProjectWorkGO_IssuingAuthority = ddlIssuingAuthority.SelectedItem.Text.Trim();
            obj_tbl_ProjectWorkGO.ProjectWorkGO_DepartmentName = txtDepartent_Name.Text.Trim();
            obj_tbl_ProjectWorkGO.ProjectWorkGO_ULBShare = ULBShare_Budget * 100000;
            //(scheme_Id == 10 || scheme_Id == 11 || )
            if (obj_tbl_ProjectWorkGO.ProjectWorkGO_TotalRelease + obj_tbl_ProjectWorkGO.ProjectWorkGO_ULBShare > 0)
            {
                if (txtGODate.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill GO Date for ULB Share");
                    return;
                }
                if (txtFinancialTrans_GO_Number.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill GO No for ULB Share");
                    return;
                }
                if (ULBShare_Budget == 0)
                {
                    MessageBox.Show("Please Fill ULB Share");
                    return;
                }
                if (FilePath.Replace("&nbsp;", "") == "")
                {
                    if (!flUploadGO.HasFile)
                    {
                        MessageBox.Show("Please Upload GO Document.");
                        return;
                    }
                }
                if (flUploadGO.HasFile)
                {
                    obj_tbl_ProjectWorkGO.ProjectWorkGO_Document_Bytes = flUploadGO.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectWorkGO.ProjectWorkGO_Document_Bytes = null;
                }                
                obj_tbl_ProjectWorkGO_Li.Add(obj_tbl_ProjectWorkGO);
            }
        }
        if (obj_tbl_ProjectWorkGO_Li.Count > 0)
        {
            if (new DataLayer().Insert_Project_GO_Details(obj_tbl_ProjectWorkGO_Li, 0, Convert.ToInt32(hf_ProjectWork_Id.Value)))
            {
                Response.Redirect("MasterProjectWorkMIS_3.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
            }
            else
            {
                MessageBox.Show("Error In Saving Details!");
                return;
            }
        }
        else
        {
            Response.Redirect("MasterProjectWorkMIS_3.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
        }
    }

    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        Add_Questionire("S");
    }

    private void Add_Questionire(string Entry_Type)
    {
        DataTable dtQuestionnaire;
        if (ViewState["dtQuestionnaireU"] != null)
        {
            dtQuestionnaire = (DataTable)(ViewState["dtQuestionnaireU"]);
            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
            ViewState["dtQuestionnaireU"] = dtQuestionnaire;

            grdULBShare.DataSource = dtQuestionnaire;
            grdULBShare.DataBind();
        }
        else
        {
            dtQuestionnaire = new DataTable();

            DataColumn dc_ProjectWorkGO_Id = new DataColumn("ProjectWorkGO_Id", typeof(int));
            DataColumn dc_ProjectWorkGO_GO_Date = new DataColumn("ProjectWorkGO_GO_Date", typeof(string));
            DataColumn dc_ProjectWorkGO_GO_Number = new DataColumn("ProjectWorkGO_GO_Number", typeof(string));
            DataColumn dc_ProjectWorkGO_CentralShare = new DataColumn("ProjectWorkGO_CentralShare", typeof(decimal));
            DataColumn dc_ProjectWorkGO_StateShare = new DataColumn("ProjectWorkGO_StateShare", typeof(decimal));
            DataColumn dc_ProjectWorkGO_ULBShare = new DataColumn("ProjectWorkGO_ULBShare", typeof(decimal));
            DataColumn dc_ProjectWorkGO_Centage = new DataColumn("ProjectWorkGO_Centage", typeof(decimal));
            DataColumn dc_ProjectWorkGO_IssuingAuthority = new DataColumn("ProjectWorkGO_IssuingAuthority", typeof(string));
            DataColumn dc_ProjectWorkGO_ULB_Id = new DataColumn("ProjectWorkGO_ULB_Id", typeof(int));
            DataColumn dc_ProjectWorkGO_Document_Path = new DataColumn("ProjectWorkGO_Document_Path", typeof(string));

            dtQuestionnaire.Columns.AddRange(new DataColumn[] { dc_ProjectWorkGO_Id, dc_ProjectWorkGO_GO_Date, dc_ProjectWorkGO_GO_Number, dc_ProjectWorkGO_CentralShare, dc_ProjectWorkGO_StateShare, dc_ProjectWorkGO_ULBShare, dc_ProjectWorkGO_Centage, dc_ProjectWorkGO_IssuingAuthority, dc_ProjectWorkGO_ULB_Id, dc_ProjectWorkGO_Document_Path });

            DataRow dr = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(dr);
            ViewState["dtQuestionnaireU"] = dtQuestionnaire;

            grdULBShare.DataSource = dtQuestionnaire;
            grdULBShare.DataBind();
        }
    }

    protected void grdULBShare_PreRender(object sender, EventArgs e)
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

    protected void btnQuestionnaireU_Click(object sender, ImageClickEventArgs e)
    {
        Add_Questionire("U");
    }

    protected void btnDeleteQuestionnaireU_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtQuestionnaireU"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtQuestionnaireU"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdULBShare.DataSource = dt;
            grdULBShare.DataBind();
            ViewState["dtQuestionnaireU"] = dt;
        }
    }

    protected void grdULBShare_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[8].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[1].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkULB");
                lnkBtn.Visible = false;
            }
        }
    }

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS_3.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
    }

    protected void btnDeleteULB_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWorkGO_Id = 0;
        try
        {
            ProjectWorkGO_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectWorkGO_Id = 0;
        }
        if (ProjectWorkGO_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectGO(ProjectWorkGO_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            get_tbl_ProjectWorkGO(ProjectWork_Id);
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
}
