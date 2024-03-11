using Aspose.Pdf.Operators;
using NPOI.OpenXmlFormats;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Jal_Prahari_Vendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            get_tbl_ProjectDPR_Bidder_Order();
            if (Session["Bidders_Info"] != null)
            {
                Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
                divVendorName.InnerHtml = obj_Bidders_Info.Bidders_FirmName;
                divPAN.InnerHtml = "PAN:" + obj_Bidders_Info.Bidders_PAN;
                divGSTIN.InnerHtml = "GSTIN: " + obj_Bidders_Info.Bidders_GSTIN;
                get_Bidders_Data(obj_Bidders_Info);
                get_JalPrahariBidderDPR_Linked(obj_Bidders_Info);
            }
            else
            {
                Response.Redirect("Jal_Prahari.aspx");
            }
        }
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

    private void get_JalPrahariBidderDPR_Linked(Bidders_Info obj_Bidders_Info)
    {
        DataSet ds = new DataSet();
        ds = new DataLayer().get_JalPrahariBidderDPR_Linked(obj_Bidders_Info.JalPrahariBidderInfo_Id, obj_Bidders_Info.Zone_Id, obj_Bidders_Info.Circle_Id, obj_Bidders_Info.Division_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["DPR_Linked"] = ds.Tables[0];
            AllClasses.FillDropDown(ds.Tables[0], ddlDPRListSearch, "Data_Bind", "ProjectDPR_Id");
        }
        else
        {
            ViewState["DPR_Linked"] = null;
            ddlDPRListSearch.Items.Clear();
        }
    }

    private void get_Bidders_Data(Bidders_Info obj_Bidders_Info)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Data(obj_Bidders_Info);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkLeadBidder.Text = ds.Tables[0].Rows[0]["Participated_As_Lead"].ToString();
            lnkPartnerBidder.Text = ds.Tables[0].Rows[0]["Participated_As_JV"].ToString();
            lnkTechnical.Text = ds.Tables[0].Rows[0]["Technically_Qualified"].ToString();
            lnkFinancial.Text = ds.Tables[0].Rows[0]["Financially_Qualified"].ToString();
        }
        else
        {
            lnkLeadBidder.Text = "0";
            lnkPartnerBidder.Text = "0";
            lnkTechnical.Text = "0";
            lnkFinancial.Text = "0";
        }
    }
    private void get_Bidders_Participated_Projects(Bidders_Info obj_Bidders_Info, string Mode)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Participated_Projects(obj_Bidders_Info, Mode);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdProjectsParticipated.DataSource = ds.Tables[0];
            grdProjectsParticipated.DataBind();
            mpProjects.Show();
        }
        else
        {
            MessageBox.Show("No records Found");
            grdProjectsParticipated.DataSource = null;
            grdProjectsParticipated.DataBind();
        }
    }
    protected void lnkLeadBidder_Click(object sender, EventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        get_Bidders_Participated_Projects(obj_Bidders_Info, "Lead");
    }

    protected void lnkPartnerBidder_Click(object sender, EventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        get_Bidders_Participated_Projects(obj_Bidders_Info, "JV");
    }

    protected void lnkTechnical_Click(object sender, EventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        get_Bidders_Participated_Projects(obj_Bidders_Info, "TQ");
    }

    protected void lnkFinancial_Click(object sender, EventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        get_Bidders_Participated_Projects(obj_Bidders_Info, "FQ");
    }
    private void get_Bidders_Work_Order_Repos(Bidders_Info obj_Bidders_Info, int DPR_Id)
    {
        divWorkOrderAdd.Visible = true;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Work_Order_Repos(obj_Bidders_Info, DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divWorkOrder.Visible = true;
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            divWorkOrder.Visible = false;
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }
    private void get_Bidders_Net_Worth_Repos(Bidders_Info obj_Bidders_Info, int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Net_Worth_Repos(obj_Bidders_Info, DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divNetWorth.Visible = true;
            grdNetWorth.DataSource = ds.Tables[0];
            grdNetWorth.DataBind();
        }
        else
        {
            divNetWorth.Visible = false;
            grdNetWorth.DataSource = null;
            grdNetWorth.DataBind();
        }
    }

    private void get_Bidders_Solvency_Repos(Bidders_Info obj_Bidders_Info, int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Solvency_Repos(obj_Bidders_Info, DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divNetWorth.Visible = true;
            grdNetWorth.DataSource = ds.Tables[0];
            grdNetWorth.DataBind();
        }
        else
        {
            divNetWorth.Visible = false;
            grdNetWorth.DataSource = null;
            grdNetWorth.DataBind();
        }
    }

    private void get_Bidders_Balance_Sheet_Repos(Bidders_Info obj_Bidders_Info, int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Balance_Sheet_Repos(obj_Bidders_Info, DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divNetWorth.Visible = true;
            grdNetWorth.DataSource = ds.Tables[0];
            grdNetWorth.DataBind();
        }
        else
        {
            divNetWorth.Visible = false;
            grdNetWorth.DataSource = null;
            grdNetWorth.DataBind();
        }
    }

    private void get_Bidders_ITR_Repos(Bidders_Info obj_Bidders_Info, int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_ITR_Repos(obj_Bidders_Info, DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divNetWorth.Visible = true;
            grdNetWorth.DataSource = ds.Tables[0];
            grdNetWorth.DataBind();
        }
        else
        {
            divNetWorth.Visible = false;
            grdNetWorth.DataSource = null;
            grdNetWorth.DataBind();
        }
    }

    private void get_Bidders_Bid_Capacity_Repos(Bidders_Info obj_Bidders_Info, int DPR_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Bidders_Bid_Capacity_Repos(obj_Bidders_Info, DPR_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divNetWorth.Visible = true;
            grdNetWorth.DataSource = ds.Tables[0];
            grdNetWorth.DataBind();
        }
        else
        {
            divNetWorth.Visible = false;
            grdNetWorth.DataSource = null;
            grdNetWorth.DataBind();
        }
    }

    private void reset_Document_Layout()
    {
        divWorkOrderAdd.Visible = false;
        divWorkOrder.Visible = false;
        grdPost.DataSource = null;
        grdPost.DataBind();
        divNetWorth.Visible = false;
        grdNetWorth.DataSource = null;
        grdNetWorth.DataBind();
    }
    protected void btnWorkOrder_Click(object sender, ImageClickEventArgs e)
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
        }
        catch
        {
            DPR_Id = 0;
        }
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        reset_Document_Layout();
        get_Bidders_Work_Order_Repos(obj_Bidders_Info, DPR_Id);
    }

    protected void btnBalanceSheet_Click(object sender, ImageClickEventArgs e)
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
        }
        catch
        {
            DPR_Id = 0;
        }
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        reset_Document_Layout();
        get_Bidders_Balance_Sheet_Repos(obj_Bidders_Info, DPR_Id);
    }

    protected void btnITR_Click(object sender, ImageClickEventArgs e)
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
        }
        catch
        {
            DPR_Id = 0;
        }
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        reset_Document_Layout();
        get_Bidders_ITR_Repos(obj_Bidders_Info, DPR_Id);
    }

    protected void btnNetWorth_Click(object sender, ImageClickEventArgs e)
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
        }
        catch
        {
            DPR_Id = 0;
        }
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        reset_Document_Layout();
        get_Bidders_Net_Worth_Repos(obj_Bidders_Info, DPR_Id);
    }

    protected void btnSolvency_Click(object sender, ImageClickEventArgs e)
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
        }
        catch
        {
            DPR_Id = 0;
        }
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        reset_Document_Layout();
        get_Bidders_Solvency_Repos(obj_Bidders_Info, DPR_Id);
    }

    protected void btnBidCapacity_Click(object sender, ImageClickEventArgs e)
    {
        int DPR_Id = 0;
        try
        {
            DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
        }
        catch
        {
            DPR_Id = 0;
        }
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        reset_Document_Layout();
        get_Bidders_Bid_Capacity_Repos(obj_Bidders_Info, DPR_Id);
    }

    protected void grdNetWorth_PreRender(object sender, EventArgs e)
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

    protected void grdProjectsParticipated_PreRender(object sender, EventArgs e)
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

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int JalPrahariBidderDoc_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_tbl_JalPrahariBidderDoc(JalPrahariBidderDoc_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion Document!!");
            return;
        }
    }

    protected void btnDeleteWorkOrder_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int JalPrahariBidder_Order_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_tbl_JalPrahariBidder_Order(JalPrahariBidder_Order_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            int DPR_Id = 0;
            try
            {
                DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
            }
            catch
            {
                DPR_Id = 0;
            }
            Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
            reset_Document_Layout();
            get_Bidders_Work_Order_Repos(obj_Bidders_Info, DPR_Id);
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion Order!!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectDPR_Bidder_Order_OrderPath = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            string ProjectDPR_Bidder_Order_VerificationPath = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            string ProjectDPR_Bidder_Order_VerificationLetterPath = e.Row.Cells[3].Text.Trim().Replace("&nbsp;", "");

            if (ProjectDPR_Bidder_Order_OrderPath == "")
            {
                e.Row.Cells[16].BackColor = Color.LightPink;
            }

            if (ProjectDPR_Bidder_Order_VerificationPath == "")
            {
                e.Row.Cells[17].BackColor = Color.LightPink;
            }

            if (ProjectDPR_Bidder_Order_VerificationLetterPath == "")
            {
                e.Row.Cells[18].BackColor = Color.LightPink;
            }
        }
    }

    protected void lnkReplaceWO_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int JalPrahariBidder_Order_Id = Convert.ToInt32(((sender as LinkButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_JalPrahariBidder_Order_Id.Value = JalPrahariBidder_Order_Id.ToString();
        hf_FileType.Value = "Order";
        mpReplace.Show();
    }

    protected void lnkReplaceVer_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int JalPrahariBidder_Order_Id = Convert.ToInt32(((sender as LinkButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_JalPrahariBidder_Order_Id.Value = JalPrahariBidder_Order_Id.ToString();
        hf_FileType.Value = "Verification";
        mpReplace.Show();
    }

    protected void lnkReplaceVerLetter_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int JalPrahariBidder_Order_Id = Convert.ToInt32(((sender as LinkButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_JalPrahariBidder_Order_Id.Value = JalPrahariBidder_Order_Id.ToString();
        hf_FileType.Value = "Letter";
        mpReplace.Show();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!flReplace.HasFile)
        {
            MessageBox.Show("Please Upload File");
            return;
        }

        string[] FileName = flReplace.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        if (hf_FileType.Value == "Doc")
        {
            int JalPrahariBidderDoc_Id = Convert.ToInt32(hf_JalPrahariBidderDoc_Id.Value);
            if (new DataLayer().Replace_tbl_JalPrahariBidderDoc(JalPrahariBidderDoc_Id, Person_Id, flReplace.FileBytes, FileName[FileName.Length - 1]))
            {
                MessageBox.Show("Uploaded Successfully!!");
                return;
            }
            else
            {
                MessageBox.Show("Error In Replacing!!");
                return;
            }
        }
        else
        {
            int JalPrahariBidder_Order_Id = Convert.ToInt32(hf_JalPrahariBidder_Order_Id.Value);
            if (new DataLayer().Replace_tbl_JalPrahariBidder_Order_Doc(JalPrahariBidder_Order_Id, Person_Id, flReplace.FileBytes, FileName[FileName.Length - 1], hf_FileType.Value))
            {
                MessageBox.Show("Uploaded Successfully!!");
                return;
            }
            else
            {
                MessageBox.Show("Error In Replacing!!");
                return;
            }
        }


    }

    protected void lnkReplace_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int JalPrahariBidderDoc_Id = Convert.ToInt32(((sender as LinkButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_JalPrahariBidderDoc_Id.Value = JalPrahariBidderDoc_Id.ToString();
        hf_FileType.Value = "Doc";
        mpReplace.Show();
    }

    protected void grdBidderOrder_PreRender(object sender, EventArgs e)
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
    protected bool checkDate(DateTime dtFYStart, DateTime dtFYEnd, DateTime dtOrderDate)
    {
        return dtOrderDate >= dtFYStart && dtOrderDate < dtFYEnd;
    }
    protected void Calculate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;
        TextBox txtEndDate = (gr.FindControl("txtEndDate") as TextBox);
        TextBox txtOrderAmount = (gr.FindControl("txtOrderAmount") as TextBox);
        TextBox txtInflation = (gr.FindControl("txtInflation") as TextBox);
        TextBox txtValueAfterInflation = (gr.FindControl("txtValueAfterInflation") as TextBox);
        TextBox txtJVShare = (gr.FindControl("txtJVShare") as TextBox);
        TextBox txtJVContractValue = (gr.FindControl("txtJVContractValue") as TextBox);

        DateTime dtOrderDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dtFYStart = new DateTime(2023, 4, 1);
        DateTime dtFYEnd = new DateTime(2024, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "0";
        }
        dtFYStart = new DateTime(2022, 4, 1);
        dtFYEnd = new DateTime(2023, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1";
        }
        dtFYStart = new DateTime(2021, 4, 1);
        dtFYEnd = new DateTime(2022, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.07";
        }
        dtFYStart = new DateTime(2020, 4, 1);
        dtFYEnd = new DateTime(2021, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.14";
        }
        dtFYStart = new DateTime(2019, 4, 1);
        dtFYEnd = new DateTime(2020, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.21";
        }
        dtFYStart = new DateTime(2018, 4, 1);
        dtFYEnd = new DateTime(2019, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.28";
        }
        dtFYStart = new DateTime(2017, 4, 1);
        dtFYEnd = new DateTime(2018, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.35";
        }
        dtFYStart = new DateTime(2016, 4, 1);
        dtFYEnd = new DateTime(2017, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.42";
        }
        dtFYStart = new DateTime(2015, 4, 1);
        dtFYEnd = new DateTime(2016, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.49";
        }
        dtFYStart = new DateTime(2014, 4, 1);
        dtFYEnd = new DateTime(2015, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.56";
        }
        dtFYStart = new DateTime(2013, 4, 1);
        dtFYEnd = new DateTime(2014, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.63";
        }
        dtFYStart = new DateTime(2012, 4, 1);
        dtFYEnd = new DateTime(2013, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.70";
        }
        dtFYStart = new DateTime(2011, 4, 1);
        dtFYEnd = new DateTime(2012, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.77";
        }
        dtFYStart = new DateTime(2010, 4, 1);
        dtFYEnd = new DateTime(2011, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.84";
        }
        dtFYStart = new DateTime(2009, 4, 1);
        dtFYEnd = new DateTime(2010, 3, 31);
        if (checkDate(dtFYStart, dtFYEnd, dtOrderDate))
        {
            txtInflation.Text = "1.91";
        }

        decimal OrderAmount = 0;
        try
        {
            OrderAmount = decimal.Parse(txtOrderAmount.Text.Trim());
        }
        catch
        {
            OrderAmount = 0;
        }

        decimal JVShare = 0;
        try
        {
            JVShare = decimal.Parse(txtJVShare.Text.Trim());
        }
        catch
        {
            JVShare = 100;
        }
        decimal JVContractValue = decimal.Round(OrderAmount * JVShare / 100, 2, MidpointRounding.AwayFromZero);
        txtJVContractValue.Text = JVContractValue.ToString();

        decimal Inflation = 0;
        try
        {
            Inflation = decimal.Parse(txtInflation.Text.Trim());
        }
        catch
        {
            Inflation = 0;
        }
        decimal ValueAfterInflation = JVContractValue;
        //decimal ValueAfterInflation = decimal.Round(JVContractValue + (JVContractValue * Inflation / 100), 2, MidpointRounding.AwayFromZero);
        if (Inflation > 0)
        {
            ValueAfterInflation = decimal.Round(JVContractValue * Inflation, 2, MidpointRounding.AwayFromZero);
        }
        txtValueAfterInflation.Text = ValueAfterInflation.ToString();
        mpWorkOrder.Show();
    }
    private void get_tbl_ProjectDPR_Bidder_Order()
    {
        DataSet ds = new DataSet();
        DataTable dtBidder_Order = new DataTable();
        DataColumn dc_ProjectDPR_Bidder_Order_Id = new DataColumn("ProjectDPR_Bidder_Order_Id", typeof(int));
        DataColumn dc_ProjectDPR_Bidder_Order_DPR_Id = new DataColumn("ProjectDPR_Bidder_Order_DPR_Id", typeof(int));
        DataColumn dc_ProjectDPR_Bidder_Order_DPRBidder_Id = new DataColumn("ProjectDPR_Bidder_Order_DPRBidder_Id", typeof(int));
        DataColumn dc_ProjectDPR_Bidder_Order_Name_Of_Work = new DataColumn("ProjectDPR_Bidder_Order_Name_Of_Work", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_StartDate = new DataColumn("ProjectDPR_Bidder_Order_StartDate", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_EndDate = new DataColumn("ProjectDPR_Bidder_Order_EndDate", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_Amount = new DataColumn("ProjectDPR_Bidder_Order_Amount", typeof(decimal));
        DataColumn dc_ProjectDPR_Bidder_Order_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Inflation", typeof(decimal));
        DataColumn dc_ProjectDPR_Bidder_Order_Amount_After_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Amount_After_Inflation", typeof(decimal));
        DataColumn dc_ProjectDPR_Bidder_Order_JV_Share = new DataColumn("ProjectDPR_Bidder_Order_JV_Share", typeof(decimal));
        DataColumn dc_ProjectDPR_Bidder_Order_JV_Contract_Value = new DataColumn("ProjectDPR_Bidder_Order_JV_Contract_Value", typeof(decimal));
        DataColumn dc_ProjectDPR_Bidder_Order_Simmilar_Nature = new DataColumn("ProjectDPR_Bidder_Order_Simmilar_Nature", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_Completed = new DataColumn("ProjectDPR_Bidder_Order_Completed", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_Comments = new DataColumn("ProjectDPR_Bidder_Order_Comments", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_BidderType = new DataColumn("ProjectDPR_Bidder_Order_BidderType", typeof(string));

        DataColumn dc_ProjectDPR_Bidder_Order_OrderPath = new DataColumn("ProjectDPR_Bidder_Order_OrderPath", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationPath", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath1", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath2", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate1", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate2", typeof(string));
        DataColumn dc_ProjectDPR_Bidder_Order_ReminderCount = new DataColumn("ProjectDPR_Bidder_Order_ReminderCount", typeof(int));

        dtBidder_Order.Columns.AddRange(new DataColumn[] { dc_ProjectDPR_Bidder_Order_Id, dc_ProjectDPR_Bidder_Order_DPR_Id, dc_ProjectDPR_Bidder_Order_DPRBidder_Id, dc_ProjectDPR_Bidder_Order_Name_Of_Work, dc_ProjectDPR_Bidder_Order_StartDate, dc_ProjectDPR_Bidder_Order_EndDate, dc_ProjectDPR_Bidder_Order_Amount, dc_ProjectDPR_Bidder_Order_Inflation, dc_ProjectDPR_Bidder_Order_Amount_After_Inflation, dc_ProjectDPR_Bidder_Order_JV_Share, dc_ProjectDPR_Bidder_Order_JV_Contract_Value, dc_ProjectDPR_Bidder_Order_Simmilar_Nature, dc_ProjectDPR_Bidder_Order_Completed, dc_ProjectDPR_Bidder_Order_Comments, dc_ProjectDPR_Bidder_Order_BidderType, dc_ProjectDPR_Bidder_Order_OrderPath, dc_ProjectDPR_Bidder_Order_VerificationPath, dc_ProjectDPR_Bidder_Order_VerificationLetterPath, dc_ProjectDPR_Bidder_Order_VerificationLetterPath1, dc_ProjectDPR_Bidder_Order_VerificationLetterPath2, dc_ProjectDPR_Bidder_Order_VerificationLetterDate, dc_ProjectDPR_Bidder_Order_VerificationLetterDate1, dc_ProjectDPR_Bidder_Order_VerificationLetterDate2, dc_ProjectDPR_Bidder_Order_ReminderCount });

        DataRow dr = dtBidder_Order.NewRow();
        dtBidder_Order.Rows.Add(dr);
        ViewState["dtBidder_Order"] = dtBidder_Order;

        grdBidderOrder.DataSource = dtBidder_Order;
        grdBidderOrder.DataBind();
    }
    protected void btnAddWork_Click(object sender, ImageClickEventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        DataTable dtBidder_Order;
        if (ViewState["dtBidder_Order"] != null)
        {
            dtBidder_Order = (DataTable)(ViewState["dtBidder_Order"]);

            if (AllClasses.CheckDt(dtBidder_Order) && dtBidder_Order.Rows.Count == grdBidderOrder.Rows.Count)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    TextBox txtNameOfWork = (grdBidderOrder.Rows[i].FindControl("txtNameOfWork") as TextBox);
                    TextBox txtStartDate = (grdBidderOrder.Rows[i].FindControl("txtStartDate") as TextBox);
                    TextBox txtEndDate = (grdBidderOrder.Rows[i].FindControl("txtEndDate") as TextBox);
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    RadioButtonList ddlCompleted = (grdBidderOrder.Rows[i].FindControl("ddlCompleted") as RadioButtonList);
                    TextBox txtVerificationLetter = (grdBidderOrder.Rows[i].FindControl("txtVerificationLetter") as TextBox);
                    FileUpload flWorkOrder = (grdBidderOrder.Rows[i].FindControl("flWorkOrder") as FileUpload);
                    FileUpload flOrderVerification = (grdBidderOrder.Rows[i].FindControl("flOrderVerification") as FileUpload);
                    FileUpload flVerificationLetter = (grdBidderOrder.Rows[i].FindControl("flVerificationLetter") as FileUpload);
                    RadioButtonList rbtLetterReminder = (grdBidderOrder.Rows[i].FindControl("rbtLetterReminder") as RadioButtonList);

                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPRBidder_Id"] = obj_Bidders_Info.JalPrahariBidderInfo_Id;
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Name_Of_Work"] = txtNameOfWork.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_StartDate"] = txtStartDate.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_EndDate"] = txtEndDate.Text.Trim();
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = Convert.ToDecimal(txtOrderAmount.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = Convert.ToDecimal(txtInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = Convert.ToDecimal(txtJVShare.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = 100;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = Convert.ToDecimal(txtJVContractValue.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = ddlSimmilarNature.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = "Yes";
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = ddlCompleted.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = "Yes";
                    }
                    int ReminderCount = 0;
                    if (rbtLetterReminder.SelectedValue == "1")
                    {
                        ReminderCount = 1;
                    }
                    else if (rbtLetterReminder.SelectedValue == "2")
                    {
                        ReminderCount = 2;
                    }
                    else
                    {
                        ReminderCount = 0;
                    }
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_ReminderCount"] = ReminderCount;
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_OrderPath"] = grdBidderOrder.Rows[i].Cells[5].Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationPath"] = grdBidderOrder.Rows[i].Cells[6].Text.Trim();

                    if (ReminderCount == 0)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath"] = grdBidderOrder.Rows[i].Cells[7].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate"] = txtVerificationLetter.Text.Trim();
                    }
                    else if (ReminderCount == 1)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath1"] = grdBidderOrder.Rows[i].Cells[9].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate1"] = txtVerificationLetter.Text.Trim();
                    }
                    else
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath2"] = grdBidderOrder.Rows[i].Cells[10].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate2"] = txtVerificationLetter.Text.Trim();
                    }
                }
            }


            DataRow dr = dtBidder_Order.NewRow();
            dr["ProjectDPR_Bidder_Order_DPRBidder_Id"] = Convert.ToInt32(dtBidder_Order.Rows[0]["ProjectDPR_Bidder_Order_DPRBidder_Id"].ToString());
            dtBidder_Order.Rows.Add(dr);
            ViewState["dtBidder_Order"] = dtBidder_Order;

            grdBidderOrder.DataSource = dtBidder_Order;
            grdBidderOrder.DataBind();
        }
        else
        {
            dtBidder_Order = new DataTable();
            DataColumn dc_ProjectDPR_Bidder_Order_Id = new DataColumn("ProjectDPR_Bidder_Order_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_DPR_Id = new DataColumn("ProjectDPR_Bidder_Order_DPR_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_DPRBidder_Id = new DataColumn("ProjectDPR_Bidder_Order_DPRBidder_Id", typeof(int));
            DataColumn dc_ProjectDPR_Bidder_Order_Name_Of_Work = new DataColumn("ProjectDPR_Bidder_Order_Name_Of_Work", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_StartDate = new DataColumn("ProjectDPR_Bidder_Order_StartDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_EndDate = new DataColumn("ProjectDPR_Bidder_Order_EndDate", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Amount = new DataColumn("ProjectDPR_Bidder_Order_Amount", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Inflation", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Amount_After_Inflation = new DataColumn("ProjectDPR_Bidder_Order_Amount_After_Inflation", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_JV_Share = new DataColumn("ProjectDPR_Bidder_Order_JV_Share", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_JV_Contract_Value = new DataColumn("ProjectDPR_Bidder_Order_JV_Contract_Value", typeof(decimal));
            DataColumn dc_ProjectDPR_Bidder_Order_Simmilar_Nature = new DataColumn("ProjectDPR_Bidder_Order_Simmilar_Nature", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Completed = new DataColumn("ProjectDPR_Bidder_Order_Completed", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_Comments = new DataColumn("ProjectDPR_Bidder_Order_Comments", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_BidderType = new DataColumn("ProjectDPR_Bidder_Order_BidderType", typeof(string));

            DataColumn dc_ProjectDPR_Bidder_Order_OrderPath = new DataColumn("ProjectDPR_Bidder_Order_OrderPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate", typeof(string));

            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath1", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate1 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate1", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterPath2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterPath2", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_VerificationLetterDate2 = new DataColumn("ProjectDPR_Bidder_Order_VerificationLetterDate2", typeof(string));
            DataColumn dc_ProjectDPR_Bidder_Order_ReminderCount = new DataColumn("ProjectDPR_Bidder_Order_ReminderCount", typeof(string));


            dtBidder_Order.Columns.AddRange(new DataColumn[] { dc_ProjectDPR_Bidder_Order_Id, dc_ProjectDPR_Bidder_Order_DPR_Id, dc_ProjectDPR_Bidder_Order_DPRBidder_Id, dc_ProjectDPR_Bidder_Order_Name_Of_Work, dc_ProjectDPR_Bidder_Order_StartDate, dc_ProjectDPR_Bidder_Order_EndDate, dc_ProjectDPR_Bidder_Order_Amount, dc_ProjectDPR_Bidder_Order_Inflation, dc_ProjectDPR_Bidder_Order_Amount_After_Inflation, dc_ProjectDPR_Bidder_Order_JV_Share, dc_ProjectDPR_Bidder_Order_JV_Contract_Value, dc_ProjectDPR_Bidder_Order_Simmilar_Nature, dc_ProjectDPR_Bidder_Order_Completed, dc_ProjectDPR_Bidder_Order_Comments, dc_ProjectDPR_Bidder_Order_BidderType, dc_ProjectDPR_Bidder_Order_OrderPath, dc_ProjectDPR_Bidder_Order_VerificationPath, dc_ProjectDPR_Bidder_Order_VerificationLetterPath, dc_ProjectDPR_Bidder_Order_VerificationLetterDate, dc_ProjectDPR_Bidder_Order_VerificationLetterPath1, dc_ProjectDPR_Bidder_Order_VerificationLetterDate1, dc_ProjectDPR_Bidder_Order_VerificationLetterPath2, dc_ProjectDPR_Bidder_Order_VerificationLetterDate2, dc_ProjectDPR_Bidder_Order_ReminderCount });

            DataRow dr = dtBidder_Order.NewRow();
            dtBidder_Order.Rows.Add(dr);
            ViewState["dtBidder_Order"] = dtBidder_Order;

            grdBidderOrder.DataSource = dtBidder_Order;
            grdBidderOrder.DataBind();
        }
        mpWorkOrder.Show();
    }

    protected void btnMinusWork_Click(object sender, ImageClickEventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtBidder_Order"] != null)
        {
            DataTable dtBidder_Order = (DataTable)ViewState["dtBidder_Order"];
            if (AllClasses.CheckDt(dtBidder_Order) && dtBidder_Order.Rows.Count == grdBidderOrder.Rows.Count)
            {
                for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
                {
                    TextBox txtNameOfWork = (grdBidderOrder.Rows[i].FindControl("txtNameOfWork") as TextBox);
                    TextBox txtStartDate = (grdBidderOrder.Rows[i].FindControl("txtStartDate") as TextBox);
                    TextBox txtEndDate = (grdBidderOrder.Rows[i].FindControl("txtEndDate") as TextBox);
                    TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
                    TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
                    TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
                    TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
                    TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
                    RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
                    RadioButtonList ddlCompleted = (grdBidderOrder.Rows[i].FindControl("ddlCompleted") as RadioButtonList);
                    TextBox txtVerificationLetter = (grdBidderOrder.Rows[i].FindControl("txtVerificationLetter") as TextBox);
                    FileUpload flWorkOrder = (grdBidderOrder.Rows[i].FindControl("flWorkOrder") as FileUpload);
                    FileUpload flOrderVerification = (grdBidderOrder.Rows[i].FindControl("flOrderVerification") as FileUpload);
                    FileUpload flVerificationLetter = (grdBidderOrder.Rows[i].FindControl("flVerificationLetter") as FileUpload);
                    RadioButtonList rbtLetterReminder = (grdBidderOrder.Rows[i].FindControl("rbtLetterReminder") as RadioButtonList);

                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_DPRBidder_Id"] = obj_Bidders_Info.JalPrahariBidderInfo_Id;
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Name_Of_Work"] = txtNameOfWork.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_StartDate"] = txtStartDate.Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_EndDate"] = txtEndDate.Text.Trim();
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = Convert.ToDecimal(txtOrderAmount.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = Convert.ToDecimal(txtInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Amount_After_Inflation"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = Convert.ToDecimal(txtJVShare.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Share"] = 100;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = Convert.ToDecimal(txtJVContractValue.Text.Trim());
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_JV_Contract_Value"] = 0;
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = ddlSimmilarNature.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Simmilar_Nature"] = "Yes";
                    }
                    try
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = ddlCompleted.SelectedValue;
                    }
                    catch
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_Completed"] = "Yes";
                    }
                    int ReminderCount = 0;
                    if (rbtLetterReminder.SelectedValue == "1")
                    {
                        ReminderCount = 1;
                    }
                    else if (rbtLetterReminder.SelectedValue == "2")
                    {
                        ReminderCount = 2;
                    }
                    else
                    {
                        ReminderCount = 0;
                    }
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_ReminderCount"] = ReminderCount;
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_OrderPath"] = grdBidderOrder.Rows[i].Cells[5].Text.Trim();
                    dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationPath"] = grdBidderOrder.Rows[i].Cells[6].Text.Trim();

                    if (ReminderCount == 0)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath"] = grdBidderOrder.Rows[i].Cells[7].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate"] = txtVerificationLetter.Text.Trim();
                    }
                    else if (ReminderCount == 1)
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath1"] = grdBidderOrder.Rows[i].Cells[9].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate1"] = txtVerificationLetter.Text.Trim();
                    }
                    else
                    {
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterPath2"] = grdBidderOrder.Rows[i].Cells[10].Text.Trim();
                        dtBidder_Order.Rows[i]["ProjectDPR_Bidder_Order_VerificationLetterDate2"] = txtVerificationLetter.Text.Trim();
                    }
                }
            }
            if (dtBidder_Order.Rows.Count > 1)
            {
                dtBidder_Order.Rows.RemoveAt(dtBidder_Order.Rows.Count - 1);
                grdBidderOrder.DataSource = dtBidder_Order;
                grdBidderOrder.DataBind();
                ViewState["dtBidder_Order"] = dtBidder_Order;
            }
        }
        mpWorkOrder.Show();
    }

    protected void btnSaveBidderOrder_Click(object sender, EventArgs e)
    {
        Bidders_Info obj_Bidders_Info = (Bidders_Info)Session["Bidders_Info"];
        List<tbl_ProjectDPR_Bidder_Order> obj_tbl_ProjectDPR_Bidder_Order_Li = new List<tbl_ProjectDPR_Bidder_Order>();
        for (int i = 0; i < grdBidderOrder.Rows.Count; i++)
        {
            tbl_ProjectDPR_Bidder_Order obj_tbl_ProjectDPR_Bidder_Order = new tbl_ProjectDPR_Bidder_Order();
            TextBox txtNameOfWork = (grdBidderOrder.Rows[i].FindControl("txtNameOfWork") as TextBox);
            TextBox txtStartDate = (grdBidderOrder.Rows[i].FindControl("txtStartDate") as TextBox);
            TextBox txtEndDate = (grdBidderOrder.Rows[i].FindControl("txtEndDate") as TextBox);
            TextBox txtOrderAmount = (grdBidderOrder.Rows[i].FindControl("txtOrderAmount") as TextBox);
            TextBox txtInflation = (grdBidderOrder.Rows[i].FindControl("txtInflation") as TextBox);
            TextBox txtValueAfterInflation = (grdBidderOrder.Rows[i].FindControl("txtValueAfterInflation") as TextBox);
            TextBox txtJVShare = (grdBidderOrder.Rows[i].FindControl("txtJVShare") as TextBox);
            TextBox txtJVContractValue = (grdBidderOrder.Rows[i].FindControl("txtJVContractValue") as TextBox);
            RadioButtonList ddlSimmilarNature = (grdBidderOrder.Rows[i].FindControl("ddlSimmilarNature") as RadioButtonList);
            RadioButtonList ddlCompleted = (grdBidderOrder.Rows[i].FindControl("ddlCompleted") as RadioButtonList);
            TextBox txtVerificationLetter = (grdBidderOrder.Rows[i].FindControl("txtVerificationLetter") as TextBox);
            FileUpload flWorkOrder = (grdBidderOrder.Rows[i].FindControl("flWorkOrder") as FileUpload);
            FileUpload flOrderVerification = (grdBidderOrder.Rows[i].FindControl("flOrderVerification") as FileUpload);
            FileUpload flVerificationLetter = (grdBidderOrder.Rows[i].FindControl("flVerificationLetter") as FileUpload);
            RadioButtonList rbtLetterReminder = (grdBidderOrder.Rows[i].FindControl("rbtLetterReminder") as RadioButtonList);
            DropDownList ddlDPRList = (grdBidderOrder.Rows[i].FindControl("ddlDPRList") as DropDownList);

            if (txtNameOfWork.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Work Name");
                txtNameOfWork.Focus();
                return;
            }
            if (txtStartDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Date of Start");
                txtStartDate.Focus();
                return;
            }
            if (txtEndDate.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Actual Date of Completion");
                txtEndDate.Focus();
                return;
            }
            if (txtOrderAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Actual Amount of work Done");
                txtOrderAmount.Focus();
                return;
            }
            if (txtVerificationLetter.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill work Order Verificarion Letter Date");
                txtVerificationLetter.Focus();
                return;
            }
            if (ddlDPRList.SelectedValue == "0")
            {
                MessageBox.Show("Please Select A DPR");
                ddlDPRList.Focus();
                return;
            }
            if (!flWorkOrder.HasFile)
            {
                MessageBox.Show("Please Upload work Order Copy");
                flWorkOrder.Focus();
                return;
            }
            if (!flVerificationLetter.HasFile)
            {
                MessageBox.Show("Please Upload work Order Verification Letter Copy");
                flVerificationLetter.Focus();
                return;
            }
            if (!flOrderVerification.HasFile)
            {
                MessageBox.Show("Please Upload work Order Verification Copy");
                flOrderVerification.Focus();
                return;
            }
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Status = 1;
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_DPRBidder_Id = obj_Bidders_Info.JalPrahariBidderInfo_Id;
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Name_Of_Work = txtNameOfWork.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_StartDate = txtStartDate.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_EndDate = txtEndDate.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_DPR_Id = Convert.ToInt32(ddlDPRList.SelectedValue);
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount = Convert.ToDecimal(txtOrderAmount.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Inflation = Convert.ToDecimal(txtInflation.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Inflation = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount_After_Inflation = Convert.ToDecimal(txtValueAfterInflation.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Amount_After_Inflation = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Share = Convert.ToDecimal(txtJVShare.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Share = 0;
            }
            try
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Contract_Value = Convert.ToDecimal(txtJVContractValue.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_JV_Contract_Value = 0;
            }
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Simmilar_Nature = ddlSimmilarNature.Text.Trim();
            obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_Completed = ddlCompleted.Text.Trim();
            if (rbtLetterReminder.SelectedValue == "1")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_ReminderCount = 1;
            }
            else if (rbtLetterReminder.SelectedValue == "2")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_ReminderCount = 2;
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_ReminderCount = 0;
            }
            if (rbtLetterReminder.SelectedValue == "1")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterDate1 = txtVerificationLetter.Text.Trim();
            }
            else if (rbtLetterReminder.SelectedValue == "2")
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterDate2 = txtVerificationLetter.Text.Trim();
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterDate = txtVerificationLetter.Text.Trim();
            }
            if (flWorkOrder.HasFile)
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_OrderBytes = flWorkOrder.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_OrderPath = grdBidderOrder.Rows[i].Cells[5].Text.Replace("&nbsp;", "").Trim();
            }
            if (flOrderVerification.HasFile)
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationByts = flOrderVerification.FileBytes;
            }
            else
            {
                obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationPath = grdBidderOrder.Rows[i].Cells[6].Text.Replace("&nbsp;", "").Trim();
            }
            if (rbtLetterReminder.SelectedValue == "1")
            {
                if (flVerificationLetter.HasFile)
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterByts1 = flVerificationLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterPath1 = grdBidderOrder.Rows[i].Cells[9].Text.Replace("&nbsp;", "").Trim();
                }
            }
            if (rbtLetterReminder.SelectedValue == "2")
            {
                if (flVerificationLetter.HasFile)
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterByts2 = flVerificationLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterPath2 = grdBidderOrder.Rows[i].Cells[10].Text.Replace("&nbsp;", "").Trim();
                }
            }
            else
            {
                if (flVerificationLetter.HasFile)
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterByts = flVerificationLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectDPR_Bidder_Order.ProjectDPR_Bidder_Order_VerificationLetterPath = grdBidderOrder.Rows[i].Cells[7].Text.Replace("&nbsp;", "").Trim();
                }
            }
            obj_tbl_ProjectDPR_Bidder_Order_Li.Add(obj_tbl_ProjectDPR_Bidder_Order);
        }
        if (new DataLayer().Insert_tbl_JalPrahariBidder_Order(obj_tbl_ProjectDPR_Bidder_Order_Li))
        {
            MessageBox.Show("Work Order Details Saved Successfully");
            int DPR_Id = 0;
            try
            {
                DPR_Id = Convert.ToInt32(ddlDPRListSearch.SelectedValue);
            }
            catch
            {
                DPR_Id = 0;
            }
            reset_Document_Layout();
            get_Bidders_Work_Order_Repos(obj_Bidders_Info, DPR_Id);
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving Work Order Details");
            mpWorkOrder.Show();
            return;
        }
    }

    protected void btnAddWorkOrder_Click(object sender, EventArgs e)
    {
        mpWorkOrder.Show();
    }

    protected void grdBidderOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlDPRList = e.Row.FindControl("ddlDPRList") as DropDownList;
            DataTable dt = (DataTable)(ViewState["DPR_Linked"]);
            if (AllClasses.CheckDt(dt))
            {
                AllClasses.FillDropDown(dt, ddlDPRList, "ProjectDPR_Code", "ProjectDPR_Id");
            }
        }
    }
}