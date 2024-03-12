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

public partial class MasterDependency : System.Web.UI.Page
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
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
                ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
            }
            catch
            {
                ddlScheme.SelectedIndex = 0;
            }
        }
        else
        {
            ddlScheme.Items.Clear();
        }
    }
    private void get_tbl_ProjectIssue(int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectIssue(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlIssue, "ProjectIssue_Name", "ProjectIssue_Id");
        }
        else
        {
            ddlIssue.Items.Clear();
        }
    }
    private void get_tbl_Dependency(int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Dependency(Scheme_Id, 0);
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
        if (ddlIssue.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Project Issue");
            ddlIssue.Focus();
            return;
        }
        if (txtDependency.Text.Trim() == string.Empty)
        {
            Msg = "Give Dependency";
            txtDependency.Focus();
            return;
        }

        tbl_Dependency obj_tbl_Dependency = new tbl_Dependency();
        if (hf_Dependency_Id.Value == "0" || hf_Dependency_Id.Value == "")
        {
            obj_tbl_Dependency.Dependency_Id = 0;
        }
        else
        {
            obj_tbl_Dependency.Dependency_Id = Convert.ToInt32(hf_Dependency_Id.Value);
        }
        obj_tbl_Dependency.Dependency_Issue_Id = Convert.ToInt32(ddlIssue.SelectedValue);
        obj_tbl_Dependency.Dependency_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_Dependency.Dependency_Name = txtDependency.Text.Trim();
        obj_tbl_Dependency.Dependency_Status = 1;

        if (obj_tbl_Dependency == null)
        {
            MessageBox.Show(Msg);
            return;
        }
        if (new DataLayer().Insert_tbl_Dependency(obj_tbl_Dependency, obj_tbl_Dependency.Dependency_Id, ref Msg))
        {
            MessageBox.Show("Dependency Created Successfully ! ");
            reset();
            ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Dependency Already Exist. Give another! ");
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
        ddlIssue.SelectedValue = "0";
        txtDependency.Text = "";
        hf_Dependency_Id.Value = "0";
        ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
        mp1.Hide();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Dependency_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Issue_Id = 0;
        try
        {
            Issue_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Issue_Id = 0;
        }
        hf_Dependency_Id.Value = Dependency_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Edit_tbl_Dependency(Dependency_Id.ToString());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtDependency.Text = ds.Tables[0].Rows[0]["Dependency_Name"].ToString();
            try
            {
                ddlIssue.SelectedValue = ds.Tables[0].Rows[0]["Dependency_Issue_Id"].ToString();
            }
            catch
            {
                ddlIssue.SelectedValue = "0";
            }
        }
        mp1.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        int Dependency_Id = Convert.ToInt32(hf_Dependency_Id.Value);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_Dependency(Dependency_Id, Person_Id))
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
        btnDelete.Visible = false;
        txtDependency.Text = "";
        hf_Dependency_Id.Value = "0";
        ddlIssue.SelectedValue = "0";
        mp1.Show();
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            ddlIssue.Items.Clear();

            grdPost.DataSource = null;
            grdPost.DataBind();
        }
        else
        {
            get_tbl_ProjectIssue(Convert.ToInt32(ddlScheme.SelectedValue));
            get_tbl_Dependency(Convert.ToInt32(ddlScheme.SelectedValue));
        }
    }
}
