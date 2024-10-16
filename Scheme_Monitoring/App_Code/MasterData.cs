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
public class MasterDate
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


    public DataTable SaveAkanshiHead(string Actions, int FYID, string AkanshiHead, decimal CostPerHead, int CreatedBy)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Actions", Actions);
            param[1] = new SqlParameter("@FYID", FYID);
            param[2] = new SqlParameter("@AkanshiHead", AkanshiHead);
            param[3] = new SqlParameter("@CostPerHead", CostPerHead);
            param[4] = new SqlParameter("@CreatedBy", CreatedBy);

            return objDAL.GetDataByProcedure("sp_AkanshiHeadMaster", param);


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable UpdateAkanshiHead(int AkanshiHeadId, int FYID, string AkanshiHead, decimal CostPerHead, int CreatedBy)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@Actions", "Update");
            param[1] = new SqlParameter("@AkanshiHeadId", AkanshiHeadId);
            param[2] = new SqlParameter("@FYID", FYID);
            param[3] = new SqlParameter("@AkanshiHead", AkanshiHead);
            param[4] = new SqlParameter("@CostPerHead", CostPerHead);
            param[5] = new SqlParameter("@CreatedBy", CreatedBy);

            return objDAL.GetDataByProcedure("sp_AkanshiHeadMaster", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable DeleteAkanshiHead(int AkanshiHeadId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Actions", "Delete");
            param[1] = new SqlParameter("@AkanshiHeadId", AkanshiHeadId);

            return objDAL.GetDataByProcedure("sp_AkanshiHeadMaster", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetAkanshiHead(int AkanshiHeadId, string AkanshiHead)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@Actions", "Search");
            param[1] = new SqlParameter("@AkanshiHeadId", AkanshiHeadId);
            param[2] = new SqlParameter("@AkanshiHead", AkanshiHead);

            return objDAL.GetDataByProcedure("sp_AkanshiHeadMaster", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetAkanshiHeadById(int AkanshiHeadId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Actions", "SearchById");
            param[1] = new SqlParameter("@AkanshiHeadId", AkanshiHeadId);

            return objDAL.GetDataByProcedure("sp_AkanshiHeadMaster", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //public (DataTable Result, string Remark) SaveAkanshiHead(string Actions, int FYID, string AkanshiHead, decimal CostPerHead, int CreatedBy)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        SqlParameter[] param = new SqlParameter[5];

    //        param[0] = new SqlParameter("@Actions", Actions);
    //        param[1] = new SqlParameter("@FYID", FYID);
    //        param[2] = new SqlParameter("@AkanshiHead", AkanshiHead);
    //        param[3] = new SqlParameter("@CostPerHead", CostPerHead);
    //        param[4] = new SqlParameter("@CreatedBy", CreatedBy);

    //        // Execute the stored procedure
    //        dt = objDAL.GetDataByProcedure("sp_AkanshiHeadMaster", param);

    //        // Assuming the Remark is returned as a single row with a single column named "Remark"
    //        string remark = dt.Rows.Count > 0 ? dt.Rows[0]["Remark"].ToString() : string.Empty;

    //        return (dt, remark); // Return the DataTable and the Remark
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
}