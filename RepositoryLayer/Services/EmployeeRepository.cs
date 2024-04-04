using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RepositoryLayer.Services
{
    public  class EmployeeRepository:IEmployeeRepository
    {
        string connectionString = @"Data Source=LAPTOP-HFJ7MFRU\SQLEXPRESS;Initial Catalog=MVC;Integrated Security=True;TrustServerCertificate=True";

        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Emp_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(reader["EmpID"]);
                    employee.Name = reader["Name"].ToString();
                    employee.ProfileImage = reader["ProfileImage"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.Department = reader["Department"].ToString();
                    employee.Salary = Convert.ToInt32(reader["Salary"]);
                    employee.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    employee.Notes = reader["Notes"].ToString();
                    employees.Add(employee);

                }
                conn.Close();
                return employees;
            }

        }
        public bool AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddEmp_Sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Id", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;

            }

        }
        public bool EmployeeUpdate(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("upEmp_Sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }

        }

        public bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("deleteEmp_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);

                       conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return false;
            }
        }



        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("getEmpById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {

                        employee.EmployeeId = Convert.ToInt32(dataReader["EmpID"]);
                        employee.Name = dataReader["Name"].ToString();
                        employee.ProfileImage = dataReader["ProfileImage"].ToString();
                        employee.Gender = dataReader["Gender"].ToString();
                        employee.Department = dataReader["Department"].ToString();
                        employee.Salary = Convert.ToInt64(dataReader["Salary"]);
                        employee.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                        employee.Notes = dataReader["Notes"].ToString();

                    }
                    return employee;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;

            }
        }

        public Employee Login(LoginModel model)
        {
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand("Login_Sp", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", model.Id);
                com.Parameters.AddWithValue("@Name", model.Name);
                conn.Open();
                SqlDataReader dr = com.ExecuteReader();
                while(dr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(dr["EmpID"]);
                    employee.Name= dr["Name"].ToString();
                    return employee;

                }
                return null;
                
            }
        }
        public Employee GetEmployeeBYName(string name)
        {
            Employee emp = new Employee();
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("byName", conn);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@name", name);
                try 
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        emp.EmployeeId = Convert.ToInt32(dr["EmpID"]);
                        emp.Name = dr["Name"].ToString();
                        emp.ProfileImage = dr["ProfileImage"].ToString();
                        emp.Gender = dr["Gender"].ToString();
                        emp.Department = dr["Department"].ToString();
                        emp.Salary = Convert.ToInt64(dr["Salary"]);
                        emp.StartDate = Convert.ToDateTime(dr["StartDate"]);
                        emp.Notes = dr["Notes"].ToString();

                    
                    }
                    return emp;
                }
                catch(Exception ex)
                {
                    return null;
                } 
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool  InsertUpdate(Employee employee)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {


                try
                {
                    SqlCommand cmd = new SqlCommand("updInsert", conn);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    //cmd.Parameters.AddWithValue("@Id", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                    cmd.ExecuteNonQuery();
                    return true;
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }
    
    }

}
