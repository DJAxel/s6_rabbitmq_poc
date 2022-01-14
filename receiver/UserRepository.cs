using System;
using System.Collections.Generic;
using System.Linq;
using receiver.domain;

namespace receiver
{
    internal class UserRepository
    {
        private static UserRepository instance;
        private List<User> userList = new List<User>();

        public static UserRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserRepository();
                }
                return instance;
            }
        }

        UserRepository()
        {

        }

        internal string add(string name)
        {
            User user = new User(name);
            this.userList.Add(user);
            return string.Format("User '{0}' added with age '{1}' and city '{2}'", user.Name, user.Age, user.City);
        }

        internal string delete(string name)
        {
            var userToRemove = this.userList.SingleOrDefault(u => u.Name == name);
            if (userToRemove != null)
            {
                this.userList.Remove(userToRemove);
                return string.Format("User '{0}' has been deleted.", userToRemove.Name);
            }
            return string.Format("There is no user named '{0}' to remove.", name);
        }

        public User[] getAll()
        {
            return this.userList.ToArray();
        }
    }
}