using Model;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DAO
{
    public class UserDAO
    {

        private static UserDAO instance;
        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserDAO();
                return instance;
            }
        }

        private UserDAO()
        {

        }
        public List<User> See()
        {
            List<User> users = new List<User>();
            string query = "select * from [User]";
            DataTable dt = Provider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                int id = int.Parse(item["Id"].ToString());
                string account = item["Account"].ToString();
                string firstname = item["FirstName"].ToString();
                string lastname = item["LastName"].ToString();
                string email = item["Email"].ToString();
                User i = new User(id, account, firstname, lastname, email);
                users.Add(i);
            }
            return users;
        }
        public bool Add(User user)
        {
            string query = "INSERT INTO  [User](Account, FirstName, LastName, Email) Values( @account , @firstName , @lastName , @email )";
            object[] para = new object[] { user.Account, user.FirstName, user.LastName, user.Email };
            if (Provider.Instance.ExecuteNonQuery(query, para) > 0)
                return true;
            return false;
        }
        public bool Update(User user)
        {
            string query = "Update  [User] set account = @account , firstname = @firstname , lastname = @lastname , email = @email where id = @id ";
            object[] para = { user.Account, user.FirstName, user.LastName, user.Email, user.Id };
            if (Provider.Instance.ExecuteNonQuery(query, para) > 0)
                return true;

            return false;
        }

        public bool Delete(int Id)
        {
            string query = "DELETE FROM [User] WHERE id = @id ";
            object[] para = {Id };
            if (Provider.Instance.ExecuteNonQuery(query, para) > 0)
                return true;

            return false;
        }

        public bool checkAccountExists(string account)
        {
            string query = "select Account from [User] where Account = @Account ";
            object[] para = new object[] { account };
            DataTable dt = Provider.Instance.ExecuteQuery(query , para);
            string accounts = "";
            foreach (DataRow item in dt.Rows)
            {
                accounts = item["Account"].ToString();
            }
            return !string.IsNullOrEmpty(accounts);
        }
    }
}
