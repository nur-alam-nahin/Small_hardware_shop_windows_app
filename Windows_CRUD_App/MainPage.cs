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
    public partial class MainPage : Form
    {

        string dbConnection = "server=.;Database=customerDB; Integrated Security = true;";
        public MainPage()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            var gender = cmb_gender.Text;
            var phone = txt_phone.Text;
        }

        private void insertData(string name,string gender,string phone)
        {
            string query = @"insert into tbl_customerInfo(CustomerName,CustomerGender,phone)values(@CustomerName,@CustomerGender,@phone)";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"CustomerName", name);
                cmd.Parameters.AddWithValue(@"CustomerGender", gender);
                cmd.Parameters.AddWithValue(@"phone", phone);

                int res = cmd.ExecuteNonQuery();
                if(res > 0)
                {
                    
                }
            }

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
