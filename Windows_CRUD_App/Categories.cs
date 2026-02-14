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
    public partial class Categories : Form
    {
        string dbConnection = "server=.;database=hardwareShopDB;integrated security = true;";
        public Categories()
        {
            InitializeComponent();
            loadeData();
        }


        //  add item
        private void btn_add_Click(object sender, EventArgs e)
        {
            string categoryName = txt_name.Text;

            
            //dgv_category.Refresh();

            add_category(categoryName);

            loadeData();
        }


        // add item method
        private void add_category(string name)
        {
            string query = @"insert into tbl_Category(CategoryName)values(@CategoryName)";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"CategoryName", name);

                int res = cmd.ExecuteNonQuery();

                if(res > 0)
                {
                    MessageBox.Show("Category added");
                }
                else
                {
                    MessageBox.Show("Data sending failed. Please try again.");
                }
            }
        }



        // loade item

        private void loadeData()
        {
            string query = "SELECT * FROM tbl_Category;";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dgv_category.DataSource = dt;
                }

            }
        }



        int key = 0;


        // grid click
        private void dgv_category_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgv_category.CurrentRow.Selected = true;
            //txt_name.Text = dgv_category.SelectedRows[0].Cells[1].Value.ToString();

            if (e.RowIndex >= 0)
            {
                txt_name.Text = dgv_category.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
                key = Convert.ToInt32(dgv_category.Rows[e.RowIndex].Cells["Id"].Value);
            }

            //if (dgv_category.SelectedRows.Count > 0)
            //{

            //    //txt_name.Text = dgv_category.SelectedRows[0].Cells["CategoryName"].Value.ToString();
            //    //key = Convert.ToInt32(dgv_category.SelectedRows[0].Cells["Id"].Value);
            //    txt_name.Text = dgv_category.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
            //    key = Convert.ToInt32(dgv_category.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            //}

        }


        // edit item
        private void btn_edit_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            edit_category(name);

            loadeData();
        }



        // edit item method
        private void edit_category(string name)
        {
            string query = @"UPDATE tbl_Category set CategoryName = @CategoryName where Id = @Id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"Id", key);
                cmd.Parameters.AddWithValue(@"CategoryName", name);

                int res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    MessageBox.Show("Category data edit ");
                    txt_name.Text = "";
                    key = 0;
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
            string query = @"DELETE FROM tbl_Category WHERE id = @id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                cmd.Parameters.AddWithValue(@"Id", key);
                //cmd.Parameters.AddWithValue(@"CategoryName", name);

                int res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    MessageBox.Show("Delete Successfully");
                    txt_name.Text = "";
                }
                else
                {
                    MessageBox.Show("Delete failed. Try again.");
                }
            }

            loadeData();
            
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
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }
    }
}
