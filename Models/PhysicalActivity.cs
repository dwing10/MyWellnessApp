using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.Models
{
    public class PhysicalActivity
    {
        public enum ExerciseType
        {
            NONE,
            CARDIO,
            STRENGTH
        }

        #region Fields

        private int _id;
        private int _repetitions;
        private int _sets;
        private int _weight;
        private int _goal;
        private int _duration;

        private string _exerciseName;

        private ExerciseType _exerciseType;

        #endregion

        #region Properties

        public int ID 
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Repetitions 
        {
            get { return _repetitions; }
            set { _repetitions = value; }
        }

        public int Sets 
        {
            get { return _sets; }
            set { _sets = value; }
        }

        public int Weight 
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public int Goal 
        {
            get { return _goal; }
            set { _goal = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public string ExcerciseName 
        {
            get { return _exerciseName; }
            set { _exerciseName = value; }
        }

        public ExerciseType TypeOfExercise 
        {
            get { return _exerciseType; }
            set { _exerciseType = value; }
        }
        #endregion

        #region Constructors



        #endregion

        #region Methods

        #endregion

    }
}
