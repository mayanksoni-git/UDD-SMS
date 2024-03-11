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

public partial class MasterQualificationCriteria : System.Web.UI.Page
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
            get_tbl_ParentQualification();
            get_tbl_QualificationCriteria();
        }
    }
    private void get_tbl_ParentQualification()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ParentQualification();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlParentQualification, "ParentQualification_Name", "ParentQualification_Id");
        }
        else
        {
            ddlParentQualification.Items.Clear();
        }
    }
    private void get_tbl_QualificationCriteria()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_QualificationCriteria();
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
        if (ddlParentQualification.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Parent Qualification");
            ddlParentQualification.Focus();
            return;
        }
        if (txtQualificationCriteria.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Qualification Criteria");
            txtQualificationCriteria.Focus();
            return;
        }
        if (txtDocument_Count.Text.Trim() == "")
        {
            MessageBox.Show("Give Total Documents To Be Uploaded");
            txtDocument_Count.Focus();
            return;
        }
        if (txtOrder.Text == "")
        {
            MessageBox.Show("Give Display Order");
            txtOrder.Focus();
            return;
        }
        tbl_QualificationCriteria obj_tbl_QualificationCriteria = new tbl_QualificationCriteria();
        if (hf_QualificationCriteria_Id.Value == "0" || hf_QualificationCriteria_Id.Value == "")
        {
            obj_tbl_QualificationCriteria.QualificationCriteria_Id = 0;
        }
        else
        {
            obj_tbl_QualificationCriteria.QualificationCriteria_Id = Convert.ToInt32(hf_QualificationCriteria_Id.Value);
        }
        obj_tbl_QualificationCriteria.QualificationCriteria_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_QualificationCriteria.QualificationCriteria_Name = txtQualificationCriteria.Text.Trim();
        obj_tbl_QualificationCriteria.QualificationCriteria_ParentQualification_Id = Convert.ToInt32(ddlParentQualification.SelectedValue);
        try
        {
            obj_tbl_QualificationCriteria.QualificationCriteria_Order = Convert.ToInt32(txtOrder.Text.Trim());
        }
        catch
        {
            obj_tbl_QualificationCriteria.QualificationCriteria_Order = 0;
        }
        if (chkMandatory.Checked)
            obj_tbl_QualificationCriteria.QualificationCriteria_Mandatory = 1;
        else
            obj_tbl_QualificationCriteria.QualificationCriteria_Mandatory = 0;
        if (chkAutoCalculate.Checked)
            obj_tbl_QualificationCriteria.QualificationCriteria_Auto_Calculated = 1;
        else
            obj_tbl_QualificationCriteria.QualificationCriteria_Auto_Calculated = 0;
        obj_tbl_QualificationCriteria.QualificationCriteria_Upload_Document_Count = Convert.ToInt32(txtDocument_Count.Text.Trim());
        if (chkVerificationDocs.Checked)
            obj_tbl_QualificationCriteria.QualificationCriteria_Enable_Verification_Document = 1;
        else
            obj_tbl_QualificationCriteria.QualificationCriteria_Enable_Verification_Document = 0;
        obj_tbl_QualificationCriteria.QualificationCriteria_Status = 1;

        if (obj_tbl_QualificationCriteria == null)
        {
            MessageBox.Show(Msg);
            return;
        }
        if (new DataLayer().Insert_tbl_QualificationCriteria(obj_tbl_QualificationCriteria, obj_tbl_QualificationCriteria.QualificationCriteria_Id, ref Msg))
        {
            MessageBox.Show("Qualification Criteria Created Successfully ! ");
            reset();
            get_tbl_QualificationCriteria();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Qualification Criteria Already Exist. Give another! ");
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
        txtQualificationCriteria.Text = "";
        hf_QualificationCriteria_Id.Value = "0";
        txtOrder.Text = "";
        get_tbl_QualificationCriteria();
        mp1.Hide();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int QualificationCriteria_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int ParentQualification_Id = 0;
        try
        {
            ParentQualification_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            ParentQualification_Id = 0;
        }
        hf_QualificationCriteria_Id.Value = QualificationCriteria_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Edit_tbl_QualificationCriteria(QualificationCriteria_Id.ToString());
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtQualificationCriteria.Text = ds.Tables[0].Rows[0]["QualificationCriteria_Name"].ToString();
            txtDocument_Count.Text = ds.Tables[0].Rows[0]["QualificationCriteria_Upload_Document_Count"].ToString();
            txtOrder.Text = ds.Tables[0].Rows[0]["QualificationCriteria_Order"].ToString();
            try
            {
                ddlParentQualification.SelectedValue = ds.Tables[0].Rows[0]["QualificationCriteria_ParentQualification_Id"].ToString();
            }
            catch
            {
                ddlParentQualification.SelectedValue = "0";
            }
            if (ds.Tables[0].Rows[0]["QualificationCriteria_Auto_Calculated"].ToString() == "1")
            {
                chkAutoCalculate.Checked = true;
            }
            else
            {
                chkAutoCalculate.Checked = false;
            }
            if (ds.Tables[0].Rows[0]["QualificationCriteria_Mandatory"].ToString() == "1")
            {
                chkMandatory.Checked = true;
            }
            else
            {
                chkMandatory.Checked = false;
            }
            if (ds.Tables[0].Rows[0]["QualificationCriteria_Enable_Verification_Document"].ToString() == "1")
            {
                chkVerificationDocs.Checked = true;
            }
            else
            {
                chkVerificationDocs.Checked = false;
            }
        }
        mp1.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int QualificationCriteria_Id = Convert.ToInt32(hf_QualificationCriteria_Id.Value);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (new DataLayer().Delete_QualificationCriteria(QualificationCriteria_Id, Person_Id))
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
        txtQualificationCriteria.Text = "";
        txtDocument_Count.Text = "";
        txtOrder.Text = "";
        hf_QualificationCriteria_Id.Value = "0";
        ddlParentQualification.SelectedValue = "0";
        mp1.Show();
    }
}
