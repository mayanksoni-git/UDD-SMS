using System;
using System.Data;
using System.Web.UI.WebControls;

public class BasePage : System.Web.UI.Page
{
    // Method to fill dropdowns
    protected void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
        }
        else
        {
            ddl.Items.Clear();
        }
    }

    // Method to get Zone data
    protected void GetTblZone(DropDownList ddlZone, DropDownList ddlCircle, DropDownList ddlDivision)
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem != null && ddlZone.SelectedItem.Value != "0")
        {
            GetTblCircle(Convert.ToInt32(ddlZone.SelectedValue), ddlCircle, ddlDivision);
        }
    }

    // Method to get Circle data
    protected void GetTblCircle(int zoneId, DropDownList ddlCircle, DropDownList ddlDivision)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }

    // Method to get Division data
    protected void GetTblDivision(int circleId, DropDownList ddlDivision)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }

    // Method to handle Zone dropdown change
    protected void DdlZone_SelectedIndexChanged(DropDownList ddlZone, DropDownList ddlCircle, DropDownList ddlDivision)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            GetTblCircle(Convert.ToInt32(ddlZone.SelectedValue), ddlCircle, ddlDivision);
        }
    }

    // Method to handle Circle dropdown change
    protected void DdlCircle_SelectedIndexChanged(DropDownList ddlCircle, DropDownList ddlDivision)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            GetTblDivision(Convert.ToInt32(ddlCircle.SelectedValue), ddlDivision);
        }
    }

    // Method to set dropdowns based on user type
    protected void SetDropdownsBasedOnUserType(DropDownList ddlZone, DropDownList ddlCircle, DropDownList ddlDivision)
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);

        if (userType == 4 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId, ddlCircle, ddlDivision);
        }
        else if (userType == 6 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId, ddlCircle, ddlDivision);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId, null, ddlDivision);
            }
        }
        else if (userType == 7 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId, ddlCircle, ddlDivision);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId, null, ddlDivision);
                if (divisionId > 0)
                {
                    SetDropdownValueAndDisable(ddlDivision, divisionId);
                }
            }
        }
    }

    // Method to set dropdown value and disable it
    protected void SetDropdownValueAndDisable(DropDownList ddl, int value, DropDownList ddlCircle = null, DropDownList ddlDivision = null)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;

            // Trigger the appropriate event based on the dropdown ID
            if (ddl.ID == "ddlZone" && ddlCircle != null && ddlDivision != null)
            {
                DdlZone_SelectedIndexChanged(ddl, ddlCircle, ddlDivision);
            }
            else if (ddl.ID == "ddlCircle" && ddlDivision != null)
            {
                DdlCircle_SelectedIndexChanged(ddl, ddlDivision);
            }
        }
        catch
        {
            // Handle exception if needed
        }
    }
}