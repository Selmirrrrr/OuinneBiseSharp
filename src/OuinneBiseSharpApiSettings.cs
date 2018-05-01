namespace Bizy.OuinneBiseSharp
{
    public struct OuinneBiseApiSettings
    {
        public string Key { get; }
        public string Company { get; }
        public string Username { get; }
        public string Password { get; }
        public string EncryptionKey { get; }
        public string Url { get; }

        public OuinneBiseApiSettings(string winbizApiKey, string winbizApiCompany, string winbizApiUsername, string winbizApiPassword, string encryptionKey, string url)
        {
            Key = winbizApiKey;
            Company = winbizApiCompany;
            Username = winbizApiUsername;
            Password = winbizApiPassword;
            EncryptionKey = encryptionKey;
            Url = url;
        }
    }
}