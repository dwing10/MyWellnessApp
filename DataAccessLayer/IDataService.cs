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

        void AddTask(Task task);

        void AddPhysicalActivity(PhysicalActivity activity);

        void Update(User user);

        void UpdateTask(Task task);

        void UpdatePhysicalActivity(PhysicalActivity activity);

        List<Task> GetListOfTasks(User user);

        List<PhysicalActivity> GetListOfActivities(User user);

        void Delete(int id);

        void DeleteTask(Task task);

        void DeleteCurrentUserTask(int id);

        void DeletePhysicalActivity(PhysicalActivity activity);

        void DeleteCurrentUserPhysicalActivity(int id);
    }
}
