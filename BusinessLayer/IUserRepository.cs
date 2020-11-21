using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.BusinessLayer
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByID(int id);
        void Add(User user);
        void Delete(int id);
    }
}
