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
                    Task = new List<string>{"build out my capstone app", "find developer job"},
                    UserTheme = UserTheme.NIGHTFALL,
                    PhysicalActivities = new List<PhysicalActivity>
                                            {
                                                new PhysicalActivity
                                                {
                                                    ID = 1,
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
                                                    ID = 1,
                                                    ExcerciseName = "Squat",
                                                    TypeOfExercise = PhysicalActivity.ExerciseType.STRENGTH,
                                                    Repetitions = 4,
                                                    Sets = 4,
                                                    Weight = 175,
                                                    Goal = 225,
                                                    Duration = 0,
                                                }
                                            }
                }
            };
        }
    }
}
