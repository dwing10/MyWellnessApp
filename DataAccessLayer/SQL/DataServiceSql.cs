using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MyWellnessApp.DataAccessLayer.SQL
{
    class DataServiceSql : IDataService
    {

        #region Fields

        private List<User> _users;
        private List<Task> _tasks;
        private List<PhysicalActivity> _activities;

        #endregion

        #region Constructor

        public DataServiceSql()
        {
            DataSet user_ds = GetUserDataSet();
            DataSet task_ds = GetTasksDataSet();
            DataSet activities_ds = GetActivitiesDataSet();
            _users = GetUser(user_ds);
            _tasks = GetTasks(task_ds);
            _activities = GetActivities(activities_ds);
        }

        #endregion

        #region Methods

        private List<User> GetUser(DataSet user_ds)
        {
            DataTable user_dt = user_ds.Tables["Users"];

            List<User> user = (from u in user_dt.AsEnumerable()
                               select new User()
                               {
                                   ID = Convert.ToInt32(u["ID"]),
                                   Name = Convert.ToString(u["Name"]),
                                   UserName = Convert.ToString(u["Username"]),
                                   Password = Convert.ToString(u["Password"])
                               }).ToList();

            return user;
        }

        /// <summary>
        /// gets list of tasks from database
        /// </summary>
        private List<Task> GetTasks(DataSet task_ds)
        {
            DataTable task_dt = task_ds.Tables["Tasks"];

            List<Task> tasks = (from t in task_dt.AsEnumerable()
                                select new Task()
                                {
                                    ID = Convert.ToInt32(t["TaskID"]),
                                    UserId = Convert.ToInt32(t["UserID"]),
                                    Content = Convert.ToString(t["Content"]),
                                    Date = Convert.ToDateTime(t["Date"])
                                }).ToList();

            return tasks;
        }

        /// <summary>
        /// gets list of PhysicalActivities from database
        /// </summary>
        private List<PhysicalActivity> GetActivities(DataSet activities_ds)
        {
            DataTable activities_dt = activities_ds.Tables["PhysicalActivities"];

            List<PhysicalActivity> activities = (from a in activities_dt.AsEnumerable()
                                select new PhysicalActivity()
                                {
                                    ID = Convert.ToInt32(a["ID"]),
                                    UserID = Convert.ToInt32(a["UserID"]),
                                    ExcerciseName = Convert.ToString(a["ExcersiseName"]),
                                    Repetitions = Convert.ToInt32(a["Reps"]),
                                    Sets = Convert.ToInt32(a["Sets"]),
                                    Weight = Convert.ToDouble(a["Weight"]),
                                    Duration = Convert.ToDouble(a["Duration"]),
                                    Goal = Convert.ToDouble(a["Goal"]),
                                    TypeOfExercise = ConvertToType(a["ExcersiseType"])
                                }).ToList();

            return activities;
        }

        /// <summary>
        /// connects to database and returns user table
        /// </summary>
        private DataSet GetUserDataSet()
        {
            DataSet user_dataset = new DataSet();

            string connectionString = SqlDataSettings.ConnectionString;
            string command = "SELECT * FROM Users";

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConn);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlConn.Open();

                    sqlDataAdapter.Fill(user_dataset, "Users");
                }
                catch (SqlException sqlException)
                {
                    var exceptionMessage = sqlException.Message;
                    throw;
                }
            }

            return user_dataset;
        }

        /// <summary>
        /// connects to database and returns task table
        /// </summary>
        private DataSet GetTasksDataSet()
        {
            DataSet tasks_dataset = new DataSet();

            string connectionString = SqlDataSettings.ConnectionString;
            string command = "SELECT * FROM Tasks";

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConn);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlConn.Open();

                    sqlDataAdapter.Fill(tasks_dataset, "Tasks");
                }
                catch (SqlException sqlException)
                {
                    var exceptionMessage = sqlException.Message;
                    throw;
                }
            }

            return tasks_dataset;
        }

        /// <summary>
        /// connects to database and returns PhysicalActivities table
        /// </summary>
        private DataSet GetActivitiesDataSet()
        {
            DataSet activities_dataset = new DataSet();

            string connectionString = SqlDataSettings.ConnectionString;
            string command = "SELECT * FROM PhysicalActivities";

            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConn);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlConn.Open();

                    sqlDataAdapter.Fill(activities_dataset, "PhysicalActivities");
                }
                catch (SqlException sqlException)
                {
                    var exceptionMessage = sqlException.Message;
                    throw;
                }
            }

            return activities_dataset;
        }

        /// <summary>
        /// converts data from sql into enum
        /// </summary>
        private PhysicalActivity.ExerciseType ConvertToType(object type)
        {
            PhysicalActivity.ExerciseType activity = new PhysicalActivity.ExerciseType();
            string typeString = Convert.ToString(type);

            Enum.TryParse(typeString, out activity);

            return activity;
        }

        /// <summary>
        /// returns all users
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        /// <summary>
        /// retrieves user by ID
        /// </summary>
        public User GetByID(int id)
        {
            List<Task> tasksToAdd = new List<Task>();
            List<PhysicalActivity> activitiesToAdd = new List<PhysicalActivity>();
            User user = _users.FirstOrDefault(u => u.ID == id);

            foreach (var task in _tasks)
            {
                if (task.UserId == user.ID)
                {
                    tasksToAdd.Add(task);
                }
            }

            foreach (var activity in _activities)
            {
                if (activity.UserID == user.ID)
                {
                    activitiesToAdd.Add(activity);
                }
            }

            user.Task = tasksToAdd;
            user.PhysicalActivities = activitiesToAdd;

            return user;
        }

        /// <summary>
        /// add user to the database
        /// </summary>
        public void Add(User user)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("INSERT INTO Users");
            sb.Append("([Name], [Username], [Password])");
            sb.Append("VALUES (");
            sb.Append("'").Append(user.Name).Append("',");
            sb.Append("'").Append(user.UserName).Append("',");
            sb.Append("'").Append(user.Password).Append("')");

            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// add task to database
        /// </summary>
        public void AddTask(Task task) 
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("INSERT INTO Tasks");
            sb.Append("([UserID], [Content], [Date])");
            sb.Append("VALUES (");
            sb.Append("'").Append(task.UserId).Append("',");
            sb.Append("'").Append(task.Content).Append("',");
            sb.Append("'").Append(task.Date).Append("')");

            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// add Physical Activity to database
        /// </summary>
        public void AddPhysicalActivity(PhysicalActivity activity)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("INSERT INTO PhysicalActivities");
            sb.Append("([UserID], [ExcersiseName], [Reps], [Sets], [Weight], [Duration], [Goal], [ExcersiseType])");
            sb.Append("VALUES (");
            sb.Append("'").Append(activity.UserID).Append("',");
            sb.Append("'").Append(activity.ExcerciseName).Append("',");
            sb.Append("'").Append(activity.Repetitions).Append("',");
            sb.Append("'").Append(activity.Sets).Append("',");
            sb.Append("'").Append(activity.Weight).Append("',");
            sb.Append("'").Append(activity.Duration).Append("',");
            sb.Append("'").Append(activity.Goal).Append("',");
            sb.Append("'").Append(activity.TypeOfExercise.ToString()).Append("')");

            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// deletes user by ID
        /// </summary>
        public void Delete(int id)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("DELETE FROM Users");
            sb.Append(" WHERE ID = ").Append(id);
            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// deletes task from user
        /// </summary>
        public void DeleteTask(Task task)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("DELETE FROM Tasks");
            sb.Append(" WHERE TaskID = ").Append(task.ID);
            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// deletes PhysicalACtivity from user
        /// </summary>
        public void DeletePhysicalActivity(PhysicalActivity activity)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("DELETE FROM PhysicalActivities");
            sb.Append(" WHERE ID = ").Append(activity.ID);
            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// updates user
        /// </summary>
        public void Update(User user)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("UPDATE Users SET ");
            sb.Append("Name = '").Append(user.Name).Append("', ");
            sb.Append("Username = '").Append(user.UserName).Append("', ");
            sb.Append("Password = '").Append(user.Password).Append("' ");
            sb.Append(" WHERE ID = ").Append(user.ID);

            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// updates task
        /// </summary>
        public void UpdateTask(Task task)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("UPDATE Tasks SET ");
            sb.Append("Content = '").Append(task.Content).Append("', ");
            sb.Append("Date = '").Append(task.Date).Append("' ");
            sb.Append(" WHERE TaskID = ").Append(task.ID);

            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        /// <summary>
        /// updates Physical Activity
        /// </summary>
        public void UpdatePhysicalActivity(PhysicalActivity activity)
        {
            string connectionString = SqlDataSettings.ConnectionString;

            var sb = new StringBuilder("UPDATE PhysicalActivities SET ");
            sb.Append("ExcersiseName = '").Append(activity.ExcerciseName).Append("', ");
            sb.Append("Reps = '").Append(activity.Repetitions).Append("', ");
            sb.Append("Sets = '").Append(activity.Sets).Append("', ");
            sb.Append("Weight = '").Append(activity.Weight).Append("', ");
            sb.Append("Duration = '").Append(activity.Duration).Append("', ");
            sb.Append("Goal = '").Append(activity.Goal).Append("', ");
            sb.Append("ExcersiseType = '").Append(activity.Repetitions.ToString()).Append("' ");
            sb.Append(" WHERE ID = ").Append(activity.ID);

            string commandString = sb.ToString();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlAdapter.InsertCommand = new SqlCommand(commandString, sqlConnection);
                    sqlAdapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception msg)
                {
                    var exceptionMessage = msg.Message;
                    throw;
                }
            }
        }

        public void WriteAll(IEnumerable<User> user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ReadAll()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
