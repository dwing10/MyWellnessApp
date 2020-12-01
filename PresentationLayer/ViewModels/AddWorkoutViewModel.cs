using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MyWellnessApp.PresentationLayer.Views;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class AddWorkoutViewModel : ObservableObject
    {
        #region Fields

        private WorkoutViewModel _workoutViewModel;
        private User _currentUser;
        private MyWellnessAppBusiness _myWellnessAppBusiness;

        private string _nameToAdd;
        private string _categoryToAdd;
        private string _message;

        private int _repsToAdd;
        private int _setsToAdd;

        private double _weightToAdd;
        private double _durationToAdd;
        private double _goalToAdd;

        private PhysicalActivity.ExerciseType _exerciseTypeToAdd;
        private ObservableCollection<string> _category;

        #endregion

        #region Properties

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public string NameToAdd
        {
            get { return _nameToAdd; }
            set
            {
                _nameToAdd = value;
                OnPropertyChanged(nameof(NameToAdd));
            }
        }

        public string CategoryToAdd
        {
            get { return _categoryToAdd; }
            set
            {
                _categoryToAdd = value;
                OnPropertyChanged(nameof(CategoryToAdd));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public int RepsToAdd
        {
            get { return _repsToAdd; }
            set
            {
                _repsToAdd = value;
                OnPropertyChanged(nameof(RepsToAdd));
            }
        }

        public int SetsToAdd
        {
            get { return _setsToAdd; }
            set
            {
                _setsToAdd = value;
                OnPropertyChanged(nameof(SetsToAdd));
            }
        }

        public double WeightToAdd
        {
            get { return _weightToAdd; }
            set
            {
                _weightToAdd = value;
                OnPropertyChanged(nameof(WeightToAdd));
            }
        }

        public double DurationToAdd
        {
            get { return _durationToAdd; }
            set
            {
                _durationToAdd = value;
                OnPropertyChanged(nameof(DurationToAdd));
            }
        }

        public double GoalToAdd
        {
            get { return _goalToAdd; }
            set
            {
                _goalToAdd = value;
                OnPropertyChanged(nameof(GoalToAdd));
            }
        }

        public PhysicalActivity.ExerciseType ExcerciseTypeToAdd
        {
            get { return _exerciseTypeToAdd; }
            set
            {
                _exerciseTypeToAdd = value;
                OnPropertyChanged(nameof(ExcerciseTypeToAdd));
            }
        }

        public ObservableCollection<string> Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        #endregion

        #region Constructors

        public AddWorkoutViewModel(User user)
        {
            //_workoutViewModel = new WorkoutViewModel(user);
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _currentUser = user;
            _category = new ObservableCollection<string>(Enum.GetNames(typeof(PhysicalActivity.ExerciseType)));
        }

        #endregion

        #region Commands

        public ICommand AddWorkoutCommand
        {
            get { return new RelayCommand(new Action<object>(AddWorkout)); }
        }

        public ICommand ClearCommand
        {
            get { return new RelayCommand(new Action<object>(Clear)); }
        }

        public ICommand ExitCommand
        {
            get { return new RelayCommand(new Action<object>(Close)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// adds a workout to user 
        /// </summary>
        private void AddWorkout(object obj)
        {
            AddWorkout addWorkout = new AddWorkout
            {
                DataContext = this
            };
            PhysicalActivity physicalActivity = CreatePhysicalActivity();
            try
            {
                _myWellnessAppBusiness.AddExerciseToUser(CurrentUser, physicalActivity);
                _currentUser.PhysicalActivities.Add(physicalActivity);
                ResetInputBoxes();
                Message = "Success!";
                if (obj is System.Windows.Controls.UserControl)
                {
                    (obj as System.Windows.Controls.UserControl).Content = addWorkout;
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        /// <summary>
        /// clears the form
        /// </summary>
        private void Clear(object obj) 
        {
            AddWorkout addWorkout = new AddWorkout
            {
                DataContext = this
            };
            Message = null;
            ResetInputBoxes();
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = addWorkout;
            }
        }

        /// <summary>
        /// resets the UserControl's content
        /// </summary>
        private void Close(object obj)
        {
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = null;
            }
        }

        /// <summary>
        /// creates the PhysicalActivity that is to be added
        /// </summary>
        private PhysicalActivity CreatePhysicalActivity()
        {
            Enum.TryParse(CategoryToAdd, out PhysicalActivity.ExerciseType excersiseType);
            return new PhysicalActivity
            {
                UserID = CurrentUser.ID,
                ExcerciseName = NameToAdd,
                TypeOfExercise = excersiseType,
                Repetitions = RepsToAdd,
                Sets = SetsToAdd,
                Weight = WeightToAdd,
                Duration = DurationToAdd,
                Goal = GoalToAdd
            };
        }

        /// <summary>
        /// resets the input boxes
        /// </summary>
        private void ResetInputBoxes() 
        {
            NameToAdd = null;
            CategoryToAdd = null;
            RepsToAdd = 0;
            SetsToAdd = 0;
            WeightToAdd = 0;
            DurationToAdd = 0;
            GoalToAdd = 0;
        }

        #endregion
    }
}
