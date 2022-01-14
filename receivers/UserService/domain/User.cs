using Faker;

namespace receiver.domain
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public User(string name)
        {
            this.Name = name;
            this.Age = Faker.RandomNumber.Next(18, 100);
            this.City = Faker.Address.City();
        }
    }
}