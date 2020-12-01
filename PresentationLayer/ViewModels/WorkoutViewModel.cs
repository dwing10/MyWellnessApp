using MyWellnessApp.BusinessLayer;
using MyWellnessApp.Models;
using MyWellnessApp.PresentationLayer.Views;
using MyWellnessApp.PresentationLayer.Views.UserControls;
using MyWellnessApp.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWellnessApp.PresentationLayer.ViewModels
{
    public class WorkoutViewModel : ObservableObject
    {
        #region Fields

        private MyWellnessAppBusiness _myWellnessAppBusiness;
        private DashboardWindowViewModel _dashboardWindowViewModel;

        private User _currentUser;
        private ObservableCollection<PhysicalActivity> _currentUserWorkouts;
        private ObservableCollection<string> _categoryForFilter;
        private PhysicalActivity _selectedWorkout;

        private string _workoutCategory;
        private string _categoryFilter;

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

        public ObservableCollection<string> CategoryForFilter
        {
            get { return _categoryForFilter; }
            set
            {
                _categoryForFilter = value;
                OnPropertyChanged(nameof(CategoryForFilter));
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
                GenerateProgressBar();
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

        public string CategoryFilter
        {
            get { return _categoryFilter; }
            set
            {
                _categoryFilter = value;
                OnPropertyChanged(nameof(CategoryFilter));
            }
        }

        #endregion

        #region Constructors

        public WorkoutViewModel(User user, DashboardWindowViewModel dashboardWindowViewModel)
        {
            _myWellnessAppBusiness = new MyWellnessAppBusiness();
            _dashboardWindowViewModel = dashboardWindowViewModel;
            _currentUser = user;
            _currentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);
            _categoryForFilter = new ObservableCollection<string>(Enum.GetNames(typeof(PhysicalActivity.ExerciseType)));
        }

        #endregion

        #region Commands

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(new Action<object>(RefreshList)); }
        }

        public ICommand FilterCommand
        {
            get { return new RelayCommand(new Action<object>(OnFilterCategory)); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(new Action<object>(DeleteWorkout)); }
        }

        public ICommand ExitCommand
        {
            get { return new RelayCommand(new Action<object>(Close)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// refreshes the list of user workouts
        /// </summary>
        private void RefreshList(object obj)
        {
            UserWorkouts userWorkouts = new UserWorkouts
            {
                DataContext = this
            };
            _currentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);

            if (obj is System.Windows.Controls.UserControl)
            {
                (obj as System.Windows.Controls.UserControl).Content = userWorkouts;
            }
        }

        /// <summary>
        /// filters workouts based on category
        /// </summary>
        private void OnFilterCategory(object obj)
        {
            if (!String.IsNullOrEmpty(CategoryFilter))
            {
                Enum.TryParse(CategoryFilter, out PhysicalActivity.ExerciseType exerciseType);
                _currentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);
                CurrentUserWorkouts = new ObservableCollection<PhysicalActivity>(_currentUserWorkouts.Where(w => w.TypeOfExercise == exerciseType));
            }
        }

        /// <summary>
        /// deletes the selected workout
        /// </summary>
        private void DeleteWorkout(object obj)
        {
            UserWorkouts userWorkouts = new UserWorkouts
            {
                DataContext = this
            };
            if (SelectedWorkout != null)
            {
                _myWellnessAppBusiness.DeleteExercise(CurrentUser, SelectedWorkout);
                _currentUser.PhysicalActivities.Remove(SelectedWorkout);
                _currentUserWorkouts = new ObservableCollection<PhysicalActivity>(CurrentUser.PhysicalActivities);
                if (obj is System.Windows.Controls.UserControl)
                {
                    (obj as System.Windows.Controls.UserControl).Content = userWorkouts;
                }
                SelectedWorkout = null;
                _dashboardWindowViewModel.SetProgressBar(SelectedWorkout);
            }
        }

        /// <summary>
        /// converts the TypeOfExcercise into a string for display purposes
        /// </summary>
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
        /// generates the progress bar
        /// </summary>
        private void GenerateProgressBar()
        {
            _dashboardWindowViewModel.SetProgressBar(SelectedWorkout);
        }

        #endregion
    }
}
