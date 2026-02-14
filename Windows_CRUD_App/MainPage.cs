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

        string dbConnection = "server=.;database=hardwareShopDB;integrated security = true;";
        public MainPage()
        {
            InitializeComponent();
            get_gender();
            loadData();
        }

      

        private void get_gender()
        {
            string query = "select Id,Gender from  tbl_Gender;";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                adapter.Fill(dt);
                cmb_gender.DataSource = dt;
                cmb_gender.DisplayMember = "Gender";
                cmb_gender.ValueMember = "Id";
                cmb_gender.SelectedIndex = -1;

            }
        }        

        // add custInfo

        private void btn_add_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            var gender = Convert.ToInt32(cmb_gender.SelectedValue);
            var phone = txt_phone.Text;

            add_customer(name, gender, phone);

            loadData();
        }


        // add coutInfo method
        private void add_customer(string name, int gender, string phone)
        {
            string query = @"insert into tbl_CustomerInfo(CustomerName,GenderId,phone)values(@CustomerName,@GenderId,@phone)";
            
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"CustomerName", name);
                cmd.Parameters.AddWithValue(@"GenderId", gender);
                cmd.Parameters.AddWithValue(@"phone", phone);

                int res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    MessageBox.Show("Data added Successfully");
                    clearFields();
                }
                else
                {
                    MessageBox.Show("Data sending failed. Please try again.");
                }
            }

        }



        // loading
        private void loadData()
        {
            string query = "SELECT * FROM tbl_CustomerInfo;";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query,connection))
            {
                connection.Open();
                adapter.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    dgv_customerInfo.DataSource = dt;
                }

                
            }
        }



        // edit custInfo
        private void btn_edit_Click(object sender, EventArgs e)
        {
            string custName = txt_name.Text;
            int genderId = Convert.ToInt32(cmb_gender.SelectedValue);
            var phone = txt_phone.Text;

            edit_customer(custName, genderId, phone);

            loadData();

        }


        // custInfo gridView
        int key = 0;
        private void dgv_customerInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                key = Convert.ToInt32(dgv_customerInfo.Rows[e.RowIndex].Cells["Id"].Value);
                txt_name.Text = dgv_customerInfo.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                cmb_gender.SelectedValue = dgv_customerInfo.Rows[e.RowIndex].Cells["GenderId"].Value;
                txt_phone.Text = dgv_customerInfo.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
            }

        }


        // edit custInfo method
        private void edit_customer(string custName , int genderId, string phone)
        {
            string query = @"update tbl_CustomerInfo set CustomerName = @CustomerName, GenderId = @GenderId, Phone = @Phone where Id = @Id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"Id", key);
                cmd.Parameters.AddWithValue(@"CustomerName", custName);
                cmd.Parameters.AddWithValue(@"GenderId", genderId);
                cmd.Parameters.AddWithValue(@"Phone", phone);

                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Edit complete");
                    clearFields();
                }
                else
                {
                    MessageBox.Show("Update failed. Try again.");
                }

            }
        }



        // delete custInfo
        private void btn_delete_Click(object sender, EventArgs e)
        {

            cust_delete();
            loadData();
        }


        private void cust_delete()
        {
            string query = @"DELETE FROM tbl_CustomerInfo WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", key);


                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Delete Successfully");
                    clearFields();
                    loadData();
                }
                else
                {
                    MessageBox.Show("Delete failed. Try again.");
                }
            }
        }

        private void clearFields()
        {
            txt_name.Text = "";
            cmb_gender.Text = "";
            txt_phone.Text = "";
            key = 0;
        }

        private void page_items_Click(object sender, EventArgs e)
        {
            Items items = new Items();
            items.Show();
            this.Hide();
        }

        private void page_category_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();
        }

        private void page_customer_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void page_billing_Click(object sender, EventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
