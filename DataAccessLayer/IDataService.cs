using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.DataAccessLayer
{
    public interface IDataService
    {
        IEnumerable<User> ReadAll();

        IEnumerable<User> GetAll();

        void WriteAll(IEnumerable<User> user);

        User GetByID(int id);

        void Add(User user);

        void Update(User user);

        void Delete(int id);
    }
}
