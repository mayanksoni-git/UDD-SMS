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

public class Pyres
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    public DataTable Method1(string CIF, int SponserID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CIF", CIF);
            param[1] = new SqlParameter("@SponserID", SponserID);

            return objDAL.GetDataByProcedure("BecomeAgent", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
   
    public void Method2(int AccountID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@AccountID", AccountID);

            objDAL.InsertWithParam("update customer_account set deletestatus=0 where accountid=@AccountID", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int InsertMainTracker(tbl_PyresTracker obj_PyresTracker)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[24];

            param[0] =  new SqlParameter("@AddedBy", obj_PyresTracker.AddedBy);
            param[1] =  new SqlParameter("@Year", obj_PyresTracker.Year);
            param[2] =  new SqlParameter("@Month", obj_PyresTracker.Month);
            param[3] =  new SqlParameter("@Zone", obj_PyresTracker.Zone );
            param[4] =  new SqlParameter("@Circle", obj_PyresTracker.Circle);
            param[5] =  new SqlParameter("@Division", obj_PyresTracker.Division);
            param[6] =  new SqlParameter("@UrbanPopulation", obj_PyresTracker.UrbanPopulation);
            param[7] =  new SqlParameter("@PopulationCreamtion80", obj_PyresTracker.PopulationCreamtion80);
            param[8] =  new SqlParameter("@DeathPer1000", obj_PyresTracker.DeathPer1000);
            param[9] =  new SqlParameter("@EstDeath10Buffer", obj_PyresTracker.EstDeath10Buffer);
            param[10] = new SqlParameter("@Conventional", obj_PyresTracker.Conventional);
            param[11] = new SqlParameter("@ImprovisedWood", obj_PyresTracker.ImprovisedWood);
            param[12] = new SqlParameter("@Gas", obj_PyresTracker.Gas);
            param[13] = new SqlParameter("@Electric", obj_PyresTracker.Electric);
            param[14] = new SqlParameter("@ExistCapacity", obj_PyresTracker.ExistCapacity);
            param[15] = new SqlParameter("@UpgradeExisting", obj_PyresTracker.UpgradeExisting);
            param[16] = new SqlParameter("@RemainingToBeHandled", obj_PyresTracker.RemainingToBeHandled);
            param[17] = new SqlParameter("@UpgradeImprovisedWood", obj_PyresTracker.UpgradeImprovisedWood);
            param[18] = new SqlParameter("@UpgradeGas", obj_PyresTracker.UpgradeGas);
            param[19] = new SqlParameter("@UpgradeElectric", obj_PyresTracker.UpgradeElectric);
            param[20] = new SqlParameter("@RemainingCapacity", obj_PyresTracker.RemainingCapacity);
            param[21] = new SqlParameter("@CommentOnCapacity", obj_PyresTracker.CommentOnCapacity);
            param[22] = new SqlParameter("@PyresToBeRevamped", obj_PyresTracker.PyresToBeRevamped);
            param[23] = new SqlParameter("@FundsRequired", obj_PyresTracker.FundsRequired);
            
            return objDAL.ExecuteProcedure("sp_InsertPyresTracker", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int Method4()
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from OfferMaster where DeleteStatus=0 and CAST(OfferFrom as date)<=cast(DATEADD(minute, 330, getutcdate()) as date) and CAST(OfferTo as date)>=cast(DATEADD(minute, 330, getutcdate()) as date)", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
             
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
