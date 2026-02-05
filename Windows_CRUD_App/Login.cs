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
    public partial class Login : Form
    {

        string dbConnection = "server=.;Database=customerDB; Integrated Security = true;";
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string userName = txt_userName.Text;
            string password = txt_password.Text;

            login(userName, password);
        }

        private void login(string userName,string password)
        {
            //string query = @"select * from tbl_createAccount;";
            string query = @"select UserName ,   Password from tbl_createAccount where (UserName = @UserName and   Password = @Password);";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"UserName", userName);
                cmd.Parameters.AddWithValue(@"Password", password);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        MainPage mainPage = new MainPage();
                        mainPage.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed");
                    }
                }

                cmd.ExecuteNonQuery();
            }
        }

        private void link_createAC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Create_account create_Account = new Create_account();
            create_Account.Show();
            this.Hide();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
