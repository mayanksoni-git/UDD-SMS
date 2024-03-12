using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GPFPassbookEntry : System.Web.UI.Page
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
            get_tbl_Basic_GPF_Rate();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (Request.QueryString.Count > 0)
            {
                int Person_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                hf_Person_Id.Value = Person_Id.ToString();
                DataSet ds = new DataSet();
                ds = new DataLayer().get_tbl_HRMSEmployeeEdit(Person_Id);
                if (AllClasses.CheckDataSet(ds))
                {
                    //txtEmpName.Text = ds.Tables[0].Rows[0]["HRMSEmployee_Name"].ToString();
                    //txtDOJ.Text = ds.Tables[0].Rows[0]["HRMSEmployee_JoinDateInService"].ToString();
                    //txtEmpCode.Text = ds.Tables[0].Rows[0]["HRMSEmployee_DepartmentalEmployeeCode"].ToString();

                }
                get_tbl_HRMSEmployeeEdit(Person_Id);
                get_tbl_GPF_Income(Person_Id);
                Get_Year();
                get_tbl_GPFPassbookDetails(Person_Id);
            }
        }
      
    }
    protected void grdGPFDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int GPFDetails_BasicGPFRate = 0;
            try
            {
                GPFDetails_BasicGPFRate = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                GPFDetails_BasicGPFRate = 0;
            }
            DropDownList ddlRate = e.Row.FindControl("ddlRate") as DropDownList;
            DataTable dt = new DataTable();

            if (ViewState["Basic_GPF_Rate"] != null)
            {
                dt = (DataTable)ViewState["Basic_GPF_Rate"];
                if (AllClasses.CheckDt(dt))
                {
                    AllClasses.FillDropDown_WithOutSelect(dt, ddlRate, "Basic_GPF_Rate", "Basic_GPF_Rate_Id");

                    if (GPFDetails_BasicGPFRate > 0)
                    {
                        try
                        {
                            ddlRate.SelectedValue = GPFDetails_BasicGPFRate.ToString();
                        }
                        catch
                        {
                            ddlRate.SelectedValue = "0";
                        }
                    }
                }
            }
            int Open_Balance = 0;
            try
            {
                Open_Balance = Convert.ToInt32(grdIncome.Rows[0].Cells[2].Text.Trim());
            }
            catch
            {
                Open_Balance = 0;
            }

            for (int i = 0; i < grdGPFDetails.Rows.Count; i++)
            {
                int Subscription = 0;
                try
                {
                    Subscription = Convert.ToInt32((grdGPFDetails.Rows[i].FindControl("txtSubscription") as TextBox).Text);
                }
                catch
                {
                    Subscription = 0;
                }
                int Refund = 0;
                try
                {
                    Refund = Convert.ToInt32((grdGPFDetails.Rows[i].FindControl("txtRefund") as TextBox).Text);
                }
                catch
                {
                    Refund = 0;
                }
                int Withdrawal = 0;
                try
                {
                    Withdrawal = Convert.ToInt32((grdGPFDetails.Rows[i].FindControl("txtWithdrawal") as TextBox).Text);
                }
                catch
                {
                    Withdrawal = 0;
                }
                int MonthlyBalanceOnIntrest = Subscription + Refund - Withdrawal;

                try
                {
                    (grdGPFDetails.Rows[i].FindControl("txtCalculation") as TextBox).Text = Convert.ToString(MonthlyBalanceOnIntrest);
                }
                catch
                {
                    (grdGPFDetails.Rows[i].FindControl("txtCalculation") as TextBox).Text = null;
                }
            }
        }

    }
    private void get_tbl_GPFPassbookDetails(int Person_Id)
    {
        int Year = 0;
        try
        {
             Year = Convert.ToInt32(ddlYear.SelectedValue);
        }
        catch
        {
            Year = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_GPFPassbookDetails(Person_Id,Year);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdGPFDetails.DataSource = ds.Tables[0];
            grdGPFDetails.DataBind();
        }
        else
        {
            if (ds != null)
            {
                DataRow dr;
                for (int i = 0; i <= 11; i++)
                {
                    dr = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(dr);
                }
                grdGPFDetails.DataSource = ds.Tables[0];
                grdGPFDetails.DataBind();
            }
            else
            {
                grdGPFDetails.DataSource = null;
                grdGPFDetails.DataBind();
            }
        }
    }
    protected void grdGPFDetails_PreRender(object sender, EventArgs e)
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //int Zone_Id = 0;
        //int Circle_Id = 0;
        //int Division_Id = 0;

        //try
        //{
        //    Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        //}
        //catch
        //{
        //    Zone_Id = 0;
        //}
        //try
        //{
        //    Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        //}
        //catch
        //{
        //    Circle_Id = 0;
        //}
        //try
        //{
        //    Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        //}
        //catch
        //{
        //    Division_Id = 0;
        //}
        //get_tbl_GPFPassbookDetails(Zone_Id, Circle_Id, Division_Id);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        int _HRMSEmployee_Id = Convert.ToInt32(hf_Person_Id.Value);
        int Person_Id= Convert.ToInt32(Session["Person_Id"].ToString());
        int Year= Convert.ToInt32(ddlYear.SelectedValue);
        List<tbl_GPFDetails> obj_tbl_GPFDetails_Li = new List<tbl_GPFDetails>();
        for (int i = 0; i < grdGPFDetails.Rows.Count; i++)
        {
            tbl_GPFDetails obj_tbl_GPFDetails = new tbl_GPFDetails();
            obj_tbl_GPFDetails.GPFDetails_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            string MOnth_Id = (grdGPFDetails.Rows[i].Cells[0].Text.Trim().Replace("&nbsp;", ""));
            obj_tbl_GPFDetails.GPFDetails_MonthId = Convert.ToInt32(MOnth_Id);
            obj_tbl_GPFDetails.GPFDetails_Subscription = (grdGPFDetails.Rows[i].FindControl("txtSubscription") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_VoucherNo = (grdGPFDetails.Rows[i].FindControl("txtVoucherNo") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_VoucherDate = (grdGPFDetails.Rows[i].FindControl("txtVoucherDate") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_Refund = (grdGPFDetails.Rows[i].FindControl("txtRefund") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_Total = (grdGPFDetails.Rows[i].FindControl("txtTotal") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_Withdrawal = (grdGPFDetails.Rows[i].FindControl("txtWithdrawal") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_Calculation = (grdGPFDetails.Rows[i].FindControl("txtCalculation") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_Remarks = (grdGPFDetails.Rows[i].FindControl("txtRemarks") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_TotalSubscription = (grdGPFDetails.Rows[i].FindControl("txtTotalSubscription") as TextBox).Text.Trim();
            obj_tbl_GPFDetails.GPFDetails_BasicGPFRate = (grdGPFDetails.Rows[i].FindControl("ddlRate") as DropDownList).SelectedValue;
            //obj_tbl_GPFDetails.GPFDetails_Year = (grdGPFDetails.Rows[i].FindControl("ddlYear") as DropDownList).SelectedValue;
            obj_tbl_GPFDetails.GPFDetails_Year =Convert.ToInt32(ddlYear.SelectedValue);
            obj_tbl_GPFDetails.GPFDetails_HRMS_Employee_Id = _HRMSEmployee_Id;
            obj_tbl_GPFDetails.GPFDetails_Status = 1;
            obj_tbl_GPFDetails_Li.Add(obj_tbl_GPFDetails);
        }
        if ((new DataLayer()).Insert_tbl_GPFDetails(obj_tbl_GPFDetails_Li, ref Msg, _HRMSEmployee_Id,Person_Id,Year))
        {
            MessageBox.Show("Passbook Details Created Successfully!");
            Reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Add Passbook Details");
            return;
        }

    }  
    protected void get_tbl_HRMSEmployeeEdit(int HRMSEmployee_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_HRMSEmployeeEdit(HRMSEmployee_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            grdView.DataSource = ds.Tables[0];
            grdView.DataBind();
        }
        else
        {
            grdView.DataSource = null;
            grdView.DataBind();
        }
    }
    protected void grdView_PreRender(object sender, EventArgs e)
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
    
    protected void grdIncome_PreRender(object sender, EventArgs e)
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
    private void get_tbl_Basic_GPF_Rate()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Basic_GPF_Rate();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["Basic_GPF_Rate_Id"] = 0;
            dr["Basic_GPF_Rate"] = 0;
            ds.Tables[0].Rows.InsertAt(dr, 0);
            ViewState["Basic_GPF_Rate"] = ds.Tables[0];
        }
        else
        {
            ViewState["Basic_GPF_Rate"] = null;
        }
    }
    protected void txtSaveIncome_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_GPF_Income obj_GPF_Income = new tbl_GPF_Income();
        int Person_Id= Convert.ToInt32(Session["Person_Id"].ToString());
        int HRMSEmployee_Id = Convert.ToInt32(hf_Person_Id.Value);
        if (hf_GPF_Income_Id.Value == "0" || hf_GPF_Income_Id.Value == "")
        {
            obj_GPF_Income.GPF_Income_Id = 0;
        }
        else
        {
            try
            {
                obj_GPF_Income.GPF_Income_Id = Convert.ToInt32(hf_GPF_Income_Id.Value);
            }
            catch
            {
                obj_GPF_Income.GPF_Income_Id = 0;
            }
        }
        obj_GPF_Income.GPF_Income_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_GPF_Income.GPF_Income_Month = ddlMonth.SelectedValue;
            obj_GPF_Income.GPF_Income_Year = txtYear.Text.Trim();
            obj_GPF_Income.GPF_Income_DepositAmount = Convert.ToInt32(txtDeposit.Text.Trim()).ToString();
            obj_GPF_Income.GPF_Income_Interest = Convert.ToInt32(txtInterest.Text.Trim()).ToString();
            obj_GPF_Income.GPF_Income_Withdrawal = Convert.ToInt32(txtWidhdrawal.Text.Trim()).ToString();
            obj_GPF_Income.GPF_Income_Refund = Convert.ToInt32(txtRefund.Text.Trim()).ToString();
            obj_GPF_Income.GPF_Income_Total = Convert.ToInt32(txtTotal.Text.Trim()).ToString();
        }
        catch
        {
            obj_GPF_Income.GPF_Income_Month = "";
            obj_GPF_Income.GPF_Income_Year = "";
            obj_GPF_Income.GPF_Income_DepositAmount = "";
            obj_GPF_Income.GPF_Income_Interest = "";
            obj_GPF_Income.GPF_Income_Withdrawal= "";
            obj_GPF_Income.GPF_Income_Refund = "";
            obj_GPF_Income.GPF_Income_Total = "";
        }
        obj_GPF_Income.GPF_Income_Status = 1;
        obj_GPF_Income.GPF_Income_HRMSEmployee_Id = HRMSEmployee_Id;
        if (new DataLayer().Insert_tbl_GPF_Income(obj_GPF_Income, obj_GPF_Income.GPF_Income_Id,HRMSEmployee_Id,Person_Id, ref Msg))
        {
            MessageBox.Show("GPF Income Created Successfully ! ");
            Reset();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This GPF Income Already Exist. Give another! ");
            }
            else
            {
                MessageBox.Show("Error ! ");
            }
            return;
        }
    }
    private void Reset()
    {
       ddlMonth.SelectedValue = "0";
        txtYear.Text = "";
        txtDeposit.Text = "";
        txtInterest.Text = "";
        txtWidhdrawal.Text = "";
        txtRefund.Text = "";
        txtTotal.Text = "";
        divCreateNew.Visible = false;
    }
    protected void get_tbl_GPF_Income(int GPF_Income_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_GPF_Income(GPF_Income_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {

            grdIncome.DataSource = ds.Tables[0];
            grdIncome.DataBind();
        }
        else
        {
            grdIncome.DataSource = null;
            grdIncome.DataBind();
        }
    }
    public void calculate()
    {
       
    }
    public void Get_Year()
    {
        int OpningBalanceYear = 0;
        try
        {
            OpningBalanceYear = Convert.ToInt32(grdIncome.Rows[0].Cells[1].Text.Trim());
        }
        catch
        {
            OpningBalanceYear = 0;
        }
        if (OpningBalanceYear!=0)
        {
            List<int> obj_Year = new List<int>();
            for (int i = OpningBalanceYear; i <= DateTime.Now.Year; i++)
            {
                obj_Year.Add(i);
            }
            ddlYear.DataSource = obj_Year;
            ddlYear.DataBind();
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Reset();
        divCreateNew.Visible = true;    
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Year = Convert.ToInt32(ddlYear.SelectedValue);
        int Person_Id = Convert.ToInt32(hf_Person_Id.Value);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_GPFPassbookDetails(Person_Id, Year);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdGPFDetails.DataSource = ds.Tables[0];
            grdGPFDetails.DataBind();
        }
        else
        {
            grdGPFDetails.DataSource =null;
            grdGPFDetails.DataBind();
        }
    }
    
}
