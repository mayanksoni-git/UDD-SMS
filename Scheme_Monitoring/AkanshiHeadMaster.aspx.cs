using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AkanshiHeadMaster : System.Web.UI.Page
{
    MasterDate objMaster = new MasterDate(); // Adjust this to the relevant object for Master Data

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
            get_tbl_FinancialYear(); // Populate Financial Year dropdown
            BtnUpdate.Visible = false;
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearFromId(19);
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }

    private void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown2(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                return;
            }

            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            decimal costPerHead = Convert.ToDecimal(txtCostPerHead.Text); // Assuming it's a decimal field

            DataTable dt = objMaster.SaveAkanshiHead(
                "Insert",
                Convert.ToInt32(ddlFY.SelectedValue),
                txtHeadName.Text,
                costPerHead,
                Person_Id
            );

            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(dt.Rows[0]["Remark"].ToString());
                if (dt.Rows[0]["Remark"].ToString() == "Record Saved.")
                {
                    ResetForm();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + " Please Enter valid data.");
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                return;
            }

            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            decimal costPerHead = Convert.ToDecimal(txtCostPerHead.Text);
            int AkanshiHeadId = Convert.ToInt32(lblHiddenId.Text.ToString());

            DataTable dt = objMaster.UpdateAkanshiHead(
                AkanshiHeadId,
                Convert.ToInt32(ddlFY.SelectedValue),
                txtHeadName.Text,
                costPerHead,
                Person_Id
            );

            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(dt.Rows[0]["Remark"].ToString());
                if (dt.Rows[0]["Remark"].ToString() == "Record Updated.")
                {
                    ResetForm();
                    GetAllData(); // Refresh the data
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + " Please Enter valid data.");
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        GetAllData();
    }

    private void GetAllData()
    {
        DataTable dt = objMaster.GetAkanshiHead(Convert.ToInt32(ddlFY.SelectedValue), txtHeadName.Text.Trim().ToString());
        grdPost.DataSource = dt;
        grdPost.DataBind();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());
        DataTable dt = objMaster.GetAkanshiHeadById(id);

        if (dt != null && dt.Rows.Count > 0)
        {
            BtnSave.Visible = false;
            BtnUpdate.Visible = true;
            ddlFY.SelectedValue = dt.Rows[0]["FYID"].ToString();
            txtHeadName.Text = dt.Rows[0]["AkanshiHead"].ToString();
            txtCostPerHead.Text = dt.Rows[0]["CostPerHead"].ToString();
            lblHiddenId.Text = dt.Rows[0]["AkanshiHeadId"].ToString();
        }
        else
        {
            MessageBox.Show("Record Not Found");
        }
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());
        DataTable dt = objMaster.DeleteAkanshiHead(id);

        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(dt.Rows[0]["Remark"].ToString());
            GetAllData();
        }
    }

    private bool ValidateFields()
    {
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial Year.");
            ddlFY.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtHeadName.Text))
        {
            MessageBox.Show("Please Enter Head Name.");
            txtHeadName.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtCostPerHead.Text) || !IsDecimal(txtCostPerHead.Text))
        {
            MessageBox.Show("Please Enter a valid Cost Per Head.");
            txtCostPerHead.Focus();
            return false;
        }
        return true;
    }

    private bool IsDecimal(string value)
    {
        decimal number;
        return decimal.TryParse(value, out number);
    }

    private void ResetForm()
    {
        ddlFY.SelectedValue = "0";
        txtHeadName.Text = "";
        txtCostPerHead.Text = "";
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        //hdnAkanshiHeadId.Value = "";
        lblHiddenId.Text = "";
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
}
