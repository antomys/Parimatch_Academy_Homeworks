namespace Task_3
{
    public class LoginCredentials
    {
        private string Login { get;}
        private string Password { get; }

        protected internal LoginCredentials(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return $"Login: {Login}, Password: {Password}";
        }

        public string GetLogin()
        {
            return Login;
        }

        public string GetPassword()
        {
            return Password;
        }
    }
}