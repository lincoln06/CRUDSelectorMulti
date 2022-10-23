using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDSelectorMulti
{
    class MongoCRUD : ICrud
    {
        readonly IMongoCollection<User> collection = new MongoClient().GetDatabase("Service").GetCollection<User>("Users");
        public void Register(User user)
        {
            collection.InsertOne(user);
        }
        public User Login(string email, string password)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq("Email", email),
                Builders<User>.Filter.Eq("Password", password));
            try
            {
                var record = (User)collection.Find(filter).First();

                return record;
            }
            catch
            {
                return null;
            }
        }
    }
}

