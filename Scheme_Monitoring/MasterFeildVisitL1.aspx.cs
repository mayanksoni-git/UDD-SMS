using Obout.Ajax.UI.HTMLEditor;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFeildVisitL1 : System.Web.UI.Page
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
            get_tbl_FeildVisitL1();
        }
    }
    
    private void get_tbl_FeildVisitL1()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FeildVisitL1();
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
        if (txtFeildVisitL1.Text.Trim() == string.Empty)
        {
            Msg = "Give Field Visit";
            txtFeildVisitL1.Focus();
            return;
        }
        tbl_FeildVisitL1 obj_tbl_FeildVisitL1 = new tbl_FeildVisitL1();
        if (hf_FeildVisitL1_Id.Value == "0" || hf_FeildVisitL1_Id.Value == "")
        {
            obj_tbl_FeildVisitL1.FeildVisitL1_Id = 0;
        }
        else
        {
            obj_tbl_FeildVisitL1.FeildVisitL1_Id = Convert.ToInt32(hf_FeildVisitL1_Id.Value);
        }
        obj_tbl_FeildVisitL1.FeildVisitL1_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_FeildVisitL1.FeildVisitL1_Name = txtFeildVisitL1.Text.Trim();
        obj_tbl_FeildVisitL1.FeildVisitL1_Status = 1;

        if (obj_tbl_FeildVisitL1 == null)
        {
            MessageBox.Show(Msg);
            return;
        }
        if (new DataLayer().Insert_tbl_FeildVisitL1(obj_tbl_FeildVisitL1, obj_tbl_FeildVisitL1.FeildVisitL1_Id, ref Msg))
        {
            MessageBox.Show("Field Visit Created Successfully ! ");
            reset();
            get_tbl_FeildVisitL1();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Field Visit Already Exist. Give another! ");
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
        txtFeildVisitL1.Text = "";
        hf_FeildVisitL1_Id.Value = "0";
        get_tbl_FeildVisitL1();
        divCreateNew.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int FeildVisitL1_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_FeildVisitL1_Id.Value = FeildVisitL1_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Edit_tbl_FeildVisitL1(FeildVisitL1_Id.ToString());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtFeildVisitL1.Text = ds.Tables[0].Rows[0]["FeildVisitL1_Name"].ToString();
        }
        divCreateNew.Visible = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int FeildVisitL1_Id = Convert.ToInt32(hf_FeildVisitL1_Id.Value);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_FeildVisitL1(FeildVisitL1_Id, Person_Id))
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
        txtFeildVisitL1.Text = "";
        hf_FeildVisitL1_Id.Value = "0";
        divCreateNew.Visible = true;
    }
}
