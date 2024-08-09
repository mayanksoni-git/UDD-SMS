using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class PrintULBIncomeExpenseReport : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    string ConStr = ConfigurationManager.AppSettings.Get("conn");
    private int _serialNumber = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var state = Convert.ToInt32(Request.QueryString["state"]);
            var Dist = Convert.ToInt32(Request.QueryString["District"]);
            var ULB = Convert.ToInt32(Request.QueryString["ULB"]);
            var type = Request.QueryString["ULBType"];
            var FYID = Convert.ToInt32(Request.QueryString["FYID"]);
            if (Request.QueryString.Count > 0)
            {
                GetAllData(state, Dist, ULB, type, FYID);
            }
            }
    }

    protected void GetAllData(int? st, int? dis, int? ulb, string type, int? fy)
    {
        // );
        if (type == "0")
        {
            type = "";
            utype.InnerText = "All";
        }

        //var state= ddlZone.SelectedValue != "" ? Convert.ToInt32(ddlZone.SelectedValue) : 0;
        //var Dist= ddlCircle.SelectedValue != "" ? Convert.ToInt32(ddlCircle.SelectedValue) : 0;
        //var ULB = ddlDivision.SelectedValue != "" ? Convert.ToInt32(ddlDivision.SelectedValue) : 0;
        //var ULBType=ddlULBType.SelectedValue;
        //var FY= ddlFY.SelectedValue != "" ? Convert.ToInt32(ddlFY.SelectedValue) : 0;

        DataTable dt = new DataTable();
        dt = objLoan.GetReportOfULBIncomeExpense(st, dis, ulb, type, fy);
        if(dt.Rows.Count==0)
        {
            MessageBox.Show("Record NOt Found");
          return;
        }
        State.InnerText = st == 0 ? "All" : dt.Rows[0]["Zone_Name"].ToString();
        dist.InnerText = dis == 0 ? "All" : dt.Rows[0]["Circle_Name"].ToString();
        uname.InnerText = ulb == 0 ? "All" : dt.Rows[0]["ULBNAme"].ToString();
        utype.InnerText = type == ""   ? "All"  : type == "Np"  ? "Nagar Parishad"  : type == "NPP" ? "Nagar Palika Parishad"  : type == "NN" ? "Nagar Nigam" : string.Empty;
        fyr.InnerText = fy == 0 ? "All" : dt.Rows[0]["FinancialYear_Comments"].ToString();
        _serialNumber = 1;
        rptSearchResult.DataSource = dt;
        rptSearchResult.DataBind();
        
    }
}