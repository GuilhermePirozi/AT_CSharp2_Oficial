namespace AT_CSharp2_Oficial.Services {
    public static class LoginService {
        public const string AdminLogin = "admin";
        public const string AdminPassword = "123";

        public static bool IsValidLogin(string login, string senha) =>
            login == AdminLogin && senha == AdminPassword;
    }
}