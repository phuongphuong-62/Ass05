using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_05Opt_1
{
    public partial class FormAddUser : Form
    {
        Ass05Entities db;
        bool hasError = true;
        public FormAddUser()
        {
            InitializeComponent();
            db = new Ass05Entities();
            dGV.DataSource = db.Users.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            User user = userAvaiable(false);
           
            if (hasError)return;
            db.Users.Add(user);
            db.SaveChanges();
            notifyDataChange();
            MessageBox.Show("Add Success", "Add", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(dGV.SelectedCells[0].OwningRow.Cells["Id"].Value.ToString());
            User user = db.Users.Find(id);
            if (!txtAccount.Text.ToString().Equals(user.Account))
            {
                MessageBox.Show("Not Change Account", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            User tempUser = userAvaiable(true);
            user.FirstName = tempUser.FirstName;
            user.LastName = tempUser.LastName;
            user.Email = tempUser.Email;
            if (hasError)
                return;
            db.SaveChanges();
            notifyDataChange();
            //todo success
            MessageBox.Show("Update success !!", "Update", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dGV.SelectedCells[0].OwningRow.Cells["Id"].Value.ToString());
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            notifyDataChange();
        }
        private User userAvaiable(bool isUpdate)
        {
            User user = new User();
            try
            {
                string account = txtAccount.Text.ToString();
                string FirstName = txtFirstName.Text.ToString();
                string LastName = txtLastName.Text.ToString();
                string Email = txtEmail.Text.ToString();

                // check error
                if(!isUpdate && isAccountExists(account))
                {
                    throw new Exception("account is Exists!! ");
                }
                if (!isAvailable(account))
                {
                    throw new Exception("account not avaliable!! ");
                }
                if (!isAvailable(FirstName))
                {
                    throw new Exception("FirstName not avaliable!! ");
                }
                if (!isAvailable(LastName))
                {
                    throw new Exception("LastName not avaliable!! ");
                }
                if (!isAvailable(Email))
                {
                    throw new Exception("Email not avaliable!! ");
                }
                hasError = false;
                user.Account = account;
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Email = Email;
                return user;
            }

            catch (Exception ex)
            {
                hasError = true;
                MessageBox.Show(ex.Message.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            
        }
        private bool isAvailable(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return true;
        }

        private bool isAccountExists(string account)
        {
            User user = db.Users.Where(x => x.Account.Equals(account)).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        private void dGV_SelectionChanged(object sender, EventArgs e)
        {
            if (dGV.SelectedCells.Count > 0)
            {
                int rowindex = dGV.CurrentCell.RowIndex;
                int columnindex = dGV.CurrentCell.ColumnIndex;
                txtAccount.Text = dGV.Rows[rowindex].Cells[1].Value.ToString();
                txtFirstName.Text = dGV.Rows[rowindex].Cells[2].Value.ToString();
                txtLastName.Text = dGV.Rows[rowindex].Cells[3].Value.ToString();
                txtEmail.Text = dGV.Rows[rowindex].Cells[4].Value.ToString();
            }

        }
        private void notifyDataChange()
        {
            dGV.DataSource = db.Users.ToList();
        }
    }
}
