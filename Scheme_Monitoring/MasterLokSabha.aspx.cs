using Obout.Ajax.UI.HTMLEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterLokSabha : System.Web.UI.Page
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
            get_tbl_Jurisdiction();
            get_tbl_LokSabha();
        }
    }
    private void get_tbl_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataTextField = "Circle_Name";
            ddlDistrict.DataValueField = "Circle_Id";
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataBind();
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }
    private void get_tbl_LokSabha()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_LokSabha();
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
        if (txtLokSabha.Text.Trim() == string.Empty)
        {
            Msg = "Give Lok Sabha";
            txtLokSabha.Focus();
            return;
        }
        tbl_LokSabha obj_tbl_LokSabha = new tbl_LokSabha();
        if (hf_LokSabha_Id.Value == "0" || hf_LokSabha_Id.Value == "")
        {
            obj_tbl_LokSabha.LokSabha_Id = 0;
        }
        else
        {
            obj_tbl_LokSabha.LokSabha_Id = Convert.ToInt32(hf_LokSabha_Id.Value);
        }
        obj_tbl_LokSabha.LokSabha_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_LokSabha.LokSabha_Name = txtLokSabha.Text.Trim();
        obj_tbl_LokSabha.LokSabha_Status = 1;

        List<tbl_LokSabhaDistrictLink> obj_tbl_LokSabhaDistrictLink_Li = new List<tbl_LokSabhaDistrictLink>();
        foreach (ListItem listItem in ddlDistrict.Items)
        {
            if (listItem.Selected)
            {
                tbl_LokSabhaDistrictLink obj_tbl_LokSabhaDistrictLink = new tbl_LokSabhaDistrictLink();
                obj_tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_LokSabhaId = obj_tbl_LokSabha.LokSabha_Id;
                obj_tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_DistrictId = Convert.ToInt32(listItem.Value);
                obj_tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_Status = 1;
                obj_tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_LokSabhaDistrictLink_Li.Add(obj_tbl_LokSabhaDistrictLink);
            }
        }
        if (new DataLayer().Insert_tbl_LokSabha(obj_tbl_LokSabha, obj_tbl_LokSabhaDistrictLink_Li, obj_tbl_LokSabha.LokSabha_Id, ref Msg))
        {
            MessageBox.Show("Lok Sabha Created Successfully ! ");
            reset();
            get_tbl_LokSabha();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Lok Sabha Already Exist. Give another! ");
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
        txtLokSabha.Text = "";
        ddlDistrict.SelectedValue= "0";
        hf_LokSabha_Id.Value = "0";
        get_tbl_LokSabha();
        divCreateNew.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int LokSabha_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_LokSabha_Id.Value = LokSabha_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Edit_tbl_LokSabha(LokSabha_Id.ToString());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtLokSabha.Text = ds.Tables[0].Rows[0]["LokSabha_Name"].ToString();
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                foreach (ListItem listItem in ddlDistrict.Items)
                {
                    if (ds.Tables[1].Rows[i]["LokSabhaDistrictLink_DistrictId"].ToString() == listItem.Value)
                    {
                        listItem.Selected = true;
                        break;
                    }
                }
            }
        }
        divCreateNew.Visible = true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int LokSabha_Id = Convert.ToInt32(hf_LokSabha_Id.Value);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_LokSabha(LokSabha_Id, Person_Id))
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
        txtLokSabha.Text = "";
        hf_LokSabha_Id.Value = "0";
        divCreateNew.Visible = true;
    }
}
