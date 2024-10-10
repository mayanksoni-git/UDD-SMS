using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ePayment_API.Models;

namespace ePayment_API.Controllers
{
    public class UserManager
    {

        public static UserLogin GetUserLoginInfo(string _LoginID)
        {
            try
            {
                string db = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                var LoginID = long.Parse(_LoginID.Replace("UDD", ""));

                using (SqlConnection con = new SqlConnection(db))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM UserLogin WHERE UserId = @UserId", con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", LoginID);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserLogin
                            {
                                UserID = (long)reader["UserId"],
                                RoleID = (int)reader["RoleId"],
                               
                            };
                        }
                    }
                }

                // If no user was found, return a default UserLogin object
                return new UserLogin
                {
                    RoleID = 0,
                    UserID = 0
                };
            }
            catch (Exception Ex)
            {
                // Log the exception (optional)
                // Logger.LogError(Ex);

                return new UserLogin
                {
                    RoleID = 0,
                    UserID = 0
                };
            }
        }

    }
}