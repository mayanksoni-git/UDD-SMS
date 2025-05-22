using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Services;

public partial class CreateVisionPlan: System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    string ConStr = ConfigurationManager.AppSettings.Get("conn");
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string personId = Session["Person_Id"].ToString();
        string Designation = Session["PersonJuridiction_DesignationId"].ToString();
        //if (Designation == "1056" || personId == "3297" || personId == "2288"){}
        //else{Response.Redirect("VisionPlan.aspx");}
        if (!IsPostBack)
        {
            get_tbl_Zone();
            get_tbl_FinancialYear();
            RadioButton_CheckedChanged(sender, e);
            GetCMVNYProject();
            SetDropdownsBasedOnUserType();

            sectionyear.Visible = false;
            sectionCond.Visible = false;
            secUser.Visible = false;
            sectionuOwner.Visible = true;
            secOtherown.Visible = false;
            sectionusercharge.Visible = false;

            if (Request.QueryString.Count > 0)
            {
                ULBID.Value = Request.QueryString["ULBID"].ToString();
                FYID.Value = Request.QueryString["FYID"].ToString();
                VisionPlanID.Value = Request.QueryString["id"].ToString();
                GetEditList(Request.QueryString["id"].ToString());
            }
            if (Session["Person_Id"] == null || Session["Login_Id"] == null)
            {
                Response.Redirect("Index.aspx");
            }

            sectionFilter.Visible = true;
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                MessageBox.Show("All Fields required.");
                return;
            }

            double projectCost;
            string costText = txtProjectCost.Text.Trim();

            // Server-side validation
            if (string.IsNullOrEmpty(costText))
            {
                MessageBox.Show("Project Cost is required.");
                return;
            }
            if (!double.TryParse(costText, out projectCost) || projectCost <= 0 || projectCost > 10000.00)
            {
                MessageBox.Show("Project Cost must be a number between 0.01 and 10000.00 Lakhs.");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Project Cost must be a number between 0.01 and 10000.00.');", true);
                return;
            }
            if (Convert.ToDecimal(txtProjectCost.Text.Trim().ToString()) >= 100000)
            {
                MessageBox.Show("Please enter project cost in Lakhs not in Rupees.");
                return;
            }

            VisionPlan vp = new VisionPlan();
            int fyid = ddlFY.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlFY.SelectedValue);
            int IsQualified = vp.IsQualifiedForVisionPlan(Convert.ToInt32(ddlDivision.SelectedValue), fyid);
            if (IsQualified == 0)
            {
                MessageBox.Show("This ULB does not qualify to submit a Vision Plan under the CM-VNY Scheme for the financial year 2025–2026.");
                ddlDivision.Focus();
                return;
            }
            var constructed = "";
            var isHeritage = "";
            var constructedyear = "";
            var condition = "";
            var UserCharg = "";
            decimal Amount = 0;
            var owner = "";
            var IsOwnerShip = "";

            if (RadioButton1.Checked)
            {
                //---- Check That Condition Radion Button Checked or Not-----
                if (TxtYear.Text != "")
                {
                    constructedyear = TxtYear.Text;
                }
                else
                {
                    MessageBox.Show("Please Enter Constructed Year.");
                    TxtYear.Focus();
                    return;
                }

                if (RadioButton4.Checked)
                { condition = "1"; }
                else if (RadioButton5.Checked)
                { condition = "2"; }
                else if (RadioButton6.Checked)
                { condition = "3"; }
                else
                {
                    MessageBox.Show("Please Select Condition. ");
                    RadioButton4.Focus();
                    return;
                }
                //---- Check That User Charge Radion Button Checked or Not-----

                if (RadioButton7.Checked)
                {
                    UserCharg = "1";
                    Amount = Convert.ToDecimal(Amounts.Text);
                }
                else if (RadioButton8.Checked)
                {
                    UserCharg = "0";
                }
                else
                {
                    MessageBox.Show("Please Select User Charge. ");
                    RadioButton7.Focus();
                    return;
                }

                if (RadioButton11.Checked)
                    isHeritage = "1";
                else if (RadioButton12.Checked)
                    isHeritage = "0";
                else
                {
                    MessageBox.Show("Please Select Building is Heritage?");
                    RadioButton7.Focus();
                    return;
                }
                //---- Check That OwnerShip Radion Button Checked or Not-----

                constructed = "1";
            }
            else if (RadioButton2.Checked)
            {
                constructed = "2";
            }
            else if (RadioButton3.Checked)
            {
                constructed = "3";
            }
            else
            {
                MessageBox.Show("Please Select Construction ");
                RadioButton1.Focus();
                return;
            }


            if (RadioButton9.Checked)
            {
                IsOwnerShip = "1";
            }
            else if (RadioButton10.Checked)
            {
                IsOwnerShip = "0";
                if (OtherDepartment.Text == "")
                {
                    MessageBox.Show("Please Enter Other Department.");
                    OtherDepartment.Focus();
                    return;
                }
                owner = OtherDepartment.Text;
            }
            else
            {
                MessageBox.Show("Please Select OwnerShip.");
                RadioButton9.Focus();
                return;
            }
            if (similarProj.Text == "")
            {
                MessageBox.Show("Please Enter Number Of Similar project In City.");
                similarProj.Focus();
                return;
            }
            if (Location.Text == "")
            {
                MessageBox.Show("Please Enter Location.");
                Location.Focus();
                return;
            }
            //if (txtQuantity.Text == "")
            //{
            //    MessageBox.Show("Please Enter Quantity/Capacity.");
            //    Location.Focus();
            //    return;
            //}
            var cmvny = Convert.ToInt32(DDLProj.SelectedValue);
            var ULB = Convert.ToInt32(ddlDivision.SelectedValue);
            var State = Convert.ToInt32(ddlZone.SelectedValue);
            var Dis = Convert.ToInt32(ddlCircle.SelectedValue);
            var Fy = Convert.ToInt32(ddlFY.SelectedValue);

            var SameProj = similarProj.Text;
            var location = Location.Text;
            var prt = Convert.ToInt32(DdlPriority.SelectedValue);
            
            if (prt == 0)
            {
                MessageBox.Show("Please select  priority.");
                return;
            }

            

            int Quantity=0;

            //if (!string.IsNullOrEmpty(txtQuantity.Text))
            //{
            //    // Validate the input
            //    if (int.TryParse(txtQuantity.Text, out Quantity))
            //    {

            //    }
            //    else
            //    {
            //        MessageBox.Show("Please enter only number in the Quantity.");
            //        txtQuantity.Text = string.Empty; // Optionally clear the invalid input
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Quantity cannot be empty.");
            //    txtQuantity.Text = string.Empty; // Optionally clear the invalid input
            //    return;
            //}

            Decimal SiteArea=0;

            //if (!string.IsNullOrEmpty(txtSiteArea.Text))
            //{
            //    // Validate the input
            //    if (decimal.TryParse(txtSiteArea.Text, out SiteArea))
            //    {

            //    }
            //    else
            //    {
            //        MessageBox.Show("Please enter only decimal value in the Site Area.");
            //        txtSiteArea.Text = string.Empty; // Optionally clear the invalid input
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Site Area cannot be empty.");
            //    txtSiteArea.Text = string.Empty; // Optionally clear the invalid input
            //    return;
            //}


            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            DataTable dt = new DataTable();
            decimal approvedProjCost = 0;
            dt = objLoan.InsertVisionPlan("insert", cmvny, ULB, 0, State, constructed, Dis, Fy, constructedyear, condition, UserCharg, Person_Id, IsOwnerShip,
                Amount, owner, DdlPriority.SelectedValue, SameProj, location, "0", TxtProject.Text, Convert.ToDecimal(txtProjectCost.Text.Trim().ToString()), 0, -1, Quantity, Convert.ToDecimal(SiteArea), approvedProjCost, isHeritage);

            if (dt.Rows.Count > 0)
            {
                var check = dt.Rows[0]["Remark"].ToString();
                if (check == "Record Saved.")
                {
                    reset();
                }
                MessageBox.Show(dt.Rows[0]["Remark"].ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    protected void GetEditList(string taskid)
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetVisionPlan("selectbyid", 0, 0, Convert.ToInt32(taskid), 0, "", 0, 0, "", "", "", 0, "", 0, "", "", "", "", "","",0,0,-1,0);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlZone.SelectedValue = dt.Rows[0]["stateId"].ToString();
            ddlZone.Enabled = false;
            ddlCircle.Enabled = false;
            ddlDivision.Enabled = false;
            ddlFY.Enabled = false;
            ddlCircle.SelectedValue = dt.Rows[0]["distId"].ToString();
            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["stateId"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            ddlFY.SelectedValue = dt.Rows[0]["FYID"].ToString();
            DDLProj.SelectedValue = dt.Rows[0]["CMVNYId"].ToString();
            DdlPriority.SelectedValue = dt.Rows[0]["selfPriority"].ToString();
            TxtProject.Text=dt.Rows[0]["ProjectName"].ToString();
            var checkuser = dt.Rows[0]["IsUserCharger"].ToString();
            if (dt.Rows[0]["IsConstructed"].ToString()=="1")
            {
                sectionyear.Visible = true;
                sectionCond.Visible = true;
                secUser.Visible = true;
                sectionuOwner.Visible = true;
                RadioButton1.Checked = true;
                divIsHeritage.Visible = true;

                TxtYear.Text = dt.Rows[0]["constructedYear"].ToString();
                if (dt.Rows[0]["Conditionof"].ToString() == "1")
                {
                    RadioButton4.Checked = true;
                }
                if (dt.Rows[0]["Conditionof"].ToString() == "2")
                {
                    RadioButton5.Checked = true;
                }
                if (dt.Rows[0]["Conditionof"].ToString() == "3")
                {
                    RadioButton6.Checked = true;
                }
                if (dt.Rows[0]["IsUserCharger"].ToString() == "True")
                {
                    RadioButton7.Checked = true;
                    Amounts.Text = dt.Rows[0]["AmountOfUserCharge"].ToString();
                    sectionusercharge.Visible = true;
                }
                else
                {
                    RadioButton8.Checked = true;
                    sectionusercharge.Visible = false;
                }
                if (dt.Rows[0]["IsHeritage"].ToString() == "True")
                {
                    RadioButton11.Checked = true;
                }
                else
                {
                    RadioButton12.Checked = true;
                }

            }
            else if (dt.Rows[0]["IsConstructed"].ToString() == "2")
            {
                RadioButton2.Checked = true;
                sectionyear.Visible = false;
                sectionCond.Visible = false;
                secUser.Visible = false;
                sectionusercharge.Visible = false;
                secOtherown.Visible = false;
                sectionuOwner.Visible = true;
                divIsHeritage.Visible = false;



            }
            else
            {
                RadioButton3.Checked = true;
                sectionyear.Visible = false;
                sectionCond.Visible = false;
                secUser.Visible = false;
                sectionusercharge.Visible = false;
                divIsHeritage.Visible = false;
            }

            if (dt.Rows[0]["IsOwnerNagarNigamOrULB"].ToString() == "True")
            {
                RadioButton9.Checked = true;
                secOtherown.Visible = false;
            }
            else
            {
                RadioButton10.Checked = true;
                OtherDepartment.Text = dt.Rows[0]["OtherOwner"].ToString();
                secOtherown.Visible = true;
            }

            DDLProj.SelectedValue = dt.Rows[0]["CMVNYId"].ToString();
            ddlDivision.SelectedValue = dt.Rows[0]["ULBID"].ToString();
            ddlZone.SelectedValue = dt.Rows[0]["stateId"].ToString();
            ddlCircle.SelectedValue = dt.Rows[0]["distId"].ToString();
            ddlFY.SelectedValue = dt.Rows[0]["FYID"].ToString();
            similarProj.Text = dt.Rows[0]["NoOfSameProjInCity"].ToString();
            Location.Text = dt.Rows[0]["Loactions"].ToString();
            if(!String.IsNullOrWhiteSpace(dt.Rows[0]["ProjectCost"].ToString()))
            {
                txtProjectCost.Text = dt.Rows[0]["ProjectCost"].ToString();
            }
            if(!String.IsNullOrWhiteSpace(dt.Rows[0]["Quantity"].ToString()))
            {
                //txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                txtQuantity.Text = "0";
            }
            //if(!String.IsNullOrWhiteSpace(dt.Rows[0]["SiteArea"].ToString()))
            //{
            //    txtSiteArea.Text = dt.Rows[0]["SiteArea"].ToString();
            //}
            BtnSave.Visible = false;
            BtnUpdate.Visible = true;
        }
        else
        {
            // exportToExcel.Visible = false;
            MessageBox.Show("Record Not Found");
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                MessageBox.Show("All Fields required");
                return;
            }
            double projectCost;
            string costText = txtProjectCost.Text.Trim();

            // Server-side validation
            if (string.IsNullOrEmpty(costText))
            {
                MessageBox.Show("Project Cost is required.");
                return;
            }
            if (!double.TryParse(costText, out projectCost) || projectCost <= 0 || projectCost > 10000.00)
            {
                MessageBox.Show("Project Cost must be a number between 0.01 and 10000.00 Lakhs.");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Project Cost must be a number between 0.01 and 10000.00.');", true);
                return;
            }
            if (Convert.ToDecimal(txtProjectCost.Text.Trim().ToString()) >= 100000)
            {
                MessageBox.Show("Please enter project cost in Lakhs not in Rupees.");
                return;
            }
            var constructed = "";
            var isHeritage = "";
            var constructedyear = "";
            var condition = "";
            var UserCharg = "";
            decimal Amount = 0;
            var owner = "";
            var IsOwnerShip = "";

            if (RadioButton1.Checked)
            {
                if (TxtYear.Text != "")
                {
                    constructedyear = TxtYear.Text;
                }
                else
                {
                    MessageBox.Show("Please Enter Constructed Year.");
                    TxtYear.Focus();
                    return;
                }
                //---- Check That Condition Radion Button Checked or Not-----
                if (RadioButton4.Checked)
                { condition = "1"; }
                else if (RadioButton5.Checked)
                { condition = "2"; }
                else if (RadioButton6.Checked)
                { condition = "3"; }
                else
                {
                    MessageBox.Show("Please Select Condition ");
                    RadioButton4.Focus();
                    return;
                }
                //---- Check That User Charge Radion Button Checked or Not-----

                if (RadioButton7.Checked)
                {
                    UserCharg = "1";
                    Amount = Convert.ToDecimal(Amounts.Text);
                }
                else if (RadioButton8.Checked)
                {
                    UserCharg = "0";
                    Amount = Convert.ToDecimal(0.00);
                }
                else
                {
                    MessageBox.Show("Please Select User Charge ");
                    RadioButton7.Focus();
                    return;
                }
                //---- Check That OwnerShip Radion Button Checked or Not-----
               
                constructedyear = TxtYear.Text;
                if (RadioButton11.Checked)
                    isHeritage = "1";
                else if (RadioButton12.Checked)
                    isHeritage = "2";
                else
                {
                    MessageBox.Show("Please Select building Is Heritage ?");
                    RadioButton7.Focus();
                    return;
                }
                constructed = "1";
            }
            else if (RadioButton2.Checked)
            {
                constructed = "2";
            }
            else if (RadioButton3.Checked)
            {
                constructed = "3";
            }
            else
            {
                MessageBox.Show("Please Select Construction ");
                RadioButton1.Focus();
                return;
            }

            if (RadioButton9.Checked)
            {
                IsOwnerShip = "1";
                owner = "";
            }
            else if (RadioButton10.Checked)
            {
                IsOwnerShip = "0";
                owner = OtherDepartment.Text;
            }
            else
            {
                MessageBox.Show("Please Select OwnerShip ");
                RadioButton9.Focus();
                return;
            }
            if (similarProj.Text == "")
            {
                MessageBox.Show("Please Enter Number Of Similar project In City.");
                similarProj.Focus();
                return;
            }
            if (Location.Text == "")
            {
                MessageBox.Show("Please Enter Location.");
                Location.Focus();
                return;
            }

            //if (txtQuantity.Text == "")
            //{
            //    MessageBox.Show("Please Enter Quantity/Capacity.");
            //    Location.Focus();
            //    return;
            //}
            var pk = Convert.ToInt32(VisionPlanID.Value);
            var cmvny = Convert.ToInt32(DDLProj.SelectedValue);
            var ULB = Convert.ToInt32(ddlDivision.SelectedValue);
            var State = Convert.ToInt32(ddlZone.SelectedValue);
            var Dis = Convert.ToInt32(ddlCircle.SelectedValue);
            var Fy = Convert.ToInt32(ddlFY.SelectedValue);
            var SameProj = similarProj.Text;
            var location = Location.Text;
            var prt = Convert.ToInt32(DdlPriority.SelectedValue);
            if (prt == 0)
            {
                MessageBox.Show("Please select  priority");
                return;
            }
            

            var SiteArea = 0.00;
            var Quantity = 0;
            //if (!string.IsNullOrEmpty(txtQuantity.Text))
            //{
            //    Quantity = Convert.ToInt16(txtQuantity.Text.ToString());
            //}

            //if (!string.IsNullOrEmpty(txtSiteArea.Text))
            //{
            //    SiteArea = Convert.ToDouble(txtSiteArea.Text.ToString());
            //}

            // var person=Convert.ToInt32(se)
            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            DataTable dt = new DataTable();
            //decimal approvedProjCost = 0;
            dt = objLoan.InsertVisionPlan("update", cmvny, ULB, pk, State, constructed, Dis, Fy, constructedyear, condition, UserCharg, Person_Id, IsOwnerShip,
                Amount, owner, DdlPriority.SelectedValue, SameProj, location, "0",TxtProject.Text, Convert.ToDecimal(txtProjectCost.Text.Trim().ToString()),0,-1, Quantity, Convert.ToDecimal(SiteArea),Convert.ToInt32(txtApprovedProjCost.Text.Trim()), isHeritage);
            
            if (dt.Rows.Count > 0)
            {
                BtnSave.Visible = true;
                BtnUpdate.Visible = false;
                ddlZone.Enabled = true;
                ddlCircle.Enabled = true;
                ddlDivision.Enabled = true;
                ddlFY.Enabled = true;
                VisionPlanID.Value = "";
                
                reset();
               
                MessageBox.Show(dt.Rows[0]["Remark"].ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    #region Helper Methods
    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton radioButton = sender as RadioButton;
        if (radioButton != null)
        {
            var txt = radioButton.Text.ToString();

            if (txt == "Constructed ")
            {
                sectionyear.Visible = true;

                sectionCond.Visible = true;
                secUser.Visible = true;
                sectionusercharge.Visible = true;
                divIsHeritage.Visible = true;

                sectionuOwner.Visible = true;
                RadioButton1.Checked = true;
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
            }
            else if (txt == "Under Construction " || txt == "Under Sanction ")
            {
                RadioButton11.Checked = false;
                RadioButton12.Checked = true;
                RadioButton8.Checked = true;
                RadioButton7.Checked = false;
                RadioButton4.Checked = false;
                RadioButton5.Checked = false;
                RadioButton6.Checked = false;
                Amounts.Text = "";
                TxtYear.Text = "";
                sectionyear.Visible = false;
                sectionCond.Visible = false;
                secUser.Visible = false;
                sectionusercharge.Visible = false;
                divIsHeritage.Visible = false;
                sectionuOwner.Visible = true;
                secOtherown.Visible = false;
            }
            if (RadioButton7.Checked == true)
            {
                sectionusercharge.Visible = true;
            }
            else
            {
                sectionusercharge.Visible = false;
                Amounts.Text = "";
            }
            if (RadioButton10.Checked == true)
            {
                secOtherown.Visible = true;
            }
            else
            {
                secOtherown.Visible = false;
            }
        }
    }

    [WebMethod]
    public static string DDLProj_SelectedIndexChanged(int id)
    {
        int selectedValue = id;
        string scalarValue = HttpUtility.UrlDecode((new DataLayer()).GetScalarValueFromDatabase(selectedValue));

        // Serialize as JSON using JavaScriptSerializer
        var result = new { scalarValue = scalarValue };
        return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(result);
    }

    private void reset()
    {
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
        try
        {
            ddlDivision.SelectedValue = "0";
        }
        catch
        {
            ddlDivision.SelectedValue = "0";
        }
        DDLProj.SelectedValue = "0";
        ddlFY.SelectedValue = "0";
        DdlPriority.SelectedValue = "0";
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        
        Location.Text = "";
        similarProj.Text = "";
        
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;
        RadioButton4.Checked = false;
        RadioButton5.Checked = false;
        RadioButton6.Checked = false;
        RadioButton7.Checked = false;
        RadioButton8.Checked = false;
        RadioButton9.Checked = false;
        RadioButton10.Checked = false;
        SetDropdownsBasedOnUserType();
    }

    public bool ValidateFields()
    {
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
            return false;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            return false;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            return false;
        }
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial year. ");
            ddlFY.Focus();
            return false;
        }
        if (TxtProject.Text == "" || TxtProject.Text == null)
        {
            MessageBox.Show("Please Enter Project Name. ");
            TxtProject.Focus();
            return false;
        }
        if (txtProjectCost.Text == "" || txtProjectCost.Text == null)
        {
            MessageBox.Show("Please Enter Project Cost. ");
            txtProjectCost.Focus();
            return false;
        }
        //if (txtQuantity.Text == "" || txtQuantity.Text == null)
        //{
        //    MessageBox.Show("Please Enter Quantity/Capacity. ");
        //    txtQuantity.Focus();
        //    return false;
        //}
        else
        {
            return true;
        }
    }
    #endregion

    #region DropDowns
    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }
    private void get_tbl_FinancialYearold()
    {
        DataSet ds = (new ULBFund()).getFYDetail();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }
    private void get_tbl_FinancialYear()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FinancialYear();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // Filter only the record with FinancialYear_Id = 21
            DataRow[] rows = ds.Tables[0].Select("FinancialYear_Id = 21");

            if (rows.Length > 0)
            {
                DataTable filteredTable = rows.CopyToDataTable();
                ddlFY.DataSource = filteredTable;
                ddlFY.DataTextField = "FinancialYear_Comments";
                ddlFY.DataValueField = "FinancialYear_Id";
                ddlFY.DataBind();

                // Optionally add a default selection item at the top
                ddlFY.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));

                // Set the default selection to 21
                ddlFY.SelectedValue = "21";
            }
            else
            {
                // No matching FY found; clear and show only default option
                ddlFY.Items.Clear();
                ddlFY.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));
            }
        }
    }
    private void get_tbl_Circle(int zoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    protected void GetCMVNYProject()
    {
        DataSet dt = new DataSet();
        dt = objLoan.GetAllCMVNYProject();
        FillDropDown(dt, DDLProj, "ProjectType_Name", "ProjectType_Id");
    }
    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer3()).get_tbl_DivisionForVisionPlan(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
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
            var divi = "";
            if (ddlDivision.SelectedValue == "")
            {
                divi = "0";
            }
            else
            {
                divi = ddlDivision.SelectedValue;
            }

        }
    }
    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);
        int fyid = ddlFY.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlFY.SelectedValue);

        if (userType == 4 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
        }
        else if (userType == 6 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
            }
        }
        else if (userType == 7 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
                if (divisionId > 0)
                {
                    SetDropdownValueAndDisable(ddlDivision, divisionId);
                }
            }
        }
    }
    private void SetDropdownValueAndDisable(DropDownList ddl, int value)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;
            if (ddl.ID.ToString() == "ddlZone")
            {
                ddlZone_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
            else if (ddl.ID.ToString() == "ddlCircle")
            {
                ddlCircle_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
        }
        catch
        {
            // Handle exception if needed
        }
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
    #endregion
}