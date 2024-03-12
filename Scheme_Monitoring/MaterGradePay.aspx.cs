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

public partial class MaterGradePay : System.Web.UI.Page
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
            get_tbl_PayScale();
            get_tbl_GradePay();
        }
    }
    private void get_tbl_PayScale()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PayScale(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlPayScale, "PayScale_Name", "PayScale_Id");
        }
        else
        {
            ddlPayScale.Items.Clear();
        }
    }
    private void get_tbl_GradePay()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_GradePay(0);
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
        tbl_GradePay obj_tbl_GradePay = new tbl_GradePay();
        if (hf_GradePay_Id.Value == "0" || hf_GradePay_Id.Value == "")
        {
            obj_tbl_GradePay.GradePay_Id = 0;
        }
        else
        {
            obj_tbl_GradePay.GradePay_Id = Convert.ToInt32(hf_GradePay_Id.Value);
        }
        obj_tbl_GradePay.GradePay_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtGradePay.Text.Trim() == string.Empty)
        {
            Msg = "Give GradePay";
            txtGradePay.Focus();
            return;
        }
        obj_tbl_GradePay.GradePay_Value = txtGradePay.Text.Trim();
        obj_tbl_GradePay.GradePay_PayScaleId = Convert.ToInt32(ddlPayScale.SelectedValue);
        obj_tbl_GradePay.GradePay_Status = 1;
        //obj_tbl_GradePay.GradePay_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Insert_tbl_GradePay(obj_tbl_GradePay, obj_tbl_GradePay.GradePay_Id, ref Msg))
        {
            MessageBox.Show("Grade Pay Created Successfully ! ");
            reset();
            get_tbl_GradePay();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Grade Pay Already Exist. Give another! ");
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
        txtGradePay.Text = "";
        ddlPayScale.SelectedValue = "0";
        hf_GradePay_Id.Value = "0";
        get_tbl_GradePay();
        mp1.Hide();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int GradePay_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_GradePay_Id.Value = GradePay_Id.ToString();
        ddlPayScale.SelectedValue = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[1].Text.Trim();
        txtGradePay.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[4].Text.Trim();
        btnDelete.Visible = true;
        mp1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int GradePay_Id = Convert.ToInt32(hf_GradePay_Id.Value);
        if (new DataLayer().Delete_GradePay(GradePay_Id, Person_Id))
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
        txtGradePay.Text = "";
        ddlPayScale.SelectedValue = "0";
        hf_GradePay_Id.Value = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}