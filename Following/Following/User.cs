namespace Following
{
    public class User
    {
        private string Name { get; set; }
        private string eMail { get; set; }
        private string Password { get; set; }
        public User(string name, string email, string password)
        {
            Name = name;
            eMail = email;
            Password = password;
        }
    }
}
