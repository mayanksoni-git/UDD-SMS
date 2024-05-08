using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PyersTracker : System.Web.UI.Page
{
    Pyres objPyres = new Pyres();

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

            lblZoneH.Text = Session["Default_Zone"].ToString() + "*";
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            lblDivisionH.Text = Session["Default_Division"].ToString() + "*";

            get_tbl_Zone();
            get_tbl_Month();
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    string value = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }


    //Dropdowns-------------------------------------------------------------------------------------
    private void get_tbl_Month()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Month();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlMonth, "Month_MonthName", "Month_Id");
        }
        else
        {
            ddlZone.Items.Clear();
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


    //Calculations---------------------------------------------------------------------------------
    protected void txtUrbanPopulation_TextChanged(object sender, EventArgs e)
    {
        double population = 0.00;
        if (txtUrbanPopulation.Text != "")
        {
            population = Int32.Parse(txtUrbanPopulation.Text) * .8;
        }
        
        txtPopulationCreamtion80.Text = population.ToString();
    }
    protected void txtDeathPer1000_TextChanged(object sender, EventArgs e)
    {
        double D13 = 0.00;
        double E13 = 0.00;
        if (txtPopulationCreamtion80.Text != "")
        {
            D13 = double.Parse(txtPopulationCreamtion80.Text);
        }
        if (txtDeathPer1000.Text != "")
        {
            E13 = double.Parse(txtDeathPer1000.Text);
        }

        // Calculation of EstimatedDeath10Buffer
        double EstDeath10Buffer = Math.Round(1.1 * ((D13 / 1000) * E13) / 365, 0);

        txtEstDeath10Buffer.Text = EstDeath10Buffer.ToString();
    }
    protected void ExistingPyersHandlingCapacity(object sender, EventArgs e)
    {
        //Calculation for Existing Capacity--------------------------------------------------------------------
        int Conventional = 0;
        int ImprovisedWood = 0;
        int Gas = 0;
        int Electric = 0;

        int C2 = 1;
        int C3 = 2;
        int C4 = 4;
        int C5 = 4;

        if (txtConventional.Text!="")
        {
            Conventional = Int32.Parse(txtConventional.Text);
        }
        if(txtImprovisedWood.Text!="")
        {
            ImprovisedWood = Int32.Parse(txtImprovisedWood.Text);
        }
        if (txtGas.Text != "")
        {
            Gas = Int32.Parse(txtGas.Text);
        }
        if (txtElectric.Text != "")
        {
            Electric = Int32.Parse(txtElectric.Text);
        }
        // Perform the calculations
        int ExistingCapacity = Conventional * C2 + ImprovisedWood * C3 + Gas * C4 + Electric * C5;
        txtExistCapacity.Text = ExistingCapacity.ToString();

        // Calculation for Upgrade Decision------------------------------------------------------------------------------------

        
        int F13 = 0;
        int K13 = ExistingCapacity;
        int G13 = Conventional;
        int M13 = 0;

        if (txtEstDeath10Buffer.Text != "")
        {
            F13 = Int32.Parse(txtEstDeath10Buffer.Text);
        }

        //Calculation for Ramaining to be handled
        int RemainingToBeHandled = F13 - (ImprovisedWood * C3 + Gas * C4 + Electric * C5);
        M13 = RemainingToBeHandled;
        string UpgradeDecision = "";

        if (F13 > K13 && G13 > 0)
        {
            UpgradeDecision = "Build New + Upgrade Existing";
        }
        else if (F13 <= K13 && G13 > 0 && M13>0)
        {
            UpgradeDecision = "Upgrade Existing";
        }
        else
        {
            UpgradeDecision = "Upgradation not required";
        }
        txtUpgradeExisting.Text = UpgradeDecision;
        
        txtRemainingToBeHandled.Text = RemainingToBeHandled.ToString();
        if(RemainingToBeHandled>0)
        {
            divUpgradation.Visible = true;
            txtUpgradeImprovisedWood.Text = "";
            txtUpgradeGas.Text = "";
            txtUpgradeElectric.Text = "";
        }
        else
        {
            divUpgradation.Visible = false;
            txtUpgradeImprovisedWood.Text = "0";
            txtUpgradeGas.Text = "0";
            txtUpgradeElectric.Text = "0";
            txtPyresToBeRevamped.Text = "0";
        }
    }
    protected void CalculateCheckOn(object sender, EventArgs e)
    {
        // Sample values
        int N13 = 0;
        int O13 = 0;
        int P13 = 0;
        int G13 = 0;

        if (txtUpgradeImprovisedWood.Text != "")
        {
            N13 = Int32.Parse(txtUpgradeImprovisedWood.Text);
        }
        if (txtUpgradeGas.Text != "")
        {
            O13 = Int32.Parse(txtUpgradeGas.Text);
        }
        if (txtUpgradeElectric.Text != "")
        {
            P13 = Int32.Parse(txtUpgradeElectric.Text);
        }
        if (txtConventional.Text != "")
        {
            G13 = Int32.Parse(txtConventional.Text);
        }

        // Perform the calculation
        int sumNtoP = N13 + O13 + P13;
        string result = sumNtoP > G13 ? "Exceeded total upgradation possible" : "OK";
        txtPyresToBeRevamped.Text = sumNtoP.ToString();
        //txtCheckOn.Text = result;
    }
    protected void GetCommentOnCapacity(object sender, EventArgs e)
    {
        double U13 = 0;
        if (txtRemainingCapacity.Text != "")
        {
            U13 = double.Parse(txtRemainingCapacity.Text);
        }

        // Perform the calculation
        double roundU13 = Math.Round(U13, 0);
        string CommentOnCapacity = "";

        if (roundU13 > 0)
        {
            CommentOnCapacity = "More EFP required";
        }
        else if (roundU13 == 0)
        {
            CommentOnCapacity = "EFP requirement met";
        }
        else
        {
            CommentOnCapacity = "Exceeded EFP requirement";
        }
        txtCommentOnCapacity.Text = CommentOnCapacity;
    }


    //Click Events --------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Year. ");
            ddlYear.Focus();
            return;
        }
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Month. ");
            ddlMonth.Focus();
            return;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
            return;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            return;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            return;
        }

        if (txtUrbanPopulation.Text=="")
        {
            MessageBox.Show("Please enter Total urban population. ");
            txtUrbanPopulation.Focus();
            return;
        }

        if (txtDeathPer1000.Text=="")
        {
            MessageBox.Show("Please enter Death rate per 1000 per year. ");
            txtDeathPer1000.Focus();
            return;
        }

        if (txtConventional.Text == "")
        {
            MessageBox.Show("Please enter existing conventional pyres. ");
            txtConventional.Focus();
            return;
        }

        if (txtImprovisedWood.Text == "")
        {
            MessageBox.Show("Please enter existing Improvised Wood pyres. ");
            txtImprovisedWood.Focus();
            return;
        }

        if (txtGas.Text == "")
        {
            MessageBox.Show("Please enter existing Gas pyres. ");
            txtGas.Focus();
            return;
        }

        if (txtElectric.Text == "")
        {
            MessageBox.Show("Please enter existing Electric pyres. ");
            txtElectric.Focus();
            return;
        }

        if (txtRemainingCapacity.Text == "")
        {
            MessageBox.Show("Please enter Remaining capacity (ideally should be negative) field. ");
            txtRemainingCapacity.Focus();
            return;
        }

        if (txtFundsRequired.Text == "")
        {
            MessageBox.Show("Please enter Funds required in lacs (only includes pyres and not other facilities) field. ");
            txtFundsRequired.Focus();
            return;
        }
        tbl_PyresTracker obj_PyresTracker = new tbl_PyresTracker();

        obj_PyresTracker.AddedBy = Int32.Parse(Session["Person_Id"].ToString());
        obj_PyresTracker.Year = Int32.Parse(ddlYear.SelectedValue.ToString());
        obj_PyresTracker.Month = Int32.Parse(ddlMonth.SelectedValue.ToString()); 
        obj_PyresTracker.Zone = Int32.Parse(ddlZone.SelectedValue.ToString()); 
        obj_PyresTracker.Circle = Int32.Parse(ddlCircle.SelectedValue.ToString());
        obj_PyresTracker.Division = Int32.Parse(ddlDivision.SelectedValue.ToString());
        obj_PyresTracker.UrbanPopulation = Int32.Parse(txtUrbanPopulation.Text);
        obj_PyresTracker.PopulationCreamtion80 = Int32.Parse(txtPopulationCreamtion80.Text);
        obj_PyresTracker.DeathPer1000 = double.Parse(txtDeathPer1000.Text);
        obj_PyresTracker.EstDeath10Buffer = Int32.Parse(txtEstDeath10Buffer.Text);
        obj_PyresTracker.Conventional = Int32.Parse(txtConventional.Text);
        obj_PyresTracker.ImprovisedWood = Int32.Parse(txtImprovisedWood.Text);
        obj_PyresTracker.Gas = Int32.Parse(txtGas.Text);
        obj_PyresTracker.Electric = Int32.Parse(txtElectric.Text);
        obj_PyresTracker.ExistCapacity = Int32.Parse(txtExistCapacity.Text);
        obj_PyresTracker.UpgradeExisting = txtUpgradeExisting.Text.ToString();
        obj_PyresTracker.RemainingToBeHandled = Int32.Parse(txtRemainingToBeHandled.Text);
        obj_PyresTracker.UpgradeImprovisedWood = Int32.Parse(txtUpgradeImprovisedWood.Text);
        obj_PyresTracker.UpgradeGas = Int32.Parse(txtUpgradeGas.Text);
        obj_PyresTracker.UpgradeElectric = Int32.Parse(txtUpgradeElectric.Text);
        obj_PyresTracker.RemainingCapacity = Int32.Parse(txtRemainingCapacity.Text);
        obj_PyresTracker.CommentOnCapacity = txtCommentOnCapacity.Text.ToString();
        obj_PyresTracker.PyresToBeRevamped = Int32.Parse(txtPyresToBeRevamped.Text);
        obj_PyresTracker.FundsRequired = double.Parse(txtFundsRequired.Text);

        int result = objPyres.InsertMainTracker(obj_PyresTracker);

        if(result>0)
        {
            MessageBox.Show("Record saved successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Record already exists! Year, Month and Division should be different in all the record.");
            return;
        }
    }

    private void reset()
    {
        ddlYear.SelectedValue="0";
        ddlMonth.SelectedValue = "0";
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.SelectedValue = "0";
        txtUrbanPopulation.Text="";
        txtPopulationCreamtion80.Text = "";
        txtDeathPer1000.Text = "";
        txtEstDeath10Buffer.Text = "";
        txtConventional.Text = "";
        txtImprovisedWood.Text = "";
        txtGas.Text = "";
        txtElectric.Text = "";
        txtExistCapacity.Text = "";
        txtUpgradeExisting.Text = "";
        txtRemainingToBeHandled.Text = "";
        txtUpgradeImprovisedWood.Text = "";
        txtUpgradeGas.Text = "";
        txtUpgradeElectric.Text = "";
        txtRemainingCapacity.Text = "";
        txtCommentOnCapacity.Text = "";
        txtPyresToBeRevamped.Text = "";
        txtFundsRequired.Text = "";
    }
}