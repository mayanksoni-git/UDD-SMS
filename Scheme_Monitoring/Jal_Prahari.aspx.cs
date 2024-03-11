using Aspose.Pdf.Operators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Jal_Prahari : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            ViewState["Edit_Mode"] = false;
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Zone();

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
            if (Convert.ToBoolean(ViewState["Edit_Mode"].ToString()) == false)
            {
                divEditMode.Visible = false;
            }
            else
            {
                if (ddlDivision.Enabled == false && ddlDivision.Items.Count > 0 && ddlDivision.SelectedValue != "0")
                {
                    divEditMode.Visible = true;
                }
                else
                {
                    divEditMode.Visible = false;
                }
            }
            get_Bidders_Master_Data();

            if (Session["UserType"].ToString() == "7")
            {
                btnEditMode.Visible = true;
            }
            else
            {
                btnEditMode.Visible = false;
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
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
        get_Bidders_Master_Data();
    }

    private void get_Bidders_Master_Data()
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (divEditMode.Visible)
        {
            ds = (new DataLayer()).get_Bidders_Master_Data(txtGSTIN_PAN.Text, txtFirmName.Text, Zone_Id, Circle_Id, Division_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Bidders_Master_Data(txtGSTIN_PAN.Text, txtFirmName.Text, 0, 0, 0);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
            divData.Visible = true;
            if (Session["UserType"].ToString() == "1")
            {
                grdPost.Columns[9].Visible = true;
                grdPost.Columns[10].Visible = true;
            }
            else
            {
                grdPost.Columns[9].Visible = divEditMode.Visible;
                grdPost.Columns[10].Visible = divEditMode.Visible;
            }
            List<string> _columnsList = new List<string>();

            for (int i = 0; i < grdPost.Columns.Count; i++)
            {
                if (grdPost.Columns[i].Visible == true)
                {
                    _columnsList.Add(null);
                }
            }
            //_columnsList.Add(null);
            string[] columnsList = new string[_columnsList.Count];
            columnsList = _columnsList.ToArray();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            hf_dt_Options_Dynamic1.Value = jss.Serialize(columnsList);
        }
        else
        {
            divData.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        Bidders_Info obj_Bidders_Info = new Bidders_Info();
        obj_Bidders_Info.JalPrahariBidderInfo_Id = Convert.ToInt32(gr.Cells[0].Text.Trim().Replace("&nbsp;", ""));
        obj_Bidders_Info.Bidders_GSTIN = gr.Cells[5].Text.Trim().Replace("&nbsp;", "");
        obj_Bidders_Info.Bidders_FirmName = gr.Cells[3].Text.Trim().Replace("&nbsp;", "");
        obj_Bidders_Info.Bidders_PAN = gr.Cells[4].Text.Trim().Replace("&nbsp;", "");

        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (divEditMode.Visible)
        {
            obj_Bidders_Info.Zone_Id = Zone_Id;
            obj_Bidders_Info.Circle_Id = Circle_Id;
            obj_Bidders_Info.Division_Id = Division_Id;
        }
        else
        {
            obj_Bidders_Info.Zone_Id = 0;
            obj_Bidders_Info.Circle_Id = 0;
            obj_Bidders_Info.Division_Id = 0;
        }
        Session["Bidders_Info"] = obj_Bidders_Info;
        Response.Redirect("Jal_Prahari_Vendor.aspx");
    }

    protected void btnMerge_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Please contact Admin To Merge duplicate Bidder");
    }

    protected void btnUpdateMerge_Click(object sender, EventArgs e)
    {
        if (divEditMode.Visible || Session["UserType"].ToString() == "1")
        {
            if (hf_JalPrahariBidderInfo_Id.Value == "" || hf_JalPrahariBidderInfo_Id.Value == "0")
            {
                MessageBox.Show("Please Select A Bidder");
                return;
            }
            if (hf_JalPrahariBidderInfo_Id_Merged.Value == "" || hf_JalPrahariBidderInfo_Id_Merged.Value == "0")
            {//Update Basic
                Bidders_Info obj_Bidders_Info = new Bidders_Info();
                obj_Bidders_Info.JalPrahariBidderInfo_Id = Convert.ToInt32(hf_JalPrahariBidderInfo_Id.Value);
                obj_Bidders_Info.Bidders_GSTIN = txtGSTINU.Text.Trim();
                obj_Bidders_Info.Bidders_FirmName = txtFirmnameU.Text.Trim();
                obj_Bidders_Info.Bidders_PAN = txtPANNoU.Text.Trim();
                if (new DataLayer().Update_JalPrahari_Basic_Details(obj_Bidders_Info))
                {
                    MessageBox.Show("Bidder's Info Updated Successfully");
                    hf_JalPrahariBidderInfo_Id.Value = "0";
                    get_Bidders_Master_Data();
                    return;
                }
                else
                {
                    MessageBox.Show("Error In Updation");
                    return;
                }
            }
            else
            {//Merge
                MessageBox.Show("Please contact Admin To Merge duplicate Bidder");
                return;
            }
        }
        else
        {//Merge
            MessageBox.Show("Not Allowed");
            return;
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (divEditMode.Visible || Session["UserType"].ToString() == "1")
        {
            GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
            hf_JalPrahariBidderInfo_Id_Merged.Value = "";
            hf_JalPrahariBidderInfo_Id.Value = gr.Cells[0].Text.Trim().Replace("&nbsp;", "");
            txtGSTINU.Text = gr.Cells[5].Text.Trim().Replace("&nbsp;", "");
            txtFirmnameU.Text = gr.Cells[3].Text.Trim().Replace("&nbsp;", "");
            txtPANNoU.Text = gr.Cells[4].Text.Trim().Replace("&nbsp;", "");
            mpProjects.Show();
        }
        else
        {//Merge
            MessageBox.Show("Not Allowed");
            return;
        }
    }

    protected void lnkParticipated_Click(object sender, EventArgs e)
    {

        int JalPrahariBidderInfo_Id = 0;

        if (divEditMode.Visible || Session["UserType"].ToString() == "1")
        {
            GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
            try
            {
                JalPrahariBidderInfo_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
            }
            catch
            {
                JalPrahariBidderInfo_Id = 0;
            }
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;

            try
            {
                Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
            }
            catch
            {
                Circle_Id = 0;
            }
            try
            {
                Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
            }
            catch
            {
                Division_Id = 0;
            }
            DataSet ds = new DataSet();
            ds = new DataLayer().get_JalPrahariBidderDPR_Linked(JalPrahariBidderInfo_Id, Zone_Id, Circle_Id, Division_Id);
            if (AllClasses.CheckDataSet(ds))
            {
                grdDPR.DataSource = ds.Tables[0];
                grdDPR.DataBind();
                mpDPRLink.Show();
            }
            else
            {
                MessageBox.Show("Data not Found");
                return;
            }
        }
        else
        {//Merge
            MessageBox.Show("Not Allowed");
            return;
        }
    }

    protected void grdDPR_PreRender(object sender, EventArgs e)
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

    protected void btnUpdateDPRLink_Click(object sender, EventArgs e)
    {
        if (grdDPR.Rows.Count > 0)
        {
            int JalPrahariBidderInfo_Id = 0;
            try
            {
                JalPrahariBidderInfo_Id = Convert.ToInt32(grdDPR.Rows[0].Cells[0].Text.Trim());
            }
            catch
            {
                JalPrahariBidderInfo_Id = 0;
            }
            string DPR_Id_In = "0";
            CheckBox chkRemove;
            for (int i = 0; i < grdDPR.Rows.Count; i++)
            {
                chkRemove = grdDPR.Rows[i].FindControl("chkRemove") as CheckBox;
                if (!chkRemove.Checked)
                {
                    DPR_Id_In += ", " + grdDPR.Rows[i].Cells[1].Text.Trim();
                }
            }
            if (JalPrahariBidderInfo_Id > 0 && DPR_Id_In != "0")
            {
                if (new DataLayer().Update_JalPrahariBidderDPR_Linking(JalPrahariBidderInfo_Id, DPR_Id_In, "", false))
                {
                    MessageBox.Show("Linking Updated Successfully");
                    get_Bidders_Master_Data();
                    return;
                }
                else
                {
                    MessageBox.Show("Unable To Update Linking");
                    mpDPRLink.Show();
                    return;
                }
            }
        }
    }

    protected void btnEditMode_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(ViewState["Edit_Mode"].ToString()) == false)
        {
            ViewState["Edit_Mode"] = true;
        }
        else
        {
            ViewState["Edit_Mode"] = false;
        }
        if (Convert.ToBoolean(ViewState["Edit_Mode"].ToString()) == false)
        {
            divEditMode.Visible = false;
        }
        else
        {
            if (ddlDivision.Enabled == false && ddlDivision.Items.Count > 0 && ddlDivision.SelectedValue != "0")
            {
                divEditMode.Visible = true;
            }
            else
            {
                divEditMode.Visible = false;
            }
        }
        get_Bidders_Master_Data();
    }

    protected void btnAddLinking_Click(object sender, EventArgs e)
    {
        if (txtAddDPRCode.Text == "")
        {
            MessageBox.Show("Please Add DPR Code");
            txtAddDPRCode.Focus();
            mpDPRLink.Show();
            return;
        }
        if (grdDPR.Rows.Count > 0)
        {
            int JalPrahariBidderInfo_Id = 0;
            try
            {
                JalPrahariBidderInfo_Id = Convert.ToInt32(grdDPR.Rows[0].Cells[0].Text.Trim());
            }
            catch
            {
                JalPrahariBidderInfo_Id = 0;
            }

            if (JalPrahariBidderInfo_Id > 0)
            {
                if (new DataLayer().Update_JalPrahariBidderDPR_Linking(JalPrahariBidderInfo_Id, "", txtAddDPRCode.Text.Trim(), chkIsJV.Checked))
                {
                    MessageBox.Show("Linking Updated Successfully");
                    get_Bidders_Master_Data();
                    return;
                }
                else
                {
                    MessageBox.Show("Unable To Update Linking");
                    mpDPRLink.Show();
                    return;
                }
            }
        }
    }
}