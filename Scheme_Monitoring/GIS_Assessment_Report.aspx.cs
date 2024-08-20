using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Device.Location;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
//using GoogleMaps.LocationServices;

public partial class GIS_Assessment_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Phase"] = "1";
            get_District_GIS();
            btnSearch_Click(btnSearch, e);
        }
    }
    
    private void get_District_GIS()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_District_GIS();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "District", "District");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }

    private void get_ULB_GIS(string District)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_ULB_GIS(District);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlULB, "ULB", "ULB");
        }
        else
        {
            ddlULB.Items.Clear();
        }
    }

    private void get_Tbl_ULBAssessment(string District, string ULB, string Phase_In, string BuildingType_In)
    {
        if (District == "0")
        {
            District = "";
        }
        if (ULB == "0")
        {
            ULB = "";
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_GIS_Data(District, ULB, Phase_In, BuildingType_In);
        if (AllClasses.CheckDataSet(ds))
        {
            List<CMNSY_Data> obj_CMNSY_Data_Li = new List<CMNSY_Data>();
            obj_CMNSY_Data_Li = (from DataRow dr in ds.Tables[0].Rows
                                        select new CMNSY_Data()
                                        {
                                            District = dr["District"].ToString(),
                                            Type = dr["Type"].ToString(),
                                            ULB = dr["ULB"].ToString(),
                                            Long = dr["Long"].ToString(),
                                            Lat = dr["Lat"].ToString(),
                                            Phase_1 = dr["Phase_1"].ToString(),
                                            Phase_2 = dr["Phase_2"].ToString(),
                                            Building_Type = dr["Building_Type"].ToString(),
                                            Phase = dr["Phase"].ToString(),
                                            Sr_No = Convert.ToInt32(dr["Sr_No"].ToString())
                                        }).ToList();

            if (obj_CMNSY_Data_Li != null && obj_CMNSY_Data_Li.Count > 0)
            {
                string jsonString = JsonConvert.SerializeObject(obj_CMNSY_Data_Li);
                hf_Map_Data.Value = jsonString;
            }
            else
            {
                hf_Map_Data.Value = "";
            }
        }
        else
        {
            hf_Map_Data.Value = "";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Phase_In = "";
        if (chkPhases.Items[0].Selected && chkPhases.Items[1].Selected)
        {
            Phase_In += "1, 2";
        }
        else if (chkPhases.Items[0].Selected)
        {
            Phase_In += "1";
        }
        else if (chkPhases.Items[1].Selected)
        {
            Phase_In += "2";
        }
        else
        {
            MessageBox.Show("Please Select Any Phase!!");
            return;
        }
        string Building_Type_In = "";
        string Building_Type_In_Temp1 = "";
        string Building_Type_In_Temp2 = "";
        string Building_Type_In_Temp3 = "";

        if (chkBuilding_Type.Items[0].Selected && chkBuilding_Type.Items[1].Selected && chkBuilding_Type.Items[2].Selected)
        {
            Building_Type_In += "'Kalyan Mandap', 'Office Building', 'Office Building and Kalyan Mandap'";
        }
        else
        {
            if (chkBuilding_Type.Items[0].Selected)
            {
                Building_Type_In_Temp1 = "'Kalyan Mandap'";
            }
            else if (chkBuilding_Type.Items[1].Selected)
            {
                Building_Type_In_Temp2 = "'Office Building'";
            }
            else if (chkBuilding_Type.Items[2].Selected)
            {
                Building_Type_In_Temp3 = "'Office Building and Kalyan Mandap'";
            }
            else
            {
                Building_Type_In_Temp1 = "";
                Building_Type_In_Temp2 = "";
                Building_Type_In_Temp3 = "";
            }
            Building_Type_In = (Building_Type_In_Temp1 + Building_Type_In_Temp2 + Building_Type_In_Temp3).Replace("''", "','");
        }
        string District = ddlDistrict.SelectedItem.Text;
        string ULB = "";
        try
        {
            ULB = ddlULB.SelectedItem.Text;
        }
        catch
        {
            ULB = "";
        }
        get_Tbl_ULBAssessment(District, ULB, Phase_In, Building_Type_In);

        if (chkPhases.Items[0].Selected)
        {
            Session["Phase"] = "1";
        }
        else 
        {
            Session["Phase"] = "2";
        }



    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
        }
        else
        {
            get_ULB_GIS(ddlDistrict.SelectedItem.Text);
        }
    }
}