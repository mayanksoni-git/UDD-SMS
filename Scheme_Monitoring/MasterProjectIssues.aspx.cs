using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterProjectIssues : System.Web.UI.Page
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
            get_tbl_ProjectIssue();
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
    private void get_tbl_ProjectIssue()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectIssue(0);
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
        string Msg = "";
        tbl_ProjectIssue obj_tbl_ProjectIssue = new tbl_ProjectIssue();
        if (hf_ProjectIssue_Id.Value == "0" || hf_ProjectIssue_Id.Value == "")
        {
            obj_tbl_ProjectIssue.ProjectIssue_Id = 0;
        }
        else
        {
            obj_tbl_ProjectIssue.ProjectIssue_Id = Convert.ToInt32(hf_ProjectIssue_Id.Value);
        }
        obj_tbl_ProjectIssue.ProjectIssue_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtProjectIssue.Text.Trim() == string.Empty)
        {
            Msg = "Give Project Issue";
            txtProjectIssue.Focus();
            return ;
        }
        if (ddlProjectMaster.SelectedValue == "0")
        {
            Msg = "Please Select A Scheme";
            ddlProjectMaster.Focus();
            return;
        }
        obj_tbl_ProjectIssue.ProjectIssue_Name = txtProjectIssue.Text.Trim();
        obj_tbl_ProjectIssue.ProjectIssue_Project_Id = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        obj_tbl_ProjectIssue.ProjectIssue_Status = 1;
        
        if (new DataLayer().Insert_tbl_ProjectIssue(obj_tbl_ProjectIssue, obj_tbl_ProjectIssue.ProjectIssue_Id, ref Msg))
        {
            MessageBox.Show("Project Type Created Successfully ! ");
            reset();
            get_tbl_ProjectIssue();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Project Type Already Exist. Give another! ");
            }
            else
            {
                MessageBox.Show("Error ! ");
            }
            return;
        }
    }

    private void reset()
    {
        txtProjectIssue.Text = "";
        hf_ProjectIssue_Id.Value = "0";
        get_tbl_ProjectIssue();
        mp1.Hide();
    }

   
    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectIssue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        hf_ProjectIssue_Id.Value = ProjectIssue_Id.ToString();
        txtProjectIssue.Text = gr.Cells[4].Text.Trim();
        try
        {
            ddlProjectMaster.SelectedValue = gr.Cells[1].Text.Trim();
        }
        catch
        {
            ddlProjectMaster.SelectedValue = "0";
        }
        mp1.Show(); 
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int ProjectIssue_Id = Convert.ToInt32(hf_ProjectIssue_Id.Value);
        if (new DataLayer().Delete_ProjectIssue(ProjectIssue_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            reset();
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtProjectIssue.Text = "";
        hf_ProjectIssue_Id.Value = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}
