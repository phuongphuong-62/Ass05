
namespace Model
{
   public class User
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public User()
        {
        }
        public User(int id, string account, string firstname, string lastname, string email)
        {
            this.Id = id;
            this.Account = account;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;

        }

        public User( string account, string firstname, string lastname, string email)
        {
            this.Account = account;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;

        }
    }
}
