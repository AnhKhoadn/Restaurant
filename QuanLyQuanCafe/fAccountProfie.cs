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
    public partial class fAccountProfie : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public fAccountProfie(Account acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txbUserName.Text = loginAccount.UserName;
            txbDisplayName.Text = loginAccount.DisplayName;

        }

        void UpdateAccount()
        {
            string displayName = txbDisplayName.Text;
            string password = txbPassword.Text;
            string newpass = txbNewPass.Text;
            string reenterPass = txbReEnterPass.Text;
            string userName = txbUserName.Text;

            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
}
