using DAO;
using Model;
using System;
using System.Windows.Forms;

namespace BUS
{
    public class UserBUS
    {
       
        private static UserBUS instance;
        public static UserBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserBUS();
                return instance;
            }
        }
        private UserBUS()
        {

        }
        public void See(DataGridView data)
        {
            data.DataSource = UserDAO.Instance.See();
        }
        public bool Update(int id, string account, string fname, string lname, string email)
        {
            User user = new User(id, account, fname, lname, email);
            return UserDAO.Instance.Update(user);
        }
        public bool Add(string account, string fname, string lname, string email)
        {
            User user = new User(account, fname, lname, email);
            return UserDAO.Instance.Add(user);

        }
        public bool Detete(int id )
        {
            return UserDAO.Instance.Delete(id);
        }
        public Exception ex { get; set; }
        public bool UserAvaiable(string Account, string FirstName, string LastName, string Email)
        {
            try
            {
                if (!isAvailable(Account))
                {
                    throw new Exception("account not avaliable!! ");
                }
                if (!isAvailable(FirstName))
                {
                    throw new Exception("firstname not avaliable!! ");
                }
                if (!isAvailable(LastName))
                {
                    throw new Exception("lastname not avaliable!! ");
                }
                if (!isAvailable(Email))
                {
                    throw new Exception("email not avaliable!! ");
                }
                return true;
            }

            catch (Exception ex)
            {
                this.ex = ex;
                //hasError = true;
                //MessageBox.Show(ex.Message.ToString(), "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public bool isAccountExists(string account)
        {
            return UserDAO.Instance.checkAccountExists(account);
        }
        
        private bool isAvailable(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return true;
        }
    }
}
