using MyWellnessApp.DataAccessLayer;
using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.BusinessLayer
{
    public class MyWellnessAppBusiness
    {
        #region Properties

        public FileIoMessage FileIOStatus { get; set; }

        #endregion

        #region Constructors

        public MyWellnessAppBusiness() 
        { 
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// gets all of the users
        /// </summary>
        private List<User> GetAllUsers() 
        {
            List<User> user = null;
            FileIOStatus = FileIoMessage.None;

            try
            {
                using (UserRepository userRepository = new UserRepository())
                {
                    user = userRepository.GetAll() as List<User>;
                }
                if (user != null)
                {
                    FileIOStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIOStatus = FileIoMessage.NoRecordsFound;
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                FileIOStatus = FileIoMessage.FileAccessError;
                throw;
            }

            return user;
        }

        /// <summary>
        /// gets user by id
        /// </summary>
        private User GetUser(int id) 
        {
            User user = null;
            FileIOStatus = FileIoMessage.None;

            try
            {
                using (UserRepository userRepository = new UserRepository())
                {
                    user = userRepository.GetByID(id);
                }
                if (user != null)
                {
                    FileIOStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIOStatus = FileIoMessage.NoRecordsFound;
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                FileIOStatus = FileIoMessage.FileAccessError;
                throw;
            }

            return user;
        }


        /// <summary>
        /// retreives all users from seed data or from data path
        /// </summary>
        public List<User> RetreiveAllUserFromDataPath() 
        {
            return SeedData.GetAllUsers();

            //return GetUsers();
        }

        /// <summary>
        /// Adds a new user
        /// </summary>
        public void AddUser(User user) 
        {
            try
            {
                if (user != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.Add(user);
                    }
                    FileIOStatus = FileIoMessage.Complete;
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                FileIOStatus = FileIoMessage.FileAccessError;
                throw;
            }
        }

        /// <summary>
        /// deletes a user by ID
        /// </summary>
        public void DeleteUser(int id) 
        {
            try
            {
                if (GetUser(id) != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.Delete(id);
                    }
                    FileIOStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIOStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                FileIOStatus = FileIoMessage.FileAccessError;
                throw;
            }
        }

        /// <summary>
        /// updates a user by the ID
        /// </summary>
        public void UpdateUser(User userToUpdate) 
        {
            try
            {
                if (GetUser(userToUpdate.ID) != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.Update(userToUpdate);
                    }
                    FileIOStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIOStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                FileIOStatus = FileIoMessage.FileAccessError;
                throw;
            }
        }
        #endregion
    }
}
