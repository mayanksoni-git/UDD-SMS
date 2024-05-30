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

            if (Request.QueryString.Count > 0 && Request.QueryString["PyresTracker_Id"] !=null)
            {
                int PyresTracker_Id = Convert.ToInt32(Request.QueryString["PyresTracker_Id"].ToString());
                Load_PyresTracker_Details(PyresTracker_Id);
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
    //Variables names picked from the columns of ExcelSheet provided to make this form
    
    protected void TextChangedEvent(object sender, EventArgs e)
    {
        CalculateAllFields();
    }

    private void CalculateAllFields()
    {
        #region Variables
        Int64 C = 0;
        double D = 0.00;
        double E = 0.00;
        double F = 0.00;
        int G = 0;
        int H = 0;
        int I = 0;
        int J = 0;
        int K = 0;
        string L = "";
        int M = 0;
        int N = 0;
        int O = 0;
        int P = 0;
        string Q = "";
        //int R = 0;
        //int S = 0;
        //int T = 0;
        double U = 0;
        string V = "";
        int W = 0;
        int X = 0;

        int C2 = 1;
        int C3 = 2;
        int C4 = 4;
        int C5 = 4;

        int CostN = 0;
        int CostO = 0;
        int CostP = 0;
        #endregion

        #region Calculate Population likely to be cremated (80%)
        if (txtUrbanPopulation.Text != "")
        {
            C = Convert.ToInt64(txtUrbanPopulation.Text);
            D = C * .8;
        }
        txtPopulationCreamtion80.Text = D.ToString();
        #endregion

        #region Calculate Estimated no. of deaths per day (incl 10% buffer)
        if (txtDeathPer1000.Text != "")
        {
            E = double.Parse(txtDeathPer1000.Text);
            F = Math.Round(1.1 * ((D / 1000) * E) / 365, 0);
        }
        txtEstDeath10Buffer.Text = F.ToString();
        #endregion

        #region Calculate Existing 'mortal remains' handling capacity
        if (txtConventional.Text != "")
        {
            G = Int32.Parse(txtConventional.Text);
        }
        if (txtImprovisedWood.Text != "")
        {
            H = Int32.Parse(txtImprovisedWood.Text);
        }
        if (txtGas.Text != "")
        {
            I = Int32.Parse(txtGas.Text);
        }
        if (txtElectric.Text != "")
        {
            J = Int32.Parse(txtElectric.Text);
        }
        K = G * C2 + H * C3 + I * C4 + J * C5;
        txtExistCapacity.Text = K.ToString();
        #endregion

        #region Calculate Upgrade existing, Remaining 'mortal remains' to be handled
        //Calculate
        //L(Decision: Upgrade existing or build new dependent on existing capacity),
        //M (Total estimated deaths per day - Existing 'mortal remains' handling capacity of EFCs = Remaining 'mortal remains' to be handled)-----------------------------

        M = Convert.ToInt32(F) - (H * C3 + I * C4 + J * C5);
        if (F > K && G > 0)
        {
            L = "Build New + Upgrade Existing";
        }
        else if (F <= K && G > 0 && M > 0)
        {
            L = "Upgrade Existing";
        }
        else
        {
            L = "Upgradation not required";
        }
        txtUpgradeExisting.Text = L;

        txtRemainingToBeHandled.Text = M.ToString();
        if (M > 0)
        {
            divUpgradation.Visible = true;
            //txtUpgradeImprovisedWood.Text = "";
            //txtUpgradeGas.Text = "";
            //txtUpgradeElectric.Text = "";
        }
        else
        {
            divUpgradation.Visible = false;
            //txtUpgradeImprovisedWood.Text = "0";
            //txtUpgradeGas.Text = "0";
            //txtUpgradeElectric.Text = "0";
            //txtPyresToBeRevamped.Text = "0";
        }
        #endregion

        #region Calculate Check on total existing conventional pyres, Final output(Pyres to be revamped)
        if (txtUpgradeImprovisedWood.Text != "")
        {
            N = Int32.Parse(txtUpgradeImprovisedWood.Text);
        }
        if (txtUpgradeGas.Text != "")
        {
            O = Int32.Parse(txtUpgradeGas.Text);
        }
        if (txtUpgradeElectric.Text != "")
        {
            P = Int32.Parse(txtUpgradeElectric.Text);
        }
        if (txtCostImprovisedWood.Text != "")
        {
            CostN = Int32.Parse(txtCostImprovisedWood.Text);
        }
        if (txtCostGas.Text != "")
        {
            CostO = Int32.Parse(txtCostGas.Text);
        }
        if (txtCostElectric.Text != "")
        {
            CostP = Int32.Parse(txtCostElectric.Text);
        }

        W = N + O + P;
        Q = W > G ? "Exceeded total upgradation possible" : "OK";
        txtPyresToBeRevamped.Text = W.ToString();
        //txtCheckOn.Text = Q;
        #endregion

        #region Calculate Remaining capacity (ideally should be negative)
        string R = "Not applicable";
        double S = 0;
        double T = 0;
        U = Calculate(F, H, I, J, N, O, P, R, S, T, C3, C4, C5);
        U = Math.Round(U, 0);
        txtRemainingCapacity.Text = U.ToString();
        #endregion

        #region Calculate Comment on Capacity
        if (U > 0)
        {
            V = "More EFP required";
        }
        else if (U == 0)
        {
            V = "EFP requirement met";
        }
        else
        {
            V = "Exceeded EFP requirement";
        }
        txtCommentOnCapacity.Text = V;
        #endregion

        #region Calculate Total Fund Required
        X = N * CostN + O * CostO + P * CostP;
        txtFundsRequired.Text = X.ToString();
        #endregion
    }
    public static double Calculate(double F, double H, double I, double J, double N, double O, double P, string R, double S, double T, double C3, double C4, double C5)
    {
        try
        {
            double result = F - H * C3 - I * C4 - J * C5;

            result -= SafeMultiply(N, C3);
            result -= SafeMultiply(O, C4);
            result -= SafeMultiply(P, C5);

            if (R != "Not applicable")
            {
                result -= SafeMultiply(ConvertToDouble(R), C3);
                result -= SafeMultiply(S, C4);
                result -= SafeMultiply(T, C5);
            }

            return result;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    private static double SafeMultiply(double value, double multiplier)
    {
        try
        {
            return value * multiplier;
        }
        catch
        {
            return 0; // Similar to IFERROR returning 0 on error
        }
    }
    private static double ConvertToDouble(string value)
    {
        double result;
        if (double.TryParse(value, out result))
        {
            return result;
        }
        return 0; // Return 0 if conversion fails, similar to IFERROR
    }


    //Revoked Method--------------------------------------------------------------------------
    protected void txtUrbanPopulation_TextChanged(object sender, EventArgs e)
    {

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
        int CostN13 = 0;
        int CostO13 = 0;
        int CostP13 = 0;
        int G13 = 0;
        int TotalFundRequired = 0;


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
        if (txtCostImprovisedWood.Text != "")
        {
            CostN13 = Int32.Parse(txtCostImprovisedWood.Text);
        }
        if (txtCostGas.Text != "")
        {
            CostO13 = Int32.Parse(txtCostGas.Text);
        }
        if (txtCostElectric.Text != "")
        {
            CostP13 = Int32.Parse(txtCostElectric.Text);
        }



        if (txtConventional.Text != "")
        {
            G13 = Int32.Parse(txtConventional.Text);
        }

        // Perform the calculation
        int sumNtoP = N13 + O13 + P13;
        TotalFundRequired = N13 * CostN13 + O13 * CostO13 + P13 * CostP13;
        string result = sumNtoP > G13 ? "Exceeded total upgradation possible" : "OK";
        txtFundsRequired.Text = TotalFundRequired.ToString();
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
    //----------------------------------------------------------------------------------------

    

    //Click Events --------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool IsValid = true;
        IsValid = ValidateFields();
        if(IsValid==false)
        {
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
        obj_PyresTracker.ExistCMTR = Int32.Parse(txtExistCMTR.Text);

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
        obj_PyresTracker.CostImprovisedWood = Int32.Parse(txtCostImprovisedWood.Text);
        obj_PyresTracker.CostGas = Int32.Parse(txtCostGas.Text);
        obj_PyresTracker.CostElectric = Int32.Parse(txtCostElectric.Text);

        obj_PyresTracker.RemainingCapacity = Int32.Parse(txtRemainingCapacity.Text);
        obj_PyresTracker.CommentOnCapacity = txtCommentOnCapacity.Text.ToString();

        obj_PyresTracker.PyresToBeRevamped = Int32.Parse(txtPyresToBeRevamped.Text);
        obj_PyresTracker.FundsRequired = double.Parse(txtFundsRequired.Text);

        int result = objPyres.InsertPyresTracker(obj_PyresTracker);

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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool IsValid = true;
        IsValid = ValidateFields();
        if (IsValid == false)
        {
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
        obj_PyresTracker.ExistCMTR = Int32.Parse(txtExistCMTR.Text);
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
        obj_PyresTracker.CostImprovisedWood = Int32.Parse(txtCostImprovisedWood.Text);
        obj_PyresTracker.CostGas = Int32.Parse(txtCostGas.Text);
        obj_PyresTracker.CostElectric = Int32.Parse(txtCostElectric.Text);
        obj_PyresTracker.RemainingCapacity = Int32.Parse(txtRemainingCapacity.Text);
        obj_PyresTracker.CommentOnCapacity = txtCommentOnCapacity.Text.ToString();
        obj_PyresTracker.PyresToBeRevamped = Int32.Parse(txtPyresToBeRevamped.Text);
        obj_PyresTracker.FundsRequired = double.Parse(txtFundsRequired.Text);

        int result = objPyres.UpdatePyresTracker(obj_PyresTracker, Convert.ToInt32(hfPyresTracker_Id.Value));

        if (result > 0)
        {
            MessageBox.Show("Record updated successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Something wen wrong please try again or contact administration!");
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }
    public bool ValidateFields()
    {
        //CalculateAllFields();

        bool IsValid = true;
        if (ddlYear.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Year. ");
            ddlYear.Focus();
            IsValid = false;
        }
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Month. ");
            ddlMonth.Focus();
            IsValid = false;
        }
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
            IsValid = false;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            IsValid = false;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            IsValid = false;
        }

        if (txtUrbanPopulation.Text == "")
        {
            MessageBox.Show("Please enter Total urban population. ");
            txtUrbanPopulation.Focus();
            IsValid = false;
        }
        if (txtDeathPer1000.Text == "")
        {
            MessageBox.Show("Please enter Death rate per 1000 per year. ");
            txtDeathPer1000.Focus();
            IsValid = false;
        }
        if (txtExistCMTR.Text == "")
        {
            MessageBox.Show("Please enter no of existing crematorium. ");
            txtExistCMTR.Focus();
            IsValid = false;
        }
        if (txtConventional.Text == "")
        {
            MessageBox.Show("Please enter existing conventional pyres. ");
            txtConventional.Focus();
            IsValid = false;
        }
        if (txtImprovisedWood.Text == "")
        {
            MessageBox.Show("Please enter existing Improvised Wood pyres. ");
            txtImprovisedWood.Focus();
            IsValid = false;
        }
        if (txtGas.Text == "")
        {
            MessageBox.Show("Please enter existing Gas pyres. ");
            txtGas.Focus();
            IsValid = false;
        }
        if (txtElectric.Text == "")
        {
            MessageBox.Show("Please enter existing Electric pyres. ");
            txtElectric.Focus();
            IsValid = false;
        }
        if(txtRemainingToBeHandled.Text!="" && Convert.ToInt32(txtRemainingToBeHandled.Text)>0)
        {
            if(txtUpgradeImprovisedWood.Text=="")
            {
                MessageBox.Show("Please enter upgradation required for Improvised Wood Pyres!. ");
                txtUpgradeImprovisedWood.Focus();
                IsValid = false;
            }
            if (txtUpgradeGas.Text == "")
            {
                MessageBox.Show("Please enter upgradation required for Gas Pyres!. ");
                txtUpgradeGas.Focus();
                IsValid = false;
            }
            if (txtElectric.Text == "")
            {
                MessageBox.Show("Please enter upgradation required for Electric Pyres!. ");
                txtElectric.Focus();
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
        }
        else
        {
            IsValid = true;
        }

        return IsValid;
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
        txtExistCMTR.Text = "";

        txtConventional.Text = "";
        txtImprovisedWood.Text = "";
        txtGas.Text = "";
        txtElectric.Text = "";
        txtExistCapacity.Text = "";

        txtUpgradeExisting.Text = "";
        txtRemainingToBeHandled.Text = "";

        txtUpgradeImprovisedWood.Text = "";
        txtCostImprovisedWood.Text = "23";
        txtUpgradeGas.Text = "";
        txtCostGas.Text = "45";
        txtUpgradeElectric.Text = "";
        txtCostElectric.Text = "51";

        txtRemainingCapacity.Text = "";
        txtCommentOnCapacity.Text = "";

        txtPyresToBeRevamped.Text = "";
        txtFundsRequired.Text = "";
    }
    protected void Load_PyresTracker_Details(int PyresTracker_Id)
    {
        DataTable dt = new DataTable();
        dt = objPyres.getPyresTrackerById(PyresTracker_Id);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfPyresTracker_Id.Value = PyresTracker_Id.ToString();
            ddlYear.SelectedValue = dt.Rows[0]["Year"].ToString();
            ddlMonth.SelectedValue = dt.Rows[0]["Month"].ToString();
            try
            {
                ddlZone.SelectedValue = dt.Rows[0]["Zone"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }

            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
            try
            {
                ddlCircle.SelectedValue = dt.Rows[0]["Circle"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }

            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["Division"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }

            txtUrbanPopulation.Text = dt.Rows[0]["UrbanPopulation"].ToString();
            txtPopulationCreamtion80.Text = dt.Rows[0]["PopulationCreamtion80"].ToString();
            txtDeathPer1000.Text = dt.Rows[0]["DeathPer1000"].ToString();
            txtEstDeath10Buffer.Text = dt.Rows[0]["EstDeath10Buffer"].ToString();
            txtExistCMTR.Text = dt.Rows[0]["ExistCMTR"].ToString();
            txtConventional.Text = dt.Rows[0]["Conventional"].ToString();
            txtImprovisedWood.Text = dt.Rows[0]["ImprovisedWood"].ToString();
            txtGas.Text = dt.Rows[0]["Gas"].ToString();
            txtElectric.Text = dt.Rows[0]["Electric"].ToString();
            txtExistCapacity.Text = dt.Rows[0]["ExistCapacity"].ToString();
            txtUpgradeExisting.Text = dt.Rows[0]["UpgradeExisting"].ToString();
            txtRemainingToBeHandled.Text = dt.Rows[0]["RemainingToBeHandled"].ToString();
            txtUpgradeImprovisedWood.Text = dt.Rows[0]["UpgradeImprovisedWood"].ToString();
            txtUpgradeGas.Text = dt.Rows[0]["UpgradeGas"].ToString();
            txtUpgradeElectric.Text = dt.Rows[0]["UpgradeElectric"].ToString();
            txtCostImprovisedWood.Text = dt.Rows[0]["CostImprovisedWood"].ToString();
            txtCostGas.Text = dt.Rows[0]["CostGas"].ToString();
            txtCostElectric.Text = dt.Rows[0]["CostElectric"].ToString();
            if (Convert.ToInt32(dt.Rows[0]["RemainingToBeHandled"]) > 0)
            {
                divUpgradation.Visible = true;
            }
            else
            {
                divUpgradation.Visible = false;
            }
            txtRemainingCapacity.Text = dt.Rows[0]["RemainingCapacity"].ToString();
            txtCommentOnCapacity.Text = dt.Rows[0]["CommentOnCapacity"].ToString();
            txtPyresToBeRevamped.Text = dt.Rows[0]["PyresToBeRevamped"].ToString();
            txtFundsRequired.Text = dt.Rows[0]["FundsRequired"].ToString();
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        else
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            MessageBox.Show("Record with pyres tracker id = "+ PyresTracker_Id.ToString()+" does not found please contact administration.");
        }
    }
}