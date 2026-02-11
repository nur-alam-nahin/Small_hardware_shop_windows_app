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
        string dbConnection = "server=.;database=hardwareShopDB;Integrated security = true;";
        public Items()
        {
            InitializeComponent();
            //load_category();
            loadDate();


        }

        

       

        private void get_category()
        {
            string query = "select Id, CategoryName from tbl_Category";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                connection.Open();
                adapter.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    cmb_category.DataSource = dt;
                    cmb_category.DisplayMember = "CategoryName";
                    cmb_category.ValueMember = "Id";
                    cmb_category.SelectedIndex = -1;
                }
            }
            

        }
        //private void load_category()
        //{
        //    DataTable dt = get_category();
        //    cmb_category.DataSource = dt;
        //    cmb_category.DisplayMember = "CategoryName";
        //    cmb_category.ValueMember = "Id";
        //    cmb_category.SelectedIndex = -1;

        //}

        

        

        private void btn_add_Click(object sender, EventArgs e)
        {
            string ItemName = txt_items.Text;
            int CategoryId = Convert.ToInt32(cmb_category.SelectedValue);
            var price = txt_price.Text;
            var stock = txt_stock.Text;
            string menufecture = txt_manufecture.Text;

            item_add(ItemName, CategoryId, price, stock, menufecture);
            get_category();
            loadDate();
        }


      



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
                }
                else
                {
                    MessageBox.Show("Data sending failed. Please try again.");
                }



            }
        }

        private void loadDate()
        {
            string query = "SELECT * FROM tbl_Items;";
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query,connection))
            {
                connection.Open();
                adapter.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    dgv_ItemsInfo.DataSource = dt;
                }
            }

        }


        int key = 0;
        private void dgv_ItemsInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > 0)
            {
                txt_items.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
                cmb_category.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["CategoryId"].Value.ToString();
                txt_price.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                txt_stock.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["Stock"].Value.ToString();
                txt_manufecture.Text = dgv_ItemsInfo.Rows[e.RowIndex].Cells["Menufecture"].Value.ToString();
            }

        }


        private void btn_edit_Click(object sender, EventArgs e)
        {
            string ItemName = txt_items.Text;
            var Category = cmb_category.Text;
            var price = txt_price.Text;
            var stock = txt_stock.Text;
            string menufecture = txt_manufecture.Text;

            item_edit(ItemName, Category, price, stock, menufecture);

            loadDate();

            
        }

        private void item_edit(string itemName, string category, string price, string stock, string menufecture)
        {
            string query = @"update tbl_Items set ItemName = @ItemName, CategoryId = @CategoryId, Price = @Price, Stock = @Stock Menufecture = @Menufecture where Id = @Id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"ItemName", itemName);
                cmd.Parameters.AddWithValue(@"CategoryId", category);
                cmd.Parameters.AddWithValue(@"Price", price);
                cmd.Parameters.AddWithValue(@"Stock", stock);
                cmd.Parameters.AddWithValue(@"Menufecture", menufecture);

                int res = cmd.ExecuteNonQuery();
                if(res > 0)
                {
                    MessageBox.Show("Edit complete");
                    txt_items.Text = "";
                    cmb_category.Text = "";
                    txt_price.Text = "";
                    txt_manufecture.Text = "";
                    key = 0;
                }
                else
                {
                    MessageBox.Show("Update failed. Try again.");
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string query = @"DELETE FROM tbl_Items WHERE id = @id;";
            using (SqlConnection connection = new SqlConnection(dbConnection))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue(@"id", key);
               

                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Delete Successfully");
                    txt_items.Text = "";
                    cmb_category.Text = "";
                    txt_price.Text = "";
                    txt_manufecture.Text = "";
                    key = 0;
                }
                else
                {
                    MessageBox.Show("Delete failed. Try again.");
                }
            }
        }

       
    }
}
