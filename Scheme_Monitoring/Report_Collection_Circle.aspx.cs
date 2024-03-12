using Aspose.Pdf.Drawing;
using System;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Report_Collection_Circle : System.Web.UI.Page
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
            int Circle_Id = 0;

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
            }
            else
            {
                Zone_Id = 0;
                Circle_Id = 0;
            }

            //int ULB_Id = 0;
            //if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            //{//District
            //    District_Id = Convert.ToInt32(Session["District_Id"].ToString());
            //}
            //if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            //{
            //    District_Id = Convert.ToInt32(Session["District_Id"].ToString());
            //    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
            //    {//ULB
            //        ULB_Id = Convert.ToInt32(Session["ULB_Id"].ToString());
            //    }
            //}
            get_Circle_Wise_Details(Scheme_Id, Zone_Id, Circle_Id);
        }
    }

    private void get_Circle_Wise_Details(int Scheme_Id, int Zone_Id, int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Circle_Wise_Detailed(Zone_Id, Circle_Id, 0, Scheme_Id.ToString(), 0, 0, "", -1);
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
        string Circle_Id = (lnkDistrict.Parent.Parent as GridViewRow).Cells[0].Text;
        string Zone_Id = (lnkDistrict.Parent.Parent as GridViewRow).Cells[1].Text;
        string Circle_Name = lnkDistrict.Text.Trim();
        int Scheme_Id = 0;
        if (Request.QueryString.Count > 0)
        {
            Scheme_Id = Convert.ToInt32(Request.QueryString[0].ToString());
        }
        else
        {
            Scheme_Id = 0;
        }
        Response.Redirect("Report_Collection_Division.aspx?Scheme_Id=" + Scheme_Id.ToString() + "&Circle_Id=" + Circle_Id + "&Zone_Id=" + Zone_Id);
    }

    protected void grdPost1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Circle"].ToString();
        }
    }
}