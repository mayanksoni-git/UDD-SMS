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

public partial class Master_Inventory_Category : System.Web.UI.Page
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
            get_tbl_Inventory_Category();
        }
    }

    private void get_tbl_Inventory_Category()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Inventory_Category();
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
        tbl_Inventory_Category obj_tbl_Inventory_Category = new tbl_Inventory_Category();
        if (hf_Inventory_Category_Id.Value == "0" || hf_Inventory_Category_Id.Value == "")
        {
            obj_tbl_Inventory_Category.Inventory_Category_Id = 0;
        }
        else
        {
            obj_tbl_Inventory_Category.Inventory_Category_Id = Convert.ToInt32(hf_Inventory_Category_Id.Value);
        }
        obj_tbl_Inventory_Category.Inventory_Category_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtInventory_Category.Text.Trim() == string.Empty)
        {
            Msg = "Give Inventory_Category";
            txtInventory_Category.Focus();
            return;
        }
        obj_tbl_Inventory_Category.Inventory_Category_Name = txtInventory_Category.Text.Trim();
        obj_tbl_Inventory_Category.Inventory_Category_Status = 1;
        //obj_tbl_Inventory_Category.Inventory_Category_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Insert_tbl_Inventory_Category(obj_tbl_Inventory_Category, obj_tbl_Inventory_Category.Inventory_Category_Id, ref Msg))
        {
            MessageBox.Show("Inventory_Category Created Successfully ! ");
            reset();
            get_tbl_Inventory_Category();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Inventory_Category Already Exist. Give another! ");
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
        txtInventory_Category.Text = "";
        hf_Inventory_Category_Id.Value = "0";
        get_tbl_Inventory_Category();
        mp1.Hide();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int Inventory_Category_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_Inventory_Category_Id.Value = Inventory_Category_Id.ToString();
        txtInventory_Category.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[2].Text.Trim();
        btnDelete.Visible = true;
        mp1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Inventory_Category_Id = Convert.ToInt32(hf_Inventory_Category_Id.Value);
        if (new DataLayer().Delete_Inventory_Category(Inventory_Category_Id, Person_Id))
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
        txtInventory_Category.Text = "";
        hf_Inventory_Category_Id.Value = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}