using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

public class WorkProposal
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    public DataTable getWorkProposalDashbaord(string Proc)
    {
        try
        {
            DataTable dt = new DataTable();
            return objDAL.GetDataByProcedure(Proc);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}