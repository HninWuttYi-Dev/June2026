using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using June2026.NLayer.DAL;
using June2026.NLayer.Models;

namespace June2026.NLayer.BLL
{
    public class UserService
    {
        private UserRepository _repository = new UserRepository();
        public void RegisterUser(string name, string email)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name is required");
            }
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email is required");
            }
           User newUser = new User
           {
               Id= new Random().Next(1,1000),
               Name = name,
               Email = email
           };
           _repository.Add(newUser);
        }
        public List<User> GetUserList()
        {
            return _repository.GetAll();
        }
    }
}