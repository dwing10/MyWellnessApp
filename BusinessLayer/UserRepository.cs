using MyWellnessApp.DataAccessLayer;
using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWellnessApp.BusinessLayer
{
    class UserRepository : IUserRepository, IDisposable
    {
        private IDataService _dataService;
        List<User> _users;

        public UserRepository()
        {
            DataConfig dataConfig = new DataConfig();
            _dataService = dataConfig.SetDataService();
            try
            {
                _users = _dataService.GetAll() as List<User>;
                //_users = SeedData.GetAllUsers();
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// retreives all of the Users
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        /// <summary>
        /// retreives user by the ID
        /// </summary>
        public User GetByID(int id)
        {
           return _dataService.GetByID(id);
            //return _users.FirstOrDefault(u => u.ID == id);
        }

        /// <summary>
        /// adds a user to the list
        /// </summary>
        public void Add(User user)
        {
            try
            {
                _users.Add(user);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// deletes a user by id
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                _users.Remove(_users.FirstOrDefault(u => u.ID == id));
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        public void Update(User user)
        {
            try
            {
                //_dataService.Update(pokemon);

                _users.Remove(_users.FirstOrDefault(u => u.ID == user.ID));
                _users.Add(user);
                //_dataService.WriteAll(_pokemon);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        public void Dispose()
        {
            _dataService = null;
            _users = null;
        }
    }
}
