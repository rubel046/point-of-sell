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
    public partial class POS_Login_Form1 : Form
    {
        public POS_Login_Form1()
        {
            InitializeComponent();
            password_textBox2.PasswordChar = '*' ;
            password_textBox2.MaxLength = 15 ;
        }

        Operation m = new Operation();
        string temp1, temp2;
        POS_System_Form2 pos1 = new POS_System_Form2();
        POS_System_Form3 pos2 = new POS_System_Form3();
        private void authority_login_button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM [Authority Login Table1]";
                SqlConnection Conn = m.create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                SqlDataReader rdr = cmd.ExecuteReader(); ;
                while (rdr.Read())
                {
                    temp1 = rdr["user name"].ToString();
                    temp2 = rdr["password"].ToString();
                }

                if (user_name_textBox1.Text.Equals(temp1) && password_textBox2.Text.Equals(temp2))
                {
                    MessageBox.Show("Login Sucessfull.Click OK to Continue", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();     //hide the login form
                    // MessageBox.Show("Welcome To The Point Of Sell Service", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pos1.Show(); //& show the main form 
                }

                else
                {
                    MessageBox.Show("Wrong Username or Password! \n Please Try Again!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    user_name_textBox1.Text = ""; //delete written password
                    password_textBox2.Text = ""; //delete written username
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void user_login_button2_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM [User Login Table2]";
                SqlConnection Conn = m.create_connection();
                SqlCommand cmd = new SqlCommand(query, Conn);
                SqlDataReader rdr = cmd.ExecuteReader(); ;
                while (rdr.Read())
                {
                    temp1 = rdr["user name"].ToString();
                    temp2 = rdr["password"].ToString();
                }

                if (user_name_textBox1.Text.Equals(temp1) && password_textBox2.Text.Equals(temp2))
                {
                    MessageBox.Show("Login Sucessfull.Click OK to Continue", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();     //hide the login form
                    // MessageBox.Show("Welcome To The Point Of Sell Service", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pos2.Show(); //& show the main form 
                }

                else
                {
                    MessageBox.Show("Wrong Username or Password! \n Please Try Again!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    user_name_textBox1.Text = ""; //delete written password
                    password_textBox2.Text = ""; //delete written username
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void password_textBox2_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = user_login_button2;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void user_name_textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
