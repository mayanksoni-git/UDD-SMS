using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParkDetailsAndFascility : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();

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
            int Id = 0;
            if (Request.QueryString.Count > 0)
            {
                Id = Convert.ToInt32(Request.QueryString[0].ToString());
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                Get_ParkFascilityById(Id);
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
    protected void Get_ParkFascilityById(int AdoptedParkDetailID)
    {
       
        DataTable dt = new DataTable();
        dt = objLoan.Get_ParkFascilityById("selectById", AdoptedParkDetailID);
        grdPost.DataSource = dt;
        grdPost.DataBind();

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = (ImageButton)sender;

        GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

        // Retrieve the ID from the HiddenField in that row
        HiddenField hiddenField = (HiddenField)row.FindControl("hdnParkDetailFacilityId");
        int ParkDetailFacilityId = Convert.ToInt32(hiddenField.Value);

        // Redirect to the same page with the ID as a query parameter
        Response.Redirect("EditParkFascility_details.aspx?ParkDetailFacilityId=" + ParkDetailFacilityId);
    }

}