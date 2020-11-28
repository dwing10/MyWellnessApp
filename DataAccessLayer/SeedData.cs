using MyWellnessApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.DataAccessLayer
{
    public class SeedData
    {
        public static List<User> GetAllUsers()
        {
            return new List<User>
            {
                new User
                {
                    ID = 1,
                    Name = "Devin",
                    UserName = "dwing",
                    Password = "password",
                    Task = new List<Task>
                    {
                    new Task{UserId = 1 , Content = "build out my capstone app", Date = new DateTime(2020, 11, 3) },
                    new Task{UserId = 1, Content = "find developer job", Date = DateTime.Now}
                    },
                    PhysicalActivities = new List<PhysicalActivity>
                                            {
                                                new PhysicalActivity
                                                {
                                                    UserID = 1,
                                                    ExcerciseName = "Bent Over Row",
                                                    TypeOfExercise = PhysicalActivity.ExerciseType.STRENGTH,
                                                    Repetitions = 4,
                                                    Sets = 4,
                                                    Weight = 175,
                                                    Goal = 185,
                                                    Duration = 0
                                                },
                                                new PhysicalActivity
                                                {
                                                    UserID = 1,
                                                    ExcerciseName = "Squat",
                                                    TypeOfExercise = PhysicalActivity.ExerciseType.STRENGTH,
                                                    Repetitions = 4,
                                                    Sets = 4,
                                                    Weight = 175,
                                                    Goal = 225,
                                                    Duration = 0,
                                                },
                                                 new PhysicalActivity
                                                {
                                                    UserID = 1,
                                                    ExcerciseName = "Cooldown Run",
                                                    TypeOfExercise = PhysicalActivity.ExerciseType.AEROBIC,
                                                    Repetitions = 0,
                                                    Sets = 0,
                                                    Weight = 0,
                                                    Goal = 0,
                                                    Duration = 10.0,
                                                }
                                            }
                }
            };
        }
    }
}
