﻿using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace TransportDALLibrary
{
    public class EmployeeDAL
    {
        SqlConnection conn;
        SqlCommand cmd;
        string strConnection;
        public EmployeeDAL()
        {
            strConnection = @"server=LAPTOP-V7NNEK9K;Integrated security= true ; Initial catalog = dbSoftTransport";
            conn = new SqlConnection(strConnection);
        }

        public bool AddEmployee(Employee employee)
        {
            cmd = new SqlCommand("proc_InsertEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@eName",employee.Name);
                cmd.Parameters.AddWithValue("@ePassword",employee.Password);
                cmd.Parameters.AddWithValue("@eLocation",employee.Location);
                cmd.Parameters.AddWithValue("@ePhone",employee.Phone);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Close();
            }

        }

        public bool UpdatePasword(Employee employee)
        {
            cmd = new SqlCommand("proc_UpdatePassword", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@eid", employee.Id);
                cmd.Parameters.AddWithValue("@ePassword", employee.Password);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Close();
            }

        }
        public bool UpdateLocation(Employee employee)
        {
            cmd = new SqlCommand("proc_UpdateLocation", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@eid", employee.Id);
                cmd.Parameters.AddWithValue("@eLocation", employee.Location);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Close();
            }

        }

        public ICollection<Employee> SelectAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlCommand cmd = new SqlCommand("proc_GetAllEmployees");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            SqlDataAdapter daEmployee = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet(); //dataset is a collection of data tables //data tables is a collection of rows//row is a collection of columns
            try
            {
                daEmployee.Fill(ds, "Employee");//connect ->fetch the data->put it in the dataset->give the name provided->disconnect from db
                Employee employee;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    employee = new Employee();
                    employee.Id = Convert.ToInt32(dr[0]);
                    employee.Name = dr[1].ToString();
                    employee.Password = dr[2].ToString();
                    employee.Location = dr[3].ToString();
                    employee.Phone = dr[4].ToString();
                    employee.VehicleNumber = dr[5].ToString();
                    employees.Add(employee);

                }
                return employees;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
