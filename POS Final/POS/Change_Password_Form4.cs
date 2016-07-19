using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BO;
using System.Data.SqlClient;

namespace POS
{
    public partial class Change_Password_Form4 : Form
    {
        public Change_Password_Form4()
        {
            InitializeComponent();
            Current_Password_textBox1.PasswordChar = '*';
            Current_Password_textBox1.MaxLength = 15;
            New_Password_textBox2.PasswordChar = '*';
            New_Password_textBox2.MaxLength = 15;
            Confirm_Password_textBox3.PasswordChar = '*';
            Confirm_Password_textBox3.MaxLength = 15;
        }
        Operation m = new Operation();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        string temp1,temp2;
        //int count = 0;


        private void button1_Click(object sender, EventArgs e)
        {
            string current_password = Current_Password_textBox1.Text;
            string new_password = New_Password_textBox2.Text;
            string confirm_password = Confirm_Password_textBox3.Text;
            string new_user_name = New_User_Name_textBox4.Text;
            if (new_password.Trim() != confirm_password.Trim())
            {
                MessageBox.Show("New Password & Confirm Password Doesn't Match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                New_Password_textBox2.Text = "";
                Confirm_Password_textBox3.Text = "";
            }
            else
            {
                if (Authority_radioButton1.Checked)
                {
                    string query = "SELECT * FROM [Authority Login Table1] ";
                    SqlConnection Conn = m.create_connection();
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    SqlDataReader rdr = cmd.ExecuteReader(); ;
                    while (rdr.Read())
                    {
                        temp1 = rdr["user name"].ToString();
                        temp2 = rdr["password"].ToString();
                    }

                    Conn.Close();


                    if ((current_password == temp2))
                    {
                        if (new_user_name == "")
                        {
                            string query1 = "UPDATE [Authority Login Table1] SET password = '" + new_password + "'";
                            m.UpdatePassword(query1);
                            this.Close();
                        }


                        else
                        {
                            string query1 = "UPDATE [Authority Login Table1] SET [user name] = '" + new_user_name + "',password = '" + new_password + "'";
                            m.UpdatePassword(query1);
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Current Password is Incorrect !!!!\nPlease Try Again With Correct Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Current_Password_textBox1.Text = "";
                    }
                }

                else if (User_radioButton2.Checked)
                {
                    string query = "SELECT * FROM [User Login Table2] ";
                    SqlConnection Conn = m.create_connection();
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    SqlDataReader rdr = cmd.ExecuteReader(); ;
                    while (rdr.Read())
                    {
                        temp1 = rdr["user name"].ToString();
                        temp2 = rdr["password"].ToString();
                    }

                    Conn.Close();


                    if ((current_password == temp2))
                    {
                        if (new_user_name == "")
                        {
                            string query1 = "UPDATE [User Login Table2] SET password = '" + new_password + "'";
                            m.UpdatePassword(query1);
                            this.Close();
                        }


                        else
                        {
                            string query1 = "UPDATE [User Login Table2] SET [user name] = '" + new_user_name + "',password = '" + new_password + "'";
                            m.UpdatePassword(query1);
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Current Password is Incorrect !!!!\nPlease Try Again With Correct Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Current_Password_textBox1.Text = "";
                    }
                }

                else
                {
                    MessageBox.Show("Select Authority or User.\nTry Again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
            }
        }

        private void Cancel_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
