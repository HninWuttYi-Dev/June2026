using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using June2026.NLayer.Models;

namespace June2026.NLayer.DAL
{
    public class UserRepository
    {
        private static List<User> _fakeDatabase = new List<User>();
        public void Add (User user)
        {
            _fakeDatabase.Add(user);
        }
        public List<User> GetAll()
        {
            return _fakeDatabase;
        }
    }
}