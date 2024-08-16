using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

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
                //id = " + PlanID+" && Dist = "+Distid+" && ULBID = "+ULBID+" && FYID


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
       // sectionData.Visible = false;
        
            //GetEditExpenseList(ULBID.Value, FYID.Value);

            //AddSection.Visible = false;
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }
    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);

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

   
 
    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY1, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY2, "FinancialYear_Comments", "FinancialYear_Id");
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

    private void get_tbl_Circle(int zoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
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
    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            
            ddlDivision.Focus();
        }
        else
        {
            // GetAllData(Convert.ToInt32(ddlDivision.SelectedValue));
            //BindLoanReleaseGridByULB();
            var divi = "";
            if (ddlDivision.SelectedValue == "")
            {
                divi = "0";
            }
            else
            {
                divi = ddlDivision.SelectedValue;
            }
            if (divi != "" && ddlCircle.SelectedValue != "0" && ddlFY.SelectedValue != "0")
            {
                GetPopulationdata(divi, ddlFY.SelectedValue);
            }
        }
    }
    protected void GetCMVNYProject()
    {
        DataSet dt = new DataSet();
        dt = objLoan.GetAllCMVNYProject();
       FillDropDown(dt, DDLProj, "ProjectType_Name", "ProjectType_Id");
        
    }

    protected void GetEditList(string taskid)
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetVisionPlan("selectbyid", 0, 0, Convert.ToInt32(taskid), 0, "", 0, 0, "", "", "", 0, "", 0, "", "", "", "", "");

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

            var checkuser = dt.Rows[0]["IsUserCharger"].ToString();
                //---- Check That Condition Radion Button Checked or Not-----
                if (dt.Rows[0]["IsConstructed"].ToString()=="1")
              {
                sectionyear.Visible = true;
                sectionCond.Visible = true;
                secUser.Visible = true;

                sectionuOwner.Visible = true;


                RadioButton1.Checked = true;
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
            }
            else
                {
                    RadioButton3.Checked = true;
                sectionyear.Visible = false;
                sectionCond.Visible = false;
                secUser.Visible = false;
                sectionusercharge.Visible = false;
                //secOtherown.Visible = false;
                //sectionuOwner.Visible = false;
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
                TxtPopulation.Text = dt.Rows[0]["population"].ToString();
                TxtPopulation.Enabled = false;
            BtnSave.Visible = false;
            BtnUpdate.Visible = true;


            }
        else
        {
            // exportToExcel.Visible = false;
            MessageBox.Show("Record Not Found");

        }
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
       
        else
        {
            return true;
        }
    }



    protected void BtnSearch_Click(object sender, EventArgs e)
    {
      
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
        DdlPriority.SelectedValue = "1";
        BtnSave.Visible = true;
        BtnUpdate.Visible = false;
        TxtPopulation.Text = "";
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
       
    }

    protected void GetPopulationdata(string ulb,string fy)
    {
        DataTable dt = new DataTable();
        dt = objLoan.GetPopulation(ulb,fy);

        if (dt != null && dt.Rows.Count > 0)
        {
            TxtPopulation.Text = dt.Rows[0]["population"].ToString();
            TxtPopulation.Enabled = false;
        }
        else
        {
            TxtPopulation.Text = "";
            TxtPopulation.Enabled = true;
        }
    }

    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        var divi = "";
       if(ddlDivision.SelectedValue=="")
        {
            divi = "0";
        }
       else
        {
            divi = ddlDivision.SelectedValue;
        }
        if (divi != "" && ddlCircle.SelectedValue != "0" && ddlFY.SelectedValue != "0")
        {
            GetPopulationdata(divi, ddlFY.SelectedValue);
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
            var constructed = "";
            var constructedyear = "";
            var condition = "";
            var UserCharg = "";
            decimal Amount = 0;
            var owner = "";
            var IsOwnerShip = "";

            if (RadioButton1.Checked)
            {
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
            // var person=Convert.ToInt32(se)
            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            DataTable dt = new DataTable();
            dt = objLoan.GetVisionPlan("update", cmvny, ULB, pk, State, constructed, Dis, Fy, constructedyear, condition, UserCharg, Person_Id, IsOwnerShip,
                Amount, owner, DdlPriority.SelectedValue, SameProj, location, TxtPopulation.Text);
            //GetEditExpenseList(ddlZone.SelectedValue, ddlCircle.SelectedValue, ddlDivision.SelectedValue, ddlFY.SelectedValue);
            if (dt.Rows.Count > 0)
            {
               
                    BtnSave.Visible = true;
                    BtnUpdate.Visible = false;
                    ddlZone.Enabled = true;
                    ddlCircle.Enabled = true;
                    ddlDivision.Enabled = true;
                    ddlFY.Enabled = true;
                    VisionPlanID.Value = "";
                    TxtPopulation.Text = "";
                reset();
               
                MessageBox.Show(dt.Rows[0]["Remark"].ToString());
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                MessageBox.Show("All Fields required");
                return;
            }
            var constructed = "";
            var constructedyear = "";
            var condition = "";
            var UserCharg = "";
            decimal Amount = 0;
            var owner = "";
            var IsOwnerShip = "";

            if (RadioButton1.Checked)
            {
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
                }
                else
                {
                    MessageBox.Show("Please Select User Charge ");
                    RadioButton7.Focus();
                    return;
                }
                //---- Check That OwnerShip Radion Button Checked or Not-----
             
                constructedyear = TxtYear.Text;
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
                owner = OtherDepartment.Text;
            }
            else
            {
                MessageBox.Show("Please Select OwnerShip ");
                RadioButton9.Focus();
                return;
            }

            var cmvny = Convert.ToInt32(DDLProj.SelectedValue);
            var ULB = Convert.ToInt32(ddlDivision.SelectedValue);
            var State = Convert.ToInt32(ddlZone.SelectedValue);
            var Dis = Convert.ToInt32(ddlCircle.SelectedValue);
            var Fy = Convert.ToInt32(ddlFY.SelectedValue);
            var SameProj = similarProj.Text;
            var location = Location.Text;
            var prt = Convert.ToInt32(DdlPriority.SelectedValue);
            if(prt==0)
            {
                MessageBox.Show("Please select  priority");
                return;
            }
            // var person=Convert.ToInt32(se)
            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            DataTable dt = new DataTable();
            dt = objLoan.GetVisionPlan("insert", cmvny, ULB, 0, State, constructed, Dis, Fy, constructedyear, condition, UserCharg, Person_Id, IsOwnerShip,
                Amount, owner, DdlPriority.SelectedValue, SameProj, location, TxtPopulation.Text);
            //GetEditExpenseList(ddlZone.SelectedValue, ddlCircle.SelectedValue, ddlDivision.SelectedValue, ddlFY.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Remark"].ToString() == "Record Saved")
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

                sectionuOwner.Visible = true;
                RadioButton1.Checked = true;
                RadioButton2.Checked = false;
                RadioButton3.Checked = false;
            }
            else if(txt== "Under Construction "||txt== "Under Sanction ")
            {
                sectionyear.Visible = false;
                sectionCond.Visible = false;
                secUser.Visible = false;
                sectionusercharge.Visible = false;
                sectionuOwner.Visible = true;
                secOtherown.Visible = false;
                //RadioButton1.Checked = false;

                //RadioButton4.Checked = false;
                //RadioButton5.Checked = false;
                //RadioButton6.Checked = false;
                //RadioButton7.Checked = false;
                //RadioButton8.Checked = false;
                //RadioButton9.Checked = false;
                //RadioButton10.Checked = false;


             

                  //  sectionusercharge.Visible = true;

               
              

            }
            if (RadioButton7.Checked == true&&txt=="Yes")
            {

                sectionusercharge.Visible = true;
            }
            else
            {
                sectionusercharge.Visible = false;
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

    protected void ddlDivision_SelectedIndexChanged1(object sender, EventArgs e)
    {
        var divi = "";
        if (ddlDivision.SelectedValue == "")
        {
            divi = "0";
        }
        else
        {
            divi = ddlDivision.SelectedValue;
        }
        if (divi != ""  && ddlFY.SelectedValue != "0")
        {
            GetPopulationdata(divi, ddlFY.SelectedValue);
        }
       
    }
}