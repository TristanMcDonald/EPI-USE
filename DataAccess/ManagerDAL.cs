using EPI_USE.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EPI_USE.DataAccess
{
    public class ManagerDAL
    {
        //Remote database connection string
        string connectionStringPROD = "Server=tcp:epi-use-server.database.windows.net,1433;Initial Catalog=epi-use-db;Persist Security Info=False;User ID=tristan;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //Get All Managers
        public IEnumerable<Manager> GetAllManagers()
        {
            List<Manager> manList = new List<Manager>();

            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllManagers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Manager man = new Manager();
                    man.ManagerNumber = dr["managerNumber"].ToString();
                    man.Name = dr["name"].ToString();
                    man.Surname = dr["surname"].ToString();

                    manList.Add(man);
                }
                con.Close();
            }

            return manList;

        }

        //Create a new Manager
        public void CreateManager(Manager man)
        {
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_CreateManager", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@managerNumber", man.ManagerNumber);
                cmd.Parameters.AddWithValue("@name", man.Name);
                cmd.Parameters.AddWithValue("@surname", man.Surname);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        //Get Manager By manager number
        public Manager GetManager(string managerNo)
        {
            Manager man = new Manager();
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_GetManager", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@managerNumber", managerNo);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    man.ManagerNumber = dr["managerNumber"].ToString();
                    man.Name = dr["name"].ToString();
                    man.Surname = dr["surname"].ToString();
                }
                con.Close();
            }
            return man;
        }

        //Update Manager
        public void UpdateManager(Manager man)
        {
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateManager", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@managerNumber", man.ManagerNumber);
                cmd.Parameters.AddWithValue("@name", man.Name);
                cmd.Parameters.AddWithValue("@surname", man.Surname);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        //Delete Manager
        public void DeleteManager(string managerNo)
        {
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteManager", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@managerNumber", managerNo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
