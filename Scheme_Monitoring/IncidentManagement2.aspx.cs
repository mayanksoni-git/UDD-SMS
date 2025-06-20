using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IncidentManagement2 : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    string connectionString = ConfigurationManager.AppSettings.Get("conn");
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
            BindDistricts();
            BindIncidents();
        }
    }

    private void BindDistricts()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_GetDistrictsAndULBs", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Get distinct districts
            DataView dv = new DataView(dt);
            DataTable distinctDistricts = dv.ToTable(true, "DistrictId", "DistrictName");

            ddlDistrict.DataSource = distinctDistricts;
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictId";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("Select District", ""));
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex > 0)
        {
            int districtId = Convert.ToInt32(ddlDistrict.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetDistrictsAndULBs", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Filter ULBs by selected district
                DataView dv = dt.DefaultView;
                dv.RowFilter = "DistrictId = " + districtId;
                DataTable filteredULBs = dv.ToTable(true, "ULBId", "ULBName");

                ddlULB.DataSource = filteredULBs;
                ddlULB.DataTextField = "ULBName";
                ddlULB.DataValueField = "ULBId";
                ddlULB.DataBind();
                ddlULB.Items.Insert(0, new ListItem("Select ULB", ""));
            }
        }
        else
        {
            ddlULB.Items.Clear();
        }
    }

    protected void btnAddParty_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtPartyName.Text))
            {
                ShowMessage("Please select designation and enter party name");
                return;
            }

            List<IncidentParty> parties = ViewState["IncidentParties"] as List<IncidentParty> ?? new List<IncidentParty>();

            parties.Add(new IncidentParty
            {
                Designation = ddlDesignation.SelectedValue,
                PartyName = txtPartyName.Text
            });

            ViewState["IncidentParties"] = parties;
            BindPartiesGrid();
            ClearPartyForm();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("AddParty Error: " + ex.ToString());
            ShowMessage("Failed to add party. Please try again.");
        }
    }

    protected void gvParties_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveParty")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            List<IncidentParty> parties = ViewState["IncidentParties"] as List<IncidentParty>;

            if (parties != null && parties.Count > rowIndex)
            {
                parties.RemoveAt(rowIndex);
                ViewState["IncidentParties"] = parties;
                BindPartiesGrid();
            }
        }
    }

    private void BindPartiesGrid()
    {
        List<IncidentParty> parties = ViewState["IncidentParties"] as List<IncidentParty>;
        gvParties.DataSource = parties ?? new List<IncidentParty>();
        gvParties.DataBind();
    }

    private void ClearPartyForm()
    {
        ddlDesignation.SelectedIndex = 1;
        txtPartyName.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateIncidentForm())
        {
            return;
        }

        List<IncidentParty> parties = ViewState["IncidentParties"] as List<IncidentParty>;
        if (parties == null || parties.Count == 0)
        {
            ShowMessage("Please add at least one party to the incident");
            return;
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Insert incident master
                int incidentId = 0;
                SqlCommand cmdInsert = new SqlCommand("usp_InsertIncident", conn, transaction);
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@ULBId", ddlULB.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@ChairmanName", txtChairmanName.Text);
                cmdInsert.Parameters.AddWithValue("@RemindInDays", ddlRemindDays.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@CreatedBy", GetCurrentUserId());
                cmdInsert.Parameters.Add("@Incident_Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmdInsert.ExecuteNonQuery();

                incidentId = Convert.ToInt32(cmdInsert.Parameters["@Incident_Id"].Value);

                // Insert parties
                foreach (var party in parties)
                {
                    SqlCommand cmdParty = new SqlCommand("usp_InsertIncidentParty", conn, transaction);
                    cmdParty.CommandType = CommandType.StoredProcedure;
                    cmdParty.Parameters.AddWithValue("@Incident_Id", incidentId);
                    cmdParty.Parameters.AddWithValue("@Designation", party.Designation);
                    cmdParty.Parameters.AddWithValue("@PartyName", party.PartyName);
                    cmdParty.Parameters.AddWithValue("@CreatedBy", GetCurrentUserId());
                    cmdParty.ExecuteNonQuery();
                }

                transaction.Commit();
                ShowMessage("Incident saved successfully!", true);
                ClearForm();
                BindIncidents();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ShowMessage("Error saving incident: " + ex.Message);
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateIncidentForm())
        {
            return;
        }

        List<IncidentParty> parties = ViewState["IncidentParties"] as List<IncidentParty>;
        if (parties == null || parties.Count == 0)
        {
            ShowMessage("Please add at least one party to the incident");
            return;
        }

        int incidentId = Convert.ToInt32(hfIncidentId.Value);

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Update incident master
                SqlCommand cmdUpdate = new SqlCommand("usp_UpdateIncident", conn, transaction);
                cmdUpdate.CommandType = CommandType.StoredProcedure;
                cmdUpdate.Parameters.AddWithValue("@Incident_Id", incidentId);
                cmdUpdate.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@ULBId", ddlULB.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@ChairmanName", txtChairmanName.Text);
                cmdUpdate.Parameters.AddWithValue("@RemindInDays", ddlRemindDays.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@ModifiedBy", GetCurrentUserId());
                cmdUpdate.ExecuteNonQuery();

                // Delete existing parties
                SqlCommand cmdDelete = new SqlCommand("usp_DeleteIncidentParties", conn, transaction);
                cmdDelete.CommandType = CommandType.StoredProcedure;
                cmdDelete.Parameters.AddWithValue("@Incident_Id", incidentId);
                cmdDelete.ExecuteNonQuery();

                // Insert updated parties
                foreach (var party in parties)
                {
                    SqlCommand cmdParty = new SqlCommand("usp_InsertIncidentParty", conn, transaction);
                    cmdParty.CommandType = CommandType.StoredProcedure;
                    cmdParty.Parameters.AddWithValue("@Incident_Id", incidentId);
                    cmdParty.Parameters.AddWithValue("@Designation", party.Designation);
                    cmdParty.Parameters.AddWithValue("@PartyName", party.PartyName);
                    cmdParty.Parameters.AddWithValue("@CreatedBy", GetCurrentUserId());
                    cmdParty.ExecuteNonQuery();
                }

                transaction.Commit();
                ShowMessage("Incident updated successfully!", true);
                ClearForm();
                BindIncidents();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ShowMessage("Error updating incident: " + ex.Message);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    protected void gvIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditIncident")
        {
            int incidentId = Convert.ToInt32(e.CommandArgument);
            LoadIncidentForEdit(incidentId);
        }
    }

    private void LoadIncidentForEdit(int incidentId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_GetIncidentById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Incident_Id", incidentId);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                hfIncidentId.Value = incidentId.ToString();
                DataRow dr = dt.Rows[0];

                // Bind district and ULB
                ddlDistrict.SelectedValue = dr["DistrictId"].ToString();
                ddlDistrict_SelectedIndexChanged(null, null);
                ddlULB.SelectedValue = dr["ULBId"].ToString();

                txtChairmanName.Text = dr["ChairmanName"].ToString();
                ddlRemindDays.SelectedValue = dr["RemindInDays"].ToString();

                // Load parties
                List<IncidentParty> parties = new List<IncidentParty>();
                foreach (DataRow row in dt.Rows)
                {
                    if (!row.IsNull("Designation"))
                    {
                        parties.Add(new IncidentParty
                        {
                            Designation = row["Designation"].ToString(),
                            PartyName = row["PartyName"].ToString()
                        });
                    }
                }

                ViewState["IncidentParties"] = parties;
                BindPartiesGrid();

                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
        }
    }

    private void BindIncidents()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_GetAllIncidents", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            gvIncidents.DataSource = dt;
            gvIncidents.DataBind();
        }
    }

    private bool ValidateIncidentForm()
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ShowMessage("Please select district");
            return false;
        }
        if (ddlULB.SelectedIndex == 0)
        {
            ShowMessage("Please select ULB");
            return false;
        }
        if (string.IsNullOrEmpty(txtChairmanName.Text))
        {
            ShowMessage("Please enter chairman name");
            return false;
        }
        return true;
    }

    private void ClearForm()
    {
        hfIncidentId.Value = "0";
        ddlDistrict.SelectedIndex = 0;
        ddlULB.Items.Clear();
        txtChairmanName.Text = "";
        ddlRemindDays.SelectedIndex = 0;
        ViewState["IncidentParties"] = null;
        BindPartiesGrid();
        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }

    private int GetCurrentUserId()
    {
        // Implement your logic to get current user ID
        // This is just a placeholder
        return 1;
    }

    private void ShowMessage(string message, bool isSuccess = false)
    {
        string script = "alert('" + message + "');";
        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", script, true);
    }
}