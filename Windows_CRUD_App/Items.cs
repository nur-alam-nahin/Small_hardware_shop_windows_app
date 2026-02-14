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
    public partial class Items : Form
    {
        string dbConnection = "server=.;database=hardwareShopDB;integrated security = true;";
        public Items()
        {
            InitializeComponent();
            get_category();
            loadData();


        }




        // loade category

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
        




        // add item
        private void btn_add_Click(object sender, EventArgs e)
        {
            string ItemName = txt_items.Text;
            int CategoryId = Convert.ToInt32(cmb_category.SelectedValue);
            var price = txt_price.Text;
            var stock = txt_stock.Text;
            string menufecture = txt_manufecture.Text;

            item_add(ItemName, CategoryId, price, stock, menufecture);

            loadData();
        }


      


        // add item method
        private void item_add(string itemName,int CategoryId, string price,string stock, string menufecture)
        {
            string query = @"insert into tbl_Items(ItemName,CategoryId,Price,Stock,Menufecture)values(@ItemName,@CategoryId,@Price,@Stock,@Menufecture)";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query,connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"ItemName", itemName);
                cmd.Parameters.AddWithValue(@"CategoryId", CategoryId);
                cmd.Parameters.AddWithValue(@"Price", price);
                cmd.Parameters.AddWithValue(@"Stock", stock);
                cmd.Parameters.AddWithValue(@"Menufecture", menufecture);

                int res = cmd.ExecuteNonQuery();

                if(res > 0)
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


       


        int key = 0;


        // grid click
        private void dgv_ItemsInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                key = Convert.ToInt32(dgv_ItemsInfo.Rows[e.RowIndex].Cells["Id"].Value);
                txt_items.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
                cmb_category.SelectedValue = dgv_ItemsInfo.Rows[e.RowIndex].Cells["CategoryId"].Value;
                txt_price.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                txt_stock.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["Stock"].Value.ToString();
                txt_manufecture.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["Menufecture"].Value.ToString();
            }
        }


        // edit item
        private void btn_edit_Click(object sender, EventArgs e)
        {
            string ItemName = txt_items.Text;
            int CategoryId = Convert.ToInt32(cmb_category.SelectedValue);
            var price = txt_price.Text;
            var stock = txt_stock.Text;
            string menufecture = txt_manufecture.Text;

            item_edit(ItemName, CategoryId, price, stock, menufecture);

            loadData();

            
        }


        // edit item method
        private void item_edit(string itemName, int categoryId, string price, string stock, string menufecture)
        {
            string query = @"update tbl_Items set ItemName = @ItemName, CategoryId = @CategoryId, Price = @Price, Stock = @Stock, Menufecture = @Menufecture where Id = @Id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"Id", key);
                cmd.Parameters.AddWithValue(@"ItemName", itemName);
                cmd.Parameters.AddWithValue(@"CategoryId", categoryId);
                cmd.Parameters.AddWithValue(@"Price", price);
                cmd.Parameters.AddWithValue(@"Stock", stock);
                cmd.Parameters.AddWithValue(@"Menufecture", menufecture);

                int res = cmd.ExecuteNonQuery();
                if(res > 0)
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


        // delete item
        private void btn_delete_Click(object sender, EventArgs e)
        {

            item_delete();
            loadData();

        }


        private void item_delete()
        {
            string query = @"DELETE FROM tbl_Items WHERE Id = @Id;";
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
                }
                else
                {
                    MessageBox.Show("Delete failed. Try again.");
                }
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        // load item
        private void loadData()
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
                    dgv_ItemsInfo.DataSource = dt;
                }
            }

        }
        private void clearFields()
        {
            txt_items.Text = "";
            cmb_category.Text = "";
            txt_price.Text = "";
            txt_stock.Text = "";
            txt_manufecture.Text = "";
            key = 0;
        }




      

        private void page_items_Click_1(object sender, EventArgs e)
        {
            this.Show();
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
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }
    }
}
