using EPI_USE.Controllers;
using EPI_USE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EPI_USE.DataAccess
{
    public class EmployeeDAL
    {
        //Remote database connection string
        string connectionStringPROD = "Server=tcp:epi-use-server.database.windows.net,1433;Initial Catalog=epi-use-db;Persist Security Info=False;User ID=tristan;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //Get All Employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> empList = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.EmployeeNumber= dr["employeeNumber"].ToString();
                    emp.Name = dr["name"].ToString();
                    emp.Surname = dr["surname"].ToString();
                    emp.EmailAddress = dr["emailAddress"].ToString();
                    emp.BirthDate = (DateTime)dr["birthDate"];
                    emp.Salary = dr["salary"].ToString();
                    emp.Position = dr["position"].ToString();
                    emp.ReportingLineManager = dr["reportingLineManager"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }

            return empList;

        }

        //Create a new employee
        public void CreateEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_CreateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employeeNumber", emp.EmployeeNumber);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@surname", emp.Surname);
                cmd.Parameters.AddWithValue("@emailAddress", emp.EmailAddress);
                cmd.Parameters.AddWithValue("@birthDate", emp.BirthDate);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@position", emp.Position);
                cmd.Parameters.AddWithValue("@reportingLineManager", emp.ReportingLineManager);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        //Get Employee By ID

        public Employee GetEmployee(string employeeNo)
        {
            Employee emp = new Employee();
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employeeNumber", employeeNo);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.EmployeeNumber = dr["employeeNumber"].ToString();
                    emp.Name = dr["name"].ToString();
                    emp.Surname = dr["surname"].ToString();
                    emp.EmailAddress = dr["emailAddress"].ToString();
                    emp.BirthDate = (DateTime)dr["birthDate"];
                    emp.Salary = dr["salary"].ToString();
                    emp.Position = dr["position"].ToString();
                    emp.ReportingLineManager = dr["reportingLineManager"].ToString();
                }
                con.Close();
            }
            return emp;
        }

        //Update Employee

        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employeeNumber", emp.EmployeeNumber);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@surname", emp.Surname);
                cmd.Parameters.AddWithValue("@emailAddress", emp.EmailAddress);
                cmd.Parameters.AddWithValue("@birthDate", emp.BirthDate);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@position", emp.Position);
                cmd.Parameters.AddWithValue("@reportingLineManager", emp.ReportingLineManager);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        //Delete Employee

        public void DeleteEmployee(string employeeNo)
        {
            using (SqlConnection con = new SqlConnection(connectionStringPROD))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employeeNumber", employeeNo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
