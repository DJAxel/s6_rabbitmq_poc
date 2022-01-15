using System;
using System.Collections.Generic;
using System.Linq;
using UserSecretService.domain;

namespace UserSecretService
{
    internal class SecretRepository
    {
        private static SecretRepository instance;
        private List<UserSecret> secretList = new List<UserSecret>();

        public static SecretRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretRepository();
                }
                return instance;
            }
        }

        SecretRepository()
        {

        }

        internal string add(string name)
        {
            int amount = new Random().Next(1, 3);
            for (int i = 0; i < amount; i++ ) {
                UserSecret secret = new UserSecret(name);
                this.secretList.Add(secret);
            }
            return string.Format("User '{0}' added with {1} secrets", name, amount);
        }

        internal string delete(string name)
        {
            int amountDeleted = this.secretList.RemoveAll(s => s.User.Name == name);
            if (amountDeleted > 0)
            {
                return string.Format("{0} secrets removed for user '{1}'.", amountDeleted, name);
            }
            return string.Format("There are no secrets for user '{0}' to remove.", name);
        }

        public UserSecret[] getAll()
        {
            return this.secretList.ToArray();
        }

        internal void Seed()
        {
            String[] names = new String[] {"Axel", "Brad", "Charlie", "Donna", "Evita"};
            foreach (string name in names) {
                this.add(name);
            }
        }
    }
}