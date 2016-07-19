using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using BO;
namespace POS
{
    public partial class POS_System_Form3 : Form
    {
        public POS_System_Form3()
        {
            InitializeComponent();
            timer1.Start();
            op.Load_Table1(dataGridView6);
            Load_Table2(dataGridView3);
        }
        DataTable dataset;
        Operation op = new Operation();

        private void slID_textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView6.DataSource;
                bs.Filter = " [Product ID] = " + "'" + slID_textBox2.Text.ToString() + "'";
                dataGridView6.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Receipt_No_textBox2_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dataset);
                dv.RowFilter = string.Format("[Receipt No.] LIKE '%{0}%'", Receipt_No_textBox2.Text);
                dataGridView3.DataSource = dv;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Logout_button1_Click_1(object sender, EventArgs e)
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
            /*POS_Login_Form1 lf = new POS_Login_Form1();
            this.Hide();
            lf.Show();*/
        }

        private void Change_Password_button2_Click_1(object sender, EventArgs e)
        {
            Change_Password_Form4 f = new Change_Password_Form4();
            f.Show();
        }

        string temp1, temp2;
        private void Search_button1_Click(object sender, EventArgs e)
        {
            if(ID_textBox1.Text.Trim().Length > 0)
            {
                try
                {
                    String query = "SELECT * FROM [Stock List Table3]";
                    SqlConnection Conn = op.create_connection();
                    SqlCommand cd = new SqlCommand(query, Conn);
                    SqlDataReader reader = cd.ExecuteReader();

                    while (reader.Read())
                    {
                        temp1 = reader["Product ID"].ToString();
                        if (ID_textBox1.Text.Equals(temp1))
                        {
                            temp2 = temp1;
                            break;
                        }
                    }

                    if (temp1 == temp2)
                    {
                        Name_textBox2.Text = reader["Product Name"].ToString();
                        Unit_Price_textBox6.Text = reader["Sell Price"].ToString();
                        QTY_textBox5.Text = "1";
                        Total_Price_textBox7.Text = Unit_Price_textBox6.Text;
                        
                    }

                    else
                    {
                        MessageBox.Show("No Match Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ID_textBox1.Text = "";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else
            {
                MessageBox.Show("Please Give Your Product ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void TotalPrice()
        {
            decimal quantity;
            decimal unitPrice;

            try
            {
                decimal.TryParse(QTY_textBox5.Text, out quantity);
                decimal.TryParse(Unit_Price_textBox6.Text, out unitPrice);
                Total_Price_textBox7.Text = (quantity * unitPrice).ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Increment_button3_Click(object sender, EventArgs e)
        {
            decimal quantity;
            decimal.TryParse(QTY_textBox5.Text, out quantity);
            try
            {
                if (quantity >= 0)
                {
                    quantity++;
                }
                QTY_textBox5.Text = quantity.ToString();
                TotalPrice();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Decrement_button4_Click(object sender, EventArgs e)
        {
            decimal quantity;
            decimal.TryParse(QTY_textBox5.Text, out quantity);
            try
            {
                if (quantity > 1)
                {
                    quantity--;
                }
                QTY_textBox5.Text = quantity.ToString();
                TotalPrice();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Sub_Total()
        {
            decimal Total = 0;
            try
            {

                for (int i = 0; i < dataGridView7.Rows.Count; i++)
                {
                    Total += Convert.ToDecimal(dataGridView7.Rows[i].Cells[4].Value);
                }

                Sub_Total_textBox1.Text = Total.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Total()
        {
            try
            {
                if (Vat_comboBox1.Text != null || Discount_comboBox2.Text != null)
                {
                    if (Vat_comboBox1.Text == null && Discount_comboBox2.Text != null)
                    {
                        Vat_comboBox1.Text = "0";
                    }
                    else if (Vat_comboBox1.Text != null && Discount_comboBox2.Text == null)
                    {
                        Discount_comboBox2.Text = "0";
                    }
                    float subTotal;
                    float vat;
                    float discount;
                    float sum;
                    float.TryParse(Sub_Total_textBox1.Text, out subTotal);
                    float.TryParse(Vat_comboBox1.Text, out vat);
                    float.TryParse(Discount_comboBox2.Text, out discount);
                    vat = (vat * subTotal) / 100;
                    discount = (discount * subTotal) / 100;
                    sum = subTotal + vat - discount;
                    Total_textBox4.Text = sum.ToString();
                }

                else
                {
                    Total_textBox4.Text = Sub_Total_textBox1.Text;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Vat_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Total();
        }

        private void Discount_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Total();
        }

        private void Add_button2_Click(object sender, EventArgs e)
        {
            try
            {

                string firstColum = ID_textBox1.Text;
                string secondColum = Name_textBox2.Text;
                string thirdColum = QTY_textBox5.Text;
                string fourthColum = Unit_Price_textBox6.Text;
                string fifthColum = Total_Price_textBox7.Text;
                string[] row = { firstColum, secondColum, thirdColum, fourthColum, fifthColum };
                dataGridView7.Rows.Add(row);

                Sub_Total();
                Total();
                ID_textBox1.Text = "";
                Name_textBox2.Text = "";
                QTY_textBox5.Text = "";
                Unit_Price_textBox6.Text = "";
                Total_Price_textBox7.Text = "";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Delete_button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewCell oneCell in dataGridView7.SelectedCells)
                {
                    if (oneCell.Selected)
                    {
                        dataGridView7.Rows.RemoveAt(oneCell.RowIndex);
                    }
                }
                Sub_Total();
                Total();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_All_button2_Click(object sender, EventArgs e)
        {
            dataGridView7.Rows.Clear();
            Sub_Total_textBox1.Text = "";
            Vat_comboBox1.Text = "";
            Discount_comboBox2.Text = "";
            Total_textBox4.Text = "";
        }

        private void Print_button3_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < dataGridView7.Rows.Count; index++)
            {
                int editIndex = dataGridView2.Rows.Add();
                dataGridView2.Rows[editIndex].Cells[0].Value = dataGridView7.Rows[index].Cells[0].Value;
                dataGridView2.Rows[editIndex].Cells[1].Value = dataGridView7.Rows[index].Cells[1].Value;
                dataGridView2.Rows[editIndex].Cells[2].Value = dataGridView7.Rows[index].Cells[2].Value;
                dataGridView2.Rows[editIndex].Cells[3].Value = dataGridView7.Rows[index].Cells[3].Value;
                dataGridView2.Rows[editIndex].Cells[4].Value = dataGridView7.Rows[index].Cells[4].Value;
            }

            Guid guid = Guid.NewGuid();
            string str = guid.ToString();
            Receipt_No_textBox4.Text = str;

            if (Vat_comboBox1.Text == null && Discount_comboBox2.Text != null)
            {
                textBox2.Text = "0 %";
            }
            else if (Vat_comboBox1.Text != null && Discount_comboBox2.Text == null)
            {
                textBox3.Text = "0 %";
            }
            else
            {
                textBox2.Text = Vat_comboBox1.Text + " %";
                textBox3.Text = Discount_comboBox2.Text + " %";
            }

            textBox1.Text = Sub_Total_textBox1.Text + " TK";
            textBox4.Text = Total_textBox4.Text + " TK";

            dataGridView7.Rows.Clear();
            Sub_Total_textBox1.Text = "";
            Vat_comboBox1.Text = "";
            Discount_comboBox2.Text = "";
            Total_textBox4.Text = "";
            Receipt_tabPage2.Show();
            POS_System_tabPage1.Hide();

        }

        private void ID_textBox1_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = Search_button1;
        }




        private void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Bitmap bmp = new Bitmap(panel2.Width, panel2.Height, panel2.CreateGraphics());
            panel2.DrawToBitmap(bmp, new Rectangle(0, 0, panel2.Width, panel2.Height));

            RectangleF bounds = e.PageSettings.PrintableArea;
            e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, panel2.Width, panel2.Height);

        }

        public void Print()
        {
            System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
            PrintDialog PrintSettings = new PrintDialog();
            PrintSettings.Document = doc;
            PageSettings pgsetting = new PageSettings();

            if (PrintSettings.ShowDialog() == DialogResult.OK)
                doc.Print();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.time_lbl.Text = dateTime.ToString();
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



        private void Save_Print_button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Receipt_No = Receipt_No_textBox4.Text;
                string Cname = cName_textBox1.Text;
                string Cphone = cPhone_textBox2.Text;
                string Caddress = cAddress_textBox3.Text;
                string datetime = time_lbl.Text;
                string subTotal = textBox1.Text;
                string vat = textBox2.Text;
                string discount = textBox3.Text;
                string total = textBox4.Text;

                for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                {
                    string id = this.dataGridView2.Rows[i].Cells[0].Value.ToString();
                    string name = this.dataGridView2.Rows[i].Cells[1].Value.ToString();
                    string quantity = this.dataGridView2.Rows[i].Cells[2].Value.ToString();
                    op.Update_DataBase_Quantity(id, quantity);
                    string unitprice = this.dataGridView2.Rows[i].Cells[3].Value.ToString();
                    string Tprice = this.dataGridView2.Rows[i].Cells[4].Value.ToString();
                    String query = "INSERT INTO Transaction_List_Table4 ([Receipt No.],[Customer Name],[Customer Address],[Customer Phone],[Product ID],[Product Name],Quantity,[Unit Price],TPrice,[Sub Total],VAT,DISCOUNT,TOTAL,[Date Time]) VALUES('" + Receipt_No + "','" + Cname + "','" + Caddress + "','" + Cphone + "','" + id + "','" + name + "','" + quantity + "','" + unitprice + "','" + Tprice + "','" + subTotal + "','" + vat + "','" + discount + "','" + total + "','" + datetime + "')";

                    op.Save_Transaction_Information(query);

                }
                Print();
                op.Load_Table1(dataGridView6);
                Load_Table2(dataGridView3);
                dataGridView2.Rows.Clear();
                Receipt_No_textBox4.Text = "";
                cName_textBox1.Text = "";
                cPhone_textBox2.Text = "";
                cAddress_textBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

                POS_System_tabPage1.Show();
                Receipt_tabPage2.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Refresh_button1_Click(object sender, EventArgs e)
        {
            Load_Table2(dataGridView3);
        }

        private void Refresh_button2_Click(object sender, EventArgs e)
        {
            op.Load_Table1(dataGridView6);
        }

        private void slID_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void cPhone_textBox2_KeyPress(object sender, KeyPressEventArgs e)
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



        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void time_lbl_Click(object sender, EventArgs e)
        {

        }

        private void POS_System_Form3_Load(object sender, EventArgs e)
        {

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Receipt_tabPage2_Click(object sender, EventArgs e)
        {

        }


    }
}
