using System;
using System.Windows.Forms;
using BUS;

namespace Ass05_Opt1
{

    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }
        private void btnSee_Click(object sender, EventArgs e)
        {
            UserBUS.Instance.See(dgv);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string account = txtAccount.Text;
            string firstName = txtFirstname.Text;
            string lastName = txtLastname.Text;
            string email = txtEmail.Text;
            if (UserBUS.Instance.UserAvaiable(account, firstName, lastName, email))
            {
                if (UserBUS.Instance.isAccountExists(account))
                {
                    MessageBox.Show("User alrealy exists", "notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (UserBUS.Instance.Add(account, firstName, lastName, email))
                {
                    MessageBox.Show("Add success", "notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnSee_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Error", "notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(UserBUS.Instance.ex.Message, "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgv.SelectedCells[0].OwningRow;
            int id = int.Parse(row.Cells["Id"].Value.ToString());

            string account = txtAccount.Text;
            string firstName = txtFirstname.Text;
            string lastName = txtLastname.Text;
            string email = txtEmail.Text;
            if (UserBUS.Instance.UserAvaiable(account, firstName, lastName, email))
            {
                if (UserBUS.Instance.isAccountExists(account))
                {
                    if (UserBUS.Instance.Update(id, account, firstName, lastName, email))
                    {
                        MessageBox.Show("Updated success", "notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnSee_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Error", "notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("User not alrealy exists", "notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show(UserBUS.Instance.ex.Message, "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dvg_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedCells.Count > 0)
            {
                int rowindex = dgv.CurrentCell.RowIndex;
                int columnindex = dgv.CurrentCell.ColumnIndex;
                txtAccount.Text = dgv.Rows[rowindex].Cells[1].Value.ToString();
                txtFirstname.Text = dgv.Rows[rowindex].Cells[2].Value.ToString();
                txtLastname.Text = dgv.Rows[rowindex].Cells[3].Value.ToString();
                txtEmail.Text = dgv.Rows[rowindex].Cells[4].Value.ToString();
            }

        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            UserBUS.Instance.See(dgv);
        }

        private void btnDelect_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = dgv.SelectedCells[0].OwningRow;
            int id = int.Parse(row.Cells["Id"].Value.ToString());

            DialogResult dialogResult = MessageBox.Show("you want to delete ? ", "notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (UserBUS.Instance.Detete(id))
                {
                    btnSee_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Error", "notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }

        }
    }
}
