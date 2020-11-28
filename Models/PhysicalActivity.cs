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
            AEROBIC,
            STRENGTH,
            FLEXIBILITY,
            BALANCE
        }

        #region Fields

        private int _userId;
        private int _repetitions;
        private int _sets;
        private double _weight;
        private double _goal;
        private double _duration;

        private string _exerciseName;

        private ExerciseType _exerciseType;

        #endregion

        #region Properties

        public int UserID 
        {
            get { return _userId; }
            set { _userId = value; }
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

        public double Weight 
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public double Goal 
        {
            get { return _goal; }
            set { _goal = value; }
        }

        public double Duration
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
