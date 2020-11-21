using MyWellnessApp.Models;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class WorkoutViewModel : ObservableObject
    {
        #region Fields

        private User _currentUser;
        private ObservableCollection<PhysicalActivity> _currentUserWorkouts;
        private PhysicalActivity _selectedWorkout;

        private string _workoutCategory;

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

        public ObservableCollection<PhysicalActivity> CurrentUserWorkouts 
        {
            get { return _currentUserWorkouts; }
            set 
            {
                _currentUserWorkouts = value;
                OnPropertyChanged(nameof(CurrentUserWorkouts));
            }
        }

        public PhysicalActivity SelectedWorkout 
        {
            get { return _selectedWorkout; }
            set 
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                ConvertCategoryToString();
            }
        }

        public string WorkoutCategory 
        { 
            get { return _workoutCategory; }
            set 
            {
                _workoutCategory = value;
                OnPropertyChanged(nameof(WorkoutCategory));
            }
        }

        #endregion

        #region Constructors

        public WorkoutViewModel(User user)
        {
            CurrentUser = user;
            CurrentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);
        }

        #endregion

        #region Commands

        public ICommand ReturnCommand
        {
            get { return new RelayCommand(new Action<object>(Return)); }
        }

        #endregion

        #region Methods

        private void ConvertCategoryToString() 
        {
            WorkoutCategory = "";
            try
            {
                if (SelectedWorkout != null)
                {
                    WorkoutCategory = SelectedWorkout.TypeOfExercise.ToString();
                }
                else
                {
                    WorkoutCategory = "NONE";
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
                throw;
            }
        }

        private void Return(object obj)
        {
            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = null;
            }
        }

        #endregion
    }
}
