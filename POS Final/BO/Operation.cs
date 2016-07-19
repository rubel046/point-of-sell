using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace BO
{
    public class Operation
    {
        public SqlConnection create_connection()
        {
            string ConnString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=G:\C#\POS Final\POS\POS Database System.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            SqlConnection Conn = new SqlConnection(ConnString);
            Conn.Open();
            return Conn;
        }

        public void Load_Table1(DataGridView dv)
        {
            string query = "SELECT * FROM [Stock List Table3]";
            SqlConnection Conn = create_connection();
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dataset = new DataTable();
                sda.Fill(dataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataset;
                dv.DataSource = bSource;
                sda.Update(dataset);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*public void Load_Table2(DataGridView dv)
        {
            string query = "SELECT * FROM [Stock List Table3]";
            SqlConnection Conn = create_connection();
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dataset = new DataTable();
                sda.Fill(dataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataset;
                dv.DataSource = bSource;
                sda.Update(dataset);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        /*public void Load_Table2(DataGridView dv)
        {
            string query = "SELECT * FROM [Transaction_List_Table4]";
            SqlConnection Conn = create_connection();
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dataset = new DataTable();
                sda.Fill(dataset);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dataset;
                dv.DataSource = bSource;
                sda.Update(dataset);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        public void SaveInformation(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Added Product Information To Stock List Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateInformation(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Updated Product Information Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update_By_DataGridView_Information(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                //MessageBox.Show("Updated Product Information Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public void Update_Quantity_Information(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                //MessageBox.Show("Information Update Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public void DeleteInformation(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Delete Product Information From Stock List Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Delete_Transaction_Information(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Delete The Information From Transaction List Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Delete_All_Information(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Delete All Information Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdatePassword(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Password Change Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Save_Transaction_Information(string query)
        {
            try
            {
                SqlConnection Conn = create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.ExecuteNonQuery();
                Conn.Close();
                //MessageBox.Show("Information Saved Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string temp2;
        public void Update_DataBase_Quantity(string id1, string quantity1)
        {

            try
            {
                String query = "SELECT QTY FROM [Stock List Table3] WHERE [Product ID]='" + id1 + "'";
                SqlConnection Conn = create_connection();
                SqlCommand cd = new SqlCommand(query, Conn);
                SqlDataReader reader = cd.ExecuteReader();

                while (reader.Read())
                {
                    //temp1 = reader["Product ID"].ToString();
                    temp2 = reader["QTY"].ToString();
                    //MessageBox.Show(temp2);
                }

                if (double.Parse(temp2) != 0)
                {
                    string quantity2 = (double.Parse(temp2) - double.Parse(quantity1)).ToString();
                    //MessageBox.Show(quantity2);
                    String query1 = "UPDATE [Stock List Table3] SET QTY = '" + quantity2 + "' WHERE [Product ID] = '" + id1 + "'";
                    Update_Quantity_Information(query1);
                }
                else
                {
                    MessageBox.Show("Product Stock Finish.\nSo Add Your Product Quantity", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
