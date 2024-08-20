using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_6 : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                get_UC_Details(ProjectWork_Id);

            }
        }
    }
    protected void btnAction_Click(object sender, ImageClickEventArgs e)
    {
        mp1.Show();
    }
    protected void btnUpdateAction_Click(object sender, EventArgs e)
    {

    }
    private void get_UC_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectUC(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
            ViewState["dtUC"] = ds.Tables[0];
        }
        else
        {
            DataTable dt = new DataTable();
            DataColumn dc_1 = new DataColumn("ProjectUC_Id", typeof(int));
            DataColumn dc_2 = new DataColumn("ProjectUC_SubmitionDate", typeof(string));
            DataColumn dc_3 = new DataColumn("ProjectUC_Comments", typeof(string));
            DataColumn dc_4 = new DataColumn("ProjectUC_Achivment", typeof(decimal));
            DataColumn dc_5 = new DataColumn("ProjectUC_Document", typeof(string));
            dt.Columns.AddRange(new DataColumn[] { dc_1, dc_2, dc_3, dc_4, dc_5 });
            DataRow dr = dt.NewRow();
            dr["ProjectUC_Id"] = 0;
            dt.Rows.Add(dr);

            ViewState["dtUC"] = dt;
            grdCallProductDtls.DataSource = dt;
            grdCallProductDtls.DataBind();
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
        List<tbl_ProjectUC> obj_tbl_ProjectUC_Li = new List<tbl_ProjectUC>();
        for (int i = 0; i < grdCallProductDtls.Rows.Count; i++)
        {
            TextBox txtUCDate = grdCallProductDtls.Rows[i].FindControl("txtUCDate") as TextBox;
            TextBox txtUC_Number = grdCallProductDtls.Rows[i].FindControl("txtUC_Number") as TextBox;
            TextBox txtUCP = grdCallProductDtls.Rows[i].FindControl("txtUCP") as TextBox;
            FileUpload flUploadUC = grdCallProductDtls.Rows[i].FindControl("flUploadUC") as FileUpload;
            FilePath = grdCallProductDtls.Rows[i].Cells[1].Text.Trim();

            tbl_ProjectUC obj_tbl_ProjectUC = new tbl_ProjectUC();
            obj_tbl_ProjectUC.ProjectUC_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectUC.ProjectUC_SubmitionDate = txtUCDate.Text.Trim();
            obj_tbl_ProjectUC.ProjectUC_Comments = txtUC_Number.Text.Trim();
            obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_ProjectUC.ProjectUC_Status = 1;
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Id = Convert.ToInt32(grdCallProductDtls.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectUC.ProjectUC_Id = 0;
            }
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Achivment = Convert.ToDecimal(txtUCP.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectUC.ProjectUC_Achivment = 0;
            }
            if (obj_tbl_ProjectUC.ProjectUC_Achivment > 0)
            {
                if (txtUCDate.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill UC Date");
                    return;
                }
                if (txtUC_Number.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill UC Number");
                    return;
                }
                if (FilePath.Replace("&nbsp;", "") == "")
                {
                    if (!flUploadUC.HasFile)
                    {
                        MessageBox.Show("Please Upload UC Document.");
                        return;
                    }
                }
                try
                {
                    if (flUploadUC.HasFile)
                    {
                        obj_tbl_ProjectUC.ProjectUC_Document_Bytes = flUploadUC.FileBytes;
                    }
                    else
                    {
                        obj_tbl_ProjectUC.ProjectUC_Document_Bytes = null;
                    }
                }
                catch
                {

                }
                obj_tbl_ProjectUC_Li.Add(obj_tbl_ProjectUC);
            }
        }

        bool flag = false;
        try
        {
            DataLayer dataLayer = new DataLayer();
            flag = dataLayer.Update_tbl_ProjectUC(obj_tbl_ProjectUC_Li, null, Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        catch (Exception ee)
        {

        }
        if (flag != false)
        {
            MessageBox.Show("Details Updated Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
        }
    }

    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        Add_Questionire("U");
    }

    private void Add_Questionire(string Entry_Type)
    {
        DataTable dtUC;
        if (ViewState["dtUC"] != null)
        {
            dtUC = (DataTable)(ViewState["dtUC"]);
            DataRow dr = dtUC.NewRow();
            dtUC.Rows.Add(dr);
            ViewState["dtUC"] = dtUC;

            grdCallProductDtls.DataSource = dtUC;
            grdCallProductDtls.DataBind();
        }
        else
        {
            dtUC = new DataTable();

            DataColumn dc_Sr_No = new DataColumn("Sr_No", typeof(int));

            dtUC.Columns.AddRange(new DataColumn[] { dc_Sr_No });

            DataRow dr = dtUC.NewRow();
            dtUC.Rows.Add(dr);
            ViewState["dtUC"] = dtUC;

            grdCallProductDtls.DataSource = dtUC;
            grdCallProductDtls.DataBind();
        }
    }

    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtUC"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtUC"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdCallProductDtls.DataSource = dt;
            grdCallProductDtls.DataBind();
            ViewState["dtUC"] = dt;
        }
    }
        
    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectUC_Document = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (ProjectUC_Document != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkUCDoc");
                lnkBtn.Visible = false;
            }
        }
    }

    protected void btnDeleteUC_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectUC_Id = 0;
        try
        {
            ProjectUC_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectUC_Id = 0;
        }
        if (ProjectUC_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectUC(ProjectUC_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            get_UC_Details(ProjectWork_Id);
            MessageBox.Show("Deleted Successfully .");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }
}
