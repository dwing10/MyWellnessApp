using MyWellnessApp.DataAccessLayer;
using MyWellnessApp.DataAccessLayer.SQL;
using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //SqlUtilities.WriteSeedData();
        }

        #endregion

        #region Methods

        /// <summary>
        /// gets all of the users
        /// </summary>
        public List<User> GetAllUsers()
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
        public User GetUser(int id)
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
        /// adds a workout to the user
        /// </summary>
        public void AddExerciseToUser(User user, PhysicalActivity physicalActivity)
        {
            try
            {
                if (physicalActivity != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.AddPhysicalActivity(physicalActivity);
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
            //user.PhysicalActivities.Add(physicalActivity);
        }

        /// <summary>
        /// adds a task to the user
        /// </summary>
        public void AddTaskToUser(User user, Task task)
        {
            try
            {
                if (task != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.AddTask(task);
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

            //user.Task.Add(task);
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
        /// removes excersise from user
        /// </summary>
        public void DeleteExercise(User user, PhysicalActivity physicalActivity)
        {
            try
            {
                if (physicalActivity != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.DeletePhysicalActivity(physicalActivity);
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

            //user.PhysicalActivities.Remove(physicalActivity);
        }

        /// <summary>
        /// removes task from user
        /// </summary>
        public void DeleteTask(User user, Task task)
        {
            try
            {
                if (task != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.DeleteTask(task);
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

            //user.Task.Remove(task);
        }

        /// <summary>
        /// edits user excersise
        /// </summary>
        public void EditExercise(User user, PhysicalActivity physicalActivity)
        {
            try
            {
                if (physicalActivity != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.UpdatePhysicalActivity(physicalActivity);
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

            //user.PhysicalActivities.Add(physicalActivity);
        }

        /// <summary>
        /// edits user task
        /// </summary>
        /// <param name="user"></param>
        public void EditTask(User user, Task task)
        {
            try
            {
                if (task != null)
                {
                    using (UserRepository userRepository = new UserRepository())
                    {
                        userRepository.UpdateTask(task);
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

            //user.Task.Add(task);
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
