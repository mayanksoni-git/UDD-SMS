using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Menu_Analysis : System.Web.UI.Page
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
        if (Session["UserType"].ToString() == "3" || Session["UserType"].ToString() == "5" || Session["UserType"].ToString() == "7" || Session["UserType"].ToString() == "10")
        {
            divTargetAchivment.Visible = false;
        }
        else
        {
            divTargetAchivment.Visible = true;
        }
        if (!IsPostBack)
        {

        }
    }
}