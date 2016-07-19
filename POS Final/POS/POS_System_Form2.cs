using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BO;

namespace POS
{
    public partial class POS_System_Form2 : Form
    {
        public POS_System_Form2()
        {
            InitializeComponent();
            op.Load_Table1(dataGridView1);
            op.Load_Table1(dataGridView4);
            Load_Table2(dataGridView5);
        }

        DataTable dataset;
        Operation op = new Operation();

        private void Save_button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = ID_textBox1.Text.ToString();
                string name = Name_textBox2.Text;
                string description = Description_textBox3.Text;
                string category = Catagory_comboBox1.Text;
                string quantity = Quantity_textBox4.Text.ToString();
                string buyPrice = Buy_Price_textBox5.Text.ToString();
                string sellPrice = Sell_Price_textBox6.Text.ToString();
                string date = dateTimePicker1.Text;

                String query = "INSERT INTO [Stock List Table3]([Product ID],[Product Name],[Product Description],Category,QTY,[Buy Price],[Sell Price],Date) VALUES('" + id + "','" + name + "','" + description + "','" + category + "','" + quantity + "','" + buyPrice + "','" + sellPrice + "','" + date + "')";

                op.SaveInformation(query);
                op.Load_Table1(dataGridView1);
                op.Load_Table1(dataGridView4);

                ID_textBox1.Text = "";
                Name_textBox2.Text = "";
                Description_textBox3.Text = "";
                Catagory_comboBox1.Text = "";
                Quantity_textBox4.Text = "";
                Buy_Price_textBox5.Text = "";
                Sell_Price_textBox6.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Cancel_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Update_button1_Click(object sender, EventArgs e)
        {
            try
            {
                string id = uID_textBox1.Text.ToString();
                string name = uName_textBox2.Text;
                string description = uDescription_textBox3.Text;
                string category = uCatagory_comboBox1.Text;
                string quantity = uQuantity_textBox4.Text.ToString();
                string buyPrice = uBuy_Price_textBox5.Text.ToString();
                string sellPrice = uSell_Price_textBox6.Text.ToString();
                string date = dateTimePicker2.Text;

                String query = "UPDATE [Stock List Table3] SET  [Product Name] = '" + name + "',[Product Description] = '" + description + "',[Category] = '" + category + "',[QTY] = '" + quantity + "',[Buy Price] = '" + buyPrice + "',[Sell Price] = '" + sellPrice + "',Date = '" + date + "' WHERE [Product ID] = '" + id + "'";
                op.UpdateInformation(query);

                op.Load_Table1(dataGridView1);
                op.Load_Table1(dataGridView4);

                uID_textBox1.Text = "";
                uName_textBox2.Text = "";
                uDescription_textBox3.Text = "";
                uCatagory_comboBox1.Text = "";
                uQuantity_textBox4.Text = "";
                uBuy_Price_textBox5.Text = "";
                uSell_Price_textBox6.Text = "";
                sID_textBox7.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void uCANCEL_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                uID_textBox1.Text = row.Cells["Product ID"].Value.ToString();
                uName_textBox2.Text = row.Cells["Product Name"].Value.ToString();
                uDescription_textBox3.Text = row.Cells["Product Description"].Value.ToString();
                uCatagory_comboBox1.Text = row.Cells["Category"].Value.ToString();
                uQuantity_textBox4.Text = row.Cells["QTY"].Value.ToString();
                uBuy_Price_textBox5.Text = row.Cells["Buy Price"].Value.ToString();
                uSell_Price_textBox6.Text = row.Cells["Sell Price"].Value.ToString();
                dateTimePicker2.Text = row.Cells["Date"].Value.ToString();
            }
        }

        private void Delete_button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dID_textBox1.Text.Trim() != null)
                {
                    string id = dID_textBox1.Text.ToString();
                    String query = "DELETE FROM [Stock List Table3] WHERE [Product ID] ='" + id + "'";

                    op.DeleteInformation(query);
                    op.Load_Table1(dataGridView1);
                    op.Load_Table1(dataGridView4);

                    dID_textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Enter Product ID Which One You Want Delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dCancel_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tDelete_button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Receipt_No_textBox1.Text.Trim() != null)
                {
                    string Receipt_No = Receipt_No_textBox1.Text;
                    String query = "DELETE FROM Transaction_List_Table4 WHERE [Receipt No.] = '" + Receipt_No + "'";

                    op.Delete_Transaction_Information(query);

                    Load_Table2(dataGridView5);

                    Receipt_No_textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Enter Receipt No. Which One You Want Delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dID_textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView1.DataSource;
                bs.Filter = " [Product ID] = " + "'" + dID_textBox1.Text + "'";
                dataGridView1.DataSource = bs;
                this.AcceptButton = Delete_button1;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void slID_textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView4.DataSource;
                bs.Filter = "[Product ID] = " + "'" + slID_textBox1.Text + "'";
                dataGridView4.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void sID_textBox7_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView4.DataSource;
                bs.Filter = " [Product ID] = " + "'" + sID_textBox7.Text + "'";
                dataGridView1.DataSource = bs;
                this.AcceptButton = uSearch_button3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Receipt_No_textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dataset);
                dv.RowFilter = string.Format("[Receipt No.] LIKE '%{0}%'", Receipt_No_textBox1.Text);
                dataGridView5.DataSource = dv;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void uSearch_button3_Click_1(object sender, EventArgs e)
        {
            if (sID_textBox7.Text.Trim().Length > 0)
            {
                try
                {
                    String query = "SELECT * FROM [Stock List Table3] WHERE [Product ID]='" + sID_textBox7.Text.ToString() + "'";
                    SqlConnection Conn = op.create_connection();
                    SqlCommand cd = new SqlCommand(query, Conn);
                    SqlDataReader reader = cd.ExecuteReader();

                    if (reader.Read())
                    {
                        uID_textBox1.Text = reader["Product ID"].ToString();
                        uName_textBox2.Text = reader["Product Name"].ToString();
                        uDescription_textBox3.Text = reader["Product Description"].ToString();
                        uCatagory_comboBox1.Text = reader["Category"].ToString();
                        uQuantity_textBox4.Text = reader["QTY"].ToString();
                        uBuy_Price_textBox5.Text = reader["Buy Price"].ToString();
                        uSell_Price_textBox6.Text = reader["Sell Price"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Delete_All_button3_Click(object sender, EventArgs e)
        {
            try
            {

                string Receipt_No = Receipt_No_textBox1.Text;
                String query = "DELETE FROM Transaction_List_Table4;";

                op.Delete_All_Information(query);

                Load_Table2(dataGridView5);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string id = row.Cells["Product ID"].Value.ToString();
                    string name = row.Cells["Product Name"].Value.ToString();
                    string description = row.Cells["Product Description"].Value.ToString();
                    string category = row.Cells["Category"].Value.ToString();
                    string quantity = row.Cells["QTY"].Value.ToString();
                    string buyPrice = row.Cells["Buy Price"].Value.ToString();
                    string sellPrice = row.Cells["Sell Price"].Value.ToString();
                    string date = row.Cells["Date"].Value.ToString();

                    String query = "UPDATE [Stock List Table3] SET  [Product Name] = '" + name + "',[Product Description] = '" + description + "',[Category] = '" + category + "',QTY = '" + quantity + "',[Buy Price] = '" + buyPrice + "',[Sell Price] = '" + sellPrice + "',Date = '" + date + "' WHERE [Product ID] = '" + id + "'";
                    op.Update_By_DataGridView_Information(query);
                }

                op.Load_Table1(dataGridView1);
                op.Load_Table1(dataGridView4);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }



        private void Logout_button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (f.Name != "POS_Login_Form1")
                    {
                        f.Close();
                        new POS_Login_Form1().Show();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Change_Password_button2_Click(object sender, EventArgs e)
        {
            Change_Password_Form4 f = new Change_Password_Form4();
            f.Show();
        }


        private void Refresh_button2_Click(object sender, EventArgs e)
        {
            Load_Table2(dataGridView5);
        }

        private void Refresh_button1_Click(object sender, EventArgs e)
        {
            op.Load_Table1(dataGridView1);
        }

        private void Refresh_button3_Click(object sender, EventArgs e)
        {
            op.Load_Table1(dataGridView4);
        }

        public void Load_Table2(DataGridView dv)
        {
            string query = "SELECT * FROM Transaction_List_Table4;";
            SqlConnection Conn = op.create_connection();
            SqlCommand cmd = new SqlCommand(query, Conn);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                dataset = new DataTable();
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



        private void uID_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void uQuantity_textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void uBuy_Price_textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void uSell_Price_textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void ID_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Quantity_textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Buy_Price_textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void Sell_Price_textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dID_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void sID_textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void slID_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }






        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void Category_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void update_product_tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
