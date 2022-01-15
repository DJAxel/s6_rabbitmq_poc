using System;

namespace UserSecretService.domain
{
    public class UserSecret
    {
        public User User { get; set; }
        public string Secret { get; set; }

        public UserSecret(string userName)
        {
            this.User = new User(userName);
            this.Secret = UserSecret.generate(userName);
        }

        public static string generate(string UserName)
        {
            String[] secrets = new String[] {
                "{0} has a secret affair with {1}",
                "{0} has borrowed {1}'s pen, but not returned it",
                "{0} peeked during last Friday's exam",
                "{0} is not wearing any underwear",
                "{0} should have done the dishes three days ago",
                "{0} listens to Justin Bieber",
                "{0} uses 'Passw0rd' as their password everywhere",
                "{0} forgot to define their non-functionals",
                "{0} uses spaces instead of tabs for indentation",
                "{0}'s default browser is Microsoft Edge"
            };
            Random rnd = new Random();
            string secret = secrets[rnd.Next(secrets.Length)];
            return string.Format(secret, UserName, Faker.Name.First());
        }
    }
}