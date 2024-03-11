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

public partial class MasterMOMType : System.Web.UI.Page
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
            get_tbl_MOMType();
        }
    }

    private void get_tbl_MOMType()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_MOMType();
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
        if (txtMOMType.Text.Trim() == string.Empty)
        {
            Msg = "Give MOM Type";
            txtMOMType.Focus();
            return ;
        }
        tbl_MOMType obj_tbl_MOMType = new tbl_MOMType();
        if (hf_MOMType_Id.Value == "0" || hf_MOMType_Id.Value == "")
        {
            obj_tbl_MOMType.MOMType_Id = 0;
        }
        else {
            obj_tbl_MOMType.MOMType_Id = Convert.ToInt32(hf_MOMType_Id.Value);
        }
        obj_tbl_MOMType.MOMType_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_MOMType.MOMType_Name = txtMOMType.Text.Trim();
        obj_tbl_MOMType.MOMType_Status = 1;

        if (obj_tbl_MOMType == null)
        {
            MessageBox.Show(Msg);
            return;
        }
        if (new DataLayer().Insert_tbl_MOMType(obj_tbl_MOMType, obj_tbl_MOMType.MOMType_Id, ref Msg))
        {
            MessageBox.Show("MOM Type Created Successfully ! ");
            reset();
            get_tbl_MOMType();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This MOM Type Already Exist. Give another! ");
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
        txtMOMType.Text = "";
        hf_MOMType_Id.Value = "0";
        get_tbl_MOMType();
        
        mp1.Hide();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int MOMType_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_MOMType_Id.Value = MOMType_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Edit_tbl_MOMType(MOMType_Id.ToString());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtMOMType.Text = ds.Tables[0].Rows[0]["MOMType_Name"].ToString();

        }
        mp1.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        int MOMType_Id = Convert.ToInt32(hf_MOMType_Id.Value);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_MOMType(MOMType_Id, Person_Id))
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
        txtMOMType.Text = "";
        hf_MOMType_Id.Value = "0";
       
        mp1.Show();
    }
}
