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
        /// retrieves current user's list of physical activities
        /// </summary>
        public List<PhysicalActivity> GetCurrentUserPhysicalActivities(User user)
        {
            return _dataService.GetListOfActivities(user);
        }

        /// <summary>
        /// retrieves current user's list of tasks
        /// </summary>
        public List<Task> GetCurrentUserTasks(User user)
        {
            return _dataService.GetListOfTasks(user);
        }

        /// <summary>
        /// adds a user to the list
        /// </summary>
        public void Add(User user)
        {
            try
            {
                _dataService.Add(user);
                //_users.Add(user);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// adds a task 
        /// </summary>
        public void AddTask(Task task)
        {
            try
            {
                _dataService.AddTask(task);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// adds a physical activity
        /// </summary>
        public void AddPhysicalActivity(PhysicalActivity activity)
        {
            try
            {
                _dataService.AddPhysicalActivity(activity);
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
                _dataService.DeleteCurrentUserTask(id);
                _dataService.DeleteCurrentUserPhysicalActivity(id);
                _dataService.Delete(id);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// deletes a task 
        /// </summary>
        public void DeleteTask(Task task)
        {
            try
            {
                _dataService.DeleteTask(task);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// adds a physical activity
        /// </summary>
        public void DeletePhysicalActivity(PhysicalActivity activity)
        {
            try
            {
                _dataService.DeletePhysicalActivity(activity);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// updates user
        /// </summary>
        public void Update(User user)
        {
            try
            {
                _dataService.Update(user);

                //_users.Remove(_users.FirstOrDefault(u => u.ID == user.ID));
                //_users.Add(user);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// updates a task 
        /// </summary>
        public void UpdateTask(Task task)
        {
            try
            {
                _dataService.UpdateTask(task);
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// updates a physical activity
        /// </summary>
        public void UpdatePhysicalActivity(PhysicalActivity activity)
        {
            try
            {
                _dataService.UpdatePhysicalActivity(activity);
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
