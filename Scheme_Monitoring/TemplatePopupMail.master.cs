using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TemplatePopupMail : System.Web.UI.MasterPage
{
    public string baseURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        baseURL = "http://www.jnupepayment.in" + ResolveUrl("~");
    }

}
