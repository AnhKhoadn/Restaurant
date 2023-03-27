using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        
        public fAdmin()
        {
            InitializeComponent();
            LoadAccountList();
            LoadFoodlist();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            AddFoodBinding();
            LoadCategoryIntoCombobox(cbFoodCategory);
            LoadListTable();
            LoadListCategory();
            AddTableBinding();
            AddCategoryBinding();
        }
        void LoadFoodlist()
        {
            string query = "select * from Food";

            dgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        void LoadAccountList()
        {

            string query = "EXEC dbo.USP_GetAccountByUserName @userName";

            dgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "staff" });
        }


        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadListFood()
        {
            dgvFood.DataSource = FoodDAO.Instance.GetListFood();
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["IDCategory"].Value;

                Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                cbFoodCategory.SelectedItem = cateogory;

                int index = -1;
                int i = 0;
                foreach (Category item in cbFoodCategory.Items)
                {
                    if (item.ID == cateogory.ID)
                    { 
                        index = i;
                        break;
                    }
                    i++;
                }

                cbFoodCategory.SelectedIndex = index;
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm");
            }    
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi Xóa");
            }
        }


        void LoadListTable()
        {
            dgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        void AddTableBinding()
        {
            TxbTableName.DataBindings.Add(new Binding("text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbTableID.DataBindings.Add(new Binding("text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        void LoadListCategory()
        {
            dgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("text", dgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = TxbTableName.Text;


            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi Xóa");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = TxbTableName.Text;
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.UpdateTable(name, id))
            {
                MessageBox.Show("Sửa thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;


            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi Xóa");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.UpdateCategory(name, id))
            {
                MessageBox.Show("Sửa thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa");
            }
        }
    }
}
