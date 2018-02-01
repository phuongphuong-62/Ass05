using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Provider
    {
        private static Provider instance;
        public static Provider Instance
        {
            get
            {
                if (instance == null)
                    instance = new Provider();
                return instance;
            }
        }

        private Provider()
        {

        }
        private string connection = @"Data Source=PHUONGPHUONG-PC\SQLEXPRESS;Initial Catalog=Ass05;Persist Security Info=True;User ID=sa;Password=123456";
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {

            DataTable data = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
               
                SqlCommand cmd = new SqlCommand(query, con);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                con.Close();
            }
            return data;
        }

        //public int ExecuteNonQuery(string query, object[] parameter = null)
        //{
        //    int data = 0;

        //    using (SqlConnection con = new SqlConnection(connection))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(query, con);

        //        if (parameter != null)
        //        {
        //            string[] listPara = query.Split(' ');
        //            int i = 0;
        //            foreach (string item in listPara)
        //            {
        //                if (item.Contains('@'))
        //                {
        //                    cmd.Parameters.AddWithValue(item, parameter[i]);
        //                    i++;
        //                }
        //            }
        //        }

        //        data = cmd.ExecuteNonQuery();

        //        con.Close();
        //    }

        //    return data;
        //}

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = -1;
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand command = new SqlCommand(query, con);
                if (parameter != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> lstParam = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item.Length > 0)
                        {
                            if (item[0] == '@')
                                lstParam.Add(item);
                        }
                    }

                    for (int i = 0; i < parameter.Length; i++)
                    {
                        command.Parameters.AddWithValue(lstParam[i], parameter[i]);
                    }
                }

                try
                {
                    data = command.ExecuteNonQuery();
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
                

                con.Close();
            }
            return data;
        }

        public object ExecuteScalar(string query , object[] parameter = null )
        {
            object data = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
            
                SqlCommand cmd = new SqlCommand(query, con);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = cmd.ExecuteScalar();

                con.Close();
            }

            return data;
        }

    }
}
