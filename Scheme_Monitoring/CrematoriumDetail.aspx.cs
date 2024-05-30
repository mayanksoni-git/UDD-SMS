using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CrematoriumDetail : System.Web.UI.Page
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

            if (Request.QueryString["CrematoriumDetail_Id"] !=null)
            {
                int CrematoriumDetail_Id = Convert.ToInt32(Request.QueryString["CrematoriumDetail_Id"].ToString());
                Load_CrematoriumDetail(CrematoriumDetail_Id);
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

    //Calculations-------------------------------------------------------------------------------------
    protected void CalculateNoOfPyres(object sender, EventArgs e)
    {
        int NoOfPyres=0, Conventional=0, ImprovisedWood=0, Gas=0, Electric=0;

        if (txtConventional.Text != "")
        {
            Conventional = Int32.Parse(txtConventional.Text);
        }
        if (txtImprovisedWood.Text != "")
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
        NoOfPyres = Conventional + ImprovisedWood + Gas + Electric;
        txtNoOfPyres.Text = NoOfPyres.ToString();
    }


    //Click Events --------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string imageLocation = "";
        bool IsValid = true;
        IsValid = ValidateFields();
        if(IsValid==false)
        {
            return;
        }

        if (fuCMTRImage.HasFile)
        {
            string errorMessage;
            imageLocation = ImageUploader.UploadImage(fuCMTRImage.PostedFile, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                // Save relativePath to your database
                //MessageLabel.Text = "Image uploaded successfully. Path: " + imageLocation;
            }
            else
            {
                MessageBox.Show("Error: " + errorMessage);
                return;
            }
        }
        else
        {
            MessageBox.Show("Please select an image to upload.");
            return;
        }
        if (txtConventional.Text == null || txtConventional.Text == "")
        {
            txtConventional.Text = "0";
        }
        if (txtImprovisedWood.Text == null || txtImprovisedWood.Text == "")
        {
            txtImprovisedWood.Text = "0";
        }
        if (txtGas.Text == null || txtGas.Text == "")
        {
            txtGas.Text = "0";
        }
        if (txtElectric.Text == null || txtElectric.Text == "")
        {
            txtElectric.Text = "0";
        }
        if (txtNoOfPyres.Text==null || txtNoOfPyres.Text=="")
        {
            txtNoOfPyres.Text = "0";
        }
        if (txtTotalCMTRDone.Text == null || txtTotalCMTRDone.Text == "")
        {
            txtTotalCMTRDone.Text = "0";
        }

        tbl_CrematoriumDetail obj_CrematoriumDetail = new tbl_CrematoriumDetail();

        obj_CrematoriumDetail.AddedBy = Int32.Parse(Session["Person_Id"].ToString());
        obj_CrematoriumDetail.Year = Int32.Parse(ddlYear.SelectedValue.ToString());
        obj_CrematoriumDetail.Month = Int32.Parse(ddlMonth.SelectedValue.ToString()); 
        obj_CrematoriumDetail.Zone = Int32.Parse(ddlZone.SelectedValue.ToString()); 
        obj_CrematoriumDetail.Circle = Int32.Parse(ddlCircle.SelectedValue.ToString());
        obj_CrematoriumDetail.Division = Int32.Parse(ddlDivision.SelectedValue.ToString());
        obj_CrematoriumDetail.NameCMTR = txtNameCMTR.Text;
        obj_CrematoriumDetail.LocationCMTR = txtLocationCMTR.Text;

        obj_CrematoriumDetail.Conventional = Convert.ToInt32(txtConventional.Text.ToString());
        obj_CrematoriumDetail.ImprovisedWood = Convert.ToInt32(txtImprovisedWood.Text.ToString());
        obj_CrematoriumDetail.Gas = Convert.ToInt32(txtGas.Text.ToString());
        obj_CrematoriumDetail.Electric = Convert.ToInt32(txtElectric.Text.ToString());
        obj_CrematoriumDetail.NoOfPyres = Convert.ToInt32(txtNoOfPyres.Text.ToString());

        obj_CrematoriumDetail.DrinkingWater = Convert.ToBoolean(Convert.ToInt32(rblDrinkingWater.SelectedItem.Value));
        obj_CrematoriumDetail.ElecticityGrid = Convert.ToBoolean(Convert.ToInt32(rblElecticityGrid.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Parking = Convert.ToBoolean(Convert.ToInt32(rblParking.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Shed = Convert.ToBoolean(Convert.ToInt32(rblShed.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Hearse = Convert.ToBoolean(Convert.ToInt32(rblHearse.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.HandPump = Convert.ToBoolean(Convert.ToInt32(rblHandPump.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.BoundaryWall = Convert.ToBoolean(Convert.ToInt32(rblBoundaryWall.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.EntryGate = Convert.ToBoolean(Convert.ToInt32(rblEntryGate.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.PrayerHall = Convert.ToBoolean(Convert.ToInt32(rblPrayerHall.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.CareTakerRoom = Convert.ToBoolean(Convert.ToInt32(rblCareTakerRoom.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.AshStorage = Convert.ToBoolean(Convert.ToInt32(rblAshStorage.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Bathrooms = Convert.ToBoolean(Convert.ToInt32(rblBathrooms.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Washroom = Convert.ToBoolean(Convert.ToInt32(rblWashroom.SelectedItem.Value.ToString()));

        obj_CrematoriumDetail.Registration = Convert.ToInt32(rblRegistration.SelectedItem.Value.ToString());

        obj_CrematoriumDetail.CMTRImage = imageLocation;
        obj_CrematoriumDetail.FillerName = txtFillerName.Text;
        obj_CrematoriumDetail.FillerContact = txtFillerContact.Text;
        obj_CrematoriumDetail.TotalCMTRDone = Convert.ToInt32(txtTotalCMTRDone.Text.ToString());


        int result = objPyres.InsertCrematoriumDetail(obj_CrematoriumDetail);
        
        if (result>0)
        {
            reset();
            MessageBox.Show("Record has been saved successfully.");
            return;
        }
        else
        {
            reset();
            bool IsImageDelete = ImageUploader.DeleteImage(imageLocation);
            if(!IsImageDelete)
            {
                MessageBox.Show("You cannot insert more records than the number of existing crematoriums in the division. While processing this data, an image was uploaded but failed to be deleted.");
            }
            else
            {
                MessageBox.Show("You cannot insert more records than the number of existing crematoriums in the division.");
            }
            return;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string imageLocation = "";
        bool IsValid = true;
        IsValid = ValidateFields();
        if (IsValid == false)
        {
            return;
        }

        if (fuCMTRImage.HasFile)
        {
            string errorMessage;
            
            imageLocation = ImageUploader.UploadImage(fuCMTRImage.PostedFile, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                // Save relativePath to your database
                bool IsImageDelete = ImageUploader.DeleteImage(hfImageUrl.Value.ToString());

                if (!IsImageDelete)
                {
                    MessageBox.Show("While updating this data, an new image was uploaded but failed to be deleted existing image.");
                }
            }
            else
            {
                MessageBox.Show("Error: " + errorMessage);
                return;
            }
        }
        else
        {
            imageLocation = hfImageUrl.Value.ToString();
        }

        if (txtNoOfPyres.Text == null || txtNoOfPyres.Text == "")
        {
            txtNoOfPyres.Text = "0";
        }
        if (txtTotalCMTRDone.Text == null || txtTotalCMTRDone.Text == "")
        {
            txtTotalCMTRDone.Text = "0";
        }

        tbl_CrematoriumDetail obj_CrematoriumDetail = new tbl_CrematoriumDetail();

        obj_CrematoriumDetail.AddedBy = Int32.Parse(Session["Person_Id"].ToString());
        obj_CrematoriumDetail.Year = Int32.Parse(ddlYear.SelectedValue.ToString());
        obj_CrematoriumDetail.Month = Int32.Parse(ddlMonth.SelectedValue.ToString());
        obj_CrematoriumDetail.Zone = Int32.Parse(ddlZone.SelectedValue.ToString());
        obj_CrematoriumDetail.Circle = Int32.Parse(ddlCircle.SelectedValue.ToString());
        obj_CrematoriumDetail.Division = Int32.Parse(ddlDivision.SelectedValue.ToString());
        obj_CrematoriumDetail.NameCMTR = txtNameCMTR.Text;
        obj_CrematoriumDetail.LocationCMTR = txtLocationCMTR.Text;

        obj_CrematoriumDetail.Conventional = Convert.ToInt32(txtConventional.Text.ToString());
        obj_CrematoriumDetail.ImprovisedWood = Convert.ToInt32(txtImprovisedWood.Text.ToString());
        obj_CrematoriumDetail.Gas = Convert.ToInt32(txtGas.Text.ToString());
        obj_CrematoriumDetail.Electric = Convert.ToInt32(txtElectric.Text.ToString());
        obj_CrematoriumDetail.NoOfPyres = Convert.ToInt32(txtNoOfPyres.Text.ToString());

        obj_CrematoriumDetail.DrinkingWater = Convert.ToBoolean(Convert.ToInt32(rblDrinkingWater.SelectedItem.Value));
        obj_CrematoriumDetail.ElecticityGrid = Convert.ToBoolean(Convert.ToInt32(rblElecticityGrid.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Parking = Convert.ToBoolean(Convert.ToInt32(rblParking.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Shed = Convert.ToBoolean(Convert.ToInt32(rblShed.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Hearse = Convert.ToBoolean(Convert.ToInt32(rblHearse.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.HandPump = Convert.ToBoolean(Convert.ToInt32(rblHandPump.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.BoundaryWall = Convert.ToBoolean(Convert.ToInt32(rblBoundaryWall.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.EntryGate = Convert.ToBoolean(Convert.ToInt32(rblEntryGate.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.PrayerHall = Convert.ToBoolean(Convert.ToInt32(rblPrayerHall.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.CareTakerRoom = Convert.ToBoolean(Convert.ToInt32(rblCareTakerRoom.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.AshStorage = Convert.ToBoolean(Convert.ToInt32(rblAshStorage.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Bathrooms = Convert.ToBoolean(Convert.ToInt32(rblBathrooms.SelectedItem.Value.ToString()));
        obj_CrematoriumDetail.Washroom = Convert.ToBoolean(Convert.ToInt32(rblWashroom.SelectedItem.Value.ToString()));

        obj_CrematoriumDetail.Registration = Convert.ToInt32(rblRegistration.SelectedItem.Value.ToString());
        obj_CrematoriumDetail.CMTRImage = imageLocation;
        obj_CrematoriumDetail.FillerName = txtFillerName.Text;
        obj_CrematoriumDetail.FillerContact = txtFillerContact.Text;
        obj_CrematoriumDetail.TotalCMTRDone = Convert.ToInt32(txtTotalCMTRDone.Text.ToString());


        int result = objPyres.UpdateCrematoriumDetail(obj_CrematoriumDetail, Convert.ToInt32(hfCrematoriumDetail_Id.Value));

        if (result > 0)
        {
            MessageBox.Show("Record updated successfully!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Something wen wrong please try again or contact administrator!");
            return;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }

    public bool ValidateFields()
    {
        if (ddlYear.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Year. ");
            ddlYear.Focus();
            return false;
        }
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Month. ");
            ddlMonth.Focus();
            return false;
        }
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
        if (txtNameCMTR.Text == "")
        {
            MessageBox.Show("Please enter Name of Crematorium!");
            txtNameCMTR.Focus();
            return false;
        }
        if (txtLocationCMTR.Text == "")
        {
            MessageBox.Show("Please enter Location of Creamtorium!");
            txtLocationCMTR.Focus();
            return false;
        }
        if (txtNoOfPyres.Text=="")
        {
            MessageBox.Show("Please enter no of different types of pyres!");
            txtNoOfPyres.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void reset()
    {
        ddlYear.SelectedValue="0";
        ddlMonth.SelectedValue = "0";
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.SelectedValue = "0";
        txtNameCMTR.Text = "";
        txtLocationCMTR.Text = "";

        txtConventional.Text = "";
        txtImprovisedWood.Text = "";
        txtGas.Text = "";
        txtElectric.Text = "";
        txtNoOfPyres.Text = "";

        rblDrinkingWater.SelectedValue = "0";
        rblElecticityGrid.SelectedValue = "0";
        rblParking.SelectedValue = "0";
        rblShed.SelectedValue = "0";
        rblHearse.SelectedValue = "0";
        rblHandPump.SelectedValue = "0";
        rblBoundaryWall.SelectedValue = "0";
        rblEntryGate.SelectedValue = "0";
        rblPrayerHall.SelectedValue = "0";
        rblCareTakerRoom.SelectedValue = "0";
        rblAshStorage.SelectedValue = "0";
        rblBathrooms.SelectedValue = "0";
        rblWashroom.SelectedValue = "0";
        rblRegistration.SelectedValue = "0";

        
        txtFillerName.Text ="";
        txtFillerContact.Text = "";
        txtTotalCMTRDone.Text = "";


        hfImageUrl.Value = "";
        lblCMTRImageDisplay.Visible = false;
        imgCMTRImageDisplay.Visible = false;
        imgCMTRImageDisplay.ImageUrl = "";

        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void Load_CrematoriumDetail(int CrematoriumDetail_Id)
    {
        DataTable dt = new DataTable();
        dt = objPyres.getCrematoriumDetailById(CrematoriumDetail_Id);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfCrematoriumDetail_Id.Value = CrematoriumDetail_Id.ToString();
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
            ddlZone.Enabled = false;

            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
            try
            {
                ddlCircle.SelectedValue = dt.Rows[0]["Circle"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            ddlCircle.Enabled = false;

            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["Division"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            ddlDivision.Enabled = false;
            string val = dt.Rows[0]["DrinkingWater"].ToString();
            txtNameCMTR.Text = dt.Rows[0]["NameCMTR"].ToString();
            txtLocationCMTR.Text = dt.Rows[0]["LocationCMTR"].ToString();
            txtConventional.Text = dt.Rows[0]["Conventional"].ToString();
            txtImprovisedWood.Text = dt.Rows[0]["ImprovisedWood"].ToString();
            txtGas.Text = dt.Rows[0]["Gas"].ToString();
            txtElectric.Text = dt.Rows[0]["Electric"].ToString();
            txtNoOfPyres.Text = dt.Rows[0]["NoOfPyres"].ToString();
            rblDrinkingWater.SelectedValue= dt.Rows[0]["DrinkingWater"].ToString()=="True"?"1":"0";
            rblElecticityGrid.SelectedValue= dt.Rows[0]["ElecticityGrid"].ToString()=="True"?"1":"0";
            rblParking.SelectedValue= dt.Rows[0]["Parking"].ToString()=="True"?"1":"0";
            rblShed.SelectedValue= dt.Rows[0]["Shed"].ToString()=="True"?"1":"0";
            rblHearse.SelectedValue= dt.Rows[0]["Hearse"].ToString()=="True"?"1":"0";
            rblHandPump.SelectedValue= dt.Rows[0]["HandPump"].ToString()=="True"?"1":"0";
            rblBoundaryWall.SelectedValue= dt.Rows[0]["BoundaryWall"].ToString()=="True"?"1":"0";
            rblEntryGate.SelectedValue= dt.Rows[0]["EntryGate"].ToString()=="True"?"1":"0";
            rblPrayerHall.SelectedValue= dt.Rows[0]["PrayerHall"].ToString()=="True"?"1":"0";
            rblCareTakerRoom.SelectedValue= dt.Rows[0]["CareTakerRoom"].ToString()=="True"?"1":"0";
            rblAshStorage.SelectedValue= dt.Rows[0]["AshStorage"].ToString()=="True"?"1":"0";
            rblBathrooms.SelectedValue= dt.Rows[0]["Bathrooms"].ToString()=="True"?"1":"0";
            rblWashroom.SelectedValue= dt.Rows[0]["Washroom"].ToString()=="True"?"1":"0";
            rblRegistration.SelectedValue= dt.Rows[0]["Registration"].ToString();
            hfImageUrl.Value= dt.Rows[0]["CMTRImage"].ToString();
            lblCMTRImageDisplay.Visible = true;
            imgCMTRImageDisplay.Visible = true;
            imgCMTRImageDisplay.ImageUrl = dt.Rows[0]["CMTRImage"].ToString();
            txtFillerName.Text = dt.Rows[0]["FillerName"].ToString();
            txtFillerContact.Text = dt.Rows[0]["FillerContact"].ToString();
            txtTotalCMTRDone.Text = dt.Rows[0]["TotalCMTRDone"].ToString();


            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        else
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            lblCMTRImageDisplay.Visible = false;
            imgCMTRImageDisplay.Visible = false;
            MessageBox.Show("Record with Crematorium Detail id = " + CrematoriumDetail_Id.ToString()+" does not found please contact administrator.");
        }
    }
}