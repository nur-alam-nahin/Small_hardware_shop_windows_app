using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_CRUD_App
{
    public partial class Create_account : Form
    {

        string dbConnection = "server=.;database=hardwareShopDB;integrated security = true;";
        public Create_account()
        {
            InitializeComponent();
        }

        private void btn_singup_Click_1(object sender, EventArgs e)
        {
            string userName = txt_userName.Text;
            string password = txt_password.Text;
            signUp(userName, password);
        }
        private void btn_singup_Click(object sender, EventArgs e)
        {
            
        }

        private void signUp(string userName,string password)
        {
            string query = @"insert into tbl_createAccount(UserName,UserPassword)values(@UserName,@UserPassword)";

            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                if(userName == "" || password == "")
                {

                    MessageBox.Show("Account Creation Failed");
                }
                else
                {
                    cmd.Parameters.AddWithValue(@"UserName", userName);
                    cmd.Parameters.AddWithValue(@"UserPassword", password);

                    int res = cmd.ExecuteNonQuery();

                    if(res > 0)
                    {
                        MessageBox.Show("Account Created Successfully");

                        Items items = new Items();
                        items.Show();
                        this.Hide();
                    }
                }
            }
        }

        

        private void line_log_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        //private void btn_singup_Click_1(object sender, EventArgs e)
        //{
        //    Items items = new Items();
        //    items.Show();
        //    this.Close();
        //}
    }
}
