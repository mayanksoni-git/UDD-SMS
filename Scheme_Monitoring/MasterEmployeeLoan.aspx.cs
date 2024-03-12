using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeLoan : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            get_tbl_Zone();
            get_tbl_EmployeeLoan(0);
            //LoanChecked.Visible =false;
        }
    }
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }
    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int HRMSEmployeeCode = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        try
        {
            HRMSEmployeeCode = Convert.ToInt32(txtEmployeeCode.Text.Trim());
        }
        catch
        {
            HRMSEmployeeCode = 0;
        }
        get_tbl_HRMSEmployee(Zone_Id,Circle_Id,Division_Id, HRMSEmployeeCode, 0);
    }
    private void get_tbl_HRMSEmployee(int Zone_Id, int Circle_Id, int Division_Id, int HRMSEmployeeCode, int HRMSEmployee_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_HRMSEmployeeDetails(Zone_Id, Circle_Id, Division_Id, HRMSEmployeeCode, HRMSEmployee_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost1.DataSource = ds.Tables[0];
            grdPost1.DataBind();
        }
        else
        {
            grdPost1.DataSource = null;
            grdPost1.DataBind();
        }
    }
    protected void grdPost1_PreRender(object sender, EventArgs e)
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
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int HRMSEmployee_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("ViewEmployeeRecord.aspx?HRMSEmployee_Id=" + HRMSEmployee_Id.ToString());
    }

    //protected void cbLoan_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (cbLoan.Checked != true)
    //    {
    //        LoanChecked.Visible = false;
    //    }
    //    if(cbLoan.Checked == true)
    //    {
    //        LoanChecked.Visible = true;
    //    }
    //}
    private void get_tbl_EmployeeLoan(int EmployeeLoan_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_EmployeeLoan(EmployeeLoan_Id);
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
    protected void txtMonth_TextChanged(object sender, EventArgs e)
    {
        if (txtMonth.Text != "")
        {
            try
            {
                int amount = Convert.ToInt32(txtLoanAmount.Text.Trim().ToString());
                float interest = Convert.ToInt32(txtRateOfInterest.Text.Trim().ToString());
                double total = (interest * amount) / 100;
                txtGrandTotal.Text = (total + amount).ToString();
                Bindgridview();
            }
            catch
            {
                MessageBox.Show("Please Enter Rate Of Interest ! ");
            }
        }
        else
        {
            MessageBox.Show("Please Enter Month! ");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_EmployeeLoan obj_tbl_EmployeeLoan = new tbl_EmployeeLoan();
        if (hf_EmployeeLoan_Id.Value == "0" || hf_EmployeeLoan_Id.Value == "")
        {
            obj_tbl_EmployeeLoan.EmployeeLoan_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_EmployeeLoan.EmployeeLoan_Id = Convert.ToInt32(hf_EmployeeLoan_Id.Value);
            }
            catch
            {
                obj_tbl_EmployeeLoan.EmployeeLoan_Id = 0;
            }
        }
        obj_tbl_EmployeeLoan.EmployeeLoan_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtEmployeeCode.Text.Trim() == string.Empty)
        {
            Msg = "Give EmployeeLoan";
            txtEmployeeCode.Focus();
            return;
        }
        obj_tbl_EmployeeLoan.EmployeeLoan_EmpCode = txtEmployeeCode.Text.Trim();
        obj_tbl_EmployeeLoan.EmployeeLoan_Type = ddlLoanType.SelectedValue;
        obj_tbl_EmployeeLoan.EmployeeLoan_Amount = Convert.ToInt32(txtLoanAmount.Text.Trim()).ToString();
        obj_tbl_EmployeeLoan.EmployeeLoan_Month = txtMonth.Text.Trim();
        obj_tbl_EmployeeLoan.EmployeeLoan_RateOfInterest = Convert.ToInt32(txtRateOfInterest.Text.Trim()).ToString();
        obj_tbl_EmployeeLoan.EmployeeLoan_Date = txtdate.Text.Trim();
        obj_tbl_EmployeeLoan.EmployeeLoan_GrandTotal = txtGrandTotal.Text.Trim().ToString();
        obj_tbl_EmployeeLoan.EmployeeLoan_Status = 1;
        if (new DataLayer().Insert_tbl_EmployeeLoan(obj_tbl_EmployeeLoan, obj_tbl_EmployeeLoan.EmployeeLoan_Id, ref Msg))
        {
            MessageBox.Show("EmployeeLoan Created Successfully ! ");
            reset();
            Bindgridview();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This EmployeeLoan Already Exist. Give another! ");
            }
            else
            {
                MessageBox.Show("Error ! ");
            }
            return;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int EmployeeLoan_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_EmployeeLoan_Id.Value = EmployeeLoan_Id.ToString();
        txtEmployeeCode.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[4].Text.Trim();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_EmployeeLoan(EmployeeLoan_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            txtEmployeeCode.Text = ds.Tables[0].Rows[0]["EmployeeLoan_EmpCode"].ToString();
            ddlLoanType.Text = ds.Tables[0].Rows[0]["EmployeeLoan_Type"].ToString();
            txtLoanAmount.Text = ds.Tables[0].Rows[0]["EmployeeLoan_Amount"].ToString();
            txtMonth.Text = ds.Tables[0].Rows[0]["EmployeeLoan_Month"].ToString();
            txtRateOfInterest.Text = ds.Tables[0].Rows[0]["EmployeeLoan_RateOfInterest"].ToString();
            txtdate.Text = ds.Tables[0].Rows[0]["EmployeeLoan_Date"].ToString();
            txtGrandTotal.Text = ds.Tables[0].Rows[0]["EmployeeLoan_GrandTotal"].ToString();
        }

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int EmployeeLoan_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (new DataLayer().Delete_EmployeeLoan(EmployeeLoan_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            get_tbl_EmployeeLoan(0);
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            reset();
            return;
        }
    }
    private void reset()
    {
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.SelectedValue = "0";
        txtEmployeeCode.Text = "";
        ddlLoanType.SelectedValue = "0";
        txtLoanAmount.Text = "";
        txtMonth.Text = "";
        txtRateOfInterest.Text = "";
        txtdate.Text = "";
        txtGrandTotal.Text = "";
        get_tbl_EmployeeLoan(0);
    }

    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        LoanEntry.Visible = true;
    }
    protected void grdLoanDetails_PreRender(object sender, EventArgs e)
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
    void Bindgridview()
    {
        DataTable data = new DataTable();

        DataColumn dc1 = new DataColumn("lblBorrow", typeof(int));
        DataColumn dc2 = new DataColumn("txtPay", typeof(int));
        DataColumn dc3 = new DataColumn("txtPay1", typeof(int));
        DataColumn dc4 = new DataColumn("txtPay2", typeof(int));
        DataColumn dc5 = new DataColumn("txtPay3", typeof(int));
        DataColumn dc6 = new DataColumn("txtPay4", typeof(decimal));

        data.Columns.AddRange(new DataColumn[] { dc1, dc2, dc3, dc4, dc5, dc6 });

        DataRow dr;
        float month = Convert.ToInt32(txtMonth.Text.Trim().ToString());
        float total = Convert.ToInt32(txtGrandTotal.Text.Trim().ToString()) / Convert.ToInt32(txtMonth.Text.Trim().ToString());
        float Amount = Convert.ToInt32(txtLoanAmount.Text.Trim().ToString()) / Convert.ToInt32(txtMonth.Text.Trim().ToString());
        float Interest = (Convert.ToInt32(txtGrandTotal.Text.Trim().ToString()) - Convert.ToInt32(txtLoanAmount.Text.Trim().ToString())) / Convert.ToInt32(txtMonth.Text.Trim().ToString());


        for (int i = 0; i < month; i++)
        {
            dr = data.NewRow();
            dr["lblBorrow"] = i + 1;
            dr["txtPay2"] = Amount;
            dr["txtPay3"] = Interest;
            dr["txtPay4"] = total;
            data.Rows.Add(dr);

            grdLoanDetails.DataSource = data;
            grdLoanDetails.DataBind();
        }
    }
}