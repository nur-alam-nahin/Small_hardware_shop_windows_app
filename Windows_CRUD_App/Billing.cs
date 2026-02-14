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
    public partial class Billing : Form
    {

        string dbConnection = "server=.;database=hardwareShopDB;integrated security = true;";
        public Billing()
        {
            InitializeComponent();
            get_category();
            loadItems();
        }



        private void get_category()
        {
            string query = "SELECT Id, CategoryName FROM tbl_Category";

            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                adapter.Fill(dt);
                cmb_category.DataSource = dt;
                cmb_category.DisplayMember = "CategoryName";
                cmb_category.ValueMember = "Id";
                cmb_category.SelectedIndex = -1;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string items = txt_items.Text;
            var category = Convert.ToInt32(cmb_category.SelectedValue);
            string menufecture = txt_menufecture.Text;
            var price = txt_price.Text;

            add_bill(items, category, menufecture, price);
            loadClient();


        }



        private void add_bill(string items, int category, string menufecture, string price)
        {
            string query = "insert into tbl_ClientBill(ItemName,CategoryId,Menufecture,Price) values(@ItemName,@CategoryId,@Menufecture,@Price)";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"ItemName", items);
                cmd.Parameters.AddWithValue(@"CategoryId", category);
                cmd.Parameters.AddWithValue(@"Menufecture", menufecture);
                cmd.Parameters.AddWithValue(@"Price", price);

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


        

        private void loadItems()
        {
            string query = "SELECT * FROM tbl_Items;";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dgv_ITemsList.DataSource = dt;
                }


            }
        }


        private void loadClient()
        {
            string query = "select * from tbl_ClientBill;";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    dgv_clientBill.DataSource = dt;
                }
                
            }
        }



        int key = 0;
        private void dgv_ITemsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                key = Convert.ToInt32(dgv_ITemsList.Rows[e.RowIndex].Cells["Id"].Value);
                txt_items.Text = dgv_ITemsList.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
                cmb_category.SelectedValue = dgv_ITemsList.Rows[e.RowIndex].Cells["CategoryId"].Value;
                txt_menufecture.Text = dgv_ITemsList.Rows[e.RowIndex].Cells["Menufecture"].Value.ToString();
                txt_price.Text = dgv_ITemsList.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                



            }
        }
     

        private void btn_reset_Click(object sender, EventArgs e)
        {
            dgv_clientBill.ClearSelection();
        }
        
        private void reset()
        {
            string query = @"delete from ";
        }

        private void clearFields()
        {
            txt_items.Text = "";
            cmb_category.Text = "";
            txt_menufecture.Text = "";
            txt_price.Text = "";
            key = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }




      

        private void page_items_Click_1(object sender, EventArgs e)
        {
            Items items = new Items();
            items.Show();
            this.Hide();
        }

        private void page_category_Click_1(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
            this.Hide();
        }

        private void page_customer_Click_1(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }

        private void page_billing_Click_1(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
