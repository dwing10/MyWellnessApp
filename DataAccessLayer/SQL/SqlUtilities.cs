using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using MyWellnessApp.Models;

namespace MyWellnessApp.DataAccessLayer.SQL
{
    public class SqlUtilities
    {
        /// <summary>
        /// writes seed data to the database
        /// </summary>
        public static bool WriteSeedData()
        {
            bool operationSuccessful = true;

            try
            {
                DeleteAllTaskRecords();
                DeleteAllPhysicalActivityRecords();
                DeleteAllUserRecords();
                AddAllUserRecords();
                AddAllTaskRecords();
                AddAllPhysicalActivityRecords();
            }
            catch (Exception e)
            {
                operationSuccessful = false;
                string m = e.Message;
                throw;
            }

            return operationSuccessful;
        }

        /// <summary>
        /// removes all users
        /// </summary>
        private static bool DeleteAllUserRecords()
        {
            bool operationSuccessful = true;
            string connectionString = SqlDataSettings.ConnectionString;
            string commandString = "DELETE FROM Users";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlDataAdapter.DeleteCommand = new SqlCommand(commandString, sqlConnection);
                    sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string m = e.Message;
                    operationSuccessful = false;
                    throw;
                }
            }

            return operationSuccessful;
        }


        /// <summary>
        /// removes all tasks
        /// </summary>
        private static bool DeleteAllTaskRecords()
        {
            bool status = true;
            string connectionString = SqlDataSettings.ConnectionString;
            string commandString = "DELETE FROM Tasks";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlDataAdapter.DeleteCommand = new SqlCommand(commandString, sqlConnection);
                    sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string m = e.Message;
                    status = false;
                    throw;
                }
            }


            return status;
        }


        /// <summary>
        /// removes all PhysicalActivities
        /// </summary>
        private static bool DeleteAllPhysicalActivityRecords()
        {
            bool status = true;
            string connectionString = SqlDataSettings.ConnectionString;
            string commandString = "DELETE FROM PhysicalActivities";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlConnection.Open();
                    sqlDataAdapter.DeleteCommand = new SqlCommand(commandString, sqlConnection);
                    sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string m = e.Message;
                    status = false;
                    throw;
                }
            }


            return status;
        }

        /// <summary>
        /// adds users to sql database
        /// </summary>
        private static bool AddAllUserRecords()
        {
            string connectionString = SqlDataSettings.ConnectionString;
            bool operationSuccessful = true;

            foreach (var user in SeedData.GetAllUsers())
            {
                var sb = new StringBuilder("SET IDENTITY_INSERT Users ON");
                sb.Append(" ");
                sb.Append("INSERT INTO Users");
                sb.Append("([ID], [Name], [Username], [Password])");
                sb.Append("Values (");
                sb.Append("'").Append(user.ID).Append("',");
                sb.Append("'").Append(user.Name).Append("',");
                sb.Append("'").Append(user.UserName).Append("',");
                sb.Append("'").Append(user.Password).Append("')");
                sb.Append(" ");
                sb.Append("SET IDENTITY_INSERT Users OFF");

                string sqlCommandString = sb.ToString();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                        sqlConnection.Open();
                        sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConnection);
                        sqlAdapter.DeleteCommand.ExecuteNonQuery();
                    }
                    catch (Exception msg)
                    {
                        var exceptionMessage = msg.Message;
                        operationSuccessful = false;
                        throw;
                    }
                }
            }

            return operationSuccessful;
        }

        /// <summary>
        /// adds tasks to sql database
        /// </summary>
        private static bool AddAllTaskRecords()
        {
            string connectionString = SqlDataSettings.ConnectionString;
            List<Task> UserTasks = new List<Task>();
            bool operationSuccessful = true;

            foreach (var user in SeedData.GetAllUsers())
            {
                foreach (var item in user.Task)
                {
                    UserTasks.Add(item);
                }
            }

            foreach (var task in UserTasks)
            {
                var sb = new StringBuilder("INSERT INTO Tasks");
                sb.Append("([UserID], [Content], [Date])");
                sb.Append("Values (");
                sb.Append("'").Append(task.UserId).Append("',");
                sb.Append("'").Append(task.Content).Append("',");
                sb.Append("'").Append(task.Date).Append("')");

                string sqlCommandString = sb.ToString();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                        sqlConnection.Open();
                        sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConnection);
                        sqlAdapter.DeleteCommand.ExecuteNonQuery();
                    }
                    catch (Exception msg)
                    {
                        var exceptionMessage = msg.Message;
                        operationSuccessful = false;
                        throw;
                    }
                }
            }

            return operationSuccessful;
        }

        /// <summary>
        /// adds PhysicalActivities to sql database
        /// </summary>
        private static bool AddAllPhysicalActivityRecords()
        {
            string connectionString = SqlDataSettings.ConnectionString;
            List<PhysicalActivity> userActivities = new List<PhysicalActivity>();
            bool operationSuccessful = true;

            foreach (var user in SeedData.GetAllUsers())
            {
                foreach (var item in user.PhysicalActivities)
                {
                    userActivities.Add(item);
                }
            }

            foreach (var activity in userActivities)
            {
                var sb = new StringBuilder("INSERT INTO PhysicalActivities");
                sb.Append("([UserID], [ExcersiseName], [Reps], [Sets], [Weight], [Duration], [Goal], [ExcersiseType])");
                sb.Append("Values (");
                sb.Append("'").Append(activity.UserID).Append("',");
                sb.Append("'").Append(activity.ExcerciseName).Append("',");
                sb.Append("'").Append(activity.Repetitions).Append("',");
                sb.Append("'").Append(activity.Sets).Append("',");
                sb.Append("'").Append(activity.Weight).Append("',");
                sb.Append("'").Append(activity.Duration).Append("',");
                sb.Append("'").Append(activity.Goal).Append("',");
                sb.Append("'").Append(activity.TypeOfExercise.ToString()).Append("')");

                string sqlCommandString = sb.ToString();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                        sqlConnection.Open();
                        sqlAdapter.DeleteCommand = new SqlCommand(sqlCommandString, sqlConnection);
                        sqlAdapter.DeleteCommand.ExecuteNonQuery();
                    }
                    catch (Exception msg)
                    {
                        var exceptionMessage = msg.Message;
                        operationSuccessful = false;
                        throw;
                    }
                }
            }

            return operationSuccessful;
        }
    }
}
