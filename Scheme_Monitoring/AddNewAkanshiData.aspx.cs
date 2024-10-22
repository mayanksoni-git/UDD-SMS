using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddNewAkanshiData : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
    List<tbl_ProjectWorkPkgTemp> obj_tbl_ProjectWorkPkg_Li = new List<tbl_ProjectWorkPkgTemp>();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (ViewState["dtQuestionnaire"] != null)
        {
            AddDynamicFields(); // Recreate GridView rows
        }
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
            //get_tbl_AkanshiHead();
            get_tbl_FinancialYear();
            get_tbl_Zone();

            if (Request.QueryString.Count > 0)
            {
            }
            else
            {
                Bind_EmptyFields();
            }
        }
        KeepDataOnChange();
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
    }

    //private void get_tbl_AkanshiHead()
    //{
    //    DataTable dt = new DataTable();
    //    dt = (new MasterDate()).GetAkanshiHead(-1, "");
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ViewState["dtAkanshiHead"] = dt;
    //    }
    //    else
    //    {
    //        ViewState["dtAkanshiHead"] = null;
    //    }
    //}

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearFromId(19);
        AllClasses.FillDropDown(ds.Tables[0], ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
            if (ddlZone.SelectedItem.Value != "0")
            {
                get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
            }
        }
        else
        {
            ddlZone.Items.Clear();
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
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division_akankshi(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }

    //private void get_tbl_AkanshiHead()
    //{
    //    DataSet ds = new DataSet();
    //    ds = (new DataLayer()).get_tbl_AkanshiHead();
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        AllClasses.FillDropDown(ds.Tables[0], ddlAkanshiHead, "AkanshiHead", "AkanshiHeadID");
    //    }
    //    else
    //    {
    //        ddlDivision.Items.Clear();
    //    }
    //}


    protected void btnSave_Click(object sender, EventArgs e)
    {
        
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
    protected void Load_Project_Details(int ProjectWork_Id)
    {
        
    }
   
    
    protected void grdAkanshiHead_PreRender(object sender, EventArgs e)
    {
        if (ViewState["dtNewAkankshiData"] != null)
        {
            grdAkanshiHead.DataSource = (DataTable)ViewState["dtNewAkankshiData"];
            grdAkanshiHead.DataBind();
        }
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
    protected void grdAkanshiHead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtQuestionnaire = (DataTable)ViewState["dtNewAkankshiData"];
            int rowIndex = e.Row.DataItemIndex;

            if (rowIndex < dtQuestionnaire.Rows.Count)
            {
                DataRow dr = dtQuestionnaire.Rows[rowIndex];

                // Populate the Month dropdown
                DropDownList ddlAkanshiHead = (DropDownList)e.Row.FindControl("ddlAkanshiHead");
                if (AllClasses.CheckDt(dtQuestionnaire))
                {
                    DataSet ds = new DataSet();
                    ds = (new DataLayer()).get_tbl_AkanshiHead();
                    AllClasses.FillDropDown(ds.Tables[0], ddlAkanshiHead, "AkanshiHead", "AkanshiHeadID");
                }
              
                // Retrieve the monthId from the DataRow, ensuring it's from the right column
                int akanshiHeadID;
                if (int.TryParse(dr["AkanshiHead"].ToString(), out akanshiHeadID) && akanshiHeadID > 0)
                {
                    // Set the selected value of the dropdown
                    ddlAkanshiHead.SelectedValue = akanshiHeadID.ToString();
                }
                if (AllClasses.CheckDt(dtQuestionnaire))
                {
                    DataSet ds = new DataSet();
                    ds = (new DataLayer()).get_CostPerHead(akanshiHeadID);

                    // Check if the DataSet contains tables and rows
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Get the first DataTable and the first DataRow
                        DataTable dt = ds.Tables[0];
                        DataRow dr1 = dt.Rows[0]; // Use the appropriate index for the row you need

                        // Find the TextBox in the row and set its Text property
                        TextBox txtCostPerHead = (TextBox)e.Row.FindControl("CostPerHead");
                        if (txtCostPerHead != null)
                        {
                            txtCostPerHead.Text = dr1["CostPerHead"].ToString();
                        }
                        else
                        {
                            txtCostPerHead.Text = "0";
                        }
                    }

                }
                //((TextBox)e.Row.FindControl("NoOfHead")).Text = dr["NoOfHead"].ToString();
                TextBox noOfHeadTextBox = (TextBox)e.Row.FindControl("NoOfHead");
                noOfHeadTextBox.Text = dr["NoOfHead"] != DBNull.Value ? dr["NoOfHead"].ToString() : "0";

                // Set other TextBox values
                decimal costPerHead = dr["CostPerHead"] != DBNull.Value ? Convert.ToDecimal(dr["CostPerHead"]) : 0;
                decimal noOfHead = dr["NoOfHead"] != DBNull.Value ? Convert.ToDecimal(dr["NoOfHead"]) : 0;

                ((TextBox)e.Row.FindControl("Amount")).Text = (costPerHead * noOfHead).ToString();
                UpdateTotalAmount();

                //ddlAkanshiHead.SelectedValue = DataBinder.Eval(e.Row.DataItem, "AkanshiHead").ToString();
            }
        }
    }
    private void Bind_EmptyFields()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Bind_EmptyFields_Akanshi();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtNewAkankshiData"] = ds.Tables[0];
            grdAkanshiHead.DataSource = ds.Tables[0];
            grdAkanshiHead.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                //dr["ProjectWorkGO_Id"] = 0;
                dr["newAkanshiDetail_Id"] = 0;
                dr["AkanshiHead"] = "";
                dr["CostPerHead"] = 0;
                dr["NoOfHead"] = 0;
                dr["Amount"] = 0;
               
                ds.Tables[0].Rows.Add(dr);

                ViewState["dtNewAkankshiData"] = ds.Tables[0];
                grdAkanshiHead.DataSource = ds.Tables[0];
                grdAkanshiHead.DataBind();
            }
            catch (Exception ex)
            {
                grdAkanshiHead.DataSource = null;
                grdAkanshiHead.DataBind();
            }
        }
    }
    protected void btnDeleteAkashiHead_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void btnQuestionnaire_Click(object sender, EventArgs e)
    {
        AddDynamicFields();
    }
    private void AddDynamicFields()
    {
        DataTable dtQuestionnaire;

        if (ViewState["dtNewAkankshiData"] != null)
        {
            dtQuestionnaire = (DataTable)ViewState["dtNewAkankshiData"];

            // Preserve existing data from GridView into DataTable
            foreach (GridViewRow row in grdAkanshiHead.Rows)
            {
                DataRow dr = dtQuestionnaire.Rows[row.RowIndex];

                // Extract values from TextBoxes and DropDownLists
                int ddlAkanshiHead;
                dr["AkanshiHead"] = int.TryParse(((DropDownList)row.FindControl("ddlAkanshiHead")).SelectedValue, out ddlAkanshiHead) ? ddlAkanshiHead: 0;
                //dr["MonthId"] = decimal.TryParse(((DropDownList)row.FindControl("MonthId")).SelectedValue, out monthId) ? monthId : 0;

                decimal latitude;
                dr["NoOfHead"] = decimal.TryParse(((TextBox)row.FindControl("NoOfHead")).Text.Trim(), out latitude) ? latitude : 0; 
                decimal CostPerHead;
                dr["CostPerHead"] = decimal.TryParse(((TextBox)row.FindControl("CostPerHead")).Text.Trim(), out CostPerHead) ? CostPerHead : 0;
                decimal longitude;
                dr["Amount"] = decimal.TryParse(((TextBox)row.FindControl("Amount")).Text.Trim(), out longitude) ? longitude : 0;



            }

            // Add a new empty row
            DataRow newRow = dtQuestionnaire.NewRow();
            dtQuestionnaire.Rows.Add(newRow);


            // Update ViewState and rebind GridView
            ViewState["dtNewAkankshiData"] = dtQuestionnaire;
            grdAkanshiHead.DataSource = dtQuestionnaire;
            grdAkanshiHead.DataBind();
        }
    }
    private void KeepDataOnChange()
    {
        DataTable dtQuestionnaire;

        if (ViewState["dtNewAkankshiData"] != null)
        {
            dtQuestionnaire = (DataTable)ViewState["dtNewAkankshiData"];

            // Preserve existing data from GridView into DataTable
            foreach (GridViewRow row in grdAkanshiHead.Rows)
            {
                DataRow dr = dtQuestionnaire.Rows[row.RowIndex];

                // Extract values from TextBoxes and DropDownLists
                int ddlAkanshiHead;
                dr["AkanshiHead"] = int.TryParse(((DropDownList)row.FindControl("ddlAkanshiHead")).SelectedValue, out ddlAkanshiHead) ? ddlAkanshiHead : 0;
                //dr["MonthId"] = decimal.TryParse(((DropDownList)row.FindControl("MonthId")).SelectedValue, out monthId) ? monthId : 0;

                decimal latitude;
                dr["NoOfHead"] = decimal.TryParse(((TextBox)row.FindControl("NoOfHead")).Text.Trim(), out latitude) ? latitude : 0;
                decimal CostPerHead;
                dr["CostPerHead"] = decimal.TryParse(((TextBox)row.FindControl("CostPerHead")).Text.Trim(), out CostPerHead) ? CostPerHead : 0;
                decimal longitude;
                dr["Amount"] = decimal.TryParse(((TextBox)row.FindControl("Amount")).Text.Trim(), out longitude) ? longitude : 0;



            }

            //// Add a new empty row
            //DataRow newRow = dtQuestionnaire.NewRow();
            //dtQuestionnaire.Rows.Add(newRow);


            // Update ViewState and rebind GridView
            ViewState["dtNewAkankshiData"] = dtQuestionnaire;
            grdAkanshiHead.DataSource = dtQuestionnaire;
            grdAkanshiHead.DataBind();
        }
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        
    }



    //protected void ddlAkanshiHead_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //}


    protected void ddlAkanshiHead_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlAkanshiHead = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlAkanshiHead.NamingContainer;

        int selectedValue = Convert.ToInt32(ddlAkanshiHead.SelectedValue);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_CostPerHead(selectedValue);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            DataRow dr1 = dt.Rows[0];

            TextBox txtCostPerHead = (TextBox)row.FindControl("CostPerHead");
            if (txtCostPerHead != null)
            {
                txtCostPerHead.Text = dr1["CostPerHead"].ToString();
            }
        }
    }

    protected void NoOfHead_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        TextBox txtNoOfHead = (TextBox)row.FindControl("NoOfHead");
        TextBox txtCostPerHead = (TextBox)row.FindControl("CostPerHead");
        TextBox txtAmount = (TextBox)row.FindControl("Amount");

        // Parse values, setting to 0 if null or not a number
        decimal noOfHead = 0;
        decimal costPerHead = 0;

        decimal.TryParse(txtNoOfHead.Text, out noOfHead);
        decimal.TryParse(txtCostPerHead.Text, out costPerHead);

        // Calculate Amount
        decimal amount = noOfHead * costPerHead;
        txtAmount.Text = amount.ToString("F2"); // Format as needed (e.g., 2 decimal places)

        // Update total amount in footer
        //UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
    {
        decimal totalAmount = 0;

        foreach (GridViewRow row in grdAkanshiHead.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAmount = (TextBox)row.FindControl("Amount");
                decimal amount = 0;

                if (txtAmount != null && !string.IsNullOrEmpty(txtAmount.Text))
                {
                    decimal.TryParse(txtAmount.Text, out amount);
                }

                totalAmount += amount;
            }
        }

        // Update the footer total amount
        if (grdAkanshiHead.FooterRow != null)
        {
            TextBox txtTotalAmount = (TextBox)grdAkanshiHead.FooterRow.FindControl("TotalAmount");
            if (txtTotalAmount != null)
            {
                txtTotalAmount.Text = totalAmount.ToString("F2"); // Format as needed
            }
        }
    }


}