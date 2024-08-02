using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ULBFund
/// </summary>
public class ULBFund
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    private DataSet ExecuteSelectQuery(string Sql)
    {
        DataSet set1 = new DataSet();
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            SqlCommand command1 = new SqlCommand(Sql, con);
            if (command1.Connection.State == ConnectionState.Closed)
            {
                command1.Connection.Open();
            }
            command1.CommandTimeout = 7000;
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

            adapter1.Fill(set1);
        }
        return set1;
    }

    private DataTable ExecuteSelectQuerywithDatatable(string Sql)
    {
        DataTable set1 = new DataTable();
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            SqlCommand command1 = new SqlCommand(Sql, con);
            if (command1.Connection.State == ConnectionState.Closed)
            {
                command1.Connection.Open();
            }
            command1.CommandTimeout = 7000;
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

            adapter1.Fill(set1);
        }
        return set1;
    }


    private DataSet ExecuteSelectQuerywithTransaction(SqlConnection Con, string Sql, SqlTransaction trans)
    {
        DataSet set1 = new DataSet();
        SqlCommand command1 = new SqlCommand(Sql, Con, trans);
        command1.CommandTimeout = 7000;
        SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
        adapter1.Fill(set1);
        return set1;
    }


    public DataTable GetULBFundAction(string actions, int? ULBID, int? TaskId,int?  stateid, int? circleId,int? FY,decimal SFC, decimal CFC, decimal totalTax, int? personId )
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@statetId", stateid);

            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY);
            param[6] = new SqlParameter("@SFCFund", SFC);
            param[7] = new SqlParameter("@CFCFund", CFC);
            param[8] = new SqlParameter("@TotalTaxtCollection", totalTax);
            param[9] = new SqlParameter("@PersonBy", personId);
           

            return objDAL.GetDataByProcedure("ULBFunds", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    #region ULB Income
    public bool InsertULBFundIncome(List<Tbl_ULBIncomeTypeChild> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                if (AllClasses.CheckDataSet(CheckDuplicacyData(li[0].ULBID,li[0].FYID, trans, cn)))
                {
                    Msg = "A";
                    trans.Commit();
                    cn.Close();
                    return false;
                }
                for (int i = 0; i < li.Count; i++)
                {
                   
                    Insert_Tbl_ULBIncome(li[i], trans, cn);
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

                return flag;

    }
    public void Insert_Tbl_ULBIncome(Tbl_ULBIncomeTypeChild obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";



        strQuery = " set dateformat dmy;insert into Tbl_ULBIncomeTypeChild ( [stateId],[CircleId],[ULBID],[FYID],[HeadID],[Amount],[createdBy],[createdOn],[IsActive] ) values ('" + obj.stateId + "','" + obj.CircleId + "','" + obj.ULBID + "','" + obj.FYID + "','" + obj.HeadID + "','" + obj.Amount + "' ,'" + obj.createdBy + "','" + obj.createdOn + "','" + obj.IsActive + "');Select @@Identity";
        if (trans == null)
        {
            try
            {
                ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }

    public DataSet CheckDuplicacyData(int?ULBID,int?FYID, SqlTransaction trans, SqlConnection cn)
    {

        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = " set dateformat dmy; Select  * from Tbl_ULBIncomeTypeChild  where isactive = 'true' and  ULBID = '" + ULBID + "' and FYID='"+FYID+ "' ";
      
        if (trans == null)
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        else
        {
            ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
        return ds;
    }

    public DataTable GetULBIncomedata(string ULBID,string FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ULBId", ULBID);
            param[1] = new SqlParameter("@FYID", FYID);

            return objDAL.GetDataByProcedure("SpAllULBIncomeData", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDataForEditOfIncome(string ULBID, string FYID)
    {

        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;
       SELECT 
		a.*,
	    ex.ULBIncomeType_Name,
	    zn.Zone_Name,
		cl.Circle_Name,
		ul.Division_Name as ULBNAme,
		fy.FinancialYear_Comments
		,ex.ULBIncomeType_Id

FROM Tbl_ULBIncomeTypeChild a
INNER JOIN tbl_Division ul ON a.ULBID = ul.Division_Id
INNER JOIN tbl_Zone zn ON a.stateId = zn.Zone_Id
INNER JOIN tbl_Circle cl ON a.CircleId = cl.Circle_Id
INNER JOIN tbl_FinancialYear fy ON a.FYID = fy.FinancialYear_Id
INNER JOIN tbl_ULBIncomeType ex on a.HeadID=ex.ULBIncomeType_Id
                WHERE a.ULBId = '" + ULBID + "' AND a.IsActive = 'true' AND  a.FYID = '" + FYID + "' ORDER BY ex.ULBIncomeType_Name ";
        try
        {
            ds = ExecuteSelectQuerywithDatatable(strQuery);
        }
        catch (Exception e)
        {
            ds = null;
        }
        return ds;
    }


    public bool UpdateULBFundIncome(List<Tbl_ULBIncomeTypeChild> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {

                for (int i = 0; i < li.Count; i++)
                {

                    Update_Tbl_ULBIncome(li[i], trans, cn);
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

        return flag;

    }
    public void Update_Tbl_ULBIncome(Tbl_ULBIncomeTypeChild obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";



        strQuery = " set dateformat dmy;Update Tbl_ULBIncomeTypeChild  set  stateId='" + obj.stateId + "',CircleId='" + obj.CircleId + "',ULBID='" + obj.ULBID + "',FYID='" + obj.FYID + "',Amount='" + obj.Amount + "',updateBy='" + obj.updateBy + "',updateOn='" + DateTime.Now + "' where HeadID='" + obj.HeadID + "' and FYID='" + obj.FYID + "' and ULBID='" + obj.ULBID + "';Select @@Identity";
        if (trans == null)
        {
            try
            {
                ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }
    public DataTable DeleteULBIncomes(string ULBID, string FYID)
    {
        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;Update Tbl_ULBIncomeTypeChild  set IsActive='false' where  FYID='" + FYID + "' and ULBID='" + ULBID + "';  select 'Data Deleted ' as remark";
        ds = ExecuteSelectQuerywithDatatable(strQuery);
        return ds;
    }

    #endregion

    #region ULB Expense

    //UpdateULBFundExpense
    public bool InsertULBFundExpense(List<Tbl_ULBExpenses> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                if (AllClasses.CheckDataSet(CheckDuplicacyDataExpense(li[0].ULBID, li[0].FYID, trans, cn)))
                {
                    Msg = "A";
                    trans.Commit();
                    cn.Close();
                    return false;
                }
                for (int i = 0; i < li.Count; i++)
                {

                    Insert_Tbl_ULBExpense(li[i], trans, cn);
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

        return flag;

    }
    public void Insert_Tbl_ULBExpense(Tbl_ULBExpenses obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";



        strQuery = " set dateformat dmy;insert into Tbl_ULBExpenses ( [stateId],[CircleId],[ULBID],[FYID],[HeadID],[NewAmount],[MaintenanceAmount],[createdBy],[createdOn],[IsActive] ) values ('" + obj.stateId + "','" + obj.CircleId + "','" + obj.ULBID + "','" + obj.FYID + "','" + obj.HeadID + "','" + obj.NewAmount + "' ,'" + obj.MaintenanceAmount + "' ,'" + obj.createdBy + "','" + obj.createdOn + "','" + obj.IsActive + "');Select @@Identity";
        if (trans == null)
        {
            try
            {
                ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }


    public bool UpdateULBFundExpense(List<Tbl_ULBExpenses> li, string Msg)
    {
        bool flag = false;

        DataSet ds = new DataSet();
        using (SqlConnection cn = new SqlConnection(ConStr))
        {
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlTransaction trans = cn.BeginTransaction();
            try
            {
                
                for (int i = 0; i < li.Count; i++)
                {

                    Update_Tbl_ULBExpense(li[i], trans, cn);
                }
                trans.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                flag = false;
            }
        }

        return flag;

    }
    public void Update_Tbl_ULBExpense(Tbl_ULBExpenses obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";



        strQuery = " set dateformat dmy;Update Tbl_ULBExpenses  set  stateId='" + obj.stateId + "',CircleId='" + obj.CircleId + "',ULBID='" + obj.ULBID + "',FYID='" + obj.FYID + "',NewAmount='" + obj.NewAmount + "',MaintenanceAmount='" + obj.MaintenanceAmount + "',updateBy='"+obj.updateBy+ "',updateOn='"+DateTime.Now+ "' where HeadID='" + obj.HeadID+ "' and FYID='"+obj.FYID+ "' and ULBID='" + obj.ULBID + "';Select @@Identity";
        if (trans == null)
        {
            try
            {
                ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }

    public DataSet CheckDuplicacyDataExpense(int? ULBID, int? FYID, SqlTransaction trans, SqlConnection cn)
    {

        string strQuery = "";
        DataSet ds = new DataSet();
        strQuery = " set dateformat dmy; Select  * from Tbl_ULBExpenses  where isactive = 'true' and  ULBID = '" + ULBID + "' and FYID='" + FYID + "' ";

        if (trans == null)
        {
            ds = ExecuteSelectQuery(strQuery);
        }
        else
        {
            ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
        return ds;
    }

    public DataTable GetULBTbl_ULBExpensesdata(string ULBID, string FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ULBId", ULBID);
            param[1] = new SqlParameter("@FYID", FYID);

            return objDAL.GetDataByProcedure("SpAllULBExpenseData", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDataForEdit(string ULBID, string FYID)
    {

        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;
        SELECT
                a.*,
            zn.Zone_Name,
            cl.Circle_Name,
            ul.Division_Name as ULBNAme,
            fy.FinancialYear_Comments,
            ex.ULBExpenseType_Name,ex.ULBExpenseType_Id

        FROM tbl_ULBExpenses a
        INNER JOIN tbl_Division ul ON a.ULBID = ul.Division_Id
         INNER JOIN tbl_Zone zn ON a.stateId = zn.Zone_Id
          INNER JOIN tbl_Circle cl ON a.CircleId = cl.Circle_Id
            INNER JOIN tbl_FinancialYear fy ON a.FYID = fy.FinancialYear_Id
            INNER JOIN tbl_ULBExpenseType ex on a.HeadID = ex.ULBExpenseType_Id
                WHERE a.ULBId = '" + ULBID + "' AND a.IsActive = 'true' AND  a.FYID = '" + FYID + "' ORDER BY ex.ULBExpenseType_Name ";
        try
        {
            ds = ExecuteSelectQuerywithDatatable(strQuery);
        }
        catch (Exception e)
        {
            ds = null;
        }
        return ds;
    }

    public DataTable DeleteULBExpenses(string ULBID, string FYID)
    {
        string strQuery = "";
        DataTable ds = new DataTable();
        strQuery = @"set dateformat dmy;Update  Tbl_ULBExpenses   set IsActive='false' where  FYID='" + FYID + "' and ULBID='" + ULBID +"';  select 'Data Deleted ' as remark";
        ds = ExecuteSelectQuerywithDatatable(strQuery);
        return ds;
    }

    #endregion

}