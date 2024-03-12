
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_Salary_Register_Circle : System.Web.UI.Page
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
            txtYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
            if (Request.QueryString.Count > 0)
            {
                int Zone_Id = 0;
                string Month = "";
                string Year = "";
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Month = Request.QueryString["Month"].ToString();
                }
                catch
                {
                    Month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                }
                try
                {
                    Year = Request.QueryString["Year"].ToString();
                }
                catch
                {
                    Year = "2022";
                }
                txtYear.Text = Year;
                ddlMonth.SelectedValue = Month;
                int Circle_Id = 0;
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
                get_Salary_Register_Statement_Circle_Wise(Zone_Id, Circle_Id);
            }
        }
    }
        
    private void get_Salary_Register_Statement_Circle_Wise(int Zone_Id, int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Salary_Register_Statement_Circle_Wise(Zone_Id, Circle_Id, Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(txtYear.Text));
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds;
            grdPost.DataBind();

            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(Total_Employee)", "").ToString();
            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Basic)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(Grade_Pay)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(DA)", "").ToString();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(HRA)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(MA)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(Personal_Pay)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Special_Pay)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Other_All)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Gross_Sal)", "").ToString();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(Employer_NPS_cont)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(Employer_NPS_cont_arr)", "").ToString();
            grdPost.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(Total_Gross_Sal)", "").ToString();
            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(GPF)", "").ToString();
            grdPost.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(GPF_Adv)", "").ToString();
            grdPost.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(GIS)", "").ToString();
            grdPost.FooterRow.Cells[21].Text = ds.Tables[0].Compute("sum(Deduction_Total_HQ)", "").ToString();
            grdPost.FooterRow.Cells[22].Text = ds.Tables[0].Compute("sum(Income_Tax)", "").ToString();
            grdPost.FooterRow.Cells[23].Text = ds.Tables[0].Compute("sum(NPS_Employee)", "").ToString();
            grdPost.FooterRow.Cells[24].Text = ds.Tables[0].Compute("sum(NPS_Employee_Arr)", "").ToString();
            grdPost.FooterRow.Cells[25].Text = ds.Tables[0].Compute("sum(Deduction_Total_Paid)", "").ToString();
            grdPost.FooterRow.Cells[26].Text = ds.Tables[0].Compute("sum(HRA1)", "").ToString();
            grdPost.FooterRow.Cells[27].Text = ds.Tables[0].Compute("sum(Colony_Maintance)", "").ToString();
            grdPost.FooterRow.Cells[28].Text = ds.Tables[0].Compute("sum(Motor_Vehicle_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[29].Text = ds.Tables[0].Compute("sum(Other_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[30].Text = ds.Tables[0].Compute("sum(Deduction_Total_Not_Paid)", "").ToString();
            grdPost.FooterRow.Cells[31].Text = ds.Tables[0].Compute("sum(Total_Deduction)", "").ToString();
            grdPost.FooterRow.Cells[32].Text = ds.Tables[0].Compute("sum(Net_Salary)", "").ToString();
            grdPost.FooterRow.Cells[33].Text = ds.Tables[0].Compute("sum(Net_Salary_Employee)", "").ToString();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Month");
            ddlMonth.Focus();
            return;
        }
        if (txtYear.Text == "")
        {
            MessageBox.Show("Please Provide Year");
            txtYear.Focus();
            return;
        }
        if (Request.QueryString.Count > 0)
        {
            int Zone_Id = 0;
            string Month = "";
            string Year = "";
            try
            {
                Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Month = Request.QueryString["Month"].ToString();
            }
            catch
            {
                Month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            }
            try
            {
                Year = Request.QueryString["Year"].ToString();
            }
            catch
            {
                Year = "2022";
            }
            txtYear.Text = Year;
            ddlMonth.SelectedValue = Month;
            int Circle_Id = 0;
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
            {//Circle
                try
                {
                    Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
            }
            get_Salary_Register_Statement_Circle_Wise(Zone_Id, Circle_Id);
        }
    }


    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
        }
    }

    protected void lnkCircle_Click(object sender, EventArgs e)
    {
        LinkButton lnkCircle = sender as LinkButton;
        GridViewRow gr = lnkCircle.Parent.Parent as GridViewRow;
        int Zone_Id = 0;
        int Circle_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Circle_Id = 0;
        }
        Response.Redirect("Report_Salary_Register_Division.aspx?Zone_Id=" + Zone_Id.ToString() + "&Circle_Id="+ Circle_Id.ToString() + "&Month=" + ddlMonth.SelectedValue + "&Year=" + txtYear.Text);
    }

    protected void lnkCircleF_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            int Zone_Id = 0;
            try
            {
                Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
            Response.Redirect("Report_Salary_Register_Division.aspx?Zone_Id=" + Zone_Id.ToString() + "&Circle_Id=0&Month=" + ddlMonth.SelectedValue + "&Year=" + txtYear.Text);
        }
        else
        {
            Response.Redirect("Report_Salary_Register_Division.aspx?Zone_Id=0&Circle_Id=0&Month=" + ddlMonth.SelectedValue + "&Year=" + txtYear.Text);
        }
    }
}