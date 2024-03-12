using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterCadre : System.Web.UI.Page
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
            get_tbl_Cadre();
        }

    }
    private void get_tbl_Cadre()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Cadre(0);
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
        tbl_Cadre obj_tbl_Cadre = new tbl_Cadre();
        if (hf_Cadre_Id.Value == "0" || hf_Cadre_Id.Value == "")
        {
            obj_tbl_Cadre.Cadre_Id = 0;
        }
        else
        {
            obj_tbl_Cadre.Cadre_Id = Convert.ToInt32(hf_Cadre_Id.Value);
        }
        obj_tbl_Cadre.Cadre_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtCadre.Text.Trim() == string.Empty)
        {
            Msg = "Give A Cadre";
            txtCadre.Focus();
            return;
        }
        obj_tbl_Cadre.Cadre_Name = txtCadre.Text.Trim();
        try
        {
            obj_tbl_Cadre.Cadre_Old_Rate = Convert.ToDecimal(txtGIS_New.Text.Trim());
        }
        catch
        {
            obj_tbl_Cadre.Cadre_Old_Rate = 0;
        }
        try
        {
            obj_tbl_Cadre.Cadre_New_Rate = Convert.ToDecimal(txtGIS_Old.Text.Trim());
        }
        catch
        {
            obj_tbl_Cadre.Cadre_New_Rate = 0;
        }
        obj_tbl_Cadre.Cadre_Status = 1;
        //obj_tbl_PayBand.PayBand_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Insert_tbl_Cadre(obj_tbl_Cadre, obj_tbl_Cadre.Cadre_Id, ref Msg))
        {
            MessageBox.Show("Cadre Created Successfully ! ");
            reset();
            get_tbl_Cadre();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Cadre Already Exist. Give another! ");
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
        txtCadre.Text = "";
        txtGIS_New.Text = "";
        txtGIS_Old.Text = "";
        hf_Cadre_Id.Value = "0";
        get_tbl_Cadre();
        mp1.Hide();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int Cadre_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_Cadre_Id.Value = Cadre_Id.ToString();
        txtCadre.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[2].Text.Trim();
        txtGIS_Old.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[3].Text.Trim();
        txtGIS_New.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[4].Text.Trim();
        btnDelete.Visible = true;
        mp1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Cadre_Id = Convert.ToInt32(hf_Cadre_Id.Value);
        if (new DataLayer().Delete_Cadre(Cadre_Id, Person_Id))
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
        txtCadre.Text = "";
        txtGIS_New.Text = "";
        txtGIS_Old.Text = "";
        hf_Cadre_Id.Value = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}