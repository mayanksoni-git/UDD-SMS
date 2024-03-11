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

public partial class MasterCaste : System.Web.UI.Page
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
            get_tbl_Caste();
        }
    }

    private void get_tbl_Caste()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Caste();
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
        tbl_Caste obj_tbl_Caste = new tbl_Caste();
        if (hf_Caste_Id.Value == "0" || hf_Caste_Id.Value == "")
        {
            obj_tbl_Caste.Caste_Id = 0;
        }
        else
        {
            obj_tbl_Caste.Caste_Id = Convert.ToInt32(hf_Caste_Id.Value);
        }
        obj_tbl_Caste.Caste_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtCaste.Text.Trim() == string.Empty)
        {
            Msg = "Give Caste";
            txtCaste.Focus();
            return;
        }
        obj_tbl_Caste.Caste_Name = txtCaste.Text.Trim();
        obj_tbl_Caste.Caste_Status = 1;
        if (new DataLayer().Insert_tbl_Caste(obj_tbl_Caste, obj_tbl_Caste.Caste_Id, ref Msg))
        {
            MessageBox.Show("Caste Created Successfully ! ");
            reset();
            get_tbl_Caste();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Caste Already Exist. Give another! ");
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
        txtCaste.Text = "";
        hf_Caste_Id.Value = "0";
        get_tbl_Caste();
        mp1.Hide();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int Caste_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_Caste_Id.Value = Caste_Id.ToString();
        txtCaste.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[2].Text.Trim();
        btnDelete.Visible = true;
        mp1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Caste_Id = Convert.ToInt32(hf_Caste_Id.Value);
        if (new DataLayer().Delete_Caste(Caste_Id, Person_Id))
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
        txtCaste.Text = "";
        hf_Caste_Id.Value = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}