using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPersonArchitect : System.Web.UI.Page
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
        {//District
            get_tbl_Jurisdiction(3, 0);

            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    ddlDistrict.Enabled = false;
                }
                catch
                { }
            }

            get_Employee();
        }
        if (Session["UserType"].ToString() == "1")
        {
            btnAddNew.Visible = true;
        }
        else
        {
            btnAddNew.Visible = false;
        }
    }

    private void get_Employee()
    {
        bool showLinkedOnly = false;
        if (Session["UserType"].ToString() == "1")
        {
            showLinkedOnly = false;
        }
        else
        {
            showLinkedOnly = true;
        }
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee("14", 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, 0);
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
    private void get_tbl_Jurisdiction(int Level_Id, int Parent_Jurisdiction_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(Level_Id, Parent_Jurisdiction_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        tbl_PersonDetail obj_PersonDetail = new tbl_PersonDetail();
        try
        {
            obj_PersonDetail.Person_Id = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_PersonDetail.Person_Id = 0;
        }
        if (txtPersonName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Architect Name");
            txtPersonName.Focus();
            return;
        }
        if (txtPersonFName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Firm Name");
            txtPersonFName.Focus();
            return;
        }
        if (txtMobileNo1.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Architect Mobile No");
            txtMobileNo1.Focus();
            return;
        }
        if (txtMobileNo1.Text.Trim().Length != 10)
        {
            MessageBox.Show("Please Provide Correct Mobile No");
            txtMobileNo1.Focus();
            return;
        }
        if (ddlDistrict.SelectedValue == "0")
        {
            MessageBox.Show("Please Select District");
            ddlDistrict.Focus();
            return;
        }
        //if (ddlBank.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select Bank");
        //    return;
        //}
        //if (txtIFSCCode.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please Fill / Search For A IFSC No.");
        //    return;
        //}
        //if (txtAccountNo.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please Provide Account No.");
        //    return;
        //}
        obj_PersonDetail.Person_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_PersonDetail.Person_Id = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_PersonDetail.Person_Id = 0;
        }
        obj_PersonDetail.PersonDetail_Code = "";
        obj_PersonDetail.Person_BranchOffice_Id = 1;
        obj_PersonDetail.Person_AddressLine1 = txtAddress.Text.Trim();
        obj_PersonDetail.Person_EmailId = txtEmailId.Text.Trim();
        obj_PersonDetail.Person_Mobile1 = txtMobileNo1.Text.Trim();
        obj_PersonDetail.Person_Mobile2 = txtMobileNo2.Text.Trim();
        obj_PersonDetail.Person_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_PersonDetail.Person_Name = txtPersonName.Text.Trim();
        obj_PersonDetail.Person_FName = txtPersonFName.Text.Trim();
        obj_PersonDetail.Person_Status = 1;

        tbl_PersonJuridiction obj_PersonJuridiction = new tbl_PersonJuridiction();
        try
        {
            obj_PersonJuridiction.PersonJuridiction_Id = Convert.ToInt32(hf_PersonJuridiction_Id.Value);
        }
        catch
        {
            obj_PersonJuridiction.PersonJuridiction_Id = 0;
        }
        try
        {
            obj_PersonJuridiction.PersonJuridiction_PersonId = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_PersonJuridiction.PersonJuridiction_PersonId = 0;
        }
        obj_PersonJuridiction.M_Level_Id = 3;
        obj_PersonJuridiction.PersonJuridiction_PAN = txtPanNumber.Text;
        obj_PersonJuridiction.M_Jurisdiction_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        obj_PersonJuridiction.PersonJuridiction_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_PersonJuridiction.PersonJuridiction_DepartmentId = 0;
        obj_PersonJuridiction.PersonJuridiction_DesignationId = 0;
        obj_PersonJuridiction.PersonJuridiction_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_PersonJuridiction.PersonJuridiction_ParentPerson_Id = 0;
        try
        {
            obj_PersonJuridiction.M_Jurisdiction_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            obj_PersonJuridiction.M_Jurisdiction_Id = 0;
        }

        obj_PersonJuridiction.PersonJuridiction_Status = 1;

        obj_PersonJuridiction.PersonJuridiction_GSTIN = txtGSTIN.Text.Trim();
        obj_PersonJuridiction.PersonJuridiction_UserTypeId = 14;
        
        string msg = "";
        if ((new DataLayer()).Insert_Employee(obj_PersonDetail, obj_PersonJuridiction, null, Convert.ToInt32(hf_Person_Id.Value), null, null, null, null, null, null, ref msg))
        {
            if (msg == "")
            {
                if (Convert.ToInt32(hf_Person_Id.Value) > 0)
                {
                    MessageBox.Show("Architect Updated Successfully!");
                }
                else
                {
                    MessageBox.Show("Architect Created Successfully!");
                }
                reset();
                return;
            }
            else
            {
                MessageBox.Show(msg);
                return;
            }
        }
        else
        {
            MessageBox.Show("Error In Creating Architect!");
            return;
        }
    }

    private void reset()
    {
        hf_PersonJuridiction_Id.Value = "0";
        hf_Person_Id.Value = "0";
        txtAddress.Text = "";
        txtGSTIN.Text = "";
        txtEmailId.Text = "";
        txtMobileNo1.Text = "";
        txtMobileNo2.Text = "";
        txtAddress.Text = "";
        txtEmailId.Text = "";
        txtPersonFName.Text = "";
        txtPersonName.Text = "";
        get_Employee();

        divCreateNew.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Person_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_Person(Person_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            get_Employee();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
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

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        M_Jurisdiction_Detailed obj = new M_Jurisdiction_Detailed();
        ImageButton lnkUpdate = sender as ImageButton;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        (lnkUpdate.Parent.Parent as GridViewRow).BackColor = Color.LightGreen;
        divCreateNew.Visible = true;
        hf_PersonJuridiction_Id.Value = (lnkUpdate.Parent.Parent as GridViewRow).Cells[1].Text.Trim();
        hf_Person_Id.Value = (lnkUpdate.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
        GridViewRow grd = lnkUpdate.Parent.Parent as GridViewRow;
        string M_Jurisdiction_Id = grd.Cells[3].Text.Replace("&nbsp;", "").Trim();
        obj = (new DataLayer()).get_M_Jurisdiction_Detailed("3", M_Jurisdiction_Id);
        if (obj.District_Id > 0)
        {
            ddlDistrict.SelectedValue = obj.District_Id.ToString();
        }

        txtPersonName.Text = grd.Cells[10].Text.Replace("&nbsp;", "").Trim();
        txtPersonFName.Text = grd.Cells[11].Text.Replace("&nbsp;", "").Trim();
        txtAddress.Text = grd.Cells[15].Text.Replace("&nbsp;", "").Trim();
        txtGSTIN.Text = grd.Cells[16].Text.Replace("&nbsp;", "").Trim();
        txtEmailId.Text = grd.Cells[23].Text.Replace("&nbsp;", "").Trim();
        txtLandLine.Text = grd.Cells[12].Text.Replace("&nbsp;", "").Trim();
        txtMobileNo1.Text = grd.Cells[13].Text.Replace("&nbsp;", "").Trim();
        txtMobileNo2.Text = grd.Cells[14].Text.Replace("&nbsp;", "").Trim();
        
        txtPanNumber.Text = grd.Cells[28].Text.Replace("&nbsp;", "").Trim();
        divCreateNew.Focus();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        reset();
        divCreateNew.Visible = true;
    }
        
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
            if (Session["UserType"].ToString() == "1")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }

        }
    }
}