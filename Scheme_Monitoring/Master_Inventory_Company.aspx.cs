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

public partial class Master_Inventory_Company : System.Web.UI.Page
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
            get_tbl_Inventory_Company();
        }
    }

    private void get_tbl_Inventory_Company()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Inventory_Company();
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
        tbl_Inventory_Company obj_tbl_Inventory_Company = new tbl_Inventory_Company();
        if (hf_Inventory_Company_Id.Value == "0" || hf_Inventory_Company_Id.Value == "")
        {
            obj_tbl_Inventory_Company.Inventory_Company_Id = 0;
        }
        else
        {
            obj_tbl_Inventory_Company.Inventory_Company_Id = Convert.ToInt32(hf_Inventory_Company_Id.Value);
        }
        obj_tbl_Inventory_Company.Inventory_Company_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtInventory_Company.Text.Trim() == string.Empty)
        {
            Msg = "Give Inventory_Company";
            txtInventory_Company.Focus();
            return;
        }
        obj_tbl_Inventory_Company.Inventory_Company_Name = txtInventory_Company.Text.Trim();
        obj_tbl_Inventory_Company.Inventory_Company_Status = 1;
        //obj_tbl_Inventory_Company.Inventory_Company_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Insert_tbl_Inventory_Company(obj_tbl_Inventory_Company, obj_tbl_Inventory_Company.Inventory_Company_Id, ref Msg))
        {
            MessageBox.Show("Inventory_Company Created Successfully ! ");
            reset();
            get_tbl_Inventory_Company();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Inventory_Company Already Exist. Give another! ");
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
        txtInventory_Company.Text = "";
        hf_Inventory_Company_Id.Value = "0";
        get_tbl_Inventory_Company();
        mp1.Hide();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int Inventory_Company_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_Inventory_Company_Id.Value = Inventory_Company_Id.ToString();
        txtInventory_Company.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[2].Text.Trim();
        btnDelete.Visible = true;
        mp1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Inventory_Company_Id = Convert.ToInt32(hf_Inventory_Company_Id.Value);
        if (new DataLayer().Delete_Inventory_Company(Inventory_Company_Id, Person_Id))
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
        txtInventory_Company.Text = "";
        hf_Inventory_Company_Id.Value = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}