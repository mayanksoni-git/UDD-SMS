using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_PMIS_Mail : System.Web.UI.Page
{
    int Zone_Id = 0;
    int Circle_Id = 0;
    int Division_Id = 0;
    string Scheme_Id = "";
    string Mode = "";
    public string baseURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        baseURL = "http://www.jnupepayment.in" + ResolveUrl("~");
        if (Request.QueryString.Count > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
            }
            catch
            {
                Circle_Id = 0;
            }
            try
            {
                Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
            }
            catch
            {
                Division_Id = 0;
            }
            try
            {
                Mode = Request.QueryString["Mode"].ToString();
            }
            catch
            {
                Mode = "";
            }
            try
            {
                Scheme_Id = Request.QueryString["Scheme_Id"].ToString();
            }
            catch
            {
                Scheme_Id = "";
            }
        }
        if (!IsPostBack)
        {
            if (Mode == "mail" && Scheme_Id != "")
            {
                load_dashboard(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }

    private void load_dashboard(int Zone_Id, int Circle_Id, int Division_Id, string Scheme_Id)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        get_PMIS_Dashboard_View(Zone_Id, Circle_Id, Division_Id, Scheme_Id);
    }
    protected void get_PMIS_Dashboard_View(int Zone_Id, int Circle_Id, int Division_Id, string Scheme_Id)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_View(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkBG.Text = ds.Tables[0].Rows[0]["BG"].ToString();
            lnkPS.Text = ds.Tables[0].Rows[0]["PS"].ToString();
            lnkMA.Text = ds.Tables[0].Rows[0]["MA"].ToString();
            lnkCB.Text = ds.Tables[0].Rows[0]["Agreement"].ToString();
            lnkTotalProjects.Text = ds.Tables[0].Rows[0]["Total_Projects"].ToString();
            lnkWater.Text = ds.Tables[0].Rows[0]["Water_Supply"].ToString();
            lnkDranage.Text = ds.Tables[0].Rows[0]["Drainage"].ToString();
            lnkSolidWaste.Text = ds.Tables[0].Rows[0]["Solid_Waste"].ToString();
            lnkSeptage.Text = ds.Tables[0].Rows[0]["Septage"].ToString();
            lnkSewarage.Text = ds.Tables[0].Rows[0]["Sewerage"].ToString();
            lnkTotalPgg.Text = ds.Tables[0].Rows[0]["Total_Package"].ToString();
            lnkGO1.Text = ds.Tables[0].Rows[0]["GO_1"].ToString();
            lnkGO2.Text = ds.Tables[0].Rows[0]["GO_2"].ToString();
            lnkGO3.Text = ds.Tables[0].Rows[0]["GO_3"].ToString();
            lnkTarget_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTarget_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkExp_C.Text = ds.Tables[0].Rows[0]["Progress_Current"].ToString();
            lnkExp_P.Text = ds.Tables[0].Rows[0]["Progress_Previous"].ToString();
            lnkCompleted.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoing.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
            lnkLOI.Text = ds.Tables[0].Rows[0]["LOI"].ToString();
            lnkCB_Stamp.Text = ds.Tables[0].Rows[0]["CB_Stamp"].ToString();
            lnkCBFront.Text = ds.Tables[0].Rows[0]["CB_Front"].ToString();
            lnkCB_ScheduleG.Text = ds.Tables[0].Rows[0]["Schedule_G"].ToString();
            lnkTE.Text = ds.Tables[0].Rows[0]["TE"].ToString();
            lnkLD.Text = ds.Tables[0].Rows[0]["LD_Likly"].ToString();
            lnkBondDateDelay.Text = ds.Tables[0].Rows[0]["Delay_Likly1"].ToString();
            lnkBondDateDelayNotExtended.Text = ds.Tables[0].Rows[0]["Delay_Likly2"].ToString();
        }
        else
        {
            lnkCompleted.Text = "0";
            lnkOnGoing.Text = "0";
            lnkBG.Text = "0";
            lnkPS.Text = "0";
            lnkMA.Text = "0";
            lnkCB.Text = "0";
            lnkDranage.Text = "0";
            lnkSolidWaste.Text = "0";
            lnkTotalProjects.Text = "0";
            lnkWater.Text = "0";
            lnkSeptage.Text = "0";
            lnkSewarage.Text = "0";
            lnkTotalPgg.Text = "0";
            lnkGO1.Text = "0";
            lnkGO2.Text = "0";
            lnkGO3.Text = "0";
            lnkTarget_C.Text = "0";
            lnkTarget_N.Text = "0";
            lnkExp_C.Text = "0";
            lnkExp_P.Text = "0";
            lnkTE.Text = "0";
            lnkLOI.Text = "0";
            lnkCB_Stamp.Text = "0";
            lnkCBFront.Text = "0";
            lnkCB_ScheduleG.Text = "0";
            lnkLD.Text = "0";
            lnkBondDateDelay.Text = "0";
            lnkBondDateDelayNotExtended.Text = "0";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_Physical_Update(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPMISUpdation.DataSource = ds.Tables[0];
            grdPMISUpdation.DataBind();
        }
        else
        {
            grdPMISUpdation.DataSource = null;
            grdPMISUpdation.DataBind();
        }

        ds = (new DataLayer()).get_Variation_Document_Uploaded(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkVariationProject.Text = ds.Tables[0].Rows[0]["Total_Projects"].ToString();
            lnkVariationPackage.Text = ds.Tables[0].Rows[0]["Total_Pkg"].ToString();
        }
        else
        {
            lnkVariationProject.Text = "0";
            lnkVariationPackage.Text = "0";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_LD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkLDImposed.Text = ds.Tables[0].Rows[0]["LD_Imposed"].ToString() + " / " + ds.Tables[0].Rows[0]["LD_Amount"].ToString();
            lnkLDWithdrawan.Text = ds.Tables[0].Rows[0]["LD_Withdrawan"].ToString() + " / " + ds.Tables[0].Rows[0]["LD_Withdrawan_Amount"].ToString(); ;
        }
        else
        {
            lnkBondDateDelay.Text = "0 / 0";
            lnkBondDateDelayNotExtended.Text = "0 / 0";
        }

        ds = (new DataLayer()).get_Variation_Document_Upload_Details(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkVariationPending.Text = ds.Tables[0].Rows[0]["Variation_NotUploaded"].ToString();
        }
        else
        {
            lnkVariationPending.Text = "0";
        }
    }

    protected void lnkTotalProjects_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=0");
    }

    protected void lnkWater_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "26";
        if (Scheme_Id == "1013")
        {
            Type_Id = "26";
        }
        if (Scheme_Id == "12")
        {
            Type_Id = "16";
        }
        else
        {
            Type_Id = "26";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id);
    }

    protected void lnkSewarage_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "24";
        if (Scheme_Id == "1013")
        {
            Type_Id = "24";
        }
        if (Scheme_Id == "12")
        {
            Type_Id = "18";
        }
        else
        {
            Type_Id = "24";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id);
    }

    protected void lnkSeptage_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "27";
        if (Scheme_Id == "1013")
        {
            Type_Id = "27";
        }
        if (Scheme_Id == "12")
        {
            Type_Id = "21";
        }
        else
        {
            Type_Id = "27";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id);
    }
    protected void lnkSolidWaste_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "25";
        if (Scheme_Id == "1013")
        {
            Type_Id = "25";
        }
        if (Scheme_Id == "12")
        {
            Type_Id = "25";
        }
        else
        {
            Type_Id = "25";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id);
    }

    protected void lnkDranage_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        string Type_Id = "23";
        if (Scheme_Id == "1013")
        {
            Type_Id = "23";
        }
        if (Scheme_Id == "12")
        {
            Type_Id = "17";
        }
        else
        {
            Type_Id = "23";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id);
    }
    protected void lnkProjects_C_Click(object sender, EventArgs e)
    {

    }

    protected void lnkProjects_N_Click(object sender, EventArgs e)
    {

    }

    protected void lnkGO1_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&GO=1");
    }

    protected void lnkGO2_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&GO=2");
    }

    protected void lnkGO3_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&GO=3");
    }

    protected void lnkTotalPgg_Click(object sender, EventArgs e)
    {

    }

    protected void lnkCB_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=14");
    }

    protected void lnkBG_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=20");
    }

    protected void lnkPS_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=21");
    }

    protected void lnkTarget_C_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        DateTime dt = DateTime.Now;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString());
    }

    protected void lnkTarget_N_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        DateTime dt = DateTime.Now.AddMonths(1);
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString());
    }

    protected void lnkExp_C_Click(object sender, EventArgs e)
    {

    }

    protected void lnkExp_P_Click(object sender, EventArgs e)
    {

    }

    protected void lnkMA_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=22");
    }

    protected void grdPMISUpdation_PreRender(object sender, EventArgs e)
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

    protected void lnkCompleted_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=Completed");
    }

    protected void lnkOnGoing_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=OnGoing");
    }

    protected void lblUpdation2_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProjectStatus = "";
        if (gr.RowIndex == 0)
        {
            ProjectStatus = "Completed";
        }
        else
        {
            ProjectStatus = "OnGoing";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=1&ProjectStatus=" + ProjectStatus);
    }

    protected void lblUpdation6_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProjectStatus = "";
        if (gr.RowIndex == 0)
        {
            ProjectStatus = "Completed";
        }
        else
        {
            ProjectStatus = "OnGoing";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=2&ProjectStatus=" + ProjectStatus);
    }

    protected void lblUpdation15_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProjectStatus = "";
        if (gr.RowIndex == 0)
        {
            ProjectStatus = "Completed";
        }
        else
        {
            ProjectStatus = "OnGoing";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=3&ProjectStatus=" + ProjectStatus);
    }

    protected void lblUpdation30_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProjectStatus = "";
        if (gr.RowIndex == 0)
        {
            ProjectStatus = "Completed";
        }
        else
        {
            ProjectStatus = "OnGoing";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=4&ProjectStatus=" + ProjectStatus);
    }

    protected void lblUpdation60_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProjectStatus = "";
        if (gr.RowIndex == 0)
        {
            ProjectStatus = "Completed";
        }
        else
        {
            ProjectStatus = "OnGoing";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=5&ProjectStatus=" + ProjectStatus);
    }

    protected void lblUpdationMore60_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string ProjectStatus = "";
        if (gr.RowIndex == 0)
        {
            ProjectStatus = "Completed";
        }
        else
        {
            ProjectStatus = "OnGoing";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=6&ProjectStatus=" + ProjectStatus);
    }

    protected void lnkVariationProject_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkVariationDocument.aspx?Scheme_Id=" + Scheme_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkVariationPackage_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkVariationDocument.aspx?Scheme_Id=" + Scheme_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkLOI_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=15");
    }

    protected void lnkCB_Stamp_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=16");
    }

    protected void lnkCBFront_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=17");
    }

    protected void lnkCB_ScheduleG_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=18");
    }

    protected void grdPhysicalComponent_PreRender(object sender, EventArgs e)
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

    protected void lnkLD_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=0");
    }

    protected void lnkBondDateDelay_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=1");
    }

    protected void lnkBondDateDelayNotExtended_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=2");
    }

    protected void lnkTE_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=19");
    }

    protected void lnkLDImposed_Click(object sender, EventArgs e)
    {

    }

    protected void lnkLDWithdrawan_Click(object sender, EventArgs e)
    {

    }

    protected void lnkVariationPending_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int ULB_Id = 0;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=99");
    }
}