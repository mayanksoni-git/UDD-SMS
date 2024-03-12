using System;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Report_Collection_District : System.Web.UI.Page
{
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
            int Scheme_Id = 0;
            if (Request.QueryString.Count > 0)
            {
                Scheme_Id = Convert.ToInt32(Request.QueryString[0].ToString());
            }
            else
            {
                Scheme_Id = 0;
            }
            if (Scheme_Id == 0)
            {
                lblScheme.Text = "सभी योजना / कार्यक्रम";
            }
            else
            {
               DataSet ds = new DataLayer().get_tbl_Project(Scheme_Id.ToString());
                if (AllClasses.CheckDataSet(ds))
                {
                    lblScheme.Text = "योजना / कार्यक्रम: " + ds.Tables[0].Rows[0]["Project_Name"].ToString();
                }
            }
            int Zone_Id = 0;
            if (Request.QueryString.Count > 1)
            {
                Zone_Id = Convert.ToInt32(Request.QueryString[1].ToString());
            }
            else
            {
                Zone_Id = 0;
            }
            get_District_Wise_Details(Scheme_Id, Zone_Id);
        }
    }

    private void get_District_Wise_Details(int Scheme_Id, int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Zone_Wise_Detailed(Zone_Id, 0, 0, Scheme_Id.ToString(), 0, 0, "", -1);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost1.DataSource = ds.Tables[0];
            grdPost1.DataBind();

            grdPost1.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Count)", "").ToString();
            grdPost1.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Completed_Count)", "").ToString();
            grdPost1.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(OnGoing_Count)", "").ToString();
            grdPost1.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Total_Sanction)", "").ToString();
            grdPost1.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(Completed_Sanction)", "").ToString();

            grdPost1.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(OnGoing_Sanction)", "").ToString();
            grdPost1.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Total_Release)", "").ToString();
            grdPost1.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(Completed_Release)", "").ToString();
            grdPost1.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(OnGoing_Release)", "").ToString();
            grdPost1.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Total_Remaining_Amount)", "").ToString();
            grdPost1.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Completed_Remaining_Amount)", "").ToString();
            grdPost1.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(OnGoing_Remaining_Amount)", "").ToString();
            grdPost1.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(Total_Expenditure)", "").ToString();
            grdPost1.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(Completed_Expenditure)", "").ToString();
            grdPost1.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(OnGoing_Expenditure)", "").ToString();
        }
        else
        {
            grdPost1.DataSource = null;
            grdPost1.DataBind();
        }
    }

    protected void grdPost1_PreRender(object sender, EventArgs e)
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

    protected void lnkDistrict_Click(object sender, EventArgs e)
    {
        LinkButton lnkDistrict = (sender as LinkButton);
        string Zone_Id = (lnkDistrict.Parent.Parent as GridViewRow).Cells[0].Text;
        string Zone_Name = lnkDistrict.Text.Trim();
        int Scheme_Id = 0;
        if (Request.QueryString.Count > 0)
        {
            Scheme_Id = Convert.ToInt32(Request.QueryString[0].ToString());
        }
        else
        {
            Scheme_Id = 0;
        }
        Response.Redirect("Report_Collection_Circle.aspx?Scheme_Id=" + Scheme_Id.ToString() + "&Zone_Id=" + Zone_Id + "&Zone_Name=" + Zone_Name + "&Circle_Id=0&Circle_Name=");
    }

    protected void grdPost1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Zone"].ToString();
        }
    }
}